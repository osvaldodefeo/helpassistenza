Imports System.Net
Public Class frmDownloadWeb

    Dim whereToSave As String
    Dim tttip As New ToolTip
    Delegate Sub ChangeTextsSafe(ByVal length As Long, ByVal position As Integer, ByVal percent As Integer, ByVal speed As Double)
    Delegate Sub DownloadCompleteSafe(ByVal cancelled As Boolean)


    Public Sub DownloadComplete(ByVal cancelled As Boolean)
        Me.txtFileName.Enabled = True
        Me.btnDownload.Enabled = True

        tttip.UseAnimation = True
        tttip.IsBalloon = True
        tttip.BackColor = Color.WhiteSmoke
        tttip.ToolTipTitle = "HelpAssistenza - Web Download File"
        tttip.ToolTipIcon = ToolTipIcon.Info
        tttip.ForeColor = Color.Blue
        If cancelled Then
            tttip.SetToolTip(Me, "Download Annullato !")
            tttip.SetToolTip(Me.ProgressBar1, "Download Annullato !")
        Else
            tttip.SetToolTip(Me, "Download Completato !")
            tttip.SetToolTip(Me.ProgressBar1, "Download Completato !")
        End If

        Me.ProgressBar1.Value = 0
        'Me.lblsalvain.Text = "Salva in : "
        'Me.lbldimensionefile.Text = "Dimensione File : "
        Me.lblvelocità.Text = "Velocità : "

    End Sub

    Public Sub ChangeTexts(ByVal length As Long, ByVal position As Integer, ByVal percent As Integer, ByVal speed As Double)

        Me.lbldimensionefile.Text = "Dimensione File : " & Math.Round((length / 1024), 2) & " KB"

        Me.lblscaricati.Text = "Scaricati : " & Math.Round((position / 1024), 2) & " KB di " & Math.Round((length / 1024), 2) & "KB (" & Me.ProgressBar1.Value & "%)"

        If speed = -1 Then
            Me.lblvelocità.Text = "Velocità : Calcolo in corso ..."
        Else
            Me.lblvelocità.Text = "Velocità : " & Math.Round((speed / 1024), 2) & " KB/s"
        End If

        Me.Text = "HelpAssistenza - Web Download File (" & Me.ProgressBar1.Value & "%)"

        Me.ProgressBar1.Value = percent

        If percent = 100 Then
            btnDownload.Text = "Download"
            'Controllo se impostato decomprimo file dopo Download
            Me.TimerDeZip.Enabled = True
            frmAssistenza.NotifyHelp.ShowBalloonTip(5000, "HelpAssistenza - Download File", "Nuovo File Scaricato via WEB", ToolTipIcon.Info)
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        If Me.btnDownload.Text = "Annulla" Then
            Me.BackgroundWorker1.CancelAsync()
            Me.btnDownload.Text = "Download"
            Exit Sub
        End If
        If Not Me.txtFileName.Text.StartsWith("http://") Then Me.txtFileName.Text = "http://" + Me.txtFileName.Text
        If (Me.txtFileName.Text <> "" And Me.txtFileName.Text <> "http://") AndAlso Me.txtFileName.Text.StartsWith("http://") Then


            Me.SaveFileDialog1.FileName = Me.txtFileName.Text.Split("/"c)(Me.txtFileName.Text.Split("/"c).Length - 1)

            If Me.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

                Me.whereToSave = Me.SaveFileDialog1.FileName

                Me.SaveFileDialog1.FileName = ""

                Me.lblsalvain.Text = "Salva in : " & Me.whereToSave

                Me.txtFileName.Enabled = False
                Me.btnDownload.Text = "Annulla"

                'Setto Tooltip
                tttip.UseAnimation = True
                tttip.IsBalloon = True
                tttip.BackColor = Color.WhiteSmoke
                tttip.ToolTipTitle = "HelpAssistenza - Download File"
                tttip.ToolTipIcon = ToolTipIcon.Info
                tttip.ForeColor = Color.Blue
                tttip.SetToolTip(Me, "Download File : " + Me.txtFileName.Text + " in Corso ...")
                tttip.SetToolTip(Me.ProgressBar1, "Download File : " + Me.txtFileName.Text + " in Corso ...")
                Me.BackgroundWorker1.RunWorkerAsync() 'Inizio download
            End If

        Else

            MessageBox.Show("Inserisci un  URL valido per il Download", "HelpAssistenza - Errore Download", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        'Creating the request and getting the response
        Dim theResponse As HttpWebResponse
        Dim theRequest As HttpWebRequest
        Try 'Checks if the file exist

            theRequest = WebRequest.Create(Me.txtFileName.Text)
            theResponse = theRequest.GetResponse
        Catch ex As Exception

            MessageBox.Show("Errore durante il Download del File. Possibili Cause : " & ControlChars.CrLf & _
                            "1) Il File Non Esiste" & ControlChars.CrLf & _
                            "2) Errore del Web Server" & ControlChars.CrLf & _
                            "3) Hai tentato lo Scaricamento di una Directory", "HelpAssistenza - Errore Download File", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)

            Me.Invoke(cancelDelegate, True)

            Exit Sub
        End Try
        Dim length As Long = theResponse.ContentLength 'Size of the response (in bytes)

        Dim safedelegate As New ChangeTextsSafe(AddressOf ChangeTexts)
        Me.Invoke(safedelegate, length, 0, 0, 0) 'Invoke the TreadsafeDelegate

        Dim writeStream As New IO.FileStream(Me.whereToSave, IO.FileMode.Create)

        'Replacement for Stream.Position (webResponse stream doesn't support seek)
        Dim nRead As Integer

        'To calculate the download speed
        Dim speedtimer As New Stopwatch
        Dim currentspeed As Double = -1
        Dim readings As Integer = 0

        Do

            If BackgroundWorker1.CancellationPending Then 'If user abort download
                Exit Do
            End If

            speedtimer.Start()

            Dim readBytes(4095) As Byte
            Dim bytesread As Integer = theResponse.GetResponseStream.Read(readBytes, 0, 4096)

            nRead += bytesread
            Dim percent As Short = (nRead * 100) / length

            Me.Invoke(safedelegate, length, nRead, percent, currentspeed)

            If bytesread = 0 Then Exit Do

            writeStream.Write(readBytes, 0, bytesread)

            speedtimer.Stop()

            readings += 1
            If readings >= 5 Then 'For increase precision, the speed it's calculated only every five cicles
                currentspeed = 20480 / (speedtimer.ElapsedMilliseconds / 1000)
                speedtimer.Reset()
                readings = 0
            End If
        Loop

        'Close the streams
        theResponse.GetResponseStream.Close()
        writeStream.Close()



        If Me.BackgroundWorker1.CancellationPending Then

            IO.File.Delete(Me.whereToSave)

            Dim cancelDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)

            Me.Invoke(cancelDelegate, True)

            Exit Sub

        End If

        Dim completeDelegate As New DownloadCompleteSafe(AddressOf DownloadComplete)

        Me.Invoke(completeDelegate, False)

    End Sub


    Private Sub frmDownloadWeb_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SaveFileDialog1.InitialDirectory = Environ("SystemDrive")
    End Sub

    Private Sub lblsalvain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblsalvain.Click
        If lblsalvain.Text <> "Salva in : " Then
            Dim newProcess As Process
            Dim percorsosalva = whereToSave
            If percorsosalva.EndsWith(".zip") = False Then
                percorsosalva = System.IO.Path.GetDirectoryName(percorsosalva)
            End If
            newProcess = Process.Start("Explorer.exe ", percorsosalva)
        End If
    End Sub

    Private Sub TimerDeZip_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDeZip.Tick
        If whereToSave.EndsWith(".zip") And frmConfigurazione.chkdezip.Checked = True Then
            Dim a = New Ionic.Zip.ZipFile(whereToSave)
            For Each zipfile As Ionic.Zip.ZipEntry In a
                zipfile.Extract(System.IO.Path.GetDirectoryName(whereToSave), True)
            Next
            a.Dispose()
        End If
        Me.TimerDeZip.Enabled = False
    End Sub
End Class
