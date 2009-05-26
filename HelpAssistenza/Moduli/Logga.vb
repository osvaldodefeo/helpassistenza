Module Logga
    Dim LoggaHelpAssistenza As New clsLog
    Sub ScriviInLog(ByVal TestoLog As String)
        LoggaHelpAssistenza.FileName = Application.StartupPath + "\" + Application.ProductName + ".log"
        LoggaHelpAssistenza.Log(Date.Now + " - " + TestoLog)
    End Sub
End Module
