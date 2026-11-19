Imports System.Drawing


Namespace ind.cte
    ''' <summary>
    ''' Class to encapsulate generic fields
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppConst

        ''' <summary>
        ''' ProgID : OneCryptFile
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared AppProgID As String = "OneCryptFile"
        ''' <summary>
        ''' Application name (OneCrypt)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared AppName As String = "OneCrypt"
        ''' <summary>
        ''' Extension for crypted files ocpt
        ''' </summary>
        Public Shared fileCryptExtension As String = "ocpt"


        ''' <summary>
        ''' Extension for library files ocptlib
        ''' </summary>
        Public Shared projectCryptExtension As String = "ocptLib"
        ''' <summary>
        ''' separator to save data on files.
        ''' \/:?*|:" are not allowed by windows in file names
        ''' </summary>
        Public Const splitSeparator As String = "|"
        ''' <summary>
        ''' Default file name project
        ''' </summary>
        Public Const ECryptManagerFileName As String = "OneCryptDefaultLibray.ocptLib"

        Public Const check123 As String = "EnUnLugarDeLaManchaDeCuyoNombreNoQuieroAcordarmeNacioUnHidalgoCaballero"

        Public Shared Function getUserFolder() As String
            Dim mydocumentsFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim appDataFolder As String = IO.Path.Combine(mydocumentsFolder, ind.cte.AppConst.AppName)
            Return appDataFolder
        End Function

#Region "App Fonts"
        ''' <summary>
        ''' General font for application
        ''' </summary>
        Public Shared AppFont2Gen As Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ''' <summary>
        ''' General font for application bold
        ''' </summary>
        Public Shared AppFont2GenB As Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Public Shared AppFont2Tit1B As Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Public Shared AppFont2Tit2B As Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Public Shared AppFont2Tit3B As Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
#End Region


    End Class
End Namespace

