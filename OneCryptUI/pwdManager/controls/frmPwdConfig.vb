Imports My.Resources

Public Class frmPwdConfig

    Dim pwdFilePathCtrol As New PwdDataBaseFileHelper

    Private Sub frmPwdConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myAppEvents_LanguageChangedEvent()

        If ind.settings.SettingsLink.PwdDatabaseFilePath = "" Then
            Me.chkSaveOnAppFolder.Checked = True
            Me.txtFileDatabasePath.TextBoxText = "" 'My.Application.Info.DirectoryPath
            Me.txtFileDatabasePath.Enabled = False
            Me.btnSelectFolder.Enabled = False
        Else
            Me.txtFileDatabasePath.Enabled = True
            Me.btnSelectFolder.Enabled = True
            Me.txtFileDatabasePath.TextBoxText = ind.settings.SettingsLink.PwdDatabaseFilePath
            Me.chkSaveOnAppFolder.Checked = False
        End If

    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        Try

            Dim dlg As New FolderBrowserDialog
            dlg.Description = ind.lang.pwdOptions.folderbrowserdialog_Description
            If ind.settings.SettingsLink.PwdDatabaseFilePath = "" Then
                dlg.SelectedPath = My.Application.Info.DirectoryPath
            Else
                dlg.SelectedPath = ind.settings.SettingsLink.PwdDatabaseFilePath
            End If
            'dlg.RootFolder = Environment.SpecialFolder.MyDocuments 'start root on and can not up 
            dlg.ShowNewFolderButton = True

            If dlg.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            'continue
            Me.txtFileDatabasePath.TextBoxText = dlg.SelectedPath

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub


    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Try

            If Me.pwdFilePathCtrol.ValidatePath(Me.chkSaveOnAppFolder.Checked, Me.txtFileDatabasePath.TextBoxText) = True Then
                Me.lblMsgBotom.Message = ind.lang.pwdOptions.pwdDDBBpathSavedMessage
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.ToolTip1.SetToolTip(Me.btnSelectFolder, ind.lang.pwdOptions.btnSelectFolder_TT)

        Me.lbl_Title.Text = ind.lang.pwdOptions.lbl_Title
        Me.lblFileDatabasePath.Text = ind.lang.pwdOptions.lblFileDatabasePath
        Me.lblInfoDDBBPath.Text = ind.lang.pwdOptions.lblInfoDDBBPath
        Me.chkSaveOnAppFolder.Text = ind.lang.pwdOptions.chkSaveOnAppFolder


        Me.btnCancel.Text = ind.lang.frmDlgLang.cancel_Button1
        Me.btnOk.Text = ind.lang.frmDlgLang.Save_Button


    End Sub



    ''' <summary>
    ''' class helper for pwd ddbb path file
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PwdDataBaseFileHelper
        Public fileOrigin As String = ""

        Public Sub New()
            If ind.settings.SettingsLink.PwdDatabaseFilePath = "" Then
                Dim appDataFolder As String = ind.cte.AppConst.getUserFolder
                fileOrigin = IO.Path.Combine(appDataFolder, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
            Else
                fileOrigin = IO.Path.Combine(ind.settings.SettingsLink.PwdDatabaseFilePath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
            End If
        End Sub

        Public Function ValidatePath(saveOnAppFolder As Boolean, Optional pwdFileFolderNew As String = "") As Boolean
            If saveOnAppFolder = True Then
                If ind.settings.SettingsLink.PwdDatabaseFilePath <> "" Then
                    Dim fileSource As String = IO.Path.Combine(ind.settings.SettingsLink.PwdDatabaseFilePath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                    If IO.File.Exists(fileSource) = True Then
                        Dim appDataFolder As String = ind.cte.AppConst.getUserFolder
                        Dim fileTarget As String = IO.Path.Combine(appDataFolder, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                        IO.File.Move(fileSource, fileTarget)
                    End If

                    ind.settings.SettingsLink.PwdDatabaseFilePath = ind.cte.AppConst.getUserFolder
                    ind.settings.SettingsLink.saveProperties()
                    Return True
                Else
                    'continue on app folder
                    Return True
                End If


            Else
                If IO.Directory.Exists(pwdFileFolderNew) Then
                    If pwdFileFolderNew = ind.settings.SettingsLink.PwdDatabaseFilePath Then
                        Return True
                        ' it is the same
                    End If

                    'save actual file to new location
                    Dim folderSource As String = ind.settings.SettingsLink.PwdDatabaseFilePath
                    If folderSource = "" Then
                        Dim appDataFolder As String = ind.cte.AppConst.getUserFolder
                        folderSource = appDataFolder
                    End If
                    Dim fileSource As String = IO.Path.Combine(folderSource, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                    If IO.File.Exists(fileSource) = False Then
                        'old version, copy from application folder
                        fileSource = IO.Path.Combine(My.Application.Info.DirectoryPath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
                    End If
                    Dim fileTarget As String = IO.Path.Combine(pwdFileFolderNew, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)

                    IO.File.Move(fileSource, fileTarget)
                    ind.settings.SettingsLink.PwdDatabaseFilePath = pwdFileFolderNew
                    ind.settings.SettingsLink.saveProperties()
                    Return True
                Else
                    Throw New IO.DirectoryNotFoundException
                End If
            End If
        End Function

    End Class

    Private Sub chkSaveOnAppFolder_CheckedChanged(sender As Object, e As EventArgs) Handles chkSaveOnAppFolder.CheckedChanged
        Dim appDataFolder As String = ind.cte.AppConst.getUserFolder
        Dim pwdfile As String = IO.Path.Combine(appDataFolder, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)

        If chkSaveOnAppFolder.Checked = True Then
            Me.txtFileDatabasePath.TextBoxText = appDataFolder
            Me.txtFileDatabasePath.Enabled = False
            Me.btnSelectFolder.Enabled = False
        Else
            Me.txtFileDatabasePath.TextBoxText = appDataFolder
            Me.txtFileDatabasePath.Enabled = True
            Me.btnSelectFolder.Enabled = True
        End If
    End Sub

    Private Sub txtFileDatabasePath_Load(sender As Object, e As EventArgs) Handles txtFileDatabasePath.Load

    End Sub
End Class
