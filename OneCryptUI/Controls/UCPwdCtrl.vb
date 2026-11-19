Public Class UCPwdCtrl

    Private Sub UCPwdCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.myAppEvents_LanguageChangedEvent()
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.chkShowPwd.Text = ind.lang.UCPwdControllerLang.ChkPwdShow
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, ind.lang.UCPwdControllerLang.ChkPwdShow_TT)
        Me.ToolTip1.SetToolTip(Me.txtPassDecrypt, ind.lang.UCPwdControllerLang.txtPassDecrypt_TT)

        Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitle
        Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue
        Me.lblReenterPassword.Text = ind.lang.UCPwdControllerLang.lblReenterPassword

    End Sub

    Private Sub chkShowPwd_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPwd.CheckedChanged
        If Me.chkShowPwd.Checked = True Then
            Me.txtPassDecrypt.PasswordChar = Nothing
            Else
            Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        End If
    End Sub
End Class
