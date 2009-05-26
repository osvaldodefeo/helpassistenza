Imports System.Windows.Forms

Public Class mioip

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub mioip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim WC As New System.Net.WebClient
        Try
            lblextip.Text = System.Text.Encoding.ASCII.GetString((WC.DownloadData("http://whatismyip.com/automation/n09230945.asp")))
            WC.Dispose()
        Catch
            MsgBox("Impossibile recuperare Indirizzo IP Pubblico!" & vbCrLf & "Controllare lo stato della connessione ad Internet.", MsgBoxStyle.Critical, Title:="Help Assistenza - Errore")
        Finally
            lbliplan.Text = System.Net.Dns.GetHostAddresses(My.Computer.Name)(0).ToString
            lblcomputername.Text = My.Computer.Name
        End Try
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        Me.Close()
    End Sub

    Private Sub lblextip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblextip.Click
        System.Diagnostics.Process.Start("http://www.mio-ip.it")
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        System.Diagnostics.Process.Start("http://www.mio-ip.it")
    End Sub
End Class
