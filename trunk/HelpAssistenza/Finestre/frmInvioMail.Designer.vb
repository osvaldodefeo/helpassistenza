<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvioMail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvioMail))
        Me.txtMessaggio = New System.Windows.Forms.TextBox
        Me.btnInvia = New System.Windows.Forms.Button
        Me.lblmessaggio = New System.Windows.Forms.Label
        Me.txtoggetto = New System.Windows.Forms.TextBox
        Me.lbloggetto = New System.Windows.Forms.Label
        Me.cmbTecnico = New System.Windows.Forms.ComboBox
        Me.lbltecnico = New System.Windows.Forms.Label
        Me.lblproblema = New System.Windows.Forms.Label
        Me.cmbtipoProblema = New System.Windows.Forms.ComboBox
        Me.lblMail = New System.Windows.Forms.Label
        Me.pcprintscreen = New System.Windows.Forms.PictureBox
        Me.ToolTipCatturaSchermo = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.pcprintscreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMessaggio
        '
        Me.txtMessaggio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessaggio.Location = New System.Drawing.Point(12, 191)
        Me.txtMessaggio.Multiline = True
        Me.txtMessaggio.Name = "txtMessaggio"
        Me.txtMessaggio.Size = New System.Drawing.Size(297, 121)
        Me.txtMessaggio.TabIndex = 0
        '
        'btnInvia
        '
        Me.btnInvia.BackColor = System.Drawing.Color.Transparent
        Me.btnInvia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnInvia.Font = New System.Drawing.Font("MS Reference Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInvia.Location = New System.Drawing.Point(234, 318)
        Me.btnInvia.Name = "btnInvia"
        Me.btnInvia.Size = New System.Drawing.Size(75, 23)
        Me.btnInvia.TabIndex = 1
        Me.btnInvia.Text = "Invia"
        Me.btnInvia.UseVisualStyleBackColor = False
        '
        'lblmessaggio
        '
        Me.lblmessaggio.AutoSize = True
        Me.lblmessaggio.BackColor = System.Drawing.Color.Transparent
        Me.lblmessaggio.Location = New System.Drawing.Point(12, 175)
        Me.lblmessaggio.Name = "lblmessaggio"
        Me.lblmessaggio.Size = New System.Drawing.Size(164, 13)
        Me.lblmessaggio.TabIndex = 2
        Me.lblmessaggio.Text = "Messaggio Richiesta Assistenza :"
        '
        'txtoggetto
        '
        Me.txtoggetto.BackColor = System.Drawing.Color.White
        Me.txtoggetto.Location = New System.Drawing.Point(12, 143)
        Me.txtoggetto.Name = "txtoggetto"
        Me.txtoggetto.Size = New System.Drawing.Size(297, 20)
        Me.txtoggetto.TabIndex = 4
        '
        'lbloggetto
        '
        Me.lbloggetto.AutoSize = True
        Me.lbloggetto.BackColor = System.Drawing.Color.Transparent
        Me.lbloggetto.Location = New System.Drawing.Point(12, 127)
        Me.lbloggetto.Name = "lbloggetto"
        Me.lbloggetto.Size = New System.Drawing.Size(51, 13)
        Me.lbloggetto.TabIndex = 4
        Me.lbloggetto.Text = "Oggetto :"
        '
        'cmbTecnico
        '
        Me.cmbTecnico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTecnico.FormattingEnabled = True
        Me.cmbTecnico.Items.AddRange(New Object() {"Azienda Assistente"})
        Me.cmbTecnico.Location = New System.Drawing.Point(12, 61)
        Me.cmbTecnico.MaxDropDownItems = 10
        Me.cmbTecnico.Name = "cmbTecnico"
        Me.cmbTecnico.Size = New System.Drawing.Size(297, 21)
        Me.cmbTecnico.TabIndex = 2
        '
        'lbltecnico
        '
        Me.lbltecnico.AutoSize = True
        Me.lbltecnico.BackColor = System.Drawing.Color.Transparent
        Me.lbltecnico.Location = New System.Drawing.Point(12, 45)
        Me.lbltecnico.Name = "lbltecnico"
        Me.lbltecnico.Size = New System.Drawing.Size(52, 13)
        Me.lbltecnico.TabIndex = 6
        Me.lbltecnico.Text = "Tecnico :"
        '
        'lblproblema
        '
        Me.lblproblema.AutoSize = True
        Me.lblproblema.BackColor = System.Drawing.Color.Transparent
        Me.lblproblema.Location = New System.Drawing.Point(12, 85)
        Me.lblproblema.Name = "lblproblema"
        Me.lblproblema.Size = New System.Drawing.Size(81, 13)
        Me.lblproblema.TabIndex = 8
        Me.lblproblema.Text = "Tipo Problema :"
        '
        'cmbtipoProblema
        '
        Me.cmbtipoProblema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtipoProblema.FormattingEnabled = True
        Me.cmbtipoProblema.Items.AddRange(New Object() {"Richiesta Generale di Assistenza", "Errore Programma", "Malfunzionamento Sistema", "Problema Hardware", "Richiesta Informazioni"})
        Me.cmbtipoProblema.Location = New System.Drawing.Point(12, 101)
        Me.cmbtipoProblema.Name = "cmbtipoProblema"
        Me.cmbtipoProblema.Size = New System.Drawing.Size(297, 21)
        Me.cmbtipoProblema.TabIndex = 3
        '
        'lblMail
        '
        Me.lblMail.BackColor = System.Drawing.Color.Transparent
        Me.lblMail.Font = New System.Drawing.Font("MS Reference Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMail.Location = New System.Drawing.Point(12, 9)
        Me.lblMail.Name = "lblMail"
        Me.lblMail.Size = New System.Drawing.Size(297, 23)
        Me.lblMail.TabIndex = 9
        Me.lblMail.Text = "Invia Richiesta Assistenza"
        Me.lblMail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pcprintscreen
        '
        Me.pcprintscreen.Image = Global.HelpAssistenza.My.Resources.Resources.fotocamera
        Me.pcprintscreen.Location = New System.Drawing.Point(285, 169)
        Me.pcprintscreen.Name = "pcprintscreen"
        Me.pcprintscreen.Size = New System.Drawing.Size(24, 19)
        Me.pcprintscreen.TabIndex = 10
        Me.pcprintscreen.TabStop = False
        Me.ToolTipCatturaSchermo.SetToolTip(Me.pcprintscreen, "Cattura Schermo ed Allega")
        '
        'ToolTipCatturaSchermo
        '
        Me.ToolTipCatturaSchermo.IsBalloon = True
        Me.ToolTipCatturaSchermo.ToolTipTitle = "Cattura Schermo"
        '
        'frmInvioMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(321, 348)
        Me.Controls.Add(Me.pcprintscreen)
        Me.Controls.Add(Me.lblMail)
        Me.Controls.Add(Me.lblproblema)
        Me.Controls.Add(Me.cmbtipoProblema)
        Me.Controls.Add(Me.lbltecnico)
        Me.Controls.Add(Me.cmbTecnico)
        Me.Controls.Add(Me.lbloggetto)
        Me.Controls.Add(Me.txtoggetto)
        Me.Controls.Add(Me.lblmessaggio)
        Me.Controls.Add(Me.btnInvia)
        Me.Controls.Add(Me.txtMessaggio)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmInvioMail"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "HelpAssistenza - Invio eMail"
        CType(Me.pcprintscreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMessaggio As System.Windows.Forms.TextBox
    Friend WithEvents btnInvia As System.Windows.Forms.Button
    Friend WithEvents lblmessaggio As System.Windows.Forms.Label
    Friend WithEvents txtoggetto As System.Windows.Forms.TextBox
    Friend WithEvents lbloggetto As System.Windows.Forms.Label
    Friend WithEvents cmbTecnico As System.Windows.Forms.ComboBox
    Friend WithEvents lbltecnico As System.Windows.Forms.Label
    Friend WithEvents lblproblema As System.Windows.Forms.Label
    Friend WithEvents cmbtipoProblema As System.Windows.Forms.ComboBox
    Friend WithEvents lblMail As System.Windows.Forms.Label
    Friend WithEvents pcprintscreen As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTipCatturaSchermo As System.Windows.Forms.ToolTip
End Class
