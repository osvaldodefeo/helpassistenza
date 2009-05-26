Imports System.Net
Imports System.Reflection

Public Class frmAssistenza
    Public NumLinea As Integer
    Public VNCReconn As Boolean = False
    Public VNCAttivo As Boolean = False
    Public Operatore As Short = 0
    Dim ping As Net.NetworkInformation.Ping = New Net.NetworkInformation.Ping()

    Sub abilitaChiudiToolStr()
        If Operatore <> 0 Then
            Me.ChiudiConnessioneToolStripMenuItem1.Visible = True
        Else
            Me.ChiudiConnessioneToolStripMenuItem1.Visible = False
        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmioip.Click
        Me.Cursor = Cursors.WaitCursor
        frmMioip.ShowDialog()
    End Sub

    Private Sub btnabout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnabout.Click
        AbouthelAssistenza.ShowDialog()
    End Sub

    Private Sub ChiudiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChiudiToolStripMenuItem.Click
        If Operatore = 0 Then
            Me.NotifyHelp.Visible = False
            End
        Else
            ChiudiConnessioni()
            Me.NotifyHelp.Visible = False
            End
        End If
    End Sub

    Private Sub ConfigurazioneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurazioneToolStripMenuItem.Click
        frmConfigurazione.ShowDialog()
    End Sub

    Private Sub frmAssistenza_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (e.CloseReason = CloseReason.UserClosing) And (Operatore <> 0 Or ListenMode = True) Then
            e.Cancel = True
            Me.NotifyHelp.Visible = True
            Me.Hide()
        End If
        'Aggiorno Eseguibile se presente Aggiornamento del programma
        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\UPD_HelpAssistenza.exe") Then
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\HelpAssistenza.old") Then My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\HelpAssistenza.old")
            My.Computer.FileSystem.RenameFile(Application.StartupPath + "\HelpAssistenza.exe", "HelpAssistenza.old")
            My.Computer.FileSystem.RenameFile(Application.StartupPath + "\UPD_HelpAssistenza.exe", "HelpAssistenza.exe")
        End If
        'Controllo eventuale INI da aggiornare 
        AggiornamentoINI()
    End Sub

    Private Sub frmAssistenza_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ProcessoVNC As System.Diagnostics.Process()
        Settaggi.RecuperaSettaggi()
        trasportaNomeAzienda()
        trasportaCognome()
        ControllaEsistenzaEseguibili()
        Abilitalinee()
        IconeLinee()
        settatooltip()
        altrocoll2()
        gruppoaltrifire()
        ControlloListenMode()
        ControllaGeneraPathTeleControllo()
        ConfigProjector()
        ProcessoVNC = Process.GetProcessesByName("winvnc")
        If (ProcessoVNC.Length > 0) Then VNCAttivo = True
        abilitaChiudiToolStr()
        ControlloParametriAvvio()
        If CheckAutoUpdate = 1 Then SystemIdleTimer1.Start()
        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Package_Update\pkgupdate") Then Me.tmrUPDPKGOffline.Enabled = True
    End Sub

    Private Sub LinkAziendaAss_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkAziendaAss.LinkClicked
        System.Diagnostics.Process.Start(Me.LinkAziendaAss.Text)
        Me.LinkAziendaAss.LinkVisited = True
    End Sub

    Private Sub btnlinea1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea1.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 1
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny1.Text, frmConfigurazione.txtipPCA1.Text)
            Else
                'chiama remoto ADSL VNC
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, False)
                End If
            End If
        Else
            'chiama remoto modem
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA1.Text)
        End If

    End Sub

    Private Sub btnlinea1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea1.MouseDown
        NumLinea = 1
        ControllaContestualeLinee()
        If frmConfigurazione.txtnoteOPE1.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        Select Case NumLinea
            Case Is = 1
                MsgBox("Oeratore : " & frmConfigurazione.txtnomeOPE1.Text & " " & frmConfigurazione.txtCognomeOPE1.Text & vbCrLf & "eMail : " & frmConfigurazione.txtnoteOPE1.Text)
            Case Is = 2
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE2.Text & " " & frmConfigurazione.txtCognomeOPE2.Text & vbCrLf & "eMail : " & frmConfigurazione.txtnoteOPE2.Text)
            Case Is = 3
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE3.Text & " " & frmConfigurazione.txtCognomeOPE3.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE3.Text)
            Case Is = 4
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE4.Text & " " & frmConfigurazione.txtCognomeOPE4.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE4.Text)
            Case Is = 5
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE5.Text & " " & frmConfigurazione.txtCognomeOPE5.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE5.Text)
            Case Is = 6
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE6.Text & " " & frmConfigurazione.txtCognomeOPE6.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE6.Text)
            Case Is = 7
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE7.Text & " " & frmConfigurazione.txtCognomeOPE7.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE7.Text)
            Case Is = 8
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE8.Text & " " & frmConfigurazione.txtCognomeOPE8.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE8.Text)
            Case Is = 9
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE9.Text & " " & frmConfigurazione.txtCognomeOPE9.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE9.Text)
            Case Is = 10
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE10.Text & " " & frmConfigurazione.txtCognomeOPE10.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE10.Text)
            Case Is = 11
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE11.Text & " " & frmConfigurazione.txtCognomeOPE11.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE11.Text)
            Case Is = 12
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE12.Text & " " & frmConfigurazione.txtCognomeOPE12.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE12.Text)
            Case Is = 13
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE13.Text & " " & frmConfigurazione.txtCognomeOPE13.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE13.Text)
            Case Is = 14
                MsgBox("Oeratore : " & frmConfigurazione.txtNomeOPE14.Text & " " & frmConfigurazione.txtCognomeOPE14.Text & vbCrLf & "eMail : " & frmConfigurazione.txtNoteOPE14.Text)
        End Select
        NumLinea = 0
    End Sub

    Private Sub btnlinea2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea2.MouseDown
        NumLinea = 2
        ControllaContestualeLinee()
        If frmConfigurazione.txtnoteOPE2.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea3.MouseDown
        NumLinea = 3
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE3.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea4.MouseDown
        NumLinea = 4
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE4.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea5_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea5.MouseDown
        NumLinea = 5
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE5.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea6_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea6.MouseDown
        NumLinea = 6
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE6.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea7_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea7.MouseDown
        NumLinea = 7
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE7.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub


    Private Sub btnlinea8_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea8.MouseDown
        NumLinea = 8
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE8.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea9_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea9.MouseDown
        NumLinea = 9
        ControllaContestualeLinee()
        If frmConfigurazione.txtnoteOPE2.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea10_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea10.MouseDown
        NumLinea = 10
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE10.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea11_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea11.MouseDown
        NumLinea = 11
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE11.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea12_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea12.MouseDown
        NumLinea = 12
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE12.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea13_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea13.MouseDown
        NumLinea = 13
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE13.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea14_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnlinea14.MouseDown
        NumLinea = 14
        ControllaContestualeLinee()
        If frmConfigurazione.txtNoteOPE14.Text = String.Empty OrElse frmConfigurazione.chkeMailtecnici.Checked = False Then
            Me.EMailToolStripMenuItem.Visible = False
        Else
            Me.EMailToolStripMenuItem.Visible = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuLinea.Show(MousePosition)
        End If
    End Sub

    Private Sub btnlinea2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea2.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 2
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny2.Text, frmConfigurazione.txtipPCA2.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA2.Text)
        End If

    End Sub

    Private Sub btnlinea3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea3.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 3
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny3.Text, frmConfigurazione.txtipPCA3.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA3.Text)
        End If

    End Sub

    Private Sub btnlinea4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea4.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 4
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                If ControllaADSL2() = True Then Exit Sub
                'chiama remoto Pc Anywhere ADSL
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny4.Text, frmConfigurazione.txtipPCA4.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA4.Text)
        End If

    End Sub

    Private Sub btnlinea5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea5.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 5
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny5.Text, frmConfigurazione.txtipPCA5.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA5.Text)
        End If

    End Sub

    Private Sub btnlinea6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea6.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 6
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny6.Text, frmConfigurazione.txtipPCA6.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, True)
                Else
                    If ControllaModem2() = True Then Exit Sub
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA6.Text)
        End If
    End Sub

    Private Sub btnlinea7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea7.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 7
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny7.Text, frmConfigurazione.txtipPCA7.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA7.Text)
        End If

    End Sub

    Private Sub btnlinea8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea8.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 8
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny8.Text, frmConfigurazione.txtipPCA8.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA8.Text)
        End If

    End Sub

    Private Sub btnlinea9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea9.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 9
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny9.Text, frmConfigurazione.txtipPCA9.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA9.Text)
        End If

    End Sub

    Private Sub btnlinea10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea10.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 10
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny10.Text, frmConfigurazione.txtipPCA10.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA10.Text)
        End If

    End Sub

    Private Sub btnlinea11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea11.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 11
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny11.Text, frmConfigurazione.txtipPCA11.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA11.Text)
        End If

    End Sub

    Private Sub btnlinea12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea12.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 12
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny12.Text, frmConfigurazione.txtipPCA12.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA12.Text)
        End If

    End Sub

    Private Sub btnlinea13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea13.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 13
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny13.Text, frmConfigurazione.txtipPCA13.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA13.Text)
        End If

    End Sub

    Private Sub btnlinea14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlinea14.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Operatore = 14
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            If frmConfigurazione.cmbprogadsl.Text = "PcAnyWhere" OrElse frmConfigurazione.cmbprogadsl.SelectedIndex = 1 Then
                'chiama remoto Pc Anywhere ADSL
                If ControllaADSL2() = True Then Exit Sub
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny14.Text, frmConfigurazione.txtipPCA14.Text)
            Else
                If ControllaADSL() = True Then Exit Sub
                If frmConfigurazione.chkreconn.Checked = True Then
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, True)
                Else
                    Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                    Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - VNC"
                    Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                    ControllaTipListen()
                    Me.NotifyHelp.Visible = True
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    Me.Hide()
                    ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, False)
                End If
            End If
        Else
            If frmConfigurazione.chkabilitaLog.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then Logga.ScriviInLog("Test Ping Fallito, tento Connessione via Modem ! ")
            Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - Modem"
            Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
            ControllaTipListen()
            Me.NotifyHelp.Visible = True
            Me.NotifyHelp.ShowBalloonTip(2500)
            Me.Hide()
            ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA14.Text)
        End If

    End Sub

    Private Sub btncollalt1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncollalt1.Click
        Dim PercorsoTeamViewer As String = frmConfigurazione.txtteamviewer.Text
        'Controllo se presente Variabile %PROGRAMPATH% e/o %PROGRAMFILES%
        If PercorsoTeamViewer.Contains("%PROGRAMPATH%") Then PercorsoTeamViewer = Replace(PercorsoTeamViewer, "%PROGRAMPATH%", Application.StartupPath)
        If PercorsoTeamViewer.Contains("%PROGRAMFILES%") Then PercorsoTeamViewer = Replace(PercorsoTeamViewer, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If PercorsoTeamViewer = String.Empty Then
            MsgBox("Configurare il percorso Team Viewer", MsgBoxStyle.Critical, Title:="HelpAssistenza - Errore")
        Else
            Try
                System.Diagnostics.Process.Start(PercorsoTeamViewer)
            Catch
                MsgBox("Impossibile Trovare Team Viewer !" + vbCrLf + "Configurare il percorso Team Viewer", MsgBoxStyle.Critical, Title:="HelpAssistenza - Errore")
                Exit Sub
            End Try
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Team Viewer Avviato")
        End If
    End Sub

    Private Sub btncollalt2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncollalt2.Click
        Dim CollegamentoAlternativo2 As String = frmConfigurazione.txtother.Text
        'Controllo se presente Variabile %PROGRAMPATH% e/o %PROGRAMFILES%
        If CollegamentoAlternativo2.Contains("%PROGRAMPATH%") Then CollegamentoAlternativo2 = Replace(CollegamentoAlternativo2, "%PROGRAMPATH%", Application.StartupPath)
        If CollegamentoAlternativo2.Contains("%PROGRAMFILES%") Then CollegamentoAlternativo2 = Replace(CollegamentoAlternativo2, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If CollegamentoAlternativo2 = String.Empty Then
            MsgBox("Configurare il percorso " + frmConfigurazione.txtnmealtro.Text, MsgBoxStyle.Critical, Title:="HelpAssistenza - Errore")
        Else
            Try
                System.Diagnostics.Process.Start(CollegamentoAlternativo2)
            Catch
                MsgBox("Impossibile Trovare " + frmConfigurazione.txtnmealtro.Text + " !" + vbCrLf + "Configurare il percorso " + frmConfigurazione.txtnmealtro.Text, MsgBoxStyle.Critical, Title:="HelpAssistenza - Errore")
                Exit Sub
            End Try
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog(frmConfigurazione.txtnmealtro.Text + " Avviato")
        End If
    End Sub

    Private Sub InfoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem1.Click
        btnabout.PerformClick()
    End Sub

    Private Sub CollegaViaModemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollegaViaModemToolStripMenuItem.Click
        If ControllaModem2() = True Then Exit Sub
        Select Case NumLinea
            Case Is = 1
                Operatore = 1
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA1.Text)
            Case Is = 2
                Operatore = 2
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA2.Text)
            Case Is = 3
                Operatore = 3
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA3.Text)
            Case Is = 4
                Operatore = 4
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA4.Text)
            Case Is = 5
                Operatore = 5
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA5.Text)
            Case Is = 6
                Operatore = 6
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA6.Text)
            Case Is = 7
                Operatore = 7
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA7.Text)
            Case Is = 8
                Operatore = 8
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA8.Text)
            Case Is = 9
                Operatore = 9
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA9.Text)
            Case Is = 10
                Operatore = 10
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA10.Text)
            Case Is = 11
                Operatore = 11
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA11.Text)
            Case Is = 12
                Operatore = 12
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA12.Text)
            Case Is = 13
                Operatore = 13
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA13.Text)
            Case Is = 14
                Operatore = 14
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - Modem"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoModem(frmConfigurazione.txtpercorsomodem.Text, frmConfigurazione.txtparPCA14.Text)
        End Select
    End Sub

    Private Sub CollegaAutoReconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollegaAutoReconnectToolStripMenuItem.Click
        If ControllaADSL() = True Then Exit Sub
        Select Case NumLinea
            Case Is = 1
                Operatore = 1
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, True)
            Case Is = 2
                Operatore = 2
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, True)
            Case Is = 3
                Operatore = 3
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, True)
            Case Is = 4
                Operatore = 4
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, True)
            Case Is = 5
                Operatore = 5
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, True)
            Case Is = 6
                Operatore = 6
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, True)
            Case Is = 7
                Operatore = 7
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, True)
            Case Is = 8
                Operatore = 8
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, True)
            Case Is = 9
                Operatore = 9
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, True)
            Case Is = 10
                Operatore = 10
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, True)
            Case Is = 11
                Operatore = 11
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, True)
            Case Is = 12
                Operatore = 12
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, True)
            Case Is = 13
                Operatore = 13
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, True)
            Case Is = 14
                Operatore = 14
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                VNCReconn = True
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, True)
        End Select
    End Sub

    Private Sub TimerFTP_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerFTP.Tick
        TeleControlloFTP.ControlloChiamataFTP(frmConfigurazione.txtSitoTeleControllo.Text, frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text, frmConfigurazione.txtPathTeleControllo.Text)
    End Sub

    Private Sub ListenModeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListenModeToolStripMenuItem.Click
        If ListenModeToolStripMenuItem.Text = "Stop Listen Mode" Then
            ListenMode = False
            TimerFTP.Enabled = False
            NotifyHelp.Visible = False
            ListenModeToolStripMenuItem.Text = "Avvia Listen Mode"
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Listen Mode Disattivata")
        Else
            If frmConfigurazione.txtSitoTeleControllo.Text = String.Empty And frmConfigurazione.txtUserTeleControllo.Text = String.Empty And frmConfigurazione.txtPassTeleControllo.Text = String.Empty Then
                MessageBox.Show("Controllare Parametri Fondamentali Connessione FTP" & vbCrLf & "URL Sito, Username, Password !", "HelpAssistenza - TeleControllo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
                If Not pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
                    If MessageBox.Show("Connessione ad Internet non Attiva o Problemi di Connettività !" + vbCrLf + "Vuoi entrare ugualmente in modalità Listen Mode ?", "HelpAssistenza - Listen Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then Exit Sub
                End If
                ListenMode = True
                Select Case frmConfigurazione.cmbIntervalloTeleControllo.SelectedIndex
                    Case Is = 0
                        Me.TimerFTP.Interval = 60000
                    Case Is = 1
                        Me.TimerFTP.Interval = 120000
                    Case Is = 2
                        Me.TimerFTP.Interval = 180000
                    Case Is = 3
                        Me.TimerFTP.Interval = 240000
                    Case Is = 4
                        Me.TimerFTP.Interval = 300000
                    Case Is = 5
                        Me.TimerFTP.Interval = 600000
                    Case Is = 6
                        Me.TimerFTP.Interval = 1800000
                    Case Else
                        Me.TimerFTP.Interval = 150000
                End Select
                Me.TimerFTP.Start()
                ListenModeToolStripMenuItem.Text = "Stop Listen Mode"
                If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Listen Mode Attivata")
                NotifyHelp.Text = "HelpAssistenza - Listen Mode"
                NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                NotifyHelp.BalloonTipTitle = "HelpAssistenza"
                NotifyHelp.BalloonTipText = "Listen Mode"
                ListenMode = True
                NotifyHelp.Visible = True
                NotifyHelp.ShowBalloonTip(5000)
                Me.Hide()
            End If
        End If

    End Sub

    Private Sub OpzioniToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpzioniToolStripMenuItem.Click
        If TimerFTP.Enabled = True Then
            ListenModeToolStripMenuItem.Text = "Stop Listen Mode"
            NotifyHelp.Visible = True
        Else
            ListenModeToolStripMenuItem.Text = "Avvia Listen Mode"
            NotifyHelp.Visible = False
        End If
    End Sub

    Private Sub NotifyHelp_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyHelp.MouseDoubleClick
        ControllaTipListen()
        If (TimerFTP.Enabled = True Or ListenMode = True) OrElse (Operatore <> 0) Then
            NotifyHelp.Visible = True
        Else
            NotifyHelp.Visible = False
        End If
        abilitaChiudiToolStr()
        Me.Focus()
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub frmAssistenza_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized And Me.Operatore <> 0 Then
            NotifyHelp.Visible = True
            NotifyHelp.Text = "HelpAssistenza - In Collegamento"
            Me.Hide()
            Me.WindowState = FormWindowState.Normal
        End If
        If (Me.WindowState = FormWindowState.Minimized) And (Me.TimerFTP.Enabled = True OrElse ListenMode = True) Then
            Me.Text = Me.Text & " - Listen Mode"
            Me.Hide()
            NotifyHelp.Text = "HelpAssistenza - Listen Mode"
            NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
            NotifyHelp.BalloonTipTitle = "HelpAssistenza"
            NotifyHelp.BalloonTipText = "Listen Mode"
            NotifyHelp.Visible = True
            NotifyHelp.ShowBalloonTip(5000)
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub CollegaPCAnywhereADSLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollegaPCAnywhereADSLToolStripMenuItem.Click
        If ControllaADSL2() = True Then Exit Sub
        Select Case NumLinea
            Case Is = 1
                Operatore = 1
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny1.Text, frmConfigurazione.txtipPCA1.Text)
            Case Is = 2
                Operatore = 2
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny2.Text, frmConfigurazione.txtipPCA2.Text)
            Case Is = 3
                Operatore = 3
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny3.Text, frmConfigurazione.txtipPCA3.Text)
            Case Is = 4
                Operatore = 4
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny4.Text, frmConfigurazione.txtipPCA4.Text)
            Case Is = 5
                Operatore = 5
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny5.Text, frmConfigurazione.txtipPCA5.Text)
            Case Is = 6
                Operatore = 6
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny6.Text, frmConfigurazione.txtipPCA6.Text)
            Case Is = 7
                Operatore = 7
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny7.Text, frmConfigurazione.txtipPCA7.Text)
            Case Is = 8
                Operatore = 8
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny8.Text, frmConfigurazione.txtipPCA8.Text)
            Case Is = 9
                Operatore = 9
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny9.Text, frmConfigurazione.txtipPCA9.Text)
            Case Is = 10
                Operatore = 10
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny10.Text, frmConfigurazione.txtipPCA10.Text)
            Case Is = 11
                Operatore = 11
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny11.Text, frmConfigurazione.txtipPCA11.Text)
            Case Is = 12
                Operatore = 12
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny12.Text, frmConfigurazione.txtipPCA12.Text)
            Case Is = 13
                Operatore = 13
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny13.Text, frmConfigurazione.txtipPCA13.Text)
            Case Is = 14
                Operatore = 14
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - PcAnywhere"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaremotoPCADSL(frmConfigurazione.txtpercorsoadsl2.Text, frmConfigurazione.txtparADSLPcAny14.Text, frmConfigurazione.txtipPCA14.Text)
        End Select
    End Sub

    Private Sub CollegaVNCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollegaVNCToolStripMenuItem.Click
        If ControllaADSL() = True Then Exit Sub
        Select Case NumLinea
            Case Is = 1
                Operatore = 1
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 1 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC1.Text, False)
            Case Is = 2
                Operatore = 2
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 2 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC2.Text, False)
            Case Is = 3
                Operatore = 3
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 3 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC3.Text, False)
            Case Is = 4
                Operatore = 4
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 4 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC4.Text, False)
            Case Is = 5
                Operatore = 5
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 5 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC5.Text, False)
            Case Is = 6
                Operatore = 6
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 6 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC6.Text, False)
            Case Is = 7
                Operatore = 7
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 7 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC7.Text, False)
            Case Is = 8
                Operatore = 8
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 8 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC8.Text, False)
            Case Is = 9
                Operatore = 9
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 9 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC9.Text, False)
            Case Is = 10
                Operatore = 10
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 10 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC10.Text, False)
            Case Is = 11
                Operatore = 11
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 11 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC11.Text, False)
            Case Is = 12
                Operatore = 12
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 12 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC12.Text, False)
            Case Is = 13
                Operatore = 13
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 13 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC13.Text, False)
            Case Is = 14
                Operatore = 14
                Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
                Me.NotifyHelp.BalloonTipTitle = "Collegamento Linea 14 - VNC"
                Me.NotifyHelp.BalloonTipText = "Connessione in Corso ..."
                ControllaTipListen()
                Me.NotifyHelp.Visible = True
                Me.NotifyHelp.ShowBalloonTip(2500)
                Me.Hide()
                ChiamaRemotoADSL(frmConfigurazione.txtpercorsoadsl.Text, frmConfigurazione.txtparVNC14.Text, False)
        End Select
    End Sub

    Private Sub EsciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsciToolStripMenuItem.Click
        If Operatore = 0 Then
            Me.NotifyHelp.Visible = False
            End
        Else
            ChiudiConnessioni()
            Me.NotifyHelp.Visible = False
            End
        End If
    End Sub

    Private Sub ChiudiConnessioneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChiudiConnessioneToolStripMenuItem.Click
        If TimerFTP.Enabled = True OrElse ListenMode = True Then
            ChiudiConnessioni()
        Else
            ChiudiConnessioni()
            Me.NotifyHelp.Visible = False
            Me.Show()
        End If
    End Sub

    Private Sub ApriToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApriToolStripMenuItem.Click
        ControllaTipListen()
        If (TimerFTP.Enabled = True Or ListenMode = True) OrElse (Operatore <> 0) Then
            NotifyHelp.Visible = True
        Else
            NotifyHelp.Visible = False
        End If
        abilitaChiudiToolStr()
        Me.Focus()
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub NuovoReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NuovoReportToolStripMenuItem.Click
        frmReport.lblAzienda.Visible = True
        frmReport.Label1.Visible = True
        frmReport.Label2.Visible = True
        frmReport.Label3.Visible = True
        frmReport.lblDurataAssistenza.Visible = True
        frmReport.lblIntDurataAssistenza.Visible = True
        frmReport.lblNomeAzienda.Visible = True
        frmReport.lblnometecnico.Visible = True
        frmReport.lblTecnico.Visible = True
        frmReport.btnFinito.Visible = True
        frmReport.btnStampa.Visible = False
        frmReport.lblInterrotto.Visible = False
        frmReport.PicWarning.Visible = False
        frmReport.BtnOK.Enabled = False
        If frmConfigurazione.chkabilitaReport.Checked = True And frmConfigurazione.chkCronoSessioni.Checked = True Then
            frmReport.lblDurataAssistenza.Visible = True
            frmReport.lblIntDurataAssistenza.Visible = True
            frmReport.Label2.Visible = True
        Else
            frmReport.lblDurataAssistenza.Visible = False
            frmReport.lblIntDurataAssistenza.Visible = False
            frmReport.Label2.Visible = False
        End If
        If frmConfigurazione.chkoperatoreReport.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then
            frmReport.Label3.Visible = True
            frmReport.lblnometecnico.Visible = True
            frmReport.lblTecnico.Visible = True
            Select Case Operatore
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
        Else
            frmReport.Label3.Visible = False
            frmReport.lblnometecnico.Visible = False
            frmReport.lblTecnico.Visible = False
        End If
        If frmReport.lblnometecnico.Visible = True Then
            frmReport.Label2.Visible = True
        Else
            frmReport.Label2.Visible = False
        End If
        If frmReport.lblDurataAssistenza.Visible = True Then
            frmReport.Label3.Visible = True
        Else
            frmReport.Label3.Visible = False
        End If
        frmReport.Show()
    End Sub

    Private Sub TimerMonitorProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMonitorProcess.Tick
        'Controllo se Attivo PCAnyWhere oppure VNC
        If PCAny_Attivo = True Then
            Dim PCanyProcess() As Process = Process.GetProcessesByName("awhost32")
            If frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True And Operatore <> 0 Then
                If Not PCanyProcess.Length > 0 Then
                    frmReport.lblAzienda.Visible = False
                    frmReport.Label1.Visible = False
                    frmReport.Label2.Visible = False
                    frmReport.Label3.Visible = False
                    frmReport.lblDurataAssistenza.Visible = False
                    frmReport.lblIntDurataAssistenza.Visible = False
                    frmReport.lblNomeAzienda.Visible = False
                    frmReport.lblnometecnico.Visible = False
                    frmReport.lblTecnico.Visible = False
                    frmReport.btnFinito.Visible = False
                    frmReport.lblInterrotto.Visible = True
                    frmReport.PicWarning.Visible = True
                    frmReport.BtnOK.Enabled = True
                    frmReport.RichTextBoxRiepilogo.Text = frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text
                    NotifyHelp.Visible = False
                    NotifyHelp.Visible = True
                    NotifyHelp.BalloonTipIcon = ToolTipIcon.Error
                    NotifyHelp.BalloonTipText = "Errore Connessione!"
                    NotifyHelp.ShowBalloonTip(5000)
                    frmReport.Show()
                    Me.Hide()
                    Me.TimerMonitorProcess.Enabled = False
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta")
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Terminata")
                ElseIf frmConfigurazione.chkChiudiInterruzzioneassi.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then
                    If Not PCanyProcess.Length > 0 Then
                        If Me.TimerFTP.Enabled = True OrElse ListenMode = True Then
                            Me.Hide()
                            Me.TimerMonitorProcess.Enabled = False
                            NotifyHelp.Visible = False
                            NotifyHelp.Visible = True
                            NotifyHelp.BalloonTipIcon = ToolTipIcon.Error
                            NotifyHelp.BalloonTipText = "Errore Connessione!"
                            NotifyHelp.ShowBalloonTip(5000)
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta, torno In Listen Mode")
                        Else
                            Me.NotifyHelp.Visible = False
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta")
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Terminata")
                            End
                        End If
                    End If
                Else
                    Me.TimerMonitorProcess.Enabled = False
                End If
            End If

        Else  'Switch di Controllo se PCAnywhere o VNC


            ' Controllo Uso Memoria VNC
            Dim processes() As Process = Process.GetProcessesByName("winvnc")
            If frmConfigurazione.chkNotificheInterruzzioniTeleAssistenza.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True And Operatore <> 0 Then
                If processes.Length > 0 Then
                    Dim cnt As Integer
                    ' WorkingSet64: The amount of physical memory, in bytes, allocated for the associated process
                    If (processes(cnt).WorkingSet64 / 1024) < 6000 Then
                        frmReport.lblAzienda.Visible = False
                        frmReport.Label1.Visible = False
                        frmReport.Label2.Visible = False
                        frmReport.Label3.Visible = False
                        frmReport.lblDurataAssistenza.Visible = False
                        frmReport.lblIntDurataAssistenza.Visible = False
                        frmReport.lblNomeAzienda.Visible = False
                        frmReport.lblnometecnico.Visible = False
                        frmReport.lblTecnico.Visible = False
                        frmReport.btnFinito.Visible = False
                        frmReport.lblInterrotto.Visible = True
                        frmReport.PicWarning.Visible = True
                        frmReport.BtnOK.Enabled = True
                        frmReport.RichTextBoxRiepilogo.Text = frmConfigurazione.txtmessaggioInteruzzioneTeleAssistenza.Text
                        NotifyHelp.Visible = False
                        NotifyHelp.Visible = True
                        NotifyHelp.BalloonTipIcon = ToolTipIcon.Error
                        NotifyHelp.BalloonTipText = "Errore Connessione!"
                        NotifyHelp.ShowBalloonTip(5000)
                        ChiudiConnessioni()
                        frmReport.Show()
                        Me.Hide()
                        Me.TimerMonitorProcess.Enabled = False
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta")
                        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Terminata")
                    End If
                End If

            ElseIf frmConfigurazione.chkChiudiInterruzzioneassi.Checked = True And frmConfigurazione.chkabilitaReport.Checked = True Then
                If processes.Length > 0 Then
                    Dim cnt As Integer
                    If (processes(cnt).WorkingSet64 / 1024) < 6000 Then
                        If Me.TimerFTP.Enabled = True OrElse ListenMode = True Then
                            Me.Hide()
                            Me.TimerMonitorProcess.Enabled = False
                            NotifyHelp.Visible = False
                            NotifyHelp.Visible = True
                            NotifyHelp.BalloonTipIcon = ToolTipIcon.Error
                            NotifyHelp.BalloonTipText = "Errore Connessione!"
                            NotifyHelp.ShowBalloonTip(5000)
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta, torno In Listen Mode")
                        Else
                            Me.NotifyHelp.Visible = False
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Interrotta")
                            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Terminata")
                            Me.EsciToolStripMenuItem.PerformClick()
                        End If
                    End If
                End If
            Else
                Me.TimerMonitorProcess.Enabled = False
            End If

        End If  'Chiudo Controllo se Controllare VNC o PCAnywhere

    End Sub

    Private Sub btnGuida_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMail.Click
        If frmConfigurazione.vtxtemailaz.Text = String.Empty And frmConfigurazione.txtemailazienda.Text = String.Empty Then
            Dim tip As New ToolTip
            tip.BackColor = Color.White
            tip.ToolTipIcon = ToolTipIcon.Error
            tip.ToolTipTitle = "HelpAssistenza - eMail"
            tip.Show("Errore : eMail non Configurata !", Me.btnMail)
            Exit Sub
        End If
        frmInvioMail.TOMAIL = frmConfigurazione.vtxtemailaz.Text
        If frmConfigurazione.vtxtemailaz.Text = String.Empty And frmConfigurazione.txtemailazienda.Text <> String.Empty Then frmInvioMail.TOMAIL = frmConfigurazione.txtemailazienda.Text
        If frmConfigurazione.chkeMailtecnici.Checked = False Then
            frmInvioMail.lbltecnico.Visible = False
            frmInvioMail.cmbTecnico.Visible = False
        Else
            frmInvioMail.lbltecnico.Visible = True
            frmInvioMail.cmbTecnico.Visible = True
        End If
        frmInvioMail.ShowDialog()
    End Sub

    Private Sub GuidaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuidaToolStripMenuItem.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath + "\" + Application.ProductName + ".chm") = True Then
            System.Diagnostics.Process.Start(Application.StartupPath + "\" + Application.ProductName + ".chm")
        Else
            MsgBox("File di Guida HelpAssistenza.chm Non Trovato!", MsgBoxStyle.Exclamation, "HelpAssistenza - Avviso")
        End If
    End Sub

    Private Sub ContextMenuNotify_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuNotify.Opened
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then ToolStripComboBox1.Enabled = True
    End Sub

    Private Sub ContextMenuNotify_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuNotify.Opening
        If Operatore <> 0 Then
            NuovoReportToolStripMenuItem.Enabled = True
            ChiudiConnessioneToolStripMenuItem.Enabled = True
            NuovoReportToolStripMenuItem.Visible = True
            ChiudiConnessioneToolStripMenuItem.Visible = True
        Else
            NuovoReportToolStripMenuItem.Enabled = False
            ChiudiConnessioneToolStripMenuItem.Enabled = False
            NuovoReportToolStripMenuItem.Visible = False
            ChiudiConnessioneToolStripMenuItem.Visible = False
        End If
        If frmConfigurazione.chkAbilitaUploadFile.Checked = True Then
            InviaFileToolStripMenuItem.Visible = True
            InviaCartellaToolStripMenuItem.Visible = True
            InviaFileToolStripMenuItem.Enabled = True
            InviaCartellaToolStripMenuItem.Enabled = True
            DownloadFileToolStripMenuItem.Visible = True
            DownloadFileToolStripMenuItem.Enabled = True
        Else
            InviaFileToolStripMenuItem.Visible = False
            InviaCartellaToolStripMenuItem.Visible = False
            InviaFileToolStripMenuItem.Enabled = False
            InviaCartellaToolStripMenuItem.Enabled = False
            DownloadFileToolStripMenuItem.Visible = False
            DownloadFileToolStripMenuItem.Enabled = False
        End If
        If frmConfigurazione.chkAbilitaDownloadFile.Checked Then
            DownloadFileToolStripMenuItem1.Visible = True
            ToolStripComboBox1.Visible = True
            If frmConfigurazione.cmbdownloadconn.SelectedIndex = 0 And (frmConfigurazione.chklistdownweb.Checked = False And frmConfigurazione.txtServerUP.Text = String.Empty) Then ToolStripComboBox1.Visible = False
        Else
            DownloadFileToolStripMenuItem1.Visible = False
            ToolStripComboBox1.Visible = False
        End If
        If Operatore = 0 And ListenMode = True Then
            InviaFileToolStripMenuItem.Visible = False
            InviaCartellaToolStripMenuItem.Visible = False
            DownloadFileToolStripMenuItem.Visible = False
        End If
        ToolStripComboBox1.SelectedIndex = 0
    End Sub

    Private Sub TimerAvvioPar_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerAvvioPar.Tick
        Me.Hide()
        Me.TimerAvvioPar.Enabled = False
    End Sub

    Private Sub SpegniPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpegniPCToolStripMenuItem.Click
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
        Me.NotifyHelp.Visible = False
        Application.Exit()
    End Sub

    Private Sub RiavviaPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RiavviaPCToolStripMenuItem.Click
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
        Me.NotifyHelp.Visible = False
        Application.Exit()
    End Sub

    Private Sub EseguiScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EseguiScriptToolStripMenuItem.Click
        Me.NotifyHelp.BalloonTipIcon = ToolTipIcon.Info
        Me.NotifyHelp.BalloonTipTitle = "HelpAssistenza"
        Me.NotifyHelp.BalloonTipText = "Esecuzione Script "
        ControllaTipListen()

        Try
            Dim PathScript As String
            If OpenFileScript.ShowDialog = Windows.Forms.DialogResult.OK Then
                If OpenFileScript.FileName <> String.Empty Then
                    PathScript = OpenFileScript.FileName
                    Shell(Chr(34) + PathScript + Chr(34), AppWinStyle.NormalFocus, False)
                    Me.NotifyHelp.ShowBalloonTip(2500)
                    If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Eseguito Script con Successo")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Errore Esecuzione Script ! " + vbCrLf + ex.Message, "HelpAssistenza - Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Esecuzione Script")
        End Try
    End Sub

    Private Sub Linea1ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea1ToolStripMenuItem1.Click
        If Linea1.Visible = True Then btnlinea1.PerformClick()
    End Sub

    Private Sub Linea02ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea02ToolStripMenuItem.Click
        If Linea2.Visible = True Then btnlinea2.PerformClick()
    End Sub

    Private Sub Linea03ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea03ToolStripMenuItem.Click
        If Linea3.Visible = True Then btnlinea3.PerformClick()
    End Sub

    Private Sub Linea04ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea04ToolStripMenuItem.Click
        If Linea4.Visible = True Then btnlinea4.PerformClick()
    End Sub

    Private Sub Linea05ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea05ToolStripMenuItem.Click
        If Linea5.Visible = True Then btnlinea5.PerformClick()
    End Sub

    Private Sub Linea06ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea06ToolStripMenuItem.Click
        If Linea6.Visible = True Then btnlinea6.PerformClick()
    End Sub

    Private Sub Linea07ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea07ToolStripMenuItem.Click
        If Linea7.Visible = True Then btnlinea7.PerformClick()
    End Sub

    Private Sub Linea08ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea08ToolStripMenuItem.Click
        If Linea8.Visible = True Then btnlinea8.PerformClick()
    End Sub

    Private Sub Linea09ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea09ToolStripMenuItem.Click
        If Linea9.Visible = True Then btnlinea9.PerformClick()
    End Sub

    Private Sub Linea10ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea10ToolStripMenuItem1.Click
        If Linea10.Visible = True Then btnlinea10.PerformClick()
    End Sub

    Private Sub Linea11ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea11ToolStripMenuItem1.Click
        If Linea11.Visible = True Then btnlinea11.PerformClick()
    End Sub

    Private Sub Linea12ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea12ToolStripMenuItem1.Click
        If Linea12.Visible = True Then btnlinea12.PerformClick()
    End Sub

    Private Sub Linea13ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea13ToolStripMenuItem1.Click
        If Linea13.Visible = True Then btnlinea13.PerformClick()
    End Sub

    Private Sub Linea14ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea14ToolStripMenuItem1.Click
        If Linea14.Visible = True Then btnlinea14.PerformClick()
    End Sub

    Private Sub Linea1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea1ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(1)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub RiavviaPCERichiamaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RiavviaPCERichiamaToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(NumLinea)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea2ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(2)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea3ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea3ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(3)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea4ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea4ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(4)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea5ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(5)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea6ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea6ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(6)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea7ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea7ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(7)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea8ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea8ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(8)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea9ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea9ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(9)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea10ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea10ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(10)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea11ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea11ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(11)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea12ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea12ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(12)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea13ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea13ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(13)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub Linea14ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Linea14ToolStripMenuItem.Click
        If CheckService("HelpAssistenza") OrElse (CheckService("helpassistenza") Or CheckService("HELPASSISTENZA")) Then
            'Setta il Servizio per l'avvio collegamento al riavvio tramite servizio
        Else
            'Setta Avvio Automatico su registro Sistema RunOnce
            RicollegaAvvioReg(14)
        End If
        Me.NotifyHelp.Visible = False
        RiavviaPC()
        Application.Exit()
    End Sub

    Private Sub tsmiTerminale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmiTerminale.Click
        If My.Computer.Info.OSFullName.Contains("98") Or My.Computer.Info.OSFullName.Contains("95") Then
            Shell("command.com", AppWinStyle.NormalFocus, False)
        Else
            Shell("cmd.exe", AppWinStyle.NormalFocus, False)
        End If
    End Sub

    Private Sub InviaFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InviaFileToolStripMenuItem.Click
        SelFiletoUpload.InitialDirectory = Environ("SystemDrive")
        If SelFiletoUpload.ShowDialog = Windows.Forms.DialogResult.OK Then
            If SelFiletoUpload.FileName <> String.Empty Then
                'Upload del file su FTP
                FileTransefer = 1
                UploadFileTransf = SelFiletoUpload.FileName
                SetTemporaneiUp()
                frmUploadFile.Show()
            End If
        End If
    End Sub

    Private Sub RiavviaPCERichiamaToolStripMenuItem_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles RiavviaPCERichiamaToolStripMenuItem.MouseHover
        Abilitalinee()
    End Sub

    Private Sub DownloadFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadFileToolStripMenuItem.Click
        SelFiletoUpload.InitialDirectory = Environ("SystemDrive")
        If SelFiletoUpload.ShowDialog = Windows.Forms.DialogResult.OK Then
            If SelFiletoUpload.FileName <> String.Empty Then
                'Upload del file su FTP
                FileTransefer = 3
                UploadFileTransf = SelFiletoUpload.FileName
                SetTemporaneiUp()
                frmUploadFile.Show()
            End If
        End If
    End Sub

    Private Sub ChiudiConnessioneToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChiudiConnessioneToolStripMenuItem1.Click
        If TimerFTP.Enabled = True OrElse ListenMode = True Then
            ChiudiConnessioni()
        Else
            ChiudiConnessioni()
        End If
        abilitaChiudiToolStr()
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateToolStripMenuItem.Click
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            ModuloUpdate.CheckForUpdates("http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.txt", "http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.zip", "", True, "Una Nuova Versione di HelpAssistenza è disponibile !" + vbCrLf + "Vuoi Scaricarla adesso ?")
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\UPD_HelpAssistenza.exe") Then
                If MessageBox.Show("L'aggiornamento è stato Scaricato Correttamente !" + vbCrLf + "Vuoi Installare l'aggiornamento adesso?", "Helpassistenza - AutoUpdate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Application.Exit()
                End If
            End If
        Else
            MessageBox.Show("Connessione ad Internet NON Attiva !" + vbCrLf + "Impossibile Aggiornare HelpAssistenza", "HelpAssistenza - AutoUpdate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub tmrUPDPKGOffline_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUPDPKGOffline.Tick
        ControlloOffline(frmConfigurazione.txtSitoTeleControllo.Text, frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text, frmConfigurazione.txtPathTeleControllo.Text)
    End Sub

    Private Sub InviaCartellaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InviaCartellaToolStripMenuItem.Click
        If Me.CartellaInvio.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If My.Computer.FileSystem.FileExists("UploadDirectory.zip") Then My.Computer.FileSystem.DeleteFile("UploadDirectory.zip")
            Using zip As Ionic.Zip.ZipFile = New Ionic.Zip.ZipFile
                zip.AddDirectory(Me.CartellaInvio.SelectedPath)
                zip.Save("UploadDirectory.zip")
            End Using
            'Upload del file su FTP
            FileTransefer = 1
            UploadFileTransf = "UploadDirectory.zip"
            SetTemporaneiUp()
            frmUploadFile.Show()
        End If

    End Sub

    Private Sub SystemIdleTimer1_OnEnterIdleState(ByVal sender As System.Object, ByVal e As HelpAssistenza.IdleEventArgs) Handles SystemIdleTimer1.OnEnterIdleState
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            SystemIdleTimer1.Stop()
            If ListenMode <> True And Operatore = 0 Then
                Try
                    ModuloUpdate.CheckForUpdatesSilent("http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.txt", "http://www.pollerosoftware.com/HelpAssistenza/update/helpassistenza.zip", "", True, "Una Nuova Versione di HelpAssistenza è disponibile !" + vbCrLf + "Vuoi Scaricarla adesso ?")
                Catch
                End Try
            End If
        End If
    End Sub

    Private Sub btnProjector1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjector1.Click
        frmProjector.indirizzo = frmConfigurazione.txtProjhost1.Text
        frmProjector.RemoteDesktop1.VncPort = Convert.ToInt64(frmConfigurazione.txtportaproj1.Text)
        frmProjector.Show()
    End Sub

    Private Sub btnProjector2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProjector2.Click
        frmProjector.indirizzo = frmConfigurazione.txtProjhost2.Text
        frmProjector.RemoteDesktop1.VncPort = Convert.ToInt64(frmConfigurazione.txtportaproj2.Text)
        frmProjector.Show()
    End Sub

    Private Sub EMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EMailToolStripMenuItem.Click
        Select Case NumLinea
            Case 1
                frmInvioMail.TOMAIL = frmConfigurazione.txtnoteOPE1.Text
            Case 2
                frmInvioMail.TOMAIL = frmConfigurazione.txtnoteOPE2.Text
            Case 3
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE3.Text
            Case 4
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE4.Text
            Case 5
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE5.Text
            Case 6
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE6.Text
            Case 7
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE7.Text
            Case 8
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE8.Text
            Case 9
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE9.Text
            Case 10
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE10.Text
            Case 11
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE11.Text
            Case 12
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE12.Text
            Case 13
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE13.Text
            Case 14
                frmInvioMail.TOMAIL = frmConfigurazione.txtNoteOPE14.Text
        End Select
        frmInvioMail.ShowDialog()
        NumLinea = 0
    End Sub

    Private Sub ToolStripComboBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.Click
        If frmConfigurazione.chklistdownweb.Checked = True And frmConfigurazione.chkAbilitaDownloadFile.Checked = True Then
            'Tento il List dei file su FTP TeleControllo
            Try
                Me.ToolStripComboBox1.Enabled = True
                Dim req As FtpWebRequest = FtpWebRequest.Create("ftp://" + frmConfigurazione.txtSitoTeleControllo.Text + frmConfigurazione.txtPathTeleControllo.Text + "download/")
                req.Credentials = New NetworkCredential(frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text)
                req.Method = WebRequestMethods.Ftp.ListDirectory
                ToolStripComboBox1.Items.Clear()
                ToolStripComboBox1.Items.Add("DownloadFile")
                ToolStripComboBox1.SelectedIndex = 0
                Dim sr As New IO.StreamReader(req.GetResponse().GetResponseStream())
                Dim str As String = sr.ReadLine()

                While Not str Is Nothing
                    ToolStripComboBox1.Items.Add(str)
                    str = sr.ReadLine()
                End While

                sr.Close()
                sr.Dispose()
                sr = Nothing
                req = Nothing
            Catch ex As Exception
                Me.ToolStripComboBox1.Enabled = False
            End Try
        ElseIf frmConfigurazione.chkAbilitaDownloadFile.Checked = True Then
            'Tento List Server Appoggio Alternativo
            Try
                Me.ToolStripComboBox1.Enabled = True
                Dim req As FtpWebRequest = FtpWebRequest.Create("ftp://" + frmConfigurazione.txtServerUP.Text + frmConfigurazione.txtpathServerUp.Text)
                req.Credentials = New NetworkCredential(frmConfigurazione.txtUserNameServerUp.Text, frmConfigurazione.txtPasswordServerUp.Text)
                req.Method = WebRequestMethods.Ftp.ListDirectory
                ToolStripComboBox1.Items.Clear()
                ToolStripComboBox1.Items.Add("DownloadFile")
                ToolStripComboBox1.SelectedIndex = 0
                Dim sr As New IO.StreamReader(req.GetResponse().GetResponseStream())
                Dim str As String = sr.ReadLine()

                While Not str Is Nothing
                    ToolStripComboBox1.Items.Add(str)
                    str = sr.ReadLine()
                End While

                sr.Close()
                sr.Dispose()
                sr = Nothing
                req = Nothing
            Catch ex As Exception
                Me.ToolStripComboBox1.Enabled = False
            End Try
        End If
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        If ToolStripComboBox1.SelectedIndex <> 0 Then
            If frmConfigurazione.cmbdownloadconn.SelectedIndex = 0 And frmConfigurazione.chklistdownweb.Checked = True Then
                Dim FilesulServer As String = frmConfigurazione.txtSitoTeleControllo.Text + frmConfigurazione.txtPathTeleControllo.Text + "download/" + ToolStripComboBox1.SelectedItem.ToString
                If FilesulServer.StartsWith("ftp.") Then FilesulServer = FilesulServer.Replace("ftp.", "www.")
                FilesulServer = "http://" + FilesulServer
                Dim DownloadF As New frmDownloadWeb
                DownloadF.txtFileName.Text = FilesulServer
                DownloadF.Show()
                DownloadF.btnDownload.PerformClick()
                Exit Sub

            ElseIf frmConfigurazione.cmbdownloadconn.SelectedIndex = 0 And frmConfigurazione.chklistdownweb.Checked = False And frmConfigurazione.txtServerUP.Text <> String.Empty Then
                Dim FilesulServer As String = frmConfigurazione.txtServerWeb.Text + frmConfigurazione.txtpathServerUp.Text + ToolStripComboBox1.SelectedItem.ToString
                If FilesulServer.StartsWith("ftp.") Then FilesulServer = FilesulServer.Replace("ftp.", "www.")
                FilesulServer = "http://" + FilesulServer
                Dim DownloadF As New frmDownloadWeb
                DownloadF.txtFileName.Text = FilesulServer
                DownloadF.Show()
                DownloadF.btnDownload.PerformClick()
                Exit Sub

            ElseIf frmConfigurazione.cmbdownloadconn.SelectedIndex = 1 And frmConfigurazione.chklistdownweb.Checked = True Then
                Dim FilesulServer As String = frmConfigurazione.txtSitoTeleControllo.Text + frmConfigurazione.txtPathTeleControllo.Text + "download/" + ToolStripComboBox1.SelectedItem.ToString
                FilesulServer = "ftp://" + FilesulServer
                Dim salvain As New FolderBrowserDialog
                salvain.RootFolder = Environment.SpecialFolder.Desktop
                If Not salvain.ShowDialog = Windows.Forms.DialogResult.OK Then Exit Sub
                Dim downFTP As New frmUploadFile
                downFTP.DownloadviFTP = True
                downFTP.Show()
                downFTP.BackgroundDownloadFile.WorkerReportsProgress = True
                If frmConfigurazione.chkdezip.Checked = True Then
                    downFTP.BackgroundDownloadFile.RunWorkerAsync(New Object() {salvain.SelectedPath + "\" + ToolStripComboBox1.SelectedItem.ToString, FilesulServer, frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text, True})
                Else
                    downFTP.BackgroundDownloadFile.RunWorkerAsync(New Object() {salvain.SelectedPath + "\" + ToolStripComboBox1.SelectedItem.ToString, FilesulServer, frmConfigurazione.txtUserTeleControllo.Text, frmConfigurazione.txtPassTeleControllo.Text, False})
                End If
                Exit Sub

            ElseIf frmConfigurazione.cmbdownloadconn.SelectedIndex = 1 And frmConfigurazione.chklistdownweb.Checked = False Then
                Dim FilesulServer As String = frmConfigurazione.txtServerUP.Text + frmConfigurazione.txtpathServerUp.Text + ToolStripComboBox1.SelectedItem.ToString
                FilesulServer = "ftp://" + FilesulServer
                Dim salvain As New FolderBrowserDialog
                salvain.RootFolder = Environment.SpecialFolder.Desktop
                If Not salvain.ShowDialog = Windows.Forms.DialogResult.OK Then Exit Sub
                Dim downFTP As New frmUploadFile
                downFTP.DownloadviFTP = True
                downFTP.Show()
                downFTP.BackgroundDownloadFile.WorkerReportsProgress = True
                If frmConfigurazione.chkdezip.Checked = True Then
                    downFTP.BackgroundDownloadFile.RunWorkerAsync(New Object() {salvain.SelectedPath + "\" + ToolStripComboBox1.SelectedItem.ToString, FilesulServer, frmConfigurazione.txtUserNameServerUp.Text, frmConfigurazione.txtPasswordServerUp.Text, True})
                Else
                    downFTP.BackgroundDownloadFile.RunWorkerAsync(New Object() {salvain.SelectedPath + "\" + ToolStripComboBox1.SelectedItem.ToString, FilesulServer, frmConfigurazione.txtUserNameServerUp.Text, frmConfigurazione.txtPasswordServerUp.Text, False})
                End If
                Exit Sub
            End If

        End If
    End Sub

    Private Sub DownloadFileToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadFileToolStripMenuItem1.Click
        Dim DownloadManager As New frmDownloadWeb
        DownloadManager.Show()
    End Sub

    Private Sub TimerErrTeleControllo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerErrTeleControllo.Tick
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success And ListenMode = True Then
            TimerFTP.Enabled = True
            TimerErrTeleControllo.Enabled = False
        End If
    End Sub

    Private Sub btncollalt2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncollalt2.MouseHover
        Dim Tip As New ToolTip
        Tip.IsBalloon = True
        Tip.ToolTipIcon = ToolTipIcon.Info
        Tip.ToolTipTitle = "Teleassistenza " + frmConfigurazione.txtnomeazienda.Text
        Tip.SetToolTip(Me.btncollalt2, "Apri " + frmConfigurazione.txtnmealtro.Text)
    End Sub

    
    Private Sub CatturaSchermoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CatturaSchermoToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(150)
        PrintScreen.CaptureScreen()
        Dim pcscreen As New PictureBox
        pcscreen.Image = PrintScreen.Schermata
        If NotifyHelp.Visible = True Then Me.Show()
        Me.WindowState = FormWindowState.Normal
        Dim Salvain As New SaveFileDialog
        Salvain.AddExtension = True
        If Not My.Computer.Info.OSFullName.Contains("Server") Then If My.Computer.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.MyPictures) Then Salvain.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        Salvain.Title = "HelpAssistenza - Salvataggio Print Screen"
        Salvain.Filter = "File Immagine JPG (*.jpg)|*.jpg | File Immagine PNG (*.png)|*.png | File Immagine BMP (*.bmp)|*.bmp"
        Salvain.DefaultExt = "jpg"
        Salvain.FileName = "Help_PrintScreen"
        If Salvain.ShowDialog = Windows.Forms.DialogResult.OK Then
            pcscreen.Image.Save(Salvain.FileName)
        End If
    End Sub

    Private Sub EMailToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EMailToolStripMenuItem1.Click
        btnMail.PerformClick()
    End Sub

    Private Sub MioIPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MioIPToolStripMenuItem.Click
        btnmioip.PerformClick()
    End Sub

    Private Sub TelefonaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TelefonaToolStripMenuItem.Click
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles + "\Skype\Phone\Skype.exe") And frmConfigurazione.txttelefonoAzienda.Text <> String.Empty Then
            Dim numTelefono As String = frmConfigurazione.txttelefonoAzienda.Text
            numTelefono = Replace(numTelefono, " ", vbNullString)
            Try
                System.Diagnostics.Process.Start(My.Computer.FileSystem.SpecialDirectories.ProgramFiles + "\Skype\Phone\Skype.exe", "/callto:" + numTelefono)
            Catch err As Exception
                MessageBox.Show("Errore Chiamata ! " + err.Message, "HelpAssistenza - Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
                If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Chiamata ! " + err.Message)
            End Try
        Else
            MessageBox.Show("Errore Chiamata ! " + vbCrLf + "Impossibile trovare Skype !", "HelpAssistenza - Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Chiamata ! Impossibile trovare Skype !")
        End If
    End Sub

    Private Sub btnProjector1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProjector1.MouseHover
        Dim tip As New ToolTip
        tip.IsBalloon = True
        tip.ToolTipIcon = ToolTipIcon.Info
        tip.ToolTipTitle = "Projector"
        tip.SetToolTip(Me.btnProjector1, "Visualizza Proiezione Remota")
    End Sub

    Private Sub btnProjector2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProjector2.MouseHover
        Dim tip As New ToolTip
        tip.IsBalloon = True
        tip.ToolTipIcon = ToolTipIcon.Info
        tip.ToolTipTitle = "Projector"
        tip.SetToolTip(Me.btnProjector1, "Visualizza Proiezione Remota")
    End Sub

    Private Sub UtilityToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles UtilityToolStripMenuItem.DropDownOpening
        If frmConfigurazione.txttelefonoAzienda.Text <> String.Empty And frmConfigurazione.txttelefonoAzienda.Text <> "+        " Then
            Me.TelefonaToolStripMenuItem.Visible = True
            If frmConfigurazione.txtnomeazienda.Text <> String.Empty Then TelefonaToolStripMenuItem.ToolTipText = "Telefona a " + frmConfigurazione.txtnomeazienda.Text + " mediante Skype"
        Else
            Me.TelefonaToolStripMenuItem.Visible = False
        End If
    End Sub
End Class
