Imports System.Configuration




'This class allows you to handle specific events on the settings class:
' The SettingChanging event is raised before a setting's value is changed.
' The PropertyChanged event is raised after a setting's value is changed.
' The SettingsLoaded event is raised after the setting values are loaded.
' The SettingsSaving event is raised before the setting values are saved.

Namespace ind.settings

    ''' <summary>
    ''' This module is used only to simplify the operations
    ''' </summary>
    ''' <remarks></remarks>
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute()>
    Public Module SettingsShortcut

        ''' <summary>
        ''' link shortcut to settings
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SettingsLink() As onecryptSettings
            Get
                Return ind.settings.onecryptSettings.Default
            End Get
        End Property
    End Module

    Public NotInheritable Class onecryptSettings
        Inherits PortableSettingsProvider
        ''' <summary>
        ''' indicates the name of the settings file
        ''' </summary>
        ''' <returns></returns>
        Overrides Function GetAppSettingsFilename() As String
            Return "onecrypt.settings"
        End Function

        Private Shared defaultInstance As onecryptSettings
        ''' <summary>
        ''' the path to file
        ''' </summary>
        Public Shared appSettingsPath As String = String.Empty

        Public Shared ReadOnly Property [Default]() As onecryptSettings
            Get
                If defaultInstance Is Nothing Then
                    'If appSettingsPath = String.Empty Then
                    '    Throw New Exception("For developer only: Settings must be initialized before call.")
                    'End If
                    defaultInstance = New onecryptSettings()
                    defaultInstance.LoadProperties()
                End If
                Return defaultInstance
            End Get
        End Property



#Region "My.Settings Auto-Save Functionality"

        Public Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
            ind.settings.SettingsLink.saveProperties()
        End Sub

#End Region

        Public Sub saveProperties()
            Me.SetPropertyValues(Me.getPropertiesByReflection)
        End Sub

        Public Sub LoadProperties()
            Me.GetPropertyValues(Me.getPropertiesByReflection)
        End Sub

        ''' <summary>
        ''' Get the folder where DLL is stored, it is diferent from executable path.
        ''' Why this? Because DLL and executable are in the same path but when windows explorer open using shell the path is windows and do not find configuration file.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function GetAppSettingsPath() As String
            'Used to determine where to store the settings
            Dim mydocumentsFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim appDataFolder As String = IO.Path.Combine(mydocumentsFolder, ind.cte.AppConst.AppName)
            Return appDataFolder
        End Function

        Public Function getPropertiesByReflection() As Reflection.PropertyInfo()
            Dim myType As Type = Me.GetType
            ' Get the properties. 
            Dim myPropertyInfo As Reflection.PropertyInfo() = myType.GetProperties((Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance))
            Return myPropertyInfo
        End Function 'Main


        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property storedpwd() As String = ""

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property storepwdCheck() As Boolean = True

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property chkNotDecryptWithoutExtension() As Boolean = True

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("False")>
        Public Property chkIntegrateInWindowsMenu() As Boolean = False

        <Global.System.Configuration.UserScopedSettingAttribute()>
        Public Property chkSaveProjectsOnExit() As Boolean = False


        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property p2pudpReceiverPort() As String = "54545"

        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property p2pudpReceiverIPEndPoint() As String = "any"

        Dim ExplorerLastFolderVar As String = ""
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property ExplorerLastFolder() As String
            Get
                Return ExplorerLastFolderVar
            End Get
            Set(value As String)
                ExplorerLastFolderVar = value
            End Set
        End Property
        Dim chkOpenAfterDecryptVar As Boolean = True
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property chkOpenAfterDecrypt() As Boolean
            Get
                Return chkOpenAfterDecryptVar
            End Get
            Set(value As Boolean)
                chkOpenAfterDecryptVar = value
            End Set
        End Property
        Dim chkStartWithWindowsVar As Boolean = False
        <Global.System.Configuration.UserScopedSettingAttribute()>
        Public Property chkStartWithWindows() As Boolean
            Get
                Return chkStartWithWindowsVar
            End Get
            Set(value As Boolean)
                chkStartWithWindowsVar = value
            End Set
        End Property

        Dim LanguageSelectedVar As String
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property LanguageSelected() As String
            Get

                If LanguageSelectedVar Is Nothing Or LanguageSelectedVar = "" Then
                    LanguageSelectedVar = Globalization.CultureInfo.CurrentUICulture.Name 'use this for user interface messages
                    'lang = Globalization.CultureInfo.CurrentCulture.Name 'use this for formatting, like dates and others. This is selected by user in windows control panel.
                End If
                Return LanguageSelectedVar
            End Get
            Set(value As String)
                LanguageSelectedVar = value
                My.Application.ChangeUICulture(value)
                ind.encrypter.AppEventsClass.LanguageChangedEventRaise()
            End Set
        End Property

        Dim chkAppFirstInitVar As Boolean = False
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("False")>
        Public Property chkAppFirstInit() As Boolean
            Get
                Return chkAppFirstInitVar
            End Get
            Set(value As Boolean)

                chkAppFirstInitVar = value
            End Set
        End Property

        Dim chkAppFirstInitVersionVar As String = ""
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("0.0.0.0")>
        Public Property chkAppFirstVersion() As String
            Get
                Return chkAppFirstInitVersionVar
            End Get
            Set(value As String)

                chkAppFirstInitVersionVar = value
            End Set
        End Property

        Dim fileDatabasePathVar As String = ""
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property OCFileDatabasePath() As String
            Get
                Return fileDatabasePathVar
            End Get
            Set(value As String)

                fileDatabasePathVar = value
            End Set
        End Property

        Dim filePwdDatabasePathVar As String = ""
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("")>
        Public Property PwdDatabaseFilePath() As String
            Get
                Return filePwdDatabasePathVar
            End Get
            Set(value As String)
                filePwdDatabasePathVar = value
            End Set
        End Property

        Dim chkOpenImgInAppVar As Boolean = True
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property chkOpenImgInApp() As Boolean
            Get
                Return chkOpenImgInAppVar
            End Get
            Set(value As Boolean)
                chkOpenImgInAppVar = value
            End Set
        End Property

        Dim chkOpenAppWithCtrolKeyVar As Boolean = True
        <Global.System.Configuration.UserScopedSettingAttribute(),
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),
         Global.System.Configuration.DefaultSettingValueAttribute("True")>
        Public Property chkOpenAppWithCtrolKey() As Boolean
            Get
                Return chkOpenAppWithCtrolKeyVar
            End Get
            Set(value As Boolean)
                chkOpenAppWithCtrolKeyVar = value
            End Set
        End Property


    End Class

End Namespace