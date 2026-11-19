<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCCryptExplorer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCCryptExplorer))
        Me.lvTreeExp = New System.Windows.Forms.ListView()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLastAccess = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLastOpenComputer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstViewImageListState = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mniOpenFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.tstSeparatorTopEncrypt = New System.Windows.Forms.ToolStripSeparator()
        Me.mniEncrypt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDecrypt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDecryptAndOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.tstSeparatorTopZip = New System.Windows.Forms.ToolStripSeparator()
        Me.mniZip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniFileNotFoundText = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniRemoveFromLibraries = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDeleteFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.pnlCryptExplorerTop = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblListViewItemCount = New System.Windows.Forms.Label()
        Me.lblEncryptedLib = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tssStatusExplorer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssFileName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssFileCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnsOpenFile = New System.Windows.Forms.ToolStripButton()
        Me.btnsOpenFolder = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnsEncrypt = New System.Windows.Forms.ToolStripButton()
        Me.btnsDecrypt = New System.Windows.Forms.ToolStripButton()
        Me.btnsDecryptAndOpen = New System.Windows.Forms.ToolStripButton()
        Me.btnsZip = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnsRemoveFromLibrary = New System.Windows.Forms.ToolStripButton()
        Me.btnDeleteFile = New System.Windows.Forms.ToolStripButton()
        Me.UcSearch1 = New UCSearch()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.pnlCryptExplorerTop.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvTreeExp
        '
        Me.lvTreeExp.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvTreeExp.AllowColumnReorder = True
        Me.lvTreeExp.AllowDrop = True
        Me.lvTreeExp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chPath, Me.chType, Me.chSize, Me.chLastAccess, Me.chLastOpenComputer})
        Me.lvTreeExp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTreeExp.FullRowSelect = True
        Me.lvTreeExp.LabelEdit = True
        Me.lvTreeExp.Location = New System.Drawing.Point(24, 0)
        Me.lvTreeExp.Name = "lvTreeExp"
        Me.lvTreeExp.ShowItemToolTips = True
        Me.lvTreeExp.Size = New System.Drawing.Size(648, 211)
        Me.lvTreeExp.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTreeExp.StateImageList = Me.lstViewImageListState
        Me.lvTreeExp.TabIndex = 4
        Me.lvTreeExp.UseCompatibleStateImageBehavior = False
        Me.lvTreeExp.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "Nombre"
        Me.chName.Width = 218
        '
        'chPath
        '
        Me.chPath.Text = "Ruta del fichero"
        Me.chPath.Width = 350
        '
        'chType
        '
        Me.chType.Text = "Tipo"
        Me.chType.Width = 65
        '
        'chSize
        '
        Me.chSize.Text = "Tamaño"
        Me.chSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chSize.Width = 50
        '
        'chLastAccess
        '
        Me.chLastAccess.Text = "Ultimo acceso"
        Me.chLastAccess.Width = 90
        '
        'chLastOpenComputer
        '
        Me.chLastOpenComputer.Text = "Abierto en"
        Me.chLastOpenComputer.Width = 80
        '
        'lstViewImageListState
        '
        Me.lstViewImageListState.ImageStream = CType(resources.GetObject("lstViewImageListState.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.lstViewImageListState.TransparentColor = System.Drawing.Color.Transparent
        Me.lstViewImageListState.Images.SetKeyName(0, "lv_security_lock_16.png")
        Me.lstViewImageListState.Images.SetKeyName(1, "lv_filenotexistwarning1_16.png")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniOpenFile, Me.mniOpenFolder, Me.tstSeparatorTopEncrypt, Me.mniEncrypt, Me.mniDecrypt, Me.mniDecryptAndOpen, Me.tstSeparatorTopZip, Me.mniZip, Me.ToolStripSeparator3, Me.mniFileNotFoundText, Me.mniRemove, Me.mniRemoveFromLibraries, Me.mniDeleteFile})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(278, 242)
        '
        'mniOpenFile
        '
        Me.mniOpenFile.Image = Global.My.Resources.Resources.document_open_16
        Me.mniOpenFile.Name = "mniOpenFile"
        Me.mniOpenFile.Size = New System.Drawing.Size(277, 22)
        Me.mniOpenFile.Text = "Abrir"
        '
        'mniOpenFolder
        '
        Me.mniOpenFolder.Image = Global.My.Resources.Resources.folderOpen32
        Me.mniOpenFolder.Name = "mniOpenFolder"
        Me.mniOpenFolder.Size = New System.Drawing.Size(277, 22)
        Me.mniOpenFolder.Text = "Abrir carpeta contenedora"
        '
        'tstSeparatorTopEncrypt
        '
        Me.tstSeparatorTopEncrypt.Name = "tstSeparatorTopEncrypt"
        Me.tstSeparatorTopEncrypt.Size = New System.Drawing.Size(274, 6)
        '
        'mniEncrypt
        '
        Me.mniEncrypt.Image = Global.My.Resources.Resources.security_lock_16
        Me.mniEncrypt.Name = "mniEncrypt"
        Me.mniEncrypt.Size = New System.Drawing.Size(277, 22)
        Me.mniEncrypt.Text = "Encriptar"
        '
        'mniDecrypt
        '
        Me.mniDecrypt.Image = Global.My.Resources.Resources.security_unlock_16
        Me.mniDecrypt.Name = "mniDecrypt"
        Me.mniDecrypt.Size = New System.Drawing.Size(277, 22)
        Me.mniDecrypt.Text = "Desencriptar"
        '
        'mniDecryptAndOpen
        '
        Me.mniDecryptAndOpen.Image = Global.My.Resources.Resources.file_DecryptAndOpen_16
        Me.mniDecryptAndOpen.Name = "mniDecryptAndOpen"
        Me.mniDecryptAndOpen.Size = New System.Drawing.Size(277, 22)
        Me.mniDecryptAndOpen.Text = "Desencriptar y abrir"
        '
        'tstSeparatorTopZip
        '
        Me.tstSeparatorTopZip.Name = "tstSeparatorTopZip"
        Me.tstSeparatorTopZip.Size = New System.Drawing.Size(274, 6)
        '
        'mniZip
        '
        Me.mniZip.Image = Global.My.Resources.Resources.compressed_16
        Me.mniZip.Name = "mniZip"
        Me.mniZip.Size = New System.Drawing.Size(277, 22)
        Me.mniZip.Text = "Compress selected files"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(274, 6)
        '
        'mniFileNotFoundText
        '
        Me.mniFileNotFoundText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mniFileNotFoundText.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mniFileNotFoundText.Name = "mniFileNotFoundText"
        Me.mniFileNotFoundText.ShowShortcutKeys = False
        Me.mniFileNotFoundText.Size = New System.Drawing.Size(277, 22)
        Me.mniFileNotFoundText.Text = "Este fichero no existe"
        Me.mniFileNotFoundText.Visible = False
        '
        'mniRemove
        '
        Me.mniRemove.Image = Global.My.Resources.Resources.removeItem32
        Me.mniRemove.Name = "mniRemove"
        Me.mniRemove.Size = New System.Drawing.Size(277, 22)
        Me.mniRemove.Text = "Quitar de la libreria"
        Me.mniRemove.ToolTipText = "Quita el fichero de la libreria pero no del disco duro"
        '
        'mniRemoveFromLibraries
        '
        Me.mniRemoveFromLibraries.Name = "mniRemoveFromLibraries"
        Me.mniRemoveFromLibraries.Size = New System.Drawing.Size(277, 22)
        Me.mniRemoveFromLibraries.Text = "Quitar de todas las librerias(pendiente)"
        Me.mniRemoveFromLibraries.Visible = False
        '
        'mniDeleteFile
        '
        Me.mniDeleteFile.Image = Global.My.Resources.Resources.file_delete_32
        Me.mniDeleteFile.Name = "mniDeleteFile"
        Me.mniDeleteFile.Size = New System.Drawing.Size(277, 22)
        Me.mniDeleteFile.Text = "Borrar fisicamente"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'pnlCryptExplorerTop
        '
        Me.pnlCryptExplorerTop.BackColor = System.Drawing.Color.IndianRed
        Me.pnlCryptExplorerTop.Controls.Add(Me.UcSearch1)
        Me.pnlCryptExplorerTop.Controls.Add(Me.PictureBox1)
        Me.pnlCryptExplorerTop.Controls.Add(Me.lblListViewItemCount)
        Me.pnlCryptExplorerTop.Controls.Add(Me.lblEncryptedLib)
        Me.pnlCryptExplorerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCryptExplorerTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlCryptExplorerTop.Name = "pnlCryptExplorerTop"
        Me.pnlCryptExplorerTop.Size = New System.Drawing.Size(672, 34)
        Me.pnlCryptExplorerTop.TabIndex = 31
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.My.Resources.Resources.project_menu_white32
        Me.PictureBox1.Location = New System.Drawing.Point(2, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'lblListViewItemCount
        '
        Me.lblListViewItemCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListViewItemCount.BackColor = System.Drawing.Color.IndianRed
        Me.lblListViewItemCount.ForeColor = System.Drawing.Color.White
        Me.lblListViewItemCount.Location = New System.Drawing.Point(530, -2)
        Me.lblListViewItemCount.Name = "lblListViewItemCount"
        Me.lblListViewItemCount.Size = New System.Drawing.Size(140, 18)
        Me.lblListViewItemCount.TabIndex = 6
        Me.lblListViewItemCount.Text = "{0} Elementos añadidos"
        Me.lblListViewItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEncryptedLib
        '
        Me.lblEncryptedLib.BackColor = System.Drawing.Color.IndianRed
        Me.lblEncryptedLib.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncryptedLib.ForeColor = System.Drawing.Color.White
        Me.lblEncryptedLib.Location = New System.Drawing.Point(40, 0)
        Me.lblEncryptedLib.Name = "lblEncryptedLib"
        Me.lblEncryptedLib.Size = New System.Drawing.Size(502, 34)
        Me.lblEncryptedLib.TabIndex = 4
        Me.lblEncryptedLib.Text = "libreria"
        Me.lblEncryptedLib.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssStatusExplorer, Me.tssFileName, Me.tssFileCount})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 245)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.ShowItemToolTips = True
        Me.StatusStrip1.Size = New System.Drawing.Size(672, 25)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 32
        '
        'tssStatusExplorer
        '
        Me.tssStatusExplorer.AutoSize = False
        Me.tssStatusExplorer.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssStatusExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssStatusExplorer.Name = "tssStatusExplorer"
        Me.tssStatusExplorer.Size = New System.Drawing.Size(220, 20)
        Me.tssStatusExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssFileName
        '
        Me.tssFileName.AutoToolTip = True
        Me.tssFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssFileName.Name = "tssFileName"
        Me.tssFileName.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.tssFileName.Size = New System.Drawing.Size(0, 0)
        Me.tssFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssFileCount
        '
        Me.tssFileCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssFileCount.Name = "tssFileCount"
        Me.tssFileCount.Size = New System.Drawing.Size(121, 15)
        Me.tssFileCount.Text = "ToolStripStatusLabel1"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.lvTreeExp)
        Me.pnlMain.Controls.Add(Me.ToolStrip1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 34)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(672, 211)
        Me.pnlMain.TabIndex = 33
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnsOpenFile, Me.btnsOpenFolder, Me.ToolStripSeparator1, Me.btnsEncrypt, Me.btnsDecrypt, Me.btnsDecryptAndOpen, Me.btnsZip, Me.ToolStripSeparator2, Me.btnsRemoveFromLibrary, Me.btnDeleteFile})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(24, 211)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnsOpenFile
        '
        Me.btnsOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsOpenFile.Image = Global.My.Resources.Resources.document_search_32
        Me.btnsOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsOpenFile.Name = "btnsOpenFile"
        Me.btnsOpenFile.Size = New System.Drawing.Size(21, 20)
        Me.btnsOpenFile.Text = "ToolStripButton1"
        '
        'btnsOpenFolder
        '
        Me.btnsOpenFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsOpenFolder.Image = Global.My.Resources.Resources.folderOpen32
        Me.btnsOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsOpenFolder.Name = "btnsOpenFolder"
        Me.btnsOpenFolder.Size = New System.Drawing.Size(21, 20)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(21, 6)
        '
        'btnsEncrypt
        '
        Me.btnsEncrypt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsEncrypt.Image = Global.My.Resources.Resources.security_lock_32
        Me.btnsEncrypt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsEncrypt.Name = "btnsEncrypt"
        Me.btnsEncrypt.Size = New System.Drawing.Size(21, 20)
        Me.btnsEncrypt.Text = "ToolStripButton2"
        '
        'btnsDecrypt
        '
        Me.btnsDecrypt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsDecrypt.Image = Global.My.Resources.Resources.security_unlock_32
        Me.btnsDecrypt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsDecrypt.Name = "btnsDecrypt"
        Me.btnsDecrypt.Size = New System.Drawing.Size(21, 20)
        Me.btnsDecrypt.Text = "ToolStripButton2"
        '
        'btnsDecryptAndOpen
        '
        Me.btnsDecryptAndOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsDecryptAndOpen.Image = Global.My.Resources.Resources.file_DecryptAndOpen
        Me.btnsDecryptAndOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsDecryptAndOpen.Name = "btnsDecryptAndOpen"
        Me.btnsDecryptAndOpen.Size = New System.Drawing.Size(21, 20)
        '
        'btnsZip
        '
        Me.btnsZip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsZip.Image = Global.My.Resources.Resources.compressed_16
        Me.btnsZip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsZip.Name = "btnsZip"
        Me.btnsZip.Size = New System.Drawing.Size(21, 20)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(21, 6)
        '
        'btnsRemoveFromLibrary
        '
        Me.btnsRemoveFromLibrary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnsRemoveFromLibrary.Image = Global.My.Resources.Resources.removeItem32
        Me.btnsRemoveFromLibrary.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnsRemoveFromLibrary.Name = "btnsRemoveFromLibrary"
        Me.btnsRemoveFromLibrary.Size = New System.Drawing.Size(21, 20)
        '
        'btnDeleteFile
        '
        Me.btnDeleteFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDeleteFile.Image = Global.My.Resources.Resources.file_delete_32
        Me.btnDeleteFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDeleteFile.Name = "btnDeleteFile"
        Me.btnDeleteFile.Size = New System.Drawing.Size(21, 20)
        Me.btnDeleteFile.Text = "ToolStripButton1"
        '
        'UcSearch1
        '
        Me.UcSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcSearch1.Location = New System.Drawing.Point(491, 9)
        Me.UcSearch1.Name = "UcSearch1"
        Me.UcSearch1.Size = New System.Drawing.Size(176, 29)
        Me.UcSearch1.TabIndex = 8
        Me.UcSearch1.Visible = False
        '
        'UCCryptExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnlCryptExplorerTop)
        Me.DoubleBuffered = True
        Me.Name = "UCCryptExplorer"
        Me.Size = New System.Drawing.Size(672, 270)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.pnlCryptExplorerTop.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvTreeExp As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mniOpenFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniEncrypt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniDecrypt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tstSeparatorTopEncrypt As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniDecryptAndOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tstSeparatorTopZip As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chLastAccess As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLastOpenComputer As System.Windows.Forms.ColumnHeader
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents mniZip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlCryptExplorerTop As System.Windows.Forms.Panel
    Friend WithEvents lblListViewItemCount As System.Windows.Forms.Label
    Friend WithEvents lblEncryptedLib As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tssStatusExplorer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssFileCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lstViewImageListState As System.Windows.Forms.ImageList
    Friend WithEvents mniRemoveFromLibraries As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniFileNotFoundText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlMain As Panel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnsOpenFile As ToolStripButton
    Friend WithEvents btnsOpenFolder As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnsEncrypt As ToolStripButton
    Friend WithEvents btnsDecrypt As ToolStripButton
    Friend WithEvents btnsDecryptAndOpen As ToolStripButton
    Friend WithEvents btnsZip As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnsRemoveFromLibrary As ToolStripButton
    Friend WithEvents mniDeleteFile As ToolStripMenuItem
    Friend WithEvents btnDeleteFile As ToolStripButton
    Friend WithEvents UcSearch1 As UCSearch
End Class
