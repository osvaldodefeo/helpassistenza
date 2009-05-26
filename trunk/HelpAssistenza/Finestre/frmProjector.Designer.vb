<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjector
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjector))
        Me.RemoteDesktop1 = New VncSharp.RemoteDesktop
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EsciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FullscreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RemoteDesktop1
        '
        Me.RemoteDesktop1.AutoScroll = True
        Me.RemoteDesktop1.AutoScrollMinSize = New System.Drawing.Size(608, 427)
        Me.RemoteDesktop1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RemoteDesktop1.Location = New System.Drawing.Point(0, 0)
        Me.RemoteDesktop1.Name = "RemoteDesktop1"
        Me.RemoteDesktop1.Size = New System.Drawing.Size(803, 535)
        Me.RemoteDesktop1.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullscreenToolStripMenuItem, Me.EsciToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 70)
        '
        'EsciToolStripMenuItem
        '
        Me.EsciToolStripMenuItem.Name = "EsciToolStripMenuItem"
        Me.EsciToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EsciToolStripMenuItem.Text = "Esci"
        '
        'FullscreenToolStripMenuItem
        '
        Me.FullscreenToolStripMenuItem.Name = "FullscreenToolStripMenuItem"
        Me.FullscreenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.FullscreenToolStripMenuItem.Text = "Fullscreen"
        '
        'frmProjector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(803, 535)
        Me.Controls.Add(Me.RemoteDesktop1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmProjector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "HelpAssistenza - Projector"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RemoteDesktop1 As VncSharp.RemoteDesktop
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EsciToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FullscreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
