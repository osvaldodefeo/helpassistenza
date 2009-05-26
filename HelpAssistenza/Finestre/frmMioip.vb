Imports System.Windows.Forms
Imports System.Net
Imports System.Net.NetworkInformation


Public Class frmMioip
    Dim imgerrCon As Boolean = True
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frmMioip_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            Me.Dispose()
        End If
    End Sub

    Private Sub mioip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim WC As New System.Net.WebClient
        Dim ping As Net.NetworkInformation.Ping = New Net.NetworkInformation.Ping()
        Dim pingreply As Net.NetworkInformation.PingReply = ping.Send(IPAddrTestConnection)
        Timerimgcon.Enabled = False
        If pingreply.Status = Net.NetworkInformation.IPStatus.Success Then
            Try
                lblextip.Text = System.Text.Encoding.ASCII.GetString((WC.DownloadData("http://whatismyip.com/automation/n09230945.asp")))
                WC.Dispose()
            Catch
                TooltipErroreConnessione()
            Finally
                lbliplan.Text = System.Net.Dns.GetHostAddresses(My.Computer.Name)(0).ToString
                lblcomputername.Text = My.Computer.Name
                Dim sHostName As String
                Dim i As Integer
                sHostName = Dns.GetHostName()
                Dim ipE As IPHostEntry = Dns.GetHostByName(sHostName)
                Dim IpA() As IPAddress = ipE.AddressList
                Dim ip As String
                For i = 0 To IpA.GetUpperBound(0)
                    ip += vbCrLf + IpA(i).ToString
                Next
                ToolIP.SetToolTip(Me.lbliplan, ip)
                If My.Computer.Info.OSFullName.Contains("Vista") OrElse My.Computer.Info.OSFullName.Contains("2008") OrElse My.Computer.Info.OSFullName.Contains("Windows 7") Then lbliplan.Text = IpA(0).ToString
            End Try
        Else
            lbliplan.Text = System.Net.Dns.GetHostAddresses(My.Computer.Name)(0).ToString
            lblcomputername.Text = My.Computer.Name
            Dim sHostName As String
            Dim i As Integer
            sHostName = Dns.GetHostName()
            Dim ipE As IPHostEntry = Dns.GetHostByName(sHostName)
            Dim IpA() As IPAddress = ipE.AddressList
            Dim ip As String
            For i = 0 To IpA.GetUpperBound(0)
                ip += vbCrLf + IpA(i).ToString
            Next
            ToolIP.SetToolTip(Me.lbliplan, ip)
            If My.Computer.Info.OSFullName.Contains("Vista") OrElse My.Computer.Info.OSFullName.Contains("2008") OrElse My.Computer.Info.OSFullName.Contains("Windows 7") Then lbliplan.Text = IpA(0).ToString
            TooltipErroreConnessione()
            Timerimgcon.Enabled = True
        End If
        TooltipISPInfo()
        TooltipPC()
        frmAssistenza.Cursor = Cursors.Default
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        frmAssistenza.Cursor = Cursors.Default
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub lblextip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblextip.Click
        System.Diagnostics.Process.Start("http://www.mio-ip.it")
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        System.Diagnostics.Process.Start("http://www.mio-ip.it")
    End Sub
    Private Sub TooltipISPInfo()
        If lblextip.Text <> String.Empty Then
            Try
                Dim info As New IPLookup(lblextip.Text)
                Dim toooltip As New ToolTip
                toooltip.IsBalloon = True
                toooltip.ToolTipTitle = "Internet Provaider Info"
                toooltip.SetToolTip(Me.lblextip, "IP       : " + info.IP + vbCrLf + "ISP     : " + info.ISPDescription + vbCrLf + "Paese : " + info.Country)
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub TooltipPC()
        Dim toltippc As New ToolTip
        toltippc.IsBalloon = True
        toltippc.ToolTipTitle = "Computer Info"
        toltippc.SetToolTip(lblcomputername, "Nome : " + My.Computer.Name + vbCrLf + "S.O.   : " + My.Computer.Info.OSFullName)
    End Sub
    Private Sub TooltipErroreConnessione()
        Dim titp As New ToolTip
        titp.ToolTipIcon = ToolTipIcon.Error
        titp.ForeColor = Color.OrangeRed
        titp.BackColor = Color.Azure
        titp.ToolTipTitle = "HelpAssistenza - Errore Connessione"
        titp.AutoPopDelay = 25000
        titp.SetToolTip(Me, "Impossibile recuperare Indirizzo IP Pubblico!" & vbCrLf & "Controllare lo stato della connessione ad Internet!")
        titp.SetToolTip(Me.PictureBox1, "Impossibile recuperare Indirizzo IP Pubblico!" & vbCrLf & "Controllare lo stato della connessione ad Internet!")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timerimgcon.Tick
        If imgerrCon = True Then
            PictureBox1.Image = My.Resources.internet_si
            imgerrCon = False
        Else
            PictureBox1.Image = My.Resources.internet_no
            imgerrCon = True
        End If
    End Sub
End Class
