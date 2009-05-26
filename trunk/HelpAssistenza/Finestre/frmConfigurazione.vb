Public Class frmConfigurazione
    Dim ProgOpenDialog As Short
    Dim ChiudoeSalvo As Boolean = False
    Sub adslcontrol()
        If chkvnc.Checked = True Then
            grpadsl.Enabled = True
        Else
            grpadsl.Enabled = False
        End If
        If Me.cmbprogadsl.Text = "VNC" Then
            txtpercorsoadsl.Visible = True
            txtpercorsoadsl2.Visible = False
            Me.chkNascondiVNCIcon.Visible = True
            Me.chkreconn.Visible = True
            Me.chkSettaPortePCA.Visible = False
        ElseIf Me.cmbprogadsl.Text = "PcAnyWhere" Then
            txtpercorsoadsl2.Visible = True
            txtpercorsoadsl.Visible = False
            Me.chkNascondiVNCIcon.Visible = False
            Me.chkreconn.Visible = False
            Me.chkSettaPortePCA.Visible = True
        End If
    End Sub
    Sub modemcontrol()
        If chkmodem.Checked = True Then
            grpModem.Enabled = True
        Else
            grpModem.Enabled = False
        End If
    End Sub
    Sub popolacombonumlinee()
        Dim numline(14) As String
        numline(0) = "0"
        numline(1) = "1"
        numline(2) = "2"
        numline(3) = "3"
        numline(4) = "4"
        numline(5) = "5"
        numline(6) = "6"
        numline(7) = "7"
        numline(8) = "8"
        numline(9) = "9"
        numline(10) = "10"
        numline(11) = "11"
        numline(12) = "12"
        numline(13) = "13"
        numline(14) = "14"
        cmbnumlinee.Items.Clear()
        cmbnumlinee.Items.AddRange(numline)
    End Sub
    Sub linea()
        If Me.cmbnumlinee.Text <> String.Empty Then
            Me.cmblinea.Items.Clear()
            For i As Integer = 1 To Me.cmbnumlinee.Text
                Me.cmblinea.Items.Add(i)
            Next
            Me.cmblinea.Text = "1"
        End If
        If Me.cmblinea.Text <> String.Empty Then
            Me.cmblinea.Enabled = True
        End If
        If Me.cmbnumlinee.Text = "0" Then
            Me.grpconfline.Enabled = False
            Me.lblnomeOPE.Visible = False
            Me.lblcognomeOPE.Visible = False
            Me.lblNoteOPE.Visible = False
            Me.lblparVNC.Visible = False
            Me.lblparADSLPCAny.Visible = False
            Me.lblparPCA.Visible = False
            Me.txtnomeOPE1.Visible = False
            Me.txtCognomeOPE1.Visible = False
            Me.txtnoteOPE1.Visible = False
            Me.txtparVNC1.Visible = False
            Me.txtparPCA1.Visible = False
            Me.txtparADSLPcAny1.Visible = False
            Me.txtipPCA1.Visible = False
            Me.txtNomeOPE2.Visible = False
            Me.txtCognomeOPE2.Visible = False
            Me.txtnoteOPE2.Visible = False
            Me.txtparVNC2.Visible = False
            Me.txtparPCA2.Visible = False
            Me.txtparADSLPcAny2.Visible = False
            Me.txtipPCA2.Visible = False
            Me.txtNomeOPE3.Visible = False
            Me.txtCognomeOPE3.Visible = False
            Me.txtNoteOPE3.Visible = False
            Me.txtparVNC3.Visible = False
            Me.txtparPCA3.Visible = False
            Me.txtparADSLPcAny3.Visible = False
            Me.txtipPCA3.Visible = False
            Me.txtNomeOPE4.Visible = False
            Me.txtCognomeOPE4.Visible = False
            Me.txtNoteOPE4.Visible = False
            Me.txtparVNC4.Visible = False
            Me.txtparPCA4.Visible = False
            Me.txtparADSLPcAny4.Visible = False
            Me.txtipPCA4.Visible = False
            Me.txtNomeOPE5.Visible = False
            Me.txtCognomeOPE5.Visible = False
            Me.txtNoteOPE5.Visible = False
            Me.txtparVNC5.Visible = False
            Me.txtparPCA5.Visible = False
            Me.txtparADSLPcAny5.Visible = False
            Me.txtipPCA5.Visible = False
            Me.txtNomeOPE6.Visible = False
            Me.txtCognomeOPE6.Visible = False
            Me.txtNoteOPE6.Visible = False
            Me.txtparVNC6.Visible = False
            Me.txtparPCA6.Visible = False
            Me.txtparADSLPcAny6.Visible = False
            Me.txtipPCA6.Visible = False
            Me.txtNomeOPE7.Visible = False
            Me.txtCognomeOPE7.Visible = False
            Me.txtNoteOPE7.Visible = False
            Me.txtparVNC7.Visible = False
            Me.txtparPCA7.Visible = False
            Me.txtparADSLPcAny7.Visible = False
            Me.txtipPCA7.Visible = False
            Me.txtNomeOPE8.Visible = False
            Me.txtCognomeOPE8.Visible = False
            Me.txtNoteOPE8.Visible = False
            Me.txtparVNC8.Visible = False
            Me.txtparPCA8.Visible = False
            Me.txtparADSLPcAny8.Visible = False
            Me.txtipPCA8.Visible = False
            Me.txtNomeOPE9.Visible = False
            Me.txtCognomeOPE9.Visible = False
            Me.txtNoteOPE9.Visible = False
            Me.txtparVNC9.Visible = False
            Me.txtparPCA9.Visible = False
            Me.txtparADSLPcAny9.Visible = False
            Me.txtipPCA9.Visible = False
            Me.txtNomeOPE10.Visible = False
            Me.txtCognomeOPE10.Visible = False
            Me.txtNoteOPE10.Visible = False
            Me.txtparVNC10.Visible = False
            Me.txtparPCA10.Visible = False
            Me.txtparADSLPcAny10.Visible = False
            Me.txtipPCA10.Visible = False
            Me.txtNomeOPE11.Visible = False
            Me.txtCognomeOPE11.Visible = False
            Me.txtNoteOPE11.Visible = False
            Me.txtparVNC11.Visible = False
            Me.txtparPCA11.Visible = False
            Me.txtparADSLPcAny11.Visible = False
            Me.txtipPCA11.Visible = False
            Me.txtNomeOPE12.Visible = False
            Me.txtCognomeOPE12.Visible = False
            Me.txtNoteOPE12.Visible = False
            Me.txtparVNC12.Visible = False
            Me.txtparPCA12.Visible = False
            Me.txtparADSLPcAny12.Visible = False
            Me.txtipPCA12.Visible = False
            Me.txtNomeOPE13.Visible = False
            Me.txtCognomeOPE13.Visible = False
            Me.txtNoteOPE13.Visible = False
            Me.txtparVNC13.Visible = False
            Me.txtparPCA13.Visible = False
            Me.txtparADSLPcAny13.Visible = False
            Me.txtipPCA13.Visible = False
            Me.txtNomeOPE14.Visible = False
            Me.txtCognomeOPE14.Visible = False
            Me.txtNoteOPE14.Visible = False
            Me.txtparVNC14.Visible = False
            Me.txtparPCA14.Visible = False
            Me.txtparADSLPcAny14.Visible = False
            Me.txtipPCA14.Visible = False
        End If
    End Sub
    Sub ControllaAutoStart()
        Dim Value As String = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
        Dim Value2 As String = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
        If Value = "NonEsiste" And Value2 = "NonEsiste" Then
            Me.lblStatoAutoStart.ForeColor = System.Drawing.Color.Black
            Me.lblStatoAutoStart.Text = "Autostart HelpAssistenza NON Attivato"
        ElseIf Value.Contains("HelpAssistenza") Then
            Me.lblStatoAutoStart.ForeColor = System.Drawing.Color.OrangeRed
            Me.lblStatoAutoStart.Text = "Autostart HelpAssistenza Attivato Per Utente"
        ElseIf Value2.Contains("HelpAssistenza") Then
            Me.lblStatoAutoStart.ForeColor = System.Drawing.Color.Red
            Me.lblStatoAutoStart.Text = "Autostart HelpAssistenza Attivato Per Tutti"
        End If
    End Sub
    Sub Abilita_TransferFile()
        If chkAbilitaUploadFile.Checked = True Or chkAbilitaDownloadFile.Checked = True Then
            Me.pnlServerUpload.Enabled = True
            Me.txtServerUP.Enabled = True
            Me.txtpathServerUp.Enabled = True
            Me.txtPasswordServerUp.Enabled = True
            Me.txtUserNameServerUp.Enabled = True
            Me.lblServerUp.Enabled = True
            Me.lblpathServerUp.Enabled = True
            Me.lblUserNameUp.Enabled = True
            Me.lblPasswordServerUp.Enabled = True
        Else
            Me.pnlServerUpload.Enabled = False
            Me.txtServerUP.Enabled = False
            Me.txtpathServerUp.Enabled = False
            Me.txtPasswordServerUp.Enabled = False
            Me.txtUserNameServerUp.Enabled = False
            Me.lblServerUp.Enabled = False
            Me.lblpathServerUp.Enabled = False
            Me.lblUserNameUp.Enabled = False
            Me.lblPasswordServerUp.Enabled = False
        End If
    End Sub

    Sub chkaltri()
        If chkother.Checked = True Then
            txtother.Enabled = True
            lblicocollalt2.Enabled = True
            lblicocollalt2.Visible = True
        Else
            txtother.Enabled = False
            lblicocollalt2.Enabled = False
            lblicocollalt2.Visible = False
        End If
        If chkteamviewer.Checked = True Then
            txtteamviewer.Enabled = True
        Else
            txtteamviewer.Enabled = False
        End If
        If chkUPugualeTele.Checked = True Then cmbTipoUpload.SelectedIndex = 0
        If Me.chkUPugualeTele.Checked = True Then
            Me.pnlServerUpload.Enabled = False
        Else
            Me.pnlServerUpload.Enabled = True
        End If
        If chkteamviewer.Checked = False And chkother.Checked = False Then
            frmAssistenza.Grpaltriprog.Visible = False
        End If
    End Sub

    Private Sub chkvnc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkvnc.CheckedChanged
        adslcontrol()
    End Sub

    Private Sub chkmodem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmodem.CheckedChanged
        modemcontrol()
    End Sub

    Private Sub frmConfigurazione_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing And ChiudoeSalvo = False Then
            If MessageBox.Show("Eventuali Modifiche non Salvate andranno perse !" + vbCrLf + "Vuoi Veramente Uscire dalla Configurazione ?", "HelpAssistenza - Configurazione", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.No Then e.Cancel = True
        End If
    End Sub

    Private Sub frmConfigurazione_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmConfigurazione_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        adslcontrol()
        modemcontrol()
        chkaltri()
        popolacombonumlinee()
        linea()
        If cmbnumlinee.Text = String.Empty Then cmbnumlinee.SelectedIndex = 0
        ChiudoeSalvo = False
        Configlinea()
        If cmbnumlinee.Text = "0" Then Me.grpconfline.Enabled = False
    End Sub

    Private Sub chkteamviewer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkteamviewer.CheckedChanged
        chkaltri()
    End Sub

    Private Sub chkother_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkother.CheckedChanged
        chkaltri()
        If chkother.Checked = True Then
            lblnomealtro.Visible = True
            txtnmealtro.Visible = True
        Else
            lblnomealtro.Visible = False
            txtnmealtro.Visible = False
        End If
    End Sub

    Private Sub cmbnumlinee_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbnumlinee.KeyPress
        Dim oldvalue As String = cmbnumlinee.Text
        Try
            If Char.IsLetter(e.KeyChar) Then
                e.Handled = True
                Exit Sub
            End If
            If Int32.Parse(cmbnumlinee.Text & e.KeyChar.ToString) > 14 Then
                e.Handled = True
            End If
        Catch ex As Exception
            e.Handled = False
            cmbnumlinee.Text = oldvalue
        End Try
    End Sub

    Private Sub cmbnumlinee_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbnumlinee.SelectedIndexChanged
        linea()
        Me.cmblinea.Enabled = True
    End Sub
    Private Sub ControllaPath()
        Dim pathSito1 As String = Me.txtSitoTeleControllo.Text
        Dim pathSito2 As String = Me.txtServerUP.Text
        If pathSito1 <> String.Empty And pathSito1.EndsWith("/") Then
            pathSito1 = pathSito1.Remove(pathSito1.Length - 1)
            Me.txtSitoTeleControllo.Text = pathSito1
        End If
        If pathSito2 <> String.Empty And pathSito2.EndsWith("/") Then
            pathSito2 = pathSito2.Remove(pathSito2.Length - 1)
            Me.txtpathServerUp.Text = pathSito2
        End If
    End Sub
    Private Sub btnSalvaConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalvaConfig.Click
        ControllaPath()
        If Not (txtpathServerUp.Text.StartsWith("/") And txtpathServerUp.Text.EndsWith("/")) And Me.txtpathServerUp.Text <> String.Empty Then
            If MessageBox.Show("Formato campo Path Errato!" + vbCrLf + "Controllare il Formato dei Campi Path !" + vbCrLf + "Vuoi Salvare Ugualmente ?", "HelpAssistenza - Errore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then Exit Sub
        End If
        If Not (txtPathTeleControllo.Text.StartsWith("/") And txtPathTeleControllo.Text.EndsWith("/")) And Me.txtPathTeleControllo.Text <> String.Empty Then
            If MessageBox.Show("Formato campo Path Errato!" + vbCrLf + "Controllare il Formato dei Campi Path !" + vbCrLf + "Vuoi Salvare Ugualmente ?", "HelpAssistenza - Errore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then Exit Sub
        End If
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
        ChiudoeSalvo = True
        Me.Close()
    End Sub

    Private Sub txtlogo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtlogo.DoubleClick
        OpenFileLogo.ShowDialog()
    End Sub

    Private Sub OpenFileLogo_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileLogo.FileOk
        Me.txtlogo.Text = OpenFileLogo.FileName
    End Sub

    Private Sub cmblinea_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmblinea.KeyPress
        If e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = "10" Or e.KeyChar = "11" Or e.KeyChar = "12" Or e.KeyChar = "13" Or e.KeyChar = "14" Then
            e.Handled = False
        Else
            e.Handled = True
        End If
        cmblinea.SelectAll()
    End Sub

    Private Sub cmblinea_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblinea.SelectedIndexChanged
        Configlinea()
    End Sub

    Private Sub RadioAutoStartUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioAutoStartUser.CheckedChanged
        Dim PercorsoApp As String = Application.ExecutablePath & " -listen"
        If RadioAutoStartUser.Checked = True Then
            Dim registro As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            Dim Value As String = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
            Dim registro2 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            If Value.Contains("HelpAssistenza") Then
                registro2.DeleteValue("HelpAssistenza")
            End If
            registro.SetValue("HelpAssistenza", PercorsoApp)
        End If
        ControllaAutoStart()
    End Sub

    Private Sub RadioNessunAutostart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioNessunAutostart.CheckedChanged
        If RadioNessunAutostart.Checked = True Then
            Dim registro1 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            Dim registro2 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
            Dim Value As String = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
            Dim Value2 As String = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
            If Value = "NonEsiste" And Value2 = "NonEsiste" Then
                MsgBox("Autostart HelpAssistenza non attivo!", MsgBoxStyle.Exclamation, Title:="HelpAssistenza")
            ElseIf Value.Contains("HelpAssistenza") Then
                registro1.DeleteValue("HelpAssistenza")
            ElseIf Value2.Contains("HelpAssistenza") Then
                registro2.DeleteValue("HelpAssistenza")
            End If
        End If
        ControllaAutoStart()
    End Sub

    Private Sub RadioAutoStartGlobale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioAutoStartGlobale.CheckedChanged
        Dim PercorsoApp As String = Application.ExecutablePath & " -listen"
        Dim registro As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        Dim registro2 As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        If RadioAutoStartGlobale.Checked = True Then
            Dim Value As String = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "HelpAssistenza", "NonEsiste")
            If Value.Contains("HelpAssistenza") Then
                registro2.DeleteValue("HelpAssistenza")
            End If
            registro.SetValue("HelpAssistenza", PercorsoApp)
        End If
        ControllaAutoStart()
    End Sub

    Private Sub TabConfig_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabConfig.SelectedIndexChanged
        ControllaAutoStart()
        ControllaConfigReport()
        Abilita_TransferFile()
        ProjectorCheck()
        If Me.txtemailazienda.Text <> String.Empty AndAlso Me.vtxtemailaz.Text = String.Empty Then Me.vtxtemailaz.Text = Me.txtemailazienda.Text
    End Sub

    Private Sub cmbprogadsl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbprogadsl.SelectedIndexChanged
        If Me.cmbprogadsl.SelectedIndex = 0 Then
            Me.txtpercorsoadsl.Visible = True
            Me.txtpercorsoadsl2.Visible = False
            Me.chkNascondiVNCIcon.Visible = True
            Me.chkreconn.Visible = True
            Me.chkSettaPortePCA.Visible = False
        ElseIf Me.cmbprogadsl.SelectedIndex = 1 Then
            Me.txtpercorsoadsl.Visible = False
            Me.txtpercorsoadsl2.Visible = True
            Me.chkNascondiVNCIcon.Visible = False
            Me.chkreconn.Visible = False
            Me.chkSettaPortePCA.Visible = True
        End If
    End Sub

    Private Sub chkabilitaReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkabilitaReport.CheckedChanged
        ControllaConfigReport()
    End Sub

    Private Sub chkNotificheInterruzzioniTeleAssistenza_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotificheInterruzzioniTeleAssistenza.CheckedChanged
        If Me.chkNotificheInterruzzioniTeleAssistenza.Checked = True Then
            Me.lblMessaggioInterruzione.Visible = True
            Me.txtmessaggioInteruzzioneTeleAssistenza.Visible = True
        Else
            Me.lblMessaggioInterruzione.Visible = False
            Me.txtmessaggioInteruzzioneTeleAssistenza.Visible = False
        End If
        chkChiudiInterruzzioneassi.Checked = False
    End Sub


    Private Sub chkNascondiVNCIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNascondiVNCIcon.CheckedChanged
        Dim IconaVNCTray As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\ORL\WinVNC3", True)
        Dim PercorsoINIVNC As String = Me.txtpercorsoadsl.Text
        If PercorsoINIVNC.Contains("%PROGRAMPATH%") Then PercorsoINIVNC = Replace(PercorsoINIVNC, "%PROGRAMPATH%", Application.StartupPath)
        PercorsoINIVNC = Replace(PercorsoINIVNC, "winvnc.exe", "ultravnc.ini")

        Try
            If chkNascondiVNCIcon.Checked = True Then
                IconaVNCTray.SetValue("DisableTrayIcon", 1)
            Else
                IconaVNCTray.SetValue("DisableTrayIcon", 0)
            End If
        Catch
            Dim INIUltraVNC As New INIFiles(PercorsoINIVNC)
            If chkNascondiVNCIcon.Checked = True Then
                INIUltraVNC.WriteInteger("admin", "DisableTrayIcon", 1)
            Else
                INIUltraVNC.WriteInteger("admin", "DisableTrayIcon", 0)
            End If
        End Try

    End Sub

    Private Sub chkChiudiInterruzzioneassi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkChiudiInterruzzioneassi.CheckedChanged
        chkNotificheInterruzzioniTeleAssistenza.Checked = False
    End Sub

    Private Sub txtpercorsoadsl2_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtpercorsoadsl2.MouseDoubleClick
        ProgOpenDialog = 2
        OpenFileProg.ShowDialog()
    End Sub


    Private Sub txtpercorsoadsl_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtpercorsoadsl.MouseDoubleClick
        ProgOpenDialog = 1
        OpenFileProg.ShowDialog()
    End Sub

    Private Sub OpenFileProg_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileProg.FileOk

        Select Case ProgOpenDialog
            Case 1
                txtpercorsoadsl.Text = OpenFileProg.FileName
            Case 2
                txtpercorsoadsl2.Text = OpenFileProg.FileName
            Case 3
                txtpercorsomodem.Text = OpenFileProg.FileName
            Case 4
                txtteamviewer.Text = OpenFileProg.FileName
            Case 5
                txtother.Text = OpenFileProg.FileName
        End Select

    End Sub

    Private Sub txtpercorsomodem_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtpercorsomodem.MouseDoubleClick
        ProgOpenDialog = 3
        OpenFileProg.ShowDialog()
    End Sub

    Private Sub txtteamviewer_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtteamviewer.MouseDoubleClick
        ProgOpenDialog = 4
        OpenFileProg.ShowDialog()
    End Sub

    Private Sub txtother_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtother.MouseDoubleClick
        ProgOpenDialog = 5
        OpenFileProg.ShowDialog()
    End Sub

    Private Sub chkUPugualeTele_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUPugualeTele.CheckedChanged
        Abilita_TransferFile()
    End Sub

    Private Sub chkGeneraPathTeleControllo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneraPathTeleControllo.CheckedChanged
        If Me.chkGeneraPathTeleControllo.Checked = True Then
            Me.txtPathTeleControllo.Enabled = False
            Dim Toltipavgene As New ToolTip
            Toltipavgene.ToolTipTitle = "HelpAssistenza - Genera Path TeleControllo"
            Toltipavgene.ToolTipIcon = ToolTipIcon.Warning
            Toltipavgene.UseFading = True
            Toltipavgene.IsBalloon = True
            Toltipavgene.Show("Per Completare la Generazione Path è Necessario riavviare HelpAssistenza !", Me.txtPathTeleControllo, 5000)
        Else
            Me.txtPathTeleControllo.Enabled = True
        End If
    End Sub

    Private Sub txtPathTeleControllo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPathTeleControllo.TextChanged
        If (txtPathTeleControllo.Text.StartsWith("/") And txtPathTeleControllo.Text.EndsWith("/")) Then
            Me.btnSalvaConfig.Enabled = True
            ToolTipTeleControllo.IsBalloon = False
            ToolTipTeleControllo.ToolTipIcon = ToolTipIcon.Info
            ToolTipTeleControllo.Hide(Me.txtPathTeleControllo)
        End If
    End Sub

    Private Sub txtPathTeleControllo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPathTeleControllo.Validating
        If Not (txtPathTeleControllo.Text.StartsWith("/") And txtPathTeleControllo.Text.EndsWith("/")) And Me.txtPathTeleControllo.Text <> String.Empty Then
            e.Cancel = True
            Me.btnSalvaConfig.Enabled = False
            ToolTipTeleControllo.IsBalloon = True
            ToolTipTeleControllo.ToolTipIcon = ToolTipIcon.Warning
            ToolTipTeleControllo.Show("La Path deve iniziare con '/' e finire con '/' - es : /prova/", Me.txtPathTeleControllo)
        Else
            e.Cancel = False
            Me.btnSalvaConfig.Enabled = True
            ToolTipTeleControllo.IsBalloon = False
            ToolTipTeleControllo.ToolTipIcon = ToolTipIcon.Info
            ToolTipTeleControllo.Hide(Me.txtPathTeleControllo)
        End If
    End Sub

    Private Sub txtpathServerUp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpathServerUp.TextChanged
        If (txtpathServerUp.Text.StartsWith("/") And txtpathServerUp.Text.EndsWith("/")) Then
            Me.btnSalvaConfig.Enabled = True
            ToolTipTransfer.IsBalloon = False
            ToolTipTransfer.ToolTipIcon = ToolTipIcon.Info
            ToolTipTransfer.Hide(Me.txtpathServerUp)
        End If
    End Sub

    Private Sub txtpathServerUp_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpathServerUp.Validating
        If Not (txtpathServerUp.Text.StartsWith("/") And txtpathServerUp.Text.EndsWith("/")) And Me.txtpathServerUp.Text <> String.Empty And Me.chkAbilitaUploadFile.Checked = False Then
            e.Cancel = True
            Me.btnSalvaConfig.Enabled = False
            ToolTipTransfer.IsBalloon = True
            ToolTipTransfer.ToolTipIcon = ToolTipIcon.Warning
            ToolTipTransfer.Show("La Path deve iniziare con '/' e finire con '/' - es : /prova/", Me.txtpathServerUp)
        Else
            e.Cancel = False
            Me.btnSalvaConfig.Enabled = True
            ToolTipTransfer.IsBalloon = False
            ToolTipTransfer.ToolTipIcon = ToolTipIcon.Info
            ToolTipTransfer.Hide(Me.txtpathServerUp)
        End If
    End Sub

    Private Sub chkAbilitaUploadFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAbilitaUploadFile.CheckedChanged
        Abilita_TransferFile()
    End Sub

    Private Sub cmbnumlinee_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbnumlinee.TextChanged
        linea()
    End Sub

    Private Sub btnCancellaLinee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancellaLinee.Click
        If MessageBox.Show("Attenzione !!! " + vbCrLf + "Tutte le Linee di Collegamento verranno Cancellate !!!" + vbCrLf + "Vuoi procedere ?", "HelpAssistenza", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Me.cmbnumlinee.SelectedIndex = 0
            Me.txtnomeOPE1.Text = String.Empty
            Me.txtCognomeOPE1.Text = String.Empty
            Me.txtnoteOPE1.Text = String.Empty
            Me.txtparVNC1.Text = String.Empty
            Me.txtparPCA1.Text = String.Empty
            Me.txtparADSLPcAny1.Text = String.Empty
            Me.txtipPCA1.Text = String.Empty
            Me.txtNomeOPE2.Text = String.Empty
            Me.txtCognomeOPE2.Text = String.Empty
            Me.txtnoteOPE2.Text = String.Empty
            Me.txtparVNC2.Text = String.Empty
            Me.txtparPCA2.Text = String.Empty
            Me.txtparADSLPcAny2.Text = String.Empty
            Me.txtipPCA2.Text = String.Empty
            Me.txtNomeOPE3.Text = String.Empty
            Me.txtCognomeOPE3.Text = String.Empty
            Me.txtNoteOPE3.Text = String.Empty
            Me.txtparVNC3.Text = String.Empty
            Me.txtparPCA3.Text = String.Empty
            Me.txtparADSLPcAny3.Text = String.Empty
            Me.txtipPCA3.Text = String.Empty
            Me.txtNomeOPE4.Text = String.Empty
            Me.txtCognomeOPE4.Text = String.Empty
            Me.txtNoteOPE4.Text = String.Empty
            Me.txtparVNC4.Text = String.Empty
            Me.txtparPCA4.Text = String.Empty
            Me.txtparADSLPcAny4.Text = String.Empty
            Me.txtipPCA4.Text = String.Empty
            Me.txtNomeOPE5.Text = String.Empty
            Me.txtCognomeOPE5.Text = String.Empty
            Me.txtNoteOPE5.Text = String.Empty
            Me.txtparVNC5.Text = String.Empty
            Me.txtparPCA5.Text = String.Empty
            Me.txtparADSLPcAny5.Text = String.Empty
            Me.txtipPCA5.Text = String.Empty
            Me.txtNomeOPE6.Text = String.Empty
            Me.txtCognomeOPE6.Text = String.Empty
            Me.txtNoteOPE6.Text = String.Empty
            Me.txtparVNC6.Text = String.Empty
            Me.txtparPCA6.Text = String.Empty
            Me.txtparADSLPcAny6.Text = String.Empty
            Me.txtipPCA6.Text = String.Empty
            Me.txtNomeOPE7.Text = String.Empty
            Me.txtCognomeOPE7.Text = String.Empty
            Me.txtNoteOPE7.Text = String.Empty
            Me.txtparVNC7.Text = String.Empty
            Me.txtparPCA7.Text = String.Empty
            Me.txtparADSLPcAny7.Text = String.Empty
            Me.txtipPCA7.Text = String.Empty
            Me.txtNomeOPE8.Text = String.Empty
            Me.txtCognomeOPE8.Text = String.Empty
            Me.txtNoteOPE8.Text = String.Empty
            Me.txtparVNC8.Text = String.Empty
            Me.txtparPCA8.Text = String.Empty
            Me.txtparADSLPcAny8.Text = String.Empty
            Me.txtipPCA8.Text = String.Empty
            Me.txtNomeOPE9.Text = String.Empty
            Me.txtCognomeOPE9.Text = String.Empty
            Me.txtNoteOPE9.Text = String.Empty
            Me.txtparVNC9.Text = String.Empty
            Me.txtparPCA9.Text = String.Empty
            Me.txtparADSLPcAny9.Text = String.Empty
            Me.txtipPCA9.Text = String.Empty
            Me.txtNomeOPE10.Text = String.Empty
            Me.txtCognomeOPE10.Text = String.Empty
            Me.txtNoteOPE10.Text = String.Empty
            Me.txtparVNC10.Text = String.Empty
            Me.txtparPCA10.Text = String.Empty
            Me.txtparADSLPcAny10.Text = String.Empty
            Me.txtipPCA10.Text = String.Empty
            Me.txtNomeOPE11.Text = String.Empty
            Me.txtCognomeOPE11.Text = String.Empty
            Me.txtNoteOPE11.Text = String.Empty
            Me.txtparVNC11.Text = String.Empty
            Me.txtparPCA11.Text = String.Empty
            Me.txtparADSLPcAny11.Text = String.Empty
            Me.txtipPCA11.Text = String.Empty
            Me.txtNomeOPE12.Text = String.Empty
            Me.txtCognomeOPE12.Text = String.Empty
            Me.txtNoteOPE12.Text = String.Empty
            Me.txtparVNC12.Text = String.Empty
            Me.txtparPCA12.Text = String.Empty
            Me.txtparADSLPcAny12.Text = String.Empty
            Me.txtipPCA12.Text = String.Empty
            Me.txtNomeOPE13.Text = String.Empty
            Me.txtCognomeOPE13.Text = String.Empty
            Me.txtNoteOPE13.Text = String.Empty
            Me.txtparVNC13.Text = String.Empty
            Me.txtparPCA13.Text = String.Empty
            Me.txtparADSLPcAny13.Text = String.Empty
            Me.txtipPCA13.Text = String.Empty
            Me.txtNomeOPE14.Text = String.Empty
            Me.txtCognomeOPE14.Text = String.Empty
            Me.txtNoteOPE14.Text = String.Empty
            Me.txtparVNC14.Text = String.Empty
            Me.txtparPCA14.Text = String.Empty
            Me.txtparADSLPcAny14.Text = String.Empty
            Me.txtipPCA14.Text = String.Empty
        End If
    End Sub

    Private Sub cmblinea_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblinea.TextChanged
        Configlinea()
    End Sub

    Private Sub chkproj_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ProjectorCheck()
    End Sub


    Private Sub chkAbilitaProjector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAbilitaProjector.CheckedChanged
        ProjectorCheck()
    End Sub

    Private Sub chkeverpass_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ProjectorCheck()
    End Sub

    Private Sub chkonlyprojector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkonlyprojector.CheckedChanged
        If Me.chkonlyprojector.Checked = False Then
            frmAssistenza.Groupline.Visible = True
            frmAssistenza.lblteleassistenza.AutoSize = False
            frmAssistenza.lblteleassistenza.Location = New Point(17, 35)
            frmAssistenza.lblteleassistenza.Text = "Tele Assistenza "
            frmAssistenza.picLogoAzienda.Location = New Point(343, 72)
            frmAssistenza.picLogoAzienda.Size = New Size(266, 177)
            frmAssistenza.LinkAziendaAss.Location = New Point(343, 265)
            frmAssistenza.grpProjector.Location = New Point(344, 367)
            frmAssistenza.grpInfo.Location = New Point(343, 441)
            frmAssistenza.Size = New Size(628, 543)
        End If
    End Sub

    Private Sub cmbpreconfmail_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbpreconfmail.SelectedIndexChanged
        Select Case cmbpreconfmail.SelectedIndex
            Case 0
                Me.txtportaSMTP.Text = "465"
                Me.txtServerSMTP.Text = "smtp.google.com"
                Me.chkSSL.Checked = True
            Case 1
                Me.txtportaSMTP.Text = "465"
                Me.txtServerSMTP.Text = "smtp.mail.yahoo.it"
                Me.chkSSL.Checked = True
            Case 2
                Me.txtportaSMTP.Text = "587"
                Me.txtServerSMTP.Text = "smtp.email.it"
                Me.chkSSL.Checked = True
        End Select
    End Sub

    Private Sub chkAbilitaDownloadFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAbilitaDownloadFile.CheckedChanged
        Abilita_TransferFile()
    End Sub
    Public IconaCollegamentoAlt2 As String
    Private Sub lblicocollalt2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblicocollalt2.Click
        Dim apriicona2 As New OpenFileDialog
        apriicona2.DefaultExt = "*.png"
        apriicona2.Title = "Selezione File Immagine PNG (Preferibilmente 24X24)"
        apriicona2.Filter = "File di Immagine PNG | *.png"
        If apriicona2.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.IconaCollegamentoAlt2 = apriicona2.FileName
        End If
    End Sub

    Private Sub lblicocollalt2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblicocollalt2.MouseHover
        Me.Cursor = Cursors.Hand
    End Sub

    Private Sub lblicocollalt2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblicocollalt2.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub
End Class