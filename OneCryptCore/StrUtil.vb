
Namespace ind.string
    Public Class StrUtil
        Public Shared Function StringAvoidNull(value As Object) As String
            If value Is Nothing Then
                Return String.Empty
            ElseIf IsDBNull(value) Then
                Return String.Empty
            Else
                Return CStr(value)
            End If
        End Function
    End Class
End Namespace


