Imports System.Net

Public Class frmUploadFile
    Dim fileName As String
    Sub UpdateProgressBar(ByVal sender As Object, ByVal e As UploadProgressChangedEventArgs)
        If ProgressBar1.InvokeRequired Then
            Try
                ProgressBar1.Invoke(New UploadProgressChangedEventHandler(AddressOf UpdateProgressBar), sender, e)
                Exit Sub
            Catch
                Exit Sub
            End Try
        End If
        ProgressBar1.Value = CInt(ProgressBar1.Minimum + ((ProgressBar1.Maximum - ProgressBar1.Minimum) * e.ProgressPercentage) / 50)
    End Sub

    Private Sub TimerControllaFine_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerControllaFine.Tick
        If ProgressBar1.Value = 100 Then
            Me.TimerChiudi.Enabled = True
            Me.TimerControllaFine.Enabled = False
        End If
    End Sub

    Private Sub TimerChiudi_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerChiudi.Tick
        If FileTransefer = 3 Then
            Me.Close()
            If (frmAssistenza.TimerFTP.Enabled = True OrElse ListenMode = True) And frmAssistenza.Operatore <> 0 Then
                ChiudiConnessioni()
            Else
                frmAssistenza.EsciToolStripMenuItem.PerformClick()
            End If
        Else
            Me.Close()
        End If
        Me.TimerChiudi.Enabled = False
    End Sub

    Private Sub frmTransferFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Metto il form nell'angolo in basso a destra
        Dim working_area As Rectangle = _
       SystemInformation.WorkingArea
        Dim x As Integer = _
            working_area.Left + _
            working_area.Width - _
            Me.Width
        Dim y As Integer = _
            working_area.Top + _
            working_area.Height - _
            Me.Height
        Me.Location = New Point(x, y)

        'Parte Timer Controllo Fine Trasferimento su ProgressBar
        Me.TimerControllaFine.Enabled = True
        'Se attivo il Download File Skippa
        If Me.DownloadviFTP = True Then
            Me.ProgressBar1.Value = 15
            ToolTipTrasferimento.IsBalloon = True
            ToolTipTrasferimento.SetToolTip(ProgressBar1, "Download File in Corso")
            Exit Sub
        End If

        'Eseguo upload del File
        Dim filePath As String = UploadFileTransf
        Dim fnPeices() As String = filePath.Split("\")
        fileName = fnPeices(fnPeices.Length - 1)
        ToolTipTrasferimento.IsBalloon = True
        ToolTipTrasferimento.SetToolTip(ProgressBar1, "Upload File in Corso")
        ToolTipTrasferimento.SetToolTip(Me, "Upload File : " + fileName + " in Corso")
        If frmConfigurazione.chkAbilitaUploadFile.Checked = True Then
            BackgroundUploadFile.RunWorkerAsync()
        Else
            Me.Close()
        End If
    End Sub


    Private Sub BackgroundUploadFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundUploadFile.DoWork
        If CheckUgualeTele = True Then
            Try
                Dim fwr As FtpWebRequest = FtpWebRequest.Create("ftp://" + SitoTelecontrollo + PathSitoTeleControllo + "upload/")
                fwr.Credentials = New NetworkCredential(UserTeleControllo, PassTeleControllo)
                fwr.Method = WebRequestMethods.Ftp.MakeDirectory
                If FTPasv = True Then fwr.UsePassive = True
                fwr.KeepAlive = False
                fwr.GetResponse()
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try


            Dim client As New System.Net.WebClient()
            AddHandler client.UploadProgressChanged, AddressOf UpdateProgressBar
            With client
                .Credentials = New NetworkCredential(UserTeleControllo, PassTeleControllo)
                .UploadFileAsync(New Uri("ftp://" + SitoTelecontrollo + PathSitoTeleControllo + "upload/" + fileName), UploadFileTransf)
            End With

        Else

            Try
                Dim fwr As FtpWebRequest = FtpWebRequest.Create("ftp://" + SitoServerUP + PathServerUP + "upload/")
                fwr.Credentials = New NetworkCredential(UserServerUP, PassServerUP)
                fwr.Method = WebRequestMethods.Ftp.MakeDirectory
                If FTPasv = True Then fwr.UsePassive = True
                fwr.KeepAlive = False
                fwr.GetResponse()
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try


            Dim client As New System.Net.WebClient()
            AddHandler client.UploadProgressChanged, AddressOf UpdateProgressBar
            With client
                .Credentials = New NetworkCredential(UserServerUP, PassServerUP)
                .UploadFileAsync(New Uri("ftp://" + SitoServerUP + PathServerUP + "upload/" + fileName), UploadFileTransf)
            End With

        End If

    End Sub

    Private Sub AnnullaUploadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnnullaUploadToolStripMenuItem.Click
        Me.BackgroundUploadFile.CancelAsync()
        Me.BackgroundDownloadFile.CancelAsync()
        Me.Close()
        Me.Dispose()
    End Sub
    Private download As Boolean
    Public Property DownloadviFTP() As Boolean
        Get
            Return download
        End Get
        Set(ByVal value As Boolean)
            download = value
        End Set
    End Property

    Private Sub BackgroundDownloadFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundDownloadFile.DoWork
        Dim args As Object() = DirectCast(e.Argument, Object())

        Dim Salvain As String = CStr(args(0))
        Dim Server As String = CStr(args(1))
        Dim user As String = CStr(args(2))
        Dim pass As String = CStr(args(3))
        Dim dezip As Boolean = CBool(args(4))

        'Scarico il Filer via FTP
        Dim client As New WebClient
        client.Credentials = New NetworkCredential(user, pass)
        My.Computer.FileSystem.WriteAllBytes(Salvain, client.DownloadData(Server), True)

        'Decomprimo File se Imopostato
        If Salvain.EndsWith(".zip") And dezip = True Then
            Dim a = New Ionic.Zip.ZipFile(Salvain)
            For Each zipfile As Ionic.Zip.ZipEntry In a
                zipfile.Extract(System.IO.Path.GetDirectoryName(Salvain), True)
            Next
            a.Dispose()
        End If
    End Sub

    Private Sub BackgroundDownloadFile_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundDownloadFile.ProgressChanged
        Me.ProgressBar1.Value = CInt(e.ProgressPercentage)
    End Sub

    Private Sub BackgroundDownloadFile_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundDownloadFile.RunWorkerCompleted
        Me.ProgressBar1.Value = 100
        frmAssistenza.NotifyHelp.ShowBalloonTip(5000, "HelpAssistenza - Download File", "Nuovo File Scaricato via FTP", ToolTipIcon.Info)
    End Sub

End Class