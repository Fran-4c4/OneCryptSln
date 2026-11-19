

Namespace ind.exceptions

    ''' <summary>
    ''' exception when access to a file opened by another application
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable>
    Public Class FileInUseException
        Inherits ApplicationException

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        Public Sub New(ByVal message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        '''This constructor is needed for serialization.
        ''' </summary>
        ''' <param name="info"></param>
        ''' <param name="context"></param>
        ''' <remarks></remarks>
        Protected Sub New(info As Runtime.Serialization.SerializationInfo, context As Runtime.Serialization.StreamingContext)
            MyBase.New(info:=info, context:=context)
        End Sub

    End Class
End Namespace