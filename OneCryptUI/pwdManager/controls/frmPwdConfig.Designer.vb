<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPwdConfig
    Inherits FrmBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPwdConfig))
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.lbl_Title = New System.Windows.Forms.Label()
        Me.pnlLine = New System.Windows.Forms.Panel()
        Me.lblFileDatabasePath = New System.Windows.Forms.Label()
        Me.lblInfoDDBBPath = New System.Windows.Forms.Label()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.txtFileDatabasePath = New TextBoxEx0()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblMsgBotom = New LabelMessageEx()
        Me.chkSaveOnAppFolder = New System.Windows.Forms.CheckBox()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgMain
        '
        Me.imgMain.BackColor = System.Drawing.Color.Transparent
        Me.imgMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.imgMain.Image = Global.My.Resources.Resources.options
        Me.imgMain.Location = New System.Drawing.Point(12, 6)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(55, 39)
        Me.imgMain.TabIndex = 30
        Me.imgMain.TabStop = False
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!)
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(73, 20)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(384, 29)
        Me.lbl_Title.TabIndex = 29
        Me.lbl_Title.Text = "Configuración"
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLine
        '
        Me.pnlLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLine.BackColor = System.Drawing.Color.DarkOrange
        Me.pnlLine.Location = New System.Drawing.Point(1, 42)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Size = New System.Drawing.Size(633, 3)
        Me.pnlLine.TabIndex = 31
        '
        'lblFileDatabasePath
        '
        Me.lblFileDatabasePath.AutoSize = True
        Me.lblFileDatabasePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileDatabasePath.ForeColor = System.Drawing.Color.DimGray
        Me.lblFileDatabasePath.Location = New System.Drawing.Point(23, 57)
        Me.lblFileDatabasePath.Name = "lblFileDatabasePath"
        Me.lblFileDatabasePath.Size = New System.Drawing.Size(219, 13)
        Me.lblFileDatabasePath.TabIndex = 33
        Me.lblFileDatabasePath.Text = "Ruta de la base datos de contaseñas"
        '
        'lblInfoDDBBPath
        '
        Me.lblInfoDDBBPath.ForeColor = System.Drawing.Color.Gray
        Me.lblInfoDDBBPath.Location = New System.Drawing.Point(23, 151)
        Me.lblInfoDDBBPath.Name = "lblInfoDDBBPath"
        Me.lblInfoDDBBPath.Size = New System.Drawing.Size(486, 49)
        Me.lblInfoDDBBPath.TabIndex = 33
        Me.lblInfoDDBBPath.Text = resources.GetString("lblInfoDDBBPath.Text")
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Location = New System.Drawing.Point(563, 110)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(25, 25)
        Me.btnSelectFolder.TabIndex = 34
        Me.btnSelectFolder.Text = "..."
        Me.btnSelectFolder.UseVisualStyleBackColor = True
        '
        'txtFileDatabasePath
        '
        Me.txtFileDatabasePath.BackColor = System.Drawing.Color.White
        Me.txtFileDatabasePath.Location = New System.Drawing.Point(22, 109)
        Me.txtFileDatabasePath.Name = "txtFileDatabasePath"
        Me.txtFileDatabasePath.Size = New System.Drawing.Size(539, 28)
        Me.txtFileDatabasePath.TabIndex = 35
        Me.txtFileDatabasePath.TextBoxText = ""
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(369, 13)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(114, 29)
        Me.btnCancel.TabIndex = 37
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(491, 13)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(124, 29)
        Me.btnOk.TabIndex = 36
        Me.btnOk.Text = "&Save"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.lblMsgBotom)
        Me.pnlBottom.Controls.Add(Me.btnOk)
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 218)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(628, 58)
        Me.pnlBottom.TabIndex = 38
        '
        'lblMsgBotom
        '
        Me.lblMsgBotom.BackColor = System.Drawing.Color.White
        Me.lblMsgBotom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMsgBotom.Location = New System.Drawing.Point(3, 3)
        Me.lblMsgBotom.Message = ""
        Me.lblMsgBotom.Name = "lblMsgBotom"
        Me.lblMsgBotom.ShowAuto = True
        Me.lblMsgBotom.Size = New System.Drawing.Size(327, 52)
        Me.lblMsgBotom.TabIndex = 38
        '
        'chkSaveOnAppFolder
        '
        Me.chkSaveOnAppFolder.AutoSize = True
        Me.chkSaveOnAppFolder.Location = New System.Drawing.Point(26, 86)
        Me.chkSaveOnAppFolder.Name = "chkSaveOnAppFolder"
        Me.chkSaveOnAppFolder.Size = New System.Drawing.Size(213, 17)
        Me.chkSaveOnAppFolder.TabIndex = 39
        Me.chkSaveOnAppFolder.Text = "Guardar en el directorio de la aplicación"
        Me.chkSaveOnAppFolder.UseVisualStyleBackColor = True
        '
        'frmPwdConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(628, 276)
        Me.Controls.Add(Me.chkSaveOnAppFolder)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.txtFileDatabasePath)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Controls.Add(Me.lblInfoDDBBPath)
        Me.Controls.Add(Me.lblFileDatabasePath)
        Me.Controls.Add(Me.pnlLine)
        Me.Controls.Add(Me.lbl_Title)
        Me.Controls.Add(Me.imgMain)
        Me.Name = "frmPwdConfig"
        Me.Text = "Password manager configuration"
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
    Friend WithEvents pnlLine As System.Windows.Forms.Panel
    Friend WithEvents lblFileDatabasePath As System.Windows.Forms.Label
    Friend WithEvents lblInfoDDBBPath As System.Windows.Forms.Label
    Friend WithEvents btnSelectFolder As System.Windows.Forms.Button
    Friend WithEvents txtFileDatabasePath As TextBoxEx0
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents chkSaveOnAppFolder As System.Windows.Forms.CheckBox
    Friend WithEvents lblMsgBotom As LabelMessageEx

End Class
