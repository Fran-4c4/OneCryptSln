<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCpwdListView
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCpwdListView))
        Me.lstViewEnt = New System.Windows.Forms.ListView()
        Me.clnEntry = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnUserName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnPassword = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnNotes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnDateCreation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnDateModified = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clnAutotype = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList16 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btnCopyUserNameMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCopypwdMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpenURLMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCopyUrlMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAutoTypeMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEditEntryMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRemoveEntryMni = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAddEntry = New System.Windows.Forms.ToolStripButton()
        Me.btnViewGroups = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCopyUserName = New System.Windows.Forms.ToolStripButton()
        Me.btnCopypwd = New System.Windows.Forms.ToolStripButton()
        Me.btnCopyURL = New System.Windows.Forms.ToolStripButton()
        Me.btnOpenURL = New System.Windows.Forms.ToolStripButton()
        Me.btnAutoType = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnShowPwd = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnConfig = New System.Windows.Forms.ToolStripButton()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.rhEntryView = New System.Windows.Forms.RichTextBox()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstViewEnt
        '
        Me.lstViewEnt.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clnEntry, Me.clnUserName, Me.clnPassword, Me.clnURL, Me.clnNotes, Me.clnDateCreation, Me.clnDateModified, Me.clnAutotype})
        Me.lstViewEnt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstViewEnt.FullRowSelect = True
        Me.lstViewEnt.Location = New System.Drawing.Point(0, 38)
        Me.lstViewEnt.Name = "lstViewEnt"
        Me.lstViewEnt.ShowItemToolTips = True
        Me.lstViewEnt.Size = New System.Drawing.Size(614, 218)
        Me.lstViewEnt.SmallImageList = Me.ImageList16
        Me.lstViewEnt.TabIndex = 0
        Me.lstViewEnt.UseCompatibleStateImageBehavior = False
        Me.lstViewEnt.View = System.Windows.Forms.View.Details
        '
        'clnEntry
        '
        Me.clnEntry.Text = "Entry"
        Me.clnEntry.Width = 140
        '
        'clnUserName
        '
        Me.clnUserName.Text = "User Name"
        Me.clnUserName.Width = 140
        '
        'clnPassword
        '
        Me.clnPassword.Text = "password"
        Me.clnPassword.Width = 80
        '
        'clnURL
        '
        Me.clnURL.Text = "Web URL"
        Me.clnURL.Width = 180
        '
        'clnNotes
        '
        Me.clnNotes.Text = "Notes"
        Me.clnNotes.Width = 200
        '
        'clnDateCreation
        '
        Me.clnDateCreation.Text = "Creation"
        Me.clnDateCreation.Width = 100
        '
        'clnDateModified
        '
        Me.clnDateModified.Text = "Modified"
        Me.clnDateModified.Width = 100
        '
        'clnAutotype
        '
        Me.clnAutotype.Width = 100
        '
        'ImageList16
        '
        Me.ImageList16.ImageStream = CType(resources.GetObject("ImageList16.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList16.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList16.Images.SetKeyName(0, "Generic16.png")
        Me.ImageList16.Images.SetKeyName(1, "url16.png")
        Me.ImageList16.Images.SetKeyName(2, "mail16.png")
        Me.ImageList16.Images.SetKeyName(3, "card16.png")
        Me.ImageList16.Images.SetKeyName(4, "bank16.png")
        Me.ImageList16.Images.SetKeyName(5, "other16.png")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCopyUserNameMni, Me.btnCopypwdMni, Me.btnOpenURLMni, Me.btnCopyUrlMni, Me.btnAutoTypeMni, Me.ToolStripSeparator3, Me.btnEditEntryMni, Me.btnRemoveEntryMni})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(292, 164)
        '
        'btnCopyUserNameMni
        '
        Me.btnCopyUserNameMni.Name = "btnCopyUserNameMni"
        Me.btnCopyUserNameMni.Size = New System.Drawing.Size(291, 22)
        Me.btnCopyUserNameMni.Text = "Copy user name to clipboard"
        '
        'btnCopypwdMni
        '
        Me.btnCopypwdMni.Name = "btnCopypwdMni"
        Me.btnCopypwdMni.Size = New System.Drawing.Size(291, 22)
        Me.btnCopypwdMni.Text = "Copy password to clipboard"
        '
        'btnOpenURLMni
        '
        Me.btnOpenURLMni.Name = "btnOpenURLMni"
        Me.btnOpenURLMni.Size = New System.Drawing.Size(291, 22)
        Me.btnOpenURLMni.Text = "Open location"
        '
        'btnCopyUrlMni
        '
        Me.btnCopyUrlMni.Name = "btnCopyUrlMni"
        Me.btnCopyUrlMni.Size = New System.Drawing.Size(291, 22)
        Me.btnCopyUrlMni.Text = "Copy location to clipboard"
        '
        'btnAutoTypeMni
        '
        Me.btnAutoTypeMni.Name = "btnAutoTypeMni"
        Me.btnAutoTypeMni.Size = New System.Drawing.Size(291, 22)
        Me.btnAutoTypeMni.Text = "Autotype data entry to previous windows"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(288, 6)
        '
        'btnEditEntryMni
        '
        Me.btnEditEntryMni.Name = "btnEditEntryMni"
        Me.btnEditEntryMni.Size = New System.Drawing.Size(291, 22)
        Me.btnEditEntryMni.Text = "Edit Entry"
        '
        'btnRemoveEntryMni
        '
        Me.btnRemoveEntryMni.Image = Global.My.Resources.Resources.RemoveItem
        Me.btnRemoveEntryMni.Name = "btnRemoveEntryMni"
        Me.btnRemoveEntryMni.Size = New System.Drawing.Size(291, 22)
        Me.btnRemoveEntryMni.Text = "Remove entry"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.ToolStrip1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(614, 38)
        Me.pnlTop.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAddEntry, Me.btnViewGroups, Me.ToolStripSeparator2, Me.btnCopyUserName, Me.btnCopypwd, Me.btnCopyURL, Me.btnOpenURL, Me.btnAutoType, Me.ToolStripSeparator1, Me.btnShowPwd, Me.ToolStripSeparator4, Me.btnConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(405, 39)
        Me.ToolStrip1.TabIndex = 0
        '
        'btnAddEntry
        '
        Me.btnAddEntry.AutoSize = False
        Me.btnAddEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAddEntry.Image = Global.My.Resources.Resources.document_search_32
        Me.btnAddEntry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnAddEntry.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddEntry.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnAddEntry.Name = "btnAddEntry"
        Me.btnAddEntry.Size = New System.Drawing.Size(36, 36)
        '
        'btnViewGroups
        '
        Me.btnViewGroups.AutoSize = False
        Me.btnViewGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnViewGroups.Image = CType(resources.GetObject("btnViewGroups.Image"), System.Drawing.Image)
        Me.btnViewGroups.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewGroups.Margin = New System.Windows.Forms.Padding(0)
        Me.btnViewGroups.Name = "btnViewGroups"
        Me.btnViewGroups.Size = New System.Drawing.Size(36, 36)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.AutoSize = False
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(16, 39)
        '
        'btnCopyUserName
        '
        Me.btnCopyUserName.AutoSize = False
        Me.btnCopyUserName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCopyUserName.Enabled = False
        Me.btnCopyUserName.Image = CType(resources.GetObject("btnCopyUserName.Image"), System.Drawing.Image)
        Me.btnCopyUserName.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCopyUserName.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnCopyUserName.Name = "btnCopyUserName"
        Me.btnCopyUserName.Size = New System.Drawing.Size(36, 36)
        '
        'btnCopypwd
        '
        Me.btnCopypwd.AutoSize = False
        Me.btnCopypwd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCopypwd.Enabled = False
        Me.btnCopypwd.Image = CType(resources.GetObject("btnCopypwd.Image"), System.Drawing.Image)
        Me.btnCopypwd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCopypwd.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnCopypwd.Name = "btnCopypwd"
        Me.btnCopypwd.Size = New System.Drawing.Size(36, 36)
        '
        'btnCopyURL
        '
        Me.btnCopyURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCopyURL.Enabled = False
        Me.btnCopyURL.Image = CType(resources.GetObject("btnCopyURL.Image"), System.Drawing.Image)
        Me.btnCopyURL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCopyURL.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnCopyURL.Name = "btnCopyURL"
        Me.btnCopyURL.Size = New System.Drawing.Size(36, 39)
        '
        'btnOpenURL
        '
        Me.btnOpenURL.AutoSize = False
        Me.btnOpenURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnOpenURL.Enabled = False
        Me.btnOpenURL.Image = CType(resources.GetObject("btnOpenURL.Image"), System.Drawing.Image)
        Me.btnOpenURL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOpenURL.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnOpenURL.Name = "btnOpenURL"
        Me.btnOpenURL.Size = New System.Drawing.Size(36, 36)
        '
        'btnAutoType
        '
        Me.btnAutoType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAutoType.Enabled = False
        Me.btnAutoType.Image = CType(resources.GetObject("btnAutoType.Image"), System.Drawing.Image)
        Me.btnAutoType.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAutoType.Margin = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.btnAutoType.Name = "btnAutoType"
        Me.btnAutoType.Size = New System.Drawing.Size(36, 39)
        Me.btnAutoType.Text = "Autotype"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.AutoSize = False
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(16, 39)
        '
        'btnShowPwd
        '
        Me.btnShowPwd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnShowPwd.Image = CType(resources.GetObject("btnShowPwd.Image"), System.Drawing.Image)
        Me.btnShowPwd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnShowPwd.Margin = New System.Windows.Forms.Padding(0)
        Me.btnShowPwd.Name = "btnShowPwd"
        Me.btnShowPwd.Size = New System.Drawing.Size(36, 39)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.AutoSize = False
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(16, 39)
        '
        'btnConfig
        '
        Me.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnConfig.Image = CType(resources.GetObject("btnConfig.Image"), System.Drawing.Image)
        Me.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnConfig.Margin = New System.Windows.Forms.Padding(0)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(36, 39)
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.rhEntryView)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 256)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(614, 66)
        Me.pnlBottom.TabIndex = 2
        '
        'rhEntryView
        '
        Me.rhEntryView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rhEntryView.Location = New System.Drawing.Point(0, 0)
        Me.rhEntryView.Name = "rhEntryView"
        Me.rhEntryView.Size = New System.Drawing.Size(614, 66)
        Me.rhEntryView.TabIndex = 0
        Me.rhEntryView.Text = ""
        '
        'UCpwdListView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.lstViewEnt)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Name = "UCpwdListView"
        Me.Size = New System.Drawing.Size(614, 322)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstViewEnt As System.Windows.Forms.ListView
    Friend WithEvents clnEntry As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnUserName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnPassword As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnNotes As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnDateCreation As System.Windows.Forms.ColumnHeader
    Friend WithEvents clnDateModified As System.Windows.Forms.ColumnHeader
    Friend WithEvents ImageList16 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAddEntry As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnViewGroups As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCopyUserName As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCopypwd As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOpenURL As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnShowPwd As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCopyUserNameMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCopypwdMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnOpenURLMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCopyUrlMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRemoveEntryMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCopyURL As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEditEntryMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents clnAutotype As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAutoType As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAutoTypeMni As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents rhEntryView As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnConfig As System.Windows.Forms.ToolStripButton

End Class
