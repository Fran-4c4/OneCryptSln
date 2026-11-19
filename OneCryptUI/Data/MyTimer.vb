
Namespace ind.tools
    Public Class MyTimer
        Public stopWatch1 As New Stopwatch()
        Public ts As TimeSpan
        Public elapsedTimeDesc As String
        Public Shared timeResultFormat As String = "{0:00}:{1:00}:{2:00}.{3:000}"
        Public watchDescription As String = ""
        Public Enum WatchOptionsEnum As Byte
            stopWatch = 0
            continueWatch = 1
            reestartWatch = 2
        End Enum
        Public Sub New()

        End Sub

        Public Sub New(start As Boolean, Optional watchDescription As String = "")
            Me.watchDescription = watchDescription
            If start = True Then
                stopWatch1.Start()
            End If
        End Sub

        Public Function StopGetResult(Optional watchOption As WatchOptionsEnum = WatchOptionsEnum.stopWatch) As String
            Return StopGetResult("", watchOption)
        End Function

        Public Function StopGetResult(Optional elapsedTimeComment As String = "", Optional watchOption As WatchOptionsEnum = WatchOptionsEnum.stopWatch) As String
            stopWatch1.Stop()
            ts = stopWatch1.Elapsed
            ' Format and display the TimeSpan value. 
            elapsedTimeDesc = String.Format(timeResultFormat, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)
            Debug.WriteLine(" ElapsedTime:" & elapsedTimeDesc & "|" & Me.watchDescription & "|" & elapsedTimeComment)

            If watchOption = WatchOptionsEnum.stopWatch Then
                'nothing
            ElseIf watchOption = WatchOptionsEnum.reestartWatch Then
                stopWatch1.Restart()
            ElseIf watchOption = WatchOptionsEnum.continueWatch Then
                stopWatch1.Start()
            End If
            Return elapsedTimeDesc
        End Function

    End Class
End Namespace

