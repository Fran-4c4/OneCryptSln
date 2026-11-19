
Namespace ind.win
    ''' <summary>
    ''' class to encapsulate generic events for all the application
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppEventsClass

#Region "Events"
        Public Shared Event LibraryNewCreatedEvent(drLibrary As LibraryDataSet.T_LibraryRow)
        Public Shared Sub LibraryNewCreatedEventRaise(drLibrary As LibraryDataSet.T_LibraryRow)
            RaiseEvent LibraryNewCreatedEvent(drLibrary)
        End Sub

        Public Shared Event LibraryDeletedEvent(libraryIdKey As Integer)
        Public Shared Sub LibraryDeletedEventRaise(libraryIdKey As Integer)
            RaiseEvent LibraryDeletedEvent(libraryIdKey)
        End Sub

        Public Shared Event LibraryOpenEvent(libraryToLoad As LibraryDataSet.T_LibraryRow, options As String)
        Public Shared Sub LibraryOpenEventRaise(libraryToLoad As LibraryDataSet.T_LibraryRow, options As String)
            RaiseEvent LibraryOpenEvent(libraryToLoad, options)
        End Sub

        Public Shared Event LibraryCloseEvent(libraryIdKey As Integer)
        Public Shared Sub LibraryCloseEventRaise(libraryIdKey As Integer)
            RaiseEvent LibraryCloseEvent(libraryIdKey)
        End Sub

        Public Shared Event ObjectOpenEvent(code As Integer)
        Public Shared Sub ObjectOpenEventRaise(code As Integer)
            RaiseEvent ObjectOpenEvent(code)
        End Sub

        Public Shared Event LibrarySaveEvent(libraryToSave As LibraryDataSet.T_LibraryRow)
        Public Shared Sub LibrarySaveEventRaise(libraryToSave As LibraryDataSet.T_LibraryRow)
            RaiseEvent LibrarySaveEvent(libraryToSave)
        End Sub

#End Region

    End Class
End Namespace

