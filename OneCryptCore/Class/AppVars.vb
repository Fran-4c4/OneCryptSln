
Namespace ind.cte
    ''' <summary>
    ''' generic vars for the application
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppVars
        ''' <summary>
        ''' load user settings
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub LoadUserSettings()
            'put here the vars that are not loaded directly inline

            My.Application.ChangeUICulture(ind.settings.SettingsLink.LanguageSelected)

        End Sub
        ''' <summary>
        ''' When the user is not validated some functions are not available
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared UserValidated As Boolean = False
        ''' <summary>
        ''' password introduced by user
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared AppPasswordVar As String
        ''' <summary>
        ''' password introduced by user
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Property AppPassword As String

            Get
                Return ind.cte.AppVars.AppPasswordVar
            End Get
            Set(value As String)
                If Not value Is Nothing Then
                    ind.cte.AppVars.AppPasswordVar = value.Trim
                Else
                    ind.cte.AppVars.AppPasswordVar = ""
                End If
            End Set
        End Property
    End Class
End Namespace


