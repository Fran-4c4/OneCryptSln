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


Public Class UCTreeExplorer

    ''' <summary>
    ''' Explorer tree
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents expTree As ExpTree


#Region "   Listview inicialization Vars"
    'avoid Globalization problem-- an empty timevalue
    Dim EmptyTimeValue As New DateTime(1, 1, 1, 0, 0, 0)

    Private LastSelectedCSI As CShItem

    Private dragWrapper As CDragWrapper              'wrapper for Drag ops originating in lv1
    Private dropWrapper As ClvDropWrapper      'wrapper for Drop ops targeting lv1

    Private m_CreateNew As Boolean = False  'Flag for NewMenu processing of "New" item
#End Region

#Region "Enums"
    ''' <summary>
    ''' listview subitems positions. This is only for <see cref=" UCTreeExplorer">UCTreeExplorer</see> listview. For listview of crypted items see <see cref="UCCryptExplorer.lvTreeExp">UCCryptExplorer</see>
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum lvFieldEnum As Byte
        name = 0
        ''' <summary>
        ''' file Full path
        ''' </summary>

        type = 1
        Length = 2
        path = 3
        datemodified = 4
        dateCreated = 5
    End Enum
#End Region


#Region "   Public Properties"
    ''' <summary>
    ''' InitialLoadLimit is a the number of lv1.Items whose IconIndex will we fetched on initial load
    ''' the balance will be fetched AFTER lv1 shows its initial display
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), Category("Misc"), _
    Description("Maximum # of Items to build in GUI Thread for initial display"), _
    DefaultValue(32)> _
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
    <Browsable(True), Category("Misc"), _
    Description("Maximum # of Items in each Background update interval"), _
    DefaultValue(100)> _
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

#Region "   Form Load/VisibleChanged, lv1 HandleCreated"
    Private Sub UCTreeExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TODO: Setup Drag and Drop Wrappers
        dragWrapper = New CDragWrapper(lvTreeExp)
        dropWrapper = New ClvDropWrapper(lvTreeExp)
        'Setup Change Notification	7/1/2012
        'AddHandler CShItemUpdate, AddressOf UpdateInvoke        '7/1/2012
    End Sub

    Private Sub lvTreeExp_HandleCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTreeExp.HandleCreated
        SystemImageListManager.SetListViewImageList(lvTreeExp, False, False)
        SystemImageListManager.SetListViewImageList(lvTreeExp, True, False)
    End Sub

    Private Sub control_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        SystemImageListManager.SetListViewImageList(lvTreeExp, False, False)
        SystemImageListManager.SetListViewImageList(lvTreeExp, True, False)
    End Sub

#End Region

#Region "   ExplorerTree Event Handling -- AfterNodeSelect"

    Private Sub ExpTree1_ExpTreeNodeSelected(pathName As String, CSI As ExpTreeLib.CShItem) Handles expTree.ExpTreeNodeSelected
        If LastSelectedCSI IsNot Nothing AndAlso LastSelectedCSI Is CSI Then Exit Sub
        Cursor = Cursors.WaitCursor

        If BGW2 IsNot Nothing Then
            BGW2.CancelAsync()
            Event2.WaitOne()
        End If
        Dim TotalItems As Integer
        Dim combList As ArrayList = CSI.GetItems

        TotalItems = combList.Count
        If TotalItems > 0 Then
            'Build the ListViewItems & add to lv1
            lvTreeExp.BeginUpdate()
            lvTreeExp.Items.Clear()
            If LastSelectedCSI IsNot Nothing AndAlso LastSelectedCSI IsNot CSI Then
                LastSelectedCSI.ClearItems(True)
            End If
            lvTreeExp.Refresh()

            Dim InitialFillLim As Integer = Math.Min(combList.Count, InitialLoadLimit)
            Dim FirstLoad As New List(Of ListViewItem)(InitialFillLim)
            For i As Integer = 0 To InitialFillLim - 1
                Dim lvi As ListViewItem = MakeSparceLVItem(combList(i))
                RefreshLvi(lvi)
                lvi.ImageIndex = SystemImageListManager.GetIconIndex(combList(i), False)
                FirstLoad.Add(lvi)
            Next
            lvTreeExp.Items.AddRange(FirstLoad.ToArray)

            'Fill the ListView with the remaining items without FileInfo or ICon
            Dim SparseLoad As New List(Of ListViewItem)(combList.Count - InitialFillLim)
            If combList.Count > InitialFillLim Then
                For i As Integer = InitialFillLim To combList.Count - 1
                    Dim lvi As ListViewItem = MakeSparceLVItem(combList(i))
                    RefreshLvi(lvi, True)
                    SparseLoad.Add(lvi)
                Next
                lvTreeExp.Items.AddRange(SparseLoad.ToArray)
            End If
            lvTreeExp.EndUpdate()

            If combList.Count > InitialLoadLimit Then
                LoadLV1(SparseLoad)
            End If
        Else
            lvTreeExp.Items.Clear()
            If LastSelectedCSI IsNot Nothing AndAlso LastSelectedCSI IsNot CSI Then
                LastSelectedCSI.ClearItems(True)
            End If
        End If
        LastSelectedCSI = CSI
        lvTreeExp.Tag = LastSelectedCSI           '7/5/2012   For ClvDropWapper

        'Now that lv.ListViewItems has been set up (and MakeLvItem does attach the appropriate tags
        ' to both the ListViewItem and the appropriate SubItems), set the ListViewItemSorter
        lvTreeExp.ListViewItemSorter = New LVColSorter(lvTreeExp)
        Cursor = Cursors.Default
    End Sub

#End Region

#Region "   Various MakeLV routines"

    ''' <summary>
    ''' Creates a minimal ListViewItem from the input CShItem
    ''' </summary>
    ''' <param name="item">The CShItem that this ListViewItem represents</param>
    ''' <returns>A ListViewItem with .Tag and .Text filled, with empty SubItems for the balance of the SubItems required by the ListView.</returns>
    ''' <remarks>When it is timely to fill the remaining SubItems with data (after BGW2 has completed), call RefreshLvi.</remarks>
    Private Function MakeSparceLVItem(ByVal item As CShItem) As ListViewItem
        Dim lvi As New ListViewItem(item.DisplayName)
        With lvi
            .Tag = item
            For i As Integer = 1 To lvTreeExp.Columns.Count - 1
                .SubItems.Add("")
            Next
        End With
        Return lvi
    End Function

    ''' <summary>
    ''' Loads all of a ListViewItem's SubItems with values from the associated CShItem - obtained from the ListViewItem's .Tag.
    ''' Note that the CShItem's time sensitive values will be set by CShItem using a W32_FindData structure if it finds one in the
    ''' CShItem's .Tag - as set in this Form by a BackgroundWorker.
    ''' </summary>
    ''' <param name="lvi">The ListViewItem to be refreshed</param>
    ''' <param name="DeferSet">If True, Defer the filling of Length and Date information until later (in the BackgroundWorker).
    '''                        If False (the default) fill Length and Date information in this call.</param>
    ''' <remarks>For optimization, depends on BGW2 having filled CSI.W32Data with a W32Find_Data structure which CShItem will 
    '''          use for Length, Attributes, and Date information.</remarks>
    Private Sub RefreshLvi(ByVal lvi As ListViewItem, Optional ByVal DeferSet As Boolean = False)
        Dim CSI As CShItem = lvi.Tag
        'Set the Items that must come from a CShItem
        'lvi.Text = CSI.Name
        lvi.SubItems(UCTreeExplorer.lvFieldEnum.name).Text = CSI.Name
        lvi.SubItems(UCTreeExplorer.lvFieldEnum.type).Text = CSI.TypeName

        If Not DeferSet Then    'Set the SubItems that may come from an W32Find_Data
            With lvi
                'Set path
                If CSI.IsDisk OrElse (CSI.IsFileSystem OrElse Not CSI.IsFolder) Then      'Not CSI.IsDisk And
                    .SubItems(UCTreeExplorer.lvFieldEnum.path).Text = CSI.Path
                Else
                    '.SubItems(me.lvFieldEnum.path).Text = CSI.Path
                End If

                'Set Length
                If CSI.IsDisk OrElse (CSI.IsFileSystem And Not CSI.IsFolder) Then      'Not CSI.IsDisk And
                    If CSI.Length > 1024 Then
                        .SubItems(UCTreeExplorer.lvFieldEnum.Length).Text = (Format(CSI.Length / 1024, "#,### KB"))
                    Else
                        .SubItems(UCTreeExplorer.lvFieldEnum.Length).Text = (Format(CSI.Length, "##0 Bytes"))
                    End If
                    .SubItems(UCTreeExplorer.lvFieldEnum.Length).Tag = CSI.Length
                Else
                    '.SubItems(me.lvFieldEnum.size) already has been correctly set to blank entry
                    'But, to make LVColSorter work correctly, then we have to Set the .Tag to 0 (really)
                    .SubItems(UCTreeExplorer.lvFieldEnum.Length).Tag = 0L
                End If
                'Set LastWriteTime
                If CSI.IsDisk OrElse CSI.LastWriteTime = EmptyTimeValue Then '"#1/1/0001 12:00:00 AM#" is empty
                    '.SubItems(me.lvFieldEnum.datemodified) already has been correctly set to blank entry
                    'But, to make LVColSorter work correctly, then we have to Set the .Tag to EmptyTimeValue
                    ' (Not really in this case, but it is good to do in the general case)
                    .SubItems(UCTreeExplorer.lvFieldEnum.datemodified).Tag = EmptyTimeValue
                Else
                    .SubItems(UCTreeExplorer.lvFieldEnum.datemodified).Text = CSI.LastWriteTime.ToString("MM/dd/yyyy HH:mm:ss")   '01/01/2014
                    .SubItems(UCTreeExplorer.lvFieldEnum.datemodified).Tag = CSI.LastWriteTime
                End If
                'Set Attributes
                'If Not CSI.IsDisk And CSI.IsFileSystem Then
                '    Dim SB As New StringBuilder()
                '    Try
                '        Dim attr As FileAttributes = CSI.Attributes
                '        If (attr And FileAttributes.System) = FileAttributes.System Then SB.Append("S")
                '        If (attr And FileAttributes.Hidden) = FileAttributes.Hidden Then SB.Append("H")
                '        If (attr And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then SB.Append("R")
                '        If (attr And FileAttributes.Archive) = FileAttributes.Archive Then SB.Append("A")
                '    Catch
                '    End Try
                '    .SubItems(me.lvFieldEnum.attributes).Text = SB.ToString
                'Else
                '    '.SubItems(me.lvFieldEnum.attributes) already has been correctly set to blank entry
                '    'But, to make LVColSorter work correctly, then we have to Set the .Tag to EmptyTimeValue
                '    ' (Not really in this case, but it is good to do in the general case)
                '    .SubItems(me.lvFieldEnum.attributes).Tag = EmptyTimeValue
                'End If
                'Set CreationTime
                If CSI.IsDisk OrElse CSI.CreationTime = EmptyTimeValue Then '"#1/1/0001 12:00:00 AM#" is empty
                    '.SubItems(me.lvFieldEnum.dateCreated) already has been correctly set to blank entry
                    'But, to make LVColSorter work correctly, then we have to Set the .Tag to EmptyTimeValue
                    ' (Not really in this case, but it is good to do in the general case)
                    .SubItems(UCTreeExplorer.lvFieldEnum.dateCreated).Tag = EmptyTimeValue
                Else
                    .SubItems(UCTreeExplorer.lvFieldEnum.dateCreated).Text = CSI.CreationTime.ToString("MM/dd/yyyy HH:mm:ss")   '01/01/2014
                    .SubItems(UCTreeExplorer.lvFieldEnum.dateCreated).Tag = CSI.CreationTime
                End If
            End With
        End If
    End Sub

#End Region

#Region "   The Background worker"
    Private WithEvents BGW2 As BackgroundWorker
    Private Event2 As New ManualResetEvent(True)
    Private InBkground As Integer = 0
    Private ItemInfo As Dictionary(Of String, W32Find_Data)
    Private Sub LoadLV1(ByVal ListToDo As List(Of ListViewItem))
        If ListToDo IsNot Nothing AndAlso ListToDo.Count > 0 Then
            Event2.Reset()
            BGW2 = New BackgroundWorker
            'tsslLeft.Text = "Loading Info"
            With BGW2
                .WorkerReportsProgress = True
                .WorkerSupportsCancellation = True
                InBkground = 0
                .RunWorkerAsync(ListToDo)
            End With
        End If
    End Sub

    '7/8/2012 - Routine modified to correct a potential deadlock
    Private Sub BGW2_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW2.DoWork
        Dim ThisWorker As BackgroundWorker = sender
        Dim WorkList As List(Of ListViewItem) = e.Argument
        Dim Results As New List(Of ListViewItem)(WorkUpdateInterval)
        Dim Item As CShItem = DirectCast(WorkList(0).Tag, CShItem).Parent
        If Item.IsFileSystem AndAlso WorkList.Count > 0 Then
            GetItemDatas(Item, WorkList)
        End If
        For i As Integer = 0 To WorkList.Count - 1
            If ThisWorker.CancellationPending Then
                e.Cancel = True : Results = Nothing
                Exit For
            End If
            Item = WorkList(i).Tag
            'Force fetch of IconIndex 
            Dim tmp As Integer = Item.IconIndexNormal
            Results.Add(WorkList(i))
            If Results.Count = WorkUpdateInterval Then
                If ThisWorker.CancellationPending Then
                    e.Cancel = True : Results = Nothing
                    Exit For
                End If
                ThisWorker.ReportProgress(i, Results)
                Results = New List(Of ListViewItem)(WorkUpdateInterval)
            End If
        Next
        Event2.Set()
        e.Result = Results
    End Sub

    Private Sub BGW2_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGW2.ProgressChanged
        Dim Items As List(Of ListViewItem) = e.UserState
        For Each Lvi As ListViewItem In Items
            SetLvi(Lvi)
            InBkground += 1
        Next
    End Sub

    Private Sub BGW2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW2.RunWorkerCompleted
        If Not e.Cancelled AndAlso e.Error Is Nothing Then
            If e.Result IsNot Nothing Then
                Dim Items As List(Of ListViewItem) = e.Result
                For Each Lvi As ListViewItem In Items
                    SetLvi(Lvi)
                    InBkground += 1
                Next
            End If
        End If
        Event2.Set()
        BGW2.Dispose()          '7/8/2012
        BGW2 = Nothing
    End Sub

    Private Sub GetItemDatas(ByVal BaseCSI As CShItem, ByVal LVIList As List(Of ListViewItem))
        ItemInfo = New Dictionary(Of String, W32Find_Data)(LVIList.Count)
        Try
            GetInfos(BaseCSI)
        Catch ex As ApplicationException    'only occurs on Invalid Handle - we have nothing to do
            Exit Sub
        End Try
        For Each Lvi As ListViewItem In LVIList
            Dim CSI As CShItem = Lvi.Tag
            Dim csiName As String = IO.Path.GetFileName(CSI.Path)
            If CSI.IsFolder Then
                csiName = csiName & "\"
            End If
            If ItemInfo.ContainsKey(csiName) Then
                CSI.W32Data = ItemInfo(csiName)
            Else
#If DEBUG Then
                Throw New ArgumentException("No ItemData for " & CSI.Path)
#End If
            End If
        Next
    End Sub

    Private Sub GetInfos(ByVal CSI As CShItem)

        Dim DirName As String = CSI.Path
        Dim Data As New W32Find_Data(DirName)

        Dim Handle As SafeFindHandle = Nothing

        Handle = FindFirstFile(DirName & "\*", Data)
        If Handle.IsInvalid Then
            Debug.WriteLine("Invalid Handle for " & CSI.Path)
            Throw New ApplicationException("Invalid FindFileHandle returned for " & DirName)
        End If
        Dim HR As Boolean = True
        While (HR)
            If (Data.dwFileAttributes And FileAttributes.Directory) <> 0 Then
                If Not Data.cFileName.StartsWith(".") Then
                    ItemInfo.Add(Data.Name & "\", Data)
                End If
            Else
                ItemInfo.Add(Data.Name, Data)
            End If
            Data = New W32Find_Data(DirName)
            HR = FindNextFile(Handle, Data)
        End While
        Handle.Close()
    End Sub

    Private Sub SetLvi(ByVal Lvi As ListViewItem)
        Dim CSI As CShItem = Lvi.Tag
        Lvi.ImageIndex = CSI.IconIndexNormal
        RefreshLvi(Lvi)
    End Sub

#End Region

#Region "   Dynamic Update Handler"
    ''' <summary>
    ''' To receive notification of changes to the FileSystem which may affect the GUI display, declare
    ''' DeskTopItem WithEvents. Changes to CShItem's internal tree which are caused by notification of 
    ''' FileSystem changes or by a refresh of the contents of the internal tree raise CShItemUpdate
    ''' events.  For possible future changes, we check to see if an Invoke is required or not.
    ''' </summary>
    ''' <remarks></remarks>
    Private Delegate Sub InvokeUpdate(ByVal sender As Object, ByVal e As ShellItemUpdateEventArgs)

    'Private WithEvents DeskTopItem As CShItem = CShItem.GetDeskTop '7/1/2012

    Private m_InvokeUpdate As New InvokeUpdate(AddressOf DoItemUpdate)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler CShItemUpdate, AddressOf UpdateInvoke        '7/1/2012
    End Sub

    ''' <summary>
    ''' Returns the last CShItem Selected.
    ''' </summary>
    Public ReadOnly Property SelectedItem() As CShItem
        Get
            Return LastSelectedCSI
        End Get
    End Property
    ''' <summary>
    ''' Determines if DoItemUpdate should be called directly or via Invoke, and then calls it.
    ''' </summary>
    ''' <param name="sender">The CShItem of the Folder of the changed item.</param>
    ''' <param name="e">Contains information about the type of change and items affected.</param>
    ''' <remarks>Responds to events raised by either WM_Notify messages or FileWatch.</remarks>
    Private Sub UpdateInvoke(ByVal sender As Object, ByVal e As ShellItemUpdateEventArgs) '7/1/2012 removed Handles clause
        If Me.InvokeRequired Then
            Invoke(m_InvokeUpdate, sender, e)
        Else
            DoItemUpdate(sender, e)
        End If
    End Sub
    ''' <summary>
    ''' Makes changes in lv1 GUI in response to updating events raised by CShItem.
    ''' </summary>
    ''' <param name="sender">The CShItem of the Folder of the changed item.</param>
    ''' <param name="e">Contains information about the type of change and items affected.</param>
    ''' <remarks>Responds to events raised by WM_Notify messages. </remarks>
    Private Sub DoItemUpdate(ByVal sender As Object, ByVal e As ShellItemUpdateEventArgs)
        Dim Parent As CShItem = DirectCast(sender, CShItem)
        If Parent Is LastSelectedCSI Then ' 6/11/2012 - OrElse (e.Item Is LastSelectedCSI AndAlso e.UpdateType = CShItemUpdateType.Updated) Then   'If not, then of no interest to us
            Try
                lvTreeExp.BeginUpdate()
                Select Case e.UpdateType
                    Case CShItem.CShItemUpdateType.Created
                        Dim lvi As ListViewItem = MakeSparceLVItem(e.Item)
                        lvi.ImageIndex = e.Item.IconIndexNormal
                        RefreshLvi(lvi)
                        InsertLvi(lvi, lvTreeExp)     '6/11/2012
                        If m_CreateNew Then
                            m_CreateNew = False
                            lvi.BeginEdit()
                        End If
                    Case CShItemUpdateType.Deleted
                        Dim lvi As ListViewItem = FindLVItem(e.Item)
                        If lvi IsNot Nothing Then
                            lvTreeExp.Items.Remove(lvi)
                        End If
                    Case CShItemUpdateType.Renamed
                        Dim lvi As ListViewItem = FindLVItem(e.Item)
                        If lvi IsNot Nothing Then
                            If e.Item.Parent IsNot LastSelectedCSI Then     'if true = item renamed to different directory
                                lvTreeExp.Items.Remove(lvi)
                            Else
                                RefreshLvi(lvi)
                                e.Item.ResetIconIndex()                       'may have changed
                                lvi.ImageIndex = e.Item.IconIndexNormal
                                lvTreeExp.Items.Remove(lvi)   '6/11/2012
                                InsertLvi(lvi, lvTreeExp)     '6/11/2012
                            End If
                        End If
                    Case CShItemUpdateType.UpdateDir  'in this case Parent/sender is the item of interest
                        ' CShItemUpdater, etc. will do the appropriate Adds and Removes, generating
                        ' Created/Deleted events that will occur before an UpdateDir event. There is
                        ' no need to do anything here.
                        'lv1.BeginUpdate()
                        'lv1.Sort()
                        'lv1.EndUpdate()

                    Case CShItemUpdateType.Updated
                        Dim lvi As ListViewItem = FindLVItem(e.Item)
                        If lvi IsNot Nothing Then
                            Dim indx As Integer = lvTreeExp.Items.IndexOf(lvi)
                            Dim newLVI As ListViewItem = MakeSparceLVItem(e.Item)
                            RefreshLvi(newLVI)
                            e.Item.ResetIconIndex()                       'may have changed
                            newLVI.ImageIndex = e.Item.IconIndexNormal
                            lvTreeExp.Items.RemoveAt(indx)
                            lvTreeExp.Items.Insert(indx, newLVI)
                        End If
                    Case CShItemUpdateType.IconChange
                        Dim lvi As ListViewItem = FindLVItem(e.Item)
                        If lvi IsNot Nothing Then
                            e.Item.ResetIconIndex()
                            lvi.ImageIndex = e.Item.IconIndexNormal
                        End If
                    Case CShItemUpdateType.MediaChange
                        Dim lvi As ListViewItem = FindLVItem(e.Item)
                        If lvi IsNot Nothing Then
                            RefreshLvi(lvi)
                            e.Item.ResetIconIndex()
                            lvi.ImageIndex = e.Item.IconIndexNormal
                        End If
                End Select

            Catch ex As Exception
                Debug.WriteLine("Error in frmThread -- lv1 updater -- " & ex.ToString)
            Finally
                lvTreeExp.EndUpdate()
            End Try

        End If      'end of Parent Is LastSelectedCSI test
    End Sub

    Private Function FindLVItem(ByVal item As CShItem) As ListViewItem
        For Each lvi As ListViewItem In lvTreeExp.Items
            If lvi.Tag Is item Then
                Return lvi
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Given a ListViewItem with a  CShItem in its' Tag, and a ListView whose Items all have a CShItem in
    ''' their Tags, Insert the ListViewItem in its' proper place in the ListView.
    ''' </summary>
    ''' <param name="lvi">The ListViewItem to be inserted.</param>
    ''' <param name="LV">The ListView into which the ListViewItem is to be inserted.</param>
    ''' <remarks>6/11/2012 - better than a Sort when the list is in order.<br />
    '''          Will honor any prior Column Sorts.</remarks>
    Private Sub InsertLvi(ByVal lvi As ListViewItem, ByVal LV As ListView)
        Dim Item As CShItem = lvi.Tag
        If lvTreeExp.ListViewItemSorter IsNot Nothing AndAlso TypeOf lvTreeExp.ListViewItemSorter Is LVColSorter Then
            Dim CSorter As LVColSorter = DirectCast(lvTreeExp.ListViewItemSorter, LVColSorter)
            For i As Integer = 0 To LV.Items.Count - 1
                If CSorter.Compare(LV.Items(i), lvi) > 0 Then
                    LV.Items.Insert(i, lvi)
                    lvi.EnsureVisible()
                    Exit Sub
                End If
            Next
        Else
            For i As Integer = 0 To LV.Items.Count - 1
                If DirectCast(LV.Items(i).Tag, CShItem).CompareTo(Item) > 0 Then
                    LV.Items.Insert(i, lvi)
                    lvi.EnsureVisible()
                    Exit Sub
                End If
            Next
        End If
        'on fall thru, did not find a spot to insert, so it goes at then end
        LV.Items.Add(lvi)
        lvi.EnsureVisible()
    End Sub

#End Region

#Region "   Listview_DoubleClick"

    Private Sub lv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTreeExp.DoubleClick
        Dim csi As CShItem = lvTreeExp.SelectedItems(0).Tag
        If csi.IsFolder Then
            Me.expTree.ExpandANode(csi)
        Else
            Try
                'TODO: check if file is encrypted 
                Process.Start(csi.Path)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, "Error in starting application")
            End Try
        End If
    End Sub
#End Region


    Private Sub lvTreeExp_MouseUp(sender As Object, e As MouseEventArgs) Handles lvTreeExp.MouseUp
        'Dim lv As ListView = DirectCast(sender, ListView)
        ''Checking the Mouse right Button
        'If e.Button = MouseButtons.Right Then
        '    Dim SelectedItemsCount As Integer = lv.SelectedItems.Count
        '    Dim lvi As ListView.SelectedListViewItemCollection = lv.SelectedItems
        '    If SelectedItemsCount = 0 Then
        '        lv.ContextMenuStrip = Nothing
        '        Exit Sub
        '    Else
        '        lv.ContextMenuStrip = Me.ContextMenuStrip1
        '    End If

        '    If SelectedItemsCount = 1 Then
        '        lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
        '    Else
        '        lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
        '    End If

        'End If
    End Sub
End Class
