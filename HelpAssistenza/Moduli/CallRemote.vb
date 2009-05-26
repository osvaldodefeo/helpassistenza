Module CallRemote
    Public TempoAssistenza As New Stopwatch
    Public ProcessTeleAssi As Process
    Public PortePCAnyADSL As Boolean = False
    Public PCAny_Attivo As Boolean = False
    Public Declare Function apiBlockInput Lib "user32" Alias "BlockInput" (ByVal fBlock As Integer) As Integer
    Public Sub ChiamaRemotoADSL(ByVal pathvnc As String, ByVal parametriVNC As String, ByVal Reconnect As Boolean)
        Dim PrcProcesso As System.Diagnostics.Process()
        PCAny_Attivo = False
        frmAssistenza.Cursor = Cursors.WaitCursor
        'Controllo se presente Variabile %PROGRAMPATH%
        If pathvnc.Contains("%PROGRAMPATH%") Then pathvnc = Replace(pathvnc, "%PROGRAMPATH%", Application.StartupPath)
        If parametriVNC.Contains("%PROGRAMPATH%") Then parametriVNC = Replace(parametriVNC, "%PROGRAMPATH%", Application.StartupPath)
        'Controllo se presente Variabile %PROGRAMFILES%
        If pathvnc.Contains("%PROGRAMFILES%") Then pathvnc = Replace(pathvnc, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If parametriVNC.Contains("%PROGRAMFILES%") Then parametriVNC = Replace(parametriVNC, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        'Controllo quale programma Server VNC controllare
        If frmConfigurazione.txtpercorsoadsl.Text.Contains("winvnc") Then
            PrcProcesso = Process.GetProcessesByName("winvnc")
        Else
            PrcProcesso = Process.GetProcessesByName("winvnc")
        End If
        If Reconnect = True Then
            If Not (PrcProcesso.Length > 0) Then
                System.Diagnostics.Process.Start(pathvnc)
                System.Threading.Thread.Sleep(900)
            End If
            ProcessTeleAssi = System.Diagnostics.Process.Start(pathvnc, "-autoreconnect " & parametriVNC)
            frmAssistenza.TimerMonitorProcess.Enabled = True
        Else
            If Not (PrcProcesso.Length > 0) Then
                System.Diagnostics.Process.Start(pathvnc)
                System.Threading.Thread.Sleep(900)
            End If
            ProcessTeleAssi = System.Diagnostics.Process.Start(pathvnc, parametriVNC)
            frmAssistenza.TimerMonitorProcess.Enabled = True
        End If
        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Collegamento Linea : " + frmAssistenza.Operatore.ToString + " ADSL(VNC)")
        frmAssistenza.Cursor = Cursors.Default
        TempoAssistenza.Reset()
        TempoAssistenza.Start()
    End Sub
    Public Sub ChiamaRemotoModem(ByVal pathanywhere As String, ByVal ParametriAnyWhere As String)
        PCAny_Attivo = True
        frmAssistenza.Cursor = Cursors.WaitCursor
        'Controllo se presente Variabile %PROGRAMPATH%
        If pathanywhere.Contains("%PROGRAMPATH%") Then pathanywhere = Replace(pathanywhere, "%PROGRAMPATH%", Application.StartupPath)
        If ParametriAnyWhere.Contains("%PROGRAMPATH%") Then ParametriAnyWhere = Replace(ParametriAnyWhere, "%PROGRAMPATH%", Application.StartupPath)
        'Controllo se presente Variabile %PROGRAMFILES%
        If pathanywhere.Contains("%PROGRAMFILES%") Then pathanywhere = Replace(pathanywhere, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If ParametriAnyWhere.Contains("%PROGRAMFILES%") Then ParametriAnyWhere = Replace(ParametriAnyWhere, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If (pathanywhere <> String.Empty And ParametriAnyWhere <> String.Empty) AndAlso My.Computer.FileSystem.FileExists(pathanywhere) Then
            ProcessTeleAssi = System.Diagnostics.Process.Start(pathanywhere, ParametriAnyWhere)
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Collegamento Linea : " + frmAssistenza.Operatore.ToString + " MODEM")
            TempoAssistenza.Reset()
            TempoAssistenza.Start()
        Else
            frmAssistenza.NotifyHelp.Visible = False
            MessageBox.Show("Connessione ad Internet non Attiva o Problemi di Connettività !" & vbCrLf & "Programma Collegamento Modem non Configurato!" & vbCrLf & "Controllare lo Stato rete Internet o Configurare Programma Collegamento via Modem!", "HelpAssistenza - Errore Collegamento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione ad Internet non Attiva o Problemi di Connettività!")
            frmAssistenza.Operatore = 0
            frmAssistenza.Show()
        End If
        frmAssistenza.Cursor = Cursors.Default
        frmAssistenza.TimerMonitorProcess.Enabled = True
    End Sub
    Public Sub ChiamaremotoPCADSL(ByVal percorsoPCAnywhere As String, ByVal parametriPCAnywhere As String, ByVal IPRemotoPCA As String)
        Dim p As Process = New Process
        Dim PrcProcesso As System.Diagnostics.Process()
        PCAny_Attivo = True
        frmAssistenza.Cursor = Cursors.WaitCursor
        'Controllo se Settare le porte per PCAnywhereADSL
        If frmConfigurazione.chkSettaPortePCA.Checked = True Then
            PortePCAnyADSL = True
            SettaPortePCAny(frmAssistenza.Operatore)
        End If

        'Controllo se presente Variabile %PROGRAMPATH%
        If percorsoPCAnywhere.Contains("%PROGRAMPATH%") Then percorsoPCAnywhere = Replace(percorsoPCAnywhere, "%PROGRAMPATH%", Application.StartupPath)
        If parametriPCAnywhere.Contains("%PROGRAMPATH%") Then parametriPCAnywhere = Replace(parametriPCAnywhere, "%PROGRAMPATH%", Application.StartupPath)
        'Controllo se presente Variabile %PROGRAMFILES%
        If percorsoPCAnywhere.Contains("%PROGRAMFILES%") Then percorsoPCAnywhere = Replace(percorsoPCAnywhere, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If parametriPCAnywhere.Contains("%PROGRAMFILES%") Then parametriPCAnywhere = Replace(parametriPCAnywhere, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)

        Do

            'Cerco il prodesso che mi interessa
            PrcProcesso = Process.GetProcessesByName("awhost32")

            'se Length>0 significa che il processo è attivo
            If (PrcProcesso.Length > 0) Then
                'chiudo il porcesso
                PrcProcesso(0).Kill()

                'pausa per permettergli di aggiornare la proprietà lenght
                System.Threading.Thread.Sleep(300)

            End If

        Loop While (PrcProcesso.Length > 0)

        p.StartInfo.FileName = percorsoPCAnywhere

        p.StartInfo.Arguments = Chr(34) & parametriPCAnywhere & Chr(34) & " /D /C"

        p.Start()

        apiBlockInput(1)
        System.Threading.Thread.Sleep(4000)
        apiBlockInput(0)

        SendKeys.Send(IPRemotoPCA)

        SendKeys.Send("{ENTER}")
        frmAssistenza.Cursor = Cursors.Default
        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Collegamento Linea : " + frmAssistenza.Operatore.ToString + " ADSL(PCAnyWhere)")
        TempoAssistenza.Reset()
        TempoAssistenza.Start()
        frmAssistenza.TimerMonitorProcess.Enabled = True
    End Sub
    Sub ChiudiConnessioni()
        'Se VNC Server era attivo all'apertura di Helpassistenza lo rilancio se no lo chiudo
        Dim StopHost As String = String.Empty
        Dim pathchiudiVNC As String = frmConfigurazione.txtpercorsoadsl.Text
        Dim pathChiudiPcAny As String = frmConfigurazione.txtpercorsoadsl2.Text
        frmAssistenza.Cursor = Cursors.WaitCursor
        frmAssistenza.TimerMonitorProcess.Enabled = False
        'Controllo se presente Variabile %PROGRAMPATH%
        If pathchiudiVNC.Contains("%PROGRAMPATH%") Then pathchiudiVNC = Replace(pathchiudiVNC, "%PROGRAMPATH%", Application.StartupPath)
        If pathChiudiPcAny.Contains("%PROGRAMPATH%") Then pathChiudiPcAny = Replace(pathChiudiPcAny, "%PROGRAMPATH%", Application.StartupPath)
        'Controllo se presente Variabile %PROGRAMFILES%
        If pathchiudiVNC.Contains("%PROGRAMFILES%") Then pathchiudiVNC = Replace(pathchiudiVNC, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)
        If pathChiudiPcAny.Contains("%PROGRAMFILES%") Then pathChiudiPcAny = Replace(pathChiudiPcAny, "%PROGRAMFILES%", My.Computer.FileSystem.SpecialDirectories.ProgramFiles)

        If frmAssistenza.VNCAttivo = True Then

            Try
                System.Diagnostics.Process.Start(pathchiudiVNC, "-kill")
                System.Threading.Thread.Sleep(1300)
                Dim strservice As String = String.Empty

                If CheckService("winvnc") Then strservice = "winvnc"
                If CheckService("uvnc_service") Then strservice = "uvnc_service"

                If strservice <> String.Empty Then

                    frmAssistenza.VNCController.MachineName = My.Computer.Name
                    frmAssistenza.VNCController.ServiceName = strservice
                    If frmAssistenza.VNCController.Status = ServiceProcess.ServiceControllerStatus.Running OrElse frmAssistenza.VNCController.Status = ServiceProcess.ServiceControllerStatus.Paused Then
                        frmAssistenza.VNCController.Stop()
                        frmAssistenza.VNCController.Start()
                    ElseIf frmAssistenza.VNCController.Status = ServiceProcess.ServiceControllerStatus.Stopped Then
                        frmAssistenza.VNCController.Start()
                    End If

                Else
                    If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.ProgramFiles + "\UltraVNC\winvnc.exe") Then
                        System.Diagnostics.Process.Start(My.Computer.FileSystem.SpecialDirectories.ProgramFiles + "\UltraVNC\winvnc.exe")
                    Else
                        System.Diagnostics.Process.Start(pathchiudiVNC)
                    End If
                End If

            Catch ex As Exception

            End Try


        Dim ProcessoPCAnyWhere As System.Diagnostics.Process()
        If frmConfigurazione.txtpercorsoadsl2.Text.Contains("awhost32.exe") Then StopHost = Replace(pathChiudiPcAny, "awhost32.exe", "stophost.exe")
        Do
            'Cerco il processo awhost32
            ProcessoPCAnyWhere = Process.GetProcessesByName("awhost32")
            'se Length>0 significa che il processo è attivo
            If (ProcessoPCAnyWhere.Length > 0) Then
                If StopHost.Contains("stophost.exe") Then
                    'Lancio PCAnywhere StopHost
                    System.Diagnostics.Process.Start(StopHost)
                Else
                    'chiudo il porcesso
                    ProcessoPCAnyWhere(0).Kill()
                End If
                'pausa per permettergli di aggiornare la proprietà lenght
                System.Threading.Thread.Sleep(300)
            End If

        Loop While (ProcessoPCAnyWhere.Length > 0)

        Else
        Dim ProcessoVNC As System.Diagnostics.Process()
        Do
            ProcessoVNC = Process.GetProcessesByName("winvnc")
            If (ProcessoVNC.Length > 0) Then
                System.Diagnostics.Process.Start(pathchiudiVNC, "-kill")
                System.Threading.Thread.Sleep(1300)
                Try
                    ProcessoVNC(0).Kill()
                Catch
                    Exit Do
                End Try
                'pausa per permettergli di aggiornare la proprietà lenght
                System.Threading.Thread.Sleep(300)
            End If

        Loop While (ProcessoVNC.Length > 0)


        Dim ProcessoPCAnyWhere As System.Diagnostics.Process()

        If frmConfigurazione.txtpercorsoadsl2.Text.Contains("awhost32.exe") Then StopHost = Replace(pathChiudiPcAny, "awhost32.exe", "stophost.exe")
        Do
            'Cerco il processo awhost32
            ProcessoPCAnyWhere = Process.GetProcessesByName("awhost32")
            If (ProcessoPCAnyWhere.Length > 0) Then
                If StopHost.Contains("stophost.exe") Then
                    'Lancio PCAnywhere StopHost
                    System.Diagnostics.Process.Start(StopHost)
                Else
                    'chiudo il porcesso
                    ProcessoPCAnyWhere(0).Kill()
                End If
                'pausa per permettergli di aggiornare la proprietà lenght
                System.Threading.Thread.Sleep(300)
            End If

        Loop While (ProcessoPCAnyWhere.Length > 0)
        End If
        If frmConfigurazione.chkabilitaLog.Checked = True Then ScriviInLog("Connessione Terminata")
        PCAny_Attivo = False
        frmAssistenza.Operatore = 0
        frmAssistenza.Cursor = Cursors.Default
        If frmConfigurazione.chkSettaPortePCA.Checked = True And PortePCAnyADSL = True Then SettaPortePCAny()
    End Sub
    Sub SettaPortePCAny(Optional ByVal linea As Short = 0)
        Try
            Dim porta As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Symantec\pcAnywhere\CurrentVersion\System", True)

            Select Case linea
                Case Is = 0
                    'Setta porte di default PCAnyWhere
                    porta.SetValue("TCPIPDataPort", 5631, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5632, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 1
                    'Setta porte linea 1
                    porta.SetValue("TCPIPDataPort", 5931, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5932, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 2
                    'Setta porte linea 2
                    porta.SetValue("TCPIPDataPort", 5933, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5934, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 3
                    'Setta porte linea 3
                    porta.SetValue("TCPIPDataPort", 5935, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5936, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 4
                    'Setta porte linea 4
                    porta.SetValue("TCPIPDataPort", 5937, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5938, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 5
                    'Setta porte linea 5
                    porta.SetValue("TCPIPDataPort", 5939, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5940, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 6
                    'Setta porte linea 6
                    porta.SetValue("TCPIPDataPort", 5941, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5942, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 7
                    'Setta porte linea 7
                    porta.SetValue("TCPIPDataPort", 5943, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5944, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 8
                    'Setta porte linea 8
                    porta.SetValue("TCPIPDataPort", 5945, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5946, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 9
                    'Setta porte linea 9
                    porta.SetValue("TCPIPDataPort", 5947, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5948, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 10
                    'Setta porte linea 10
                    porta.SetValue("TCPIPDataPort", 5949, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5950, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 11
                    'Setta porte linea 11
                    porta.SetValue("TCPIPDataPort", 5951, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5952, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 12
                    'Setta porte linea 12
                    porta.SetValue("TCPIPDataPort", 5953, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5954, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 13
                    'Setta porte linea
                    porta.SetValue("TCPIPDataPort", 5955, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5956, Microsoft.Win32.RegistryValueKind.DWord)
                Case Is = 14
                    'Setta porte linea
                    porta.SetValue("TCPIPDataPort", 5957, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5958, Microsoft.Win32.RegistryValueKind.DWord)
                Case Else
                    'Setta porte di default PCAnyWhere
                    porta.SetValue("TCPIPDataPort", 5631, Microsoft.Win32.RegistryValueKind.DWord)
                    porta.SetValue("TCPIPStatusPort", 5632, Microsoft.Win32.RegistryValueKind.DWord)
            End Select
        Catch
            MsgBox("Impossibile Settare Porte Symantec PCAnywhere" + vbCrLf + _
                   "Controllare Installazione PCAnywhere !!!")
        End Try
    End Sub
    Public Function CheckService(ByVal Name As String) As Boolean
        Dim colServices As Object
        Dim objService As Object
        colServices = GetObject("winmgmts:").ExecQuery("Select Name from Win32_Service where Name = '" & Name & "'")
        For Each objService In colServices
            If Len(objService.Name) Then
                CheckService = True
            End If
        Next
        colServices = Nothing
    End Function
End Module
