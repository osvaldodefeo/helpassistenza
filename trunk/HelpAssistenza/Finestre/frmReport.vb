Imports System.Drawing.Printing
Public Class frmReport
#Region "Dichiarazioni"
    Private WithEvents m_DocumentoStampa As New PrintDocument()
    Private m_Testo As String = Nothing
#End Region
    Private Sub m_DocumentoStampa_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles m_DocumentoStampa.PrintPage
        Dim Img As Image = frmAssistenza.picLogoAzienda.Image
        Dim CarattereStampa As New Font(Me.Font.FontFamily, 10, FontStyle.Regular)
        Dim TopArea As Integer = e.MarginBounds.Top + 80

        Dim NumeroCaratteriInseriti As Integer = 0
        Dim NumeroLineeInserite As Integer = 0
        Dim Fmt As New StringFormat(StringFormatFlags.LineLimit)

        Static CarattereCorrente As Integer
        'Controllo se c'è Logo impostato, se no imposto immagine di errore
        If frmConfigurazione.txtlogo.Text = String.Empty OrElse frmConfigurazione.txtlogo.Text = "Percorso Logo" Then
            'Settare Immagine di errore
            Img = PicNoLogo.Image
        End If
        'COSTRUZIONE DI UNA SEMPLICE INTESTAZIONE DI PAGINA 

        'Disegna una stringa contentente la data di stampa a seconda delle Voci Abilitate nel Form Report
        Dim ora As String = " Ore " & Hour(Now) & ":" & Minute(Now)
        If Me.lblnometecnico.Visible = False And Me.lblIntDurataAssistenza.Visible = False Then
            e.Graphics.DrawString("RAPPORTO ASSISTENZA di: " & Date.Now.ToLongDateString & ora & vbCrLf & Me.lblNomeAzienda.Text & " Assistente : " & Me.lblAzienda.Text & vbCrLf, New Font("Microsoft Sans Serif", 10, FontStyle.Bold), _
            New SolidBrush(Color.Black), e.MarginBounds.Left, TopArea - 70)
        ElseIf Me.lblnometecnico.Visible = False Then
            e.Graphics.DrawString("RAPPORTO ASSISTENZA di: " & Date.Now.ToLongDateString & ora & vbCrLf & Me.lblNomeAzienda.Text & " Assistente : " & Me.lblAzienda.Text & vbCrLf & lblIntDurataAssistenza.Text & " : " & lblDurataAssistenza.Text, New Font("Microsoft Sans Serif", 10, FontStyle.Bold), _
            New SolidBrush(Color.Black), e.MarginBounds.Left, TopArea - 70)
        ElseIf Me.lblIntDurataAssistenza.Visible = False Then
            e.Graphics.DrawString("RAPPORTO ASSISTENZA di: " & Date.Now.ToLongDateString & ora & vbCrLf & Me.lblNomeAzienda.Text & " Assistente : " & Me.lblAzienda.Text & vbCrLf & Me.lblnometecnico.Text & " : " & Me.lblTecnico.Text & vbCrLf, New Font("Microsoft Sans Serif", 10, FontStyle.Bold), _
              New SolidBrush(Color.Black), e.MarginBounds.Left, TopArea - 70)
        Else
            e.Graphics.DrawString("RAPPORTO ASSISTENZA di: " & Date.Now.ToLongDateString & ora & vbCrLf & Me.lblNomeAzienda.Text & " Assistente : " & Me.lblAzienda.Text & vbCrLf & Me.lblnometecnico.Text & " : " & Me.lblTecnico.Text & vbCrLf & lblIntDurataAssistenza.Text & " : " & lblDurataAssistenza.Text, New Font("Microsoft Sans Serif", 10, FontStyle.Bold), _
            New SolidBrush(Color.Black), e.MarginBounds.Left, TopArea - 70)
        End If
        
        'Rettangolo di destinazionie nel quale sarà diesgnata l'immaagine
        Dim RettangoloLogo As New Rectangle(e.MarginBounds.Right - 60, e.MarginBounds.Top - 5, 96, 80)

        'Effetto ombra per il logo
        e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), _
            RettangoloLogo.Left + 5, RettangoloLogo.Top + 5, _
            RettangoloLogo.Width, RettangoloLogo.Height)

        'Disegna l'immagine, ad esempio un logo aziendale        
        e.Graphics.DrawImage(Img, RettangoloLogo)


        'Disegna una linea rossa
        e.Graphics.DrawLine(New Pen(Color.Red), e.MarginBounds.Left, TopArea + 5, e.MarginBounds.Right, TopArea + 5)

        'STAMPA DEL TESTO DELLA PAGINA

        'Area di stampa
        Dim rectPrintingArea As New RectangleF(e.MarginBounds.Left, TopArea + CarattereStampa.Height, e.MarginBounds.Width, _
                    e.MarginBounds.Bottom - e.MarginBounds.Top - Img.Height - CarattereStampa.Height)

        'Misura la stringa per controllare che non sia troppo lunga,nel caso la divide
        e.Graphics.MeasureString(Mid(m_Testo, CarattereCorrente + 1), CarattereStampa, _
                    New SizeF(e.MarginBounds.Right, e.MarginBounds.Bottom), Fmt, _
                    NumeroCaratteriInseriti, NumeroLineeInserite)

        'Disegna la stringa
        e.Graphics.DrawString(Mid(m_Testo, CarattereCorrente + 1), CarattereStampa, _
            Brushes.Black, rectPrintingArea, Fmt)
        'Incrementa il carrattere corrente

        CarattereCorrente += NumeroCaratteriInseriti

        'CREAZIONE DI UN SEMPLICE PIE' DI PAGINA
        'Disegna una linea rossa        
        e.Graphics.DrawLine(New Pen(Color.Red), e.MarginBounds.Left, e.MarginBounds.Bottom _
                    , e.MarginBounds.Right, e.MarginBounds.Bottom)

        e.Graphics.DrawString("Rapporto Assistenza Generato da HelpAssistenza", New Font("Microsoft Sans Serif", 8, FontStyle.Italic), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Bottom + 10)

        'CI SONO ALTRE PAGINE DA STAMPARE?
        If CarattereCorrente < m_Testo.Length Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            'Riazzera il contatore del carattere corrente
            CarattereCorrente = 0
        End If
    End Sub

    Private Sub InputNomeOpe()
        If btnFinito.Visible = True Then
            Dim NomeOp As String = InputBox("Inserire il Nome dell' Operatore/Tecnico :", "HelpAssistenza - Report")
            Me.lblTecnico.Text = NomeOp
        End If
    End Sub

    Private Sub RichTextBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBoxRiepilogo.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            ContextMenuReport.Show(Cursor.Position)
        End If
    End Sub

    Private Sub btnFinito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinito.Click
        TempoAssistenza.Stop()
        Me.btnFinito.Visible = False
        If frmConfigurazione.chkabilitaStampa.Checked = True Then Me.btnStampa.Visible = True
        Me.RichTextBoxRiepilogo.ReadOnly = True
        Me.TopMost = True
        ChiudiConnessioni()
        Me.BtnOK.Enabled = True
    End Sub

    Private Sub Report_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BtnOK.Enabled = False Then
        Else
            If frmAssistenza.TimerFTP.Enabled = True OrElse ListenMode = True Then
                Me.Hide()
            Else
                frmAssistenza.NotifyHelp.Visible = False
                End
            End If
        End If
    End Sub

    Private Sub Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TopMost = False
        Me.RichTextBoxRiepilogo.ReadOnly = False
        Me.lblAzienda.Text = frmConfigurazione.txtnomeazienda.Text
        Me.lblDurataAssistenza.Text = TempoAssistenza.Elapsed.Minutes & " Minuti"
        If TempoAssistenza.Elapsed.Minutes = "1" Then Me.lblDurataAssistenza.Text = TempoAssistenza.Elapsed.Minutes & " Minuto"
        If Me.lblnometecnico.Visible = True Then Label2.Visible = True
        If Me.lblIntDurataAssistenza.Visible = True Then Label3.Visible = True
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        If frmAssistenza.TimerFTP.Enabled = True OrElse ListenMode = True Then
            frmAssistenza.NotifyHelp.Visible = True
            Me.Close()
        Else
            Me.Close()
            frmAssistenza.NotifyHelp.Visible = False
            Application.Exit()
        End If
    End Sub

    Private Sub ContextMenuReport_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuReport.Opening
        If RichTextBoxRiepilogo.SelectedText.Length > 0 Then
            TagliaToolStripMenuItem.Enabled = True
            CopiaToolStripMenuItem.Enabled = True
        Else
            TagliaToolStripMenuItem.Enabled = False
            CopiaToolStripMenuItem.Enabled = False
        End If
        If RichTextBoxRiepilogo.Text <> String.Empty Then
            CancellaTuttoToolStripMenuItem.Visible = True
        Else
            CancellaTuttoToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub ElencoPuntatoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ElencoPuntatoToolStripMenuItem.Click
        If Me.RichTextBoxRiepilogo.SelectionBullet = False Then
            Me.RichTextBoxRiepilogo.SelectionBullet = True
        Else
            Me.RichTextBoxRiepilogo.SelectionBullet = False
        End If
    End Sub

    Private Sub TagliaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TagliaToolStripMenuItem.Click
        RichTextBoxRiepilogo.Cut()
    End Sub

    Private Sub CopiaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopiaToolStripMenuItem.Click
        RichTextBoxRiepilogo.Copy()
    End Sub

    Private Sub IncollaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncollaToolStripMenuItem.Click
        RichTextBoxRiepilogo.Paste()
    End Sub

    Private Sub CancellaTuttoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancellaTuttoToolStripMenuItem.Click
        RichTextBoxRiepilogo.Clear()
        RichTextBoxRiepilogo.Enabled = True
        RichTextBoxRiepilogo.Select()
    End Sub

    Private Sub btnStampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        m_Testo = RichTextBoxRiepilogo.Text
        m_DocumentoStampa.Print()
    End Sub

    Private Sub FontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontToolStripMenuItem.Click
        FontDialogReport.ShowDialog()
        Me.RichTextBoxRiepilogo.Font = FontDialogReport.Font
    End Sub

    Private Sub FontDialogReport_Apply(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FontDialogReport.Apply
        Me.RichTextBoxRiepilogo.Font = FontDialogReport.Font
    End Sub

    Private Sub lblTecnico_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTecnico.DoubleClick
        InputNomeOpe()
    End Sub

    Private Sub lblnometecnico_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblnometecnico.DoubleClick
        InputNomeOpe()
    End Sub

    Private Sub RiferimentoTicketToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RiferimentoTicketToolStripMenuItem.Click
        Dim numTicket As String = InputBox("Immetere il numero di Ticket di Riferimento :", "HelpAssistenza")
        RichTextBoxRiepilogo.Text = "RIFERIMENTO TICKET : " + numTicket + vbCrLf + vbCrLf + RichTextBoxRiepilogo.Text
        RichTextBoxRiepilogo.SelectionStart = RichTextBoxRiepilogo.TextLength
    End Sub
End Class