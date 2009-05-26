Module Settaggi
    Dim objIniFile As New INIFiles(Application.StartupPath & "\HelpAssistenza.ini")
    Dim Krypto As New TripleDES()
    Dim key As String = "jojg_è@9056gohfg"
    Public IPAddrTestConnection As String
    Public CheckEXE As Int16
    Public CheckAutoUpdate As Short
    Sub RecuperaSettaggi()
        'Recupero Settaggi Sezione Generale INI
        frmConfigurazione.chkvnc.Checked = objIniFile.GetString("GENERALE", "ADSL", "False")
        frmConfigurazione.chkmodem.Checked = objIniFile.GetString("GENERALE", "MODEM", "False")
        frmConfigurazione.txtnomeazienda.Text = objIniFile.GetString("GENERALE", "AZIENDA", String.Empty)
        frmConfigurazione.txtlogo.Text = objIniFile.GetString("GENERALE", "LOGO", "Percorso Logo")
        frmConfigurazione.txtsitointernet.Text = objIniFile.GetString("GENERALE", "SITOINTERNET", String.Empty)
        frmConfigurazione.txttelefonoAzienda.Text = objIniFile.GetString("GENERALE", "TELEFONO", String.Empty)
        frmConfigurazione.txtemailazienda.Text = objIniFile.GetString("GENERALE", "EMAIL", String.Empty)
        frmConfigurazione.cmbprogadsl.Text = objIniFile.GetString("GENERALE", "PROGRAMMA ADSL", String.Empty)
        frmConfigurazione.txtpercorsoadsl.Text = objIniFile.GetString("GENERALE", "PERCORSOADSL1", String.Empty)
        frmConfigurazione.txtpercorsoadsl2.Text = objIniFile.GetString("GENERALE", "PERCORSOADSL2", String.Empty)
        frmConfigurazione.txtpercorsomodem.Text = objIniFile.GetString("GENERALE", "PERCORSO MODEM", String.Empty)
        frmConfigurazione.chkreconn.Checked = objIniFile.GetString("GENERALE", "RICONNESSIONE VNC", "False")
        frmConfigurazione.chkSettaPortePCA.Checked = objIniFile.GetString("GENERALE", "SETTA PORTE PCANY", "False")
        frmConfigurazione.chkNascondiVNCIcon.Checked = objIniFile.GetString("GENERALE", "NASCONDI ICONA VNC", "False")
        frmConfigurazione.chkteamviewer.Checked = objIniFile.GetString("GENERALE", "TEAMVIEWER", "False")
        frmConfigurazione.chkother.Checked = objIniFile.GetString("GENERALE", "ALTROPROG", "False")
        frmConfigurazione.txtteamviewer.Text = objIniFile.GetString("GENERALE", "PERCORSO TEAM VIEWER", String.Empty)
        frmConfigurazione.txtnmealtro.Text = objIniFile.GetString("GENERALE", "NOME ALTROPROG", String.Empty)
        frmConfigurazione.txtother.Text = objIniFile.GetString("GENERALE", "PERCORSO ALTROPROG", String.Empty)
        frmConfigurazione.IconaCollegamentoAlt2 = objIniFile.GetString("GENERALE", "ICONA ALTROPROG", String.Empty)
        IPAddrTestConnection = objIniFile.GetString("GENERALE", "IP_TEST_CONNECTION", "208.69.34.230")
        CheckEXE = objIniFile.GetInteger("GENERALE", "CONTROLLAEXE", 1)
        CheckAutoUpdate = objIniFile.GetInteger("GENERALE", "AUTOUPDATE", 1)
        'Recupero Settaggi Sezione Linee INI
        frmConfigurazione.cmbnumlinee.Text = objIniFile.GetString("LINEE", "NUMERO LINEE", String.Empty)
        frmConfigurazione.txtnomeOPE1.Text = objIniFile.GetString("LINEE", "NOME LINEA01", String.Empty)
        frmConfigurazione.txtCognomeOPE1.Text = objIniFile.GetString("LINEE", "COGNOME LINEA01", String.Empty)
        frmConfigurazione.txtnoteOPE1.Text = objIniFile.GetString("LINEE", "NOTE LINEA01", String.Empty)
        frmConfigurazione.txtparVNC1.Text = objIniFile.GetString("LINEE", "PARVNC LINEA01", String.Empty)
        frmConfigurazione.txtparADSLPcAny1.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA01", String.Empty)
        frmConfigurazione.txtipPCA1.Text = objIniFile.GetString("LINEE", "IPCANY LINEA01", String.Empty)
        frmConfigurazione.txtparPCA1.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA01", String.Empty)

        frmConfigurazione.txtNomeOPE2.Text = objIniFile.GetString("LINEE", "NOME LINEA02", String.Empty)
        frmConfigurazione.txtCognomeOPE2.Text = objIniFile.GetString("LINEE", "COGNOME LINEA02", String.Empty)
        frmConfigurazione.txtnoteOPE2.Text = objIniFile.GetString("LINEE", "NOTE LINEA02", String.Empty)
        frmConfigurazione.txtparVNC2.Text = objIniFile.GetString("LINEE", "PARVNC LINEA02", String.Empty)
        frmConfigurazione.txtparADSLPcAny2.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA02", String.Empty)
        frmConfigurazione.txtipPCA2.Text = objIniFile.GetString("LINEE", "IPCANY LINEA02", String.Empty)
        frmConfigurazione.txtparPCA2.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA02", String.Empty)

        frmConfigurazione.txtNomeOPE3.Text = objIniFile.GetString("LINEE", "NOME LINEA03", String.Empty)
        frmConfigurazione.txtCognomeOPE3.Text = objIniFile.GetString("LINEE", "COGNOME LINEA03", String.Empty)
        frmConfigurazione.txtNoteOPE3.Text = objIniFile.GetString("LINEE", "NOTE LINEA03", String.Empty)
        frmConfigurazione.txtparVNC3.Text = objIniFile.GetString("LINEE", "PARVNC LINEA03", String.Empty)
        frmConfigurazione.txtparADSLPcAny3.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA03", String.Empty)
        frmConfigurazione.txtipPCA3.Text = objIniFile.GetString("LINEE", "IPCANY LINEA03", String.Empty)
        frmConfigurazione.txtparPCA3.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA03", String.Empty)

        frmConfigurazione.txtNomeOPE4.Text = objIniFile.GetString("LINEE", "NOME LINEA04", String.Empty)
        frmConfigurazione.txtCognomeOPE4.Text = objIniFile.GetString("LINEE", "COGNOME LINEA04", String.Empty)
        frmConfigurazione.txtNoteOPE4.Text = objIniFile.GetString("LINEE", "NOTE LINEA04", String.Empty)
        frmConfigurazione.txtparVNC4.Text = objIniFile.GetString("LINEE", "PARVNC LINEA04", String.Empty)
        frmConfigurazione.txtparADSLPcAny4.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA04", String.Empty)
        frmConfigurazione.txtipPCA4.Text = objIniFile.GetString("LINEE", "IPCANY LINEA04", String.Empty)
        frmConfigurazione.txtparPCA4.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA04", String.Empty)

        frmConfigurazione.txtNomeOPE5.Text = objIniFile.GetString("LINEE", "NOME LINEA05", String.Empty)
        frmConfigurazione.txtCognomeOPE5.Text = objIniFile.GetString("LINEE", "COGNOME LINEA05", String.Empty)
        frmConfigurazione.txtNoteOPE5.Text = objIniFile.GetString("LINEE", "NOTE LINEA05", String.Empty)
        frmConfigurazione.txtparVNC5.Text = objIniFile.GetString("LINEE", "PARVNC LINEA05", String.Empty)
        frmConfigurazione.txtparADSLPcAny5.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA05", String.Empty)
        frmConfigurazione.txtipPCA5.Text = objIniFile.GetString("LINEE", "IPCANY LINEA05", String.Empty)
        frmConfigurazione.txtparPCA5.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA05", String.Empty)

        frmConfigurazione.txtNomeOPE6.Text = objIniFile.GetString("LINEE", "NOME LINEA06", String.Empty)
        frmConfigurazione.txtCognomeOPE6.Text = objIniFile.GetString("LINEE", "COGNOME LINEA06", String.Empty)
        frmConfigurazione.txtNoteOPE6.Text = objIniFile.GetString("LINEE", "NOTE LINEA06", String.Empty)
        frmConfigurazione.txtparVNC6.Text = objIniFile.GetString("LINEE", "PARVNC LINEA06", String.Empty)
        frmConfigurazione.txtparADSLPcAny6.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA06", String.Empty)
        frmConfigurazione.txtipPCA6.Text = objIniFile.GetString("LINEE", "IPCANY LINEA06", String.Empty)
        frmConfigurazione.txtparPCA6.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA06", String.Empty)

        frmConfigurazione.txtNomeOPE7.Text = objIniFile.GetString("LINEE", "NOME LINEA07", String.Empty)
        frmConfigurazione.txtCognomeOPE7.Text = objIniFile.GetString("LINEE", "COGNOME LINEA07", String.Empty)
        frmConfigurazione.txtNoteOPE7.Text = objIniFile.GetString("LINEE", "NOTE LINEA07", String.Empty)
        frmConfigurazione.txtparVNC7.Text = objIniFile.GetString("LINEE", "PARVNC LINEA07", String.Empty)
        frmConfigurazione.txtparADSLPcAny7.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA07", String.Empty)
        frmConfigurazione.txtipPCA7.Text = objIniFile.GetString("LINEE", "IPCANY LINEA07", String.Empty)
        frmConfigurazione.txtparPCA7.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA07", String.Empty)

        frmConfigurazione.txtNomeOPE8.Text = objIniFile.GetString("LINEE", "NOME LINEA08", String.Empty)
        frmConfigurazione.txtCognomeOPE8.Text = objIniFile.GetString("LINEE", "COGNOME LINEA08", String.Empty)
        frmConfigurazione.txtNoteOPE8.Text = objIniFile.GetString("LINEE", "NOTE LINEA08", String.Empty)
        frmConfigurazione.txtparVNC8.Text = objIniFile.GetString("LINEE", "PARVNC LINEA08", String.Empty)
        frmConfigurazione.txtparADSLPcAny8.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA08", String.Empty)
        frmConfigurazione.txtipPCA8.Text = objIniFile.GetString("LINEE", "IPCANY LINEA08", String.Empty)
        frmConfigurazione.txtparPCA8.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA08", String.Empty)

        frmConfigurazione.txtNomeOPE9.Text = objIniFile.GetString("LINEE", "NOME LINEA09", String.Empty)
        frmConfigurazione.txtCognomeOPE9.Text = objIniFile.GetString("LINEE", "COGNOME LINEA09", String.Empty)
        frmConfigurazione.txtNoteOPE9.Text = objIniFile.GetString("LINEE", "NOTE LINEA09", String.Empty)
        frmConfigurazione.txtparVNC9.Text = objIniFile.GetString("LINEE", "PARVNC LINEA09", String.Empty)
        frmConfigurazione.txtparADSLPcAny9.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA09", String.Empty)
        frmConfigurazione.txtipPCA9.Text = objIniFile.GetString("LINEE", "IPCANY LINEA09", String.Empty)
        frmConfigurazione.txtparPCA9.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA09", String.Empty)

        frmConfigurazione.txtNomeOPE10.Text = objIniFile.GetString("LINEE", "NOME LINEA10", String.Empty)
        frmConfigurazione.txtCognomeOPE10.Text = objIniFile.GetString("LINEE", "COGNOME LINEA10", String.Empty)
        frmConfigurazione.txtNoteOPE10.Text = objIniFile.GetString("LINEE", "NOTE LINEA10", String.Empty)
        frmConfigurazione.txtparVNC10.Text = objIniFile.GetString("LINEE", "PARVNC LINEA10", String.Empty)
        frmConfigurazione.txtparADSLPcAny10.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA10", String.Empty)
        frmConfigurazione.txtipPCA10.Text = objIniFile.GetString("LINEE", "IPCANY LINEA10", String.Empty)
        frmConfigurazione.txtparPCA10.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA10", String.Empty)

        frmConfigurazione.txtNomeOPE11.Text = objIniFile.GetString("LINEE", "NOME LINEA11", String.Empty)
        frmConfigurazione.txtCognomeOPE11.Text = objIniFile.GetString("LINEE", "COGNOME LINEA11", String.Empty)
        frmConfigurazione.txtNoteOPE11.Text = objIniFile.GetString("LINEE", "NOTE LINEA11", String.Empty)
        frmConfigurazione.txtparVNC11.Text = objIniFile.GetString("LINEE", "PARVNC LINEA11", String.Empty)
        frmConfigurazione.txtparADSLPcAny11.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA11", String.Empty)
        frmConfigurazione.txtipPCA11.Text = objIniFile.GetString("LINEE", "IPCANY LINEA11", String.Empty)
        frmConfigurazione.txtparPCA11.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA11", String.Empty)

        frmConfigurazione.txtNomeOPE12.Text = objIniFile.GetString("LINEE", "NOME LINEA12", String.Empty)
        frmConfigurazione.txtCognomeOPE12.Text = objIniFile.GetString("LINEE", "COGNOME LINEA12", String.Empty)
        frmConfigurazione.txtNoteOPE12.Text = objIniFile.GetString("LINEE", "NOTE LINEA12", String.Empty)
        frmConfigurazione.txtparVNC12.Text = objIniFile.GetString("LINEE", "PARVNC LINEA12", String.Empty)
        frmConfigurazione.txtparADSLPcAny12.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA12", String.Empty)
        frmConfigurazione.txtipPCA12.Text = objIniFile.GetString("LINEE", "IPCANY LINEA12", String.Empty)
        frmConfigurazione.txtparPCA12.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA12", String.Empty)

        frmConfigurazione.txtNomeOPE13.Text = objIniFile.GetString("LINEE", "NOME LINEA13", String.Empty)
        frmConfigurazione.txtCognomeOPE13.Text = objIniFile.GetString("LINEE", "COGNOME LINEA13", String.Empty)
        frmConfigurazione.txtNoteOPE13.Text = objIniFile.GetString("LINEE", "NOTE LINEA13", String.Empty)
        frmConfigurazione.txtparVNC13.Text = objIniFile.GetString("LINEE", "PARVNC LINEA13", String.Empty)
        frmConfigurazione.txtparADSLPcAny13.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA13", String.Empty)
        frmConfigurazione.txtipPCA13.Text = objIniFile.GetString("LINEE", "IPCANY LINEA13", String.Empty)
        frmConfigurazione.txtparPCA13.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA13", String.Empty)

        frmConfigurazione.txtNomeOPE14.Text = objIniFile.GetString("LINEE", "NOME LINEA14", String.Empty)
        frmConfigurazione.txtCognomeOPE14.Text = objIniFile.GetString("LINEE", "COGNOME LINEA14", String.Empty)
        frmConfigurazione.txtNoteOPE14.Text = objIniFile.GetString("LINEE", "NOTE LINEA14", String.Empty)
        frmConfigurazione.txtparVNC14.Text = objIniFile.GetString("LINEE", "PARVNC LINEA14", String.Empty)
        frmConfigurazione.txtparADSLPcAny14.Text = objIniFile.GetString("LINEE", "PARPCANY LINEA14", String.Empty)
        frmConfigurazione.txtipPCA14.Text = objIniFile.GetString("LINEE", "IPCANY LINEA14", String.Empty)
        frmConfigurazione.txtparPCA14.Text = objIniFile.GetString("LINEE", "PARMODEM LINEA14", String.Empty)

        'Recupero Settaggi Sezione Telecontrollo INI
        frmConfigurazione.chkChiamateTeleControllo.Checked = objIniFile.GetString("TELECONTROLLO", "CHIAMATE", "False")
        frmConfigurazione.chkspegniTeleControllo.Checked = objIniFile.GetString("TELECONTROLLO", "SPEGNI-RIAVVIA", "False")
        frmConfigurazione.chkscript.Checked = objIniFile.GetString("TELECONTROLLO", "SCRIPT-MESSAGGI", "False")
        frmConfigurazione.chkFTPPassive.Checked = objIniFile.GetString("TELECONTROLLO", "FTPPASSIVE", "False")
        frmConfigurazione.txtSitoTeleControllo.Text = objIniFile.GetString("TELECONTROLLO", "URLSITO", String.Empty)
        frmConfigurazione.txtPathTeleControllo.Text = objIniFile.GetString("TELECONTROLLO", "PATHSITO", String.Empty)
        frmConfigurazione.chkGeneraPathTeleControllo.Checked = objIniFile.GetString("TELECONTROLLO", "GENERA_PATH", "False")
        frmConfigurazione.txtUserTeleControllo.Text = objIniFile.GetString("TELECONTROLLO", "USERNAME", String.Empty)
        If objIniFile.GetString("TELECONTROLLO", "PASSWORD", String.Empty) = String.Empty Then
            frmConfigurazione.txtPassTeleControllo.Text = objIniFile.GetString("TELECONTROLLO", "PASSWORD", String.Empty)
        Else
            Dim decriptata As String = Krypto.TripleDESDecode(objIniFile.GetString("TELECONTROLLO", "PASSWORD", String.Empty), key)
            frmConfigurazione.txtPassTeleControllo.Text = decriptata
        End If
        frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex = objIniFile.GetInteger("TELECONTROLLO", "INTERVALLO", 2)

        'Recupero Settaggi Sezione Transfer File
        frmConfigurazione.chkAbilitaUploadFile.Checked = objIniFile.GetString("TRANSFERFILE", "ABILITATO", "False")
        frmConfigurazione.chkUPugualeTele.Checked = objIniFile.GetString("TRANSFERFILE", "UGUALE_TELECONTROLLO", "True")
        frmConfigurazione.chkFTPPassTrans.Checked = objIniFile.GetString("TRANSFERFILE", "FTPPASSIVE", "False")
        frmConfigurazione.txtServerUP.Text = objIniFile.GetString("TRANSFERFILE", "SERVER_UPLOAD", String.Empty)
        frmConfigurazione.txtpathServerUp.Text = objIniFile.GetString("TRANSFERFILE", "PATH_UPLOAD", String.Empty)
        frmConfigurazione.txtUserNameServerUp.Text = objIniFile.GetString("TRANSFERFILE", "USER_UPLOAD", String.Empty)
        If objIniFile.GetString("TRANSFERFILE", "PASS_UPLOAD", String.Empty) = String.Empty Then
            frmConfigurazione.txtPasswordServerUp.Text = objIniFile.GetString("TRANSFERFILE", "PASS_UPLOAD", String.Empty)
        Else
            Dim DecryTrans As String = Krypto.TripleDESDecode(objIniFile.GetString("TRANSFERFILE", "PASS_UPLOAD", String.Empty), key)
            frmConfigurazione.txtPasswordServerUp.Text = DecryTrans
        End If
        frmConfigurazione.txtServerWeb.Text = objIniFile.GetString("TRANSFERFILE", "INDIRIZZOHTTP", String.Empty)
        frmConfigurazione.chkAbilitaDownloadFile.Checked = objIniFile.GetString("TRANSFERFILE", "DOWNLOAD", "False") 'Sottosezione Download
        frmConfigurazione.chkdezip.Checked = objIniFile.GetString("TRANSFERFILE", "DEZIP", "False")
        frmConfigurazione.chklistdownweb.Checked = objIniFile.GetString("TRANSFERFILE", "FTPLISTWEB", "False")
        frmConfigurazione.cmbdownloadconn.SelectedIndex = objIniFile.GetInteger("TRANSFERFILE", "TIPODOWNLOAD", 0) - 1

        'Recupero Settaggi Sezione Project
        frmConfigurazione.chkAbilitaProjector.Checked = objIniFile.GetString("PROJECTOR", "ABILITATO", "False")
        frmConfigurazione.chkprojFullScreen.Checked = objIniFile.GetString("PROJECTOR", "FULLSCREEN", "False")
        frmConfigurazione.chkonlyprojector.Checked = objIniFile.GetString("PROJECTOR", "SOLOPROJECTOR", "False")
        frmConfigurazione.txtNomeStanza1.Text = objIniFile.GetString("PROJECTOR", "NOMESTANZA1", String.Empty)
        frmConfigurazione.txtNomeStanza2.Text = objIniFile.GetString("PROJECTOR", "NOMESTANZA2", String.Empty)
        frmConfigurazione.txtProjhost1.Text = objIniFile.GetString("PROJECTOR", "HOST1", String.Empty)
        frmConfigurazione.txtProjhost2.Text = objIniFile.GetString("PROJECTOR", "HOST2", String.Empty)
        frmConfigurazione.txtportaproj1.Text = Convert.ToString(objIniFile.GetString("PROJECTOR", "PORTA1", "5900"))
        frmConfigurazione.txtportaproj2.Text = Convert.ToString(objIniFile.GetString("PROJECTOR", "PORTA2", "5900"))

        'Recupero Settaggi Sezione eMAil
        frmConfigurazione.vtxtemailaz.Text = objIniFile.GetString("EMAIL", "ASSISTENTE", String.Empty)
        frmConfigurazione.chkeMailtecnici.Checked = objIniFile.GetString("EMAIL", "ABILITATECNICI", "False")
        frmConfigurazione.chkinfoPC.Checked = objIniFile.GetString("EMAIL", "INFOPC", "False")
        frmConfigurazione.txtnomemittente.Text = objIniFile.GetString("EMAIL", "NOMEMITTENTE", String.Empty)
        frmConfigurazione.vtxtmailmittente.Text = objIniFile.GetString("EMAIL", "MITTENTE", String.Empty)
        frmConfigurazione.txtServerSMTP.Text = objIniFile.GetString("EMAIL", "SERVERSMTP", String.Empty)
        frmConfigurazione.txtportaSMTP.Text = objIniFile.GetString("EMAIL", "PORTASMTP", "25")
        frmConfigurazione.chkSSL.Checked = objIniFile.GetString("EMAIL", "SSL", "False")
        frmConfigurazione.txtuserSMTP.Text = objIniFile.GetString("EMAIL", "USERNAME", String.Empty)
        'frmConfigurazione.txtpassSMTP.Text = objIniFile.GetString("EMAIL", "PASSWORD", String.Empty)
        If objIniFile.GetString("EMAIL", "PASSWORD", String.Empty) = String.Empty Then
            frmConfigurazione.txtpassSMTP.Text = objIniFile.GetString("EMAIL", "PASSWORD", String.Empty)
        Else
            Dim DecryTrans2 As String = Krypto.TripleDESDecode(objIniFile.GetString("EMAIL", "PASSWORD", String.Empty), key)
            frmConfigurazione.txtpassSMTP.Text = DecryTrans2
        End If

        'Recupero Settaggi Sezione Report INI
        frmConfigurazione.chkabilitaReport.Checked = objIniFile.GetString("REPORT", "ABILITATO", "False")
        frmConfigurazione.chkCronoSessioni.Checked = objIniFile.GetString("REPORT", "CRONOMETRA", "False")
        frmConfigurazione.chkoperatoreReport.Checked = objIniFile.GetString("REPORT", "OPERATORE", "False")
        frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked = objIniFile.GetString("REPORT", "NOTIFICHE", "False")
        frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text = objIniFile.GetString("REPORT", "MESSAGGIO INTERRUZZIONI", String.Empty)
        frmConfigurazione.chkChiudiInterruzzioneassi.Checked = objIniFile.GetString("REPORT", "CHIUDI INTERRUZZIONE", "False")
        frmConfigurazione.chkabilitaLog.Checked = objIniFile.GetString("REPORT", "ABILITA LOG", "False")
        frmConfigurazione.chkabilitaStampa.Checked = objIniFile.GetString("REPORT", "STAMPA", "False")
    End Sub
    Sub ScriviSettaggi()
        'Salvo Settaggi Sezione Generale INI
        objIniFile.WriteString("GENERALE", "ADSL", frmConfigurazione.chkvnc.Checked)
        objIniFile.WriteString("GENERALE", "MODEM", frmConfigurazione.chkmodem.Checked)
        objIniFile.WriteString("GENERALE", "AZIENDA", frmConfigurazione.txtnomeazienda.Text)
        objIniFile.WriteString("GENERALE", "LOGO", frmConfigurazione.txtlogo.Text)
        objIniFile.WriteString("GENERALE", "SITOINTERNET", frmConfigurazione.txtsitointernet.Text)
        objIniFile.WriteString("GENERALE", "TELEFONO", frmConfigurazione.txttelefonoAzienda.Text)
        objIniFile.WriteString("GENERALE", "EMAIL", frmConfigurazione.txtemailazienda.Text)
        objIniFile.WriteString("GENERALE", "PROGRAMMA ADSL", frmConfigurazione.cmbprogadsl.Text)
        objIniFile.WriteString("GENERALE", "PERCORSOADSL1", frmConfigurazione.txtpercorsoadsl.Text)
        objIniFile.WriteString("GENERALE", "PERCORSOADSL2", frmConfigurazione.txtpercorsoadsl2.Text)
        objIniFile.WriteString("GENERALE", "PERCORSO MODEM", frmConfigurazione.txtpercorsomodem.Text)
        objIniFile.WriteString("GENERALE", "RICONNESSIONE VNC", frmConfigurazione.chkreconn.Checked)
        objIniFile.WriteString("GENERALE", "SETTA PORTE PCANY", frmConfigurazione.chkSettaPortePCA.Checked)
        objIniFile.WriteString("GENERALE", "NASCONDI ICONA VNC", frmConfigurazione.chkNascondiVNCIcon.Checked)
        objIniFile.WriteString("GENERALE", "TEAMVIEWER", frmConfigurazione.chkteamviewer.Checked)
        objIniFile.WriteString("GENERALE", "ALTROPROG", frmConfigurazione.chkother.Checked)
        objIniFile.WriteString("GENERALE", "PERCORSO TEAM VIEWER", frmConfigurazione.txtteamviewer.Text)
        objIniFile.WriteString("GENERALE", "NOME ALTROPROG", frmConfigurazione.txtnmealtro.Text)
        objIniFile.WriteString("GENERALE", "PERCORSO ALTROPROG", frmConfigurazione.txtother.Text)
        objIniFile.WriteString("GENERALE", "ICONA ALTROPROG", frmConfigurazione.IconaCollegamentoAlt2)
        'Salvo Settaggi Sezione Linee INI
        objIniFile.WriteString("LINEE", "NUMERO LINEE", frmConfigurazione.cmbnumlinee.Text)
        objIniFile.WriteString("LINEE", "NOME LINEA01", frmConfigurazione.txtnomeOPE1.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA01", frmConfigurazione.txtCognomeOPE1.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA01", frmConfigurazione.txtnoteOPE1.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA01", frmConfigurazione.txtparVNC1.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA01", frmConfigurazione.txtparADSLPcAny1.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA01", frmConfigurazione.txtipPCA1.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA01", frmConfigurazione.txtparPCA1.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA02", frmConfigurazione.txtNomeOPE2.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA02", frmConfigurazione.txtCognomeOPE2.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA02", frmConfigurazione.txtnoteOPE2.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA02", frmConfigurazione.txtparVNC2.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA02", frmConfigurazione.txtparADSLPcAny2.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA02", frmConfigurazione.txtipPCA2.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA02", frmConfigurazione.txtparPCA2.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA03", frmConfigurazione.txtNomeOPE3.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA03", frmConfigurazione.txtCognomeOPE3.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA03", frmConfigurazione.txtNoteOPE3.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA03", frmConfigurazione.txtparVNC3.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA03", frmConfigurazione.txtparADSLPcAny3.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA03", frmConfigurazione.txtipPCA3.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA03", frmConfigurazione.txtparPCA3.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA04", frmConfigurazione.txtNomeOPE4.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA04", frmConfigurazione.txtCognomeOPE4.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA04", frmConfigurazione.txtNoteOPE4.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA04", frmConfigurazione.txtparVNC4.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA04", frmConfigurazione.txtparADSLPcAny4.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA04", frmConfigurazione.txtipPCA4.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA04", frmConfigurazione.txtparPCA4.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA05", frmConfigurazione.txtNomeOPE5.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA05", frmConfigurazione.txtCognomeOPE5.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA05", frmConfigurazione.txtNoteOPE5.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA05", frmConfigurazione.txtparVNC5.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA05", frmConfigurazione.txtparADSLPcAny5.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA05", frmConfigurazione.txtipPCA5.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA05", frmConfigurazione.txtparPCA5.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA06", frmConfigurazione.txtNomeOPE6.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA06", frmConfigurazione.txtCognomeOPE6.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA06", frmConfigurazione.txtNoteOPE6.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA06", frmConfigurazione.txtparVNC6.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA06", frmConfigurazione.txtparADSLPcAny6.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA06", frmConfigurazione.txtipPCA6.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA06", frmConfigurazione.txtparPCA6.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA07", frmConfigurazione.txtNomeOPE7.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA07", frmConfigurazione.txtCognomeOPE7.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA07", frmConfigurazione.txtNoteOPE7.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA07", frmConfigurazione.txtparVNC7.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA07", frmConfigurazione.txtparADSLPcAny7.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA07", frmConfigurazione.txtipPCA7.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA07", frmConfigurazione.txtparPCA7.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA08", frmConfigurazione.txtNomeOPE8.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA08", frmConfigurazione.txtCognomeOPE8.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA08", frmConfigurazione.txtNoteOPE8.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA08", frmConfigurazione.txtparVNC8.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA08", frmConfigurazione.txtparADSLPcAny8.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA08", frmConfigurazione.txtipPCA8.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA08", frmConfigurazione.txtparPCA8.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA09", frmConfigurazione.txtNomeOPE9.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA09", frmConfigurazione.txtCognomeOPE9.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA09", frmConfigurazione.txtNoteOPE9.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA09", frmConfigurazione.txtparVNC9.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA09", frmConfigurazione.txtparADSLPcAny9.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA09", frmConfigurazione.txtipPCA9.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA09", frmConfigurazione.txtparPCA9.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA10", frmConfigurazione.txtNomeOPE10.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA10", frmConfigurazione.txtCognomeOPE10.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA10", frmConfigurazione.txtNoteOPE10.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA10", frmConfigurazione.txtparVNC10.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA10", frmConfigurazione.txtparADSLPcAny10.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA10", frmConfigurazione.txtipPCA10.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA10", frmConfigurazione.txtparPCA10.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA11", frmConfigurazione.txtNomeOPE11.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA11", frmConfigurazione.txtCognomeOPE11.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA11", frmConfigurazione.txtNoteOPE11.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA11", frmConfigurazione.txtparVNC11.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA11", frmConfigurazione.txtparADSLPcAny11.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA11", frmConfigurazione.txtipPCA11.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA11", frmConfigurazione.txtparPCA11.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA12", frmConfigurazione.txtNomeOPE12.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA12", frmConfigurazione.txtCognomeOPE12.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA12", frmConfigurazione.txtNoteOPE12.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA12", frmConfigurazione.txtparVNC12.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA12", frmConfigurazione.txtparADSLPcAny12.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA12", frmConfigurazione.txtipPCA12.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA12", frmConfigurazione.txtparPCA12.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA13", frmConfigurazione.txtNomeOPE13.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA13", frmConfigurazione.txtCognomeOPE13.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA13", frmConfigurazione.txtNoteOPE13.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA13", frmConfigurazione.txtparVNC13.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA13", frmConfigurazione.txtparADSLPcAny13.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA13", frmConfigurazione.txtipPCA13.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA13", frmConfigurazione.txtparPCA13.Text)

        objIniFile.WriteString("LINEE", "NOME LINEA14", frmConfigurazione.txtNomeOPE14.Text)
        objIniFile.WriteString("LINEE", "COGNOME LINEA14", frmConfigurazione.txtCognomeOPE14.Text)
        objIniFile.WriteString("LINEE", "NOTE LINEA14", frmConfigurazione.txtNoteOPE14.Text)
        objIniFile.WriteString("LINEE", "PARVNC LINEA14", frmConfigurazione.txtparVNC14.Text)
        objIniFile.WriteString("LINEE", "PARPCANY LINEA14", frmConfigurazione.txtparADSLPcAny14.Text)
        objIniFile.WriteString("LINEE", "IPCANY LINEA14", frmConfigurazione.txtipPCA14.Text)
        objIniFile.WriteString("LINEE", "PARMODEM LINEA14", frmConfigurazione.txtparPCA14.Text)

        'Salvo Settaggi Sezione Telecontrollo INI
        objIniFile.WriteString("TELECONTROLLO", "CHIAMATE", frmConfigurazione.chkChiamateTeleControllo.Checked)
        objIniFile.WriteString("TELECONTROLLO", "SPEGNI-RIAVVIA", frmConfigurazione.chkspegniTeleControllo.Checked)
        objIniFile.WriteString("TELECONTROLLO", "SCRIPT-MESSAGGI", frmConfigurazione.chkscript.Checked)
        objIniFile.WriteString("TELECONTROLLO", "FTPPASSIVE", frmConfigurazione.chkFTPPassive.Checked)
        objIniFile.WriteString("TELECONTROLLO", "URLSITO", frmConfigurazione.txtSitoTeleControllo.Text)
        objIniFile.WriteString("TELECONTROLLO", "PATHSITO", frmConfigurazione.txtPathTeleControllo.Text)
        objIniFile.WriteString("TELECONTROLLO", "GENERA_PATH", frmConfigurazione.chkGeneraPathTeleControllo.Checked)
        objIniFile.WriteString("TELECONTROLLO", "USERNAME", frmConfigurazione.txtUserTeleControllo.Text)
        If frmConfigurazione.txtPassTeleControllo.Text = String.Empty Then
            objIniFile.WriteString("TELECONTROLLO", "PASSWORD", frmConfigurazione.txtPassTeleControllo.Text)
        Else
            Dim cryptata As String = Krypto.TripleDESEncode(frmConfigurazione.txtPassTeleControllo.Text, key)
            objIniFile.WriteString("TELECONTROLLO", "PASSWORD", cryptata)
        End If
        objIniFile.WriteInteger("TELECONTROLLO", "INTERVALLO", frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex)

        'Salvo Settaggi Transfer File
        objIniFile.WriteString("TRANSFERFILE", "ABILITATO", frmConfigurazione.chkAbilitaUploadFile.Checked)
        objIniFile.WriteString("TRANSFERFILE", "UGUALE_TELECONTROLLO", frmConfigurazione.chkUPugualeTele.Checked)
        objIniFile.WriteString("TRANSFERFILE", "FTPPASSIVE", frmConfigurazione.chkFTPPassTrans.Checked)
        objIniFile.WriteString("TRANSFERFILE", "SERVER_UPLOAD", frmConfigurazione.txtServerUP.Text)
        objIniFile.WriteString("TRANSFERFILE", "PATH_UPLOAD", frmConfigurazione.txtpathServerUp.Text)
        objIniFile.WriteString("TRANSFERFILE", "USER_UPLOAD", frmConfigurazione.txtUserNameServerUp.Text)
        If frmConfigurazione.txtPasswordServerUp.Text = String.Empty Then
            objIniFile.WriteString("TRANSFERFILE", "PASS_UPLOAD", frmConfigurazione.txtPasswordServerUp.Text)
        Else
            Dim cyptTrans As String = Krypto.TripleDESEncode(frmConfigurazione.txtPasswordServerUp.Text, key)
            objIniFile.WriteString("TRANSFERFILE", "PASS_UPLOAD", cyptTrans)
        End If
        objIniFile.WriteString("TRANSFERFILE", "INDIRIZZOHTTP", frmConfigurazione.txtServerWeb.Text)
        objIniFile.WriteString("TRANSFERFILE", "DOWNLOAD", frmConfigurazione.chkAbilitaDownloadFile.Checked) 'Sottosezione Download
        objIniFile.WriteString("TRANSFERFILE", "DEZIP", frmConfigurazione.chkdezip.Checked)
        objIniFile.WriteString("TRANSFERFILE", "FTPLISTWEB", frmConfigurazione.chklistdownweb.Checked)
        objIniFile.WriteInteger("TRANSFERFILE", "TIPODOWNLOAD", frmConfigurazione.cmbdownloadconn.SelectedIndex + 1)

        'Salvo Settaggi Sezione Projector
        objIniFile.WriteString("PROJECTOR", "ABILITATO", frmConfigurazione.chkAbilitaProjector.Checked)
        objIniFile.WriteString("PROJECTOR", "FULLSCREEN", frmConfigurazione.chkprojFullScreen.Checked)
        objIniFile.WriteString("PROJECTOR", "SOLOPROJECTOR", frmConfigurazione.chkonlyprojector.Checked)
        objIniFile.WriteString("PROJECTOR", "NOMESTANZA1", frmConfigurazione.txtNomeStanza1.Text)
        objIniFile.WriteString("PROJECTOR", "NOMESTANZA2", frmConfigurazione.txtNomeStanza2.Text)
        objIniFile.WriteString("PROJECTOR", "HOST1", frmConfigurazione.txtProjhost1.Text)
        objIniFile.WriteString("PROJECTOR", "HOST2", frmConfigurazione.txtProjhost2.Text)
        objIniFile.WriteInteger("PROJECTOR", "PORTA1", Convert.ToInt64(frmConfigurazione.txtportaproj1.Text))
        objIniFile.WriteInteger("PROJECTOR", "PORTA2", Convert.ToInt64(frmConfigurazione.txtportaproj2.Text))

        'Salvo Settaggi Sezione eMail
        objIniFile.WriteString("EMAIL", "ASSISTENTE", frmConfigurazione.vtxtemailaz.Text)
        objIniFile.WriteString("EMAIL", "ABILITATECNICI", frmConfigurazione.chkeMailtecnici.Checked)
        objIniFile.WriteString("EMAIL", "INFOPC", frmConfigurazione.chkinfoPC.Checked)
        objIniFile.WriteString("EMAIL", "NOMEMITTENTE", frmConfigurazione.txtnomemittente.Text)
        objIniFile.WriteString("EMAIL", "MITTENTE", frmConfigurazione.vtxtmailmittente.Text)
        objIniFile.WriteString("EMAIL", "SERVERSMTP", frmConfigurazione.txtServerSMTP.Text)
        objIniFile.WriteString("EMAIL", "PORTASMTP", frmConfigurazione.txtportaSMTP.Text)
        objIniFile.WriteString("EMAIL", "SSL", frmConfigurazione.chkSSL.Checked)
        objIniFile.WriteString("EMAIL", "USERNAME", frmConfigurazione.txtuserSMTP.Text)
        If frmConfigurazione.txtpassSMTP.Text = String.Empty Then
            objIniFile.WriteString("EMAIL", "PASSWORD", String.Empty)
        Else
            Dim cyptTrans2 As String = Krypto.TripleDESEncode(frmConfigurazione.txtpassSMTP.Text, key)
            objIniFile.WriteString("EMAIL", "PASSWORD", cyptTrans2)
        End If

        'Salvo Settaggi Sezione Report INI
        objIniFile.WriteString("REPORT", "ABILITATO", frmConfigurazione.chkabilitaReport.Checked)
        objIniFile.WriteString("REPORT", "CRONOMETRA", frmConfigurazione.chkCronoSessioni.Checked)
        objIniFile.WriteString("REPORT", "OPERATORE", frmConfigurazione.chkoperatoreReport.Checked)
        objIniFile.WriteString("REPORT", "NOTIFICHE", frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked)
        objIniFile.WriteString("REPORT", "MESSAGGIO INTERRUZZIONI", frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text)
        objIniFile.WriteString("REPORT", "CHIUDI INTERRUZZIONE", frmConfigurazione.chkChiudiInterruzzioneassi.Checked)
        objIniFile.WriteString("REPORT", "ABILITA LOG", frmConfigurazione.chkabilitaLog.Checked)
        objIniFile.WriteString("REPORT", "STAMPA", frmConfigurazione.chkabilitaStampa.Checked)
    End Sub

    Sub RicollegaAvvioReg(ByVal Numlinea As Integer)
        Dim parametro As String = String.Empty
        Select Case Numlinea
            Case Is = 1
                parametro = " -call01"
            Case Is = 2
                parametro = " -call02"
            Case Is = 3
                parametro = " -call03"
            Case Is = 4
                parametro = " -call04"
            Case Is = 5
                parametro = " -call05"
            Case Is = 6
                parametro = " -call06"
            Case Is = 7
                parametro = " -call07"
            Case Is = 8
                parametro = " -call08"
            Case Is = 9
                parametro = " -call09"
            Case Is = 10
                parametro = " -call10"
            Case Is = 11
                parametro = " -call11"
            Case Is = 12
                parametro = " -call12"
            Case Is = 13
                parametro = " -call13"
            Case Is = 14
                parametro = " -call14"
        End Select

        Dim PercorsoApp As String = Chr(34) & Application.ExecutablePath & Chr(34) & parametro
        Dim registro As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\RunOnce", True)
        Try
            registro.SetValue("HelpAssistenzaCall", PercorsoApp)
        Catch errore As Exception
            MessageBox.Show("Errore nel Salvataggio su Registro!" + vbCrLf + errore.Message, "HelpAssistenza - Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Settaggi Temporanei per Transfer File 
    Public CheckUgualeTele As Boolean
    Public SitoTelecontrollo As String
    Public PathSitoTeleControllo As String
    Public UserTeleControllo As String
    Public PassTeleControllo As String
    Public SitoServerUP As String
    Public PathServerUP As String
    Public UserServerUP As String
    Public PassServerUP As String
    Public FileTransefer As Short = 0
    Public UploadFileTransf As String = String.Empty
    Public FTPasv As Boolean = False
    Sub SetTemporaneiUp()
        CheckUgualeTele = frmConfigurazione.chkUPugualeTele.Checked
        SitoTelecontrollo = frmConfigurazione.txtSitoTeleControllo.Text
        PathSitoTeleControllo = frmConfigurazione.txtPathTeleControllo.Text
        UserTeleControllo = frmConfigurazione.txtUserTeleControllo.Text
        PassTeleControllo = frmConfigurazione.txtPassTeleControllo.Text
        SitoServerUP = frmConfigurazione.txtServerUP.Text
        PathServerUP = frmConfigurazione.txtpathServerUp.Text
        UserServerUP = frmConfigurazione.txtUserNameServerUp.Text
        PassServerUP = frmConfigurazione.txtPasswordServerUp.Text
        FTPasv = frmConfigurazione.chkFTPPassTrans.Checked
    End Sub
    'Aggiornamento INI
    Sub AggiornamentoINI()
        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.ini.upd") Then
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.ini.old") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\HelpAssistenza.ini.old")
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.ini") Then My.Computer.FileSystem.RenameFile(Application.StartupPath + "\HelpAssistenza.ini", "HelpAssistenza.ini.old")
            My.Computer.FileSystem.RenameFile(Application.StartupPath + "\HelpAssistenza.ini.upd", "HelpAssistenza.ini")
        End If
    End Sub

    Sub RecuperaSettaggi_UpdINI(ByVal fileUPDINI As String)
        Dim updIniFile As New INIFiles(fileUPDINI)
        'Recupero Settaggi Sezione Generale INI
        frmConfigurazione.chkvnc.Checked = updIniFile.GetString("GENERALE", "ADSL", frmConfigurazione.chkvnc.Checked)
        frmConfigurazione.chkmodem.Checked = updIniFile.GetString("GENERALE", "MODEM", frmConfigurazione.chkmodem.Checked)
        frmConfigurazione.txtnomeazienda.Text = updIniFile.GetString("GENERALE", "AZIENDA", frmConfigurazione.txtnomeazienda.Text)
        frmConfigurazione.txtlogo.Text = updIniFile.GetString("GENERALE", "LOGO", frmConfigurazione.txtlogo.Text)
        frmConfigurazione.txtsitointernet.Text = updIniFile.GetString("GENERALE", "SITOINTERNET", frmConfigurazione.txtsitointernet.Text)
        frmConfigurazione.txttelefonoAzienda.Text = updIniFile.GetString("GENERALE", "TELEFONO", frmConfigurazione.txttelefonoAzienda.Text)
        frmConfigurazione.txtemailazienda.Text = updIniFile.GetString("GENERALE", "EMAIL", frmConfigurazione.txtemailazienda.Text)
        frmConfigurazione.cmbprogadsl.Text = updIniFile.GetString("GENERALE", "PROGRAMMA ADSL", frmConfigurazione.cmbprogadsl.Text)
        frmConfigurazione.txtpercorsoadsl.Text = updIniFile.GetString("GENERALE", "PERCORSOADSL1", frmConfigurazione.txtpercorsoadsl.Text)
        frmConfigurazione.txtpercorsoadsl2.Text = updIniFile.GetString("GENERALE", "PERCORSOADSL2", frmConfigurazione.txtpercorsoadsl2.Text)
        frmConfigurazione.txtpercorsomodem.Text = updIniFile.GetString("GENERALE", "PERCORSO MODEM", frmConfigurazione.txtpercorsomodem.Text)
        frmConfigurazione.chkreconn.Checked = updIniFile.GetString("GENERALE", "RICONNESSIONE VNC", frmConfigurazione.chkreconn.Checked)
        frmConfigurazione.chkSettaPortePCA.Checked = updIniFile.GetString("GENERALE", "SETTA PORTE PCANY", frmConfigurazione.chkSettaPortePCA.Checked)
        frmConfigurazione.chkNascondiVNCIcon.Checked = updIniFile.GetString("GENERALE", "NASCONDI ICONA VNC", frmConfigurazione.chkNascondiVNCIcon.Checked)
        frmConfigurazione.chkteamviewer.Checked = updIniFile.GetString("GENERALE", "TEAMVIEWER", frmConfigurazione.chkteamviewer.Checked)
        frmConfigurazione.chkother.Checked = updIniFile.GetString("GENERALE", "ALTROPROG", frmConfigurazione.chkother.Checked)
        frmConfigurazione.txtteamviewer.Text = updIniFile.GetString("GENERALE", "PERCORSO TEAM VIEWER", frmConfigurazione.txtteamviewer.Text)
        frmConfigurazione.txtnmealtro.Text = updIniFile.GetString("GENERALE", "NOME ALTROPROG", frmConfigurazione.txtnmealtro.Text)
        frmConfigurazione.txtother.Text = updIniFile.GetString("GENERALE", "PERCORSO ALTROPROG", frmConfigurazione.txtother.Text)
        frmConfigurazione.IconaCollegamentoAlt2 = objIniFile.GetString("GENERALE", "ICONA ALTROPROG", String.Empty)
        IPAddrTestConnection = updIniFile.GetString("GENERALE", "IP_TEST_CONNECTION", IPAddrTestConnection)
        CheckEXE = updIniFile.GetInteger("GENERALE", "CONTROLLAEXE", CheckEXE)
        CheckAutoUpdate = updIniFile.GetInteger("GENERALE", "AUTOUPDATE", CheckAutoUpdate)
        'Recupero Settaggi Sezione Linee INI
        frmConfigurazione.cmbnumlinee.Text = updIniFile.GetString("LINEE", "NUMERO LINEE", frmConfigurazione.cmbnumlinee.Text)
        frmConfigurazione.txtnomeOPE1.Text = updIniFile.GetString("LINEE", "NOME LINEA01", frmConfigurazione.txtnomeOPE1.Text)
        frmConfigurazione.txtCognomeOPE1.Text = updIniFile.GetString("LINEE", "COGNOME LINEA01", frmConfigurazione.txtCognomeOPE1.Text)
        frmConfigurazione.txtnoteOPE1.Text = updIniFile.GetString("LINEE", "NOTE LINEA01", frmConfigurazione.txtnoteOPE1.Text)
        frmConfigurazione.txtparVNC1.Text = updIniFile.GetString("LINEE", "PARVNC LINEA01", frmConfigurazione.txtparVNC1.Text)
        frmConfigurazione.txtparADSLPcAny1.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA01", frmConfigurazione.txtparADSLPcAny1.Text)
        frmConfigurazione.txtipPCA1.Text = updIniFile.GetString("LINEE", "IPCANY LINEA01", frmConfigurazione.txtipPCA1.Text)
        frmConfigurazione.txtparPCA1.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA01", frmConfigurazione.txtparPCA1.Text)

        frmConfigurazione.txtNomeOPE2.Text = updIniFile.GetString("LINEE", "NOME LINEA02", frmConfigurazione.txtNomeOPE2.Text)
        frmConfigurazione.txtCognomeOPE2.Text = updIniFile.GetString("LINEE", "COGNOME LINEA02", frmConfigurazione.txtCognomeOPE2.Text)
        frmConfigurazione.txtnoteOPE2.Text = updIniFile.GetString("LINEE", "NOTE LINEA02", frmConfigurazione.txtnoteOPE2.Text)
        frmConfigurazione.txtparVNC2.Text = updIniFile.GetString("LINEE", "PARVNC LINEA02", frmConfigurazione.txtparVNC2.Text)
        frmConfigurazione.txtparADSLPcAny2.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA02", frmConfigurazione.txtparADSLPcAny2.Text)
        frmConfigurazione.txtipPCA2.Text = updIniFile.GetString("LINEE", "IPCANY LINEA02", frmConfigurazione.txtipPCA2.Text)
        frmConfigurazione.txtparPCA2.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA02", frmConfigurazione.txtparPCA2.Text)

        frmConfigurazione.txtNomeOPE3.Text = updIniFile.GetString("LINEE", "NOME LINEA03", frmConfigurazione.txtNomeOPE3.Text)
        frmConfigurazione.txtCognomeOPE3.Text = updIniFile.GetString("LINEE", "COGNOME LINEA03", frmConfigurazione.txtCognomeOPE3.Text)
        frmConfigurazione.txtnoteOPE3.Text = updIniFile.GetString("LINEE", "NOTE LINEA03", frmConfigurazione.txtnoteOPE3.Text)
        frmConfigurazione.txtparVNC3.Text = updIniFile.GetString("LINEE", "PARVNC LINEA03", frmConfigurazione.txtparVNC3.Text)
        frmConfigurazione.txtparADSLPcAny3.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA03", frmConfigurazione.txtparADSLPcAny3.Text)
        frmConfigurazione.txtipPCA3.Text = updIniFile.GetString("LINEE", "IPCANY LINEA03", frmConfigurazione.txtipPCA3.Text)
        frmConfigurazione.txtparPCA3.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA03", frmConfigurazione.txtparPCA3.Text)

        frmConfigurazione.txtNomeOPE4.Text = updIniFile.GetString("LINEE", "NOME LINEA04", frmConfigurazione.txtNomeOPE4.Text)
        frmConfigurazione.txtCognomeOPE4.Text = updIniFile.GetString("LINEE", "COGNOME LINEA04", frmConfigurazione.txtCognomeOPE4.Text)
        frmConfigurazione.txtnoteOPE4.Text = updIniFile.GetString("LINEE", "NOTE LINEA04", frmConfigurazione.txtnoteOPE4.Text)
        frmConfigurazione.txtparVNC4.Text = updIniFile.GetString("LINEE", "PARVNC LINEA04", frmConfigurazione.txtparVNC4.Text)
        frmConfigurazione.txtparADSLPcAny4.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA04", frmConfigurazione.txtparADSLPcAny4.Text)
        frmConfigurazione.txtipPCA4.Text = updIniFile.GetString("LINEE", "IPCANY LINEA04", frmConfigurazione.txtipPCA4.Text)
        frmConfigurazione.txtparPCA4.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA04", frmConfigurazione.txtparPCA4.Text)

        frmConfigurazione.txtNomeOPE5.Text = updIniFile.GetString("LINEE", "NOME LINEA05", frmConfigurazione.txtNomeOPE5.Text)
        frmConfigurazione.txtCognomeOPE5.Text = updIniFile.GetString("LINEE", "COGNOME LINEA05", frmConfigurazione.txtCognomeOPE5.Text)
        frmConfigurazione.txtnoteOPE5.Text = updIniFile.GetString("LINEE", "NOTE LINEA05", frmConfigurazione.txtnoteOPE5.Text)
        frmConfigurazione.txtparVNC5.Text = updIniFile.GetString("LINEE", "PARVNC LINEA05", frmConfigurazione.txtparVNC5.Text)
        frmConfigurazione.txtparADSLPcAny5.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA05", frmConfigurazione.txtparADSLPcAny5.Text)
        frmConfigurazione.txtipPCA5.Text = updIniFile.GetString("LINEE", "IPCANY LINEA05", frmConfigurazione.txtipPCA5.Text)
        frmConfigurazione.txtparPCA5.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA05", frmConfigurazione.txtparPCA5.Text)

        frmConfigurazione.txtNomeOPE6.Text = updIniFile.GetString("LINEE", "NOME LINEA06", frmConfigurazione.txtNomeOPE6.Text)
        frmConfigurazione.txtCognomeOPE6.Text = updIniFile.GetString("LINEE", "COGNOME LINEA06", frmConfigurazione.txtCognomeOPE6.Text)
        frmConfigurazione.txtnoteOPE6.Text = updIniFile.GetString("LINEE", "NOTE LINEA06", frmConfigurazione.txtnoteOPE6.Text)
        frmConfigurazione.txtparVNC6.Text = updIniFile.GetString("LINEE", "PARVNC LINEA06", frmConfigurazione.txtparVNC6.Text)
        frmConfigurazione.txtparADSLPcAny6.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA06", frmConfigurazione.txtparADSLPcAny6.Text)
        frmConfigurazione.txtipPCA6.Text = updIniFile.GetString("LINEE", "IPCANY LINEA06", frmConfigurazione.txtipPCA6.Text)
        frmConfigurazione.txtparPCA6.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA06", frmConfigurazione.txtparPCA6.Text)

        frmConfigurazione.txtNomeOPE7.Text = updIniFile.GetString("LINEE", "NOME LINEA07", frmConfigurazione.txtNomeOPE7.Text)
        frmConfigurazione.txtCognomeOPE7.Text = updIniFile.GetString("LINEE", "COGNOME LINEA07", frmConfigurazione.txtCognomeOPE7.Text)
        frmConfigurazione.txtnoteOPE7.Text = updIniFile.GetString("LINEE", "NOTE LINEA07", frmConfigurazione.txtnoteOPE7.Text)
        frmConfigurazione.txtparVNC7.Text = updIniFile.GetString("LINEE", "PARVNC LINEA07", frmConfigurazione.txtparVNC7.Text)
        frmConfigurazione.txtparADSLPcAny7.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA07", frmConfigurazione.txtparADSLPcAny7.Text)
        frmConfigurazione.txtipPCA7.Text = updIniFile.GetString("LINEE", "IPCANY LINEA07", frmConfigurazione.txtipPCA7.Text)
        frmConfigurazione.txtparPCA7.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA07", frmConfigurazione.txtparPCA7.Text)

        frmConfigurazione.txtNomeOPE8.Text = updIniFile.GetString("LINEE", "NOME LINEA08", frmConfigurazione.txtNomeOPE8.Text)
        frmConfigurazione.txtCognomeOPE8.Text = updIniFile.GetString("LINEE", "COGNOME LINEA08", frmConfigurazione.txtCognomeOPE8.Text)
        frmConfigurazione.txtnoteOPE8.Text = updIniFile.GetString("LINEE", "NOTE LINEA08", frmConfigurazione.txtnoteOPE8.Text)
        frmConfigurazione.txtparVNC8.Text = updIniFile.GetString("LINEE", "PARVNC LINEA08", frmConfigurazione.txtparVNC8.Text)
        frmConfigurazione.txtparADSLPcAny8.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA08", frmConfigurazione.txtparADSLPcAny8.Text)
        frmConfigurazione.txtipPCA8.Text = updIniFile.GetString("LINEE", "IPCANY LINEA08", frmConfigurazione.txtipPCA8.Text)
        frmConfigurazione.txtparPCA8.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA08", frmConfigurazione.txtparPCA8.Text)

        frmConfigurazione.txtNomeOPE9.Text = updIniFile.GetString("LINEE", "NOME LINEA09", frmConfigurazione.txtNomeOPE9.Text)
        frmConfigurazione.txtCognomeOPE9.Text = updIniFile.GetString("LINEE", "COGNOME LINEA09", frmConfigurazione.txtCognomeOPE9.Text)
        frmConfigurazione.txtnoteOPE9.Text = updIniFile.GetString("LINEE", "NOTE LINEA09", frmConfigurazione.txtnoteOPE9.Text)
        frmConfigurazione.txtparVNC9.Text = updIniFile.GetString("LINEE", "PARVNC LINEA09", frmConfigurazione.txtparVNC9.Text)
        frmConfigurazione.txtparADSLPcAny9.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA09", frmConfigurazione.txtparADSLPcAny9.Text)
        frmConfigurazione.txtipPCA9.Text = updIniFile.GetString("LINEE", "IPCANY LINEA09", frmConfigurazione.txtipPCA9.Text)
        frmConfigurazione.txtparPCA9.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA09", frmConfigurazione.txtparPCA9.Text)

        frmConfigurazione.txtNomeOPE10.Text = updIniFile.GetString("LINEE", "NOME LINEA10", frmConfigurazione.txtNomeOPE10.Text)
        frmConfigurazione.txtCognomeOPE10.Text = updIniFile.GetString("LINEE", "COGNOME LINEA10", frmConfigurazione.txtCognomeOPE10.Text)
        frmConfigurazione.txtnoteOPE10.Text = updIniFile.GetString("LINEE", "NOTE LINEA10", frmConfigurazione.txtnoteOPE10.Text)
        frmConfigurazione.txtparVNC10.Text = updIniFile.GetString("LINEE", "PARVNC LINEA10", frmConfigurazione.txtparVNC10.Text)
        frmConfigurazione.txtparADSLPcAny10.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA10", frmConfigurazione.txtparADSLPcAny10.Text)
        frmConfigurazione.txtipPCA10.Text = updIniFile.GetString("LINEE", "IPCANY LINEA10", frmConfigurazione.txtipPCA10.Text)
        frmConfigurazione.txtparPCA10.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA10", frmConfigurazione.txtparPCA10.Text)

        frmConfigurazione.txtNomeOPE11.Text = updIniFile.GetString("LINEE", "NOME LINEA11", frmConfigurazione.txtNomeOPE11.Text)
        frmConfigurazione.txtCognomeOPE11.Text = updIniFile.GetString("LINEE", "COGNOME LINEA11", frmConfigurazione.txtCognomeOPE11.Text)
        frmConfigurazione.txtnoteOPE11.Text = updIniFile.GetString("LINEE", "NOTE LINEA11", frmConfigurazione.txtnoteOPE11.Text)
        frmConfigurazione.txtparVNC11.Text = updIniFile.GetString("LINEE", "PARVNC LINEA11", frmConfigurazione.txtparVNC11.Text)
        frmConfigurazione.txtparADSLPcAny11.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA11", frmConfigurazione.txtparADSLPcAny11.Text)
        frmConfigurazione.txtipPCA11.Text = updIniFile.GetString("LINEE", "IPCANY LINEA11", frmConfigurazione.txtipPCA11.Text)
        frmConfigurazione.txtparPCA11.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA11", frmConfigurazione.txtparPCA11.Text)

        frmConfigurazione.txtNomeOPE12.Text = updIniFile.GetString("LINEE", "NOME LINEA12", frmConfigurazione.txtNomeOPE12.Text)
        frmConfigurazione.txtCognomeOPE12.Text = updIniFile.GetString("LINEE", "COGNOME LINEA12", frmConfigurazione.txtCognomeOPE12.Text)
        frmConfigurazione.txtnoteOPE12.Text = updIniFile.GetString("LINEE", "NOTE LINEA12", frmConfigurazione.txtnoteOPE12.Text)
        frmConfigurazione.txtparVNC12.Text = updIniFile.GetString("LINEE", "PARVNC LINEA12", frmConfigurazione.txtparVNC12.Text)
        frmConfigurazione.txtparADSLPcAny12.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA12", frmConfigurazione.txtparADSLPcAny12.Text)
        frmConfigurazione.txtipPCA12.Text = updIniFile.GetString("LINEE", "IPCANY LINEA12", frmConfigurazione.txtipPCA12.Text)
        frmConfigurazione.txtparPCA12.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA12", frmConfigurazione.txtparPCA12.Text)

        frmConfigurazione.txtNomeOPE13.Text = updIniFile.GetString("LINEE", "NOME LINEA13", frmConfigurazione.txtNomeOPE13.Text)
        frmConfigurazione.txtCognomeOPE13.Text = updIniFile.GetString("LINEE", "COGNOME LINEA13", frmConfigurazione.txtCognomeOPE13.Text)
        frmConfigurazione.txtnoteOPE13.Text = updIniFile.GetString("LINEE", "NOTE LINEA13", frmConfigurazione.txtnoteOPE13.Text)
        frmConfigurazione.txtparVNC13.Text = updIniFile.GetString("LINEE", "PARVNC LINEA13", frmConfigurazione.txtparVNC13.Text)
        frmConfigurazione.txtparADSLPcAny13.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA13", frmConfigurazione.txtparADSLPcAny13.Text)
        frmConfigurazione.txtipPCA13.Text = updIniFile.GetString("LINEE", "IPCANY LINEA13", frmConfigurazione.txtipPCA13.Text)
        frmConfigurazione.txtparPCA13.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA13", frmConfigurazione.txtparPCA13.Text)

        frmConfigurazione.txtNomeOPE14.Text = updIniFile.GetString("LINEE", "NOME LINEA14", frmConfigurazione.txtNomeOPE14.Text)
        frmConfigurazione.txtCognomeOPE14.Text = updIniFile.GetString("LINEE", "COGNOME LINEA14", frmConfigurazione.txtCognomeOPE14.Text)
        frmConfigurazione.txtnoteOPE14.Text = updIniFile.GetString("LINEE", "NOTE LINEA14", frmConfigurazione.txtnoteOPE14.Text)
        frmConfigurazione.txtparVNC14.Text = updIniFile.GetString("LINEE", "PARVNC LINEA14", frmConfigurazione.txtparVNC14.Text)
        frmConfigurazione.txtparADSLPcAny14.Text = updIniFile.GetString("LINEE", "PARPCANY LINEA14", frmConfigurazione.txtparADSLPcAny14.Text)
        frmConfigurazione.txtipPCA14.Text = updIniFile.GetString("LINEE", "IPCANY LINEA14", frmConfigurazione.txtipPCA14.Text)
        frmConfigurazione.txtparPCA14.Text = updIniFile.GetString("LINEE", "PARMODEM LINEA14", frmConfigurazione.txtparPCA14.Text)

        'Recupero Settaggi Sezione Telecontrollo INI
        frmConfigurazione.chkChiamateTeleControllo.Checked = updIniFile.GetString("TELECONTROLLO", "CHIAMATE", frmConfigurazione.chkChiamateTeleControllo.Checked)
        frmConfigurazione.chkspegniTeleControllo.Checked = updIniFile.GetString("TELECONTROLLO", "SPEGNI-RIAVVIA", frmConfigurazione.chkspegniTeleControllo.Checked)
        frmConfigurazione.chkscript.Checked = updIniFile.GetString("TELECONTROLLO", "SCRIPT-MESSAGGI", frmConfigurazione.chkscript.Checked)
        frmConfigurazione.chkFTPPassive.Checked = updIniFile.GetString("TELECONTROLLO", "FTPPASSIVE", frmConfigurazione.chkFTPPassive.Checked)
        frmConfigurazione.txtSitoTeleControllo.Text = updIniFile.GetString("TELECONTROLLO", "URLSITO", frmConfigurazione.txtSitoTeleControllo.Text)
        frmConfigurazione.txtPathTeleControllo.Text = updIniFile.GetString("TELECONTROLLO", "PATHSITO", frmConfigurazione.txtPathTeleControllo.Text)
        frmConfigurazione.chkGeneraPathTeleControllo.Checked = updIniFile.GetString("TELECONTROLLO", "GENERA_PATH", frmConfigurazione.chkGeneraPathTeleControllo.Checked)
        frmConfigurazione.txtUserTeleControllo.Text = updIniFile.GetString("TELECONTROLLO", "USERNAME", frmConfigurazione.txtUserTeleControllo.Text)
        If updIniFile.GetString("TELECONTROLLO", "PASSWORD", String.Empty) <> String.Empty Then
            Dim decriptata As String = Krypto.TripleDESDecode(updIniFile.GetString("TELECONTROLLO", "PASSWORD", String.Empty), key)
            frmConfigurazione.txtPassTeleControllo.Text = decriptata
        End If
        frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex = updIniFile.GetInteger("TELECONTROLLO", "INTERVALLO", frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex)

        'Recupero Settaggi Sezione Transfer File
        frmConfigurazione.chkAbilitaUploadFile.Checked = updIniFile.GetString("TRANSFERFILE", "ABILITATO", frmConfigurazione.chkAbilitaUploadFile.Checked)
        frmConfigurazione.chkUPugualeTele.Checked = updIniFile.GetString("TRANSFERFILE", "UGUALE_TELECONTROLLO", frmConfigurazione.chkUPugualeTele.Checked)
        frmConfigurazione.chkFTPPassTrans.Checked = updIniFile.GetString("TRANSFERFILE", "FTPPASSIVE", frmConfigurazione.chkFTPPassTrans.Checked)
        frmConfigurazione.txtServerUP.Text = updIniFile.GetString("TRANSFERFILE", "SERVER_UPLOAD", frmConfigurazione.txtServerUP.Text)
        frmConfigurazione.txtpathServerUp.Text = updIniFile.GetString("TRANSFERFILE", "PATH_UPLOAD", frmConfigurazione.txtpathServerUp.Text)
        frmConfigurazione.txtUserNameServerUp.Text = updIniFile.GetString("TRANSFERFILE", "USER_UPLOAD", frmConfigurazione.txtUserNameServerUp.Text)
        If updIniFile.GetString("TRANSFERFILE", "PASS_UPLOAD", String.Empty) <> String.Empty Then
            Dim DecryTrans As String = Krypto.TripleDESDecode(updIniFile.GetString("TRANSFERFILE", "PASS_UPLOAD", String.Empty), key)
            frmConfigurazione.txtPasswordServerUp.Text = DecryTrans
        End If
        frmConfigurazione.txtServerWeb.Text = updIniFile.GetString("TRANSFERFILE", "INDIRIZZOHTTP", frmConfigurazione.txtServerWeb.Text)
        frmConfigurazione.chkAbilitaDownloadFile.Checked = updIniFile.GetString("TRANSFERFILE", "DOWNLOAD", frmConfigurazione.chkAbilitaDownloadFile.Checked) 'Sottosezione Download
        frmConfigurazione.chkdezip.Checked = updIniFile.GetString("TRANSFERFILE", "DEZIP", frmConfigurazione.chkdezip.Checked)
        frmConfigurazione.chklistdownweb.Checked = updIniFile.GetString("TRANSFERFILE", "FTPLISTWEB", frmConfigurazione.chklistdownweb.Checked)
        frmConfigurazione.cmbdownloadconn.SelectedIndex = updIniFile.GetInteger("TRANSFERFILE", "TIPODOWNLOAD", 0) - 1

        'Recupero Settaggi Sezione Project
        frmConfigurazione.chkAbilitaProjector.Checked = updIniFile.GetString("PROJECTOR", "ABILITATO", frmConfigurazione.chkAbilitaProjector.Checked)
        frmConfigurazione.chkprojFullScreen.Checked = updIniFile.GetString("PROJECTOR", "FULLSCREEN", frmConfigurazione.chkprojFullScreen.Checked)
        frmConfigurazione.chkonlyprojector.Checked = updIniFile.GetString("PROJECTOR", "SOLOPROJECTOR", frmConfigurazione.chkonlyprojector.Checked)
        frmConfigurazione.txtNomeStanza1.Text = updIniFile.GetString("PROJECTOR", "NOMESTANZA1", frmConfigurazione.txtNomeStanza1.Text)
        frmConfigurazione.txtNomeStanza2.Text = updIniFile.GetString("PROJECTOR", "NOMESTANZA2", frmConfigurazione.txtNomeStanza2.Text)
        frmConfigurazione.txtProjhost1.Text = updIniFile.GetString("PROJECTOR", "HOST1", frmConfigurazione.txtProjhost1.Text)
        frmConfigurazione.txtProjhost2.Text = updIniFile.GetString("PROJECTOR", "HOST2", frmConfigurazione.txtProjhost2.Text)
        frmConfigurazione.txtportaproj1.Text = Convert.ToString(updIniFile.GetString("PROJECTOR", "PORTA1", frmConfigurazione.txtportaproj1.Text))
        frmConfigurazione.txtportaproj2.Text = Convert.ToString(updIniFile.GetString("PROJECTOR", "PORTA2", frmConfigurazione.txtportaproj2.Text))

        'Recupero Settaggi Sezione eMAil
        frmConfigurazione.vtxtemailaz.Text = updIniFile.GetString("EMAIL", "ASSISTENTE", frmConfigurazione.vtxtemailaz.Text)
        frmConfigurazione.chkeMailtecnici.Checked = updIniFile.GetString("EMAIL", "ABILITATECNICI", frmConfigurazione.chkeMailtecnici.Checked)
        frmConfigurazione.chkinfoPC.Checked = updIniFile.GetString("EMAIL", "INFOPC", frmConfigurazione.chkinfoPC.Checked)
        frmConfigurazione.txtnomemittente.Text = updIniFile.GetString("EMAIL", "NOMEMITTENTE", frmConfigurazione.txtnomemittente.Text)
        frmConfigurazione.vtxtmailmittente.Text = updIniFile.GetString("EMAIL", "MITTENTE", frmConfigurazione.vtxtmailmittente.Text)
        frmConfigurazione.txtServerSMTP.Text = updIniFile.GetString("EMAIL", "SERVERSMTP", frmConfigurazione.txtServerSMTP.Text)
        frmConfigurazione.txtportaSMTP.Text = updIniFile.GetString("EMAIL", "PORTASMTP", frmConfigurazione.txtportaSMTP.Text)
        frmConfigurazione.chkSSL.Checked = updIniFile.GetString("EMAIL", "SSL", frmConfigurazione.chkSSL.Checked)
        frmConfigurazione.txtuserSMTP.Text = updIniFile.GetString("EMAIL", "USERNAME", frmConfigurazione.txtuserSMTP.Text)
        If updIniFile.GetString("EMAIL", "PASSWORD", String.Empty) <> String.Empty Then
            Dim DecryTrans2 As String = Krypto.TripleDESDecode(updIniFile.GetString("EMAIL", "PASSWORD", String.Empty), key)
            frmConfigurazione.txtpassSMTP.Text = DecryTrans2
        End If


        'Recupero Settaggi Sezione Report INI
        frmConfigurazione.chkabilitaReport.Checked = updIniFile.GetString("REPORT", "ABILITATO", frmConfigurazione.chkabilitaReport.Checked)
        frmConfigurazione.chkCronoSessioni.Checked = updIniFile.GetString("REPORT", "CRONOMETRA", frmConfigurazione.chkCronoSessioni.Checked)
        frmConfigurazione.chkoperatoreReport.Checked = updIniFile.GetString("REPORT", "OPERATORE", frmConfigurazione.chkoperatoreReport.Checked)
        frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked = updIniFile.GetString("REPORT", "NOTIFICHE", frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked)
        frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text = updIniFile.GetString("REPORT", "MESSAGGIO INTERRUZZIONI", frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text)
        frmConfigurazione.chkChiudiInterruzzioneassi.Checked = updIniFile.GetString("REPORT", "CHIUDI INTERRUZZIONE", frmConfigurazione.chkChiudiInterruzzioneassi.Checked)
        frmConfigurazione.chkabilitaLog.Checked = updIniFile.GetString("REPORT", "ABILITA LOG", frmConfigurazione.chkabilitaLog.Checked)
        frmConfigurazione.chkabilitaStampa.Checked = updIniFile.GetString("REPORT", "STAMPA", frmConfigurazione.chkabilitaStampa.Checked)
    End Sub
End Module
