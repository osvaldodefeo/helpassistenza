Public Class Report

    Private Sub RichTextBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBoxRiepilogo.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.RichTextBoxRiepilogo.BulletIndent = False Then
                Me.RichTextBoxRiepilogo.BulletIndent = True
            Else
                Me.RichTextBoxRiepilogo.BulletIndent = False
            End If
        End If
    End Sub

    Private Sub btnFinito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinito.Click
        Me.btnFinito.Visible = False
        Me.RichTextBoxRiepilogo.ReadOnly = True
        Me.BtnOK.Enabled = True
    End Sub

    Private Sub Report_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If frmAssistenza.TimerFTP.Enabled = True OrElse ListenMode = True Then
            Me.Hide()
        Else
            frmAssistenza.NotifyHelp.Visible = False
            End
        End If
    End Sub

    Private Sub Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RichTextBoxRiepilogo.ReadOnly = False
        Me.lblAzienda.Text = frmConfigurazione.txtnomeazienda.Text
        TempoAssistenza.Stop()
        Me.lblDurataAssistenza.Text = TempoAssistenza.Elapsed.Minutes & " Minuti"
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If frmAssistenza.TimerFTP.Enabled = True OrElse ListenMode = True Then
            Me.Hide()
        Else
            Me.Close()
            frmAssistenza.NotifyHelp.Visible = False
            End
        End If
    End Sub
End Class