Imports System.Net
Imports System.IO
Module PKG_Update
    Sub ControlloOffline(ByVal URLsitoftp As String, ByVal Username As String, ByVal Password As String, Optional ByVal Pathsitoftp As String = "/")
        Try
            Dim urlsito As String = "ftp://" & URLsitoftp & Pathsitoftp

            'leggo File pkgupdate
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath + "\Package_Update\pkgupdate") Then Exit Sub
            Dim objReader As New StreamReader(Application.StartupPath + "\Package_Update\pkgupdate")
            Dim sLine As String = String.Empty
            Dim arrText As New ArrayList()
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing Then
                    arrText.Add(sLine)
                End If
            Loop Until sLine Is Nothing
            objReader.Close()
            objReader.Dispose()

            'Controllo se processo è Attivo 
            Dim Nomeproc As String = arrText(6)
            If Nomeproc = String.Empty Then Nomeproc = arrText(2) + ".exe"
            If Diagnostics.Process.GetProcessesByName(Nomeproc).Length > 0 Then
                Exit Sub
            End If

            'Scarico il file Pacchetto Aggiornmento
            Dim filename As String = urlsito & arrText(0)
            Dim client03 As New WebClient
            client03.Credentials = New NetworkCredential(Username, Password)
            If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Package_Update") Then My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Package_Update")
            My.Computer.FileSystem.WriteAllBytes(Application.StartupPath + "\Package_Update\" + arrText(0), _
               client03.DownloadData(filename), True)
            'Elimino il file scaricato su FTP
            Dim reqFTP4 As FtpWebRequest
            Dim URLDownl As String = urlsito & arrText(0)
            reqFTP4 = DirectCast(FtpWebRequest.Create(New Uri(URLDownl)), FtpWebRequest)
            reqFTP4.Credentials = New NetworkCredential(Username, Password)
            If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP4.UsePassive = True
            reqFTP4.KeepAlive = False

            reqFTP4.Method = WebRequestMethods.Ftp.DeleteFile

            Dim result2 As String = [String].Empty
            Dim response4 As FtpWebResponse = DirectCast(reqFTP4.GetResponse(), FtpWebResponse)
            Dim sizee As Long = response4.ContentLength
            Dim datastream1 As Stream = response4.GetResponseStream()
            Dim str5 As New StreamReader(datastream1)
            result2 = str5.ReadToEnd()
            str5.Close()
            datastream1.Close()
            response4.Close()

            'Estraggo i file nella directory Destinazione
            Dim ZipToUnpack As String = Application.StartupPath + "\Package_Update\" + arrText(0)
            Dim TargetDir As String = arrText(1)
            Using zip1 As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read(ZipToUnpack)
                Dim eo As Ionic.Zip.ZipEntry
                For Each eo In zip1
                    eo.Extract(TargetDir, True)
                Next
            End Using
            'Elimino File Zip  e File pkgupdate Scaricato 
            If My.Computer.FileSystem.FileExists(ZipToUnpack) Then My.Computer.FileSystem.DeleteFile(ZipToUnpack)
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Package_Update\pkgupdate") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Package_Update\pkgupdate")

            'Display del Report a Video 
            PKGUPDATEREPORT(Convert.ToInt16(arrText(3)), arrText(4), arrText(2), arrText(5))

            'Creo File Report Aggiornamento Eseguibile
            Dim Oggi As String = Date.Today
            Oggi = Oggi.Replace("/", "")
            Dim FileReportFTP As String = "ReportUPDPKG" + Oggi + ".txt"
            Dim FileCompletoFTP As String = Application.StartupPath + "\Package_Update\" + FileReportFTP
            Dim FSS As New IO.FileStream(FileCompletoFTP, IO.FileMode.Create)
            Dim SW As New IO.StreamWriter(FSS)
            SW.Write(Date.Now + " - " + "Pacchetto : " + arrText(2) + " Aggiornato Correttamente")
            SW.Flush()
            FSS.Close()
            'Upload del Report di Avvenuto Aggiornamento Pacchetto
            UPloadReport(URLsitoftp & Pathsitoftp & FileReportFTP, FileCompletoFTP, Username, Password)
            If My.Computer.FileSystem.FileExists(FileCompletoFTP) Then My.Computer.FileSystem.DeleteFile(FileCompletoFTP)
            'Disabilito il Timer di Controllo
            frmAssistenza.tmrUPDPKGOffline.Enabled = False
            'Scrivo in Log File
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Aggiornato Pacchetto : " + arrText(2))
        Catch
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Impossibile Aggiornare Pacchetto !")
            frmAssistenza.tmrUPDPKGOffline.Enabled = False
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Package_Update\pkgupdate") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Package_Update\pkgupdate")
        End Try
    End Sub
    Sub UPloadReport(ByVal indirizzoenomefileFTP As String, ByVal FileLocale As String, ByVal Username As String, ByVal Password As String)
        Dim clsRequest As System.Net.FtpWebRequest = _
           DirectCast(System.Net.WebRequest.Create("ftp://" + indirizzoenomefileFTP), System.Net.FtpWebRequest)
        clsRequest.Credentials = New System.Net.NetworkCredential(Username, Password)
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile

        ' read in file...
        Dim bFile() As Byte = System.IO.File.ReadAllBytes(FileLocale)

        ' upload file...
        Dim clsStream As System.IO.Stream = _
            clsRequest.GetRequestStream()
        clsStream.Write(bFile, 0, bFile.Length)
        clsStream.Close()
        clsStream.Dispose()
    End Sub
    Sub PKGUPDATEREPORT(ByVal Oper As Integer, ByVal Ticket As String, ByVal PKG_Aggiornato As String, ByVal Messaggio As String)
        frmReport.lblAzienda.Visible = True
        frmReport.Label1.Visible = True
        frmReport.Label2.Visible = True
        frmReport.Label3.Visible = False
        frmReport.lblNomeAzienda.Visible = True
        frmReport.lblnometecnico.Visible = True
        frmReport.lblDurataAssistenza.Visible = False
        frmReport.lblIntDurataAssistenza.Visible = False
        frmReport.lblTecnico.Visible = True
        frmReport.btnFinito.Visible = False
        frmReport.btnStampa.Visible = True
        frmReport.btnStampa.Enabled = True
        frmReport.lblInterrotto.Visible = False
        frmReport.PicWarning.Visible = False
        frmReport.BtnOK.Enabled = True
        frmReport.lblAzienda.Text = frmConfigurazione.txtnomeazienda.Text
        frmReport.RichTextBoxRiepilogo.Text = Messaggio
        frmReport.RichTextBoxRiepilogo.Text = "RIFERIMENTO TICKET : " + Ticket + vbCrLf + vbCrLf + "PACCHETTO AGGIORNATO : " + PKG_Aggiornato + vbCrLf + vbCrLf + frmReport.RichTextBoxRiepilogo.Text
        frmReport.RichTextBoxRiepilogo.SelectionStart = frmReport.RichTextBoxRiepilogo.TextLength
        Select Case Oper
            Case Is = 1
                frmReport.lblTecnico.Text = frmConfigurazione.txtnomeOPE1.Text & " " & frmConfigurazione.txtCognomeOPE1.Text
            Case Is = 2
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE2.Text & " " & frmConfigurazione.txtCognomeOPE2.Text
            Case Is = 3
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE3.Text & " " & frmConfigurazione.txtCognomeOPE3.Text
            Case Is = 4
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE4.Text & " " & frmConfigurazione.txtCognomeOPE4.Text
            Case Is = 5
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE5.Text & " " & frmConfigurazione.txtCognomeOPE5.Text
            Case Is = 6
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE6.Text & " " & frmConfigurazione.txtCognomeOPE6.Text
            Case Is = 7
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE7.Text & " " & frmConfigurazione.txtCognomeOPE7.Text
            Case Is = 8
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE8.Text & " " & frmConfigurazione.txtCognomeOPE8.Text
            Case Is = 9
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE9.Text & " " & frmConfigurazione.txtCognomeOPE9.Text
            Case Is = 10
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE10.Text & " " & frmConfigurazione.txtCognomeOPE10.Text
            Case Is = 11
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE11.Text & " " & frmConfigurazione.txtCognomeOPE11.Text
            Case Is = 12
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE12.Text & " " & frmConfigurazione.txtCognomeOPE12.Text
            Case Is = 13
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE13.Text & " " & frmConfigurazione.txtCognomeOPE13.Text
            Case Is = 14
                frmReport.lblTecnico.Text = frmConfigurazione.txtNomeOPE14.Text & " " & frmConfigurazione.txtCognomeOPE14.Text
            Case Else
                frmReport.Label3.Visible = False
                frmReport.lblnometecnico.Visible = False
                frmReport.lblTecnico.Visible = False
        End Select
        frmReport.Show()
        frmReport.TopMost = True
    End Sub
End Module
