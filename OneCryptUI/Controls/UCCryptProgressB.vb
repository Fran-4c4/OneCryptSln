Public Class UCCryptProgressB

    Public BackgroundWorker1 As System.ComponentModel.BackgroundWorker

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        If Not Me.BackgroundWorker1 Is Nothing Then
            Me.BackgroundWorker1.CancelAsync()
        Else
            Me.Dispose()
        End If
    End Sub

    Private Sub UCCryptProgress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myAppEvents_LanguageChangedEvent()
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Try
            Me.lbl_Title.Text = ind.lang.UCCryptProgressLang.lbl_Title_encrypt
            Me.Cancel_Button.Text = ind.lang.UCCryptProgressLang.Cancel_Button

        Finally

        End Try

    End Sub

End Class
