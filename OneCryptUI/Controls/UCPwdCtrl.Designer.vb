<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPwdCtrl
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
        Me.txtPassDecrypt = New System.Windows.Forms.TextBox()
        Me.chkShowPwd = New System.Windows.Forms.CheckBox()
        Me.lbl_pwdTitle = New System.Windows.Forms.Label()
        Me.lblReenterPassword = New System.Windows.Forms.Label()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPassDecrypt
        '
        Me.txtPassDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassDecrypt.Location = New System.Drawing.Point(163, 74)
        Me.txtPassDecrypt.Name = "txtPassDecrypt"
        Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassDecrypt.Size = New System.Drawing.Size(176, 31)
        Me.txtPassDecrypt.TabIndex = 1
        Me.txtPassDecrypt.Text = "123"
        Me.ToolTip1.SetToolTip(Me.txtPassDecrypt, "Introduzca la contraseña para encriptar o desencriptar ficheros")
        '
        'chkShowPwd
        '
        Me.chkShowPwd.AutoSize = True
        Me.chkShowPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkShowPwd.Location = New System.Drawing.Point(163, 111)
        Me.chkShowPwd.Name = "chkShowPwd"
        Me.chkShowPwd.Size = New System.Drawing.Size(98, 17)
        Me.chkShowPwd.TabIndex = 26
        Me.chkShowPwd.TabStop = False
        Me.chkShowPwd.Text = "Ver contraseña"
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, "Marque esta opción si desea ver la contraseña que escribe")
        Me.chkShowPwd.UseVisualStyleBackColor = True
        '
        'lbl_TitleH1
        '
        Me.lbl_pwdTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pwdTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_pwdTitle.Location = New System.Drawing.Point(158, 19)
        Me.lbl_pwdTitle.Name = "lbl_TitleH1"
        Me.lbl_pwdTitle.Size = New System.Drawing.Size(384, 29)
        Me.lbl_pwdTitle.TabIndex = 27
        Me.lbl_pwdTitle.Text = "Desencriptar ficheros"
        Me.lbl_pwdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReenterPassword
        '
        Me.lblReenterPassword.ForeColor = System.Drawing.Color.DimGray
        Me.lblReenterPassword.Location = New System.Drawing.Point(160, 59)
        Me.lblReenterPassword.Name = "lblReenterPassword"
        Me.lblReenterPassword.Size = New System.Drawing.Size(262, 12)
        Me.lblReenterPassword.TabIndex = 28
        Me.lblReenterPassword.Text = "Vuelve a introducir la contraseña"
        Me.lblReenterPassword.Visible = False
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.Gainsboro
        Me.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.White
        Me.btnContinue.Image = Global.My.Resources.Resources.security_unlock_32
        Me.btnContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContinue.Location = New System.Drawing.Point(305, 161)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(225, 50)
        Me.btnContinue.TabIndex = 29
        Me.btnContinue.Text = "&Desencriptar"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'imgMain
        '
        Me.imgMain.Image = Global.My.Resources.Resources.lock_open_128x128_gray
        Me.imgMain.Location = New System.Drawing.Point(12, 19)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(133, 148)
        Me.imgMain.TabIndex = 25
        Me.imgMain.TabStop = False
        '
        'UCPwdCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.lblReenterPassword)
        Me.Controls.Add(Me.lbl_pwdTitle)
        Me.Controls.Add(Me.chkShowPwd)
        Me.Controls.Add(Me.imgMain)
        Me.Controls.Add(Me.txtPassDecrypt)
        Me.Name = "UCPwdCtrl"
        Me.Size = New System.Drawing.Size(548, 214)
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPassDecrypt As System.Windows.Forms.TextBox
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents chkShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_pwdTitle As System.Windows.Forms.Label
    Friend WithEvents lblReenterPassword As System.Windows.Forms.Label
    Friend WithEvents btnContinue As System.Windows.Forms.Button

End Class
