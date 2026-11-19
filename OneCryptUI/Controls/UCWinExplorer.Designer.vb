<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCWinExplorer
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
        Me.components = New System.ComponentModel.Container()
        Dim AppEventsClass1 As ind.encrypter.AppEventsClass = New ind.encrypter.AppEventsClass()
        Me.SplitExplorer = New System.Windows.Forms.SplitContainer()
        Me.ExpTree1 = New ExpTreeLib.ExpTree()
        Me.UcTreeExplorer1 = New UCTreeExplorer()
        Me.pnlExplorerTop = New System.Windows.Forms.Panel()
        Me.btnCollapseExplorer = New System.Windows.Forms.PictureBox()
        Me.lblExplorerTop = New System.Windows.Forms.Label()
        Me.imgExplorerTop = New System.Windows.Forms.PictureBox()
        CType(Me.SplitExplorer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitExplorer.Panel1.SuspendLayout()
        Me.SplitExplorer.Panel2.SuspendLayout()
        Me.SplitExplorer.SuspendLayout()
        Me.pnlExplorerTop.SuspendLayout()
        CType(Me.btnCollapseExplorer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgExplorerTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitExplorer
        '
        Me.SplitExplorer.BackColor = System.Drawing.Color.Silver
        Me.SplitExplorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitExplorer.Location = New System.Drawing.Point(0, 34)
        Me.SplitExplorer.Name = "SplitExplorer"
        '
        'SplitExplorer.Panel1
        '
        Me.SplitExplorer.Panel1.Controls.Add(Me.ExpTree1)
        '
        'SplitExplorer.Panel2
        '
        Me.SplitExplorer.Panel2.Controls.Add(Me.UcTreeExplorer1)
        Me.SplitExplorer.Panel2Collapsed = True
        Me.SplitExplorer.Panel2MinSize = 0
        Me.SplitExplorer.Size = New System.Drawing.Size(252, 410)
        Me.SplitExplorer.SplitterDistance = 125
        Me.SplitExplorer.TabIndex = 1
        '
        'ExpTree1
        '
        Me.ExpTree1.AllowDrop = True
        Me.ExpTree1.AllowFolderRename = True
        Me.ExpTree1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ExpTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpTree1.Location = New System.Drawing.Point(0, 0)
        Me.ExpTree1.Name = "ExpTree1"
        Me.ExpTree1.ShowRootLines = False
        Me.ExpTree1.Size = New System.Drawing.Size(252, 410)
        Me.ExpTree1.StartUpDirectory = ExpTreeLib.ExpTree.StartDir.Desktop
        Me.ExpTree1.TabIndex = 2
        '
        'UcTreeExplorer1
        '
        Me.UcTreeExplorer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcTreeExplorer1.expTree = Nothing
        Me.UcTreeExplorer1.Location = New System.Drawing.Point(0, 0)
        Me.UcTreeExplorer1.myAppEvents = AppEventsClass1
        Me.UcTreeExplorer1.Name = "UcTreeExplorer1"
        Me.UcTreeExplorer1.Size = New System.Drawing.Size(96, 100)
        Me.UcTreeExplorer1.TabIndex = 1
        '
        'pnlExplorerTop
        '
        Me.pnlExplorerTop.BackColor = System.Drawing.Color.Silver
        Me.pnlExplorerTop.Controls.Add(Me.btnCollapseExplorer)
        Me.pnlExplorerTop.Controls.Add(Me.lblExplorerTop)
        Me.pnlExplorerTop.Controls.Add(Me.imgExplorerTop)
        Me.pnlExplorerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlExplorerTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlExplorerTop.Name = "pnlExplorerTop"
        Me.pnlExplorerTop.Size = New System.Drawing.Size(252, 34)
        Me.pnlExplorerTop.TabIndex = 4
        Me.pnlExplorerTop.Visible = False
        '
        'btnCollapseExplorer
        '
        Me.btnCollapseExplorer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCollapseExplorer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCollapseExplorer.Image = Global.My.Resources.Resources.closeGray
        Me.btnCollapseExplorer.Location = New System.Drawing.Point(217, 0)
        Me.btnCollapseExplorer.Name = "btnCollapseExplorer"
        Me.btnCollapseExplorer.Size = New System.Drawing.Size(34, 34)
        Me.btnCollapseExplorer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnCollapseExplorer.TabIndex = 3
        Me.btnCollapseExplorer.TabStop = False
        Me.btnCollapseExplorer.Visible = False
        '
        'lblExplorerTop
        '
        Me.lblExplorerTop.BackColor = System.Drawing.Color.Silver
        Me.lblExplorerTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExplorerTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExplorerTop.ForeColor = System.Drawing.Color.White
        Me.lblExplorerTop.Location = New System.Drawing.Point(34, 0)
        Me.lblExplorerTop.Name = "lblExplorerTop"
        Me.lblExplorerTop.Size = New System.Drawing.Size(218, 34)
        Me.lblExplorerTop.TabIndex = 1
        Me.lblExplorerTop.Text = "Explorador"
        Me.lblExplorerTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imgExplorerTop
        '
        Me.imgExplorerTop.Cursor = System.Windows.Forms.Cursors.Hand
        Me.imgExplorerTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.imgExplorerTop.Image = Global.My.Resources.Resources.ExplorerTop_32
        Me.imgExplorerTop.Location = New System.Drawing.Point(0, 0)
        Me.imgExplorerTop.Name = "imgExplorerTop"
        Me.imgExplorerTop.Size = New System.Drawing.Size(34, 34)
        Me.imgExplorerTop.TabIndex = 2
        Me.imgExplorerTop.TabStop = False
        '
        'UCWinExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitExplorer)
        Me.Controls.Add(Me.pnlExplorerTop)
        Me.Name = "UCWinExplorer"
        Me.Size = New System.Drawing.Size(252, 444)
        Me.SplitExplorer.Panel1.ResumeLayout(False)
        Me.SplitExplorer.Panel2.ResumeLayout(False)
        CType(Me.SplitExplorer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitExplorer.ResumeLayout(False)
        Me.pnlExplorerTop.ResumeLayout(False)
        CType(Me.btnCollapseExplorer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgExplorerTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitExplorer As SplitContainer
    Friend WithEvents ExpTree1 As ExpTreeLib.ExpTree
    Friend WithEvents UcTreeExplorer1 As UCTreeExplorer
    Friend WithEvents pnlExplorerTop As Panel
    Friend WithEvents btnCollapseExplorer As PictureBox
    Friend WithEvents lblExplorerTop As Label
    Friend WithEvents imgExplorerTop As PictureBox
End Class
