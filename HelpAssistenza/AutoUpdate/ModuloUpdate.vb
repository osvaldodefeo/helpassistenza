Public Module ModuloUpdate
    Public SilentMode As Boolean = False
    Public Function CheckForUpdates(ByVal NewestVersionLocation As String, ByVal NewestVersionDownloadLocation As String, Optional ByVal UnzipDir As String = "", Optional ByVal DisplayWindow As Boolean = True, Optional ByVal MsgBoxText As String = "", Optional ByVal MsgBoxTitle As String = "") As Boolean

        On Error GoTo errorhandle 'If there is an error, return false

        Dim dlg = New DownloadAggio 'Create the download dialog (but don't show it directly)

        If UnzipDir = "" Then 'If the UnzipDir isn't set, use the application's startup path
            dlg.vars_unzipdir = System.Windows.Forms.Application.StartupPath
        End If

        If MsgBoxText <> "" Then 'If the MsgBoxText isn't set, use the default text
            dlg.vars_bericht = MsgBoxText
        End If

        If MsgBoxTitle <> "" Then  'If the MsgBoxTitle isn't set, use the default title
            dlg.vars_berichttitel = MsgBoxTitle
        End If

        dlg.vars_downloadlocatie = NewestVersionDownloadLocation 'Set the NewestVersionDownloadLocation var in the dialog
        dlg.vars_versielocatie = NewestVersionLocation 'Set the NewestVersionLocation var in the dialog

        If DisplayWindow <> False Then 'If the DisplayWindow var is set, display the dialog, otherwise not.
            dlg.ShowDialog()
        End If

        dlg.Timer1.Enabled = True 'I've created a timer (otherwise it doesn't work, I don't know why)

        Return True 'Return true if all's gone well

        Exit Function

errorhandle:
        Return False

    End Function

    Public Function CheckForUpdatesSilent(ByVal NewestVersionLocation As String, ByVal NewestVersionDownloadLocation As String, Optional ByVal UnzipDir As String = "", Optional ByVal DisplayWindow As Boolean = True, Optional ByVal MsgBoxText As String = "", Optional ByVal MsgBoxTitle As String = "") As Boolean
        SilentMode = True
        On Error GoTo errorhandle 'If there is an error, return false
        Dim dlg = New DownloadAggio 'Create the download dialog (but don't show it directly)
        If UnzipDir = "" Then 'If the UnzipDir isn't set, use the application's startup path
            dlg.vars_unzipdir = System.Windows.Forms.Application.StartupPath
        End If
        If MsgBoxText <> "" Then 'If the MsgBoxText isn't set, use the default text
            dlg.vars_bericht = MsgBoxText
        End If
        If MsgBoxTitle <> "" Then  'If the MsgBoxTitle isn't set, use the default title
            dlg.vars_berichttitel = MsgBoxTitle
        End If
        dlg.vars_downloadlocatie = NewestVersionDownloadLocation 'Set the NewestVersionDownloadLocation var in the dialog
        dlg.vars_versielocatie = NewestVersionLocation 'Set the NewestVersionLocation var in the dialog
        If DisplayWindow <> False Then 'If the DisplayWindow var is set, display the dialog, otherwise not.
            dlg.ShowDialog()
        End If
        dlg.Timer1.Enabled = True 'I've created a timer (otherwise it doesn't work, I don't know why)
        Return True 'Return true if all's gone well

        Exit Function

errorhandle:
        Return False

    End Function


End Module