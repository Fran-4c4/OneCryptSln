


Public Class UCLibraryCrypt123

    ''' <summary>
    ''' uclibrarytabapge that holds this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Property TabPageContainer() As UCLibraryTabpage

#Region "Events"
    ' ''' <summary>
    ' ''' shared event raised to indicate that an entywas changed
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    'Public Shared Event EntityChangedEvent(ByVal sender As UCLibraryCrypt)
    ' ''' <summary>
    ' ''' event that indicate that the control must be unloaded
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    'Public Shared Event closeControlEvent(ByVal sender As UCLibraryCrypt)


#End Region


    'Public Property LibraryLoaded As ind.crypt.LibraryObj

    '    Get
    '        Return Me.UcCryptExplorer1.LibraryLoaded
    '    End Get
    '    Set(value As ind.crypt.LibraryObj)
    '        Me.UcCryptExplorer1.LibraryLoaded = value
    '        Me.lblEncryptedLib.Text = value.LibraryName
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' load control and necesary sub controls
    ' ''' </summary>
    ' ''' <param name="LibraryPathToOpen"></param>
    ' ''' <remarks></remarks>
    'Public Sub initConfig(LibraryPathToOpen As LibraryDataSet.T_LibraryRow)
    '    Me.UcCryptExplorer1.LibraryLoad(LibraryPathToOpen)
    '    Me.lblEncryptedLib.Text = LibraryPathToOpen.Name
    'End Sub


    'Private Sub UcCryptExplorer1_LvItemAddedCountEvent(listViewItemOper As UCCryptExplorer.listViewItemOperationEnum, addedCount As Integer, newCount As Integer) Handles UcCryptExplorer1.LvItemAddedCountEvent
    '    'draw in title the number of elements
    '    Me.lblListViewItemCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UCExplorer_lblListViewItemCount, newCount)
    '    If listViewItemOper = UCCryptExplorer.listViewItemOperationEnum.removeItem Then
    '        Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemRemoveCountEvent, addedCount)
    '    ElseIf listViewItemOper = UCCryptExplorer.listViewItemOperationEnum.addItem Then
    '        Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemAddedCountEvent, addedCount)
    '    ElseIf listViewItemOper = UCCryptExplorer.listViewItemOperationEnum.loadingLibrary Then
    '        Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemAddedLibraryLoad, addedCount)
    '    ElseIf listViewItemOper = UCCryptExplorer.listViewItemOperationEnum.savingLibrary Then
    '        Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemSaveCountEvent, addedCount)
    '    End If
    'End Sub




    'Private Sub ucCryptExp_lvItemSelectedEvent(lviListSel As ListView.SelectedListViewItemCollection) Handles UcCryptExplorer1.lvItemSelectedEvent
    '    Me.tssFileCount.Text = ""
    '    Me.tssFileName.Text = ""
    '    Dim lviListSelCount As Integer = lviListSel.Count
    '    If lviListSelCount = 0 Then
    '        Me.tssStatusExplorer.Text = ""
    '    Else
    '        Me.tssStatusExplorer.Text = String.Format(ind.lang.UILang.frmMain_ucCryptExp_lvItemSelectedEvent, lviListSelCount)
    '    End If
    'End Sub

    'Private Sub myAppEvents_FileEncryptedEvent(fileName As String, filePath As String, action As ind.crypt.Crypto.CryptoActionEnum) Handles UcCryptExplorer1.FileEncryptedEvent
    '    If action = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
    '        Me.tssFileName.Text = String.Format(ind.lang.UILang.frmMain_myAppEvents_FileEncryptedEvent_Dec, fileName, filePath)
    '    Else
    '        Me.tssFileName.Text = String.Format(ind.lang.UILang.frmMain_myAppEvents_FileEncryptedEvent_Enc, fileName, filePath)
    '    End If
    'End Sub






End Class
