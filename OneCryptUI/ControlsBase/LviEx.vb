
Namespace ind.win
    ''' <summary>
    ''' listview item extended
    ''' </summary>
    Public Class ListViewItemEx
        Inherits Windows.Forms.ListViewItem
        ''' <summary>
        ''' file asociated to this listviewitem
        ''' </summary>
        Public WithEvents coFile As ind.crypt.FileObject



        Private Sub coFile_fileChangedEvent(fileObj As crypt.FileObject) Handles coFile.fileChangedEvent
            If Me.ListView.InvokeRequired Then
                Me.ListView.Invoke(New MethodInvoker(Sub() ind.win.ListViewHelper.ListViewItemFillByObject(Me, fileObj)))
            Else
                ind.win.ListViewHelper.ListViewItemFillByObject(Me, fileObj)
            End If

        End Sub

        Private Sub coFile_fileChangedEvent1(fileObj As crypt.FileObject) Handles coFile.fileStatusEvent
            If Me.ListView.InvokeRequired Then
                Me.ListView.Invoke(New MethodInvoker(Sub() ListViewHelper.ListViewItemUpdate(Me, fileObj)))
            Else
                ind.win.ListViewHelper.ListViewItemUpdate(Me, fileObj)
            End If

        End Sub

    End Class
End Namespace

