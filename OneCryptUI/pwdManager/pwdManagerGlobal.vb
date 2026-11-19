
Namespace ind.pwd

    ''' <summary>
    ''' class to manage global data for password manager and include in other projects
    ''' </summary>
    ''' <remarks></remarks>
    Public Class pwdManagerGlobal
        ''' <summary>
        ''' group codes for listview
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum pwdGroupsEnum As Byte
            generic = 0
            Websites = 1
            Email = 2
            creditcard = 3
            bank = 4
            computer = 5
            other = 6
        End Enum
        ''' <summary>
        ''' name of password database file
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared OneCryptpwdFile As String = "OneCryptPwd.ocpt"

    End Class
End Namespace