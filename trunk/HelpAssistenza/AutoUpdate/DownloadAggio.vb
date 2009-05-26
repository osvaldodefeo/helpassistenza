Imports System.Net
Imports System.IO

Friend Class DownloadAggio

    'Thanks to http://www.vbforums.com/showthread.php?t=396260 for the progressbar download method

    Public Event AmountDownloadedChanged(ByVal iNewProgress As Long)
    Public Event FileDownloadSizeObtained(ByVal iFileSize As Long)
    Public Event FileDownloadComplete()
    Public Event FileDownloadFailed(ByVal ex As Exception)

    Private filesize As Long

    'Vars in the dialog
    Public vars_unzipdir As String
    Public vars_downloadlocatie As String
    Public vars_versielocatie As String
    Public vars_bericht As String = "C'è un Aggiornamento Disponibile. Vuoi scaricarlo adessso?"
    Public vars_berichttitel As String = "Helpassistenza - AutoUpdate"

    Private mCurrentFile As String = String.Empty

    Public Function CheckForUpdates(ByVal Location As String)
        Try
            Dim temppath = IIf(Environ$("tmp") <> "", Environ$("tmp"), Environ$("temp")) 'Get temp path
            Dim rand = Int((10000 - 1 + 1) * Rnd()) + 1 'Generate random number to have an unique filename
            Dim dldname = Path.GetFileName(Location) 'Get filename of file to be downloaded
            DownloadFileWithProgress(Location, temppath.ToString + "\" + rand.ToString + dldname) 'Download the file
            Dim a = New Ionic.Zip.ZipFile(temppath.ToString + "\" + rand.ToString + dldname) 'Create new var with zip file
            For Each zipfile As Ionic.Zip.ZipEntry In a 'for each loop for every file in zip
                If My.Computer.FileSystem.FileExists(vars_unzipdir + "\" + zipfile.FileName) Then 'If this file already exist, delete it
                    My.Computer.FileSystem.DeleteFile(vars_unzipdir + "\" + zipfile.FileName)
                End If
                zipfile.Extract(vars_unzipdir, True) 'Extract this file
            Next
            a.Dispose()
        Catch
            MessageBox.Show("Impossibile Contattare Server di Aggiornamento !", "HelpAssistenza - AutoUpdate", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles Me.AmountDownloadedChanged
        ProgressBar1.Value = Convert.ToInt32(iNewProgress)
    End Sub

    Private ReadOnly Property CurrentFile() As String
        Get
            Return mCurrentFile
        End Get
    End Property

    Private Function DownloadFile(ByVal URL As String, ByVal Location As String) As Boolean
        Try
            mCurrentFile = GetFileName(URL)
            Dim WC As New WebClient
            WC.DownloadFile(URL, Location)
            RaiseEvent FileDownloadComplete()
            Return True
        Catch ex As Exception
            RaiseEvent FileDownloadFailed(ex)
            Return False
        End Try
    End Function

    Private Function GetFileName(ByVal URL As String) As String
        Try
            Return URL.Substring(URL.LastIndexOf("/") + 1)
        Catch ex As Exception
            Return URL
        End Try
    End Function

    Private Function DownloadFileWithProgress(ByVal URL As String, ByVal Location As String) As Boolean
        Dim FS As FileStream = Nothing
        Try
            mCurrentFile = GetFileName(URL)
            Dim wRemote As WebRequest
            Dim bBuffer As Byte()
            ReDim bBuffer(256)
            Dim iBytesRead As Integer
            Dim iTotalBytesRead As Integer

            FS = New FileStream(Location, FileMode.Create, FileAccess.Write)
            wRemote = WebRequest.Create(URL)
            Dim myWebResponse As WebResponse = wRemote.GetResponse
            RaiseEvent FileDownloadSizeObtained(myWebResponse.ContentLength)
            Dim sChunks As Stream = myWebResponse.GetResponseStream
            Do
                iBytesRead = sChunks.Read(bBuffer, 0, 256)
                FS.Write(bBuffer, 0, iBytesRead)
                iTotalBytesRead += iBytesRead
                If myWebResponse.ContentLength < iTotalBytesRead Then
                    RaiseEvent AmountDownloadedChanged(myWebResponse.ContentLength)
                Else
                    RaiseEvent AmountDownloadedChanged(iTotalBytesRead)
                End If
            Loop While Not iBytesRead = 0
            sChunks.Close()
            FS.Close()
            RaiseEvent FileDownloadComplete()
            Return True
        Catch ex As Exception
            If Not (FS Is Nothing) Then
                FS.Close()
                FS = Nothing
            End If
            RaiseEvent FileDownloadFailed(ex)
            Return False
        End Try
    End Function

    Private Function FormatFileSize(ByVal Size As Long) As String
        Try
            Dim KB As Integer = 1024
            Dim MB As Integer = KB * KB

            If Size < KB Then
                Return (Size.ToString("D") & " bytes")
            Else
                Select Case Size / KB
                    Case Is < 1000
                        Return (Size / KB).ToString("N") & " kB"
                    Case Is < 1000000
                        Return (Size / MB).ToString("N") & " MB"
                    Case Is < 10000000
                        Return (Size / MB / KB).ToString("N") & " GB"
                End Select
            End If
        Catch ex As Exception
            Return Size.ToString
        End Try
    End Function

    Private Sub _Downloader_FileDownloadSizeObtained(ByVal iFileSize As Long) Handles Me.FileDownloadSizeObtained
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = Convert.ToInt32(iFileSize)
        Me.filesize = Convert.ToInt32(iFileSize)
    End Sub

    Private Sub _Downloader_FileDownloadComplete() Handles Me.FileDownloadComplete
        ProgressBar1.Value = ProgressBar1.Maximum
        Dim client = New System.Net.WebClient()
        Dim versie_beschikbaar = client.DownloadString(Me.vars_versielocatie)
        Me.Close()
    End Sub

    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles Me.FileDownloadFailed
        MsgBox("Errore Scaricamento" + vbNewLine + vbNewLine + ex.Message)
        Me.Close()
    End Sub

    Private Function NieuweVersieBeschikbaar(ByVal Location As String) As Boolean
        Dim versie_nu = My.Application.Info.Version.ToString 'Get installed version
        Dim client = New System.Net.WebClient() 'Create new var to get newest version available
        Dim versie_beschikbaar = client.DownloadString(Location) 'Get newest version available
        If CLng(versie_beschikbaar) > CLng(versie_nu) Then 'Check if newest version is > than installed version
            Return True 'If so, return true
        Else
            Return False 'Otherwise, return false
        End If
    End Function

    Friend Sub Downloaden_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If SilentMode = True Then
            Me.WindowState = Windows.Forms.FormWindowState.Minimized
            Me.ShowInTaskbar = False
            Timer2.Enabled = True
        Else
            Timer1.Enabled = True 'Enable the timer if the dialog is loaded
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Interval = 999999999
        Timer1.Enabled = False

        If NieuweVersieBeschikbaar(Me.vars_versielocatie) Then 'Check if there is a newer version available
            If MsgBox(Me.vars_bericht, MsgBoxStyle.Information + MsgBoxStyle.YesNo, vars_berichttitel) = MsgBoxResult.Yes Then 'Display MsgBox to ask to download newest version
                CheckForUpdates(Me.vars_downloadlocatie) 'Download the file, if wanted
            Else
                Me.Close() 'Otherwise, close the download window
            End If
        Else
            Me.Hide()
            MsgBox("La tua Versione di Helpassistenza è Aggiornata !", MsgBoxStyle.Information, "HelpAssistenza - Aggiornamento")
            Me.Close() 'Close the download window if there isn't a newer version available
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Interval = 999999999
        Timer2.Enabled = False

        If NieuweVersieBeschikbaar(Me.vars_versielocatie) Then
            CheckForUpdates(Me.vars_downloadlocatie)
        Else
            Me.Close()
        End If
    End Sub

End Class