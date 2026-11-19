<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.lbl_pwdTitle = New System.Windows.Forms.Label()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.lblInfoOptions = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkOpenAfterDecrypt = New System.Windows.Forms.CheckBox()
        Me.chkNotDecryptWithoutExtension = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.imgInfoOptions = New System.Windows.Forms.PictureBox()
        Me.chkIntegrateInWindowsMenu = New System.Windows.Forms.CheckBox()
        Me.pnlLine = New System.Windows.Forms.Panel()
        Me.chkStartWithWindows = New System.Windows.Forms.CheckBox()
        Me.chkOpenImgInApp = New System.Windows.Forms.CheckBox()
        Me.chkOpenAppWithCtrolKey = New System.Windows.Forms.CheckBox()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgInfoOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_pwdTitle
        '
        Me.lbl_pwdTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!)
        Me.lbl_pwdTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_pwdTitle.Location = New System.Drawing.Point(73, 25)
        Me.lbl_pwdTitle.Name = "lbl_pwdTitle"
        Me.lbl_pwdTitle.Size = New System.Drawing.Size(384, 29)
        Me.lbl_pwdTitle.TabIndex = 15
        Me.lbl_pwdTitle.Text = "Configuración"
        Me.lbl_pwdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imgMain
        '
        Me.imgMain.BackColor = System.Drawing.Color.Transparent
        Me.imgMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.imgMain.Image = Global.My.Resources.Resources.options
        Me.imgMain.Location = New System.Drawing.Point(12, 15)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(55, 39)
        Me.imgMain.TabIndex = 25
        Me.imgMain.TabStop = False
        '
        'lblInfoOptions
        '
        Me.lblInfoOptions.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.lblInfoOptions.ForeColor = System.Drawing.Color.Gray
        Me.lblInfoOptions.Location = New System.Drawing.Point(431, 78)
        Me.lblInfoOptions.Name = "lblInfoOptions"
        Me.lblInfoOptions.Size = New System.Drawing.Size(341, 137)
        Me.lblInfoOptions.TabIndex = 26
        Me.lblInfoOptions.Text = "Informacion sobre cada opcion" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblInfoOptions.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.My.Resources.Resources.file_DecryptAndOpenDisabled_16
        Me.PictureBox1.Location = New System.Drawing.Point(9, 76)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'chkOpenAfterDecrypt
        '
        Me.chkOpenAfterDecrypt.AutoSize = True
        Me.chkOpenAfterDecrypt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkOpenAfterDecrypt.Location = New System.Drawing.Point(42, 87)
        Me.chkOpenAfterDecrypt.Name = "chkOpenAfterDecrypt"
        Me.chkOpenAfterDecrypt.Size = New System.Drawing.Size(246, 17)
        Me.chkOpenAfterDecrypt.TabIndex = 27
        Me.chkOpenAfterDecrypt.TabStop = False
        Me.chkOpenAfterDecrypt.Tag = "Marque esta opción si desea que se abran los ficheros automaticamente despues de " &
    "desencriptar"
        Me.chkOpenAfterDecrypt.Text = "Abrir archivos automaticamente al desencriptar"
        Me.chkOpenAfterDecrypt.UseVisualStyleBackColor = True
        '
        'chkNotDecryptWithoutExtension
        '
        Me.chkNotDecryptWithoutExtension.AutoSize = True
        Me.chkNotDecryptWithoutExtension.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkNotDecryptWithoutExtension.Location = New System.Drawing.Point(42, 123)
        Me.chkNotDecryptWithoutExtension.Name = "chkNotDecryptWithoutExtension"
        Me.chkNotDecryptWithoutExtension.Size = New System.Drawing.Size(318, 17)
        Me.chkNotDecryptWithoutExtension.TabIndex = 27
        Me.chkNotDecryptWithoutExtension.TabStop = False
        Me.chkNotDecryptWithoutExtension.Tag = resources.GetString("chkNotDecryptWithoutExtension.Tag")
        Me.chkNotDecryptWithoutExtension.Text = "No intentar desencriptar ficheros sin la extensión del programa"
        Me.chkNotDecryptWithoutExtension.UseVisualStyleBackColor = True
        '
        'imgInfoOptions
        '
        Me.imgInfoOptions.Image = CType(resources.GetObject("imgInfoOptions.Image"), System.Drawing.Image)
        Me.imgInfoOptions.Location = New System.Drawing.Point(402, 72)
        Me.imgInfoOptions.Name = "imgInfoOptions"
        Me.imgInfoOptions.Size = New System.Drawing.Size(383, 150)
        Me.imgInfoOptions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgInfoOptions.TabIndex = 25
        Me.imgInfoOptions.TabStop = False
        Me.imgInfoOptions.Visible = False
        '
        'chkIntegrateInWindowsMenu
        '
        Me.chkIntegrateInWindowsMenu.AutoSize = True
        Me.chkIntegrateInWindowsMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkIntegrateInWindowsMenu.Location = New System.Drawing.Point(43, 159)
        Me.chkIntegrateInWindowsMenu.Name = "chkIntegrateInWindowsMenu"
        Me.chkIntegrateInWindowsMenu.Size = New System.Drawing.Size(176, 17)
        Me.chkIntegrateInWindowsMenu.TabIndex = 27
        Me.chkIntegrateInWindowsMenu.TabStop = False
        Me.chkIntegrateInWindowsMenu.Tag = "Al activar esta función se mostrará una opción para desencriptar los ficheros en " &
    "el menu contextual de windows"
        Me.chkIntegrateInWindowsMenu.Text = "Integrar en el menu de windows"
        Me.chkIntegrateInWindowsMenu.UseVisualStyleBackColor = True
        '
        'pnlLine
        '
        Me.pnlLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLine.BackColor = System.Drawing.Color.DarkOrange
        Me.pnlLine.Location = New System.Drawing.Point(1, 54)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Size = New System.Drawing.Size(794, 3)
        Me.pnlLine.TabIndex = 28
        '
        'chkStartWithWindows
        '
        Me.chkStartWithWindows.AutoSize = True
        Me.chkStartWithWindows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkStartWithWindows.Location = New System.Drawing.Point(42, 195)
        Me.chkStartWithWindows.Name = "chkStartWithWindows"
        Me.chkStartWithWindows.Size = New System.Drawing.Size(297, 17)
        Me.chkStartWithWindows.TabIndex = 27
        Me.chkStartWithWindows.TabStop = False
        Me.chkStartWithWindows.Tag = "Arrancar la aplicación automaticamente al iniciar windows"
        Me.chkStartWithWindows.Text = "Arrancar la aplicación automaticamente al iniciar windows"
        Me.chkStartWithWindows.UseVisualStyleBackColor = True
        '
        'chkOpenImgInApp
        '
        Me.chkOpenImgInApp.AutoSize = True
        Me.chkOpenImgInApp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkOpenImgInApp.Location = New System.Drawing.Point(42, 231)
        Me.chkOpenImgInApp.Name = "chkOpenImgInApp"
        Me.chkOpenImgInApp.Size = New System.Drawing.Size(157, 17)
        Me.chkOpenImgInApp.TabIndex = 27
        Me.chkOpenImgInApp.TabStop = False
        Me.chkOpenImgInApp.Tag = "Abrir imagenes en OneCrypt"
        Me.chkOpenImgInApp.Text = "Abrir imagenes en OneCrypt"
        Me.chkOpenImgInApp.UseVisualStyleBackColor = True
        '
        'chkOpenAppWithCtrolKey
        '
        Me.chkOpenAppWithCtrolKey.AutoSize = True
        Me.chkOpenAppWithCtrolKey.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkOpenAppWithCtrolKey.Location = New System.Drawing.Point(43, 263)
        Me.chkOpenAppWithCtrolKey.Name = "chkOpenAppWithCtrolKey"
        Me.chkOpenAppWithCtrolKey.Size = New System.Drawing.Size(285, 17)
        Me.chkOpenAppWithCtrolKey.TabIndex = 27
        Me.chkOpenAppWithCtrolKey.TabStop = False
        Me.chkOpenAppWithCtrolKey.Tag = "Restaurar Ventana pulsando la tecla ""Control"" 2 veces"
        Me.chkOpenAppWithCtrolKey.Text = "Restaurar Ventana pulsando la tecla ""Control"" 2 veces"
        Me.chkOpenAppWithCtrolKey.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(794, 310)
        Me.Controls.Add(Me.chkOpenAppWithCtrolKey)
        Me.Controls.Add(Me.chkOpenImgInApp)
        Me.Controls.Add(Me.chkStartWithWindows)
        Me.Controls.Add(Me.chkIntegrateInWindowsMenu)
        Me.Controls.Add(Me.chkNotDecryptWithoutExtension)
        Me.Controls.Add(Me.chkOpenAfterDecrypt)
        Me.Controls.Add(Me.lblInfoOptions)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.imgMain)
        Me.Controls.Add(Me.lbl_pwdTitle)
        Me.Controls.Add(Me.imgInfoOptions)
        Me.Controls.Add(Me.pnlLine)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Tag = "test eng"
        Me.Text = "Opciones de configuración"
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgInfoOptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_pwdTitle As System.Windows.Forms.Label
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents lblInfoOptions As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkOpenAfterDecrypt As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotDecryptWithoutExtension As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents imgInfoOptions As System.Windows.Forms.PictureBox
    Friend WithEvents chkIntegrateInWindowsMenu As System.Windows.Forms.CheckBox
    Friend WithEvents pnlLine As System.Windows.Forms.Panel
    Friend WithEvents chkStartWithWindows As System.Windows.Forms.CheckBox
    Friend WithEvents chkOpenImgInApp As CheckBox
    Friend WithEvents chkOpenAppWithCtrolKey As CheckBox
End Class
