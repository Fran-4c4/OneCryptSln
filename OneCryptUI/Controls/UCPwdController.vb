Imports System.Security.Cryptography

''' <summary>
''' password controller class. Show textboxes to introduce the password
''' </summary>
''' <remarks></remarks>
Public Class UCPwdController

    Public Interface IPwdSender
        Sub passwordIsCorrect(sender As Object, isCorrect As Boolean)
    End Interface
    ''' <summary>
    ''' object that need to check the password
    ''' </summary>
    ''' <remarks></remarks>
    Public senderController As IPwdSender

    ''' <summary>
    ''' load the control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UCPwdController_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        Me.txtPassDecrypt.Text = "123"
#End If

        Me.myAppEvents_LanguageChangedEvent()
        'attach the textbox password to control the strenght
        Me.PasswordStrengthControl1.txtPwdAtach = Me.txtPassDecrypt
        'Me.chkStorePwd.Checked = CBool(ind.settings.SettingsLink.storepwdCheck)
        Me.chkStorePwd.Checked = CBool(ind.settings.SettingsLink.storepwdCheck)
    End Sub


#Region "DrawBorder on text"
    Private Sub txtPassDecrypt_Enter(sender As Object, e As EventArgs) Handles txtPassDecrypt.Enter
        Me.pnl1.Visible = True
        Me.pnl2.Visible = False
    End Sub

    Private Sub txtPassDecrypt_Leave(sender As Object, e As EventArgs) Handles txtPassDecrypt.Leave
        Me.pnl1.Visible = False
    End Sub

    Private Sub txtPwdConfirm_Enter(sender As Object, e As EventArgs) Handles txtPwdConfirm.Enter
        Me.pnl1.Visible = False
        Me.pnl2.Visible = True
    End Sub

    Private Sub txtPwdConfirm_Leave(sender As Object, e As EventArgs) Handles txtPwdConfirm.Leave
        Me.pnl2.Visible = False
    End Sub

    'Dim p As New Pen(Brushes.Aqua)
    'Dim penClean As New Pen(Brushes.White)
    'Private Sub drawBorderForFocus(oX As Integer, oY As Integer, oWidth As Integer, oHeigth As Integer, Optional deleteBorder As Boolean = False)
    '    Dim pnlGraph As Graphics = Me.PnlPassword.CreateGraphics
    '    Dim w As Integer = 3
    '    If deleteBorder = False Then
    '        p.Width = w
    '        pnlGraph.DrawRectangle(p, oX - w, oY - w, oWidth + w, oHeigth + w)
    '    Else
    '        penClean.Width = w
    '        pnlGraph.DrawRectangle(penClean, oX - w, oY - w, oWidth + w, oHeigth + w)
    '    End If
    'End Sub

#End Region
    ''' <summary>
    ''' validate password
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePassword() As Boolean
        Try
            If Me.txtPassDecrypt.Text.Trim = "" Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_empty_msg)
                Me.txtPassDecrypt.Focus()
                Return False
            End If

            If Me.txtPassDecrypt.Text.Trim <> "" Then
                ind.cte.AppVars.AppPassword = ind.settings.SettingsLink.storedpwd.Trim
                If ind.cte.AppVars.AppPassword = "" Then
                    Me.txtPwdConfirm.Focus()
                    Return False
                Else
                    'validate
                    Dim crp As New ind.crypt.Crypto(Me.txtPassDecrypt.Text.Trim)
                    Dim AppPasswordForCheck As String = crp.DecryptData(ind.cte.AppVars.AppPassword)

                    If ind.cte.AppConst.check123 <> AppPasswordForCheck Then
                        ind.cte.AppVars.UserValidated = False
                        ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_msg, ind.lang.UCPwdControllerLang.ValidatePassword_msg_title)
                        Return False
                    End If

                    If Me.pwdAction = pwdActionEnum.normalProcess Then
                        ind.cte.AppVars.AppPassword = Me.txtPassDecrypt.Text.Trim
                        ind.cte.AppVars.UserValidated = True
                        Me.txtPassDecrypt.Text = ""
                        Me.txtPwdConfirm.Text = ""
                        senderController.passwordIsCorrect(Me, True)
                        Return True
                    ElseIf Me.pwdAction = pwdActionEnum.changepwd Then
                        'pass to second step, set the new password 
                        Me.txtPassDecrypt.Text = ""
                        Me.txtPassDecrypt.Focus()
                        Me.txtPwdConfirm.Visible = True
                        Me.lblInfoPwd.Visible = True
                        Me.lblReenterPassword.Visible = True
                        Me.btnDeletePwd.Visible = False
                        Me.lblInfoPwd.Visible = False
                        Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleNewPwd
                        Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue_changepwd
                        Me.ParentForm.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleNewPwd

                        Return True
                    End If


                End If

            End If
        Catch exCrypt As CryptographicException
            ind.win.Msg.mError(ind.lang.UCPwdControllerLang.ValidatePassword_Wrong_msg)

        Catch ex As Exception
            ind.win.Msg.mError(ex.Message)
        End Try

        Return False
    End Function

    Private Sub txtPassDecrypt_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPassDecrypt.KeyDown
        Try
            If e.KeyValue = Keys.Enter Then
                If Me.txtPwdConfirm.Visible = False Then
                    bContinue()
                    e.Handled = True
                ElseIf Me.txtPwdConfirm.Visible = True Then
                    Me.txtPwdConfirm.Focus()
                    e.Handled = True
                End If
            Else
                ind.crypt.PasswordQuality.ValidatePasswordQuality(Me.txtPassDecrypt.Text.Trim)
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try


    End Sub

    Private Sub txtPwdConfirm_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPwdConfirm.KeyDown
        Try

            If e.KeyValue = Keys.Enter Then
                bContinue()
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Function ValidatePasswordConfirm() As Boolean
        Try
            If Me.txtPassDecrypt.Text.Trim = "" Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_empty_msg)
                Me.txtPassDecrypt.Focus()
                Return False
            End If

            If Me.txtPwdConfirm.Text.Trim = "" Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePasswordConfirm_Pwd1_msg)
                Me.txtPwdConfirm.Focus()
                Return False
            End If

            If Me.txtPassDecrypt.Text.Trim <> Me.txtPwdConfirm.Text.Trim Then
                ind.win.Msg.mWarning(ind.lang.UCPwdControllerLang.ValidatePassword_PwdNotCoincident_msg, ind.lang.UCPwdControllerLang.ValidatePassword_msg_title)
                Return False
            Else
                'coincide
                ind.cte.AppVars.AppPassword = Me.txtPassDecrypt.Text.Trim
                ind.cte.AppVars.UserValidated = True
                Me.txtPassDecrypt.Text = ""
                Me.txtPwdConfirm.Text = ""


                If Me.chkStorePwd.Checked = True Then
                    'store password encrypted in config
                    Dim crp As New ind.crypt.Crypto(ind.cte.AppVars.AppPassword)
                    Dim checkPwdEncrypted As String = crp.enCryptData(ind.cte.AppConst.check123)
                    ind.settings.SettingsLink.storedpwd = checkPwdEncrypted
                End If
                If Me.pwdAction = pwdActionEnum.normalProcess Then
                    senderController.passwordIsCorrect(Me, True)
                End If
                Return True
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex.Message)
        Finally

        End Try

        Return False
    End Function

    Private Sub chkStorePwd_CheckedChanged(sender As Object, e As EventArgs) Handles chkStorePwd.CheckedChanged
        If chkStorePwd.Checked = False Then
            Dim storedPwd As String = ind.settings.SettingsLink.storedpwd.Trim
            If storedPwd <> "" Then
                Dim dlgRes As DialogResult = ind.win.Msg.mQuestion(ind.lang.UCPwdControllerLang.ValidatePasswordConfirm_PwdStore_msg & vbCr & ind.lang.UCPwdControllerLang.ValidatePasswordConfirm_PwdStore1_msg)
                If dlgRes <> Windows.Forms.DialogResult.OK Then
                    chkStorePwd.Checked = True
                Else
                    ind.settings.SettingsLink.storedpwd = ""
                    Me.validatePwdConfiguration()
                End If
            End If
            ind.settings.SettingsLink.storepwdCheck = chkStorePwd.Checked
        Else
            ind.settings.SettingsLink.storepwdCheck = chkStorePwd.Checked
        End If
    End Sub

    ''' <summary>
    ''' Configure the control to use in main form
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub validatePwdConfiguration()
        Dim pwdEncrypted As String = ind.settings.SettingsLink.storedpwd.Trim
        If pwdEncrypted = "" Then
            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleChoose
            Me.txtPwdConfirm.Visible = True
            Me.lblInfoPwd.Visible = True
            Me.lblReenterPassword.Visible = True
            Me.btnDeletePwd.Visible = False
            'Me.drawBorderForFocus(Me.txtPassDecrypt.Left, Me.txtPassDecrypt.Top, Me.txtPassDecrypt.Width, Me.txtPassDecrypt.Height)
            Me.txtPassDecrypt.Focus()
        Else
            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitle
            Me.txtPwdConfirm.Visible = False
            Me.lblInfoPwd.Visible = False
            Me.lblReenterPassword.Visible = False
            Me.btnDeletePwd.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Configure the control to use in main form
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub configForPwdOnly()

            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitle
            Me.txtPwdConfirm.Visible = False
            Me.lblReenterPassword.Visible = False
            Me.txtPwdConfirm.Visible = False
            Me.lblInfoPwd.Visible = False
            Me.lblReenterPassword.Visible = False
            Me.chkStorePwd.Visible = False
            Me.btnDeletePwd.Visible = False
            Me.txtPassDecrypt.Focus()
    End Sub

    Private Sub UCPwdController_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            Me.txtPassDecrypt.Focus()
        End If

    End Sub

    ''' <summary>
    ''' continue button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        Me.bContinue()

    End Sub
    ''' <summary>
    ''' control action to do after user press continue button
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub bContinue()

        If Me.pwdAction = pwdActionEnum.normalProcess Then
            If Me.txtPwdConfirm.Visible = True Then
                Me.ValidatePasswordConfirm()
            Else
                Me.ValidatePassword()
            End If
        ElseIf Me.pwdAction = pwdActionEnum.changepwd Then
            Dim result As Boolean = Me.ValidatePassword()
            If result = True Then
                Me.pwdAction = pwdActionEnum.setNewPassword
            End If
        ElseIf Me.pwdAction = pwdActionEnum.setNewPassword Then
            Dim result As Boolean = Me.ValidatePasswordConfirm()
            If result = True Then
                Me.btnContinue.DialogResult = DialogResult.OK
                Me.ParentForm.DialogResult = DialogResult.OK
            End If
        End If

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

#Region "CHANGE PASSWORD"

    Dim pwdAction As pwdActionEnum = pwdActionEnum.normalProcess

    Private Enum pwdActionEnum As Byte
        normalProcess = 0
        changepwd = 1
        setNewPassword = 2
    End Enum


    Public Shared Sub changePassword()
        Try
            Dim msg As String = ind.lang.UCPwdControllerLang.MessagePwdChange
            Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(msg, ind.lang.UCPwdControllerLang.lbl_pwdTitleNewPwd, ind.win.Msg.MessageIconEnum.Warning, frmDlg.FocusButtonEnum.cancelBtn)
            fd.setFocusOnBtn(frmDlg.FocusButtonEnum.cancelBtn)
            If fd.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If

            Dim f As New Form
            f.Icon = My.Resources.lock_server
            f.FormBorderStyle = FormBorderStyle.FixedDialog
            f.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleNewPwd
            f.MinimizeBox = False
            f.MaximizeBox = False
            Dim ucpwd As New UCPwdController
            ucpwd.pwdAction = pwdActionEnum.changepwd

            f.Width = ucpwd.Width + 10
            f.Height = ucpwd.Height + 20

            ucpwd.Dock = DockStyle.Fill
            ucpwd.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleOldPwd
            ucpwd.validatePwdConfiguration()
            f.Controls.Add(ucpwd)

            ucpwd.chkStorePwd.Visible = False
            ucpwd.btnDeletePwd.Visible = False
            ucpwd.lblInfoPwd.Visible = True
            ucpwd.Visible = True


            Dim dr As DialogResult = f.ShowDialog()


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub
#End Region

    Public Sub deletePwdStored()
        Try
            Dim dlgRes As DialogResult = ind.win.Msg.mQuestion(ind.lang.UCPwdControllerLang.DeletePassword1_msg & vbCr & ind.lang.UCPwdControllerLang.DeletePassword2_msg)
            If dlgRes = Windows.Forms.DialogResult.OK Then
                ind.settings.SettingsLink.storedpwd = ""
                Me.chkStorePwd.Checked = False
                validatePwdConfiguration()
                senderController.passwordIsCorrect(Me, False)

                Me.Visible = True
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnDeletePwd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnDeletePwd.LinkClicked
        Me.deletePwdStored()
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.btnDeletePwd.Text = ind.lang.UCPwdControllerLang.btnDeletePwd
        Me.ToolTip1.SetToolTip(Me.PasswordStrengthControl1, ind.lang.UCPwdControllerLang.PasswordStrengthControl1_TT)
        Me.chkShowPwd.Text = ind.lang.UCPwdControllerLang.ChkPwdShow
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, ind.lang.UCPwdControllerLang.ChkPwdShow_TT)
        Me.ToolTip1.SetToolTip(Me.txtPassDecrypt, ind.lang.UCPwdControllerLang.txtPassDecrypt_TT)
        Me.ToolTip1.SetToolTip(Me.txtPwdConfirm, ind.lang.UCPwdControllerLang.txtPwdConfirm_TT)
        Me.chkStorePwd.Text = ind.lang.UCPwdControllerLang.chkPwdStore
        Me.ToolTip1.SetToolTip(Me.chkStorePwd, ind.lang.UCPwdControllerLang.chkPwdStore_TT)

        Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue
        Me.lblReenterPassword.Text = ind.lang.UCPwdControllerLang.lblReenterPassword
        Me.lblInfoPwd.Text = ind.lang.UCPwdControllerLang.lblInfoPwd

        If Me.pwdAction = pwdActionEnum.normalProcess Then
            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitle
            Me.lblInfoPwd.Text = ind.lang.UCPwdControllerLang.lblInfoPwd
            Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue
        ElseIf Me.pwdAction = pwdActionEnum.changepwd Then
            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleOldPwd
            Me.lblInfoPwd.Text = ind.lang.UCPwdControllerLang.lblInfoPwdChangePwd
            Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue
        ElseIf Me.pwdAction = pwdActionEnum.setNewPassword Then
            Me.lbl_pwdTitle.Text = ind.lang.UCPwdControllerLang.lbl_pwdTitleNewPwd
            Me.lblInfoPwd.Text = ind.lang.UCPwdControllerLang.lblInfoPwdChangePwd
            Me.btnContinue.Text = ind.lang.UCPwdControllerLang.btnContinue_changepwd
        End If

        Dim culture As String = Globalization.CultureInfo.CurrentUICulture.Name 'use this for user interface messages

        If ind.settings.SettingsLink.LanguageSelected = "en-US" Then
            LblLnkLanguage.Text = "Español"
        Else
            LblLnkLanguage.Text = "English"
        End If

    End Sub

    Private Sub LabelLnkLanguage_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LblLnkLanguage.LinkClicked

        Dim culture As String = Globalization.CultureInfo.CurrentUICulture.Name 'use this for user interface messages

        If LblLnkLanguage.Text = "Español" Then
            LblLnkLanguage.Text = "Español"
            ind.settings.SettingsLink.LanguageSelected = "es-ES"
        Else
            LblLnkLanguage.Text = "English"
            ind.settings.SettingsLink.LanguageSelected = "en-US"

        End If
    End Sub
End Class
