
Imports System.IO
Imports sharpshell.tools
''' <summary>
''' Form to configurate some options for the application
''' </summary>
''' <remarks></remarks>
Public Class frmOptions

    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass

    Dim loadingSettings As Boolean = False

    Private Sub frmOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub



    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.myAppEvents_LanguageChangedEvent()
        loadingSettings = True
        Me.chkOpenAfterDecrypt.Checked = ind.settings.SettingsLink.chkOpenAfterDecrypt
        Me.chkNotDecryptWithoutExtension.Checked = ind.settings.SettingsLink.chkNotDecryptWithoutExtension
        Me.chkIntegrateInWindowsMenu.Checked = ind.settings.SettingsLink.chkIntegrateInWindowsMenu
        Me.chkStartWithWindows.Checked = ind.settings.SettingsLink.chkStartWithWindows
        Me.chkOpenImgInApp.Checked = ind.settings.SettingsLink.chkOpenImgInApp
        Me.chkOpenAppWithCtrolKey.Checked = ind.settings.SettingsLink.chkOpenAppWithCtrolKey
        loadingSettings = False
        'Me.LoadDatabasePath()
    End Sub



    'Public Shared Sub LoadDatabasePath()
    '    Dim dbPath As String = Global.My.MySettings.Default.oneCryptDatabaseConnectionString

    '    If ind.cte.AppVars.FileDatabasePath <> "" Then
    '        'load to global wich is used by datasets
    '        Global.My.MySettings.Default.oneCryptDatabaseConnectionString = ind.cte.AppVars.FileDatabasePath
    '    Else
    '        'not existObj: check by default

    '    End If


    '    If ind.cte.AppVars.FileDatabasePath = "" Then
    '        ind.cte.AppVars.FileDatabasePath = dbPath
    '    Else
    '        Global.My.MySettings.Default.oneCryptDatabaseConnectionString = ind.cte.AppVars.FileDatabasePath
    '    End If
    '    'Me.txtFileDatabasePath.Text = ind.cte.AppVars.FileDatabasePath
    '    ind.settings.SettingsLink.saveProperties()
    'End Sub


    Private Sub chkOpenAfterDecrypt_MouseHover(sender As Object, e As EventArgs) Handles chkOpenAfterDecrypt.MouseHover, chkNotDecryptWithoutExtension.MouseHover _
                                                        , chkIntegrateInWindowsMenu.MouseHover, chkStartWithWindows.MouseHover, chkOpenImgInApp.MouseHover, chkOpenAppWithCtrolKey.MouseHover
        Try
            Dim ctrl As Control = CType(sender, Control)

            If Not ctrl.Tag Is Nothing Then
                Me.lblInfoOptions.Text = CType(ctrl.Tag, String)
                Me.lblInfoOptions.Visible = True
                Me.imgInfoOptions.Visible = True
            Else
                Me.lblInfoOptions.Visible = False
                Me.imgInfoOptions.Visible = False
            End If

        Catch ex As Exception
            Me.lblInfoOptions.Visible = False
            Me.imgInfoOptions.Visible = False
        End Try
    End Sub


    Private Sub chkOpenAfterDecrypt_MouseLeave(sender As Object, e As EventArgs) Handles chkOpenAfterDecrypt.MouseLeave, chkNotDecryptWithoutExtension.MouseLeave _
                                                        , chkIntegrateInWindowsMenu.MouseLeave, chkStartWithWindows.MouseLeave, chkOpenImgInApp.MouseLeave, chkOpenAppWithCtrolKey.MouseLeave
        Me.lblInfoOptions.Visible = False
        Me.imgInfoOptions.Visible = False
    End Sub


    Private Sub chkIntegrateInWindowsMenu_CheckedChanged(sender As Object, e As EventArgs) Handles chkIntegrateInWindowsMenu.CheckedChanged
        If loadingSettings = True Then Exit Sub
        Dim previousCheckState As Boolean = Not Me.chkIntegrateInWindowsMenu.Checked
        Try
            AsociateApplicationExtensions(Me.chkIntegrateInWindowsMenu.Checked)
            ind.settings.SettingsLink.chkIntegrateInWindowsMenu = Me.chkIntegrateInWindowsMenu.Checked
            ind.settings.SettingsLink.saveProperties()
        Catch exFile As IO.FileNotFoundException
            loadingSettings = True
            Me.chkIntegrateInWindowsMenu.Checked = previousCheckState
            loadingSettings = False
            Dim msg As String = "Application needed to registry not found: " & exFile.FileName & vbCr & exFile.Message
            ind.win.Msg.mError(msg)

        Catch ex As Exception
            loadingSettings = True
            Me.chkIntegrateInWindowsMenu.Checked = previousCheckState
            loadingSettings = False
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub chkOpenAfterDecrypt_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenAfterDecrypt.CheckedChanged
        If loadingSettings = True Then Exit Sub
        ind.settings.SettingsLink.chkOpenAfterDecrypt = Me.chkOpenAfterDecrypt.Checked
        ind.settings.SettingsLink.saveProperties()
    End Sub

    Private Sub chkNotDecryptWithoutExtension_CheckedChanged(sender As Object, e As EventArgs) Handles chkNotDecryptWithoutExtension.CheckedChanged
        If loadingSettings = True Then Exit Sub
        ind.settings.SettingsLink.chkNotDecryptWithoutExtension = Me.chkNotDecryptWithoutExtension.Checked
        ind.settings.SettingsLink.saveProperties()
    End Sub


    Public Shared Sub AsociateApplicationExtensions(install As Boolean)

        ''  Get the architecture.
        'Dim registrationType As sharpshell.ServerRegistration.RegistrationType
        'If Environment.Is64BitOperatingSystem = True Then
        '    registrationType = sharpshell.ServerRegistration.RegistrationType.OS64Bit
        'Else
        '    registrationType = sharpshell.ServerRegistration.RegistrationType.OS32Bit
        'End If

        'If install = True Then
        '    'contextmenu
        '    Dim contextmenupath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "OneCryptCtxExplorer.dll")
        '    sharpshell.tools.ServerManagerApi.installServer(contextmenupath, registrationType)

        '    Dim iconhandlerpath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "OneCryptIconHandler.dll")
        '    sharpshell.tools.ServerManagerApi.installServer(iconhandlerpath, registrationType)
        'Else
        '    Dim contextmenupath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "OneCryptCtxExplorer.dll")
        '    sharpshell.tools.ServerManagerApi.uninstallServer(contextmenupath, registrationType)

        '    Dim iconhandlerpath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "OneCryptIconHandler.dll")
        '    sharpshell.tools.ServerManagerApi.uninstallServer(iconhandlerpath, registrationType)
        'End If


        Dim filename As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "OneCryptCtxInstaller.exe")
        If IO.File.Exists(filename) = False Then
            Throw New Exception("File to make associations not exist: " & filename)
        End If

        If install = True Then
            Dim pHelp As New ProcessStartInfo

            pHelp.FileName = filename
            'pHelp.Arguments = "install OneCryptCtxExplorer.dll -codebase "
            pHelp.Verb = "runas"
            pHelp.Arguments = "install " & ind.settings.SettingsLink.LanguageSelected
            pHelp.UseShellExecute = True
            'pHelp.WindowStyle = ProcessWindowStyle.Hidden

            Dim proc As Process = Process.Start(pHelp)
        Else
            Dim pHelp As New ProcessStartInfo
            pHelp.FileName = filename
            pHelp.Verb = "runas"
            'pHelp.Arguments = "uninstall OneCryptCtxExplorer.dll  "
            pHelp.Arguments = "uninstall en"
            pHelp.UseShellExecute = True
            'pHelp.WindowStyle = ProcessWindowStyle.Hidden
            Dim proc As Process = Process.Start(pHelp)
        End If

        'If ind.cte.AppVars.chkIntegrateInWindowsMenu = False Then
        '    'todo: eliminar asociacion
        '    ind.win.tools.FileAssociation.ExtensionRemove("." & ind.cte.AppConst.fileCryptExtension)
        '    Exit Sub
        'End If
        'Dim desc As String = ind.cte.AppConst.AppName & " file"
        'Dim appPath As String = My.Application.Info.DirectoryPath
        'Dim icopath As String = IO.Path.Combine(appPath, "onecrypt.ico")
        'ind.win.tools.FileAssociation.ExtensionAssociate("." & ind.cte.AppConst.fileCryptExtension, ind.cte.AppConst.AppProgID, desc, icopath, ind.cte.AppConst.AppName)
    End Sub

    Private Sub chkStartWithWindows_CheckedChanged(sender As Object, e As EventArgs) Handles chkStartWithWindows.CheckedChanged
        If loadingSettings = True Then
            Exit Sub
        End If
        Dim flag As Boolean = chkStartWithWindows.Checked
        Try
            ind.settings.SettingsLink.chkStartWithWindows = flag
            ind.settings.SettingsLink.saveProperties()
            ind.tools.AppWindowsHelper.AddApplicationToWindowsStart(flag, Application.ProductName, Application.ExecutablePath)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try


    End Sub

    Private Sub chkOpenImgInApp_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenImgInApp.CheckedChanged
        If loadingSettings = True Then
            Exit Sub
        End If
        Try
            ind.settings.SettingsLink.chkOpenImgInApp = Me.chkOpenImgInApp.Checked
            ind.settings.SettingsLink.saveProperties()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub
    Private Sub chkOpenAppWithCtrolKey_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenAppWithCtrolKey.CheckedChanged
        If loadingSettings = True Then
            Exit Sub
        End If
        Dim previousCheckState As Boolean = Not Me.chkOpenAppWithCtrolKey.Checked
        Try
            If Me.chkOpenAppWithCtrolKey.Checked <> ind.settings.SettingsLink.chkOpenAppWithCtrolKey Then
                ind.settings.SettingsLink.chkOpenAppWithCtrolKey = Me.chkOpenAppWithCtrolKey.Checked
                ind.crypt.OCGlobal.mainForm.installRemoveKeyboardHook(Me.chkOpenAppWithCtrolKey.Checked, True)

                ind.settings.SettingsLink.saveProperties()
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub


    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.Text = Global.ind.lang.UILang.frmOptions_text
        Me.lbl_pwdTitle.Text = Global.ind.lang.UILang.frmOptions_lbl_pwdTitle
        Me.lblInfoOptions.Text = Global.ind.lang.UILang.frmOptions_lblInfoOptions

        Me.chkOpenAfterDecrypt.Tag = Global.ind.lang.UILang.frmOptions_chkOpenAfterDecrypt_tag
        Me.chkOpenAfterDecrypt.Text = Global.ind.lang.UILang.frmOptions_chkOpenAfterDecrypt

        Me.chkNotDecryptWithoutExtension.Tag = Global.ind.lang.UILang.frmOptions_chkNotDecryptWithoutExtension_tag
        Me.chkNotDecryptWithoutExtension.Text = Global.ind.lang.UILang.frmOptions_chkNotDecryptWithoutExtension

        Me.chkIntegrateInWindowsMenu.Tag = Global.ind.lang.UILang.frmOptions_chkIntegrateInWindowsMenu_tag
        Me.chkIntegrateInWindowsMenu.Text = Global.ind.lang.UILang.frmOptions_chkIntegrateInWindowsMenu

        Me.chkStartWithWindows.Tag = Global.ind.lang.UILang.frmOptions_chkStartWithWindows_tag
        Me.chkStartWithWindows.Text = String.Format(Global.ind.lang.UILang.frmOptions_chkStartWithWindows, ind.cte.AppConst.AppName)

        Me.chkOpenImgInApp.Text = Global.ind.lang.UILang.frmOptions_chkOpenImg
        Me.chkOpenImgInApp.Tag = Global.ind.lang.UILang.frmOptions_chkOpenImg_tag

        Me.chkOpenAppWithCtrolKey.Text = Global.ind.lang.UILang.frmOptions_chkOpenAppWithCtrolKey
        Me.chkOpenAppWithCtrolKey.Tag = Global.ind.lang.UILang.frmOptions_chkOpenAppWithCtrolKey_tag

    End Sub


End Class