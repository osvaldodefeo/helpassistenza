<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Report))
        Me.lblNomeAzienda = New System.Windows.Forms.Label
        Me.lblnometecnico = New System.Windows.Forms.Label
        Me.lblIntDurataAssistenza = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.RichTextBoxRiepilogo = New System.Windows.Forms.RichTextBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.btnFinito = New System.Windows.Forms.Button
        Me.lblAzienda = New System.Windows.Forms.Label
        Me.lblTecnico = New System.Windows.Forms.Label
        Me.lblDurataAssistenza = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblNomeAzienda
        '
        Me.lblNomeAzienda.AutoSize = True
        Me.lblNomeAzienda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomeAzienda.Location = New System.Drawing.Point(18, 18)
        Me.lblNomeAzienda.Name = "lblNomeAzienda"
        Me.lblNomeAzienda.Size = New System.Drawing.Size(64, 16)
        Me.lblNomeAzienda.TabIndex = 0
        Me.lblNomeAzienda.Text = "Azienda"
        '
        'lblnometecnico
        '
        Me.lblnometecnico.AutoSize = True
        Me.lblnometecnico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnometecnico.Location = New System.Drawing.Point(18, 49)
        Me.lblnometecnico.Name = "lblnometecnico"
        Me.lblnometecnico.Size = New System.Drawing.Size(147, 16)
        Me.lblnometecnico.TabIndex = 1
        Me.lblnometecnico.Text = "Operatore / Tecnico"
        '
        'lblIntDurataAssistenza
        '
        Me.lblIntDurataAssistenza.AutoSize = True
        Me.lblIntDurataAssistenza.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntDurataAssistenza.Location = New System.Drawing.Point(18, 80)
        Me.lblIntDurataAssistenza.Name = "lblIntDurataAssistenza"
        Me.lblIntDurataAssistenza.Size = New System.Drawing.Size(133, 16)
        Me.lblIntDurataAssistenza.TabIndex = 2
        Me.lblIntDurataAssistenza.Text = "Durata Assistenza"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(193, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = ":"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(193, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(193, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = ":"
        '
        'RichTextBoxRiepilogo
        '
        Me.RichTextBoxRiepilogo.BackColor = System.Drawing.Color.DodgerBlue
        Me.RichTextBoxRiepilogo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxRiepilogo.Location = New System.Drawing.Point(0, 122)
        Me.RichTextBoxRiepilogo.Name = "RichTextBoxRiepilogo"
        Me.RichTextBoxRiepilogo.Size = New System.Drawing.Size(536, 148)
        Me.RichTextBoxRiepilogo.TabIndex = 6
        Me.RichTextBoxRiepilogo.Text = ""
        '
        'BtnOK
        '
        Me.BtnOK.AutoSize = True
        Me.BtnOK.Enabled = False
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(225, 272)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 28)
        Me.BtnOK.TabIndex = 7
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'btnFinito
        '
        Me.btnFinito.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFinito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinito.Location = New System.Drawing.Point(451, 276)
        Me.btnFinito.Name = "btnFinito"
        Me.btnFinito.Size = New System.Drawing.Size(75, 21)
        Me.btnFinito.TabIndex = 8
        Me.btnFinito.Text = "Finito"
        Me.btnFinito.UseVisualStyleBackColor = True
        '
        'lblAzienda
        '
        Me.lblAzienda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAzienda.Location = New System.Drawing.Point(236, 18)
        Me.lblAzienda.Name = "lblAzienda"
        Me.lblAzienda.Size = New System.Drawing.Size(290, 16)
        Me.lblAzienda.TabIndex = 9
        Me.lblAzienda.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTecnico
        '
        Me.lblTecnico.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTecnico.Location = New System.Drawing.Point(236, 49)
        Me.lblTecnico.Name = "lblTecnico"
        Me.lblTecnico.Size = New System.Drawing.Size(290, 16)
        Me.lblTecnico.TabIndex = 10
        Me.lblTecnico.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDurataAssistenza
        '
        Me.lblDurataAssistenza.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDurataAssistenza.Location = New System.Drawing.Point(236, 80)
        Me.lblDurataAssistenza.Name = "lblDurataAssistenza"
        Me.lblDurataAssistenza.Size = New System.Drawing.Size(290, 16)
        Me.lblDurataAssistenza.TabIndex = 11
        Me.lblDurataAssistenza.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(538, 303)
        Me.Controls.Add(Me.lblDurataAssistenza)
        Me.Controls.Add(Me.lblTecnico)
        Me.Controls.Add(Me.lblAzienda)
        Me.Controls.Add(Me.btnFinito)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.RichTextBoxRiepilogo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblIntDurataAssistenza)
        Me.Controls.Add(Me.lblnometecnico)
        Me.Controls.Add(Me.lblNomeAzienda)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Report"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HelpAssistenza - Report"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNomeAzienda As System.Windows.Forms.Label
    Friend WithEvents lblnometecnico As System.Windows.Forms.Label
    Friend WithEvents lblIntDurataAssistenza As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RichTextBoxRiepilogo As System.Windows.Forms.RichTextBox
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents btnFinito As System.Windows.Forms.Button
    Friend WithEvents lblAzienda As System.Windows.Forms.Label
    Friend WithEvents lblTecnico As System.Windows.Forms.Label
    Friend WithEvents lblDurataAssistenza As System.Windows.Forms.Label
End Class
