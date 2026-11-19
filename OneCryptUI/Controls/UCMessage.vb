

''' <summary>
''' Control to show messages to user in runtime
''' </summary>
Public Class UCMessage

    Private WithEvents aTimer As New System.Timers.Timer

    Public messages As Integer = 0



    Private Sub aTimer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles aTimer.Elapsed
        aTimer.Enabled = False
        changeVisibility()

    End Sub

    Private Sub changeVisibility()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf changeVisibility))
        Else
            Me.Visible = False
        End If
    End Sub


    Public Sub setMessage(message As String)
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(Sub() setMessage(message)))
        End If
        messages += 1
        Me.lblMessage.Text = message
        If Me.Visible = False And Me.aTimer.Enabled = False Then
            Me.Visible = True
            Me.BringToFront()
            ' Create a timer with a 3 second interval.
            aTimer.Interval = 2000
            aTimer.Start()
        Else
            aTimer.Interval = 3000
        End If
    End Sub
End Class
