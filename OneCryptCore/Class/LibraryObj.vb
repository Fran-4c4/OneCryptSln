

Namespace ind.crypt

    ''' <summary>
    ''' class to encapsulate a project
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LibraryObj

        Public libraryID As Integer
        ''' <summary>
        ''' library name without extension
        ''' </summary>
        ''' <remarks></remarks>
        Public LibraryName As String

        Public lastAccessDate As Date = Nothing
        ''' <summary>
        ''' items in project
        ''' </summary>
        ''' <remarks></remarks>
        Public itemsCount As Integer

        Public Sub New(LibraryName As String, libraryID As Integer)
            Me.libraryID = libraryID
            Me.LibraryName = LibraryName

        End Sub



    End Class

End Namespace
