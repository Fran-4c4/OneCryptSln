<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPwdController
    Inherits AppBaseUC

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PnlPassword = New System.Windows.Forms.Panel()
        Me.btnKeyBoard = New System.Windows.Forms.Button()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.btnDeletePwd = New System.Windows.Forms.LinkLabel()
        Me.chkShowPwd = New System.Windows.Forms.CheckBox()
        Me.txtPassDecrypt = New System.Windows.Forms.TextBox()
        Me.txtPwdConfirm = New System.Windows.Forms.TextBox()
        Me.chkStorePwd = New System.Windows.Forms.CheckBox()
        Me.lblInfoPwd = New System.Windows.Forms.Label()
        Me.lbl_pwdTitle = New System.Windows.Forms.Label()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.lblReenterPassword = New System.Windows.Forms.Label()
        Me.PasswordStrengthControl1 = New PasswordStrengthControl()
        Me.pnl2 = New System.Windows.Forms.Panel()
        Me.pnl1 = New System.Windows.Forms.Panel()
        Me.LblLnkLanguage = New System.Windows.Forms.LinkLabel()
        Me.PnlPassword.SuspendLayout()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PnlPassword
        '
        Me.PnlPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlPassword.BackColor = System.Drawing.Color.White
        Me.PnlPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PnlPassword.Controls.Add(Me.btnKeyBoard)
        Me.PnlPassword.Controls.Add(Me.imgMain)
        Me.PnlPassword.Controls.Add(Me.btnDeletePwd)
        Me.PnlPassword.Controls.Add(Me.chkShowPwd)
        Me.PnlPassword.Controls.Add(Me.txtPassDecrypt)
        Me.PnlPassword.Controls.Add(Me.txtPwdConfirm)
        Me.PnlPassword.Controls.Add(Me.chkStorePwd)
        Me.PnlPassword.Controls.Add(Me.lblInfoPwd)
        Me.PnlPassword.Controls.Add(Me.lbl_pwdTitle)
        Me.PnlPassword.Controls.Add(Me.btnContinue)
        Me.PnlPassword.Controls.Add(Me.lblReenterPassword)
        Me.PnlPassword.Controls.Add(Me.PasswordStrengthControl1)
        Me.PnlPassword.Controls.Add(Me.pnl2)
        Me.PnlPassword.Controls.Add(Me.pnl1)
        Me.PnlPassword.Location = New System.Drawing.Point(10, 10)
        Me.PnlPassword.Name = "PnlPassword"
        Me.PnlPassword.Padding = New System.Windows.Forms.Padding(5)
        Me.PnlPassword.Size = New System.Drawing.Size(730, 280)
        Me.PnlPassword.TabIndex = 22
        '
        'btnKeyBoard
        '
        Me.btnKeyBoard.Location = New System.Drawing.Point(603, 63)
        Me.btnKeyBoard.Name = "btnKeyBoard"
        Me.btnKeyBoard.Size = New System.Drawing.Size(75, 23)
        Me.btnKeyBoard.TabIndex = 26
        Me.btnKeyBoard.Text = "keyBoard"
        Me.btnKeyBoard.UseVisualStyleBackColor = True
        Me.btnKeyBoard.Visible = False
        '
        'imgMain
        '
        Me.imgMain.Image = Global.My.Resources.Resources.lock_128x128_gray
        Me.imgMain.Location = New System.Drawing.Point(8, 18)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(133, 148)
        Me.imgMain.TabIndex = 24
        Me.imgMain.TabStop = False
        '
        'btnDeletePwd
        '
        Me.btnDeletePwd.ForeColor = System.Drawing.Color.DarkGray
        Me.btnDeletePwd.LinkColor = System.Drawing.Color.Gray
        Me.btnDeletePwd.Location = New System.Drawing.Point(299, 139)
        Me.btnDeletePwd.Name = "btnDeletePwd"
        Me.btnDeletePwd.Size = New System.Drawing.Size(295, 37)
        Me.btnDeletePwd.TabIndex = 23
        Me.btnDeletePwd.TabStop = True
        Me.btnDeletePwd.Text = "Tu contraseña esta almacenada, solo necesitas introducirla una vez. Si quieres bo" &
    "rrarla pulsa aqui: Borrar contraseña"
        '
        'chkShowPwd
        '
        Me.chkShowPwd.AutoSize = True
        Me.chkShowPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkShowPwd.Location = New System.Drawing.Point(152, 116)
        Me.chkShowPwd.Name = "chkShowPwd"
        Me.chkShowPwd.Size = New System.Drawing.Size(101, 17)
        Me.chkShowPwd.TabIndex = 21
        Me.chkShowPwd.TabStop = False
        Me.chkShowPwd.Text = "Show password"
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, "Select this checkbox if you want to see the password while you are writing. ")
        Me.chkShowPwd.UseVisualStyleBackColor = True
        '
        'txtPassDecrypt
        '
        Me.txtPassDecrypt.BackColor = System.Drawing.Color.White
        Me.txtPassDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassDecrypt.Location = New System.Drawing.Point(152, 63)
        Me.txtPassDecrypt.MaxLength = 120
        Me.txtPassDecrypt.Name = "txtPassDecrypt"
        Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassDecrypt.Size = New System.Drawing.Size(176, 31)
        Me.txtPassDecrypt.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtPassDecrypt, "Enter the password to encrypt/decrypt files")
        '
        'txtPwdConfirm
        '
        Me.txtPwdConfirm.BackColor = System.Drawing.Color.White
        Me.txtPwdConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPwdConfirm.Location = New System.Drawing.Point(348, 63)
        Me.txtPwdConfirm.MaxLength = 120
        Me.txtPwdConfirm.Name = "txtPwdConfirm"
        Me.txtPwdConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwdConfirm.Size = New System.Drawing.Size(176, 31)
        Me.txtPwdConfirm.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtPwdConfirm, "Enter the password to validate")
        Me.txtPwdConfirm.Visible = False
        '
        'chkStorePwd
        '
        Me.chkStorePwd.AutoSize = True
        Me.chkStorePwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkStorePwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkStorePwd.Location = New System.Drawing.Point(152, 139)
        Me.chkStorePwd.Name = "chkStorePwd"
        Me.chkStorePwd.Size = New System.Drawing.Size(117, 17)
        Me.chkStorePwd.TabIndex = 19
        Me.chkStorePwd.TabStop = False
        Me.chkStorePwd.Text = "One password step"
        Me.ToolTip1.SetToolTip(Me.chkStorePwd, "Marque esta opción para que la aplicación almacene la clave encriptada y solo ten" &
        "ga que introducirla una vez.")
        Me.chkStorePwd.UseVisualStyleBackColor = True
        '
        'lblInfoPwd
        '
        Me.lblInfoPwd.ForeColor = System.Drawing.Color.DimGray
        Me.lblInfoPwd.Location = New System.Drawing.Point(21, 190)
        Me.lblInfoPwd.Name = "lblInfoPwd"
        Me.lblInfoPwd.Size = New System.Drawing.Size(472, 67)
        Me.lblInfoPwd.TabIndex = 18
        Me.lblInfoPwd.Text = "La primera vez se necesita introducir la contraseña 2 veces pero luego ya no será" &
    " necesario si marcas la opción ""Acceso en un paso"" " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "La aplicación no guardará n" &
    "unca la contraseña."
        Me.lblInfoPwd.Visible = False
        '
        'lbl_pwdTitle
        '
        Me.lbl_pwdTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pwdTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_pwdTitle.Location = New System.Drawing.Point(143, 18)
        Me.lbl_pwdTitle.Name = "lbl_pwdTitle"
        Me.lbl_pwdTitle.Size = New System.Drawing.Size(384, 29)
        Me.lbl_pwdTitle.TabIndex = 14
        Me.lbl_pwdTitle.Text = "Choose Your Master Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lbl_pwdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.LimeGreen
        Me.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.White
        Me.btnContinue.Image = Global.My.Resources.Resources.Security_Approved
        Me.btnContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContinue.Location = New System.Drawing.Point(537, 190)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(179, 50)
        Me.btnContinue.TabIndex = 2
        Me.btnContinue.Text = "&Continue"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'lblReenterPassword
        '
        Me.lblReenterPassword.ForeColor = System.Drawing.Color.DimGray
        Me.lblReenterPassword.Location = New System.Drawing.Point(345, 47)
        Me.lblReenterPassword.Name = "lblReenterPassword"
        Me.lblReenterPassword.Size = New System.Drawing.Size(262, 12)
        Me.lblReenterPassword.TabIndex = 18
        Me.lblReenterPassword.Text = "Enter the password again"
        Me.lblReenterPassword.Visible = False
        '
        'PasswordStrengthControl1
        '
        Me.PasswordStrengthControl1.ForeColor = System.Drawing.Color.DarkGray
        Me.PasswordStrengthControl1.Location = New System.Drawing.Point(152, 98)
        Me.PasswordStrengthControl1.Name = "PasswordStrengthControl1"
        Me.PasswordStrengthControl1.Size = New System.Drawing.Size(176, 12)
        Me.PasswordStrengthControl1.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.PasswordStrengthControl1, "Password quality")
        Me.PasswordStrengthControl1.txtPwdAtach = Nothing
        Me.PasswordStrengthControl1.txtPwdAtachVar = Nothing
        Me.PasswordStrengthControl1.Visible = False
        '
        'pnl2
        '
        Me.pnl2.BackColor = System.Drawing.Color.LimeGreen
        Me.pnl2.CausesValidation = False
        Me.pnl2.Location = New System.Drawing.Point(345, 60)
        Me.pnl2.Name = "pnl2"
        Me.pnl2.Size = New System.Drawing.Size(182, 37)
        Me.pnl2.TabIndex = 25
        Me.pnl2.Visible = False
        '
        'pnl1
        '
        Me.pnl1.BackColor = System.Drawing.Color.LimeGreen
        Me.pnl1.CausesValidation = False
        Me.pnl1.Location = New System.Drawing.Point(149, 60)
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(182, 37)
        Me.pnl1.TabIndex = 25
        '
        'LblLnkLanguage
        '
        Me.LblLnkLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblLnkLanguage.AutoSize = True
        Me.LblLnkLanguage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLnkLanguage.Location = New System.Drawing.Point(677, 4)
        Me.LblLnkLanguage.Name = "LblLnkLanguage"
        Me.LblLnkLanguage.Size = New System.Drawing.Size(59, 16)
        Me.LblLnkLanguage.TabIndex = 27
        Me.LblLnkLanguage.TabStop = True
        Me.LblLnkLanguage.Text = "English"
        '
        'UCPwdController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.LblLnkLanguage)
        Me.Controls.Add(Me.PnlPassword)
        Me.Name = "UCPwdController"
        Me.Size = New System.Drawing.Size(750, 300)
        Me.PnlPassword.ResumeLayout(False)
        Me.PnlPassword.PerformLayout()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PnlPassword As System.Windows.Forms.Panel
    Friend WithEvents txtPwdConfirm As System.Windows.Forms.TextBox
    Friend WithEvents chkStorePwd As System.Windows.Forms.CheckBox
    Friend WithEvents lblInfoPwd As System.Windows.Forms.Label
    Friend WithEvents lblReenterPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassDecrypt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_pwdTitle As System.Windows.Forms.Label
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents chkShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents PasswordStrengthControl1 As PasswordStrengthControl
    Friend WithEvents btnDeletePwd As System.Windows.Forms.LinkLabel
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents pnl1 As System.Windows.Forms.Panel
    Friend WithEvents pnl2 As System.Windows.Forms.Panel
    Friend WithEvents btnKeyBoard As Button
    Friend WithEvents LblLnkLanguage As LinkLabel
End Class
