''' <summary>
''' Detects two Control-key releases within a short interval. This class owns
''' the shortcut timing contract so the native hook remains a thin adapter.
''' </summary>
Public NotInheritable Class ControlDoublePressDetector

    ''' <summary>Maximum time between Control releases, in milliseconds.</summary>
    Public Const MaximumIntervalMilliseconds As Integer = 350

    Private lastControlReleaseTicks As Nullable(Of Long)

    Public Function RegisterKeyUp(virtualKey As Integer, utcTicks As Long) As Boolean
        If Not IsControlKey(virtualKey) Then
            Reset()
            Return False
        End If

        If lastControlReleaseTicks.HasValue Then
            Dim elapsedTicks = utcTicks - lastControlReleaseTicks.Value
            Reset()
            Return elapsedTicks >= 0 AndAlso
                   elapsedTicks <= TimeSpan.FromMilliseconds(MaximumIntervalMilliseconds).Ticks
        End If

        lastControlReleaseTicks = utcTicks
        Return False
    End Function

    Public Sub Reset()
        lastControlReleaseTicks = Nothing
    End Sub

    Private Shared Function IsControlKey(virtualKey As Integer) As Boolean
        Return virtualKey = &H11 OrElse virtualKey = &HA2 OrElse virtualKey = &HA3
    End Function

End Class
