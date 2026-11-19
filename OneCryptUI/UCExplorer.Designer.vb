<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCExplorer
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
        Me.SplitMain = New System.Windows.Forms.SplitContainer()
        Me.UcLeftMenu1 = New UCLeftMenu()
        Me.UcIndTabCtrl1 = New UCIndTabCtrl()
        CType(Me.SplitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitMain.Panel1.SuspendLayout()
        Me.SplitMain.Panel2.SuspendLayout()
        Me.SplitMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitMain
        '
        Me.SplitMain.BackColor = System.Drawing.Color.IndianRed
        Me.SplitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitMain.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.SplitMain.Name = "SplitMain"
        '
        'SplitMain.Panel1
        '
        Me.SplitMain.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SplitMain.Panel1.Controls.Add(Me.UcLeftMenu1)
        Me.SplitMain.Panel1MinSize = 22
        '
        'SplitMain.Panel2
        '
        Me.SplitMain.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitMain.Panel2.Controls.Add(Me.UcIndTabCtrl1)
        Me.SplitMain.Panel2MinSize = 100
        Me.SplitMain.Size = New System.Drawing.Size(854, 330)
        Me.SplitMain.SplitterDistance = 160
        Me.SplitMain.SplitterWidth = 7
        Me.SplitMain.TabIndex = 2
        '
        'UcLeftMenu1
        '
        Me.UcLeftMenu1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.UcLeftMenu1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcLeftMenu1.Location = New System.Drawing.Point(0, 0)
        Me.UcLeftMenu1.Margin = New System.Windows.Forms.Padding(0)
        Me.UcLeftMenu1.Name = "UcLeftMenu1"
        Me.UcLeftMenu1.Size = New System.Drawing.Size(160, 330)
        Me.UcLeftMenu1.TabIndex = 1
        '
        'UcIndTabCtrl1
        '
        Me.UcIndTabCtrl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcIndTabCtrl1.Location = New System.Drawing.Point(0, 0)
        Me.UcIndTabCtrl1.Margin = New System.Windows.Forms.Padding(0)
        Me.UcIndTabCtrl1.Name = "UcIndTabCtrl1"
        Me.UcIndTabCtrl1.Size = New System.Drawing.Size(687, 330)
        Me.UcIndTabCtrl1.TabIndex = 0
        '
        'UCExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.SplitMain)
        Me.Name = "UCExplorer"
        Me.Size = New System.Drawing.Size(854, 330)
        Me.SplitMain.Panel1.ResumeLayout(False)
        Me.SplitMain.Panel2.ResumeLayout(False)
        CType(Me.SplitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitMain As System.Windows.Forms.SplitContainer
    Public WithEvents UcIndTabCtrl1 As UCIndTabCtrl
    Friend WithEvents UcLeftMenu1 As UCLeftMenu
End Class
