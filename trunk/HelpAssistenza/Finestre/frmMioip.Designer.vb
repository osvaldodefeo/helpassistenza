<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMioip
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
        Me.btnok = New System.Windows.Forms.Button
        Me.lblip = New System.Windows.Forms.Label
        Me.lbliplan = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblextip = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblcomputername = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.ToolIP = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Timerimgcon = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnok
        '
        Me.btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.Location = New System.Drawing.Point(228, 144)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(69, 33)
        Me.btnok.TabIndex = 0
        Me.btnok.Text = "OK"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'lblip
        '
        Me.lblip.AutoSize = True
        Me.lblip.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblip.Location = New System.Drawing.Point(95, 16)
        Me.lblip.Name = "lblip"
        Me.lblip.Size = New System.Drawing.Size(183, 24)
        Me.lblip.TabIndex = 1
        Me.lblip.Text = "Indirizzo IP Interno"
        '
        'lbliplan
        '
        Me.lbliplan.AutoSize = True
        Me.lbliplan.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbliplan.Location = New System.Drawing.Point(309, 16)
        Me.lbliplan.Name = "lbliplan"
        Me.lbliplan.Size = New System.Drawing.Size(0, 24)
        Me.lbliplan.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(95, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Indirizzo IP  Esterno "
        '
        'lblextip
        '
        Me.lblextip.AutoSize = True
        Me.lblextip.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblextip.ForeColor = System.Drawing.Color.DarkRed
        Me.lblextip.Location = New System.Drawing.Point(309, 58)
        Me.lblextip.Name = "lblextip"
        Me.lblextip.Size = New System.Drawing.Size(0, 24)
        Me.lblextip.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(95, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(163, 24)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Nome Computer"
        '
        'lblcomputername
        '
        Me.lblcomputername.AutoSize = True
        Me.lblcomputername.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcomputername.Location = New System.Drawing.Point(309, 97)
        Me.lblcomputername.Name = "lblcomputername"
        Me.lblcomputername.Size = New System.Drawing.Size(0, 24)
        Me.lblcomputername.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(287, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 24)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = ":"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(287, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = ":"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(287, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 24)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = ":"
        '
        'ToolIP
        '
        Me.ToolIP.AutoPopDelay = 350000
        Me.ToolIP.InitialDelay = 500
        Me.ToolIP.IsBalloon = True
        Me.ToolIP.ReshowDelay = 100
        Me.ToolIP.ToolTipTitle = "Indirizzi IP LAN"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.HelpAssistenza.My.Resources.Resources.internet_si
        Me.PictureBox1.Location = New System.Drawing.Point(2, 25)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(96, 96)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'Timerimgcon
        '
        Me.Timerimgcon.Interval = 750
        '
        'frmMioip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(516, 180)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblcomputername)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblextip)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbliplan)
        Me.Controls.Add(Me.lblip)
        Me.Controls.Add(Me.btnok)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMioip"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "mioip"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents lblip As System.Windows.Forms.Label
    Friend WithEvents lbliplan As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblextip As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblcomputername As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolIP As System.Windows.Forms.ToolTip
    Friend WithEvents Timerimgcon As System.Windows.Forms.Timer

End Class
