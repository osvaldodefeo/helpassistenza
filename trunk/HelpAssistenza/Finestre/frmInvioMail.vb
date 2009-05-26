Imports System.Net.Mail

Public Class frmInvioMail
    Private imail As String
    Public Property TOMAIL() As String
        Get
            Return imail
        End Get
        Set(ByVal value As String)
            imail = value
        End Set
    End Property
    Private Sub CancellaFileps()
        Try
            If System.IO.File.Exists(Application.StartupPath + "\psmail.jpg") Then System.IO.File.Delete(Application.StartupPath + "\psmail.jpg")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInvia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvia.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            Composizione_Messaggio()
            SmtpServer.Host = frmConfigurazione.txtServerSMTP.Text
            If frmConfigurazione.txtportaSMTP.Text <> String.Empty And frmConfigurazione.txtportaSMTP.Text <> "25" Then SmtpServer.Port = Convert.ToInt64(frmConfigurazione.txtportaSMTP.Text)
            SmtpServer.EnableSsl = frmConfigurazione.chkSSL.Checked
            If frmConfigurazione.txtuserSMTP.Text <> String.Empty And frmConfigurazione.txtpassSMTP.Text <> String.Empty Then
                SmtpServer.Credentials = New Net.NetworkCredential(frmConfigurazione.txtuserSMTP.Text, frmConfigurazione.txtpassSMTP.Text)
            End If
            mail = New MailMessage()
            mail.From = New MailAddress(frmConfigurazione.vtxtmailmittente.Text, frmConfigurazione.txtnomemittente.Text, System.Text.Encoding.UTF8)
            mail.To.Add(Me.TOMAIL)
            mail.Subject = Me.txtoggetto.Text
            mail.Body = Me.MessaggioMail
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            If System.IO.File.Exists(Application.StartupPath + "\psmail.jpg") Then mail.Attachments.Add(New Attachment(Application.StartupPath + "\psmail.jpg"))
            SmtpServer.Send(mail)
            mail.Dispose()
            CancellaFileps()
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Dim tttip As New ToolTip
            tttip.UseAnimation = True
            tttip.BackColor = Color.AntiqueWhite
            tttip.AutoPopDelay = 25000
            tttip.ToolTipTitle = "HelpAssistenza - Invio eMail"
            tttip.ToolTipIcon = ToolTipIcon.Error
            tttip.ForeColor = Color.OrangeRed
            If frmConfigurazione.txttelefonoAzienda.Text = String.Empty Then
                tttip.Show("Errore Invio eMail !", Me.btnInvia)
                If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Invio eMail : " + ex.Message)
            Else
                tttip.Show("Errore Invio eMail !" + vbCrLf + "Solitamente è possibile Richiedere Assistenza anche Telefonando al numero : " + frmConfigurazione.txttelefonoAzienda.Text, Me.btnInvia)
                If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Errore Invio eMail : " + ex.Message)
            End If
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmInvioMail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CancellaFileps()
    End Sub

    Private Sub frmInvioMail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub frmInvioMail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arrTecnici(13) As String
        arrTecnici(0) = frmConfigurazione.txtnomeOPE1.Text + " " + frmConfigurazione.txtCognomeOPE1.Text
        arrTecnici(1) = frmConfigurazione.txtNomeOPE2.Text + " " + frmConfigurazione.txtCognomeOPE2.Text
        arrTecnici(2) = frmConfigurazione.txtNomeOPE3.Text + " " + frmConfigurazione.txtCognomeOPE3.Text
        arrTecnici(3) = frmConfigurazione.txtNomeOPE4.Text + " " + frmConfigurazione.txtCognomeOPE4.Text
        arrTecnici(4) = frmConfigurazione.txtNomeOPE5.Text + " " + frmConfigurazione.txtCognomeOPE5.Text
        arrTecnici(5) = frmConfigurazione.txtNomeOPE6.Text + " " + frmConfigurazione.txtCognomeOPE6.Text
        arrTecnici(6) = frmConfigurazione.txtNomeOPE7.Text + " " + frmConfigurazione.txtCognomeOPE7.Text
        arrTecnici(7) = frmConfigurazione.txtNomeOPE8.Text + " " + frmConfigurazione.txtCognomeOPE9.Text
        arrTecnici(8) = frmConfigurazione.txtNomeOPE9.Text + " " + frmConfigurazione.txtCognomeOPE9.Text
        arrTecnici(9) = frmConfigurazione.txtNomeOPE10.Text + " " + frmConfigurazione.txtCognomeOPE10.Text
        arrTecnici(10) = frmConfigurazione.txtNomeOPE11.Text + " " + frmConfigurazione.txtCognomeOPE11.Text
        arrTecnici(11) = frmConfigurazione.txtNomeOPE12.Text + " " + frmConfigurazione.txtCognomeOPE12.Text
        arrTecnici(12) = frmConfigurazione.txtNomeOPE13.Text + " " + frmConfigurazione.txtCognomeOPE13.Text
        arrTecnici(13) = frmConfigurazione.txtNomeOPE14.Text + " " + frmConfigurazione.txtCognomeOPE14.Text

        If frmConfigurazione.txtnomeazienda.Text <> String.Empty Then Me.cmbTecnico.Items(0) = "Azienda - " + frmConfigurazione.txtnomeazienda.Text

        Me.cmbTecnico.SelectedIndex = 0
        If frmConfigurazione.cmbnumlinee.Text <> String.Empty Then
            Me.cmbTecnico.Items.Clear()
            If frmConfigurazione.txtnomeazienda.Text <> String.Empty Then
                Me.cmbTecnico.Items.Add("Azienda - " + frmConfigurazione.txtnomeazienda.Text)
            Else
                Me.cmbTecnico.Items.Add("Azienda Assistente")
            End If
            For i As Integer = 1 To frmConfigurazione.cmbnumlinee.Text
                Me.cmbTecnico.Items.Add(i & " - " & arrTecnici((i) - 1))
            Next
            If frmAssistenza.NumLinea <> 0 Then
                Me.cmbTecnico.SelectedIndex = frmAssistenza.NumLinea
                CaseOperatore()
            Else
                Me.cmbTecnico.SelectedIndex = 0
            End If
        End If
        CancellaFileps()
        Me.pcprintscreen.Enabled = True
    End Sub

    Private Sub cmbTecnico_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTecnico.SelectedIndexChanged
        CaseOperatore()
    End Sub

    Private Sub CaseOperatore()
        Select Case Me.cmbTecnico.SelectedIndex
            Case 0
                Me.TOMAIL = frmConfigurazione.vtxtemailaz.Text
            Case 1
                Me.TOMAIL = frmConfigurazione.txtnoteOPE1.Text
            Case 2
                Me.TOMAIL = frmConfigurazione.txtnoteOPE2.Text
            Case 3
                Me.TOMAIL = frmConfigurazione.txtnoteOPE3.Text
            Case 4
                Me.TOMAIL = frmConfigurazione.txtnoteOPE4.Text
            Case 5
                Me.TOMAIL = frmConfigurazione.txtnoteOPE5.Text
            Case 6
                Me.TOMAIL = frmConfigurazione.txtnoteOPE6.Text
            Case 7
                Me.TOMAIL = frmConfigurazione.txtnoteOPE7.Text
            Case 8
                Me.TOMAIL = frmConfigurazione.txtnoteOPE8.Text
            Case 9
                Me.TOMAIL = frmConfigurazione.txtnoteOPE9.Text
            Case 10
                Me.TOMAIL = frmConfigurazione.txtnoteOPE10.Text
            Case 11
                Me.TOMAIL = frmConfigurazione.txtnoteOPE11.Text
            Case 12
                Me.TOMAIL = frmConfigurazione.txtnoteOPE12.Text
            Case 13
                Me.TOMAIL = frmConfigurazione.txtnoteOPE13.Text
            Case 14
                Me.TOMAIL = frmConfigurazione.txtnoteOPE14.Text
            Case Else
                Me.TOMAIL = frmConfigurazione.vtxtemailaz.Text
        End Select
    End Sub
    Private Sub Composizione_Messaggio()
        If frmConfigurazione.chkinfoPC.Checked = True Then
            Me.MessaggioMail = "Cliente : " + frmConfigurazione.txtnomemittente.Text + "     " + "Tipo Problema : " + Me.cmbtipoProblema.SelectedItem + _
            vbCrLf + _
            "Utente : " + My.User.Name + "     " + "Nome PC : " + My.Computer.Name + "     " + "Sistema Operativo : " + My.Computer.Info.OSFullName + _
            vbCrLf + vbCrLf + vbCrLf + _
            "Messaggio : " + vbCrLf + vbCrLf + Me.txtMessaggio.Text
        Else
            Me.MessaggioMail = "Cliente : " + frmConfigurazione.txtnomemittente.Text + "     " + "Tipo Problema : " + Me.cmbtipoProblema.SelectedItem + vbCrLf + vbCrLf + "Messaggio : " + vbCrLf + Me.txtMessaggio.Text
        End If

    End Sub
    Private mess As String
    Public Property MessaggioMail() As String
        Get
            Return mess
        End Get
        Set(ByVal value As String)
            mess = value
        End Set
    End Property

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pcprintscreen.Click
        Me.WindowState = FormWindowState.Minimized
        frmAssistenza.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(150)
        PrintScreen.CaptureScreen()
        Dim pcscreen As New PictureBox
        pcscreen.Image = PrintScreen.Schermata
        Dim tmpprintScreen As String = Application.StartupPath + "\psmail.jpg"
        pcscreen.Image.Save(tmpprintScreen)
        Me.pcprintscreen.Enabled = False
        frmAssistenza.WindowState = FormWindowState.Normal
        Me.WindowState = FormWindowState.Normal
    End Sub
End Class