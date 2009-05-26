<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDownloadWeb
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDownloadWeb))
        Me.lblfile = New System.Windows.Forms.Label
        Me.lblvelocità = New System.Windows.Forms.Label
        Me.lbldimensionefile = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.btnDownload = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.lblsalvain = New System.Windows.Forms.Label
        Me.lblscaricati = New System.Windows.Forms.Label
        Me.TimerDeZip = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblfile
        '
        Me.lblfile.AutoSize = True
        Me.lblfile.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfile.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lblfile.Location = New System.Drawing.Point(12, 18)
        Me.lblfile.Name = "lblfile"
        Me.lblfile.Size = New System.Drawing.Size(39, 15)
        Me.lblfile.TabIndex = 0
        Me.lblfile.Text = "File  :"
        '
        'lblvelocità
        '
        Me.lblvelocità.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvelocità.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lblvelocità.Location = New System.Drawing.Point(304, 56)
        Me.lblvelocità.Name = "lblvelocità"
        Me.lblvelocità.Size = New System.Drawing.Size(157, 13)
        Me.lblvelocità.TabIndex = 1
        Me.lblvelocità.Text = "Velocità  :"
        '
        'lbldimensionefile
        '
        Me.lbldimensionefile.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldimensionefile.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lbldimensionefile.Location = New System.Drawing.Point(9, 56)
        Me.lbldimensionefile.Name = "lbldimensionefile"
        Me.lbldimensionefile.Size = New System.Drawing.Size(171, 13)
        Me.lbldimensionefile.TabIndex = 2
        Me.lbldimensionefile.Text = "Dimensione File :"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 72)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(452, 66)
        Me.ProgressBar1.TabIndex = 3
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(50, 15)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(315, 20)
        Me.txtFileName.TabIndex = 4
        '
        'btnDownload
        '
        Me.btnDownload.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownload.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnDownload.Location = New System.Drawing.Point(371, 13)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(93, 23)
        Me.btnDownload.TabIndex = 5
        Me.btnDownload.Text = "Download"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Title = "Dove Vuoi Salvare il file ?"
        '
        'lblsalvain
        '
        Me.lblsalvain.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsalvain.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.lblsalvain.Location = New System.Drawing.Point(9, 39)
        Me.lblsalvain.Name = "lblsalvain"
        Me.lblsalvain.Size = New System.Drawing.Size(452, 17)
        Me.lblsalvain.TabIndex = 8
        Me.lblsalvain.Text = "Salva in :"
        '
        'lblscaricati
        '
        Me.lblscaricati.BackColor = System.Drawing.Color.Transparent
        Me.lblscaricati.Font = New System.Drawing.Font("MS Reference Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblscaricati.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblscaricati.Location = New System.Drawing.Point(9, 141)
        Me.lblscaricati.Name = "lblscaricati"
        Me.lblscaricati.Size = New System.Drawing.Size(455, 13)
        Me.lblscaricati.TabIndex = 10
        Me.lblscaricati.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TimerDeZip
        '
        Me.TimerDeZip.Interval = 800
        '
        'frmDownloadWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(477, 157)
        Me.Controls.Add(Me.lblscaricati)
        Me.Controls.Add(Me.lblsalvain)
        Me.Controls.Add(Me.btnDownload)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lbldimensionefile)
        Me.Controls.Add(Me.lblvelocità)
        Me.Controls.Add(Me.lblfile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDownloadWeb"
        Me.Text = "HelpAssistenza - Web Download File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblfile As System.Windows.Forms.Label
    Friend WithEvents lblvelocità As System.Windows.Forms.Label
    Friend WithEvents lbldimensionefile As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblsalvain As System.Windows.Forms.Label
    Friend WithEvents lblscaricati As System.Windows.Forms.Label
    Friend WithEvents TimerDeZip As System.Windows.Forms.Timer

End Class
