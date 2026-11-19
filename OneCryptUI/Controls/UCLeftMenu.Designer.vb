<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCLeftMenu
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnLibraries = New System.Windows.Forms.ToolStripButton()
        Me.btnExplorer = New System.Windows.Forms.ToolStripButton()
        Me.LabelVertical1 = New LabelVertical()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTitleTop = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlLeft.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Maroon
        Me.pnlLeft.Controls.Add(Me.ToolStrip1)
        Me.pnlLeft.Controls.Add(Me.LabelVertical1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(22, 330)
        Me.pnlLeft.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.GripMargin = New System.Windows.Forms.Padding(0)
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLibraries, Me.btnExplorer})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ToolStrip1.Size = New System.Drawing.Size(23, 330)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90
        '
        'btnLibraries
        '
        Me.btnLibraries.CheckOnClick = True
        Me.btnLibraries.Image = Global.My.Resources.Resources.project_menu
        Me.btnLibraries.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLibraries.Name = "btnLibraries"
        Me.btnLibraries.Size = New System.Drawing.Size(22, 63)
        Me.btnLibraries.Text = "Library"
        Me.btnLibraries.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270
        Me.btnLibraries.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnLibraries.ToolTipText = "Open libraries"
        '
        'btnExplorer
        '
        Me.btnExplorer.CheckOnClick = True
        Me.btnExplorer.Image = Global.My.Resources.Resources.ExplorerTop_32
        Me.btnExplorer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExplorer.Name = "btnExplorer"
        Me.btnExplorer.Size = New System.Drawing.Size(29, 69)
        Me.btnExplorer.Text = "Explorer"
        Me.btnExplorer.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270
        Me.btnExplorer.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        '
        'LabelVertical1
        '
        Me.LabelVertical1.BackColor = System.Drawing.Color.Cyan
        Me.LabelVertical1.caption = "LabelVertical+N1"
        Me.LabelVertical1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelVertical1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelVertical1.Location = New System.Drawing.Point(3, 221)
        Me.LabelVertical1.MaximumSize = New System.Drawing.Size(25, 112)
        Me.LabelVertical1.MinimumSize = New System.Drawing.Size(25, 112)
        Me.LabelVertical1.Name = "LabelVertical1"
        Me.LabelVertical1.Padding = New System.Windows.Forms.Padding(2)
        Me.LabelVertical1.Size = New System.Drawing.Size(25, 112)
        Me.LabelVertical1.TabIndex = 2
        Me.LabelVertical1.Text = "LabelVertical+N1"
        Me.LabelVertical1.upToDown = True
        Me.LabelVertical1.Visible = False
        '
        'pnlRight
        '
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(22, 22)
        Me.pnlRight.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(124, 308)
        Me.pnlRight.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Controls.Add(Me.lblTitleTop)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(22, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(124, 22)
        Me.Panel1.TabIndex = 0
        '
        'lblTitleTop
        '
        Me.lblTitleTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitleTop.Location = New System.Drawing.Point(0, 0)
        Me.lblTitleTop.Name = "lblTitleTop"
        Me.lblTitleTop.Size = New System.Drawing.Size(102, 22)
        Me.lblTitleTop.TabIndex = 0
        Me.lblTitleTop.Text = "lblTitle"
        Me.lblTitleTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SlateGray
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = Global.My.Resources.Resources.collapse_arrow_left
        Me.btnClose.Location = New System.Drawing.Point(102, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(22, 22)
        Me.btnClose.TabIndex = 2
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'UCLeftMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Name = "UCLeftMenu"
        Me.Size = New System.Drawing.Size(146, 330)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeft.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlLeft As Panel
    Friend WithEvents pnlRight As Panel
    Friend WithEvents LabelVertical1 As LabelVertical
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTitleTop As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnLibraries As ToolStripButton
    Friend WithEvents btnExplorer As ToolStripButton
End Class
