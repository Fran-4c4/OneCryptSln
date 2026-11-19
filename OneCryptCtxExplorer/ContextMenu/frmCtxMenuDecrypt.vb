

Public Class frmCtxMenuDecrypt

    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass


    Private Sub frmCtxMenuDecrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.myAppEvents_LanguageChangedEvent()
    End Sub


    Public Sub configure(action As ind.crypt.Crypto.CryptoActionEnum)

        'common buttons
        Me.lblReenterPassword.Text = ind.lang.frmCtxMenuDecryptLang.lblReenterPassword
        Me.chkShowPwd.Text = ind.lang.frmCtxMenuDecryptLang.chkShowPwd


        If action = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
            Me.imgMain.Image = My.Resources.security_lock_64
            Me.Text = ind.lang.frmCtxMenuDecryptLang.title_Encrypt
            Me.btnContinue.Text = ind.lang.frmCtxMenuDecryptLang.btnContinue_Encrypt
            Me.btnContinue.BackColor = Drawing.Color.IndianRed
            Me.Cancel_Button.Text = ind.lang.frmCtxMenuDecryptLang.Cancel_Button
            Me.lbl_pwdTitle.Text = ind.lang.frmCtxMenuDecryptLang.lbl_pwdTitle_Encrypt
            Me.txtPwdConfirm.Visible = True
        Else
            Me.imgMain.Image = My.Resources.security_unlock_64
            Me.Text = ind.lang.frmCtxMenuDecryptLang.title_decrypt
            Me.btnContinue.Text = ind.lang.frmCtxMenuDecryptLang.btnContinue_decrypt
            Me.Cancel_Button.Text = ind.lang.frmCtxMenuDecryptLang.Cancel_Button
            Me.btnContinue.BackColor = Drawing.Color.DarkOrange
            Me.lbl_pwdTitle.Text = ind.lang.frmCtxMenuDecryptLang.lbl_pwdTitle_decrypt
            Me.txtPwdConfirm.Visible = False
        End If

    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Dim pwd As String = Me.txtPassDecrypt.Text.Trim

        If pwd = "" Then
            ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_empty_msg)
            Me.txtPassDecrypt.Focus()
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If

        Dim pwdConfirm As String = Me.txtPwdConfirm.Text.Trim

        If Me.txtPwdConfirm.Visible = True Then
            If pwdConfirm = "" Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePasswordConfirm_Pwd1_msg)
                Me.txtPwdConfirm.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

            If pwd <> pwdConfirm Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_PwdNotCoincident_msg, ind.lang.UCPwdControllerLang.ValidatePassword_msg_title)
                Me.txtPwdConfirm.Focus()
                Me.txtPwdConfirm.SelectAll()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If


        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = Cancel_Button.DialogResult
        Me.Close()
    End Sub

    Private Sub chkShowPwd_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPwd.CheckedChanged
        If Me.chkShowPwd.Checked = True Then
            Me.txtPassDecrypt.PasswordChar = Nothing
            Me.txtPwdConfirm.PasswordChar = Nothing
        Else
            Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.txtPwdConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)

        End If

    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent

    End Sub



    Private Sub txtPassDecrypt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassDecrypt.KeyDown
        Debug.WriteLine("txtPassDecrypt_KeyDown")
        If e.KeyCode = Keys.Enter Then
            Dim pwd As String = Me.txtPassDecrypt.Text.Trim
            If pwd = "" Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_empty_msg)
                Me.txtPassDecrypt.Focus()
                Exit Sub
            Else
                If Me.txtPwdConfirm.Visible = False Then
                    'we are in decrypt mode
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                    Exit Sub
                End If
            End If


            If Me.txtPwdConfirm.Visible = True Then
                'we are in encrypt mode we need 2 passwords
                Dim pwdConfirm As String = Me.txtPwdConfirm.Text.Trim

                If pwdConfirm = "" Then
                    Me.txtPwdConfirm.Focus()
                    Exit Sub
                ElseIf pwdConfirm <> "" Then
                    'compare to principal
                    If pwd = pwdConfirm Then
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                        Exit Sub
                    Else
                        ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_PwdNotCoincident_msg, ind.lang.UCPwdControllerLang.ValidatePassword_msg_title)
                        Me.txtPwdConfirm.Focus()
                        Me.txtPwdConfirm.SelectAll()
                        Exit Sub
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub txtPwdConfirm_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPwdConfirm.KeyDown
        'we are in encrypt mode we need 2 passwords
        If e.KeyCode <> Keys.Enter Then
            Exit Sub
        End If
        Dim pwdConfirm As String = Me.txtPwdConfirm.Text.Trim

        If pwdConfirm = "" Then
            ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePasswordConfirm_Pwd1_msg)
            Me.txtPwdConfirm.Focus()
            Exit Sub
        ElseIf pwdConfirm <> "" Then

            Dim pwd As String = Me.txtPassDecrypt.Text.Trim
            'compare to principal
            If pwd = pwdConfirm Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
                Exit Sub
            Else

                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_PwdNotCoincident_msg, ind.lang.UCPwdControllerLang.ValidatePassword_msg_title)
                Exit Sub
            End If
        End If
    End Sub
End Class