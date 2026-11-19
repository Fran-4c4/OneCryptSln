
Namespace ind.encrypter
    ''' <summary>
    ''' class to encapsulate generic events for all the application
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppEventsClass
        ''' <summary>
        ''' Language changed event
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Event LanguageChangedEvent()
        Public Shared Sub LanguageChangedEventRaise()
            RaiseEvent LanguageChangedEvent()
        End Sub


    End Class
End Namespace

