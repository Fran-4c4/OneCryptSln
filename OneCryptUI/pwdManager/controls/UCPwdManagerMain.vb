


Public Class UCPwdManagerMain
#Region "1. Variables "
    Friend WithEvents myAppEvents As New ind.encrypter.AppEventsClass
    Friend WithEvents lstViewEnt As ListView
#End Region

    Private Sub UCPwdManagerMain_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.myAppEvents_LanguageChangedEvent()
        Me.lstViewEnt = Me.UCpwdListView1.lstViewEnt
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent


    End Sub



   

   
End Class
