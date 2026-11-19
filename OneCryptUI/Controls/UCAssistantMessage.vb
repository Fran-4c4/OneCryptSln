Public Class UCAssistantMessage
    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass



    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Try
            Me.lbl_Title.Text = ind.lang.UCCryptExplorerLang.UCAssistantMessage_lbl_title
            Me.lblDragFromExplorerTop.Text = ind.lang.UCCryptExplorerLang.UCAssistantMessage_lblDragFromExplorerTop
            Me.lbl_or.Text = ind.lang.UCCryptExplorerLang.UCAssistantMessage_lbl_or
            Me.lblDragFromExplorerInfoDown.Text = ind.lang.UCCryptExplorerLang.UCAssistantMessage_lblDragFromExplorerInfoDown
        Finally

        End Try

    End Sub

    Private Sub UCAssistantMessage_Load(sender As Object, e As EventArgs) Handles Me.Load
        myAppEvents_LanguageChangedEvent()
    End Sub
End Class
