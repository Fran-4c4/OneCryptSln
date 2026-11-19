Imports System
Imports System.IO
Imports System.Security
Imports System.Globalization
'Imports frms.hook


Partial Public Class frmMain
    Inherits System.Windows.Forms.Form
    Implements UCPwdController.IPwdSender

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        initByNew()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlTopTitle As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents UcExplorer1 As UCExplorer
    Friend WithEvents ToolStripEncryptMain As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbFileDecryptAndOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbFileEncrypt As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbFileDecrypt As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbOptions As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mniChangePwd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbchangeLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CastellanoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnglishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniCheckOpenAfterDecrypt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniOpenOptionsWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbFolderAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbProjectsMain As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mniProjectSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProjectNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripOptions As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparatorCloseMenu As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mniProjectRecents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniProjectSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbCompressFiles As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbLockScreen As System.Windows.Forms.ToolStripButton
    Friend WithEvents mniProjectRecentsFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniDDBBQuery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSaveSelectedItemsAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPwdManagerOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnTextCrypt As System.Windows.Forms.ToolStripButton
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents CtxMenuNotifyIcon As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ctxNotify_btnEncryptText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxNotify_btnCloseApplication As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxNotify_btnOpenPwdManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ctxNotify_btnShowOneCrypt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniCloseApplication As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ctxNotify_btnLockComputer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents mniProjectSaveAll As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents btnCheckNewVersion As ToolStripMenuItem
    Friend WithEvents btnEncryptOpenLibraries As System.Windows.Forms.ToolStripMenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppEventsClass1 As ind.encrypter.AppEventsClass = New ind.encrypter.AppEventsClass()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTopTitle = New System.Windows.Forms.Panel()
        Me.ToolStripOptions = New System.Windows.Forms.ToolStrip()
        Me.tsbOptions = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mniCheckOpenAfterDecrypt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniOpenOptionsWindow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniChangePwd = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbchangeLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.CastellanoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EnglishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniDDBBQuery = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnCheckNewVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparatorCloseMenu = New System.Windows.Forms.ToolStripSeparator()
        Me.mniCloseApplication = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripEncryptMain = New System.Windows.Forms.ToolStrip()
        Me.btnPwdManagerOpen = New System.Windows.Forms.ToolStripButton()
        Me.btnTextCrypt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbProjectsMain = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mniProjectRecents = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniProjectSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProjectSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSaveSelectedItemsAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProjectSaveAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnEncryptOpenLibraries = New System.Windows.Forms.ToolStripMenuItem()
        Me.mniProjectNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mniProjectRecentsFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFolderAdd = New System.Windows.Forms.ToolStripButton()
        Me.tsbFileEncrypt = New System.Windows.Forms.ToolStripButton()
        Me.tsbFileDecrypt = New System.Windows.Forms.ToolStripButton()
        Me.tsbFileDecryptAndOpen = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbCompressFiles = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbLockScreen = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.UcExplorer1 = New UCExplorer()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.CtxMenuNotifyIcon = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ctxNotify_btnShowOneCrypt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxNotify_btnEncryptText = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxNotify_btnOpenPwdManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.ctxNotify_btnLockComputer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ctxNotify_btnCloseApplication = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlTopTitle.SuspendLayout()
        Me.ToolStripOptions.SuspendLayout()
        Me.ToolStripEncryptMain.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.CtxMenuNotifyIcon.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopTitle
        '
        Me.pnlTopTitle.BackColor = System.Drawing.Color.White
        Me.pnlTopTitle.BackgroundImage = Global.My.Resources.Resources.topMenuBackGround_02
        Me.pnlTopTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopTitle.Controls.Add(Me.ToolStripOptions)
        Me.pnlTopTitle.Controls.Add(Me.ToolStripEncryptMain)
        Me.pnlTopTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopTitle.Name = "pnlTopTitle"
        Me.pnlTopTitle.Size = New System.Drawing.Size(978, 64)
        Me.pnlTopTitle.TabIndex = 26
        '
        'ToolStripOptions
        '
        Me.ToolStripOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStripOptions.AutoSize = False
        Me.ToolStripOptions.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripOptions.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripOptions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripOptions.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOptions})
        Me.ToolStripOptions.Location = New System.Drawing.Point(932, 0)
        Me.ToolStripOptions.Name = "ToolStripOptions"
        Me.ToolStripOptions.Size = New System.Drawing.Size(122, 54)
        Me.ToolStripOptions.TabIndex = 2
        '
        'tsbOptions
        '
        Me.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniCheckOpenAfterDecrypt, Me.mniOpenOptionsWindow, Me.mniChangePwd, Me.tsbchangeLanguage, Me.mniDDBBQuery, Me.mniHelp, Me.btnCheckNewVersion, Me.mniAbout, Me.ToolStripSeparatorCloseMenu, Me.mniCloseApplication})
        Me.tsbOptions.Image = Global.My.Resources.Resources.menu
        Me.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOptions.Name = "tsbOptions"
        Me.tsbOptions.Size = New System.Drawing.Size(45, 51)
        Me.tsbOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbOptions.ToolTipText = "Opciones"
        '
        'mniCheckOpenAfterDecrypt
        '
        Me.mniCheckOpenAfterDecrypt.Checked = True
        Me.mniCheckOpenAfterDecrypt.CheckOnClick = True
        Me.mniCheckOpenAfterDecrypt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mniCheckOpenAfterDecrypt.Name = "mniCheckOpenAfterDecrypt"
        Me.mniCheckOpenAfterDecrypt.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded
        Me.mniCheckOpenAfterDecrypt.Size = New System.Drawing.Size(211, 22)
        Me.mniCheckOpenAfterDecrypt.Text = "Abrir al desencriptar"
        Me.mniCheckOpenAfterDecrypt.ToolTipText = "Marque esta opción si desea que se abran los ficheros automaticamente despues de " &
    "desencriptar"
        '
        'mniOpenOptionsWindow
        '
        Me.mniOpenOptionsWindow.Image = Global.My.Resources.Resources.options
        Me.mniOpenOptionsWindow.Name = "mniOpenOptionsWindow"
        Me.mniOpenOptionsWindow.Size = New System.Drawing.Size(211, 22)
        Me.mniOpenOptionsWindow.Text = "Configurar"
        '
        'mniChangePwd
        '
        Me.mniChangePwd.Image = Global.My.Resources.Resources.security_key
        Me.mniChangePwd.Name = "mniChangePwd"
        Me.mniChangePwd.Size = New System.Drawing.Size(211, 22)
        Me.mniChangePwd.Text = "Cambiar contraseña"
        '
        'tsbchangeLanguage
        '
        Me.tsbchangeLanguage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CastellanoToolStripMenuItem, Me.EnglishToolStripMenuItem})
        Me.tsbchangeLanguage.Name = "tsbchangeLanguage"
        Me.tsbchangeLanguage.Size = New System.Drawing.Size(211, 22)
        Me.tsbchangeLanguage.Text = "Cambiar Idioma"
        '
        'CastellanoToolStripMenuItem
        '
        Me.CastellanoToolStripMenuItem.Name = "CastellanoToolStripMenuItem"
        Me.CastellanoToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.CastellanoToolStripMenuItem.Text = "Castellano"
        '
        'EnglishToolStripMenuItem
        '
        Me.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem"
        Me.EnglishToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.EnglishToolStripMenuItem.Text = "English"
        '
        'mniDDBBQuery
        '
        Me.mniDDBBQuery.Name = "mniDDBBQuery"
        Me.mniDDBBQuery.Size = New System.Drawing.Size(211, 22)
        Me.mniDDBBQuery.Text = "Database Tool"
        Me.mniDDBBQuery.Visible = False
        '
        'mniHelp
        '
        Me.mniHelp.Image = Global.My.Resources.Resources.Info
        Me.mniHelp.Name = "mniHelp"
        Me.mniHelp.Size = New System.Drawing.Size(211, 22)
        Me.mniHelp.Text = "Ayuda"
        Me.mniHelp.ToolTipText = "Abrir la ayuda de la aplicación"
        '
        'btnCheckNewVersion
        '
        Me.btnCheckNewVersion.Name = "btnCheckNewVersion"
        Me.btnCheckNewVersion.Size = New System.Drawing.Size(211, 22)
        Me.btnCheckNewVersion.Text = "Comprobar nueva version"
        '
        'mniAbout
        '
        Me.mniAbout.Name = "mniAbout"
        Me.mniAbout.Size = New System.Drawing.Size(211, 22)
        Me.mniAbout.Text = "Sobre"
        '
        'ToolStripSeparatorCloseMenu
        '
        Me.ToolStripSeparatorCloseMenu.Name = "ToolStripSeparatorCloseMenu"
        Me.ToolStripSeparatorCloseMenu.Size = New System.Drawing.Size(208, 6)
        '
        'mniCloseApplication
        '
        Me.mniCloseApplication.Image = Global.My.Resources.Resources.on_shutdown_button
        Me.mniCloseApplication.Name = "mniCloseApplication"
        Me.mniCloseApplication.Size = New System.Drawing.Size(211, 22)
        Me.mniCloseApplication.Text = "Cerrar aplicacion"
        '
        'ToolStripEncryptMain
        '
        Me.ToolStripEncryptMain.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripEncryptMain.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripEncryptMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripEncryptMain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripEncryptMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPwdManagerOpen, Me.btnTextCrypt, Me.ToolStripSeparator3, Me.tsbProjectsMain, Me.ToolStripSeparator5, Me.tsbFolderAdd, Me.tsbFileEncrypt, Me.tsbFileDecrypt, Me.tsbFileDecryptAndOpen, Me.ToolStripSeparator1, Me.tsbCompressFiles, Me.ToolStripSeparator6, Me.tsbLockScreen})
        Me.ToolStripEncryptMain.Location = New System.Drawing.Point(4, 0)
        Me.ToolStripEncryptMain.Name = "ToolStripEncryptMain"
        Me.ToolStripEncryptMain.Size = New System.Drawing.Size(724, 54)
        Me.ToolStripEncryptMain.TabIndex = 2
        Me.ToolStripEncryptMain.Text = "Menu actions"
        '
        'btnPwdManagerOpen
        '
        Me.btnPwdManagerOpen.AutoSize = False
        Me.btnPwdManagerOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPwdManagerOpen.Image = Global.My.Resources.Resources.pwdManager_menu_32
        Me.btnPwdManagerOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPwdManagerOpen.Margin = New System.Windows.Forms.Padding(5, 1, 5, 2)
        Me.btnPwdManagerOpen.Name = "btnPwdManagerOpen"
        Me.btnPwdManagerOpen.Size = New System.Drawing.Size(58, 51)
        Me.btnPwdManagerOpen.ToolTipText = "Password Manager"
        '
        'btnTextCrypt
        '
        Me.btnTextCrypt.AutoSize = False
        Me.btnTextCrypt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnTextCrypt.Image = Global.My.Resources.Resources.document_Encrypt_32
        Me.btnTextCrypt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTextCrypt.Margin = New System.Windows.Forms.Padding(0, 1, 5, 2)
        Me.btnTextCrypt.Name = "btnTextCrypt"
        Me.btnTextCrypt.Size = New System.Drawing.Size(58, 51)
        Me.btnTextCrypt.ToolTipText = "Encrypt/Decrypt text"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 54)
        '
        'tsbProjectsMain
        '
        Me.tsbProjectsMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniProjectRecents, Me.ToolStripSeparator4, Me.mniProjectSave, Me.mniProjectSaveAs, Me.btnSaveSelectedItemsAs, Me.mniProjectSaveAll, Me.ToolStripSeparator8, Me.btnEncryptOpenLibraries, Me.mniProjectNew, Me.ToolStripSeparator2, Me.mniProjectRecentsFiles})
        Me.tsbProjectsMain.Image = Global.My.Resources.Resources.project_menu
        Me.tsbProjectsMain.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbProjectsMain.Name = "tsbProjectsMain"
        Me.tsbProjectsMain.Size = New System.Drawing.Size(64, 51)
        Me.tsbProjectsMain.Text = "Librerias"
        Me.tsbProjectsMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbProjectsMain.ToolTipText = "Gestión de Librerias"
        '
        'mniProjectRecents
        '
        Me.mniProjectRecents.Image = Global.My.Resources.Resources.project_history
        Me.mniProjectRecents.Name = "mniProjectRecents"
        Me.mniProjectRecents.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectRecents.Text = "librerias Recientes"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(269, 6)
        '
        'mniProjectSave
        '
        Me.mniProjectSave.Enabled = False
        Me.mniProjectSave.Image = Global.My.Resources.Resources.project_save
        Me.mniProjectSave.Name = "mniProjectSave"
        Me.mniProjectSave.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectSave.Text = "Guardar libreria"
        '
        'mniProjectSaveAs
        '
        Me.mniProjectSaveAs.Enabled = False
        Me.mniProjectSaveAs.Image = Global.My.Resources.Resources.project_save_as1
        Me.mniProjectSaveAs.Name = "mniProjectSaveAs"
        Me.mniProjectSaveAs.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectSaveAs.Text = "Guardar libreria como"
        '
        'btnSaveSelectedItemsAs
        '
        Me.btnSaveSelectedItemsAs.Enabled = False
        Me.btnSaveSelectedItemsAs.Image = Global.My.Resources.Resources.project_save_as1
        Me.btnSaveSelectedItemsAs.Name = "btnSaveSelectedItemsAs"
        Me.btnSaveSelectedItemsAs.Size = New System.Drawing.Size(272, 22)
        Me.btnSaveSelectedItemsAs.Text = "Guardar ficheros seleccionados como"
        '
        'mniProjectSaveAll
        '
        Me.mniProjectSaveAll.Image = Global.My.Resources.Resources.project_save
        Me.mniProjectSaveAll.Name = "mniProjectSaveAll"
        Me.mniProjectSaveAll.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectSaveAll.Text = "Guardar todas las librerias"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(269, 6)
        '
        'btnEncryptOpenLibraries
        '
        Me.btnEncryptOpenLibraries.Enabled = False
        Me.btnEncryptOpenLibraries.Image = Global.My.Resources.Resources.project_Crypt_32
        Me.btnEncryptOpenLibraries.Name = "btnEncryptOpenLibraries"
        Me.btnEncryptOpenLibraries.Size = New System.Drawing.Size(272, 22)
        Me.btnEncryptOpenLibraries.Text = "Encriptar libreria"
        '
        'mniProjectNew
        '
        Me.mniProjectNew.Image = Global.My.Resources.Resources.project_add
        Me.mniProjectNew.Name = "mniProjectNew"
        Me.mniProjectNew.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectNew.Text = "Nueva libreria"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(269, 6)
        '
        'mniProjectRecentsFiles
        '
        Me.mniProjectRecentsFiles.Image = Global.My.Resources.Resources.project_menu_recentfiles_32
        Me.mniProjectRecentsFiles.Name = "mniProjectRecentsFiles"
        Me.mniProjectRecentsFiles.Size = New System.Drawing.Size(272, 22)
        Me.mniProjectRecentsFiles.Text = "Archivos abiertos recientemente"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 54)
        '
        'tsbFolderAdd
        '
        Me.tsbFolderAdd.Enabled = False
        Me.tsbFolderAdd.Image = Global.My.Resources.Resources.folder_add_32
        Me.tsbFolderAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFolderAdd.Name = "tsbFolderAdd"
        Me.tsbFolderAdd.Size = New System.Drawing.Size(88, 51)
        Me.tsbFolderAdd.Text = "Añadir carpeta"
        Me.tsbFolderAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbFolderAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbFolderAdd.Visible = False
        '
        'tsbFileEncrypt
        '
        Me.tsbFileEncrypt.Enabled = False
        Me.tsbFileEncrypt.Image = Global.My.Resources.Resources.security_lock_32
        Me.tsbFileEncrypt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFileEncrypt.Name = "tsbFileEncrypt"
        Me.tsbFileEncrypt.Size = New System.Drawing.Size(58, 51)
        Me.tsbFileEncrypt.Text = "Encriptar"
        Me.tsbFileEncrypt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbFileEncrypt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbFileEncrypt.ToolTipText = "Encriptar los ficheros seleccionados"
        '
        'tsbFileDecrypt
        '
        Me.tsbFileDecrypt.Enabled = False
        Me.tsbFileDecrypt.Image = Global.My.Resources.Resources.security_unlock_32
        Me.tsbFileDecrypt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFileDecrypt.Name = "tsbFileDecrypt"
        Me.tsbFileDecrypt.Size = New System.Drawing.Size(77, 51)
        Me.tsbFileDecrypt.Text = "Desencriptar"
        Me.tsbFileDecrypt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbFileDecrypt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbFileDecrypt.ToolTipText = "Desencriptar los ficheros seleccionados"
        '
        'tsbFileDecryptAndOpen
        '
        Me.tsbFileDecryptAndOpen.CheckOnClick = True
        Me.tsbFileDecryptAndOpen.Enabled = False
        Me.tsbFileDecryptAndOpen.Image = Global.My.Resources.Resources.file_DecryptAndOpen
        Me.tsbFileDecryptAndOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFileDecryptAndOpen.Name = "tsbFileDecryptAndOpen"
        Me.tsbFileDecryptAndOpen.Size = New System.Drawing.Size(113, 51)
        Me.tsbFileDecryptAndOpen.Text = "Desencriptar y abrir"
        Me.tsbFileDecryptAndOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbFileDecryptAndOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbFileDecryptAndOpen.ToolTipText = "Desencriptar y abrir con la aplicación asociada en windows"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 54)
        '
        'tsbCompressFiles
        '
        Me.tsbCompressFiles.Enabled = False
        Me.tsbCompressFiles.Image = Global.My.Resources.Resources.compressed_48
        Me.tsbCompressFiles.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.tsbCompressFiles.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCompressFiles.Name = "tsbCompressFiles"
        Me.tsbCompressFiles.Size = New System.Drawing.Size(64, 51)
        Me.tsbCompressFiles.Text = "Compress"
        Me.tsbCompressFiles.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbCompressFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 54)
        '
        'tsbLockScreen
        '
        Me.tsbLockScreen.Image = Global.My.Resources.Resources.system_lock_screen
        Me.tsbLockScreen.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.tsbLockScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbLockScreen.Name = "tsbLockScreen"
        Me.tsbLockScreen.Size = New System.Drawing.Size(71, 51)
        Me.tsbLockScreen.Text = "LockScreen"
        Me.tsbLockScreen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.tsbLockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.UcExplorer1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 64)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(978, 392)
        Me.pnlMain.TabIndex = 28
        '
        'UcExplorer1
        '
        Me.UcExplorer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcExplorer1.Location = New System.Drawing.Point(0, 0)
        Me.UcExplorer1.myAppEvents = AppEventsClass1
        Me.UcExplorer1.Name = "UcExplorer1"
        Me.UcExplorer1.Size = New System.Drawing.Size(978, 392)
        Me.UcExplorer1.TabIndex = 26
        Me.UcExplorer1.ucRecentFilesTab = Nothing
        Me.UcExplorer1.Visible = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.CtxMenuNotifyIcon
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "OneCrypt"
        '
        'CtxMenuNotifyIcon
        '
        Me.CtxMenuNotifyIcon.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ctxNotify_btnShowOneCrypt, Me.ctxNotify_btnEncryptText, Me.ctxNotify_btnOpenPwdManager, Me.ctxNotify_btnLockComputer, Me.ToolStripSeparator7, Me.ctxNotify_btnCloseApplication})
        Me.CtxMenuNotifyIcon.Name = "CtxMenuNotifyIcon"
        Me.CtxMenuNotifyIcon.Size = New System.Drawing.Size(207, 120)
        '
        'ctxNotify_btnShowOneCrypt
        '
        Me.ctxNotify_btnShowOneCrypt.Name = "ctxNotify_btnShowOneCrypt"
        Me.ctxNotify_btnShowOneCrypt.Size = New System.Drawing.Size(206, 22)
        Me.ctxNotify_btnShowOneCrypt.Text = "Show OneCrypt"
        Me.ctxNotify_btnShowOneCrypt.Visible = False
        '
        'ctxNotify_btnEncryptText
        '
        Me.ctxNotify_btnEncryptText.Image = Global.My.Resources.Resources.document_Encrypt_32
        Me.ctxNotify_btnEncryptText.Name = "ctxNotify_btnEncryptText"
        Me.ctxNotify_btnEncryptText.Size = New System.Drawing.Size(206, 22)
        Me.ctxNotify_btnEncryptText.Text = "Encrypt/Decrypt text"
        '
        'ctxNotify_btnOpenPwdManager
        '
        Me.ctxNotify_btnOpenPwdManager.Image = Global.My.Resources.Resources.pwdManager_menu_32
        Me.ctxNotify_btnOpenPwdManager.Name = "ctxNotify_btnOpenPwdManager"
        Me.ctxNotify_btnOpenPwdManager.Size = New System.Drawing.Size(206, 22)
        Me.ctxNotify_btnOpenPwdManager.Text = "Open password manager"
        '
        'ctxNotify_btnLockComputer
        '
        Me.ctxNotify_btnLockComputer.Image = Global.My.Resources.Resources.system_lock_screen
        Me.ctxNotify_btnLockComputer.Name = "ctxNotify_btnLockComputer"
        Me.ctxNotify_btnLockComputer.Size = New System.Drawing.Size(206, 22)
        Me.ctxNotify_btnLockComputer.Text = "Lock Computer"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(203, 6)
        '
        'ctxNotify_btnCloseApplication
        '
        Me.ctxNotify_btnCloseApplication.Image = Global.My.Resources.Resources.on_shutdown_button
        Me.ctxNotify_btnCloseApplication.Name = "ctxNotify_btnCloseApplication"
        Me.ctxNotify_btnCloseApplication.Size = New System.Drawing.Size(206, 22)
        Me.ctxNotify_btnCloseApplication.Text = "Close Application"
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(978, 456)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTopTitle)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "OneCrypt"
        Me.pnlTopTitle.ResumeLayout(False)
        Me.pnlTopTitle.PerformLayout()
        Me.ToolStripOptions.ResumeLayout(False)
        Me.ToolStripOptions.PerformLayout()
        Me.ToolStripEncryptMain.ResumeLayout(False)
        Me.ToolStripEncryptMain.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.CtxMenuNotifyIcon.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "1. Global Variables "
    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass
    ''' <summary>
    ''' selected <see cref=" ucCryptExp">ucCryptExp</see>
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents ucCryptExp As UCCryptExplorer

#End Region
    ''' <summary>
    ''' this method is called by new constructor to initialize needed components
    ''' </summary>
    Private Sub initByNew()
        configureUserAppWorkingDirectory()
        ind.cte.AppVars.LoadUserSettings()
        Dim actualVersion As String = My.Application.Info.Version.ToString
        If ind.settings.SettingsLink.chkAppFirstVersion <> actualVersion Then
            ind.settings.SettingsLink.chkAppFirstInit = True
            ind.settings.SettingsLink.saveProperties()
            Debug.WriteLine("  ind.settings.SettingsLink.chkAppFirstInit = True")
        Else
            Debug.WriteLine("  ind.settings.SettingsLink.chkAppFirstInit = false")
        End If

        AddHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
        AddHandler ind.win.AppEventsClass.LibraryCloseEvent, AddressOf LibraryDeletedEventHandler
        AddHandler ind.p2p.IPCPipe.PipeMessage, AddressOf pipeMessageHandler
    End Sub


    Shared onecryptSettingsSavedOnMydocuments As Boolean = False
    Public Shared Sub configureUserAppWorkingDirectory()
        Try
            Dim appDataFolder As String = ind.cte.AppConst.getUserFolder
            Dim dbWorking As String = IO.Path.Combine(appDataFolder, "oneCryptDatabase.sdf")
            'important to set var datadirectory of connectionstring
            AppDomain.CurrentDomain.SetData("DataDirectory", dbWorking)

            If Not Directory.Exists(appDataFolder) Then
                'onecrypt config files are not saved yet to mydocuments
                Directory.CreateDirectory(appDataFolder)
                onecryptSettingsSavedOnMydocuments = False
            End If

            'check if exist settings
            Dim settingsFile As String = "onecrypt.settings"
            Dim settingsFileNew As String = IO.Path.Combine(appDataFolder, settingsFile)
            If IO.File.Exists(settingsFileNew) = False Then
                Dim settingsFileOld As String = Path.Combine(My.Application.Info.DirectoryPath, settingsFile)
                If IO.File.Exists(settingsFileOld) = True Then
                    'copy to new location
                    IO.File.Copy(settingsFileOld, settingsFileNew)
                    'todo: borrar archivos antiguos cuando se pueda.
                End If
            Else

            End If

            'Directory.CreateDirectory(Path.Combine(Application.LocalUserAppDataPath, "onecrypt"))
            'Directory.CreateDirectory(Path.Combine(Application.UserAppDataPath, "onecrypt"))

            'check if database exists
            If IO.File.Exists(dbWorking) = False Then
                'copy databasefile
                Dim dbBaseClean As String = Path.Combine(My.Application.Info.DirectoryPath, "oneCryptDatabase.sdf")
                IO.File.Copy(dbBaseClean, dbWorking)
            End If
            Debug.WriteLine("FileDatabasePath: " & Global.My.MySettings.Default.oneCryptDatabaseConnectionString)
            ind.settings.SettingsLink.OCFileDatabasePath = dbWorking
            ind.settings.SettingsLink.saveProperties()

            'passwordmanager
            Dim pwdFolder As String = ind.settings.SettingsLink.PwdDatabaseFilePath
            If pwdFolder = "" Then
                'it means that is an old version and we need to copy to new location
                Dim fileSource As String = IO.Path.Combine(My.Application.Info.DirectoryPath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                If IO.File.Exists(fileSource) = True Then
                    'move to user folder
                    Dim fileTarget As String = IO.Path.Combine(appDataFolder, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                    IO.File.Copy(fileSource, fileTarget)
                    'todo: borrar archivos antiguos cuando se pueda.
                End If
                ind.settings.SettingsLink.PwdDatabaseFilePath = appDataFolder
                ind.settings.SettingsLink.saveProperties()
            End If


        Catch ex As Exception
            ind.log.Logging.logErr("Config error with database. " & ex.Message)
            Throw ex
        End Try

    End Sub



    ''' <summary>
    ''' load main application form. The main entry is in 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Debug.WriteLine(My.Application.Log.DefaultFileLogWriter.FullLogFileName)
            ind.log.Logging.logInfo("OneCrypt is starting.")
            ind.crypt.OCGlobal.mainForm = Me

            Me.myAppEvents_LanguageChangedEvent()
            Me.configureAutoSaveSettings()

            Me.pnlMain.Visible = False
            Me.pnlTopTitle.Visible = False
            Me.ToolStripEncryptMain.Visible = False

            Me.mniCheckOpenAfterDecrypt.Checked = ind.settings.SettingsLink.chkOpenAfterDecrypt
            Me.configureAutomaticDecryptOpen()

            'password usercontrol is showed now. All other options are not used until passwordIScorrect is called
            'password
            Dim ucPwdController1 As New UCPwdController
            ucPwdController1.Parent = Me
            'ucPwdController1.Location = New Point(0, 0)
            ucPwdController1.Dock = DockStyle.Fill
            ucPwdController1.Visible = True
            ucPwdController1.validatePwdConfiguration() 'configure control data
            ucPwdController1.senderController = Me


            ind.crypt.OCGlobal.ocExplorer = Me.UcExplorer1
            'add references
            'Me.ucCryptExp = Me.UcExplorer1.UcCryptExplorer1
            'Me.addRecentsProjectsToMenu()
            Me.configureMessageControl()
            If My.Computer.Name = "INDRON-1" Then
                Me.mniDDBBQuery.Visible = True
            Else
                Me.mniDDBBQuery.Visible = False
            End If

            frms.autoupdate.AutoUpdate.checkUpdate(Me)

            ind.p2p.IPCPipe.StartPipeServer()
            installKeyBoardHook() 'in partial class frmMainKeyBoard
            'initP2PReceiver() 'in partial class frmMainP2Mgr


            Me.loadInitialConfig() 'create recentslibrary if not exist


            If ind.settings.SettingsLink.chkAppFirstInit = True Then
                showConfigureShellExtensionDialog = True
            End If


            ind.settings.SettingsLink.chkAppFirstInit = False
            ind.settings.SettingsLink.chkAppFirstVersion = My.Application.Info.Version.ToString
            ind.settings.SettingsLink.saveProperties()

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' handles password is correct
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub passwordIScorrect(sender As Object, isCorrect As Boolean) Implements UCPwdController.IPwdSender.passwordIsCorrect
        If isCorrect = False Then
            Exit Sub
        End If
        Dim pwdController As UCPwdController = CType(sender, UCPwdController)
        pwdController.Visible = False
        Me.pnlMain.Visible = True
        Me.pnlTopTitle.Visible = True
        Me.ToolStripEncryptMain.Visible = True
        Me.NotifyIcon1.Visible = True
        'Me.Height = 514

        Me.UcExplorer1.SuspendLayout()
        Me.UcExplorer1.Dock = DockStyle.Fill
        Me.UcExplorer1.Location = New Point(0, 0)
        Me.UcExplorer1.ResumeLayout()
        Me.UcExplorer1.Visible = True
        Application.DoEvents()

        Me.UcExplorer1.loadRecentLibrariesTab()
        Dim notShow As Boolean = False 'true to show
        Me.UcExplorer1.loadLibraryRecentFilesTAB(notShow)


        'Me.loadLastLibraryOrDefault()
        Try
            If My.Application.CommandLineArgs.Count > 0 Then
                Me.Show()
                Dim ac As New ind.crypt.ArgumentController
                ac.ArgumentSwitch(My.Application.CommandLineArgs)
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

        configureShellExtension()

    End Sub


    ''' <summary>
    ''' used to indicate that we need to configure shellextention af
    ''' </summary>
    Private showConfigureShellExtensionDialog As Boolean = False
    ''' <summary>
    ''' integrate app in shell menu
    ''' </summary>
    Public Sub configureShellExtension()
        If showConfigureShellExtensionDialog = True Then
            Try
                'Dim msg As String = "Desea instalar los accesos directos en el menu de windows explorer? Con esta opción podrá encriptar/desencriptar desde el menu contextual de windows explorer, sin tener que abrir la aplicación."
                'Dim formDlg As frmDlg = ind.win.Msg.mQuestionDlg(msg, "Instalar accesos en windows explorer", msgIcon:=ind.win.Msg.MessageIconEnum.Information, defaultButton:=frmDlg.FocusButtonEnum.OkBtn)
                'Dim dlgRes As DialogResult = formDlg.ShowDialog
                'If dlgRes <> Windows.Forms.DialogResult.OK Then
                '    Exit Sub
                'Else
                frmOptions.AsociateApplicationExtensions(True)
                    ind.settings.SettingsLink.chkIntegrateInWindowsMenu = True
                    ind.settings.SettingsLink.saveProperties()
                'End If

            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try
        End If

        showConfigureShellExtensionDialog = False
    End Sub


    Private Sub pipeMessageHandler(message As String)
        Try

            Dim res As String() = message.Split(CType("|", Char()))
            If res.Length > 0 Then
                Dim action As String = res(0)
                If res.Length > 1 Then
                    Dim file As String = res(1)
                    'look in global cache
                    Dim cf As ind.crypt.FileObject = ind.crypt.OCGlobal.FileObjCollection.Item(file)
                    If Not cf Is Nothing Then
                        Debug.WriteLine("File found: " & message)
                        If action = "E" Then
                            Debug.WriteLine("Action encrypt " & file)
                            If cf.state <> ind.crypt.CryptoBase.CryptoStateEnum.IsEncripted Then
                                cf.state = ind.crypt.CryptoBase.CryptoStateEnum.IsEncripted
                                cf.UpdateByFileInfo(New IO.FileInfo(file & "." & ind.cte.AppConst.fileCryptExtension))
                                ind.crypt.LibraryDataHelper.UpdateFileByFullPath(cf, file)
                                cf.RaisefileStatusEvent()
                            End If

                        ElseIf action = "D" Then
                            Debug.WriteLine("Action decrypt " & file)
                            If cf.state <> ind.crypt.CryptoBase.CryptoStateEnum.IsDecrypted Then
                                cf.state = ind.crypt.CryptoBase.CryptoStateEnum.IsDecrypted
                                cf.UpdateByFileInfo(New IO.FileInfo(cf.FullPathWithoutCryptExtension))
                                ind.crypt.LibraryDataHelper.UpdateFileByFullPath(cf, file)
                                cf.RaisefileStatusEvent()
                            End If
                        End If
                    Else
                        'todo: add to recent files? put check in context options
                        Debug.WriteLine("File NOT found: " & message)
                    End If
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try
    End Sub



    Dim ucMessageCtrol As UCMessage
    ''' <summary>
    ''' configure the message control located in bottom/right corner of the application
    ''' </summary>
    Private Sub configureMessageControl()
        If Me.ucMessageCtrol Is Nothing Then
            ucMessageCtrol = New UCMessage
            ucMessageCtrol.Visible = False
            ucMessageCtrol.Parent = Me 'formmain
            'ucMessageCtrol.BringToFront()

            ucMessageCtrol.Left = Me.ClientSize.Width - ucMessageCtrol.Width
            ucMessageCtrol.Top = Me.ClientSize.Height - ucMessageCtrol.Height
            ucMessageCtrol.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        End If
    End Sub


#Region "   Recent projects"

    ''' <summary>
    ''' Add a recent library to the menu list
    ''' </summary>
    ''' <param name="prjnamepath"></param>
    ''' <remarks></remarks>
    'Public Sub addLibraryRecent(libraryToLoad As LibraryDataSet.T_LibraryRow) Handles myWinAppEvents.LibraryLoadedEvent

    'If libraryToLoad Is Nothing Then Exit Sub

    'Dim libName As String = libraryToLoad.Name  ' IO.Path.GetFileNameWithoutExtension(libraryToLoad)

    ''check if existObj
    'Dim mniExist As ToolStripMenuItem = Nothing

    'For Each mni As ToolStripMenuItem In Me.mniProjectRecents.DropDownItems
    '    If mni.Text = libName Then
    '        mniExist = mni
    '        Exit For
    '    End If
    'Next

    'If Not mniExist Is Nothing Then
    '    Me.mniProjectRecents.DropDownItems.Remove(mniExist)
    '    Me.mniProjectRecents.DropDownItems.Insert(1, mniExist)
    '    Exit Sub
    'End If

    'Dim maxCapacity As Integer = 10
    'If Me.mniProjectRecents.DropDownItems.Count = maxCapacity Then 'include basic project
    '    'delete first
    '    Dim mniToDelete As System.Windows.Forms.ToolStripMenuItem = CType(Me.mniProjectRecents.DropDownItems(maxCapacity - 1), ToolStripMenuItem)
    '    RemoveHandler mniToDelete.Click, AddressOf Me.mniRecent_Click
    '    Me.mniProjectRecents.DropDownItems.RemoveAt(maxCapacity - 1)
    'End If

    'Dim mniRecent As New System.Windows.Forms.ToolStripMenuItem()

    'mniRecent.Text = libName
    'mniRecent.Tag = libraryToLoad
    'Me.mniProjectRecents.DropDownItems.Insert(1, mniRecent)
    'AddHandler mniRecent.Click, AddressOf Me.mniRecent_Click

    'End Sub
#End Region

#Region "   Configure autosavesettings"
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    Private Sub configureAutoSaveSettings()
        If Not addedHandler Then
            SyncLock addedHandlerLockObject
                If Not addedHandler Then
                    AddHandler My.Application.Shutdown, AddressOf ind.settings.onecryptSettings.AutoSaveSettings
                    addedHandler = True
                End If
            End SyncLock
        End If
    End Sub
#End Region
    ''' <summary>
    ''' configure if automatic open is enabled after every decryption
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configureAutomaticDecryptOpen()

        If ind.settings.SettingsLink.chkOpenAfterDecrypt = True Then
            'Me.tssStatusAutomaticOpen.Image = My.Resources.file_DecryptAndOpen_16
            Me.mniCheckOpenAfterDecrypt.Image = My.Resources.file_DecryptAndOpen_16
            Me.mniCheckOpenAfterDecrypt.Text = ind.lang.UILang.frmMain_mniCheckOpenAfterDecrypt_ON
        Else
            'Me.tssStatusAutomaticOpen.Image = My.Resources.file_DecryptAndOpenDisabled_16
            Me.mniCheckOpenAfterDecrypt.Image = My.Resources.file_DecryptAndOpenDisabled_16
            Me.mniCheckOpenAfterDecrypt.Text = ind.lang.UILang.frmMain_mniCheckOpenAfterDecrypt_OFF
        End If
    End Sub

#Region "load necesary configuration "
    Private Sub loadInitialConfig()

        If Me.DesignMode = False Then
            Me.loadLibraryRecentFilesInfo()
        End If

    End Sub
    ''' <summary>
    ''' load recents files info for library, code and name and store in <see cref="ind.crypt.OCGlobal.libraryRecentFilesID "></see>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadLibraryRecentFilesInfo()

        Dim LibraryRecents_LibraryRow As LibraryDataSet.T_LibraryRow

        Dim da As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
        Dim dt As LibraryDataSet.T_LibraryDataTable = da.GetDtLibraryRecentFiles
        If dt.Rows.Count > 0 Then
            LibraryRecents_LibraryRow = CType(dt.Rows(0), LibraryDataSet.T_LibraryRow)
        Else
            'library recents not exist so we create it

            da.Insert("LibraryRecents", 0, Date.Now, Date.Now)
            dt = da.GetDtLibraryByLibraryName("LibraryRecents")
            LibraryRecents_LibraryRow = CType(dt.Rows(0), LibraryDataSet.T_LibraryRow)

            Dim dacfg As New LibraryDataSetTableAdapters.T_ConfigTableAdapter
            dacfg.InsertRecentLibrary(LibraryRecents_LibraryRow.id)
            da.Dispose()
            'dt = da.GetDtLibraryRecentFiles
        End If


        ind.crypt.OCGlobal.libraryRecentFilesGlob = LibraryRecents_LibraryRow
        LibraryRecents_LibraryRow.Name = ind.lang.UILang.LibraryRecentsFiles 'change the name based on language

    End Sub

#End Region


#Region "   Handlers externos tree, ucrypt, etc"
    Private Sub ucCryptExp_lvItemSelectedEvent(lviListSel As ListView.SelectedListViewItemCollection) Handles ucCryptExp.lvItemSelectedEvent
        Dim lvCol As ListView.SelectedListViewItemCollection = lviListSel
        Dim count As Integer = lvCol.Count

        If count = 1 Then
            Dim lviEx As ind.win.ListViewItemEx = DirectCast(lvCol(0), ind.win.ListViewItemEx)
            Dim coFile As ind.crypt.FileObject = lviEx.coFile

            If coFile.FileNotFound = True Then
                'hide buttons
                Me.tsbFileEncrypt.Enabled = False
                Me.tsbFileDecrypt.Enabled = False
                Me.tsbFileDecryptAndOpen.Enabled = False
                Me.tsbCompressFiles.Enabled = False
            Else
                'common buttons
                Me.tsbCompressFiles.Enabled = True
                'is encrypted?
                If coFile.state = ind.crypt.Crypto.CryptoStateEnum.IsEncripted Then
                    Me.tsbFileDecrypt.Enabled = True
                    Me.tsbFileEncrypt.Enabled = False
                    Me.tsbFileDecryptAndOpen.Enabled = True

                ElseIf coFile.state = ind.crypt.Crypto.CryptoStateEnum.IsDecrypted Then
                    Me.tsbFileEncrypt.Enabled = True
                    Me.tsbFileDecrypt.Enabled = False
                    Me.tsbFileDecryptAndOpen.Enabled = False

                End If

                Me.tsbCompressFiles.Enabled = True
            End If

        ElseIf count > 1 Then
            Me.tsbFileEncrypt.Enabled = True
            Me.tsbFileDecrypt.Enabled = True
            Me.tsbFileDecryptAndOpen.Enabled = True
            Me.tsbCompressFiles.Enabled = True
        Else
            Me.tsbFileEncrypt.Enabled = False
            Me.tsbFileDecrypt.Enabled = False
            Me.tsbFileDecryptAndOpen.Enabled = False
            Me.tsbCompressFiles.Enabled = False
        End If
    End Sub



#End Region

#Region "   Load library Methods\Events"
    ''' <summary>
    ''' Control when library is deleted to change buttons behavior
    ''' </summary>
    ''' <param name="libraryIdKey"></param>
    Public Sub LibraryDeletedEventHandler(libraryIdKey As Integer)
        If Not Me.ucCryptExp Is Nothing Then
            If Me.ucCryptExp.LibraryLoaded.libraryID = libraryIdKey Then
                Me.ucCryptExp = Nothing
                Me.mniProjectSave.Enabled = False
                Me.mniProjectSaveAs.Enabled = False
                Me.btnSaveSelectedItemsAs.Enabled = False
                Me.tsbFileEncrypt.Enabled = False
                Me.tsbFileDecrypt.Enabled = False
                Me.tsbFileDecryptAndOpen.Enabled = False
                Me.tsbCompressFiles.Enabled = False
                Me.btnEncryptOpenLibraries.Enabled = False
            End If
        End If
    End Sub

    Private Sub UcExplorer1_UCLibrarySelectedEvent(ucCryptExplorerSelected As IndronTabPage) Handles UcExplorer1.UCLibrarySelectedEvent
        'important that this must related with selected tab to activate menu commands in principal window

        'only for tablibraries
        Dim UCLibraryExp As UCCryptExplorer = UcLibraryTabpage.IsUCLibraryTabpage(ucCryptExplorerSelected)
        If UCLibraryExp Is Nothing Then
            'it is not a library, disable buttons
            Me.mniProjectSave.Enabled = False
            Me.mniProjectSaveAs.Enabled = False
            Me.btnSaveSelectedItemsAs.Enabled = False
            Me.btnEncryptOpenLibraries.Enabled = False
        Else
            Me.mniProjectSave.Enabled = True
            Me.mniProjectSaveAs.Enabled = True
            Me.btnSaveSelectedItemsAs.Enabled = True
            Me.btnEncryptOpenLibraries.Enabled = True

            Me.ucCryptExp = UCLibraryExp
            'If Me.ucCryptExp.LibraryLoaded.libraryID <> UCLibraryExp.LibraryLoaded.libraryID Then

            'Else
            '    Exit Sub
            'End If
        End If
    End Sub


#End Region

    Private CloseApplicationInProcess As Boolean = False

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            ind.settings.SettingsLink.saveProperties()
            If ind.cte.AppVars.UserValidated = False Then
                Exit Sub
            End If

            Select Case e.CloseReason
                Case CloseReason.WindowsShutDown
                    Debug.WriteLine("WindowsShutDown")
                    ind.log.Logging.logInfo("OneCrypt is closed WindowsShutDown ")
                    Dim cancel As Boolean = Me.UcExplorer1.getTabsWithChanges
                    If cancel = True Then
                        e.Cancel = True
                        Me.CloseApplicationInProcess = False
                    End If

                Case CloseReason.ApplicationExitCall
                    Debug.WriteLine("ApplicationExitCall")
                    ind.log.Logging.logInfo("OneCrypt is closed ApplicationExitCall ")
                    'Dim cancel As Boolean = Me.UcExplorer1.getTabsWithChanges
                    'If cancel = True Then
                    '    e.Cancel = True
                    '    Me.CloseApplicationInProcess = False
                    'End If
                Case CloseReason.UserClosing
                    Debug.WriteLine("UserClosing")
                    ind.log.Logging.logInfo("OneCrypt is closed UserClosing")
                    If Me.CloseApplicationInProcess = False Then
                        e.Cancel = True
                        Me.WindowState = FormWindowState.Minimized
                        Exit Sub
                    End If
                    Dim cancel As Boolean = Me.UcExplorer1.getTabsWithChanges
                    If cancel = True Then
                        e.Cancel = True
                        Me.CloseApplicationInProcess = False
                    End If
            End Select

            If Not e.Cancel Then
                uninstallHook()
            End If
            ind.log.Logging.logInfo("OneCrypt is closed.")
        Catch ex As Exception
            ind.log.Logging.logExcep(ex)
        End Try
    End Sub



    Friend Sub CloseApplication()
        Try
            Dim cancel As Boolean = Me.UcExplorer1.getTabsWithChanges
            If cancel = True Then
                Me.CloseApplicationInProcess = False
                Exit Sub
            End If
            Application.Exit()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally

            Cursor = Cursors.Default
        End Try

    End Sub


#Region "Notify icon"
    ''' <summary>
    ''' restore app when double click in icon in notify icon. 
    ''' This is the main icon showed in windows notify area
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Debug.WriteLine("Notify icon Restore app by double click")

        Try
            If e.Button = MouseButtons.Left Then
                Me.RestoreApplication()
                Me.UcExplorer1.loadPasswordManagerTab()
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
            ind.log.Logging.logExcep(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub NotifyIcon1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.RestoreApplication()
        End If

    End Sub
    ''' <summary>
    ''' close app when click on btn in context menu of the notifyicon in windows notify area
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctxNotify_btnCloseApplication_Click(sender As Object, e As EventArgs) Handles ctxNotify_btnCloseApplication.Click, mniCloseApplication.Click
        Try
            Me.CloseApplicationInProcess = True
            Me.CloseApplication()
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' show app when click on context menu in notfyicon
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctxNotify_btnShowOneCrypt_Click(sender As Object, e As EventArgs) Handles ctxNotify_btnShowOneCrypt.Click
        Try
            Debug.WriteLine("Notify icon Restore app by one click")
            Me.RestoreApplication()
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Debug.WriteLine("Form1_Resize")
            Me.ShowInTaskbar = False
            If Me.Visible = True Then
                Debug.WriteLine("Form1_Resize>hide")
                Me.Hide()
            End If

        End If
    End Sub
    ''' <summary>
    ''' restore app from hide status when needed
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RestoreApplication()
        Try
            If Me.Visible = False Then
                Debug.WriteLine("RestoreApplication>show")
                Me.Show()
            End If

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.TopMost = True

            Me.ShowInTaskbar = True
            Me.BringToFront()
            Me.Focus()
            Me.TopMost = False
        Catch ex As Exception

        End Try


    End Sub

    Private Sub CtxMenuNotifyIcon_Opening(sender As Object, e As ComponentModel.CancelEventArgs) Handles CtxMenuNotifyIcon.Opening
        If Me.WindowState = FormWindowState.Minimized Then
            Me.ctxNotify_btnShowOneCrypt.Visible = True
        Else
            Me.ctxNotify_btnShowOneCrypt.Visible = False
        End If
    End Sub

#End Region





#Region "   Menu top"

    Private Sub mniProjectRecents_Click(sender As Object, e As EventArgs) Handles mniProjectRecents.Click
        Try
            Me.UcExplorer1.loadRecentLibrariesTab()
            'Me.UcExplorer1.UcLeftMenu1.showOption(UCExplorer.menuLeft.mnuLibraries)
        Catch ex As Exception
            ind.log.Logging.logExcep(ex)
            ind.win.Msg.mError(ex)

        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub mniProjectRecentsFiles_Click(sender As Object, e As EventArgs) Handles mniProjectRecentsFiles.Click
        Try
            Me.UcExplorer1.loadLibraryRecentFilesTAB()
        Catch ex As Exception
            ind.log.Logging.logExcep(ex)
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    'Private Sub mniRecent_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim mni As System.Windows.Forms.ToolStripMenuItem = CType(sender, ToolStripMenuItem)

    '        If CStr(mni.Tag) = "0" Then
    '            'todo: to open a tab with recent files
    '            'Me.LoadLibrary(ind.cte.AppConst.ECryptManagerFileName)
    '        Else
    '            Dim libR As LibraryDataSet.T_LibraryRow = CType(mni.Tag, LibraryDataSet.T_LibraryRow)
    '            Me.LoadLibrary(libR)
    '        End If

    '    Catch ex As Exception
    '        ind.win.Msg.mError(ex)
    '    End Try

    'End Sub

    Private Sub mniProjectSave_Click(sender As Object, e As EventArgs) Handles mniProjectSave.Click
        Try
            Cursor = Cursors.WaitCursor
            Me.ucCryptExp.LibrarySave()
            Me.ucMessageCtrol.setMessage(ind.lang.UILang.frmMain_mniProjectSave_Click)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mniProjectSaveAs_Click(sender As Object, e As EventArgs) Handles mniProjectSaveAs.Click
        Try
            Dim ucl As New UcNewLibrary
            Dim sd As DialogResult = ucl.createNewLibraryDialog
            If sd <> DialogResult.OK Then
                Debug.WriteLine("cancel")
                Exit Sub
            ElseIf sd = DialogResult.OK Then
                If Not ucl.drLibrary Is Nothing Then
                    ind.win.AppEventsClass.LibraryNewCreatedEventRaise(ucl.drLibrary)
                    Me.ucCryptExp.LibrarySaveAs(ucl.drLibrary.id) 'now copy all the files
                End If
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mniProjectSaveAll_Click(sender As Object, e As EventArgs) Handles mniProjectSaveAll.Click
        Try
            Cursor = Cursors.WaitCursor

            Me.UcExplorer1.SaveAllLibraries()
            Me.ucMessageCtrol.setMessage(ind.lang.UILang.SaveAllMessage)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnSaveSelectedItemsAs_Click(sender As Object, e As EventArgs) Handles btnSaveSelectedItemsAs.Click
        Try
            Dim ucl As New UcNewLibrary
            Dim sd As DialogResult = ucl.createNewLibraryDialog
            If sd <> DialogResult.OK Then
                Debug.WriteLine("cancel")
                Exit Sub
            ElseIf sd = DialogResult.OK Then
                If Not ucl.drLibrary Is Nothing Then
                    ind.win.AppEventsClass.LibraryNewCreatedEventRaise(ucl.drLibrary)

                    Me.ucCryptExp.LibrarySaveAs(ucl.drLibrary.id, True)
                End If
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnEncryptOpenLibraries_Click(sender As Object, e As EventArgs) Handles btnEncryptOpenLibraries.Click
        Try
            Cursor = Cursors.WaitCursor
            If Me.ucCryptExp Is Nothing Then
                Exit Sub
            End If
            Me.ucCryptExp.EncryptOrDecryptAll(ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
            Me.Refresh()
            Me.ucMessageCtrol.setMessage(ind.lang.UILang.msgOpenLibrariesEncrypted)
        Catch ex As Exception
            ind.win.Msg.mError(ex)

        Finally
            Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub mniProjectNew_Click(sender As Object, e As EventArgs) Handles mniProjectNew.Click

        Try
            Dim ucl As New UcNewLibrary
            Dim sd As DialogResult = ucl.createNewLibraryDialog
            If sd <> DialogResult.OK Then
                Debug.WriteLine("cancel")
                Exit Sub
            ElseIf sd = DialogResult.OK Then
                ind.win.AppEventsClass.LibraryNewCreatedEventRaise(ucl.drLibrary)
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub mniOpenOptionsWindow_Click(sender As Object, e As EventArgs) Handles mniOpenOptionsWindow.Click
        Try
            Dim f As New frmOptions
            f.ShowDialog()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub tsbAbout_Click(sender As Object, e As EventArgs) Handles mniAbout.Click
        Dim f As New AboutBox1
        f.ShowDialog()
    End Sub


    Private Sub mniChangePwd_Click(sender As Object, e As EventArgs) Handles mniChangePwd.Click
        Try
            UCPwdController.changePassword()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub mniCheckOpenAfterDecrypt_Click(sender As Object, e As EventArgs) Handles mniCheckOpenAfterDecrypt.Click
        ind.settings.SettingsLink.chkOpenAfterDecrypt = mniCheckOpenAfterDecrypt.Checked
        ind.settings.SettingsLink.saveProperties()
        Me.configureAutomaticDecryptOpen()
    End Sub

    Private Sub tssStatusAutomaticOpen_Click(sender As Object, e As EventArgs)
        mniCheckOpenAfterDecrypt.PerformClick()
    End Sub

    Private Sub mniHelp_Click(sender As Object, e As EventArgs) Handles mniHelp.Click
        MessageBox.Show("Documentation is available in the docs folder of the OneCrypt source repository.",
                        ind.cte.AppConst.AppName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
    End Sub


    Private Sub tsbFileEncrypt_Click(sender As Object, e As EventArgs) Handles tsbFileEncrypt.Click
        Try

            tsbFileDecrypt.Enabled = True
            tsbFileEncrypt.Enabled = False
            Me.ucCryptExp.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try


    End Sub


    Private Sub tsbFileDecrypt_Click(sender As Object, e As EventArgs) Handles tsbFileDecrypt.Click
        Try
            tsbFileDecrypt.Enabled = False
            tsbFileEncrypt.Enabled = True
            Me.ucCryptExp.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, UCCryptExplorer.DecryptOpenActionEnum.DecryptAndNoOpen)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' Decrypt and open selected files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbFileDecryptAndOpen_Click(sender As Object, e As EventArgs) Handles tsbFileDecryptAndOpen.Click
        Try
            Me.ucCryptExp.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, UCCryptExplorer.DecryptOpenActionEnum.DecryptAndOpen)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' compress selected files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsbCompressFiles_Click(sender As Object, e As EventArgs) Handles tsbCompressFiles.Click
        Try
            If Not Me.ucCryptExp Is Nothing Then
                Me.ucCryptExp.compressFiles()
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
#End Region

#Region "   Change language"
    Private Sub CastellanoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CastellanoToolStripMenuItem.Click
        changeLanguage("es-ES")

    End Sub
    Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click

        changeLanguage("en-US")
    End Sub

    Private Sub changeLanguage(langCulture As String)
        Try
            If ind.settings.SettingsLink.LanguageSelected = langCulture Then
                Exit Sub
            Else
                ind.settings.SettingsLink.LanguageSelected = langCulture
                ind.settings.SettingsLink.saveProperties()
                Dim dlgRes As DialogResult = ind.win.Msg.mQuestionDlg(ind.lang.UILang.LanguageChangeMessage, ind.lang.UILang.ChangeLanguageWindowsTitle, msgIcon:=ind.win.Msg.MessageIconEnum.Information).ShowDialog
                If dlgRes <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                Else
                    Me.CloseApplication()
                End If
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
#If DEBUG Then
        Me.Text = ind.cte.AppConst.AppName '& " " & Globalization.CultureInfo.CurrentUICulture.Name
#Else
        Me.Text = ind.cte.AppConst.AppName
#End If

        Me.tsbOptions.ToolTipText = ind.lang.UILang.frmMain_tsbOptions_TT
        Me.mniCheckOpenAfterDecrypt.Text = ind.lang.UILang.frmMain_mniCheckOpenAfterDecrypt
        Me.mniCheckOpenAfterDecrypt.ToolTipText = ind.lang.UILang.frmMain_mniCheckOpenAfterDecrypt_TT
        Me.mniChangePwd.Text = Global.ind.lang.UILang.frmMain_mniChangePwd
        Me.mniHelp.Text = Global.ind.lang.UILang.frmMain_mniHelp
        Me.mniHelp.ToolTipText = ind.lang.UILang.frmMain_mniHelp_tt
        Me.mniAbout.Text = Global.ind.lang.UILang.frmMain_mniAbout
        Me.tsbProjectsMain.Text = Global.ind.lang.UILang.frmMain_tsbProjectsMain
        Me.tsbProjectsMain.ToolTipText = Global.ind.lang.UILang.frmMain_tsbProjectsMain_TT
        Me.mniProjectSave.Text = Global.ind.lang.UILang.frmMain_mniProjectSave
        Me.mniProjectSaveAll.Text = Global.ind.lang.UILang.frmMain_mniProjectSaveAll
        Me.mniProjectSaveAs.Text = Global.ind.lang.UILang.frmMain_mniProjectSaveAs
        Me.btnSaveSelectedItemsAs.Text = Global.ind.lang.UILang.frmMain_btnSaveSelectedItemsAs
        Me.btnSaveSelectedItemsAs.ToolTipText = Global.ind.lang.UILang.frmMain_btnSaveSelectedItemsAs_TT
        Me.mniProjectNew.Text = Global.ind.lang.UILang.frmMain_mniProjectNew
        Me.mniProjectRecents.Text = Global.ind.lang.UILang.frmMain_mniProjectRecents
        Me.mniProjectRecentsFiles.Text = Global.ind.lang.UILang.frmMain_mniProjectRecentsFiles
        Me.tsbFolderAdd.Text = Global.ind.lang.UILang.frmMain_tsbFolderAdd
        Me.tsbFileEncrypt.Text = Global.ind.lang.UILang.frmMain_tsbFileEncrypt
        Me.tsbFileEncrypt.ToolTipText = Global.ind.lang.UILang.frmMain_tsbFileEncrypt_TT
        Me.tsbFileDecrypt.Text = Global.ind.lang.UILang.frmMain_tsbFileDecrypt
        Me.tsbFileDecrypt.ToolTipText = Global.ind.lang.UILang.frmMain_tsbFileDecrypt_TT
        Me.tsbFileDecryptAndOpen.Text = Global.ind.lang.UILang.frmMain_tsbFileDecryptAndOpen
        Me.tsbFileDecryptAndOpen.ToolTipText = Global.ind.lang.UILang.frmMain_tsbFileDecryptAndOpen_TT
        Me.mniOpenOptionsWindow.Text = Global.ind.lang.UILang.frmMain_mniOpenOptionsWindow
        Me.tsbchangeLanguage.Text = Global.ind.lang.UILang.frmMain_tsbchangeLanguage
        Me.tsbCompressFiles.Text = ind.lang.UILang.frmmain_tsbCompressFiles
        Me.tsbCompressFiles.ToolTipText = ind.lang.UILang.frmmain_tsbCompressFiles_TT
        Me.btnPwdManagerOpen.ToolTipText = ind.lang.UCpwdMngLang.btnPwdManagerOpen
        Me.btnTextCrypt.ToolTipText = ind.lang.UILang.btnTextCrypt_tt
        Me.btnEncryptOpenLibraries.Text = ind.lang.UILang.btnEncryptOpenLibraries
        Me.btnEncryptOpenLibraries.ToolTipText = ind.lang.UILang.btnEncryptOpenLibraries_TT
        Me.mniCloseApplication.Text = ind.lang.UILang.ctxNotify_btnCloseApplication
        Me.btnCheckNewVersion.Text = ind.lang.UILang.frmmain_btnCheckNewVersion

        'notifyicon
        Me.ctxNotify_btnCloseApplication.Text = ind.lang.UILang.ctxNotify_btnCloseApplication
        Me.ctxNotify_btnEncryptText.Text = ind.lang.UILang.ctxNotify_btnEncryptText
        Me.ctxNotify_btnEncryptText.ToolTipText = ind.lang.UILang.btnTextCrypt_tt
        Me.ctxNotify_btnEncryptText.Image = My.Resources.document_Encrypt_32
        Me.ctxNotify_btnOpenPwdManager.Text = ind.lang.UILang.ctxNotify_btnOpenPwdManager
        Me.ctxNotify_btnOpenPwdManager.Image = My.Resources.pwdManager_ShowPwd
        Me.ctxNotify_btnShowOneCrypt.Text = ind.lang.UILang.ctxNotify_btnShowOneCrypt

    End Sub

#End Region

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        For x = 0 To 1000
            'Test Crypt.pdf
            IO.File.Copy("C:\OneCrypt\Masive encription\Test Crypt.pdf", "C:\OneCrypt\Masive encription\Test Crypt" & x & ".pdf")
        Next
    End Sub


    Private Sub mniDDBBQuery_Click(sender As Object, e As EventArgs) Handles mniDDBBQuery.Click
        Dim f As New FrmDDBBInspector
        f.Show()
    End Sub

    Private Sub mniEncryptDecryptText_Click(sender As Object, e As EventArgs) Handles btnTextCrypt.Click, ctxNotify_btnEncryptText.Click
        Dim f As New FrmEncryptText
        f.Show()
    End Sub

    Private Sub btnPwdManagerOpen_Click(sender As Object, e As EventArgs) Handles btnPwdManagerOpen.Click
        Try
            Me.UcExplorer1.loadPasswordManagerTab()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
            ind.log.Logging.logExcep(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ctxNotify_btnOpenPwdManager_Click(sender As Object, e As EventArgs) Handles ctxNotify_btnOpenPwdManager.Click
        Try
            Me.RestoreApplication()
            Application.DoEvents()
            Me.UcExplorer1.loadPasswordManagerTab()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
            ind.log.Logging.logExcep(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' button to lock screen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tsbLockScreen_Click(sender As Object, e As EventArgs) Handles tsbLockScreen.Click, ctxNotify_btnLockComputer.Click
        Try
            'todo: change native module to shared class to use the methods inside
            LockWorkStation() 'methdo located on module.
            Exit Sub
            Dim myObj As abcLockScreen = New abcLockScreen
            myObj.LockSystemAndShow(FrmLockScreenPwd) 'tell the form to use to lock

        Catch ex As Exception
            ind.win.Msg.mError(ex)
            ind.log.Logging.logExcep(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub UcExplorer1_Load(sender As Object, e As EventArgs) Handles UcExplorer1.Load

    End Sub

    Private Sub tsbProjectsMain_Click(sender As Object, e As EventArgs) Handles tsbProjectsMain.Click

    End Sub

    Private Sub tsbProjectsMain_DropDownOpening(sender As Object, e As EventArgs) Handles tsbProjectsMain.DropDownOpening
        If Not Me.ucCryptExp Is Nothing Then
            Dim lvColSelected As ListView.SelectedListViewItemCollection = Me.ucCryptExp.lvTreeExp.SelectedItems
            If lvColSelected.Count = 0 Then
                Me.btnSaveSelectedItemsAs.Enabled = False
                Exit Sub
            Else
                Me.btnSaveSelectedItemsAs.Enabled = True
            End If
        End If
    End Sub

    Private availableUpdate As frms.autoupdate.UpdateReleaseInfo

    Public Sub showUpdateButton(update As frms.autoupdate.UpdateReleaseInfo)
        If update Is Nothing Then
            Throw New ArgumentNullException(NameOf(update))
        End If

        availableUpdate = update
        Dim btnUpdateApp As New ToolStripButton With {
            .DisplayStyle = ToolStripItemDisplayStyle.Image,
            .Image = Global.My.Resources.Resources.update,
            .ImageTransparentColor = Color.Magenta,
            .Name = "btnUpdateApp",
            .Text = "OneCrypt " & update.Version.ToString() & " is available"
        }
        AddHandler btnUpdateApp.Click, AddressOf btnUpdateApp_Click
        Me.ToolStripOptions.Items.Clear()
        Me.ToolStripOptions.Items.AddRange(New ToolStripItem() {btnUpdateApp, Me.tsbOptions})
    End Sub

    Private Sub btnUpdateApp_Click(sender As Object, e As EventArgs)
        If availableUpdate Is Nothing Then
            Return
        End If
        Process.Start(New ProcessStartInfo(availableUpdate.DownloadUri.AbsoluteUri) With {.UseShellExecute = True})
    End Sub

    Private Sub btnCheckNewVersion_Click(sender As Object, e As EventArgs) Handles btnCheckNewVersion.Click
        Try
            frms.autoupdate.AutoUpdate.showUpdateForm()

        Catch ex As Exception
            ind.win.Msg.mError(ex)
            ind.log.Logging.logExcep(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub




End Class
