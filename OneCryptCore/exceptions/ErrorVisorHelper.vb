

Namespace ind.tools.error

    ''' <summary>
    ''' class to manage error and store as list to see later in long process
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ErrorVisorHelper

        Private lockObj As New Object

        Public errorList As New ArrayList

        Public Sub add(error_exception As Exception, Optional lviFile As Windows.Forms.ListViewItem = Nothing)
            Dim eh As New ErrorHelper
            eh.add(error_exception, lviFile)
            addError(eh)
        End Sub

        Public Sub add(error_msg As String, errType As ErrorHelper.errorTypeEnum)
            Dim em As New ErrorHelper
            em.error_msg = error_msg
            em.errType = errType
            SyncLock lockObj
                Me.errorList.Add(em)
            End SyncLock
        End Sub

        Public Sub add(error_exception As Exception)
            Dim eh As New ErrorHelper
            eh.error_exception = error_exception
            Me.addError(eh)

        End Sub

        Public Sub addError(eh As ErrorHelper)
            SyncLock lockObj
                Me.errorList.Add(eh)
            End SyncLock
        End Sub

        Public Sub clearList()
            SyncLock lockObj
                Me.errorList.Clear()
            End SyncLock
        End Sub


    End Class
    ''' <summary>
    ''' class to store errors
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ErrorHelper
        ''' <summary>
        ''' Errors that we control to show more efficient info to user.
        ''' Check description in getErrorTypeForIU method in frmerrorvisor form
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum errorTypeEnum As Byte
            None = 0
            Unknown = 1
            FileNotFoundException = 2
            FileNotDecryptedException = 3
            Other = 4
            FileInUseException = 5
            OperationCancelledByUser = 7
        End Enum

        Public errType As errorTypeEnum = errorTypeEnum.None
        Public errNumber As String
        Public fileObj As ind.crypt.FileObject

        Public error_exception As Exception
        'Public Property error_exception As Exception


        Public error_msg As String

        Public Sub add(error_exception As Exception, Optional lviFile As Windows.Forms.ListViewItem = Nothing)
            Me.error_exception = error_exception
            If error_exception Is Nothing Then
                Me.errType = errorTypeEnum.None
            ElseIf TypeOf (error_exception) Is ind.exceptions.FileNotDecryptedException Then
                Me.errType = errorTypeEnum.FileNotDecryptedException
            ElseIf TypeOf (error_exception) Is ind.exceptions.FileInUseException Then
                Me.errType = errorTypeEnum.FileInUseException
            ElseIf TypeOf (error_exception) Is IO.FileNotFoundException Then
                Me.errType = errorTypeEnum.FileNotFoundException
            Else
                Me.errType = errorTypeEnum.Other
            End If
            If Not lviFile Is Nothing Then
                Dim fi As New ind.crypt.FileObject(lviFile)
                Me.fileObj = fi
            End If

        End Sub

        Public Function getErrMsg() As String
            If Me.error_exception Is Nothing Then

                Return error_msg
            Else
                Return Me.error_exception.Message
            End If
        End Function

    End Class

End Namespace
