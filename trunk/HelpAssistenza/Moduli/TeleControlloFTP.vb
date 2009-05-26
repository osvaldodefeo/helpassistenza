Imports System.Net
Imports System.IO
Module TeleControlloFTP
    Dim erroreTeleControllo As Short = 0
    Public ListenMode As Boolean = False
    Private CreaDirectory As Boolean = True
    Public Sub ControlloChiamataFTP(ByVal URLsitoftp As String, ByVal Username As String, ByVal Password As String, Optional ByVal Pathsitoftp As String = "/")
        'Controllo errori di connessione
        If erroreTeleControllo <= 10 Then
            'Controllo di errore globale su Connessione TeleControllo
            Try
                'Primo Controllo per vedere se creare oppure no La Path sul FTP
                If CreaDirectory = True Then
                    Try
                        Dim creatdirFTP As FtpWebRequest = FtpWebRequest.Create("ftp://" & URLsitoftp & Pathsitoftp)
                        With creatdirFTP
                            .Credentials = New NetworkCredential(Username, Password)
                            .Method = WebRequestMethods.Ftp.MakeDirectory
                            If frmConfigurazione.chkFTPPassive.Checked = True Then .UsePassive = True
                            .KeepAlive = False
                            .GetResponse()
                        End With
                        'Creo il File OK_AUTOM su FTP
                        Dim sTempFileName As String = Application.StartupPath + "\OK_AUTOM"
                        Dim fsTemp As New System.IO.FileStream(sTempFileName, IO.FileMode.Create)
                        fsTemp.Close()
                        fsTemp.Dispose()
                        Dim UPTempFile As FtpWebRequest = FtpWebRequest.Create("ftp://" + frmConfigurazione.txtSitoTeleControllo.Text + frmConfigurazione.txtPathTeleControllo.Text + "OK_AUTOM")
                        With UPTempFile
                            .Credentials = New NetworkCredential(frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text)
                            .Method = WebRequestMethods.Ftp.UploadFile
                            If frmConfigurazione.chkFTPPassive.Checked = True Then .UsePassive = True
                            .KeepAlive = False
                            Dim b_file() As Byte = System.IO.File.ReadAllBytes(sTempFileName)
                            'Upload the file
                            Dim cls_stream As System.IO.Stream = .GetRequestStream()
                            cls_stream.Write(b_file, 0, b_file.Length)
                            cls_stream.Close()
                            cls_stream.Dispose()
                        End With
                        'Cancello File temporaneo su PC
                        System.IO.File.Delete(sTempFileName)
                    Catch
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Impossibile Creare Directory su Sito TeleControllo : Directory " + frmConfigurazione.txtPathTeleControllo.Text + " già Presente! ")
                    End Try
                    CreaDirectory = False
                End If

                Dim urlsito As String = "ftp://" & URLsitoftp & Pathsitoftp
                'Controllo se presente file chiamata su sito FTP
                Dim fwr As FtpWebRequest = FtpWebRequest.Create(urlsito)
                fwr.Credentials = New NetworkCredential(Username, Password)
                fwr.Method = WebRequestMethods.Ftp.ListDirectory
                If frmConfigurazione.chkFTPPassive.Checked = True Then fwr.UsePassive = True
                fwr.KeepAlive = False
                Dim srrr As New StreamReader(fwr.GetResponse().GetResponseStream())
                Dim str As String = srrr.ReadToEnd()
                srrr.Close()
                'inizio controllo line da chiamare
                If str.Contains("call01") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'Cancello il file sull'FTP
                    Dim sUri As String
                    sUri = urlsito & "call01"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()

                    'Chiamo Linea via internet
                    frmAssistenza.Operatore = 1
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny1.Text, frmConfigurazione.txtipPCA1.Text)
                    Else
                        'chiama remoto ADSL VNC
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call02") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 2
                    Dim sUri As String

                    sUri = urlsito & "call02"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo Linea 2 Via Internet
                    frmAssistenza.Operatore = 2
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny2.Text, frmConfigurazione.txtipPCA2.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call03") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 3
                    Dim sUri As String

                    sUri = urlsito & "call03"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo Linea 3 Via Internet
                    frmAssistenza.Operatore = 3
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny3.Text, frmConfigurazione.txtipPCA3.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call04") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 4
                    Dim sUri As String

                    sUri = urlsito & "call04"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 4 via Internet
                    frmAssistenza.Operatore = 4
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny4.Text, frmConfigurazione.txtipPCA4.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call05") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 5
                    Dim sUri As String

                    sUri = urlsito & "call05"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 5 via Internet
                    frmAssistenza.Operatore = 5
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny5.Text, frmConfigurazione.txtipPCA5.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call06") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 6
                    Dim sUri As String

                    sUri = urlsito & "call06"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 6 via Internet
                    frmAssistenza.Operatore = 6
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny6.Text, frmConfigurazione.txtipPCA6.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call07") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 7
                    Dim sUri As String

                    sUri = urlsito & "call07"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 7 via internet
                    frmAssistenza.Operatore = 7
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny7.Text, frmConfigurazione.txtipPCA7.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call08") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 8
                    Dim sUri As String

                    sUri = urlsito & "call08"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo Linea 8 via Internet
                    frmAssistenza.Operatore = 8
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny8.Text, frmConfigurazione.txtipPCA8.Text)
                    Else
                        If frmConfigurazione.chkreconn.Checked = True Then
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, True)
                        Else
                            frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                            frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                            frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                            ControllaTipListen()
                            frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call09") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 9
                    Dim sUri As String

                    sUri = urlsito & "call09"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 9 via Internet
                    frmAssistenza.Operatore = 9
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny9.Text, frmConfigurazione.txtipPCA9.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call10") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 10
                    Dim sUri As String

                    sUri = urlsito & "call10"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 10 via Internet
                    frmAssistenza.Operatore = 10
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny10.Text, frmConfigurazione.txtipPCA10.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call11") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 11
                    Dim sUri As String

                    sUri = urlsito & "call11"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 11 via Internet
                    frmAssistenza.Operatore = 11
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny11.Text, frmConfigurazione.txtipPCA11.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call12") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 12
                    Dim sUri As String

                    sUri = urlsito & "call12"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo Linea 12 via Internet
                    frmAssistenza.Operatore = 12
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny12.Text, frmConfigurazione.txtipPCA12.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call13") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 13
                    Dim sUri As String

                    sUri = urlsito & "call13"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo linea 13 via Internet
                    frmAssistenza.Operatore = 13
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny13.Text, frmConfigurazione.txtipPCA13.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, False)
                        End If
                    End If

                ElseIf str.Contains("call14") And frmConfigurazione.chkChiamateTeleControllo.Checked = True Then
                    'inserire codice per chiamata auto FTP 14
                    Dim sUri As String

                    sUri = urlsito & "call14"

                    Dim reqFTP As FtpWebRequest

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False

                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty

                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)

                    Dim size As Long = response.ContentLength

                    Dim datastream As Stream = response.GetResponseStream()

                    Dim sr As New StreamReader(datastream)

                    result = sr.ReadToEnd()

                    sr.Close()

                    datastream.Close()

                    response.Close()
                    'Chiamo Linea 14 via internet
                    'MessageBox.Show("Alessio The Best")
                    frmAssistenza.Operatore = 14
                    If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                        'chiama remoto Pc Anywhere ADSL
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - PcAnywhere"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny14.Text, frmConfigurazione.txtipPCA14.Text)
                    Else
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - VNC"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkreconn.Checked = True Then
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, True)
                        Else
                            ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, False)
                        End If
                    End If

                ElseIf str.Contains("reboot") And frmConfigurazione.chkspegniTeleControllo.Checked = True Then
                    RiavviaPC()
                ElseIf str.Contains("shutdown") And frmConfigurazione.chkspegniTeleControllo.Checked = True Then
                    SpegniPC()
                ElseIf str.Contains("logoff") And frmConfigurazione.chkspegniTeleControllo.Checked = True Then
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Logoff del PC da Listen Mode")
                    Try
                        Dim t As Single
                        Dim objWMIService, objComputer As Object
                        objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")

                        For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
                            t = objComputer.Win32Shutdown(0, 0)
                            If t <> 0 Then
                                MsgBox("Errore Logoff!", MsgBoxStyle.Critical, "HelpAssistenza - Errore")
                            Else
                            End If
                        Next
                    Catch
                        System.Diagnostics.Process.Start("shutdown", "-l -t 00")
                    End Try
                ElseIf str.Contains("message") And frmConfigurazione.chkscript.Checked = True Then
                    Dim sUri As String
                    sUri = urlsito & "message"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile

                    Using response As System.Net.FtpWebResponse = CType(reqFTP.GetResponse, System.Net.FtpWebResponse)
                        Using responseStream As IO.Stream = response.GetResponseStream
                            Using fs As New IO.FileStream(Application.StartupPath + "\message", IO.FileMode.Create)
                                Dim buffer(2047) As Byte
                                Dim read As Integer = 0
                                Do
                                    read = responseStream.Read(buffer, 0, buffer.Length)
                                    fs.Write(buffer, 0, read)
                                Loop Until read = 0

                                responseStream.Close()
                                fs.Flush()
                                fs.Close()
                            End Using
                            responseStream.Close()
                        End Using
                        response.Close()
                    End Using

                    'Elimino il file
                    Dim reqFTP2 As FtpWebRequest
                    reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP2.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP2.KeepAlive = False
                    reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty
                    Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                    Dim size As Long = response2.ContentLength
                    Dim datastream As Stream = response2.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response2.Close()

                    ' Apro il messaggio in Messagebox
                    Dim oRead As System.IO.StreamReader
                    Dim Messaggio As String
                    Dim TitoloMessaggio As String = "HelpAssistenza - Messaggio Remoto"
                    oRead = File.OpenText(Application.StartupPath + "\message")
                    Messaggio = oRead.ReadToEnd()
                    oRead.Close()
                    oRead.Dispose()
                    frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                    frmAssistenza.NotifyHelp.BalloonTipText = "Nuovo Messaggio Arrivato!"
                    ControllaTipListen()
                    frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                    If frmConfigurazione.txtnomeazienda.Text <> String.Empty Then TitoloMessaggio = "HelpAssistenza - Messaggio Remoto da: " + frmConfigurazione.txtnomeazienda.Text
                    MessageBox.Show(Messaggio, TitoloMessaggio, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If My.Computer.FileSystem.FileExists(Application.StartupPath + "\message") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\message")
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Aperto Messagio Remoto da Listen Mode")

                ElseIf str.Contains("executescript.cmd") And frmConfigurazione.chkscript.Checked = True Then

                    Dim sUri As String
                    sUri = urlsito & "executescript.cmd"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile

                    Using response As System.Net.FtpWebResponse = CType(reqFTP.GetResponse, System.Net.FtpWebResponse)
                        Using responseStream As IO.Stream = response.GetResponseStream
                            Using fs As New IO.FileStream(Application.StartupPath + "\executescript.cmd", IO.FileMode.Create)
                                Dim buffer(2047) As Byte
                                Dim read As Integer = 0
                                Do
                                    read = responseStream.Read(buffer, 0, buffer.Length)
                                    fs.Write(buffer, 0, read)
                                Loop Until read = 0

                                responseStream.Close()
                                fs.Flush()
                                fs.Close()
                            End Using
                            responseStream.Close()
                        End Using
                        response.Close()
                    End Using

                    'Elimino il file
                    Dim reqFTP2 As FtpWebRequest

                    reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP2.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP2.KeepAlive = False

                    reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty
                    Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                    Dim size As Long = response2.ContentLength
                    Dim datastream As Stream = response2.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response2.Close()

                    ' Lancio Script
                    frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                    frmAssistenza.NotifyHelp.BalloonTipText = "Esecuzione Script Remoto"
                    ControllaTipListen()
                    frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                    Try
                        Shell(Chr(34) + Application.StartupPath + "\executescript.cmd" + Chr(34), AppWinStyle.Hide, True)
                        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\executescript.cmd") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\executescript.cmd")
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Eseguito Script Remoto con Successo da Listen Mode")
                    Catch
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Esecuzione Script Remoto da Listen Mode")
                    End Try

                    'Download File Zip
                ElseIf str.Contains("download.zip") And frmConfigurazione.chkscript.Checked = True Then
                    Try
                        Dim filename As String = urlsito & "download.zip"
                        Dim client As New WebClient
                        client.Credentials = New NetworkCredential(Username, Password)
                        If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Download") Then My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Download")
                        Dim data As String = CStr(Date.Now)
                        data = data.Replace("/", "")
                        data = data.Replace(".", "")
                        data = data.Replace(" ", "")
                        My.Computer.FileSystem.WriteAllBytes(Application.StartupPath + "\Download\" + "Download_" + data + ".zip", _
                           client.DownloadData(filename), True)
                        'Elimino il file
                        Dim reqFTP2 As FtpWebRequest
                        Dim URLDown As String = urlsito & "download.zip"
                        reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(URLDown)), FtpWebRequest)

                        reqFTP2.Credentials = New NetworkCredential(Username, Password)
                        If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP2.UsePassive = True
                        reqFTP2.KeepAlive = False

                        reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile

                        Dim result As String = [String].Empty
                        Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                        Dim size As Long = response2.ContentLength
                        Dim datastream As Stream = response2.GetResponseStream()
                        Dim sr As New StreamReader(datastream)
                        result = sr.ReadToEnd()
                        sr.Close()
                        datastream.Close()
                        response2.Close()
                        frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                        frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                        frmAssistenza.NotifyHelp.BalloonTipText = "Nuovo File Scaricato!"
                        ControllaTipListen()
                        frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Nuovo File Scaricato da Remoto")
                    Catch ex As Exception
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Scaricamento File da Remoto")
                    End Try

                    'Aggiornamento file Configurazione HelpAssistenza.ini Selettivo
                ElseIf (str.Contains("HelpAssistenza.ini.update") OrElse str.Contains("helpassistenza.ini.update")) And frmConfigurazione.chkscript.Checked = True Then

                    Dim sUri As String
                    sUri = urlsito & "HelpAssistenza.ini.update"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile

                    Using response As System.Net.FtpWebResponse = CType(reqFTP.GetResponse, System.Net.FtpWebResponse)
                        Using responseStream As IO.Stream = response.GetResponseStream
                            Using fs As New IO.FileStream(Application.StartupPath + "\HelpAssistenza.ini.update", IO.FileMode.Create)
                                Dim buffer(2047) As Byte
                                Dim read As Integer = 0
                                Do
                                    read = responseStream.Read(buffer, 0, buffer.Length)
                                    fs.Write(buffer, 0, read)
                                Loop Until read = 0

                                responseStream.Close()
                                fs.Flush()
                                fs.Close()
                            End Using
                            responseStream.Close()
                        End Using
                        response.Close()
                    End Using

                    'Elimino il file su FTP
                    Dim reqFTP2 As FtpWebRequest

                    reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP2.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP2.KeepAlive = False

                    reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty
                    Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                    Dim size As Long = response2.ContentLength
                    Dim datastream As Stream = response2.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response2.Close()
                    'Aggiorno Ini Selettivo
                    RecuperaSettaggi_UpdINI(Application.StartupPath + "\HelpAssistenza.ini.update")
                    If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.ini.old") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\HelpAssistenza.ini.old")
                    If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.ini") Then My.Computer.FileSystem.RenameFile(Application.StartupPath + "\HelpAssistenza.ini", "HelpAssistenza.ini.old")
                    altrocoll2()
                    gruppoaltrifire()
                    trasportaNomeAzienda()
                    trasportaCognome()
                    Abilitalinee()
                    IconeLinee()
                    settatooltip()
                    ControlloListenMode()
                    ConfigProjector()
                    Settaggi.ScriviSettaggi()
                    If System.IO.File.Exists("HelpAssistenza.ini.update") Then System.IO.File.Delete("HelpAssistenza.ini.update")
                    'Visualizzo messaggio Configurazione Aggiornata
                    frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                    frmAssistenza.NotifyHelp.BalloonTipText = "Configurazione HelpAssistenza Aggiornata da Remoto"
                    ControllaTipListen()
                    frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Aggiornata Configurazione HelpAssistenza Selettivamente da Listen Mode")


                    'Aggiornamento file Configurazione HelpAssistenza.ini
                ElseIf (str.Contains("HelpAssistenza.ini") OrElse str.Contains("helpassistenza.ini")) And frmConfigurazione.chkscript.Checked = True Then
                    Dim sUri As String
                    sUri = urlsito & "HelpAssistenza.ini"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile

                    Using response As System.Net.FtpWebResponse = CType(reqFTP.GetResponse, System.Net.FtpWebResponse)
                        Using responseStream As IO.Stream = response.GetResponseStream
                            Using fs As New IO.FileStream(Application.StartupPath + "\HelpAssistenza.ini.upd", IO.FileMode.Create)
                                Dim buffer(2047) As Byte
                                Dim read As Integer = 0
                                Do
                                    read = responseStream.Read(buffer, 0, buffer.Length)
                                    fs.Write(buffer, 0, read)
                                Loop Until read = 0

                                responseStream.Close()
                                fs.Flush()
                                fs.Close()
                            End Using
                            responseStream.Close()
                        End Using
                        response.Close()
                    End Using

                    'Elimino il file su FTP
                    Dim reqFTP2 As FtpWebRequest

                    reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)

                    reqFTP2.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP2.KeepAlive = False

                    reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty
                    Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                    Dim size As Long = response2.ContentLength
                    Dim datastream As Stream = response2.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response2.Close()
                    'Elimino vecchio ini e inserisco il nuovo
                    AggiornamentoINI()
                    'Ricarico la Configurazione
                    Settaggi.RecuperaSettaggi()
                    altrocoll2()
                    gruppoaltrifire()
                    trasportaNomeAzienda()
                    trasportaCognome()
                    Abilitalinee()
                    IconeLinee()
                    settatooltip()
                    ControlloListenMode()
                    ConfigProjector()
                    'Visualizzo messaggio Configurazione Aggiornata
                    frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                    frmAssistenza.NotifyHelp.BalloonTipText = "Configurazione HelpAssistenza Aggiornata da Remoto"
                    ControllaTipListen()
                    frmAssistenza.NotifyHelp.ShowBalloonTip(2500)
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Aggiornata Configurazione HelpAssistenza da Listen Mode")

                    'Aggiornamento Programmi mediante Pacchetto Aggiornamenti
                ElseIf str.Contains("pkgupdate") And frmConfigurazione.chkscript.Checked = True Then
                    If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Package_Update") Then My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Package_Update")
                    Dim sUri As String
                    sUri = urlsito & "pkgupdate"
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile

                    Using response As System.Net.FtpWebResponse = CType(reqFTP.GetResponse, System.Net.FtpWebResponse)
                        Using responseStream As IO.Stream = response.GetResponseStream
                            Using fs As New IO.FileStream(Application.StartupPath + "\Package_Update\pkgupdate", IO.FileMode.Create)
                                Dim buffer(2047) As Byte
                                Dim read As Integer = 0
                                Do
                                    read = responseStream.Read(buffer, 0, buffer.Length)
                                    fs.Write(buffer, 0, read)
                                Loop Until read = 0

                                responseStream.Close()
                                fs.Flush()
                                fs.Close()
                            End Using
                            responseStream.Close()
                        End Using
                        response.Close()
                    End Using

                    'Elimino il file
                    Dim reqFTP2 As FtpWebRequest
                    reqFTP2 = DirectCast(FtpWebRequest.Create(New Uri(sUri)), FtpWebRequest)
                    reqFTP2.Credentials = New NetworkCredential(Username, Password)
                    If frmConfigurazione.chkFTPPassive.Checked = True Then reqFTP.UsePassive = True
                    reqFTP2.KeepAlive = False
                    reqFTP2.Method = WebRequestMethods.Ftp.DeleteFile
                    Dim result As String = [String].Empty
                    Dim response2 As FtpWebResponse = DirectCast(reqFTP2.GetResponse(), FtpWebResponse)
                    Dim size As Long = response2.ContentLength
                    Dim datastream As Stream = response2.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response2.Close()

                    'leggo File pkgupdate
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

                    'Controllo se processo è Attivo parte Timer di Aggiornamento
                    Dim Nomeproc As String = arrText(6)
                    If Nomeproc = String.Empty Then Nomeproc = arrText(2)
                    If Diagnostics.Process.GetProcessesByName(Nomeproc).Length > 0 Then
                        frmAssistenza.tmrUPDPKGOffline.Enabled = True
                        Exit Sub
                    End If

                    'Scarico il file Pacchetto Aggiornmento
                    Dim filename As String = urlsito & arrText(0)
                    Dim client03 As New WebClient
                    client03.Credentials = New NetworkCredential(Username, Password)
                    If Not My.Computer.FileSystem.DirectoryExists(Application.StartupPath + "\Package_Update") Then My.Computer.FileSystem.CreateDirectory(Application.StartupPath + "\Package_Update")
                    My.Computer.FileSystem.WriteAllBytes(Application.StartupPath + "\Package_Update\" + arrText(0), _
                       client03.DownloadData(filename), True)
                    'Elimino il file
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
                    'Scrivo in Log File
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Aggiornato Pacchetto : " + arrText(2) + " da Listen Mode")
                End If
                erroreTeleControllo = 0
            Catch errrore As Exception
                If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Errore nella Connessione TeleControllo ! " + errrore.Message)
                erroreTeleControllo = erroreTeleControllo + 1
            End Try
        Else
            frmAssistenza.TimerErrTeleControllo.Enabled = True
        End If
    End Sub

    Sub ControlloParametriAvvio()
        If My.Application.CommandLineArgs.Count > 0 Then
            If (My.Application.CommandLineArgs(0).ToString = "-listen") OrElse (My.Application.CommandLineArgs(0).ToString = "-LISTEN") Then
                If frmConfigurazione.txtSitoTeleControllo.Text = String.Empty And frmConfigurazione.txtUserTeleControllo.Text = String.Empty And frmConfigurazione.txtPassTeleControllo.Text = String.Empty Then
                    MessageBox.Show("Controllare Parametri Fondamentali Connessione FTP" & vbCrLf & "URL Sito, Username, Password !", "HelpAssistenza - TeleControllo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End
                Else
                    ListenMode = True
                    Select Case frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex
                        Case Is = 0
                            frmAssistenza.TimerFTP.Interval = 60000
                        Case Is = 1
                            frmAssistenza.TimerFTP.Interval = 120000
                        Case Is = 2
                            frmAssistenza.TimerFTP.Interval = 180000
                        Case Is = 3
                            frmAssistenza.TimerFTP.Interval = 240000
                        Case Is = 4
                            frmAssistenza.TimerFTP.Interval = 300000
                        Case Is = 5
                            frmAssistenza.TimerFTP.Interval = 600000
                        Case Is = 6
                            frmAssistenza.TimerFTP.Interval = 1800000
                        Case Else
                            frmAssistenza.TimerFTP.Interval = 150000
                    End Select
                    frmAssistenza.TimerFTP.Start()
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("HelpAssistenza Avviato in Listen Mode")
                    frmAssistenza.ListenModeToolStripMenuItem.Text = "Stop Listen Mode"
                    frmAssistenza.NotifyHelp.Text = "HelpAssistenza - Listen Mode"
                    frmAssistenza.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    frmAssistenza.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                    frmAssistenza.NotifyHelp.BalloonTipText = "Listen Mode"
                    frmAssistenza.NotifyHelp.Visible = True
                    frmAssistenza.NotifyHelp.ShowBalloonTip(5000)
                    frmAssistenza.Hide()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "/installvncdrv") OrElse (My.Application.CommandLineArgs(0).ToString = "-installvncdrv") Then
                Try
                    If (My.Computer.Info.OSFullName.Contains("Vista") OrElse My.Computer.Info.OSFullName.Contains("2008")) And My.Computer.Info.OSFullName.Contains("64") Then
                        LaunchProg(Application.StartupPath + "\Ultravnc\driver\vista64\", Application.StartupPath + "\Ultravnc\driver\vista64\setupdrv.exe", "install")
                    ElseIf (My.Computer.Info.OSFullName.Contains("Vista") OrElse My.Computer.Info.OSFullName.Contains("2008")) Then
                        LaunchProg(Application.StartupPath + "\Ultravnc\driver\vista\", +Application.StartupPath + "\Ultravnc\driver\vista\setupdrv.exe", "install")
                    ElseIf (My.Computer.Info.OSFullName.Contains("XP") OrElse My.Computer.Info.OSFullName.Contains("2003")) And My.Computer.Info.OSFullName.Contains("64") Then
                        LaunchProg(Application.StartupPath + "\Ultravnc\driver\xp64\", Application.StartupPath + "\Ultravnc\driver\xp64\setupdrv.exe", "install")
                    ElseIf (My.Computer.Info.OSFullName.Contains("XP") OrElse My.Computer.Info.OSFullName.Contains("2003")) Then
                        LaunchProg(Application.StartupPath + "\Ultravnc\driver\xp\", Application.StartupPath + "\Ultravnc\driver\xp\setupdrv.exe", "install")
                    ElseIf My.Computer.Info.OSFullName.Contains("2000") Then
                        LaunchProg(Application.StartupPath + "\Ultravnc\driver\w2K\", Application.StartupPath + "\Ultravnc\driver\w2K\setupdrv.exe", "install")
                    End If
                Catch er As Exception
                    MessageBox.Show("Installativo Driver Video VNC non trovato !!!" + vbCrLf + er.Message, "HelpAssistenza", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call01") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL01") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea1.Visible = True Then
                    frmAssistenza.btnlinea1.PerformClick()
                    frmAssistenza.TimerAvvioPar.Enabled = True
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call02") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL02") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea2.Visible = True Then
                    frmAssistenza.btnlinea2.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call03") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL03") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea3.Visible = True Then
                    frmAssistenza.btnlinea3.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call04") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL04") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea4.Visible = True Then
                    frmAssistenza.btnlinea4.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call05") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL05") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea5.Visible = True Then
                    frmAssistenza.btnlinea5.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call06") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL06") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea6.Visible = True Then
                    frmAssistenza.btnlinea6.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call07") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL07") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea7.Visible = True Then
                    frmAssistenza.btnlinea7.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call08") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL08") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea8.Visible = True Then
                    frmAssistenza.btnlinea8.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call09") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL09") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea9.Visible = True Then
                    frmAssistenza.btnlinea9.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call10") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL10") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea10.Visible = True Then
                    frmAssistenza.btnlinea10.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call11") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL11") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea11.Visible = True Then
                    frmAssistenza.btnlinea11.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call12") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL12") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea12.Visible = True Then
                    frmAssistenza.btnlinea12.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call13") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL13") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea13.Visible = True Then
                    frmAssistenza.btnlinea13.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-call14") OrElse (My.Application.CommandLineArgs(0).ToString = "-CALL14") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.NotifyHelp.Visible = True
                frmAssistenza.TimerAvvioPar.Enabled = True
                If frmAssistenza.Linea14.Visible = True Then
                    frmAssistenza.btnlinea14.PerformClick()
                Else
                    frmAssistenza.TimerAvvioPar.Enabled = False
                    frmAssistenza.NotifyHelp.Visible = False
                    Application.Exit()
                End If
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-?") OrElse (My.Application.CommandLineArgs(0).ToString = "/?") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.ShowInTaskbar = False
                MessageBox.Show("Parametri di Avvio :" + vbCrLf + "-listen : Manda il programma in ListenMode" + vbCrLf + "-callXX : Chiama una Linea all'avvio del programma (XX = Numero Linea)" + vbCrLf + "-installvncdrv : Installa Driver Video VNC", "HelpAssistenza - Parametri Avvio", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Application.Exit()
            ElseIf (My.Application.CommandLineArgs(0).ToString = "-CheckUpdate") OrElse (My.Application.CommandLineArgs(0).ToString = "/CheckUpdate") Then
                frmAssistenza.WindowState = FormWindowState.Minimized
                frmAssistenza.ShowInTaskbar = False
                ModuloUpdate.CheckForUpdatesSilent("http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.txt", "http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.zip", "", True, "Una Nuova Versione di HelpAssistenza è disponibile !" + vbCrLf + "Vuoi Scaricarla ed installarla adesso ?")
                Application.Exit()
            End If
        End If
    End Sub
    Sub ControllaTipListen()
        If frmAssistenza.TimerFTP.Enabled = True Or ListenMode = True Then
            frmAssistenza.NotifyHelp.Text = "HelpAssistenza - Listen Mode"
        Else
            frmAssistenza.NotifyHelp.Text = "HelpAssistenza"
        End If
    End Sub

    Sub SpegniPC()
        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Spengo PC da Listen Mode")
        Try
            Dim t As Single
            Dim objWMIService, objComputer As Object
            objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")

            For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
                t = objComputer.Win32Shutdown(8 + 4, 0)
                If t <> 0 Then MsgBox("Errore Spegimento!", MsgBoxStyle.Critical, "HelpAssistenza - Errore")
            Next
        Catch
            System.Diagnostics.Process.Start("shutdown", "-s -f -t 00")
        End Try
    End Sub
    Sub RiavviaPC()
        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Riavvio del PC da Listen Mode")
        Try
            Dim t As Single
            Dim objWMIService, objComputer As Object

            objWMIService = GetObject("Winmgmts:{impersonationLevel=impersonate,(Debug,Shutdown)}")

            For Each objComputer In objWMIService.InstancesOf("Win32_OperatingSystem")
                t = objComputer.Win32Shutdown(2 + 4, 0)
                If t <> 0 Then
                    MsgBox("Errore Riavvio!", MsgBoxStyle.Critical, "HelpAssistenza - Errore")
                Else

                End If
            Next
        Catch
            System.Diagnostics.Process.Start("shutdown", "-r -f -t 00")
        End Try

    End Sub
    Sub LaunchProg(ByVal percorso As String, ByVal app As String, ByVal Arguments As String)
        Dim objprc As New System.Diagnostics.ProcessStartInfo(app)
        objprc.WorkingDirectory = percorso
        objprc.UseShellExecute = True
        objprc.Arguments = Arguments
        System.Diagnostics.Process.Start(objprc)
        System.Threading.Thread.Sleep(3000)
    End Sub
End Module
