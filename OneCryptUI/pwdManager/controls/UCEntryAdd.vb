


''' <summary>
''' class to control entry
''' </summary>
''' <remarks></remarks>
Public Class UCEntryAdd

    Public editMode As editModeEnum
    Public Enum editModeEnum As Byte
        newMode
        editMode
        readOnlyMode
    End Enum

    Private entryLviEx As ind.pwd.ListViewItemEx

    Public Property entryObj As ind.pwd.ListViewItemEx
        Get
            Return Me.entryLviEx
        End Get
        Set(value As ind.pwd.ListViewItemEx)
            Me.editMode = editModeEnum.editMode
            Me.entryLviEx = value
            Me.SetEntryToControl()
        End Set
    End Property

    Private Sub UCEntryAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.imgTitle.Image = My.Resources.pwd_user_add_32
        Me.imgUrl.Image = My.Resources.pwdManager_browser_16
        Me.imgKeyboard.Image = My.Resources.key_16
        Me.myAppEvents_LanguageChangedEvent()

        Me.txtEntryPwd.TxtBoxObj.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordStrengthControl1.txtPwdAtach = Me.txtEntryPwd.TxtBoxObj
        Me.txtEntryAutoType.TxtBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.txtEntryAutoType.TextBox1.ReadOnly = True

        Me.configGroups()
        'if control is set before load we need to atach the control again for combo
        If Not Me.entryObj Is Nothing Then
            Me.SetEntryToControl()
        End If
        AddHandler ind.encrypter.AppEventsClass.LanguageChangedEvent, AddressOf Me.myAppEvents_LanguageChangedEvent
    End Sub

    Public Sub SetEntryToControl()
        If Me.entryObj Is Nothing Then
            Exit Sub
        End If
        Me.txtEntryName.TextBoxText = Me.entryObj.entryName
        Me.txtEntryUserName.TextBoxText = Me.entryObj.UserName
        Me.txtEntryPwd.TextBoxText = Me.entryObj.Password
        Me.txtEntryURL.TextBoxText = Me.entryObj.Url
        Me.txtEntryAutoType.TextBoxText = Me.entryObj.AutoTypeSequence
        Me.rhNotes.Text = Me.entryObj.Notes
        Me.cboGroup.SelectedValue = CInt(Me.entryObj.GroupPwdVal)
        Me.setUrlBoxLabel(Me.entryObj.GroupPwdVal)
    End Sub

    Private Class CboGroupClass
        Public Sub New(ByVal id As Integer, ByVal name As String)
            mID = id
            mName = name
        End Sub

        Private mID As Integer
        Public Property ID() As Integer
            Get
                Return mID
            End Get
            Set(ByVal value As Integer)
                mID = value
            End Set
        End Property

        Private mName As String
        Public Property Name() As String
            Get
                Return mName
            End Get
            Set(ByVal value As String)
                mName = value
            End Set
        End Property
    End Class

    Private Sub configGroups()
        Dim cboItems = New List(Of CboGroupClass)
        'listview groups
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.generic, ind.lang.UCpwdListView.listViewGroup_Generic))
        Dim selecteditem As CboGroupClass = New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Websites, ind.lang.UCpwdListView.listViewGroup_Website)
        cboItems.Add(selecteditem)
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Email, ind.lang.UCpwdListView.listViewGroup_email))
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.computer, ind.lang.UCpwdListView.listViewGroup_Computer))
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.bank, ind.lang.UCpwdListView.listViewGroup_Bank))
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.creditcard, ind.lang.UCpwdListView.listViewGroup_CreditCard))
        cboItems.Add(New CboGroupClass(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.other, ind.lang.UCpwdListView.listViewGroup_Other))

        Me.cboGroup.DataSource = cboItems
        Me.cboGroup.ValueMember = "id"
        Me.cboGroup.DisplayMember = "name"
        Me.cboGroup.SelectedItem = selecteditem
    End Sub

    Private Function getSelectedGroup() As CboGroupClass
        Dim obj As CboGroupClass = DirectCast(Me.cboGroup.SelectedItem, CboGroupClass)
        Return obj
    End Function


    Public Shared Function OpenAsForm(ByRef entryAddCtrl As UCEntryAdd) As Form
        If entryAddCtrl Is Nothing Then
            entryAddCtrl = New UCEntryAdd
            entryAddCtrl.Visible = True
        End If
        Dim f As New Form
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.Icon = My.Resources.lock_server
        f.Text = ind.lang.UCpwdMngLang.formAddEntryText
        f.Width = entryAddCtrl.Width
        f.Height = entryAddCtrl.Height + 30
        f.MaximizeBox = False
        f.MinimizeBox = False
        f.StartPosition = FormStartPosition.CenterParent
        f.CancelButton = entryAddCtrl.btnCancel
        f.AcceptButton = entryAddCtrl.btnOk
        f.Controls.Add(entryAddCtrl)
        entryAddCtrl.Visible = True
        Return f
    End Function

    Public Shared testX As Integer = 20
    Public Sub test()
        testX += 1
        Me.txtEntryName.TextBoxText = "entry" & testX
        Me.txtEntryUserName.TextBoxText = "user" & testX
        Me.txtEntryPwd.TextBoxText = "pwd" & testX
        Me.txtEntryURL.TextBoxText = "url" & testX
        Me.rhNotes.Text = "notes" & testX
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim result As Boolean = True

        Try
            If Me.txtEntryName.TextBoxText = "" Then
                result = False
                ind.win.Msg.mError(ind.lang.UCpwdMngLang.formAddEntryText)
                Me.txtEntryName.Focus()
                Exit Sub
            End If
            'If Me.txtEntryUserName.TextBoxText = "" Then
            '    result = False
            '    ind.win.Msg.mError(ind.lang.UCpwdMngLang.formAddEntryText)
            '    Me.txtEntryUserName.Focus()
            '    Exit Sub
            'End If
            'If Me.txtEntryPwd.TextBoxText.Trim = "" Then
            '    result = False
            '    ind.win.Msg.mError(ind.lang.UCpwdMngLang.formAddEntryText)
            '    Me.txtEntryPwd.Focus()
            '    Exit Sub
            'End If

            If Me.editMode = editModeEnum.newMode Then
                Me.entryLviEx = New ind.pwd.ListViewItemEx
                Me.entryObj.configLViEmpty(Me.txtEntryName.TextBoxText.Trim)
                Me.entryObj.creationDate = Date.Now
            End If


            Me.entryObj.entryName = Me.txtEntryName.TextBoxText.Trim
            Me.entryObj.UserName = Me.txtEntryUserName.TextBoxText.Trim
            Me.entryObj.Password = Me.txtEntryPwd.TextBoxText.Trim
            Me.entryObj.Url = Me.txtEntryURL.TextBoxText.Trim
            Me.entryObj.AutoTypeSequence = Me.txtEntryAutoType.TextBoxText.Trim
            Me.entryObj.modificationDate = Date.Now
            Me.entryObj.Notes = Me.rhNotes.Text
            Me.entryObj.GroupPwdVal = CType(Me.getSelectedGroup.ID, ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
            If Me.editMode = editModeEnum.editMode Then
                Me.entryObj.Group = Me.entryObj.ListView.Groups(Me.entryObj.groupPwd)
            End If

            Me.entryObj.ImageIndex = Me.entryObj.groupPwd

            'Throw New Exception("test err")

            Me.ParentForm.DialogResult = DialogResult.OK

        Catch ex As Exception
            ind.win.Msg.mError(ex)
            result = False
        Finally
            If result = False Then

            End If
        End Try
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent()
        If editMode = editModeEnum.newMode Then
            Me.lbl_Title.Text = ind.lang.UCpwdMngLang.UCAddEntry_Title_ForNew
            Me.lblTitlesub.Text = ind.lang.UCpwdMngLang.UCAddEntry_TitleSub_ForNew
        ElseIf editMode = editModeEnum.editMode Then
            Me.lbl_Title.Text = ind.lang.UCpwdMngLang.UCAddEntry_Title_ForEdit
            Me.lblTitlesub.Text = ind.lang.UCpwdMngLang.UCAddEntry_TitleSub_ForEdit
        End If


        Me.txtEntryName.labelBox = ind.lang.UCpwdMngLang.txtEntryName
        Me.lblEntryGroup.Text = ind.lang.UCpwdMngLang.lblEntryGroup
        Me.txtEntryUserName.labelBox = ind.lang.UCpwdMngLang.txtEntryUserName
        Me.txtEntryPwd.labelBox = ind.lang.UCpwdMngLang.txtEntryPwd
        Me.txtEntryURL.labelBox = ind.lang.UCpwdMngLang.txtEntryURL
        Me.lblNotes.Text = ind.lang.UCpwdMngLang.lblNotes
        Me.chkShowPwd.Text = ind.lang.UCpwdMngLang.chkShowPwd

        Me.btnOk.Text = ind.lang.UCpwdMngLang.UCAddEntry_btn_ok
        Me.btnCancel.Text = ind.lang.UCpwdMngLang.UCAddEntry_btn_cancel

        'autotype

        Me.ToolTip1.SetToolTip(Me.imgCleanAutotypeField, ind.lang.UCpwdMngLang.imgCleanAutotypeField_TT)
        Me.ToolTip1.SetToolTip(Me.imgKeyboard, ind.lang.UCpwdMngLang.imgKeyboard_TT)
        Me.ToolTip1.SetToolTip(Me.txtEntryAutoType.TxtBoxObj, ind.lang.UCpwdMngLang.txtEntryAutoType_tt)
        Me.lblPnlAutotypeTitle.Text = ind.lang.UCpwdMngLang.lblPnlAutotypeTitle
        Me.lblPnlAutotypeTitleSub.Text = ind.lang.UCpwdMngLang.lblPnlAutotypeTitleSub

    End Sub

    Private Sub chkShowPwd_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPwd.CheckedChanged
        If Me.chkShowPwd.Checked = True Then
            Me.txtEntryPwd.TxtBoxObj.PasswordChar = Nothing
        Else
            Me.txtEntryPwd.TxtBoxObj.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        End If
    End Sub

    Private Sub cboGroup_Enter(sender As Object, e As EventArgs) Handles cboGroup.Enter
        Me.pnlCboGroup.Visible = True
    End Sub

    Private Sub cboGroup_Leave(sender As Object, e As EventArgs) Handles cboGroup.Leave
        Me.pnlCboGroup.Visible = False
    End Sub

    Private Sub cboGroup_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cboGroup.SelectionChangeCommitted
        Dim id As ind.pwd.pwdManagerGlobal.pwdGroupsEnum = CType(Me.getSelectedGroup.ID, ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
        setUrlBoxLabel(id)
    End Sub

    Private Sub setUrlBoxLabel(id As ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
        Select Case id
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Email
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURLAsEmail
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Websites
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURL
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.computer
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURLAsComputer
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.bank
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURLAsBank
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.creditcard
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURLAsCreditCard
            Case ind.pwd.pwdManagerGlobal.pwdGroupsEnum.generic, ind.pwd.pwdManagerGlobal.pwdGroupsEnum.other
                Me.txtEntryURL.lbl.Text = ind.lang.UCpwdMngLang.txtEntryURLAsGeneric
        End Select
    End Sub

#Region "Autotype"
    Private Sub imgClean_Click(sender As Object, e As EventArgs) Handles imgCleanAutotypeField.Click
        Me.txtEntryAutoType.TextBoxText = ""
    End Sub

    Private Sub imgKeyboard_Click(sender As Object, e As EventArgs) Handles imgKeyboard.Click, btnCloseSendKeys.Click
        Me.pnlAutotype.Visible = Not Me.pnlAutotype.Visible
        If Me.pnlAutotype.Visible = True Then
            SetSpecialKeysToRichTextBox()
        End If
    End Sub

    Private specialKeysSet As Boolean = False
    Private Const VkcBreak As String = "<break />"
    Private Sub SetSpecialKeysToRichTextBox()
        If specialKeysSet = True Then
            Exit Sub
        End If
        Using rb = New RtfBuilder(MyBase.Font, MyBase.ForeColor)

            With rb
                .Append(ind.lang.UCpwdMngLang.autotypeMesage_instrucctions, FontStyle.Bold)
                .AppendLine(1)
                rb.AppendLine(ind.lang.UCpwdMngLang.autotypeMesage1, FontStyle.Regular)
                rb.AppendLine(ind.lang.UCpwdMngLang.autotypeMesage2, FontStyle.Regular)
                '//new font gets added automatically
                .AppendLine(1)
                .Append(ind.lang.UCpwdMngLang.autotypeMesage_Standardfields, FontStyle.Bold)
                .AppendLine(1)
                rb.Append("{username} ", Color.Blue)
                rb.Append("{password} ", Color.Blue)


                .AppendLine(1)
                .AppendLine(ind.lang.UCpwdMngLang.autotypeMesage_Specialkeys_common, FontStyle.Bold)
                .AppendLine()
                Dim vSpecialKeyCodes As String() = New String() {"TAB", "ENTER", _
                    "SPACE", VkcBreak, "BACKSPACE", "BREAK", "CAPSLOCK", "ESC"}
                SetKeysToRichTextBox(rb, vSpecialKeyCodes)

                .AppendLine(1)
                .AppendLine(ind.lang.UCpwdMngLang.autotypeMesage_Specialkeys, FontStyle.Bold)
                .AppendLine()
                Dim vSpecialKeyCodes2 As String() = New String() {"UP", "DOWN", "LEFT", "RIGHT", _
                    "HOME", "END", "PGUP", "PGDN", "INSERT", "DELETE", _
                    "SPACE", VkcBreak, "BACKSPACE", "BREAK", "CAPSLOCK", "ESC", _
                    "WIN", "LWIN", "RWIN", "APPS", "HELP", "NUMLOCK", _
                    "PRTSC", "SCROLLLOCK", VkcBreak, "F1", "F2", "F3", _
                    "F4", "F5", "F6", "F7", "F8", "F9", _
                    "F10", "F11", "F12", "F13", "F14", "F15", _
                    "F16", VkcBreak, "ADD", "SUBTRACT", "MULTIPLY", "DIVIDE", _
                    "NUMPAD0", "NUMPAD1", "NUMPAD2", "NUMPAD3", "NUMPAD4", "NUMPAD5", _
                    "NUMPAD6", "NUMPAD7", "NUMPAD8", "NUMPAD9"}
                SetKeysToRichTextBox(rb, vSpecialKeyCodes2)

            End With

            Me.rhSequence.Rtf = rb.ToString()
            LinkifyRtf(Me.rhSequence)

        End Using
        specialKeysSet = True

    End Sub

    Private Sub SetKeysToRichTextBox(ByVal rb As RtfBuilder, ByVal vSpecialKeyCodes As String())
        Dim bfirst As Boolean = True
        For Each strNav As String In vSpecialKeyCodes
            If (strNav = VkcBreak) Then
                rb.AppendLine()
                rb.AppendLine()
                bfirst = True
            Else
                If (bfirst = False) Then
                    rb.Append(" ")
                End If
                rb.Append("{" + strNav + "}", Color.Blue)
                bfirst = False
            End If
        Next
    End Sub

    Private Sub LinkifyRtf(rtb As RichTextBox)

        ' Flicker otherwise
        Dim str As String = rtb.Text

        Dim iPos As Integer = str.IndexOf("{"c)
        While iPos >= 0
            Dim iEnd As Integer = str.IndexOf("}"c, iPos)
            If iEnd >= 1 Then
                rtb.[Select](iPos, iEnd - iPos + 1)
                'Debug.WriteLine(rtb.SelectedText)
                RtfBuilder.RtfSetSelectionLink(rtb)
            End If

            iPos = str.IndexOf("{"c, iPos + 1)
        End While

        rtb.[Select](0, 0)
    End Sub

    Private Sub RichTextBox2_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles rhSequence.LinkClicked
        Try

            Dim m_rbKeySeq As TextBox = Me.txtEntryAutoType.TxtBox.TextBoxObject
            
            Dim nSelStart As Integer = m_rbKeySeq.SelectionStart
            Dim nSelLength As Integer = m_rbKeySeq.SelectionLength
            Dim strText As String = m_rbKeySeq.Text
            Dim strUrl As String = e.LinkText

            If nSelLength > 0 Then
                strText = strText.Remove(nSelStart, nSelLength)
            End If

            Dim str As String = strText
            Dim iPos As Integer = nSelStart

            Dim insertOk As Boolean = False
            Dim istart As Integer = str.IndexOf("{"c, iPos)
            Dim iEnd As Integer = str.IndexOf("}"c, iPos)
            If iEnd < istart Then
                'Debug.WriteLine("Esta dentro de corchetes")
                insertOk = False
            ElseIf iEnd > istart Then
                'Debug.WriteLine("Esta fuera de corchetes")
                insertOk = True
            ElseIf iEnd = istart Then
                'Debug.WriteLine("No hay corchetes")
                insertOk = True
            End If


            If insertOk = True Then
                m_rbKeySeq.Text = strText.Insert(nSelStart, strUrl)
                m_rbKeySeq.[Select](nSelStart + strUrl.Length, 0)
                m_rbKeySeq.Focus()
            Else
                ind.win.Msg.mError("Debe insertar la opcion deseada entre otras opciones.")
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub



    Private Sub txtEntryAutoType_LostFocus(sender As Object, e As EventArgs) Handles txtEntryAutoType.LostFocusEx
        Me.pnlAutotype.Visible = False
    End Sub

    Private Sub txtEntryAutoType_GotFocus(sender As Object, e As EventArgs) Handles txtEntryAutoType.GotFocusEx
        Me.pnlAutotype.Visible = True
        If Me.pnlAutotype.Visible = True Then
            SetSpecialKeysToRichTextBox()
        End If
    End Sub


#End Region

End Class
