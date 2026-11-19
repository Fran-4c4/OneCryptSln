<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCTreeView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCTreeView))
        Me.TreeV = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mniProjectSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProjectSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSaveSelectedItemsAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEncryptOpenLibraries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDeleteLibrary = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTit = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeV
        '
        Me.TreeV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeV.ImageIndex = 0
        Me.TreeV.ImageList = Me.ImageList1
        Me.TreeV.Indent = 12
        Me.TreeV.Location = New System.Drawing.Point(0, 32)
        Me.TreeV.Name = "TreeV"
        Me.TreeV.SelectedImageIndex = 0
        Me.TreeV.Size = New System.Drawing.Size(142, 298)
        Me.TreeV.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "project_menu.png")
        Me.ImageList1.Images.SetKeyName(1, "pwdManager_menu_32.png")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniProjectSave, Me.mniProjectSaveAs, Me.btnSaveSelectedItemsAs, Me.ToolStripSeparator2, Me.btnEncryptOpenLibraries, Me.ToolStripSeparator1, Me.btnDeleteLibrary})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(276, 126)
        '
        'mniProjectSave
        '
        Me.mniProjectSave.Name = "mniProjectSave"
        Me.mniProjectSave.Size = New System.Drawing.Size(275, 22)
        Me.mniProjectSave.Text = "Guardar Libreria"
        '
        'mniProjectSaveAs
        '
        Me.mniProjectSaveAs.Name = "mniProjectSaveAs"
        Me.mniProjectSaveAs.Size = New System.Drawing.Size(275, 22)
        Me.mniProjectSaveAs.Text = "Guardar Libreria como"
        '
        'btnSaveSelectedItemsAs
        '
        Me.btnSaveSelectedItemsAs.Name = "btnSaveSelectedItemsAs"
        Me.btnSaveSelectedItemsAs.Size = New System.Drawing.Size(275, 22)
        Me.btnSaveSelectedItemsAs.Text = "Guardar Ficheros Seleccionados como"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(272, 6)
        '
        'btnEncryptOpenLibraries
        '
        Me.btnEncryptOpenLibraries.Name = "btnEncryptOpenLibraries"
        Me.btnEncryptOpenLibraries.Size = New System.Drawing.Size(275, 22)
        Me.btnEncryptOpenLibraries.Text = "Encriptar Libreria"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(272, 6)
        '
        'btnDeleteLibrary
        '
        Me.btnDeleteLibrary.Name = "btnDeleteLibrary"
        Me.btnDeleteLibrary.Size = New System.Drawing.Size(275, 22)
        Me.btnDeleteLibrary.Text = "Borrar"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblTit)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(142, 32)
        Me.pnlTop.TabIndex = 1
        Me.pnlTop.Visible = False
        '
        'lblTit
        '
        Me.lblTit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTit.Location = New System.Drawing.Point(0, 0)
        Me.lblTit.Name = "lblTit"
        Me.lblTit.Size = New System.Drawing.Size(142, 16)
        Me.lblTit.TabIndex = 0
        Me.lblTit.Text = "TreeTitle"
        Me.lblTit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TreeV)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "UCTreeView"
        Me.Size = New System.Drawing.Size(142, 330)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblTit As Label
    Friend WithEvents ImageList1 As ImageList
    Public WithEvents TreeV As TreeView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents btnDeleteLibrary As ToolStripMenuItem
    Friend WithEvents mniProjectSave As ToolStripMenuItem
    Friend WithEvents mniProjectSaveAs As ToolStripMenuItem
    Friend WithEvents btnSaveSelectedItemsAs As ToolStripMenuItem
    Friend WithEvents btnEncryptOpenLibraries As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
End Class
