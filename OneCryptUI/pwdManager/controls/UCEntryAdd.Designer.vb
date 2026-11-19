<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCEntryAdd
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim pnlLine As System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.imgTitle = New System.Windows.Forms.PictureBox()
        Me.lblTitlesub = New System.Windows.Forms.Label()
        Me.lbl_Title = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.rhNotes = New System.Windows.Forms.RichTextBox()
        Me.chkShowPwd = New System.Windows.Forms.CheckBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.imgUrl = New System.Windows.Forms.PictureBox()
        Me.cboGroup = New System.Windows.Forms.ComboBox()
        Me.lblEntryGroup = New System.Windows.Forms.Label()
        Me.pnlCboGroup = New System.Windows.Forms.Panel()
        Me.imgKeyboard = New System.Windows.Forms.PictureBox()
        Me.rhSequence = New System.Windows.Forms.RichTextBox()
        Me.imgCleanAutotypeField = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlAutotype = New System.Windows.Forms.Panel()
        Me.btnCloseSendKeys = New System.Windows.Forms.PictureBox()
        Me.lblPnlAutotypeTitleSub = New System.Windows.Forms.Label()
        Me.lblPnlAutotypeTitle = New System.Windows.Forms.Label()
        Me.PasswordStrengthControl1 = New PasswordStrengthControl()
        Me.txtEntryAutoType = New textboxEx()
        Me.txtEntryURL = New textboxEx()
        Me.txtEntryUserName = New textboxEx()
        Me.txtEntryPwd = New textboxEx()
        Me.txtEntryName = New textboxEx()
        pnlLine = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        CType(Me.imgTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgKeyboard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgCleanAutotypeField, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAutotype.SuspendLayout()
        CType(Me.btnCloseSendKeys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlLine
        '
        pnlLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        pnlLine.BackColor = System.Drawing.Color.DimGray
        pnlLine.Location = New System.Drawing.Point(20, 53)
        pnlLine.Name = "pnlLine"
        pnlLine.Size = New System.Drawing.Size(498, 3)
        pnlLine.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(pnlLine)
        Me.Panel1.Controls.Add(Me.imgTitle)
        Me.Panel1.Controls.Add(Me.lblTitlesub)
        Me.Panel1.Controls.Add(Me.lbl_Title)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(538, 64)
        Me.Panel1.TabIndex = 0
        '
        'imgTitle
        '
        Me.imgTitle.Location = New System.Drawing.Point(20, 13)
        Me.imgTitle.Name = "imgTitle"
        Me.imgTitle.Size = New System.Drawing.Size(33, 33)
        Me.imgTitle.TabIndex = 25
        Me.imgTitle.TabStop = False
        '
        'lblTitlesub
        '
        Me.lblTitlesub.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitlesub.ForeColor = System.Drawing.Color.DarkGray
        Me.lblTitlesub.Location = New System.Drawing.Point(92, 35)
        Me.lblTitlesub.Name = "lblTitlesub"
        Me.lblTitlesub.Size = New System.Drawing.Size(384, 15)
        Me.lblTitlesub.TabIndex = 15
        Me.lblTitlesub.Text = "Create your new user/password"
        Me.lblTitlesub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(70, 10)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(384, 29)
        Me.lbl_Title.TabIndex = 15
        Me.lbl_Title.Text = "Add entry"
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNotes
        '
        Me.lblNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(20, 268)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(106, 22)
        Me.lblNotes.TabIndex = 5
        Me.lblNotes.Text = "Notes"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rhNotes
        '
        Me.rhNotes.Location = New System.Drawing.Point(20, 286)
        Me.rhNotes.Name = "rhNotes"
        Me.rhNotes.Size = New System.Drawing.Size(474, 122)
        Me.rhNotes.TabIndex = 4
        Me.rhNotes.Text = ""
        '
        'chkShowPwd
        '
        Me.chkShowPwd.AutoSize = True
        Me.chkShowPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkShowPwd.Location = New System.Drawing.Point(295, 165)
        Me.chkShowPwd.Name = "chkShowPwd"
        Me.chkShowPwd.Size = New System.Drawing.Size(101, 17)
        Me.chkShowPwd.TabIndex = 22
        Me.chkShowPwd.TabStop = False
        Me.chkShowPwd.Text = "Show password"
        Me.chkShowPwd.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(382, 424)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(112, 29)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "&Save"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(279, 424)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(97, 29)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'imgUrl
        '
        Me.imgUrl.Location = New System.Drawing.Point(462, 205)
        Me.imgUrl.Margin = New System.Windows.Forms.Padding(0)
        Me.imgUrl.Name = "imgUrl"
        Me.imgUrl.Size = New System.Drawing.Size(16, 16)
        Me.imgUrl.TabIndex = 25
        Me.imgUrl.TabStop = False
        '
        'cboGroup
        '
        Me.cboGroup.BackColor = System.Drawing.Color.White
        Me.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroup.FormattingEnabled = True
        Me.cboGroup.Location = New System.Drawing.Point(130, 104)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(220, 21)
        Me.cboGroup.TabIndex = 26
        '
        'lblEntryGroup
        '
        Me.lblEntryGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntryGroup.ForeColor = System.Drawing.Color.DimGray
        Me.lblEntryGroup.Location = New System.Drawing.Point(3, 102)
        Me.lblEntryGroup.Name = "lblEntryGroup"
        Me.lblEntryGroup.Size = New System.Drawing.Size(119, 22)
        Me.lblEntryGroup.TabIndex = 27
        Me.lblEntryGroup.Text = "Entry Group"
        Me.lblEntryGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCboGroup
        '
        Me.pnlCboGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.pnlCboGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCboGroup.BackColor = System.Drawing.Color.GreenYellow
        Me.pnlCboGroup.CausesValidation = False
        Me.pnlCboGroup.Location = New System.Drawing.Point(126, 101)
        Me.pnlCboGroup.Name = "pnlCboGroup"
        Me.pnlCboGroup.Size = New System.Drawing.Size(227, 38)
        Me.pnlCboGroup.TabIndex = 28
        Me.pnlCboGroup.Visible = False
        '
        'imgKeyboard
        '
        Me.imgKeyboard.Location = New System.Drawing.Point(460, 237)
        Me.imgKeyboard.Margin = New System.Windows.Forms.Padding(0)
        Me.imgKeyboard.Name = "imgKeyboard"
        Me.imgKeyboard.Size = New System.Drawing.Size(17, 17)
        Me.imgKeyboard.TabIndex = 25
        Me.imgKeyboard.TabStop = False
        '
        'rhSequence
        '
        Me.rhSequence.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rhSequence.Location = New System.Drawing.Point(3, 44)
        Me.rhSequence.Name = "rhSequence"
        Me.rhSequence.Size = New System.Drawing.Size(509, 156)
        Me.rhSequence.TabIndex = 4
        Me.rhSequence.Text = "{USERNAME}{TAB}{PASSWORD}{ENTER}"
        '
        'imgCleanAutotypeField
        '
        Me.imgCleanAutotypeField.Image = Global.My.Resources.Resources.RemoveItem
        Me.imgCleanAutotypeField.Location = New System.Drawing.Point(485, 237)
        Me.imgCleanAutotypeField.Margin = New System.Windows.Forms.Padding(0)
        Me.imgCleanAutotypeField.Name = "imgCleanAutotypeField"
        Me.imgCleanAutotypeField.Size = New System.Drawing.Size(17, 17)
        Me.imgCleanAutotypeField.TabIndex = 25
        Me.imgCleanAutotypeField.TabStop = False
        '
        'pnlAutotype
        '
        Me.pnlAutotype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAutotype.Controls.Add(Me.rhSequence)
        Me.pnlAutotype.Controls.Add(Me.btnCloseSendKeys)
        Me.pnlAutotype.Controls.Add(Me.lblPnlAutotypeTitleSub)
        Me.pnlAutotype.Controls.Add(Me.lblPnlAutotypeTitle)
        Me.pnlAutotype.Location = New System.Drawing.Point(10, 268)
        Me.pnlAutotype.Name = "pnlAutotype"
        Me.pnlAutotype.Size = New System.Drawing.Size(520, 213)
        Me.pnlAutotype.TabIndex = 30
        Me.pnlAutotype.Visible = False
        '
        'btnCloseSendKeys
        '
        Me.btnCloseSendKeys.Image = Global.My.Resources.Resources.closeGray
        Me.btnCloseSendKeys.Location = New System.Drawing.Point(474, 3)
        Me.btnCloseSendKeys.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCloseSendKeys.Name = "btnCloseSendKeys"
        Me.btnCloseSendKeys.Size = New System.Drawing.Size(29, 29)
        Me.btnCloseSendKeys.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.btnCloseSendKeys.TabIndex = 25
        Me.btnCloseSendKeys.TabStop = False
        '
        'lblPnlAutotypeTitleSub
        '
        Me.lblPnlAutotypeTitleSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPnlAutotypeTitleSub.ForeColor = System.Drawing.Color.DimGray
        Me.lblPnlAutotypeTitleSub.Location = New System.Drawing.Point(16, 26)
        Me.lblPnlAutotypeTitleSub.Name = "lblPnlAutotypeTitleSub"
        Me.lblPnlAutotypeTitleSub.Size = New System.Drawing.Size(457, 15)
        Me.lblPnlAutotypeTitleSub.TabIndex = 15
        Me.lblPnlAutotypeTitleSub.Text = "Insert the cursor into autotype textbox and then choose options below"
        Me.lblPnlAutotypeTitleSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPnlAutotypeTitle
        '
        Me.lblPnlAutotypeTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPnlAutotypeTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblPnlAutotypeTitle.Location = New System.Drawing.Point(3, 5)
        Me.lblPnlAutotypeTitle.Name = "lblPnlAutotypeTitle"
        Me.lblPnlAutotypeTitle.Size = New System.Drawing.Size(384, 15)
        Me.lblPnlAutotypeTitle.TabIndex = 15
        Me.lblPnlAutotypeTitle.Text = "Send simulated keypresses to other applications."
        Me.lblPnlAutotypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordStrengthControl1
        '
        Me.PasswordStrengthControl1.ForeColor = System.Drawing.Color.DarkGray
        Me.PasswordStrengthControl1.Location = New System.Drawing.Point(130, 190)
        Me.PasswordStrengthControl1.Name = "PasswordStrengthControl1"
        Me.PasswordStrengthControl1.Size = New System.Drawing.Size(154, 12)
        Me.PasswordStrengthControl1.TabIndex = 29
        Me.PasswordStrengthControl1.txtPwdAtach = Nothing
        Me.PasswordStrengthControl1.txtPwdAtachVar = Nothing
        Me.PasswordStrengthControl1.Visible = False
        '
        'txtEntryAutoType
        '
        Me.txtEntryAutoType.BackColor = System.Drawing.Color.White
        Me.txtEntryAutoType.labelBox = "AutoType"
        Me.txtEntryAutoType.Location = New System.Drawing.Point(1, 231)
        Me.txtEntryAutoType.Name = "txtEntryAutoType"
        Me.txtEntryAutoType.Size = New System.Drawing.Size(458, 28)
        Me.txtEntryAutoType.TabIndex = 3
        Me.txtEntryAutoType.TextBoxText = ""
        '
        'txtEntryURL
        '
        Me.txtEntryURL.BackColor = System.Drawing.Color.White
        Me.txtEntryURL.labelBox = "URL"
        Me.txtEntryURL.Location = New System.Drawing.Point(1, 200)
        Me.txtEntryURL.Name = "txtEntryURL"
        Me.txtEntryURL.Size = New System.Drawing.Size(458, 28)
        Me.txtEntryURL.TabIndex = 3
        Me.txtEntryURL.TextBoxText = ""
        '
        'txtEntryUserName
        '
        Me.txtEntryUserName.BackColor = System.Drawing.Color.White
        Me.txtEntryUserName.labelBox = "User name"
        Me.txtEntryUserName.Location = New System.Drawing.Point(1, 131)
        Me.txtEntryUserName.Name = "txtEntryUserName"
        Me.txtEntryUserName.Size = New System.Drawing.Size(352, 28)
        Me.txtEntryUserName.TabIndex = 1
        Me.txtEntryUserName.TextBoxText = ""
        '
        'txtEntryPwd
        '
        Me.txtEntryPwd.BackColor = System.Drawing.Color.White
        Me.txtEntryPwd.labelBox = "Password"
        Me.txtEntryPwd.Location = New System.Drawing.Point(1, 159)
        Me.txtEntryPwd.Name = "txtEntryPwd"
        Me.txtEntryPwd.Size = New System.Drawing.Size(286, 28)
        Me.txtEntryPwd.TabIndex = 1
        Me.txtEntryPwd.TextBoxText = ""
        '
        'txtEntryName
        '
        Me.txtEntryName.BackColor = System.Drawing.Color.White
        Me.txtEntryName.labelBox = "Entry name"
        Me.txtEntryName.Location = New System.Drawing.Point(1, 70)
        Me.txtEntryName.Name = "txtEntryName"
        Me.txtEntryName.Size = New System.Drawing.Size(352, 28)
        Me.txtEntryName.TabIndex = 0
        Me.txtEntryName.TextBoxText = ""
        '
        'UCEntryAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.pnlAutotype)
        Me.Controls.Add(Me.imgCleanAutotypeField)
        Me.Controls.Add(Me.lblEntryGroup)
        Me.Controls.Add(Me.cboGroup)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.imgKeyboard)
        Me.Controls.Add(Me.imgUrl)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.chkShowPwd)
        Me.Controls.Add(Me.rhNotes)
        Me.Controls.Add(Me.PasswordStrengthControl1)
        Me.Controls.Add(Me.txtEntryAutoType)
        Me.Controls.Add(Me.txtEntryURL)
        Me.Controls.Add(Me.txtEntryUserName)
        Me.Controls.Add(Me.txtEntryPwd)
        Me.Controls.Add(Me.txtEntryName)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblNotes)
        Me.Controls.Add(Me.pnlCboGroup)
        Me.Name = "UCEntryAdd"
        Me.Size = New System.Drawing.Size(538, 484)
        Me.Panel1.ResumeLayout(False)
        CType(Me.imgTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgUrl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgKeyboard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgCleanAutotypeField, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAutotype.ResumeLayout(False)
        CType(Me.btnCloseSendKeys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTitlesub As System.Windows.Forms.Label
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
    Friend WithEvents imgTitle As System.Windows.Forms.PictureBox
    Friend WithEvents txtEntryName As textboxEx
    Friend WithEvents txtEntryPwd As textboxEx
    Friend WithEvents txtEntryURL As textboxEx
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents rhNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents chkShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtEntryUserName As textboxEx
    Friend WithEvents imgUrl As System.Windows.Forms.PictureBox
    Friend WithEvents cboGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblEntryGroup As System.Windows.Forms.Label
    Private WithEvents pnlCboGroup As System.Windows.Forms.Panel
    Friend WithEvents PasswordStrengthControl1 As PasswordStrengthControl
    Friend WithEvents txtEntryAutoType As textboxEx
    Friend WithEvents imgKeyboard As System.Windows.Forms.PictureBox
    Friend WithEvents rhSequence As System.Windows.Forms.RichTextBox
    Friend WithEvents imgCleanAutotypeField As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlAutotype As System.Windows.Forms.Panel
    Friend WithEvents lblPnlAutotypeTitleSub As System.Windows.Forms.Label
    Friend WithEvents lblPnlAutotypeTitle As System.Windows.Forms.Label
    Friend WithEvents btnCloseSendKeys As System.Windows.Forms.PictureBox

End Class
