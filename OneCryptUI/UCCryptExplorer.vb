Option Strict Off

Imports ExpTreeLib
Imports ExpTreeLib.CShItem
Imports ExpTreeLib.SystemImageListManager
Imports ExpTreeLib.ShellDll
Imports ExpTreeLib.ShellDll.ShellAPI
Imports ExpTreeLib.ShellDll.ShellHelper
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Data.SqlServerCe
Imports ind.crypt.FileObject

''' <summary>
''' class to handle a library
''' </summary>
''' <remarks></remarks>
Public Class UCCryptExplorer
    ''' <summary>
    ''' uclibrarytabapge that holds this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TabPageContainer() As UcLibraryTabpage

    Dim WithEvents assistant As UCAssistantMessage
    ''' <summary>
    ''' Loade project
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents LibraryLoaded As ind.crypt.LibraryObj
    ''' <summary>
    ''' Collection with all files included in library
    ''' </summary>
    ''' <remarks></remarks>
    Public LibraryFilesList As New ind.crypt.FileObjCollection
    ''' <summary>
    ''' Collection with all files added to library in session without save
    ''' </summary>
    ''' <remarks></remarks>
    Public LibraryFilesAddedList As New ind.crypt.FileObjCollection
    ''' <summary>
    ''' Collection with all files removed from library in session without save
    ''' </summary>
    ''' <remarks></remarks>
    Public LibraryFilesRemovedList As New ind.crypt.FileObjCollection
    ''' <summary>
    ''' if true the library save added or removed elemnts automatically
    ''' </summary>
    Public saveAutomatic As Boolean = False

    Public operationInProgress As operationEnum
    Public Enum operationEnum As Byte
        none = 0
        loading = 1
        saving = 2
        encrypting = 3
        decrypting = 4
    End Enum

    ''' <summary>
    ''' load control and necesary sub controls
    ''' </summary>
    ''' <param name="LibraryPathToOpen"></param>
    ''' <remarks></remarks>
    Public Sub initConfig(LibraryPathToOpen As LibraryDataSet.T_LibraryRow)
        Me.LibraryLoad(LibraryPathToOpen)
        Me.lblEncryptedLib.Text = LibraryPathToOpen.Name
    End Sub

#Region "Variables"
    ''' <summary>
    ''' Initial listviewitems count to know if there are changes.
    ''' </summary>
    ''' <remarks></remarks>
    Public lvItems_InitialCount As Integer
#End Region

#Region "   Public Properties"
    ''' <summary>
    ''' InitialLoadLimit is a the number of lv1.Items whose IconIndex will we fetched on initial load
    ''' the balance will be fetched AFTER lv1 shows its initial display
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), Category("Misc"),
    Description("Maximum # of Items to build in GUI Thread for initial display"),
    DefaultValue(32)>
    Public Property InitialLoadLimit() As Integer
        Get
            Return _InitialLoadLimit
        End Get
        Set(ByVal value As Integer)
            _InitialLoadLimit = value
        End Set
    End Property
    Private _InitialLoadLimit As Integer = 32

    ''' <summary>
    ''' WorkUpdateInterval is the Maximum # of Items to build in each 
    ''' BackGroundWorker Progress Interval before reporting them back to the GUI.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>If there are 200 items to show, the first InitialLoadLimit will be built and
    '''          displayed in the GUI thread. The balance will be built in the BackgroundWorker
    '''          thread and reported back to the GUI in chunks of WorkUpdateInterval Items.</remarks>
    <Browsable(True), Category("Misc"),
    Description("Maximum # of Items in each Background update interval"),
    DefaultValue(100)>
    Public Property WorkUpdateInterval() As Integer
        Get
            Return _WorkUpdateInterval
        End Get
        Set(ByVal value As Integer)
            _WorkUpdateInterval = value
        End Set
    End Property
    Private _WorkUpdateInterval As Integer = 100
#End Region

#Region "   Events"
    ''' <summary>
    ''' Items in listview, raised after operations
    ''' </summary>
    ''' <param name="count"></param>
    ''' <remarks></remarks>
    Public Event LvItemAddedCountEvent(listViewItemOper As ListViewItemOperationEnum, addedCount As Integer, newCount As Integer)
    ''' <summary>
    ''' raised when  items/files are selected by user
    ''' </summary>
    ''' <param name="lvi"></param>
    ''' <remarks></remarks>
    Public Event lvItemSelectedEvent(lviListSel As ListView.SelectedListViewItemCollection)
    ''' <summary>
    ''' Event raised when a library is fully loaded
    ''' </summary>
    ''' <param name="projectFileInfo"></param>
    ''' <remarks></remarks>
    Public Event LibraryAfterLoadedEvent(projectFileInfo As ind.crypt.LibraryObj)
#End Region

#Region "   Form Load/VisibleChanged, lv1 HandleCreated"
    Private Sub UCCryptExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        If controlIsInitialized = False Then
            controlIsInitialized = True
            myAppEvents_LanguageChangedEvent()
            Me.setListViewWidth()
            Me.configureAssistant()

        End If

    End Sub

    Private Sub configureAssistant()
        If Me.lvTreeExp.Items.Count = 0 Then
            Me.assistant = New UCAssistantMessage
            Me.assistant.Parent = Me
            Me.assistant.Left = 70
            Me.assistant.Top = 60
            Me.assistant.Visible = True
            Me.assistant.BringToFront()
            AddHandler Me.assistant.btnClose.Click, AddressOf Me.assistant_btnClose_Click
        End If
    End Sub

    Private Sub assistant_btnClose_Click(sender As Object, e As EventArgs)
        Me.removeAssistant()
    End Sub

    Friend Sub removeAssistant()
        If Not Me.assistant Is Nothing Then
            Me.assistant.Visible = False
            RemoveHandler Me.assistant.btnClose.Click, AddressOf Me.assistant_btnClose_Click
            Me.assistant.Dispose()
            Me.assistant = Nothing
        End If
    End Sub


    Private Sub setListViewWidth()
        Try
            If Not My.Settings.CryptListViewColumnWidth = "" Then
                Dim CryptListViewColumnWidth() As String = Split(My.Settings.CryptListViewColumnWidth, "|")
                For x As Integer = 0 To CryptListViewColumnWidth.Length - 1
                    Me.lvTreeExp.Columns(x).Width = CInt(CryptListViewColumnWidth(x))
                Next
            End If
        Catch ex As Exception
            Me.lvTreeExp.Columns(ind.crypt.FileObject.lvFieldEnum.name).Width = 200
            Me.lvTreeExp.Columns(ind.crypt.FileObject.lvFieldEnum.path).Width = 350
            Me.lvTreeExp.Columns(ind.crypt.FileObject.lvFieldEnum.type).Width = 65
            Me.lvTreeExp.Columns(ind.crypt.FileObject.lvFieldEnum.Length).Width = 40
        End Try
    End Sub

    Private Sub lvTreeExp_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvTreeExp.ColumnClick

        Dim myListView As ListView = sender
        myListView.ListViewItemSorter = New ListViewItemComparer(e.Column)

    End Sub

    ''' <summary>
    ''' Implements the manual sorting of items by columns.
    ''' Good example at https://www.codeproject.com/Articles/5332/ListView-Column-Sorter with better comparers
    ''' </summary>
    ''' <remarks></remarks>
    Class ListViewItemComparer
        Implements IComparer

        Private col As Integer

        Public Sub New()
            col = 0
        End Sub

        Public Sub New(ByVal column As Integer)
            col = column
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Function
    End Class

    Private Sub lvTreeExp_ColumnReordered(sender As Object, e As ColumnReorderedEventArgs) Handles lvTreeExp.ColumnReordered

    End Sub


    Private Sub lvTreeExp_HandleCreated(sender As Object, e As EventArgs) Handles lvTreeExp.HandleCreated

        SystemImageListManager.SetListViewImageList(lvTreeExp, False, False)
        Me.lvTreeExp.StateImageList = Me.lstViewImageListState
        'SystemImageListManager.SetListViewImageList(lvTreeExp, True, False)
    End Sub

#End Region

#Region "   Handlers for actions to visualize in interface"

    Private Sub LvItemAddedCountEvent_Handler(listViewItemOper As UCCryptExplorer.ListViewItemOperationEnum, addedCount As Integer, newCount As Integer) Handles Me.LvItemAddedCountEvent
        'draw in title the number of elements
        Me.lblListViewItemCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UCExplorer_lblListViewItemCount, newCount)
        'in down
        If listViewItemOper = UCCryptExplorer.ListViewItemOperationEnum.removeItem Then
            Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemRemoveCountEvent, addedCount)
        ElseIf listViewItemOper = UCCryptExplorer.ListViewItemOperationEnum.addItem Then
            Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemAddedCountEvent, addedCount)
        ElseIf listViewItemOper = UCCryptExplorer.ListViewItemOperationEnum.loadingLibrary Then
            Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemAddedLibraryLoad, addedCount)
        ElseIf listViewItemOper = UCCryptExplorer.ListViewItemOperationEnum.savingLibrary Then
            Me.tssFileCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UcExplorer_LvItemSaveCountEvent, addedCount)
        End If
    End Sub

    Private Sub updateToolStripStatus(itemBagData As ItemBagCls)
        If itemBagData.errorHappen = False Then
            If itemBagData.CryptoAction = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                Me.tssFileName.Text = String.Format(ind.lang.UILang.frmMain_myAppEvents_FileEncryptedEvent_Dec, itemBagData.co.FileName, itemBagData.co.FullPath)
            Else
                Me.tssFileName.Text = String.Format(ind.lang.UILang.frmMain_myAppEvents_FileEncryptedEvent_Enc, itemBagData.co.FileName, itemBagData.co.FullPath)
            End If
        End If
    End Sub


#End Region

#Region "   Listview other events"
    Private Sub lvTreeExp_ColumnWidthChanged(sender As Object, e As ColumnWidthChangedEventArgs) Handles lvTreeExp.ColumnWidthChanged
        Dim str As String = "{0}|{1}|{2}|{3}"

        str = String.Format(str, Me.lvTreeExp.Columns(0).Width, Me.lvTreeExp.Columns(1).Width, Me.lvTreeExp.Columns(2).Width, Me.lvTreeExp.Columns(3).Width)

        My.Settings.CryptListViewColumnWidth = str
    End Sub



    Private Sub lvTreeExp_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvTreeExp.ItemSelectionChanged
        Dim lviListSelCount As Integer = Me.lvTreeExp.SelectedItems.Count
        Debug.WriteLine("lvTreeExp_ItemSelectionChanged count: " & lviListSelCount)

        'message below
        Me.tssFileCount.Text = ""
        Me.tssFileName.Text = ""
        If lviListSelCount = 0 Then
            Me.tssStatusExplorer.Text = ""
        Else
            Me.tssStatusExplorer.Text = String.Format(ind.lang.UILang.frmMain_ucCryptExp_lvItemSelectedEvent, lviListSelCount)
        End If

        If lviListSelCount = 1 Then
            Dim lviEx As ind.win.ListViewItemEx = DirectCast(Me.lvTreeExp.SelectedItems(0), ind.win.ListViewItemEx)
            Dim coFile As ind.crypt.FileObject = lviEx.coFile

            Dim status As ind.crypt.FileObject.checkFileStatusEnum = coFile.checkIfFileExist()
            If status <> ind.crypt.FileObject.checkFileStatusEnum.exist Then
                coFile.RaisefileStatusEvent()
            End If


            ''check if existObj 
            'Dim fileExist As Boolean = IO.File.Exists(coFile.FullPath)
            'If fileExist = False Then
            '    'check in other state
            '    If coFile.extension = "" Then
            '        'check with encrypt extension
            '        fileExist = IO.File.Exists(coFile.FullPath & "." & ind.cte.AppConst.fileCryptExtension)
            '        If fileExist = True Then
            '            'file existObj with diferent extension so we need to change 
            '            coFile.FullPath = coFile.FullPath & "." & ind.cte.AppConst.fileCryptExtension
            '            ind.win.ListViewHelper.ListViewItemUpdate(lviEx, coFile)
            '        End If
            '    Else
            '        'check if existObj without extension
            '        fileExist = IO.File.Exists(coFile.FullPathWithoutCryptExtension)
            '        If fileExist = True Then
            '            'file existObj with diferent extension so we need to change 
            '            coFile.FullPath = coFile.FullPathWithoutCryptExtension
            '            ind.win.ListViewHelper.ListViewItemUpdate(lviEx, coFile)
            '        End If
            '    End If
            'End If

            'If fileExist = False Then
            '    lviEx.StateImageIndex = 1
            '    lviEx.ToolTipText = ind.lang.ExceptionsLang.fileNotFound_listview
            'End If
        End If

        displayToolbar()

        RaiseEvent lvItemSelectedEvent(Me.lvTreeExp.SelectedItems)
    End Sub

#End Region

    Public Function getSelectedItems() As ListView.SelectedListViewItemCollection
        Return Me.lvTreeExp.SelectedItems
    End Function

    Public Function getTotalItemsCount() As Integer
        Return Me.lvTreeExp.Items.Count
    End Function

#Region "   Listview_DoubleClick and contextmenu"
    ''' <summary>
    ''' select wich buttons to show to the user
    ''' </summary>
    Private Sub displayToolbar()
        Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
        Dim count As Integer = lvCol.Count
        Debug.WriteLine("displaytoolbar count=" & count)
        If count = 0 Then
            'nothing is selected
            Me.btnsOpenFile.Enabled = False
            Me.btnsOpenFolder.Enabled = False

            Me.btnsDecrypt.Enabled = False
            Me.btnsEncrypt.Enabled = False
            Me.btnsDecryptAndOpen.Enabled = False
            Me.tstSeparatorTopZip.Enabled = False
            Me.btnsZip.Enabled = False
            Me.btnsRemoveFromLibrary.Enabled = False
            Me.btnDeleteFile.Enabled = False

        ElseIf count > 1 Then
            'more than 1 so we don´t check any and set common buttons
            Me.btnsOpenFile.Enabled = True
            Me.btnsOpenFolder.Enabled = False

            Me.btnsDecrypt.Enabled = True
            Me.btnsEncrypt.Enabled = True
            Me.btnsDecryptAndOpen.Enabled = True
            Me.tstSeparatorTopZip.Enabled = True
            Me.btnsZip.Enabled = True
            Me.btnsRemoveFromLibrary.Enabled = True
            Me.btnDeleteFile.Enabled = True
            'Me.btnsFileNotFoundText.Enabled = False


        ElseIf count = 1 Then

            Dim lviEx As ind.win.ListViewItemEx = DirectCast(lvCol(0), ind.win.ListViewItemEx)
            Dim coFile As ind.crypt.FileObject = lviEx.coFile
            'todo: check if it was checked before
            If coFile.FileNotFound = True Then
                'hide buttons
                Me.btnsOpenFile.Enabled = False

                Me.btnsEncrypt.Enabled = False
                Me.btnsDecrypt.Enabled = False
                Me.btnsDecryptAndOpen.Enabled = False
                Me.btnsZip.Enabled = False

                Me.btnDeleteFile.Enabled = False
            Else
                'File existObj
                Me.btnsOpenFile.Enabled = True

                'is encrypted?
                If coFile.state = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
                    Me.btnsDecrypt.Enabled = True
                    Me.btnsEncrypt.Enabled = False
                    Me.btnsDecryptAndOpen.Enabled = True
                ElseIf coFile.state = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                    Me.btnsDecrypt.Enabled = False
                    Me.btnsEncrypt.Enabled = True
                    Me.btnsDecryptAndOpen.Enabled = False
                End If

                Me.btnsZip.Enabled = True

                Me.btnDeleteFile.Enabled = True
            End If
            'generic actions even if not existed or yes
            Me.btnsOpenFolder.Enabled = True 'allow user to check folder
            Me.btnsRemoveFromLibrary.Enabled = True 'allow user to remove from library



        End If
    End Sub


    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        Try
            Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
            Dim count As Integer = lvCol.Count

            If count = 1 Then
                Dim lviEx As ind.win.ListViewItemEx = DirectCast(lvCol(0), ind.win.ListViewItemEx)
                Dim coFile As ind.crypt.FileObject = lviEx.coFile

                If coFile.FileNotFound = True Then
                    'hide buttons
                    Me.mniOpenFile.Visible = False
                    Me.mniOpenFolder.Visible = True 'allow user to check folder

                    Me.tstSeparatorTopEncrypt.Visible = False
                    Me.mniEncrypt.Visible = False
                    Me.mniDecrypt.Visible = False
                    Me.mniDecryptAndOpen.Visible = False

                    Me.tstSeparatorTopZip.Visible = False
                    Me.mniZip.Visible = False

                    Me.mniFileNotFoundText.Visible = True
                    Me.mniFileNotFoundText.Text = ind.lang.ExceptionsLang.fileNotFound
                Else
                    'File existObj
                    Me.mniOpenFile.Visible = True
                    Me.mniOpenFolder.Visible = True

                    'is encrypted?
                    'is encrypted?
                    Me.tstSeparatorTopEncrypt.Visible = True
                    If coFile.state = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
                        Me.mniDecrypt.Visible = True
                        Me.mniEncrypt.Visible = False
                        Me.mniDecryptAndOpen.Visible = True
                    ElseIf coFile.state = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                        Me.mniDecrypt.Visible = False
                        Me.mniEncrypt.Visible = True
                        Me.mniDecryptAndOpen.Visible = False
                    End If

                    Me.tstSeparatorTopZip.Visible = True
                    Me.mniZip.Visible = True
                    Me.mniFileNotFoundText.Visible = False
                End If

            ElseIf count > 1 Then
                Me.mniOpenFile.Visible = True
                Me.mniOpenFolder.Visible = False
                Me.tstSeparatorTopEncrypt.Visible = True
                Me.mniDecrypt.Visible = True
                Me.mniEncrypt.Visible = True
                Me.mniDecryptAndOpen.Visible = True
                Me.tstSeparatorTopZip.Visible = True
                Me.mniZip.Visible = True
                Me.mniFileNotFoundText.Visible = False

            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub lv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTreeExp.DoubleClick
        Dim lv As ListView = DirectCast(sender, ListView)
        Dim lvi As ListView.SelectedListViewItemCollection = lv.SelectedItems

        Try
            If lvi.Count = 0 Then
                Exit Sub
            End If
            Dim strFileTo As String = lvi(0).SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text
            If IO.File.Exists(strFileTo) = False Then
                Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(ind.lang.UCCryptExplorerLang.EncryptDecryptItem_questionFileNotfound, "", ind.win.Msg.MessageIconEnum.document_delete)
                fd.OK_Button.Text = ind.lang.UCCryptExplorerLang.EncryptDecryptItem_questionFileNotfound_BtnDelete
                fd.Cancel_Button.Text = ind.lang.UCCryptExplorerLang.EncryptDecryptItem_questionFileNotfound_BtnNotDelete
                fd.setFocusOnBtn(frmDlg.FocusButtonEnum.cancelBtn)

                If fd.ShowDialog = DialogResult.OK Then
                    Dim lviEx As ind.win.ListViewItemEx = lvi(0)
                    Me.LviRemove(lviEx)

                    Exit Sub
                Else
                    'do not delete from library
                    Exit Sub
                End If
            End If

            Cursor = Cursors.WaitCursor

            If ind.crypt.FileObject.fileIsEncrypted(strFileTo) = True Then
                Me.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, DecryptOpenActionEnum.DecryptAndOpen)
            Else
                'open directly or show
                openFile(strFileTo)

                'add to recents
            End If
            'todo: when open a file in library...add to recents?


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lvTreeExp_MouseUp(sender As Object, e As MouseEventArgs) Handles lvTreeExp.MouseUp
        Dim lv As ListView = DirectCast(sender, ListView)
        'Checking the Mouse right Button
        If e.Button = MouseButtons.Right Then
            Dim SelectedItemsCount As Integer = lv.SelectedItems.Count
            Dim lvi As ListView.SelectedListViewItemCollection = lv.SelectedItems
            If SelectedItemsCount = 0 Then
                lv.ContextMenuStrip = Nothing
                Exit Sub
            Else
                lv.ContextMenuStrip = Me.ContextMenuStrip1
            End If
            If SelectedItemsCount = 1 Then
                'check if encrypted to avoid to show encrypt menu
                lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
            Else
                'show all
                lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
            End If

        End If
    End Sub

    Private Sub mniOpen_Click(sender As Object, e As EventArgs) Handles mniOpenFile.Click, btnsOpenFile.Click
        Try
            Dim SelectedItemsCount As Integer = Me.lvTreeExp.SelectedItems.Count
            Dim lviCol As ListView.SelectedListViewItemCollection = lvTreeExp.SelectedItems
            Dim errorList As New ind.tools.error.ErrorVisorHelper
            For Each lvi As ind.win.ListViewItemEx In lviCol
                Try
                    Dim strFileTo As String = lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text
                    If IO.File.Exists(strFileTo) = True Then
                        openFile(strFileTo)
                    Else
                        Dim eh As New ind.tools.error.ErrorHelper
                        eh.error_msg = ind.lang.ExceptionsLang.fileNotFound
                        eh.errType = ind.tools.error.ErrorHelper.ErrorTypeEnum.FileNotFoundException
                        If Not lvi Is Nothing Then
                            eh.fileObj = lvi.coFile
                        End If
                        errorList.addErrByErrHelper(eh)
                    End If
                Catch ex As Exception
                    Dim eh As New ind.tools.error.ErrorHelper
                    eh.error_msg = ind.lang.ExceptionsLang.fileNotFound
                    eh.errType = ind.tools.error.ErrorHelper.ErrorTypeEnum.FileNotFoundException
                    If Not lvi Is Nothing Then
                        eh.fileObj = lvi.coFile
                    End If

                    errorList.addErrByErrHelper(eh)
                End Try

            Next

            errorList.ShowVisor()

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub



    Private Sub mniEncrypt_Click(sender As Object, e As EventArgs) Handles mniEncrypt.Click, btnsEncrypt.Click
        Try
            Me.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Private Sub mniDecrypt_Click(sender As Object, e As EventArgs) Handles mniDecrypt.Click, btnsDecrypt.Click
        Try
            Me.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, DecryptOpenActionEnum.DecryptAndNoOpen)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Private Sub mniDecryptAndOpen_Click(sender As Object, e As EventArgs) Handles mniDecryptAndOpen.Click, btnsDecryptAndOpen.Click
        Try
            Me.EncryptOrDecryptSelectedItems(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, DecryptOpenActionEnum.DecryptAndOpen)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Private Sub mniRemove_Click(sender As Object, e As EventArgs) Handles mniRemove.Click, btnsRemoveFromLibrary.Click
        Me.removeListViewItem()
    End Sub


    Private Sub mniRemoveFromLibraries_Click(sender As Object, e As EventArgs) Handles mniRemoveFromLibraries.Click
        Me.removeListViewItem(True)
    End Sub



    Private Sub removeListViewItem(Optional removeFromLibraries As Boolean = False)
        Try
            Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
            If lvCol.Count > 0 Then
                Dim msg As String = ind.lang.UCCryptExplorerLang.ucCryptExp_mniRemove_msgdelete
                If lvCol.Count = 1 Then
                    Dim lvi As ind.win.ListViewItemEx = lvCol(0)
                    msg = msg & vbCr & lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.name).Text
                End If
                Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(msg, , ind.win.Msg.MessageIconEnum.document_delete, frmDlg.FocusButtonEnum.cancelBtn)
                If fd.ShowDialog() <> DialogResult.OK Then
                    Exit Sub
                End If
                'remove
                For Each lvi As ind.win.ListViewItemEx In lvCol
                    If removeFromLibraries Then
                        'todo: remove from other libraries directly
                        'we need to update the UI where the item is loaded in other libraries
                        ind.crypt.LibraryDataHelper.DeleteFile(lvi.coFile.id)
                    End If
                    Me.LviRemove(lvi)
                Next

            End If


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub mniDeleteFile_Click(sender As Object, e As EventArgs) Handles mniDeleteFile.Click, btnDeleteFile.Click
        deleteFile()
    End Sub

    Private Sub deleteFile(Optional removeFromLibraries As Boolean = False)
        Try
            Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
            If lvCol.Count > 0 Then
                Dim msg As String = ind.lang.UCCryptExplorerLang.mniDelete_msgdelete1
                If lvCol.Count = 1 Then
                    Dim lvi As ind.win.ListViewItemEx = lvCol(0)
                    msg = msg & vbCr & lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.name).Text
                Else

                End If
                Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(msg, , ind.win.Msg.MessageIconEnum.document_delete, frmDlg.FocusButtonEnum.cancelBtn)
                Dim dlgres As DialogResult = fd.ShowDialog

                If dlgres <> DialogResult.OK Then
                    Exit Sub
                End If
                'remove
                For Each lvi As ind.win.ListViewItemEx In lvCol
                    If removeFromLibraries Then
                        'todo: remove from other libraries directly
                        'we need to update the UI where the item is loaded in other libraries
                        ind.crypt.LibraryDataHelper.DeleteFile(lvi.coFile.id)
                    End If
                    Me.LviRemove(lvi)
                    IO.File.Delete(lvi.coFile.FullPath)
                Next

            End If


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub



    Private Sub mniFolderOpen_Click(sender As Object, e As EventArgs) Handles mniOpenFolder.Click, btnsOpenFolder.Click
        Try
            Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
            If lvCol.Count = 0 Then
                Exit Sub
            End If
            Dim lvi As ind.win.ListViewItemEx = lvCol(0)
            Dim strFileTo As String = lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text
            Dim fi As New FileInfo(strFileTo)
            Dim folderStr As String = fi.DirectoryName
            If IO.Directory.Exists(folderStr) = False Then
                Throw New Exception(ind.lang.ExceptionsLang.folderNotFound)
            End If
            Process.Start("explorer.exe", folderStr)

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub




#Region "Compress files"

    Private Sub mniZip_Click(sender As Object, e As EventArgs) Handles mniZip.Click, btnsZip.Click
        Me.compressFiles()

    End Sub

    ''' <summary>
    ''' compress files
    ''' </summary>
    ''' <param name="autoExec"></param>
    Public Sub compressFiles()

        Dim lvColSelected As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
        If lvColSelected.Count = 0 Then
            Exit Sub
        End If

        If IO.File.Exists("7z.exe") = False Then
            ind.win.Msg.mError("The 7z.exe app used to zip files is not included in folder. Please check that exists or download from original site http://www.7-zip.org/")
            Exit Sub
        End If

        Dim fd As frmZipFiles = New frmZipFiles
        Dim dr As DialogResult = fd.ShowDialog

        If dr <> DialogResult.OK Then
            Exit Sub
        End If
        Dim targetFile As String = fd.txtFilePath.TextBoxText

        Dim autoExec As Boolean = fd.chkAutoexec.Checked
        Dim openAfter As Boolean = fd.chkopenpath.Checked
        Dim saveWithoutExtension As Boolean = fd.chkSaveWithoutExtension.Checked

        'Dim targetFile As String = "test.zip"
        'Dim SaveFileDialog As New System.Windows.Forms.SaveFileDialog
        'Try
        '    'Setup the open dialog.
        '    SaveFileDialog.FileName = ""
        '    SaveFileDialog.Title = ind.lang.UCCryptExplorerLang.OpenDialog_CompressFileNew
        '    SaveFileDialog.RestoreDirectory = True
        '    SaveFileDialog.Filter = String.Format("{0} (*.*)|*.zip|*.rar|*.7z", ind.lang.UILang.frmMain_mniProjectNew_Click_openfilter)

        '    'Find out if the user chose a file.
        '    If SaveFileDialog.ShowDialog = DialogResult.OK Then
        '        targetFile = SaveFileDialog.FileName

        '    Else
        '        Exit Sub

        '    End If
        'Catch ex As Exception
        '    ind.win.Msg.mError(ex)
        '    Exit Sub
        'Finally
        '    SaveFileDialog.Dispose()
        'End Try

        Try

            Dim p As New ProcessStartInfo
            p.FileName = "7z.exe"

            Dim files As String = ""


            For Each lvi As ind.win.ListViewItemEx In lvColSelected
                Dim sourceFile As String = lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text

                files += " """ & sourceFile & """ "
            Next

#If DEBUG Then
            p.WindowStyle = ProcessWindowStyle.Normal
#Else
                p.WindowStyle = ProcessWindowStyle.Hidden
#End If
            If saveWithoutExtension = True Then
                If targetFile.EndsWith(".zip") Then
                    targetFile = Replace(targetFile, ".zip", "_zip")
                End If
            End If
            If autoExec = False Then
                p.Arguments = "a """ & targetFile & """ " & files
            Else
                If saveWithoutExtension = True Then
                    p.Arguments = "a -sfx7z.sfx """ & targetFile & "_exe" & """ " & files
                Else
                    p.Arguments = "a -sfx7z.sfx """ & targetFile & ".exe" & """ " & files
                End If

            End If

            Process.Start(p)
            If openAfter = True Then
                Dim fi As New FileInfo(targetFile)
                Dim folderStr As String = fi.DirectoryName
                Process.Start("explorer.exe", folderStr)
            Else
                ind.win.Msg.mInformation(String.Format(ind.lang.UCCryptExplorerLang.mniZip_Click_result, targetFile))
            End If


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally

        End Try
    End Sub
#End Region

#End Region

#Region "   encrypt/decrypt"

    Public Enum DecryptOpenActionEnum As Short
        None = -1
        ''' <summary>
        ''' Used as an option to indicate if app must open after decrypt
        ''' </summary>
        DecryptAndOpen = 0
        ''' <summary>
        ''' Used as an option to indicate if app must open after decrypt
        ''' </summary>
        DecryptAndNoOpen = 1
    End Enum
    ''' <summary>
    ''' encrypt or decrypt all files in library selected on listview
    ''' </summary>
    ''' <param name="encryptOrDecrypt">action to do</param>
    ''' <remarks></remarks>
    Public Sub EncryptOrDecryptAll(encryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum)
        Dim lvColSelected As ListView.ListViewItemCollection = Me.lvTreeExp.Items
        If encryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
            Me.EncryptOrDecryptItems(lvColSelected, encryptOrDecrypt, DecryptOpenActionEnum.DecryptAndNoOpen)
        Else
            Me.EncryptOrDecryptItems(lvColSelected, encryptOrDecrypt)
        End If

    End Sub

    ''' <summary>
    ''' Encrypt Or Decrypt Selected Items in listview
    ''' </summary>
    ''' <param name="encryptOrDecrypt">action to do</param>
    ''' <param name="decryptOpenActionSelected">id decrypt action choose to open after or not</param>
    ''' <remarks></remarks>
    Public Sub EncryptOrDecryptSelectedItems(encryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, Optional decryptOpenActionSelected As UCCryptExplorer.DecryptOpenActionEnum = DecryptOpenActionEnum.None)
        Dim lvColSelected As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
        Me.EncryptOrDecryptItems(lvColSelected, encryptOrDecrypt, decryptOpenActionSelected)
        'error must be cached in UI
    End Sub

    ''' <summary>
    ''' main method for encryption or decryption of selected items in the listview
    ''' </summary>
    ''' <param name="encryptOrDecrypt"></param>
    ''' <param name="decryptOpenActionSelected"></param>
    ''' <remarks></remarks>
    Public Sub EncryptOrDecryptItems(lvColSelected As ICollection, encryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, Optional decryptOpenActionSelected As UCCryptExplorer.DecryptOpenActionEnum = DecryptOpenActionEnum.None)

        If encryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
            Me.EncryptSelectedItems(lvColSelected)
        ElseIf encryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
            Me.DecryptSelectedItems(lvColSelected, decryptOpenActionSelected)
        End If
    End Sub

    ''' <summary>
    ''' Encrypt a list of items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EncryptSelectedItems(lvColSelected As ICollection)
        Me.ConfigureBackGroundWorkItemsToParams(lvColSelected, ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt, DecryptOpenActionEnum.DecryptAndNoOpen)

        Me.BackgroundWorker1.RunWorkerAsync(bgwp)

    End Sub

#Region "BackgroundWorker"
    Dim progress As UCCryptProgressB

    Private Sub ConfigureBackGroundWorkItemsToParams(lviTemsForOper As ICollection, encryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, openAfterDecrypt As DecryptOpenActionEnum)

        Me.bgwp = New BackGroundWorkItemsToParams
        bgwp.lvColSelected = lviTemsForOper
        bgwp.EncryptOrDecrypt = encryptOrDecrypt

        'load list of selected files in array list
        For Each lvi As ind.win.ListViewItemEx In Me.bgwp.lvColSelected
            Dim itemToEncrypt As New ItemBagCls
            itemToEncrypt.lviIndex = lvi.Index
            itemToEncrypt.lviOriginal = lvi
            itemToEncrypt.FilePathTo = lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text
            itemToEncrypt.co = lvi.coFile
            itemToEncrypt.openAfterDecrypt = openAfterDecrypt 'when encrypt we need to use this because the same method use this for decrypt
            itemToEncrypt.CryptoAction = encryptOrDecrypt
            Me.bgwp.lvSelCol.Add(itemToEncrypt)
        Next

        Me.ConfigureProgress(encryptOrDecrypt, bgwp.lvColSelected.Count)
        Me.lvTreeExp.Enabled = False
        Me.lvTreeExp.Cursor = Cursors.WaitCursor
    End Sub

    Private Sub ConfigureProgress(EncryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, maxItems As Integer)
        Me.progress = New UCCryptProgressB
        Me.progress.Parent = Me
        Me.progress.BringToFront()
        Me.progress.Location = New System.Drawing.Point(Me.Width - Me.progress.Width, Me.Height - Me.progress.Height)
        Me.progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        Me.progress.ProgressBar1.Maximum = maxItems
        Me.progress.ProgressBar1.Minimum = 0
        Me.progress.BackgroundWorker1 = Me.BackgroundWorker1
        If EncryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
            Me.progress.lbl_Title.Text = ind.lang.UCCryptProgressLang.lbl_Title_encrypt
        Else
            Me.progress.lbl_Title.Text = ind.lang.UCCryptProgressLang.lbl_Title_decrypt
        End If

        Me.progress.Show()
    End Sub

    Dim bgwp As BackGroundWorkItemsToParams

    Class BackGroundWorkItemsToParams
        Public EncryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum
        ''' <summary>
        ''' original selected items, do not use in threads for loop
        ''' </summary>
        ''' <remarks></remarks>
        Public lvColSelected As ICollection ' ListView.ListViewItemCollection
        Public errorVisor As New ind.tools.error.ErrorVisorHelper
        Public worker As System.ComponentModel.BackgroundWorker
        Public e As System.ComponentModel.DoWorkEventArgs
        ''' <summary>
        ''' arraylist with listviewitems selected to avoid cross-thread problems when loop SelectedListViewItemCollection
        ''' </summary>
        ''' <remarks></remarks>
        Public lvSelCol As New ArrayList
        ''' <summary>
        ''' for more than 5 files the opena
        ''' </summary>
        ''' <remarks></remarks>
        Public DecryptOpenAction As DecryptOpenActionEnum = DecryptOpenActionEnum.None
    End Class

    Class ItemBagCls
        Public lviIndex As Integer
        Public lviOriginal As ind.win.ListViewItemEx
        Public FilePathTo As String
        Public co As ind.crypt.FileObject
        Public openAfterDecrypt As DecryptOpenActionEnum = DecryptOpenActionEnum.DecryptAndNoOpen
        Public CryptoAction As ind.crypt.Crypto.CryptoActionEnum
        Public errorHappen As Boolean = False

    End Class
#End Region


    Private Sub EncryptDecryptSelectedItems_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim bgwp As BackGroundWorkItemsToParams = DirectCast(e.Argument, BackGroundWorkItemsToParams)
        Dim worker As System.ComponentModel.BackgroundWorker = CType(sender, System.ComponentModel.BackgroundWorker)
        bgwp.worker = worker
        bgwp.e = e
        e.Result = bgwp 'set result for final operation

        Dim iprogress As Integer = 0
        For Each itemBagData As ItemBagCls In bgwp.lvSelCol
            iprogress += 1
            Try
                Debug.WriteLine("Lvi crypt start")
                If bgwp.worker.CancellationPending Then
                    bgwp.e.Cancel = True
                    Exit For
                End If

                If bgwp.EncryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
                    Try
                        Dim result As Boolean = Me.FileEncrypt(itemBagData)
                        itemBagData.errorHappen = False
                    Catch ex As Exception
                        itemBagData.errorHappen = True
                        bgwp.errorVisor.addErrByLvITem(ex, itemBagData.lviOriginal)
                    End Try

                ElseIf bgwp.EncryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                    Try

                        Dim result As Boolean = Me.FileDecrypt(itemBagData)
                        itemBagData.errorHappen = False
                    Catch exFileNotDecryptedException As ind.exceptions.FileNotDecryptedException
                        itemBagData.errorHappen = True
                        itemBagData.openAfterDecrypt = DecryptOpenActionEnum.None
                        bgwp.errorVisor.addErrByLvITem(exFileNotDecryptedException, itemBagData.lviOriginal)

                    Catch ex As Exception
                        itemBagData.errorHappen = True
                        itemBagData.openAfterDecrypt = DecryptOpenActionEnum.None
                        bgwp.errorVisor.addErrByLvITem(ex, itemBagData.lviOriginal)
                    End Try
                End If
                'Thread.Sleep(1000)
                BackgroundWorker1.ReportProgress(iprogress, itemBagData) 'raise ProgressChanged event

                Debug.WriteLine("Lvi crypt end")
            Catch ex As Exception
                bgwp.errorVisor.addErrByException(ex)
            End Try
        Next

    End Sub

    ''' <summary>
    ''' Add to this method all the operations to make in UI thread
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        Dim itemBagData As ItemBagCls = DirectCast(e.UserState, ItemBagCls)
        If itemBagData.errorHappen = False Then
            'if there is an error no change was happen
            RaiseEvent itemChangeEvent(itemBagData)
            itemBagData.co.RaisefileStatusEvent()
            updateToolStripStatus(itemBagData)
        Else

        End If

        Me.progress.ProgressBar1.Value = e.ProgressPercentage
        Me.progress.lblFileInprogress.Text = itemBagData.co.FileNameWithoutExtension
    End Sub

    Private Sub EncryptSelectedItems_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            'Control because user can cancel at the same time (improbably but...)
            Me.progress.Hide()
            Me.progress.Dispose()
        Catch ex As Exception
            'nothing. 
        End Try
        Me.lvTreeExp.Cursor = Cursors.Default

        Debug.WriteLine("EncryptSelectedItems_RunWorkerCompleted")

        'Dim bgwp As BackGroundWorkItemsToParams = DirectCast(e.Result, BackGroundWorkItemsToParams) 'do not use in this way. When operation is cancelled the process send a nothing object in e.result

        If e.Error IsNot Nothing Then
            bgwp.errorVisor.addErrByMsg(e.Error.Message, ind.tools.error.ErrorHelper.ErrorTypeEnum.Other)
        ElseIf e.Cancelled Then
            bgwp.errorVisor.addErrByMsg(ind.lang.ExceptionsLang.OperantionWasCancelled, ind.tools.error.ErrorHelper.ErrorTypeEnum.OperationCancelledByUser)
        Else
            'ALL OK
        End If

        Me.lvTreeExp.Enabled = True
        Me.lvTreeExp.Focus()
        If bgwp.errorVisor.errorList.Count > 0 Then
            bgwp.errorVisor.ShowVisor()
        End If
        Me.bgwp = Nothing
        Me.displayToolbar()
        RaiseEvent lvItemSelectedEvent(Me.lvTreeExp.SelectedItems)
    End Sub
    ''' <summary>
    ''' Encrypt a file passed As ind.win.ListViewItemEx 
    ''' </summary>
    ''' <param name="lvi1"></param>
    ''' <remarks></remarks>
    Private Function FileEncrypt(itemToEncrypt As ItemBagCls) As Boolean

        Dim outputNewFileFullPathWithCryptExt As String = itemToEncrypt.FilePathTo & "." & ind.cte.AppConst.fileCryptExtension


        Dim status As checkFileStatusEnum = itemToEncrypt.co.checkFileStatusOnDisk()
        If status = checkFileStatusEnum.existEncrypted Then
            'file exist with diferent extension so we need to change 
            Dim Original_FileNamePathQualified As String = itemToEncrypt.co.FullPath
            Dim fileInfo As IO.FileInfo = New IO.FileInfo(outputNewFileFullPathWithCryptExt)
            itemToEncrypt.co.UpdateByFileInfo(fileInfo)
            ind.crypt.LibraryDataHelper.UpdateFileByFullPath(itemToEncrypt.co, Original_FileNamePathQualified)
            Return True

        ElseIf status = checkFileStatusEnum.existDecrypted Then
            'file existObj with diferent extension so we need to change 
        ElseIf status = checkFileStatusEnum.notExist Then
            'file not existObj, it may moved or deleted outside application. Mark to delete in listview
            Dim msgError As String = String.Format(ind.lang.UILang.FileNotFoundException, itemToEncrypt.FilePathTo)
            Throw New ind.exceptions.FileNotFoundException(msgError, itemToEncrypt.FilePathTo)
        End If

        'encription-------------
        Dim crypter As New ind.crypt.Crypto(ind.cte.AppVars.AppPassword)
        crypter.EncryptOrDecryptFileEX(itemToEncrypt.FilePathTo, outputNewFileFullPathWithCryptExt, ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
        'encription End-------------
        itemToEncrypt.co.state = ind.crypt.CryptoBase.CryptoStateEnum.IsEncripted

        itemToEncrypt.co.UpdateByFileInfo(New IO.FileInfo(outputNewFileFullPathWithCryptExt))
        ind.crypt.LibraryDataHelper.UpdateFileByFullPath(itemToEncrypt.co, itemToEncrypt.FilePathTo)

        Return True
    End Function
    ''' <summary>
    ''' Decrypt listviewitems selected in current listview
    ''' </summary>
    ''' <param name="DecryptOpenActionSelected"></param>
    ''' <remarks></remarks>
    Private Sub DecryptSelectedItems(lvColSelected As ICollection, DecryptOpenActionSelected As UCCryptExplorer.DecryptOpenActionEnum)

        Dim deativateAutomaticOpen As UCCryptExplorer.DecryptOpenActionEnum = UCCryptExplorer.DecryptOpenActionEnum.None
        Dim lvCol As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
        Dim lviCount As Integer = lvCol.Count
        If lviCount > 5 And (ind.settings.SettingsLink.chkOpenAfterDecrypt = True Or DecryptOpenActionSelected = DecryptOpenActionEnum.DecryptAndOpen) Then
            Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(String.Format(ind.lang.UILang.frmMain_tsbFileDecryptAndOpen_Click_msg, lviCount), "", ind.win.Msg.MessageIconEnum.document_warning)
            fd.Cancel_Button.Text = ind.lang.UILang.frmMain_tsbFileDecryptAndOpen_frmdlg_cancelButton
            If fd.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                deativateAutomaticOpen = UCCryptExplorer.DecryptOpenActionEnum.DecryptAndNoOpen
            End If
        End If

        Dim errorList As New ind.tools.error.ErrorVisorHelper

        Dim openAfterOperation As Boolean = ind.settings.SettingsLink.chkOpenAfterDecrypt

        If DecryptOpenActionSelected = DecryptOpenActionEnum.DecryptAndNoOpen Then
            openAfterOperation = False
        ElseIf DecryptOpenActionSelected = DecryptOpenActionEnum.DecryptAndOpen Then
            openAfterOperation = True
        End If

        If deativateAutomaticOpen = DecryptOpenActionEnum.DecryptAndNoOpen Then
            openAfterOperation = False
        End If


        Me.ConfigureBackGroundWorkItemsToParams(lvColSelected, ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, DecryptOpenActionEnum.DecryptAndNoOpen)
        If openAfterOperation = False Then
            Me.bgwp.DecryptOpenAction = DecryptOpenActionEnum.DecryptAndNoOpen
        ElseIf openAfterOperation = True Then
            Me.bgwp.DecryptOpenAction = DecryptOpenActionEnum.DecryptAndOpen
        End If

        Debug.WriteLine("EncryptSelectedItems RunWorkerAsync Start")
        Me.BackgroundWorker1.RunWorkerAsync(bgwp)
        Debug.WriteLine("EncryptSelectedItems RunWorkerAsync End")


    End Sub

    ''' <summary>
    ''' Decrypt a file passed As ind.win.ListViewItemEx 
    ''' </summary>
    ''' <param name="lvi1"></param>
    ''' <param name="openAfterDecrypt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FileDecrypt(itemToDecrypt As ItemBagCls) As Boolean

        Dim inputFilePathToDecrypt As String = itemToDecrypt.FilePathTo 'file to decrypt
        Dim fileNameWithoutExtension As String = IO.Path.GetFileNameWithoutExtension(inputFilePathToDecrypt)
        Dim fileName As String = IO.Path.GetFileName(inputFilePathToDecrypt)
        Dim fileFolderPath As String = IO.Path.GetDirectoryName(inputFilePathToDecrypt)

        Dim da As LibraryDataSetTableAdapters.T_FilesTableAdapter

        Dim status As checkFileStatusEnum = itemToDecrypt.co.checkFileStatusOnDisk()
        If status = checkFileStatusEnum.existEncrypted Then

        ElseIf status = checkFileStatusEnum.existDecrypted Then
            'if exists it can mean that it was decrypted by context menu, other library(no sync yet) or moved without saving library
            'update listview
            Dim filePathWithoutExtension As String = IO.Path.Combine(fileFolderPath, fileNameWithoutExtension)
            itemToDecrypt.co.UpdateByFileInfo(New IO.FileInfo(filePathWithoutExtension)) 'use this method to avoid loose reference in main collection
            itemToDecrypt.co.state = ind.crypt.CryptoBase.CryptoStateEnum.IsDecrypted
            'update path
            da = New LibraryDataSetTableAdapters.T_FilesTableAdapter
            da.UpdateFileByFullPath(filePathWithoutExtension, itemToDecrypt.co.length, itemToDecrypt.co.dateLastAccess, My.Computer.Name, False, itemToDecrypt.FilePathTo)
            Return True
        ElseIf status = checkFileStatusEnum.notExist Then
            'file not existObj, it may moved or deleted outside application. Mark to delete in listview
            Dim msgError As String = String.Format(ind.lang.UILang.FileNotFoundException, itemToDecrypt.FilePathTo)
            Throw New ind.exceptions.FileNotFoundException(msgError, itemToDecrypt.FilePathTo)
        End If

        'decrypt
        Dim outputNewFileName As String
        Dim fileExtension As String = IO.Path.GetExtension(inputFilePathToDecrypt)

        Dim outputFilePathFI As New IO.FileInfo(inputFilePathToDecrypt)

        If fileExtension = "." & ind.cte.AppConst.fileCryptExtension Then
            'has encrypt extension, so delete
            outputNewFileName = fileNameWithoutExtension
        Else
            'file do not has fileencrypted extension so do not try. 
            Return inputFilePathToDecrypt
            'todo: check if file is encripted based on first characters in next version. Problem: can not decrypt with other applications
            'outputNewFileName = fileName
        End If
        'new file decrypted result
        Dim outputNewFileFullPath As String = IO.Path.Combine(fileFolderPath, outputNewFileName)

        Dim crypter As New ind.crypt.Crypto(ind.cte.AppVars.AppPassword)
        crypter.EncryptOrDecryptFileEX(inputFilePathToDecrypt, outputNewFileFullPath, ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt)
        itemToDecrypt.co.state = ind.crypt.CryptoBase.CryptoStateEnum.IsDecrypted

        itemToDecrypt.co.UpdateByFileInfo(New IO.FileInfo(outputNewFileFullPath))
        'update path
        da = New LibraryDataSetTableAdapters.T_FilesTableAdapter
        da.UpdateFileByFullPath(outputNewFileFullPath, itemToDecrypt.co.length, itemToDecrypt.co.dateLastAccess, My.Computer.Name, False, itemToDecrypt.FilePathTo)

        Return True

    End Function


    Private Delegate Sub itemChangeEventInvoke(itemToEncrypt As ItemBagCls)
    Public Event itemChangeEvent(itemToEncrypt As ItemBagCls)
    Private itemChangeEventInvokeDo As New itemChangeEventInvoke(AddressOf updateListViewItem)
    ''' <summary>
    ''' Determines if DoItemUpdate should be called directly or via Invoke, and then calls it.
    ''' </summary>
    ''' <param name="sender">The CShItem of the Folder of the changed item.</param>
    ''' <param name="e">Contains information about the type of change and items affected.</param>
    ''' <remarks>Responds to events raised by either WM_Notify messages or FileWatch.</remarks>
    Private Sub ListViewItemUpdateInvoke(itemToEncrypt As ItemBagCls) Handles Me.itemChangeEvent
        If Me.InvokeRequired Then
            Invoke(itemChangeEventInvokeDo, itemToEncrypt)
        Else
            updateListViewItem(itemToEncrypt)
        End If
    End Sub

    Private Sub updateListViewItem(itemToEncrypt As ItemBagCls)
        If itemToEncrypt.errorHappen = False Then
            If itemToEncrypt.CryptoAction = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                itemToEncrypt.lviOriginal.StateImageIndex = -1
            Else
                itemToEncrypt.lviOriginal.StateImageIndex = 0
            End If
            ind.win.ListViewHelper.ListViewItemFillByObject(itemToEncrypt.lviOriginal, itemToEncrypt.co)

            If Me.bgwp.DecryptOpenAction = DecryptOpenActionEnum.DecryptAndOpen Then
                openFile(itemToEncrypt.co.FullPath)
            End If
        End If



    End Sub


#End Region

#Region "   Drag & Drop"
    Friend lastAddedItemCount As Integer = 0
    Private lvItemsToAddFromDragDropList As New ArrayList
    ''' <summary>
    ''' operations made when drag and drop
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lvTreeExp_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvTreeExp.DragDrop
        Try
            Cursor = Cursors.WaitCursor
            Me.lastAddedItemCount = 0

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                'todo: add validation for certain files like .doc .xls. pdf

                Dim DTInicio As DateTime = Date.Now

                Dim datos As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

                For Each fileFullName As String In datos
                    Me.AddToListFromDragDrop(fileFullName)
                Next

                Dim lvCountAdded As Integer = Me.lvItemsToAddFromDragDropList.Count

                If lvCountAdded > 0 Then
                    Me.lvTreeExp.BeginUpdate()
                    Dim items(Me.lvItemsToAddFromDragDropList.Count - 1) As ind.win.ListViewItemEx
                    Me.lvItemsToAddFromDragDropList.CopyTo(items)
                    Me.lvItemsToAddFromDragDropList.Clear()
                    Me.lvTreeExp.Items.AddRange(items)
                    Me.lvTreeExp.EndUpdate()
                End If

                saveAutomaticallyIfValid() 'if this library is an special library or have automatic save the this method is executed

                Debug.WriteLine("Time to load listview from drag&drop: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & Me.lvTreeExp.Items.Count & " elements")
                RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.addItem, lvCountAdded, Me.lvTreeExp.Items.Count)

            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then


            ElseIf e.Data.GetDataPresent("System.Windows.Forms.ListViewItem") Then

            Else

            End If
        Catch ex As Exception
            Cursor = Cursors.Default
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListBox1_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvTreeExp.DragEnter
        'TODO: hacer las comprobaciones de cuándo debe permitirse arrastrar y soltar
        e.Effect = DragDropEffects.Copy
        Me.assistant_btnClose_Click(Nothing, Nothing)
    End Sub

    'Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles Me.lv.ItemDrag
    '    ListView1.DoDragDrop(ListView1.SelectedItems, DragDropEffects.Copy)
    'End Sub

#End Region

#Region "List view add elements"
    ''' <summary>
    ''' used for events of operations in listview
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ListViewItemOperationEnum As Byte
        None = 0
        addItem = 1
        removeItem = 2
        loadingLibrary = 3
        savingLibrary = 4
    End Enum
    ''' <summary>
    ''' Configure item to library for encryption/decryption purposes
    ''' </summary>
    ''' <param name="cFile"></param>
    ''' <remarks></remarks>
    Public Function ConfigureLVIItem(cFile As ind.crypt.FileObject) As ind.win.ListViewItemEx

        'If Me.lvTreeExp.Items.ContainsKey(cFile.FullPath) = True Then
        '    Return Nothing
        'End If

        Dim lvi As ind.win.ListViewItemEx = ind.win.ListViewHelper.CreateLViEmpty

        ind.win.ListViewHelper.ListViewItemFillByObject(lvi, cFile)

        Dim shinfo As SHFILEINFO = GetShfileinfo(cFile.FileName)
        lvi.ImageIndex = shinfo.iIcon  'SystemImageListManager.GetIconIndex(, False)
        shinfo = Nothing

        Return lvi

    End Function

    Public Shared SHFileInfodwFlag As Integer = SHGFI.USEFILEATTRIBUTES Or
                    SHGFI.SYSICONINDEX Or
                    SHGFI.SMALLICON

    Public Shared Function GetShfileinfo(fullpath As String) As SHFILEINFO
        Dim shinfo As New SHFILEINFO()
        SHGetFileInfo(fullpath,
                                FILE_ATTRIBUTE_NORMAL,
                                shinfo,
                                cbFileInfo,
                                SHFileInfodwFlag)
        Return shinfo
    End Function

    Public Sub AddListViewItem(lvi As ind.win.ListViewItemEx)
        If Me.lvTreeExp.Items.ContainsKey(lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text) = False Then
            Me.lastAddedItemCount += 1
            Me.lvTreeExp.Items.Add(lvi)
        End If
    End Sub
    ''' <summary>
    ''' Add a file to the listview. If the file is a folder the application load all the files recursively
    ''' </summary>
    ''' <param name="file2Load"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub AddToListFromDragDrop(file2Load As String)

        If IO.File.Exists(file2Load) = True Then
            If Me.lvTreeExp.Items.ContainsKey(file2Load) = False Then
                Me.AddNewLviItemToLibraryInSession(file2Load)
            End If

        Else 'file do not existObj, check if it is a directory
            If IO.Directory.Exists(file2Load) Then
                Dim diParent As New IO.DirectoryInfo(file2Load)
                Me.DirToLoadFromDragDrop(diParent)
            End If
        End If

    End Sub
    ''' <summary>
    ''' Add a folder to the listview. The application load all the files recursively
    ''' </summary>
    ''' <param name="diParent"></param>
    ''' <remarks></remarks>
    Private Sub DirToLoadFromDragDrop(diParent As IO.DirectoryInfo)

        Dim fiList As IO.FileInfo() = diParent.GetFiles
        For Each fi As IO.FileInfo In fiList
            If Me.lvTreeExp.Items.ContainsKey(fi.FullName) = False Then
                Me.AddNewLviItemToLibraryInSession(fi.FullName)
            End If
        Next
        'all folders
        Dim diSubFolderList As IO.DirectoryInfo() = diParent.GetDirectories
        For Each diSubFolder As IO.DirectoryInfo In diSubFolderList
            Me.DirToLoadFromDragDrop(diSubFolder)
        Next
    End Sub

    ''' <summary>
    ''' add file to local library and to added library and then to global library if not exists
    ''' </summary>
    ''' <param name="file2Load"></param>
    ''' <remarks></remarks>
    Private Sub AddNewLviItemToLibraryInSession(file2Load As String)

        Dim cofi As ind.crypt.FileObject
        cofi = Me.LibraryFilesList(file2Load) 'check in library
        Dim fileExistInLibrary As Boolean = False

        If Not cofi Is Nothing Then
            'exists in this library so we don´t need to add, only check if file changed in filesystem by shell
            fileExistInLibrary = True

        ElseIf cofi Is Nothing Then
            'not exists
            fileExistInLibrary = False
            'look in global collection
            cofi = ind.crypt.OCGlobal.FileObjCollection.Item(file2Load)
            If Not cofi Is Nothing Then
                'cofi exists in global collection of files
                fileExistInLibrary = True

            ElseIf cofi Is Nothing Then 'create from file
                fileExistInLibrary = False
                cofi = New ind.crypt.FileObject(New IO.FileInfo(file2Load))
                ind.crypt.OCGlobal.FileObjCollection.AddDirect(cofi) 'add to global
            End If

            'add to actual library now
            Me.LibraryFilesAddedList.AddIfNotExist(cofi) 'add to collection of added files to know which items where added
            'TODO:save recents files now
        End If

        If fileExistInLibrary = True Then
            'check if the new item have encrypted extension
            If file2Load.EndsWith("." & ind.cte.AppConst.fileCryptExtension) Then
                'file is Encrypted in filesystem
                If cofi.state = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt Then
                    'actual state is decrypted, change state to encrypted
                    cofi.state = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt
                End If
            Else
                'file is decrypted in filesystem
                If cofi.state = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
                    'actual state is Encrypted, change state to decrypted
                    cofi.state = ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt
                End If
            End If
        End If


        Dim lviEx As ind.win.ListViewItemEx = Me.ConfigureLVIItem(cofi)
        Me.lvItemsToAddFromDragDropList.Add(lviEx) 'add to collection and then add in main method the lviEx

        'in any case remove from removed files
        Debug.WriteLine("AddNewLviItemToLibraryInSession   cofi.FullPath" & cofi.FullPath)
        Me.LibraryFilesRemovedList.Remove(cofi.FullPath)
    End Sub
    ''' <summary>
    ''' remove an item from the listview
    ''' </summary>
    ''' <param name="lvi"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LviRemove(lvi As ind.win.ListViewItemEx) As Boolean
        Me.LibraryFilesRemovedList.AddIfNotExist(lvi.coFile)
        Me.LibraryFilesAddedList.Remove(lvi.coFile.FullPath)
        lvi.Remove()
        saveAutomaticallyIfValid()
        Return True
    End Function

#End Region

#Region "Keyboard Events "
    ''' <summary>
    ''' Handles Delete Key processing for the ListView
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">a KeyEventsArgs</param>
    ''' <remarks>Modified version of Calum McLellan's code from ExpList.</remarks>
    Private Sub ExpList_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTreeExp.KeyUp
        If e.KeyCode = Keys.Delete Then
            Me.lvTreeExp.BeginUpdate()
            Dim i As Integer
            Dim elementToDelete As Integer = Me.lvTreeExp.SelectedItems.Count
            For i = elementToDelete - 1 To 0 Step -1
                Dim lviEx As ind.win.ListViewItemEx = Me.lvTreeExp.SelectedItems(i)
                Me.LviRemove(lviEx)

            Next
            Me.lvTreeExp.EndUpdate()
            saveAutomaticallyIfValid()
            RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.removeItem, elementToDelete, Me.lvTreeExp.Items.Count)
        End If
    End Sub

#End Region

#Region "Save Library"
    ''' <summary>
    ''' check if changes are made to library. Check if files are added or deleted
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkChanges() As Boolean
        Dim existChanges As Boolean = False

        If Me.LibraryFilesAddedList.Count > 0 Then
            Return True
        End If
        If Me.LibraryFilesRemovedList.Count > 0 Then
            Return True
        End If
        Return existChanges
    End Function

    ''' <summary>
    ''' Save workspace
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LibrarySaveWithCheck(Optional saveAsNewLibraryName As String = "") As Boolean
        If checkChanges() = False Then
            Return True
        Else
            Dim msg As String = ind.lang.UCCryptExplorerLang.library_Save_question
            Dim msg1 As String = String.Format(ind.lang.UCCryptExplorerLang.library_Save_questionTitle, Me.LibraryLoaded.LibraryName)
            Dim dr As DialogResult = ind.win.Msg.mQuestionDlg(msg, msg1, ind.win.Msg.MessageIconEnum.Warning, frmDlg.FocusButtonEnum.OkBtn).ShowDialog
            If dr <> DialogResult.OK Then
                Return True
            End If
        End If

        Return Me.LibrarySave(saveAsNewLibraryName)

    End Function

    ''' <summary>
    ''' save automatically the library if is recent files library or have automatic save enabled
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub saveAutomaticallyIfValid()
        Try
            If saveAutomatic = False Then
                Exit Sub
            End If

            Me.LibrarySave()

        Catch ex As Exception
            'todo: show error in view not messagebox
            ind.log.Logging.logExcep(ex)
        End Try

    End Sub
    Dim savecount As Integer = 0

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler ind.win.AppEventsClass.LibrarySaveEvent, AddressOf LibrarySaveEventHandler
    End Sub

    Public Sub LibrarySaveEventHandler(libraryToSave As LibraryDataSet.T_LibraryRow)
        Debug.WriteLine("LibrarySaveEventHandler: " & libraryToSave.id & libraryToSave.Name)
        If Me.LibraryLoaded.libraryID <> libraryToSave.id Then
            Debug.WriteLine("LibrarySaveEventHandler distintos: " & libraryToSave.id & libraryToSave.Name & "-" & Me.LibraryLoaded.libraryID)
            Exit Sub
        Else
            Debug.WriteLine("LibrarySaveEventHandler iguales: " & libraryToSave.id & libraryToSave.Name & "-" & Me.LibraryLoaded.libraryID)
            LibrarySave()
        End If
    End Sub

    ''' <summary>
    ''' Save workspace
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LibrarySave(Optional saveAsNewLibraryName As String = "") As Boolean

        If checkChanges() = False Then
                Return True
            End If
            Dim stopWatch As New ind.tools.MyTimer(True, "Saving library.")

            Dim lv As ListView = Me.lvTreeExp

            Dim libHelper As New ind.crypt.LibraryDataHelper

            Dim ListCnt As Integer = lv.Items.Count
            'new files
            If Me.LibraryFilesAddedList.Count > 0 Then
                Dim addedCount As Integer = Me.LibraryFilesAddedList.Count
                Dim addedCountProgress As Integer = 0

                Try
                    libHelper.StarTrans()
                    For Each coFile As ind.crypt.FileObject In Me.LibraryFilesAddedList.Values
                        libHelper.AddFileToLibrary(Me.LibraryLoaded.libraryID, coFile)
                        addedCountProgress += 1
                        'Me.tssFileCount.Text = String.Format("Adding {0} file of {1} to library", addedCountProgress, addedCount)
                    Next

                    libHelper.CommitTrans()
                Catch ex As Exception
                    libHelper.RollBackTrans()
                    Throw ex
                End Try

                'add added files to original collection or reload from ddbb
                For Each coFile As ind.crypt.FileObject In Me.LibraryFilesAddedList.Values
                    Me.LibraryFilesList.AddDirect(coFile)
                Next
                RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.savingLibrary, Me.LibraryFilesList.Count, Me.lvTreeExp.Items.Count)
                Me.LibraryFilesAddedList.Clear()
            End If

            'deleted files
            If Me.LibraryFilesRemovedList.Count > 0 Then
                Dim removedCount As Integer = Me.LibraryFilesRemovedList.Count
                Dim removedCountProgress As Integer = 0
                Dim da As New LibraryDataSetTableAdapters.T_Library_FilesTableAdapter
                Try
                    da.Connection.Open()
                    da.Transaction = da.Connection.BeginTransaction()
                    For Each coFile As ind.crypt.FileObject In Me.LibraryFilesRemovedList.Values
                        If coFile.id > 0 Then 'only original files are removed, new/removed do not existObj in DDBB yet
                            da.DeleteByLibraryIdAndFileId(Me.LibraryLoaded.libraryID, coFile.id)
                            'Me.tssFileCount.Text = String.Format("Removing {0} file of {1} to library", removedCountProgress, removedCount)
                        End If
                    Next
                    da.Transaction.Commit()
                    da.Connection.Close()
                Catch ex As Exception
                    da.Transaction.Rollback()
                    Throw ex
                Finally
                    If da.Connection.State = Data.ConnectionState.Open Then
                        da.Connection.Close()
                    End If
                End Try

                'remove from local list
                For Each coFile As ind.crypt.FileObject In Me.LibraryFilesRemovedList.Values
                    Me.LibraryFilesList.Remove(coFile.FullPath)
                Next

                Me.LibraryFilesRemovedList.Clear()
            End If


            'delete unused Files
            ind.crypt.LibraryDataHelper.DeleteUnusedFiles()
            stopWatch.StopGetResult(ind.tools.MyTimer.WatchOptionsEnum.stopWatch)

            Me.lvItems_InitialCount = ListCnt

            Debug.WriteLine(Me.LibraryLoaded.libraryID & " " & Me.LibraryLoaded.LibraryName & " saved")
            Return True


    End Function

    ''' <summary>
    ''' load project/library files
    ''' </summary>
    ''' <param name="libraryFilePath2Load"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LibraryLoad(libraryFilePath2Load As LibraryDataSet.T_LibraryRow) As Boolean

        Try
            Cursor = Cursors.WaitCursor
            Me.lvTreeExp.Items.Clear()
            Dim DTInicio As DateTime = Date.Now
            Dim lvItems As New ArrayList

            Me.LibraryLoaded = New ind.crypt.LibraryObj(libraryFilePath2Load.Name, libraryFilePath2Load.id)

            Dim libraryDs As New LibraryDataSet
            Dim dtFiles As LibraryDataSet.T_FilesDataTable
            Dim mda1 As New LibraryDataSetTableAdapters.T_FilesTableAdapter
            'get files for library
            dtFiles = mda1.GetDTByLibraryID(libraryFilePath2Load.id)

            For Each dr As LibraryDataSet.T_FilesRow In dtFiles.Rows
                Try
                    Dim isIncludedInGlobal As Boolean = False
                    'look in global cache
                    Dim cf As ind.crypt.FileObject = ind.crypt.OCGlobal.FileObjCollection.Item(dr.FileNamePathQualified)
                    If cf Is Nothing Then
                        'not existObj in global cache
                        cf = New ind.crypt.FileObject(id:=dr.id, name:=dr.Name, FileNamePathQualified:=dr.FileNamePathQualified, size:=dr.size, lastAccess:=dr.lastAccess, Computer:=dr.Computer)
                        isIncludedInGlobal = False
                    Else
                        isIncludedInGlobal = True
                    End If

                    lvItems.Add(Me.ConfigureLVIItem(cf)) 'create listviewitem but do not add in additem

                    Me.LibraryFilesList.AddDirect(cf)
                    If isIncludedInGlobal = False Then
                        ind.crypt.OCGlobal.FileObjCollection.Add(cf)
                    End If
                Catch ex As Exception
                    ind.log.Logging.logExcep(ex)
                End Try

            Next

            Debug.WriteLine("Time to load items from file: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & Me.lvTreeExp.Items.Count & " elements")

            Me.LibraryLoaded.itemsCount = lvItems.Count
            If Me.LibraryLoaded.itemsCount > 0 Then
                Dim items(Me.LibraryLoaded.itemsCount - 1) As ind.win.ListViewItemEx
                lvItems.CopyTo(items)
                Me.lvTreeExp.Items.AddRange(items) 'add to listview 
            End If

            Debug.WriteLine("Time to load listview from file: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & Me.lvTreeExp.Items.Count & " elements")
            Me.lvItems_InitialCount = lvItems.Count
            displayToolbar()
            RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.loadingLibrary, Me.lvItems_InitialCount, Me.lvTreeExp.Items.Count)
            RaiseEvent LibraryAfterLoadedEvent(Me.LibraryLoaded)
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try
    End Function


    ''' <summary>
    ''' save view in other library
    ''' </summary>
    ''' <param name="newlibraryId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LibrarySaveAs(newlibraryId As Integer, Optional onlySelected As Boolean = False) As Boolean

        Try

            Cursor = Cursors.WaitCursor
            Dim dtInicio As DateTime = Date.Now
            Dim lv As ListView = Me.lvTreeExp

            Dim listCnt As Integer = lv.Items.Count

            Dim addedCountProgress As Integer = 0
            Dim libHelper As New ind.crypt.LibraryDataHelper

            Dim lviItems As IEnumerable

            If onlySelected = True Then
                lviItems = Me.lvTreeExp.SelectedItems
            Else
                lviItems = Me.lvTreeExp.Items
            End If


            For Each lvi As ListViewItem In lviItems
                Dim lviEx As ind.win.ListViewItemEx = DirectCast(lvi, ind.win.ListViewItemEx)
                Dim coFile As ind.crypt.FileObject = lviEx.coFile
                libHelper.AddFileToLibrary(newlibraryId, coFile)
                addedCountProgress += 1
                Me.tssFileCount.Text = String.Format("Adding {0} file of {1} to library", addedCountProgress, listCnt)
            Next

            Debug.WriteLine("Time to save listview to File: " & Date.Now.Subtract(dtInicio).Duration().TotalMilliseconds & " for " & Me.lvTreeExp.Items.Count & " elements")

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Function

#End Region


#Region "   Shell files operations"
    ''' <summary>
    ''' laod a list of files than come from shell menu
    ''' </summary>
    ''' <param name="projectFilePath2Load"></param>
    ''' <remarks></remarks>
    Public Sub LoadListOfFilesFile(projectFilePath2Load As String)

        Try
            If IO.File.Exists(projectFilePath2Load) = False Then
                Throw New FileNotFoundException(ind.lang.UILang.FileNotFoundException)
            End If

            Dim DTInicio As DateTime = Date.Now
            Dim lvItems As New ArrayList
            Dim alFiles As New ArrayList


            Using strFile As New System.IO.StreamReader(projectFilePath2Load, True)
                Do While strFile.Peek() >= 0
                    Dim line As String = strFile.ReadLine()
                    alFiles.Add(line)
                Loop
            End Using
            Try
                IO.File.Delete(projectFilePath2Load)
            Catch ex As Exception
                ind.log.Logging.logExcep(ex)
            End Try


            For Each fileFullName As String In alFiles
                Me.AddToListFromDragDrop(fileFullName)
            Next

            Dim lvCountAdded As Integer = Me.lvItemsToAddFromDragDropList.Count

            If lvCountAdded > 0 Then
                Me.lvTreeExp.BeginUpdate()
                Dim items(Me.lvItemsToAddFromDragDropList.Count - 1) As ind.win.ListViewItemEx
                Me.lvItemsToAddFromDragDropList.CopyTo(items)
                Me.lvItemsToAddFromDragDropList.Clear()
                Me.lvTreeExp.Items.AddRange(items)
                Me.lvTreeExp.EndUpdate()
            End If

            Me.removeAssistant()

            Debug.WriteLine("Time to load listview from drag&drop: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & Me.lvTreeExp.Items.Count & " elements")
            RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.addItem, lvCountAdded, Me.lvTreeExp.Items.Count)

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    ''' <summary>
    ''' operations made when drag and drop
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub loadFile(fileFullName As String)
        Try
            Cursor = Cursors.WaitCursor
            Me.lastAddedItemCount = 0
            'todo: add validation for certain files like .doc .xls. pdf

            Dim DTInicio As DateTime = Date.Now

            Me.AddToListFromDragDrop(fileFullName)

            Dim lvCountAdded As Integer = Me.lvItemsToAddFromDragDropList.Count

            If lvCountAdded > 0 Then
                Me.lvTreeExp.BeginUpdate()
                Dim items(Me.lvItemsToAddFromDragDropList.Count - 1) As ind.win.ListViewItemEx
                Me.lvItemsToAddFromDragDropList.CopyTo(items)
                Me.lvItemsToAddFromDragDropList.Clear()
                Me.lvTreeExp.Items.AddRange(items)
                Me.lvTreeExp.EndUpdate()
            End If

            saveAutomaticallyIfValid() 'if this library is an special library or have automatic save the this method is executed

            Debug.WriteLine("Time to load listview from file: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & lvCountAdded & " elements")
            RaiseEvent LvItemAddedCountEvent(ListViewItemOperationEnum.addItem, lvCountAdded, Me.lvTreeExp.Items.Count)

        Catch ex As Exception
            Cursor = Cursors.Default
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub


#End Region


#Region "Language"
    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.chName.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_name
        Me.chPath.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_path
        Me.chType.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_type
        Me.chSize.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_size
        Me.chLastAccess.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_lastaccess
        Me.chLastOpenComputer.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_lv_openAt

        Me.mniOpenFile.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mnuopenfile
        Me.mniOpenFolder.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mniFolderOpen
        Me.mniEncrypt.Text = ind.lang.UILang.WindowsContextMenu_Encrypt
        Me.mniDecrypt.Text = ind.lang.UILang.WindowsContextMenu_Decrypt
        Me.mniDecryptAndOpen.Text = ind.lang.UILang.WindowsContextMenu_DecryptAndOpen
        Me.mniRemove.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mniRemove
        Me.mniRemove.ToolTipText = ind.lang.UCCryptExplorerLang.ucCryptExp_mniRemove_tt
        Me.mniDeleteFile.Text = ind.lang.UCCryptExplorerLang.mniDeleteFileText
        Me.mniDeleteFile.ToolTipText = ind.lang.UCCryptExplorerLang.mniDeleteFileToolTipText


        Me.btnsOpenFile.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mnuopenfile
        Me.btnsOpenFolder.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mniFolderOpen
        Me.btnsEncrypt.Text = ind.lang.UILang.WindowsContextMenu_Encrypt
        Me.btnsDecrypt.Text = ind.lang.UILang.WindowsContextMenu_Decrypt
        Me.btnsDecryptAndOpen.Text = ind.lang.UILang.WindowsContextMenu_DecryptAndOpen
        Me.btnsZip.Text = ind.lang.UCCryptExplorerLang.mniZip_Click
        Me.btnsRemoveFromLibrary.Text = ind.lang.UCCryptExplorerLang.ucCryptExp_mniRemove
        Me.btnsRemoveFromLibrary.ToolTipText = ind.lang.UCCryptExplorerLang.ucCryptExp_mniRemove_tt
        Me.btnDeleteFile.Text = ind.lang.UCCryptExplorerLang.mniDeleteFileText
        Me.btnDeleteFile.ToolTipText = ind.lang.UCCryptExplorerLang.mniDeleteFileToolTipText

        Me.lblListViewItemCount.Text = String.Format(ind.lang.UCCryptExplorerLang.UCExplorer_lblListViewItemCount, Me.lvTreeExp.Items.Count)
    End Sub



#End Region

    ''' <summary>
    ''' open the file selected by user
    ''' </summary>
    ''' <param name="strFileTo"></param>
    Private Sub openFile(strFileTo As String)
        If ind.tools.FileDataHelper.HasImageExtension(strFileTo) = False Then
            ind.tools.FileDataHelper.openFileBySystemProcess(strFileTo)
        Else
            Dim f As New frmImage
            f.showImage(strFileTo)
        End If
    End Sub


End Class
