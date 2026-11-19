Option Strict Off

Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Threading


''' <summary>
''' Control to show explorer and crypto operations
''' </summary>
''' <remarks></remarks>
Public Class UCExplorer

    Friend WithEvents UcWinExplorer1 As UCWinExplorer
    Public WithEvents ucRecentFilesTab As UcLibraryTabpage


#Region "   Events"
    ''' <summary>
    ''' Event raised when <see cref=" UCCryptExplorer "></see> is selected
    ''' </summary>
    ''' <param name="ucCryptExplorerSelected"></param>
    ''' <remarks></remarks>
    Public Event UCLibrarySelectedEvent(tabSelected As IndronTabPage)
#End Region

    Public Class menuLeft
        Public Const mnuExplorer As String = "0"
        Public Const mnuLibraries As String = "1"
    End Class

#Region "Splitter var"
    ''' <summary>
    ''' distance for the central split that divides panels
    ''' </summary>
    Dim splitterDistance As Integer = 160
    ''' <summary>
    ''' splitter distance when closed
    ''' </summary>
    Dim SplitterDistanceClosed As Integer = 22
#End Region

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
        AddHandler ind.win.AppEventsClass.LibraryOpenEvent, AddressOf LibraryOpenEventHandler
        AddHandler ind.win.AppEventsClass.ObjectOpenEvent, AddressOf openObjectTab
    End Sub

    Private Sub UCExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.DesignMode = False Then

                If controlIsInitialized = False Then
                    Me.SplitMain.SplitterDistance = SplitterDistanceClosed
                    Me.SplitMain.Panel1MinSize = SplitterDistanceClosed
                    loadToolBox()

                    controlIsInitialized = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' remove tab from tabcontrol when the library is deleted
    ''' </summary>
    ''' <param name="libraryIdKey"></param>
    Public Sub LibraryDeletedEventHandler(libraryIdKey As Integer)
        Dim tab As UcLibraryTabpage = Me.UcIndTabCtrl1.GetUcTabByKey(libraryIdKey:=libraryIdKey)
        Me.UcIndTabCtrl1.TabPageRemoveBase(tab)
    End Sub

    Private Sub loadToolBox()
        'Me.UcLeftMenu1.addOption(menuLeft.mnuLibraries, Global.ind.lang.UILang.LibraryTitle, New UCLeftMenu.TheControl(Global.ind.lang.UILang.LibraryTitle, AddressOf UCTreeView.loadTreeView))
        'Me.UcLeftMenu1.addOption(menuLeft.mnuExplorer, Global.ind.lang.UILang.ExplorerTitle, New UCLeftMenu.TheControl(Global.ind.lang.UILang.ExplorerTitle, AddressOf initUCWinExplorer))
        With Me.UcLeftMenu1
            Dim libTheControl As New UCLeftMenu.TheControl(Global.ind.lang.UILang.LibraryTitle, AddressOf UCTreeView.loadTreeView)
            .controlList.Add(menuLeft.mnuLibraries, libTheControl)
            .btnLibraries.Text = Global.ind.lang.UILang.LibraryTitle
            .btnLibraries.Tag = menuLeft.mnuLibraries

            Dim explTehControl As New UCLeftMenu.TheControl(Global.ind.lang.UILang.LibraryTitle, AddressOf initUCWinExplorer)
            .controlList.Add(menuLeft.mnuExplorer, explTehControl)
            .btnExplorer.Text = Global.ind.lang.UILang.ExplorerTitle
            .btnExplorer.Tag = menuLeft.mnuExplorer
        End With
    End Sub

    Friend Function initUCWinExplorer() As UCWinExplorer
        Me.UcWinExplorer1 = New UCWinExplorer()
        '
        'UcWinExplorer1
        '
        Me.UcWinExplorer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcWinExplorer1.Location = New System.Drawing.Point(0, 0)
        Me.UcWinExplorer1.Name = "UcWinExplorer1"
        Me.UcWinExplorer1.Size = New System.Drawing.Size(34, 330)
        Me.UcWinExplorer1.TabIndex = 0
        'Me.UcWinExplorer1.expand(False)
        Return Me.UcWinExplorer1
    End Function

    Public Sub EncryptOpenLibraries()
        'not used yet, not validated
        Try
            Dim libCol As New ArrayList
            For Each tbi As IndTabInfo In Me.UcIndTabCtrl1.tabCol.Values
                If tbi.tab.GetType Is GetType(UcLibraryTabpage) Then
                    Dim EntTab As UcLibraryTabpage = DirectCast(tbi.tab, UcLibraryTabpage)
                    EntTab.ucLib.EncryptOrDecryptAll(ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
                End If
            Next
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub


    Public Function getTabsWithChanges() As Boolean
        Dim result As Boolean = False
        Dim tabsWithChanges As New IndTabInfoCollection
        For Each tbi As IndTabInfo In Me.UcIndTabCtrl1.tabCol.Values
            If tbi.tab.GetType Is GetType(UcLibraryTabpage) Then
                Dim EntTab As UcLibraryTabpage = DirectCast(tbi.tab, UcLibraryTabpage)
                If EntTab.ucLib.checkChanges = True Then
                    tabsWithChanges.Add(tbi)
                    result = True 'if one has changes then show message
                    Exit For
                End If
            End If
        Next


        Dim resultForRecent As Boolean = False

        'check recent files
        If Not Me.ucRecentFilesTab Is Nothing Then
            If Me.ucRecentFilesTab.ucLib.checkChanges = True Then
                Debug.WriteLine("ucRecentFilesTab has changes")

                resultForRecent = True
                result = True
            End If
        End If

        If result = True Then
            'ask what to do
            Dim msg As String = ind.lang.UCCryptExplorerLang.library_Save_questionForAll
            If resultForRecent = True Then
                msg = String.Format(msg, ind.lang.UCCryptExplorerLang.library_Save_questionForRecentFiles)
            Else
                msg = String.Format(msg, "") 'to clear the message {0}
            End If
            Dim dr As DialogResult = ind.win.Msg.mQuestionDlg(msg, "", ind.win.Msg.MessageIconEnum.Warning, frmDlg.FocusButtonEnum.cancelBtn).ShowDialog
            If dr = DialogResult.Cancel Then
                result = True
            Else
                result = False
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' save all libraries
    ''' </summary>
    ''' <returns></returns>
    Public Sub SaveAllLibraries()

        For Each tbi As IndTabInfo In Me.UcIndTabCtrl1.tabCol.Values
            If tbi.tab.GetType Is GetType(UcLibraryTabpage) Then
                Dim EntTab As UcLibraryTabpage = DirectCast(tbi.tab, UcLibraryTabpage)
                If EntTab.ucLib.checkChanges = True Then
                    EntTab.ucLib.LibrarySave()
                End If
            End If
        Next

        If Not FrmLibrary.formsList Is Nothing Then
            For Each frm As FrmLibrary In FrmLibrary.formsList.Values
                Dim EntTab As UcLibraryTabpage = frm.ucLibTab
                If EntTab.ucLib.checkChanges = True Then
                    EntTab.ucLib.LibrarySave()
                End If
            Next
        End If


        'todo: find tabs in windows

    End Sub

    ''' <summary>
    ''' load recenttab control with all recent libraries
    ''' </summary>
    ''' <remarks></remarks>
    Public Function openObjectTab(code As Integer) As IndronTabPage
        Select Case code
            Case ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs
                Return loadRecentLibrariesTab()
            Case ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager
                Return loadPasswordManagerTab()

        End Select
        Return Nothing
    End Function


    ''' <summary>
    ''' load recenttab control with all recent libraries
    ''' </summary>
    ''' <remarks></remarks>
    Public Function loadRecentLibrariesTab() As UCLibraryRecentsTabPage
        Cursor = Cursors.WaitCursor
        Try
            Dim UCLibraryRecentsTab As UCLibraryRecentsTabPage = Me.UcIndTabCtrl1.GetUcLibraryRecentsTab

            If UCLibraryRecentsTab Is Nothing Then
                UCLibraryRecentsTab = New UCLibraryRecentsTabPage
                UCLibraryRecentsTab.tabType = IndronTabPage.tabTypeEnum.fixed
                UCLibraryRecentsTab.UcLibraryRecents1.ucExplorer1 = Me
                Dim tbi As IndTabInfo = Me.UcIndTabCtrl1.AddTabPageBase(ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs, UCLibraryRecentsTab, True)
                UCLibraryRecentsTab.UcLibraryRecents1.initUClibraryRecentsAfterLoad()
            Else
                Me.UcIndTabCtrl1.tabSelect(UCLibraryRecentsTab)
            End If

            Return UCLibraryRecentsTab
        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try


    End Function
    ''' <summary>
    ''' load recent files tab
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadLibraryRecentFilesTAB(Optional mustSelect As Boolean = True) As UCCryptExplorer
        Cursor = Cursors.WaitCursor
        If Me.InvokeRequired Then

        End If
        Try

            If ucRecentFilesTab Is Nothing OrElse ucRecentFilesTab.IsDisposed Then
                'recent files tab wasn´t loaded before, so load
                ucRecentFilesTab = Me.LoadLibrary(ind.crypt.OCGlobal.libraryRecentFilesGlob, mustSelect)
                ucRecentFilesTab.tabType = IndronTabPage.tabTypeEnum.normal
                ucRecentFilesTab.btnTab.Icon = My.Resources.project_menu_recentfiles_32
                ucRecentFilesTab.ucLib.saveAutomatic = True
                Return ucRecentFilesTab.ucLib
            Else
                'was loaded before so check if present
                Dim UCLibraryRecentsTab As UcLibraryTabpage = Me.UcIndTabCtrl1.GetUcLibraryRecentFilesTab
                If Not UCLibraryRecentsTab Is Nothing Then
                    'is loaded so show
                    If mustSelect Then
                        Me.UcIndTabCtrl1.tabSelect(UCLibraryRecentsTab)
                    End If

                Else
                    'is not loaded on tab
                    Me.UcIndTabCtrl1.AddTabPageBase(ind.crypt.OCGlobal.libraryRecentFilesGlob.id, ucRecentFilesTab, mustSelect)
                End If

                Return ucRecentFilesTab.ucLib

            End If
        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try


    End Function


    Public Function loadPasswordManagerTab() As UCPwdManagerTabPage
        Cursor = Cursors.WaitCursor
        Try
            Dim UCTab As UCPwdManagerTabPage = Me.UcIndTabCtrl1.GetUcPwdManagerTabPage

            If UCTab Is Nothing Then
                If Not FrmPwdMgr.frmPwdMgrContainer Is Nothing Then
                    FrmPwdMgr.showFrmPwdMgr()
                    Return Nothing
                End If
                UCTab = New UCPwdManagerTabPage
                UCTab.tabType = IndronTabPage.tabTypeEnum.fixed
                UCTab.btnTab.AllowDrop = True
                Dim tbi As IndTabInfo = Me.UcIndTabCtrl1.AddTabPageBase(ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager, UCTab, True)
                'UCLibraryRecentsTab.UcLibraryRecents1.initUClibraryRecentsAfterLoad()
            Else
                Me.UcIndTabCtrl1.tabSelect(UCTab)
            End If

            Return UCTab

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default

        End Try

    End Function


#Region "Methods to load library"
    Public Function LibraryOpenEventHandler(LibraryToOpen As LibraryDataSet.T_LibraryRow, options As String) As UcLibraryTabpage

        Return LoadLibrary(LibraryToOpen, True)
    End Function

    Public Function LoadLibrary(LibraryToOpen As LibraryDataSet.T_LibraryRow, Optional mustSelect As Boolean = True) As UcLibraryTabpage

        Return Me.UcIndTabCtrl1.LoadUCLibraryTabPageByLibrary(LibraryToOpen, mustSelect)
    End Function

    Private Sub UcIndTabCtrl1_tabAddedEvent(sender As IndronTabControl, tabSelected As IndronTabPage) Handles UcIndTabCtrl1.tabAddedEvent

    End Sub

    Private Sub UcIndTabCtrl1_tabRemovedEvent(sender As IndronTabControl, tabSelected As IndronTabPage) Handles UcIndTabCtrl1.tabRemovedEvent
        Dim UCLibraryExp As UCCryptExplorer = UcLibraryTabpage.IsUCLibraryTabpage(tabSelected)
        If Not UCLibraryExp Is Nothing Then
            ind.win.AppEventsClass.LibraryCloseEventRaise(UCLibraryExp.LibraryLoaded.libraryID)
        End If
    End Sub

    Private Sub UcIndTabCtrl1_tabSelectedEvent(sender As IndronTabControl, tabSelected As IndronTabPage) Handles UcIndTabCtrl1.tabSelectedEvent

        'Dim UCLibraryExp As UCCryptExplorer = UCLibraryTabpage.IsUCLibraryTabpage(tabSelected)
        'If Not UCLibraryExp Is Nothing Then
        RaiseEvent UCLibrarySelectedEvent(tabSelected)
        'End If
    End Sub
#End Region


#Region "   TreeView"
    Private Sub ExpTree1_ExpTreeNodeSelected(pathName As String) Handles UcWinExplorer1.ExpTreeNodeSelectedEvent

        Me.SplitMain.SplitterDistance = 350
        Me.UcWinExplorer1.SplitExplorer.SplitterDistance = 140
        Me.UcWinExplorer1.SplitExplorer.Panel2Collapsed = False
        Me.UcWinExplorer1.expand(True)
    End Sub


#End Region

#Region "Expand/collapse explorer"

    Private Sub expandSplit(expand As Boolean)
        If expand = True Then
            Me.SplitMain.SplitterDistance = splitterDistance
        Else
            Me.SplitMain.SplitterDistance = SplitterDistanceClosed
        End If
    End Sub


    Public Sub ExplorerExpand(pathName As String) Handles UcWinExplorer1.ExpTreeNodeSelectedEvent
        expandSplit(True)


    End Sub


    Private Sub SplitMain_DoubleClick(sender As Object, e As EventArgs) Handles SplitMain.DoubleClick
        Dim expand As Boolean = Me.SplitMain.Panel1Collapsed
        expandSplit(expand)
    End Sub

    Private Sub UcLeftMenu1_optionClicked(open As Boolean) Handles UcLeftMenu1.optionClicked
        expandSplit(open)
    End Sub


#End Region








End Class
