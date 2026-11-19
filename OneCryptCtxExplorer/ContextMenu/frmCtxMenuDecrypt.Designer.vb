<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCtxMenuDecrypt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCtxMenuDecrypt))
        Me.txtPassDecrypt = New System.Windows.Forms.TextBox()
        Me.lbl_pwdTitle = New System.Windows.Forms.Label()
        Me.chkShowPwd = New System.Windows.Forms.CheckBox()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.txtPwdConfirm = New System.Windows.Forms.TextBox()
        Me.lblReenterPassword = New System.Windows.Forms.Label()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPassDecrypt
        '
        Me.txtPassDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassDecrypt.Location = New System.Drawing.Point(85, 47)
        Me.txtPassDecrypt.Name = "txtPassDecrypt"
        Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassDecrypt.Size = New System.Drawing.Size(176, 31)
        Me.txtPassDecrypt.TabIndex = 0
        '
        'lbl_pwdTitle
        '
        Me.lbl_pwdTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pwdTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_pwdTitle.Location = New System.Drawing.Point(81, 15)
        Me.lbl_pwdTitle.Name = "lbl_pwdTitle"
        Me.lbl_pwdTitle.Size = New System.Drawing.Size(394, 29)
        Me.lbl_pwdTitle.TabIndex = 15
        Me.lbl_pwdTitle.Text = "Introduce tu contraseña para desencriptar"
        Me.lbl_pwdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkShowPwd
        '
        Me.chkShowPwd.AutoSize = True
        Me.chkShowPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkShowPwd.Location = New System.Drawing.Point(85, 84)
        Me.chkShowPwd.Name = "chkShowPwd"
        Me.chkShowPwd.Size = New System.Drawing.Size(98, 17)
        Me.chkShowPwd.TabIndex = 22
        Me.chkShowPwd.TabStop = False
        Me.chkShowPwd.Text = "Ver contraseña"
        Me.chkShowPwd.UseVisualStyleBackColor = True
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.DarkOrange
        Me.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.White
        Me.btnContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContinue.Location = New System.Drawing.Point(303, 123)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(171, 50)
        Me.btnContinue.TabIndex = 2
        Me.btnContinue.Text = "&Desencriptar"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'imgMain
        '
        Me.imgMain.Image = Global.My.Resources.Resources.security_unlock_64
        Me.imgMain.Location = New System.Drawing.Point(12, 12)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(62, 66)
        Me.imgMain.TabIndex = 25
        Me.imgMain.TabStop = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Cancel_Button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Cancel_Button.Location = New System.Drawing.Point(143, 127)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(154, 43)
        Me.Cancel_Button.TabIndex = 3
        Me.Cancel_Button.Text = "Cancelar"
        Me.Cancel_Button.UseVisualStyleBackColor = False
        '
        'txtPwdConfirm
        '
        Me.txtPwdConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPwdConfirm.Location = New System.Drawing.Point(267, 47)
        Me.txtPwdConfirm.Name = "txtPwdConfirm"
        Me.txtPwdConfirm.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwdConfirm.Size = New System.Drawing.Size(176, 31)
        Me.txtPwdConfirm.TabIndex = 1
        Me.txtPwdConfirm.Visible = False
        '
        'lblReenterPassword
        '
        Me.lblReenterPassword.ForeColor = System.Drawing.Color.DimGray
        Me.lblReenterPassword.Location = New System.Drawing.Point(264, 81)
        Me.lblReenterPassword.Name = "lblReenterPassword"
        Me.lblReenterPassword.Size = New System.Drawing.Size(222, 12)
        Me.lblReenterPassword.TabIndex = 29
        Me.lblReenterPassword.Text = "Vuelve a introducir la contraseña"
        Me.lblReenterPassword.Visible = False
        '
        'frmCtxMenuDecrypt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(486, 182)
        Me.Controls.Add(Me.txtPwdConfirm)
        Me.Controls.Add(Me.lblReenterPassword)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.imgMain)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.chkShowPwd)
        Me.Controls.Add(Me.lbl_pwdTitle)
        Me.Controls.Add(Me.txtPassDecrypt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCtxMenuDecrypt"
        Me.Text = "Desencriptar ficheros"
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPassDecrypt As System.Windows.Forms.TextBox
    Friend WithEvents lbl_pwdTitle As System.Windows.Forms.Label
    Friend WithEvents chkShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Public WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtPwdConfirm As System.Windows.Forms.TextBox
    Friend WithEvents lblReenterPassword As System.Windows.Forms.Label
End Class
