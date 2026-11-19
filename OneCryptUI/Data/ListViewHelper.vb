

Namespace ind.win


    Public Class ListViewHelper

        Public Enum lvImageEnum As Integer
            fileNotFound = 1
            fileIsEncrypted = 0
            FileIsDecrypted = -1
        End Enum

        Public Shared Function CreateLViEmpty() As ind.win.ListViewItemEx
            Return CreateLViEmpty(-1, "", "")
        End Function

        Public Shared Function CreateLViEmpty(id As Integer, FullPath As String, filename As String) As ind.win.ListViewItemEx
            Dim lvi As New ind.win.ListViewItemEx
            lvi.Name = FullPath
            lvi.Text = filename
            'we need to add the same number of items as columns
            lvi.SubItems.Add("")    'name
            lvi.SubItems.Add("")    'path
            lvi.SubItems.Add("")    'type\extension
            lvi.SubItems.Add("")    'size
            lvi.SubItems.Add("")    'last access
            lvi.SubItems.Add("")    'computer
            Return lvi
        End Function

        Public Shared Sub ListViewItemFillByObject(ByRef lviChanged As ind.win.ListViewItemEx, ByRef coFile As ind.crypt.FileObject)
            If lviChanged Is Nothing Then
                lviChanged = CreateLViEmpty()
            End If

            If coFile.FileNotFound = True Then
                lviChanged.StateImageIndex = lvImageEnum.fileNotFound
                lviChanged.ToolTipText = ind.lang.ExceptionsLang.fileNotFound_listview
            Else
                lviChanged.ToolTipText = ""
                If coFile.state = crypt.Crypto.CryptoStateEnum.IsEncripted Then
                    lviChanged.StateImageIndex = lvImageEnum.fileIsEncrypted
                Else
                    lviChanged.StateImageIndex = lvImageEnum.FileIsDecrypted
                End If
            End If


            lviChanged.coFile = coFile
            lviChanged.Name = coFile.FullPath
            lviChanged.Text = coFile.FileName
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.name).Text = coFile.FileName
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text = coFile.FullPath
            'size
            If coFile.length >= 1048576 Then 'Mb
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length / 1048576, "#,##0 Mb"))
            ElseIf coFile.length >= 1024 And coFile.length < 1048576 Then
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length / 1024, "#,### KB"))
            ElseIf coFile.length < 1024 Then
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length, "##0 Bytes"))
            End If
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.type).Text = coFile.extension
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.dateLastAccess).Text = CStr(coFile.dateLastAccess)
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.ComputerLastAccess).Text = coFile.ComputerLastAccess
        End Sub

        Public Shared Sub ListViewItemUpdate(ByRef lviChanged As ind.win.ListViewItemEx, ByRef coFile As ind.crypt.FileObject)

            If lviChanged Is Nothing Then
                Throw New Exception("Listview item is null.")
            End If

            If coFile.FileNotFound = True Then
                lviChanged.StateImageIndex = lvImageEnum.fileNotFound
                lviChanged.ToolTipText = ind.lang.ExceptionsLang.fileNotFound_listview
                Exit Sub
            Else
                If coFile.state = crypt.Crypto.CryptoStateEnum.IsEncripted Then
                    lviChanged.StateImageIndex = lvImageEnum.fileIsEncrypted
                Else
                    lviChanged.StateImageIndex = lvImageEnum.FileIsDecrypted
                End If
            End If

            lviChanged.Name = coFile.FullPath
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text = coFile.FullPath
            'size
            If coFile.length >= 1048576 Then 'Mb
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length / 1048576, "#,##0 Mb"))
            ElseIf coFile.length >= 1024 And coFile.length < 1048576 Then
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length / 1024, "#,### KB"))
            ElseIf coFile.length < 1024 Then
                lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text = (Format(coFile.length, "##0 Bytes"))
            End If
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.dateLastAccess).Text = CStr(coFile.dateLastAccess)
            lviChanged.SubItems(ind.crypt.FileObject.lvFieldEnum.ComputerLastAccess).Text = coFile.ComputerLastAccess
        End Sub

    End Class
End Namespace

