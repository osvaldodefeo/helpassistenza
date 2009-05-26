Public Class frmProjector
    Public indirizzo As String
    Dim ChiusuraUtente As Boolean = False
    Private Declare Function SetWindowPos Lib "user32.dll" Alias "SetWindowPos" (ByVal hWnd As IntPtr, ByVal hWndIntertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
    Private Declare Function GetSystemMetrics Lib "user32.dll" Alias "GetSystemMetrics" (ByVal Which As Integer) As Integer
    Private Const SM_CXSCREEN As Integer = 0
    Private Const SM_CYSCREEN As Integer = 1
    Private Shared HWND_TOP As IntPtr = IntPtr.Zero
    Private Const SWP_SHOWWINDOW As Integer = 64
    Public ReadOnly Property ScreenX() As Integer
        Get
            Return GetSystemMetrics(SM_CXSCREEN)
        End Get
    End Property
    Public ReadOnly Property ScreenY() As Integer
        Get
            Return GetSystemMetrics(SM_CYSCREEN)
        End Get
    End Property
    Private Sub FullScreen()
        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = True
        SetWindowPos(Me.Handle, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW)
    End Sub

    Private Sub frmProjector_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then ChiusuraUtente = True
        If (RemoteDesktop1.IsConnected) Then
            RemoteDesktop1.Disconnect()
        End If
    End Sub
    Private Sub frmProjector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            RemoteDesktop1.Connect(indirizzo, True, True)
            If frmConfigurazione.chkprojFullScreen.Checked Then
                FullScreen()
                FullscreenToolStripMenuItem.Text = "Modalità Finestra"
            End If
            Timer1.Enabled = True
        Catch eroe As Exception
            MessageBox.Show("Errore Connessione ! " + vbCrLf + eroe.Message, "HelpAssistenza - Projector", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Me.Dispose()
        End Try
    End Sub

    Private Sub RemoteDesktop1_ConnectComplete(ByVal sender As Object, ByVal e As VncSharp.ConnectEventArgs) Handles RemoteDesktop1.ConnectComplete
        ClientSize = New Size(e.DesktopWidth, e.DesktopHeight)
        RemoteDesktop1.Focus()
    End Sub

    Private Sub RemoteDesktop1_ConnectionLost(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoteDesktop1.ConnectionLost
        If ChiusuraUtente <> True Then MessageBox.Show(Me, _
                            "Connessione Interrotta !", _
                            "HelpAssistenza - Projector", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If RemoteDesktop1.IsConnected = False Then
            Me.Close()
        End If
    End Sub

    Private Sub RemoteDesktop1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RemoteDesktop1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub

    Private Sub EsciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EsciToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub FullscreenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FullscreenToolStripMenuItem.Click
        If FullscreenToolStripMenuItem.Text = "Fullscreen" Then
            FullScreen()
            FullscreenToolStripMenuItem.Text = "Modalità Finestra"
        Else
            Me.WindowState = FormWindowState.Normal
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.TopMost = False
            FullscreenToolStripMenuItem.Text = "Fullscreen"
        End If

    End Sub
End Class