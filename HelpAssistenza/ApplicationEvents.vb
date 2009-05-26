Imports Microsoft.VisualBasic.Devices
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    Class MyApplication
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            e.BringToForeground = True
            ControllaTipListen()
            If (frmAssistenza.Enabled = True Or ListenMode = True) OrElse (frmAssistenza.Operatore <> 0) Then
                frmAssistenza.NotifyHelp.Visible = True
            Else
                frmAssistenza.NotifyHelp.Visible = False
            End If
            frmAssistenza.abilitaChiudiToolStr()
            frmAssistenza.Select()
            frmAssistenza.Show()
            frmAssistenza.WindowState = FormWindowState.Normal
        End Sub
    End Class

End Namespace
