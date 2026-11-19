<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZipFiles
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmZipFiles))
        Me.chkSaveWithoutExtension = New System.Windows.Forms.CheckBox()
        Me.chkAutoexec = New System.Windows.Forms.CheckBox()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.lbl_TitleH1 = New System.Windows.Forms.Label()
        Me.pnlLine = New System.Windows.Forms.Panel()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.btnSelectFolder = New System.Windows.Forms.Button()
        Me.txtFilePath = New TextBoxEx0()
        Me.chkopenpath = New System.Windows.Forms.CheckBox()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkSaveWithoutExtension
        '
        Me.chkSaveWithoutExtension.AutoSize = True
        Me.chkSaveWithoutExtension.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkSaveWithoutExtension.Location = New System.Drawing.Point(303, 113)
        Me.chkSaveWithoutExtension.Name = "chkSaveWithoutExtension"
        Me.chkSaveWithoutExtension.Size = New System.Drawing.Size(128, 17)
        Me.chkSaveWithoutExtension.TabIndex = 31
        Me.chkSaveWithoutExtension.TabStop = False
        Me.chkSaveWithoutExtension.Tag = resources.GetString("chkSaveWithoutExtension.Tag")
        Me.chkSaveWithoutExtension.Text = "Guardar sin extension"
        Me.chkSaveWithoutExtension.UseVisualStyleBackColor = True
        Me.chkSaveWithoutExtension.Visible = False
        '
        'chkAutoexec
        '
        Me.chkAutoexec.AutoSize = True
        Me.chkAutoexec.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkAutoexec.Location = New System.Drawing.Point(45, 86)
        Me.chkAutoexec.Name = "chkAutoexec"
        Me.chkAutoexec.Size = New System.Drawing.Size(124, 17)
        Me.chkAutoexec.TabIndex = 32
        Me.chkAutoexec.TabStop = False
        Me.chkAutoexec.Tag = ""
        Me.chkAutoexec.Text = "Crear autoejecutable"
        Me.chkAutoexec.UseVisualStyleBackColor = True
        '
        'imgMain
        '
        Me.imgMain.BackColor = System.Drawing.Color.Transparent
        Me.imgMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.imgMain.Image = Global.My.Resources.Resources.compressed_48
        Me.imgMain.Location = New System.Drawing.Point(15, 4)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(55, 49)
        Me.imgMain.TabIndex = 30
        Me.imgMain.TabStop = False
        '
        'lbl_TitleH1
        '
        Me.lbl_TitleH1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!)
        Me.lbl_TitleH1.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_TitleH1.Location = New System.Drawing.Point(76, 24)
        Me.lbl_TitleH1.Name = "lbl_TitleH1"
        Me.lbl_TitleH1.Size = New System.Drawing.Size(384, 29)
        Me.lbl_TitleH1.TabIndex = 29
        Me.lbl_TitleH1.Text = "Zip Files"
        Me.lbl_TitleH1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLine
        '
        Me.pnlLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLine.BackColor = System.Drawing.Color.DarkOrange
        Me.pnlLine.Location = New System.Drawing.Point(4, 53)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Size = New System.Drawing.Size(560, 3)
        Me.pnlLine.TabIndex = 33
        '
        'btnContinue
        '
        Me.btnContinue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnContinue.BackColor = System.Drawing.Color.DarkSlateBlue
        Me.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.White
        Me.btnContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContinue.Location = New System.Drawing.Point(381, 220)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(171, 50)
        Me.btnContinue.TabIndex = 35
        Me.btnContinue.Text = "&Aceptar"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.Location = New System.Drawing.Point(39, 149)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(94, 20)
        Me.lblFilePath.TabIndex = 37
        Me.lblFilePath.Text = "Guardar en "
        Me.lblFilePath.Visible = False
        '
        'btnSelectFolder
        '
        Me.btnSelectFolder.Location = New System.Drawing.Point(524, 167)
        Me.btnSelectFolder.Name = "btnSelectFolder"
        Me.btnSelectFolder.Size = New System.Drawing.Size(28, 25)
        Me.btnSelectFolder.TabIndex = 38
        Me.btnSelectFolder.Text = "..."
        Me.btnSelectFolder.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.White
        Me.txtFilePath.Location = New System.Drawing.Point(36, 167)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(482, 28)
        Me.txtFilePath.TabIndex = 39
        Me.txtFilePath.TextBoxText = ""
        '
        'chkopenpath
        '
        Me.chkopenpath.AutoSize = True
        Me.chkopenpath.Checked = True
        Me.chkopenpath.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkopenpath.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkopenpath.Location = New System.Drawing.Point(45, 113)
        Me.chkopenpath.Name = "chkopenpath"
        Me.chkopenpath.Size = New System.Drawing.Size(135, 17)
        Me.chkopenpath.TabIndex = 31
        Me.chkopenpath.TabStop = False
        Me.chkopenpath.Tag = ""
        Me.chkopenpath.Text = "Abrir carpeta al finalizar"
        Me.chkopenpath.UseVisualStyleBackColor = True
        '
        'frmZipFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(566, 290)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.btnSelectFolder)
        Me.Controls.Add(Me.lblFilePath)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.chkopenpath)
        Me.Controls.Add(Me.chkSaveWithoutExtension)
        Me.Controls.Add(Me.chkAutoexec)
        Me.Controls.Add(Me.imgMain)
        Me.Controls.Add(Me.lbl_TitleH1)
        Me.Controls.Add(Me.pnlLine)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmZipFiles"
        Me.Text = "Zip"
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkSaveWithoutExtension As CheckBox
    Friend WithEvents chkAutoexec As CheckBox
    Friend WithEvents imgMain As PictureBox
    Friend WithEvents lbl_TitleH1 As Label
    Friend WithEvents pnlLine As Panel
    Friend WithEvents btnContinue As Button
    Friend WithEvents lblFilePath As Label
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents txtFilePath As TextBoxEx0
    Friend WithEvents chkopenpath As CheckBox
End Class
