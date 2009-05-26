<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUploadFile
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AnnullaUploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lblTrasferimento = New System.Windows.Forms.Label
        Me.ToolTipTrasferimento = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimerControllaFine = New System.Windows.Forms.Timer(Me.components)
        Me.TimerChiudi = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundUploadFile = New System.ComponentModel.BackgroundWorker
        Me.BackgroundDownloadFile = New System.ComponentModel.BackgroundWorker
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(388, 67)
        Me.ProgressBar1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnnullaUploadToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(121, 26)
        '
        'AnnullaUploadToolStripMenuItem
        '
        Me.AnnullaUploadToolStripMenuItem.Name = "AnnullaUploadToolStripMenuItem"
        Me.AnnullaUploadToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.AnnullaUploadToolStripMenuItem.Text = "Annulla"
        '
        'lblTrasferimento
        '
        Me.lblTrasferimento.BackColor = System.Drawing.Color.Transparent
        Me.lblTrasferimento.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblTrasferimento.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrasferimento.Location = New System.Drawing.Point(0, 26)
        Me.lblTrasferimento.Name = "lblTrasferimento"
        Me.lblTrasferimento.Size = New System.Drawing.Size(388, 19)
        Me.lblTrasferimento.TabIndex = 1
        Me.lblTrasferimento.Text = "File :"
        Me.lblTrasferimento.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblTrasferimento.Visible = False
        '
        'ToolTipTrasferimento
        '
        Me.ToolTipTrasferimento.ToolTipTitle = "HelpAssistenza"
        '
        'TimerControllaFine
        '
        Me.TimerControllaFine.Interval = 300
        '
        'TimerChiudi
        '
        Me.TimerChiudi.Interval = 3000
        '
        'BackgroundUploadFile
        '
        Me.BackgroundUploadFile.WorkerSupportsCancellation = True
        '
        'BackgroundDownloadFile
        '
        Me.BackgroundDownloadFile.WorkerReportsProgress = True
        Me.BackgroundDownloadFile.WorkerSupportsCancellation = True
        '
        'frmUploadFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(388, 67)
        Me.Controls.Add(Me.lblTrasferimento)
        Me.Controls.Add(Me.ProgressBar1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmUploadFile"
        Me.Opacity = 0.8
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "HelpAssistenza - Upload File"
        Me.TopMost = True
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTrasferimento As System.Windows.Forms.Label
    Friend WithEvents ToolTipTrasferimento As System.Windows.Forms.ToolTip
    Friend WithEvents TimerControllaFine As System.Windows.Forms.Timer
    Friend WithEvents TimerChiudi As System.Windows.Forms.Timer
    Friend WithEvents BackgroundUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AnnullaUploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundDownloadFile As System.ComponentModel.BackgroundWorker
End Class
