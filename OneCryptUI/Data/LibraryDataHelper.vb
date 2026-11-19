Imports System.Data.SqlServerCe
Imports System.Data

Namespace ind.crypt

    ''' <summary>
    ''' class to manipulate operations with database using either dataset or direct access
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LibraryDataHelper
        Implements IDisposable

        Dim WithEvents daFiles As New LibraryDataSetTableAdapters.T_FilesTableAdapter
        Dim daLib As New LibraryDataSetTableAdapters.T_Library_FilesTableAdapter
        Dim daRowID As New LibraryDataSetTableAdapters.T_RowIDTableAdapter
        Public dtRowID As LibraryDataSet.T_RowIDDataTable
        Public Sub New()
            'this handler is necesary to get the id when is created a file due to sqlce restrictions
            'AddHandler daFiles.Adapter.RowUpdated, AddressOf HandleRowUpdated

            'RemoveHandler daFiles.Adapter.RowUpdated, AddressOf HandleRowUpdated
            dtRowID = daRowID.GetDtRowID
        End Sub

        Public Sub StarTrans()
            daFiles.Connection.Open()
            daFiles.Transaction = daFiles.Connection.BeginTransaction()
            daLib.Connection = daFiles.Connection
            daLib.Transaction = daFiles.Transaction
            daRowID.Connection = daFiles.Connection
            daRowID.Transaction = daFiles.Transaction
        End Sub

        Public Sub CommitTrans()
            Try
                daFiles.Transaction.Commit()
                daFiles.Connection.Close()
                If daLib.Connection.State = ConnectionState.Open Then
                    daLib.Connection.Close()
                End If
            Catch ex As Exception

            End Try
        End Sub
        Public Sub RollBackTrans()

            Try
                daFiles.Transaction.Rollback()
                daFiles.Connection.Close()
                If daLib.Connection.State = ConnectionState.Open Then
                    daLib.Connection.Close()
                End If
            Catch ex As Exception

            End Try

        End Sub

        Dim idrowtest As Integer = 0

        ''' <summary>
        ''' Add a file to database if not existObj an add the relation.
        ''' </summary>
        ''' <param name="libraryID"></param>
        ''' <param name="coFile"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function AddFileToLibrary(libraryID As Integer, ByRef coFile As ind.crypt.FileObject) As Integer

            Dim isNewRecord As Boolean = True ' CBool(IIf(coFile.id <= 0, True, False))

            Dim result As Integer = 0

            Try

                If isNewRecord Then
                    'is new record in library but it can be registered in DDBB for others libraries
                    'check if existObj table files
                    Dim dt As LibraryDataSet.T_FilesDataTable = daFiles.GetDTByFilePath(coFile.FullPath)
                    If dt.Rows.Count = 1 Then
                        'existObj in DDBB so we need only get the id and asociate to library
                        Dim drExist As LibraryDataSet.T_FilesRow = DirectCast(dt.Rows(0), LibraryDataSet.T_FilesRow)
                        coFile.id = drExist.id
                        'asociate with library
                        Me.AsociateFileToLibrary(libraryID, coFile.id)

                    Else
                        'file not existObj in DDBB we need to add first
                        Dim stopWatch2 As New ind.tools.MyTimer(True, "Insert in file DDBB")
                        Dim dr As LibraryDataSet.T_FilesRow
                        dr = dt.NewT_FilesRow
                        dr.id = coFile.id
                        dr.Name = coFile.FileName
                        dr.FileNamePathQualified = coFile.FullPath
                        dr.fileNotfound = coFile.FileNotFound
                        dr.size = coFile.length
                        dr.lastAccess = coFile.dateLastAccess
                        dr.Computer = coFile.ComputerLastAccess
                        dt.Rows.Add(dr)

                        idrowtest += 1
                        Me.daRowID.UpdateRowIdPlus1()
                        dtRowID = daRowID.GetDtRowID
                        Dim idRow As Integer = CInt(Me.dtRowID.Rows(0)(1))

                        dr.id = idRow
                        coFile.id = dr.id

                        result = daFiles.Update(dr)
                        stopWatch2.StopGetResult("Insert file Row ", tools.MyTimer.WatchOptionsEnum.reestartWatch)

                        'asociate with library
                        Me.AsociateFileToLibrary(libraryID, coFile.id)
                        stopWatch2.StopGetResult("Insert relation Row ", tools.MyTimer.WatchOptionsEnum.reestartWatch)
                    End If

                Else
                    'is not a new record in library, we need only update data
                    'but the validation is using global collection that can hold the file but not existObj longer in database due to clean of orphans files.
                    result = daFiles.UpdateT_Files_ByFileID(coFile.FileName, coFile.FullPath, coFile.length, coFile.dateLastAccess, coFile.ComputerLastAccess, coFile.FileNotFound, coFile.id)
                    'asociate with library
                    Try
                        Me.AsociateFileToLibrary(libraryID, coFile.id)
                    Catch ex As Exception
                        ind.log.Logging.logErr("Trying to add a file to library that exists in library. This ocurrs when it is added form diferent place. For: " & coFile.FullPath)
                        Debug.WriteLine("Trying to add a file to library that exists in library. This ocurrs when it is added form diferent place. For: " & coFile.FullPath)
                    End Try

                End If

            Catch ex As Exception
                Throw ex
            Finally


            End Try

            Return result
        End Function

        'Private Sub HandleRowUpdated(ByVal sender As Object, ByVal e As SqlCeRowUpdatedEventArgs)
        '    If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
        '        Dim cmdNewID As New SqlCeCommand("SELECT @@IDENTITY", e.Command.Connection)
        '        Dim id As Integer = CType(cmdNewID.ExecuteScalar, Integer)
        '        Dim dr As LibraryDataSet.T_FilesRow = CType(e.Row, LibraryDataSet.T_FilesRow)
        '        dr.id = id

        '        e.Row.AcceptChanges()
        '    End If
        'End Sub

        Private Sub AsociateFileToLibrary(libraryId As Integer, fileId As Integer)
            'dalib is in transaction
            daLib.Insert(libraryId, FileId)
        End Sub
        ''' <summary>
        ''' Delete orphan files that are not used anymore by other libraries
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DeleteUnusedFiles() As Integer
            Dim stopWatch As New ind.tools.MyTimer(True, "deleteUnusedFiles")

            Dim da As New LibraryDataSetTableAdapters.T_FilesTableAdapter
            Try
                da.Connection.Open()
                da.Transaction = da.Connection.BeginTransaction()
                Dim result As Integer = da.DeleteNotUsedFiles
                stopWatch.StopGetResult("Deleted " & result, tools.MyTimer.WatchOptionsEnum.stopWatch)
                da.Transaction.Commit()

                Return result
            Catch ex As Exception
                da.Transaction.Rollback()
                Throw ex
            Finally
                If da.Connection.State = ConnectionState.Open Then
                    da.Connection.Close()
                End If


            End Try


        End Function

        ''' <summary>
        ''' remove file by id
        ''' </summary>
        ''' <param name="fileId"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteFile(fileId As Integer)

            Dim da As New LibraryDataSetTableAdapters.T_FilesTableAdapter
            Dim result As Integer = da.DeleteFileByID(fileID)


        End Sub


        Public Shared Sub updateLibraryLastAccess(libraryId As Integer, dateTo As Date)
            Dim mda As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
            Dim res As Integer = mda.UpdateLibraryLastAccessById(dateTo, libraryId)

        End Sub

        Public Shared Function UpdateFileByFullPath(file As crypt.FileObject, originalPath As String) As Integer
            Return UpdateFileByFullPath(file.FullPath, file.length, file.dateLastAccess, file.ComputerLastAccess, file.FileNotFound, originalPath)
        End Function

        Public Shared Function UpdateFileByFullPath(ByVal newFileNamePathQualified As String, ByVal size As Global.System.Nullable(Of Double), ByVal lastAccess As Global.System.Nullable(Of Date) _
                                                    , ByVal Computer As String, ByVal fileNotfound As Global.System.Nullable(Of Boolean), ByVal Original_FileNamePathQualified As String) As Integer
            Dim dataAdapter As LibraryDataSetTableAdapters.T_FilesTableAdapter
            dataAdapter = New LibraryDataSetTableAdapters.T_FilesTableAdapter
            Dim res As Integer = dataAdapter.UpdateFileByFullPath(newFileNamePathQualified, size, lastAccess, Computer, fileNotfound, Original_FileNamePathQualified)
            dataAdapter.Dispose()
            Return res
        End Function

        Public Shared Function getResultsBySearchString(searchString As String) As LibraryDataSet.T_FilesDataTable
            Dim daFiles As New LibraryDataSetTableAdapters.T_FilesTableAdapter
            Dim dt As LibraryDataSet.T_FilesDataTable = daFiles.getFilesBySearchString(searchString)
            Return dt
        End Function


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

            ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).

                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class



End Namespace
