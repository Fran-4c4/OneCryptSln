<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UcLibraryRecents
    Inherits AppBaseUC

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlFlowMain = New System.Windows.Forms.Panel()
        Me.pnlFlowRecent1 = New System.Windows.Forms.Panel()
        Me.btnCreateLib = New System.Windows.Forms.Button()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lbl_Title = New System.Windows.Forms.Label()
        Me.imgMain = New System.Windows.Forms.PictureBox()
        Me.pnlFlowMain.SuspendLayout()
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlFlowMain
        '
        Me.pnlFlowMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFlowMain.AutoScroll = True
        Me.pnlFlowMain.BackColor = System.Drawing.Color.White
        Me.pnlFlowMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFlowMain.Controls.Add(Me.pnlFlowRecent1)
        Me.pnlFlowMain.Location = New System.Drawing.Point(32, 121)
        Me.pnlFlowMain.Name = "pnlFlowMain"
        Me.pnlFlowMain.Size = New System.Drawing.Size(600, 160)
        Me.pnlFlowMain.TabIndex = 34
        '
        'pnlFlowRecent1
        '
        Me.pnlFlowRecent1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFlowRecent1.BackColor = System.Drawing.Color.White
        Me.pnlFlowRecent1.Location = New System.Drawing.Point(0, 0)
        Me.pnlFlowRecent1.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFlowRecent1.Name = "pnlFlowRecent1"
        Me.pnlFlowRecent1.Size = New System.Drawing.Size(600, 0)
        Me.pnlFlowRecent1.TabIndex = 34
        '
        'btnCreateLib
        '
        Me.btnCreateLib.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateLib.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCreateLib.FlatAppearance.BorderSize = 0
        Me.btnCreateLib.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCreateLib.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateLib.Image = Global.My.Resources.Resources.project_add
        Me.btnCreateLib.Location = New System.Drawing.Point(638, 121)
        Me.btnCreateLib.Name = "btnCreateLib"
        Me.btnCreateLib.Size = New System.Drawing.Size(39, 42)
        Me.btnCreateLib.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.btnCreateLib, "Create new library")
        Me.btnCreateLib.UseVisualStyleBackColor = True
        '
        'lblSubTitle
        '
        Me.lblSubTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblSubTitle.Location = New System.Drawing.Point(91, 46)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(262, 12)
        Me.lblSubTitle.TabIndex = 31
        Me.lblSubTitle.Text = "Estas son tus librerias mas recientes."
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(67, 15)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(384, 29)
        Me.lbl_Title.TabIndex = 30
        Me.lbl_Title.Text = "Librerias recientes"
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imgMain
        '
        Me.imgMain.Image = Global.My.Resources.Resources.project_history
        Me.imgMain.Location = New System.Drawing.Point(17, 15)
        Me.imgMain.Name = "imgMain"
        Me.imgMain.Size = New System.Drawing.Size(44, 43)
        Me.imgMain.TabIndex = 29
        Me.imgMain.TabStop = False
        '
        'UcLibraryRecents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.pnlFlowMain)
        Me.Controls.Add(Me.btnCreateLib)
        Me.Controls.Add(Me.lblSubTitle)
        Me.Controls.Add(Me.lbl_Title)
        Me.Controls.Add(Me.imgMain)
        Me.Name = "UcLibraryRecents"
        Me.Size = New System.Drawing.Size(806, 298)
        Me.pnlFlowMain.ResumeLayout(False)
        CType(Me.imgMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
    Friend WithEvents imgMain As System.Windows.Forms.PictureBox
    Friend WithEvents btnCreateLib As System.Windows.Forms.Button
    Friend WithEvents pnlFlowRecent1 As System.Windows.Forms.Panel
    Friend WithEvents pnlFlowMain As System.Windows.Forms.Panel
End Class
