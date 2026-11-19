



Public Class FrmEncryptText

    'Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass

    Private Sub FrmEncryptText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.myAppEvents_LanguageChangedEvent()
    End Sub

    Private Sub btnCrypt_Click(sender As Object, e As EventArgs) Handles btnCrypt.Click
        Try
            If Me.RichTextBox1.Text.Length = 0 Then
                ind.win.Msg.mError(ind.lang.frmEncryptTextLang.messageNeededtxtToEncrypt)
                Exit Sub
            End If
            Dim txtResult As String
            Dim txtToCrypt As String = Me.RichTextBox1.Text
            Dim pwd As String = Me.txtPassDecrypt.Text.Trim
            If pwd = "" Then
                pwd = ind.cte.AppVars.AppPassword
            End If

            Dim crypter As New ind.crypt.Crypto(pwd)
            txtResult = crypter.enCryptData(txtToCrypt)
            Me.RichTextBox2.Text = txtResult
            Me.lblTitleResult.Text = ind.lang.frmEncryptTextLang.lblTitleResult
            Me.pnlResult.Visible = True
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnDecrypt_Click(sender As Object, e As EventArgs) Handles btnDecrypt.Click
        Try
            If Me.RichTextBox1.Text.Length = 0 Then
                ind.win.Msg.mError(ind.lang.frmEncryptTextLang.messageNeededtxtToDecrypt)
                Exit Sub
            End If
            Dim txtResult As String
            Dim txtToCrypt As String = Me.RichTextBox1.Text
            Dim pwd As String = Me.txtPassDecrypt.Text.Trim
            If pwd = "" Then
                pwd = ind.cte.AppVars.AppPassword
            End If
            Dim crypter As New ind.crypt.Crypto(pwd)
            txtResult = crypter.DecryptData(txtToCrypt)
            Me.RichTextBox2.Text = txtResult
            Me.lblTitleResult.Text = ind.lang.frmEncryptTextLang.lblTitleResult
            Me.pnlResult.Visible = True

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub



    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.btnCrypt.Text = ind.lang.frmEncryptTextLang.btnCrypt
        Me.btnDecrypt.Text = ind.lang.frmEncryptTextLang.btnDecrypt
        Me.btnLinkCopy2Clipboard.Text = ind.lang.frmEncryptTextLang.copytoclipboard
        Me.Text = ind.lang.frmEncryptTextLang.formText
        Me.lbl_textTitle.Text = ind.lang.frmEncryptTextLang.lbl_textTitle
        Me.lbl_Title.Text = ind.lang.frmEncryptTextLang.lbl_Title
        Me.lblTitleResult.Text = ind.lang.frmEncryptTextLang.lblTitleResult
        Me.btnSendAutotype.Text = ind.lang.frmEncryptTextLang.btnSendAutotype
        Me.ToolTip1.SetToolTip(Me.btnSendAutotype, ind.lang.frmEncryptTextLang.btnSendAutotype_tt)


        Me.lblPassword.Text = ind.lang.frmEncryptTextLang.lblPassword
        Me.lblDejaloEnblanco.Text = ind.lang.frmEncryptTextLang.lblDejaloEnblanco
        Me.chkShowPwd.Text = ind.lang.UCPwdControllerLang.ChkPwdShow
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, ind.lang.UCPwdControllerLang.ChkPwdShow_TT)
        
    End Sub

    Private Sub btnLinkClear_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnLinkClear.LinkClicked
        Me.RichTextBox1.Text = ""
    End Sub

    Private Sub ctxPaste_Click(sender As Object, e As EventArgs) Handles ctxPaste.Click
        Try
            If Clipboard.ContainsText Then
                Dim rh As RichTextBox = Me.RichTextBox1
                rh.AppendText(My.Computer.Clipboard.GetText)
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Private Sub ctxCopyAll_Click(sender As Object, e As EventArgs) Handles ctxCopyAll.Click
        Try

            Dim rh As RichTextBox = Me.RichTextBox1
            If rh.Text.Length > 0 Then
                My.Computer.Clipboard.SetText(rh.Text)
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub ctxCopySelectedText_Click(sender As Object, e As EventArgs) Handles ctxCopySelectedText.Click
        Try

            Dim rh As RichTextBox = Me.RichTextBox1
            If rh.SelectedText.Length > 0 Then
                My.Computer.Clipboard.SetText(rh.SelectedText)
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnLinkCopy2Clipboard_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnLinkCopy2Clipboard.LinkClicked
        Try
            My.Computer.Clipboard.SetText(Me.RichTextBox2.Text)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub ctxCopy_Click(sender As Object, e As EventArgs) Handles ctxCopyResults.Click
        Try
            My.Computer.Clipboard.SetText(Me.RichTextBox2.Text)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub



    Private Sub btnSend_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnSendAutotype.LinkClicked
        Try

            KeePass.Util.HotKeyManager.Initialize(Me.ParentForm)
            Dim pe As New KeePassLib.PwEntryObj
            pe.AutoTypeSequence = "{" & KeePassLib.PwDefs.NotesField & "}"
            pe.Strings.Set(KeePassLib.PwDefs.NotesField, New KeePassLib.Security.ProtectedString(Me.RichTextBox2.Text))

            If Me.ParentForm Is Nothing Then
                KeePass.Util.AutoType.PerformIntoPreviousWindow(Me, pe, Nothing)
            Else
                KeePass.Util.AutoType.PerformIntoPreviousWindow(Me.ParentForm, pe, Nothing)
            End If


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub chkShowPwd_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPwd.CheckedChanged
        If Me.chkShowPwd.Checked = True Then
            Me.txtPassDecrypt.PasswordChar = Nothing
        Else
            Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        End If
    End Sub
End Class
