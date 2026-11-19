<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEncryptText
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEncryptText))
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.lbl_Title = New System.Windows.Forms.Label()
        Me.pnlLine = New System.Windows.Forms.Panel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxCopyAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxCopySelectedText = New System.Windows.Forms.ToolStripMenuItem()
        Me.lbl_textTitle = New System.Windows.Forms.Label()
        Me.btnDecrypt = New System.Windows.Forms.Button()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxCopyResults = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblTitleResult = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlTextSup = New System.Windows.Forms.Panel()
        Me.btnCrypt = New System.Windows.Forms.Button()
        Me.btnLinkClear = New System.Windows.Forms.LinkLabel()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.chkShowPwd = New System.Windows.Forms.CheckBox()
        Me.txtPassDecrypt = New System.Windows.Forms.TextBox()
        Me.pnlResult = New System.Windows.Forms.Panel()
        Me.btnSendAutotype = New System.Windows.Forms.LinkLabel()
        Me.btnLinkCopy2Clipboard = New System.Windows.Forms.LinkLabel()
        Me.lblDejaloEnblanco = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTextSup.SuspendLayout()
        Me.pnlResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgMain
        '
        Me.imgMain.BackColor = System.Drawing.Color.Transparent
        Me.imgMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.imgMain.Image = Global.My.Resources.Resources.security_unlock_32
        Me.imgMain.Location = New System.Drawing.Point(26, 12)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(55, 39)
        Me.imgMain.TabIndex = 30
        Me.imgMain.TabStop = False
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!)
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(87, 22)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(384, 29)
        Me.lbl_Title.TabIndex = 29
        Me.lbl_Title.Text = "Encrypt or Decrypt text"
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLine
        '
        Me.pnlLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLine.BackColor = System.Drawing.Color.DarkOrange
        Me.pnlLine.Location = New System.Drawing.Point(15, 51)
        Me.pnlLine.Name = "pnlLine"
        Me.pnlLine.Size = New System.Drawing.Size(845, 3)
        Me.pnlLine.TabIndex = 31
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 35)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(773, 118)
        Me.RichTextBox1.TabIndex = 32
        Me.RichTextBox1.Text = ""
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxPaste, Me.ctxCopyAll, Me.ctxCopySelectedText})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(171, 70)
        '
        'ctxPaste
        '
        Me.ctxPaste.Name = "ctxPaste"
        Me.ctxPaste.Size = New System.Drawing.Size(170, 22)
        Me.ctxPaste.Text = "Paste"
        '
        'ctxCopyAll
        '
        Me.ctxCopyAll.Name = "ctxCopyAll"
        Me.ctxCopyAll.Size = New System.Drawing.Size(170, 22)
        Me.ctxCopyAll.Text = "Copy All"
        '
        'ctxCopySelectedText
        '
        Me.ctxCopySelectedText.Name = "ctxCopySelectedText"
        Me.ctxCopySelectedText.Size = New System.Drawing.Size(170, 22)
        Me.ctxCopySelectedText.Text = "Copy selected text"
        '
        'lbl_textTitle
        '
        Me.lbl_textTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_textTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_textTitle.Location = New System.Drawing.Point(4, 3)
        Me.lbl_textTitle.Name = "lbl_textTitle"
        Me.lbl_textTitle.Size = New System.Drawing.Size(384, 29)
        Me.lbl_textTitle.TabIndex = 33
        Me.lbl_textTitle.Text = "Enter your text here"
        Me.lbl_textTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDecrypt
        '
        Me.btnDecrypt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDecrypt.BackColor = System.Drawing.Color.DarkOrange
        Me.btnDecrypt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDecrypt.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDecrypt.ForeColor = System.Drawing.Color.White
        Me.btnDecrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDecrypt.Location = New System.Drawing.Point(602, 159)
        Me.btnDecrypt.Name = "btnDecrypt"
        Me.btnDecrypt.Size = New System.Drawing.Size(171, 50)
        Me.btnDecrypt.TabIndex = 34
        Me.btnDecrypt.Text = "&Desencriptar"
        Me.btnDecrypt.UseVisualStyleBackColor = False
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox2.ContextMenuStrip = Me.ContextMenuStrip2
        Me.RichTextBox2.Location = New System.Drawing.Point(1, 44)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(772, 123)
        Me.RichTextBox2.TabIndex = 32
        Me.RichTextBox2.Text = ""
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxCopyResults})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(118, 26)
        '
        'ctxCopyResults
        '
        Me.ctxCopyResults.Name = "ctxCopyResults"
        Me.ctxCopyResults.Size = New System.Drawing.Size(117, 22)
        Me.ctxCopyResults.Text = "Copy all"
        '
        'lblTitleResult
        '
        Me.lblTitleResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleResult.ForeColor = System.Drawing.Color.DimGray
        Me.lblTitleResult.Location = New System.Drawing.Point(3, 12)
        Me.lblTitleResult.Name = "lblTitleResult"
        Me.lblTitleResult.Size = New System.Drawing.Size(384, 29)
        Me.lblTitleResult.TabIndex = 33
        Me.lblTitleResult.Text = "This is the result"
        Me.lblTitleResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.pnlTextSup)
        Me.Panel1.Controls.Add(Me.lblPassword)
        Me.Panel1.Controls.Add(Me.chkShowPwd)
        Me.Panel1.Controls.Add(Me.txtPassDecrypt)
        Me.Panel1.Controls.Add(Me.pnlResult)
        Me.Panel1.Controls.Add(Me.lblDejaloEnblanco)
        Me.Panel1.Location = New System.Drawing.Point(26, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(828, 491)
        Me.Panel1.TabIndex = 35
        '
        'pnlTextSup
        '
        Me.pnlTextSup.Controls.Add(Me.RichTextBox1)
        Me.pnlTextSup.Controls.Add(Me.lbl_textTitle)
        Me.pnlTextSup.Controls.Add(Me.btnCrypt)
        Me.pnlTextSup.Controls.Add(Me.btnDecrypt)
        Me.pnlTextSup.Controls.Add(Me.btnLinkClear)
        Me.pnlTextSup.Location = New System.Drawing.Point(14, 75)
        Me.pnlTextSup.Name = "pnlTextSup"
        Me.pnlTextSup.Size = New System.Drawing.Size(776, 220)
        Me.pnlTextSup.TabIndex = 40
        '
        'btnCrypt
        '
        Me.btnCrypt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCrypt.BackColor = System.Drawing.Color.IndianRed
        Me.btnCrypt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCrypt.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnCrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCrypt.ForeColor = System.Drawing.Color.White
        Me.btnCrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCrypt.Location = New System.Drawing.Point(425, 159)
        Me.btnCrypt.Name = "btnCrypt"
        Me.btnCrypt.Size = New System.Drawing.Size(171, 50)
        Me.btnCrypt.TabIndex = 34
        Me.btnCrypt.Text = "&Encriptar"
        Me.btnCrypt.UseVisualStyleBackColor = False
        '
        'btnLinkClear
        '
        Me.btnLinkClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkClear.Location = New System.Drawing.Point(666, 19)
        Me.btnLinkClear.Name = "btnLinkClear"
        Me.btnLinkClear.Size = New System.Drawing.Size(104, 13)
        Me.btnLinkClear.TabIndex = 35
        Me.btnLinkClear.TabStop = True
        Me.btnLinkClear.Text = "Clear"
        Me.btnLinkClear.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPassword
        '
        Me.lblPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.ForeColor = System.Drawing.Color.DimGray
        Me.lblPassword.Location = New System.Drawing.Point(12, 4)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(179, 17)
        Me.lblPassword.TabIndex = 39
        Me.lblPassword.Text = "Introduce la contraseña"
        '
        'chkShowPwd
        '
        Me.chkShowPwd.AutoSize = True
        Me.chkShowPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkShowPwd.ForeColor = System.Drawing.Color.DimGray
        Me.chkShowPwd.Location = New System.Drawing.Point(14, 52)
        Me.chkShowPwd.Name = "chkShowPwd"
        Me.chkShowPwd.Size = New System.Drawing.Size(98, 17)
        Me.chkShowPwd.TabIndex = 38
        Me.chkShowPwd.TabStop = False
        Me.chkShowPwd.Text = "Ver contraseña"
        Me.ToolTip1.SetToolTip(Me.chkShowPwd, "Marque esta opción si desea ver la contraseña que escribe")
        Me.chkShowPwd.UseVisualStyleBackColor = True
        '
        'txtPassDecrypt
        '
        Me.txtPassDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassDecrypt.Location = New System.Drawing.Point(14, 24)
        Me.txtPassDecrypt.Name = "txtPassDecrypt"
        Me.txtPassDecrypt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassDecrypt.Size = New System.Drawing.Size(209, 22)
        Me.txtPassDecrypt.TabIndex = 37
        '
        'pnlResult
        '
        Me.pnlResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlResult.Controls.Add(Me.lblTitleResult)
        Me.pnlResult.Controls.Add(Me.RichTextBox2)
        Me.pnlResult.Controls.Add(Me.btnSendAutotype)
        Me.pnlResult.Controls.Add(Me.btnLinkCopy2Clipboard)
        Me.pnlResult.Location = New System.Drawing.Point(14, 301)
        Me.pnlResult.Name = "pnlResult"
        Me.pnlResult.Size = New System.Drawing.Size(776, 187)
        Me.pnlResult.TabIndex = 36
        Me.pnlResult.Visible = False
        '
        'btnSendAutotype
        '
        Me.btnSendAutotype.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSendAutotype.Location = New System.Drawing.Point(585, 170)
        Me.btnSendAutotype.Name = "btnSendAutotype"
        Me.btnSendAutotype.Size = New System.Drawing.Size(188, 17)
        Me.btnSendAutotype.TabIndex = 35
        Me.btnSendAutotype.TabStop = True
        Me.btnSendAutotype.Text = "Autotype to previous windows"
        Me.btnSendAutotype.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnLinkCopy2Clipboard
        '
        Me.btnLinkCopy2Clipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkCopy2Clipboard.Location = New System.Drawing.Point(373, 170)
        Me.btnLinkCopy2Clipboard.Name = "btnLinkCopy2Clipboard"
        Me.btnLinkCopy2Clipboard.Size = New System.Drawing.Size(188, 17)
        Me.btnLinkCopy2Clipboard.TabIndex = 35
        Me.btnLinkCopy2Clipboard.TabStop = True
        Me.btnLinkCopy2Clipboard.Text = "Copy to clipboard"
        Me.btnLinkCopy2Clipboard.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDejaloEnblanco
        '
        Me.lblDejaloEnblanco.ForeColor = System.Drawing.Color.DimGray
        Me.lblDejaloEnblanco.Location = New System.Drawing.Point(229, 24)
        Me.lblDejaloEnblanco.Name = "lblDejaloEnblanco"
        Me.lblDejaloEnblanco.Size = New System.Drawing.Size(172, 28)
        Me.lblDejaloEnblanco.TabIndex = 39
        Me.lblDejaloEnblanco.Text = "(dejalo en blanco para usar la contraseña maestra)"
        '
        'FrmEncryptText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(866, 564)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.imgMain)
        Me.Controls.Add(Me.lbl_Title)
        Me.Controls.Add(Me.pnlLine)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmEncryptText"
        Me.Text = "Encrypt or Decrypt text"
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlTextSup.ResumeLayout(False)
        Me.pnlResult.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
    Friend WithEvents pnlLine As System.Windows.Forms.Panel
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents lbl_textTitle As System.Windows.Forms.Label
    Friend WithEvents btnDecrypt As System.Windows.Forms.Button
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTitleResult As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCrypt As System.Windows.Forms.Button
    Friend WithEvents btnLinkCopy2Clipboard As System.Windows.Forms.LinkLabel
    Friend WithEvents btnLinkClear As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlResult As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxCopyAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxCopyResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxCopySelectedText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSendAutotype As System.Windows.Forms.LinkLabel
    'Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents chkShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents txtPassDecrypt As System.Windows.Forms.TextBox
    Friend WithEvents lblDejaloEnblanco As System.Windows.Forms.Label
    Friend WithEvents pnlTextSup As System.Windows.Forms.Panel

End Class
