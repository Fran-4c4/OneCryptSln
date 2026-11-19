Imports System.IO

Namespace ind.crypt
    ''' <summary>
    ''' class to host global variables
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OCGlobal
        ''' <summary>
        ''' Collection with all loaded files in application. only load the common files in libraies loaded
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared WithEvents FileObjCollection As New ind.crypt.FileObjCollection

        ''' <summary>
        ''' id of library of recent files
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared libraryRecentFilesGlob As LibraryDataSet.T_LibraryRow
        ''' <summary>
        ''' quick access to ocexplorer control
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared WithEvents ocExplorer As UCExplorer
        ''' <summary>
        ''' mainForm of the application
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared mainForm As frmMain

        Public Class tabFixedCodes
            Public Const tabRecentLibs As Integer = -1
            'Public Const tabRecentFiles As Integer = -2 'use libraryRecentFilesGlob instead
            Public Const tabPasswordManager As Integer = -3
        End Class


    End Class

End Namespace

