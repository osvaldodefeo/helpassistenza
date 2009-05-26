<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadAggio
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTipDownload = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(275, 43)
        Me.ProgressBar1.TabIndex = 2
        Me.ToolTipDownload.SetToolTip(Me.ProgressBar1, "Download Aggiornamento in Corso")
        '
        'Timer2
        '
        '
        'ToolTipDownload
        '
        Me.ToolTipDownload.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.ToolTipDownload.IsBalloon = True
        Me.ToolTipDownload.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipDownload.ToolTipTitle = "HelpAssistenza - AutoUpdate"
        '
        'DownloadAggio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 43)
        Me.Controls.Add(Me.ProgressBar1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DownloadAggio"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "HelpAssistenza - AutoUpdate"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ToolTipDownload As System.Windows.Forms.ToolTip
End Class
