

''' <summary>
''' class that acts as base for windows forms in global aplication
''' </summary>
''' <remarks></remarks>
Public Class FrmAppEvents

    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass

    'Public Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent


    Private Sub FrmAppEvents_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class