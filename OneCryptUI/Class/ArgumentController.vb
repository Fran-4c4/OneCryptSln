Imports System.IO

Namespace ind.crypt
    ''' <summary>
    ''' class used to manage arguments from command line
    ''' </summary>
    Public Class ArgumentController
        Public Sub ArgumentSwitch(args As Collections.ObjectModel.ReadOnlyCollection(Of String))

            Dim str As String = "Arg: " & args.Count

                Dim countArg As Integer = args.Count
                If countArg = 0 Then 'in the first position is the name of application
                    Exit Sub
                End If

                Dim arg0 As String = args(0).ToLower

                If arg0 = "/open" And countArg = 2 Then 'if open but no more data then nothing
                    Dim file As String = args(1)

                    If ind.cte.AppVars.UserValidated = False Then
                        ind.win.Msg.mWarning(ind.lang.UILang.WindowsContextMenu_OpenArgUserNotValidated)
                        Try
                            IO.File.Delete(file)
                        Catch ex As Exception
                            ind.log.Logging.logExcep(ex)
                        End Try
                        Exit Sub
                    End If
                    If Not ind.crypt.OCGlobal.mainForm Is Nothing Then
                        ind.crypt.OCGlobal.mainForm.RestoreApplication()
                    End If
                    ind.crypt.OCGlobal.ocExplorer.loadLibraryRecentFilesTAB().LoadListOfFilesFile(file)
                ElseIf arg0 = "/decrypt" And countArg = 2 Then 'if open but no more data then nothing
                    'decrypt? or show in onecrypt
                    Dim file As String = args(1)
                    ind.crypt.OCGlobal.ocExplorer.loadLibraryRecentFilesTAB().loadFile(File)
                    Exit Sub
                Else
                    'try to show
                    Dim file As String = args(0)
                    ind.crypt.OCGlobal.ocExplorer.loadLibraryRecentFilesTAB().loadFile(file)
                    Exit Sub
                End If

            'error must be controlled by caller

        End Sub

        

    End Class
End Namespace


