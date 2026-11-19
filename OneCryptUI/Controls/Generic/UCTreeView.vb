


Imports System.Data

Public Class UCTreeView

    Private Structure NodeTag
        Public Enum nodeTypeEnum As Integer
            basic = 0
            libraryrow = 1
        End Enum
        Public nodeType As nodeTypeEnum
        Public tag As Object
        Public Shared Function createNodeTag(nodeType As nodeTypeEnum, tag As Object) As NodeTag
            Dim nt As NodeTag
            nt.nodeType = nodeType
            nt.tag = tag
            Return nt
        End Function


    End Structure

    Public Sub setTitle(title As String)
        If title.Trim.Length = 0 Then
            Me.pnlTop.Visible = False

        Else
            lblTit.Text = title
            Me.pnlTop.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' access to application events
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler ind.win.AppEventsClass.LibraryNewCreatedEvent, AddressOf LibraryNewCreatedHandler
        AddHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
    End Sub

    Private Sub UCLibraryTree_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.setTitle("")
        myAppEvents_LanguageChangedEvent()
        loadTree()

    End Sub

    Private Sub LibraryNewCreatedHandler(drLibrary As LibraryDataSet.T_LibraryRow)
        loadTree()
    End Sub

    Private Function getNodeTag(tag As Object) As NodeTag
        Dim nt As NodeTag = CType(tag, NodeTag)
        Return nt
    End Function

    Public Sub LibraryDeletedEventHandler(libraryIdKey As Integer)
        For Each node As TreeNode In TreeV.Nodes
            If node Is Nothing Then
                'when we delete a node the tree collection seems do not refresh inmediatelly
                Continue For
            End If
            Dim nt As NodeTag = getNodeTag(node.Tag)
            If nt.nodeType = NodeTag.nodeTypeEnum.libraryrow Then
                Dim drLibrary As LibraryDataSet.T_LibraryRow = CType(nt.tag, LibraryDataSet.T_LibraryRow)
                If drLibrary.id = libraryIdKey Then
                    node.Remove()
                    Exit Sub
                Else
                    If node.Nodes.Count > 0 Then
                        deleteSubNode(node, libraryIdKey:=libraryIdKey)
                    End If

                End If
            Else
                If node.Nodes.Count > 0 Then
                    deleteSubNode(node, libraryIdKey:=libraryIdKey)
                End If
            End If
        Next
    End Sub

    Public Sub deleteSubNode(pnode As TreeNode, libraryIdKey As Integer)
        For Each node As TreeNode In pnode.Nodes
            If node Is Nothing Then
                'when we delete a node the tree collection seems do not refresh inmediatelly
                Continue For
            End If
            Dim nt As NodeTag = getNodeTag(node.Tag)
            If nt.nodeType = NodeTag.nodeTypeEnum.libraryrow Then
                Dim drLibrary As LibraryDataSet.T_LibraryRow = CType(nt.tag, LibraryDataSet.T_LibraryRow)
                If drLibrary.id = libraryIdKey Then
                    node.Remove()
                    Exit Sub
                Else
                    If node.Nodes.Count > 0 Then
                        deleteSubNode(node, libraryIdKey:=libraryIdKey)
                    End If
                End If
            Else
                If node.Nodes.Count > 0 Then
                    deleteSubNode(node, libraryIdKey:=libraryIdKey)
                End If
            End If


        Next
    End Sub


    Public Shared Function loadTreeView() As UCTreeView
        Return New UCTreeView
    End Function


    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        'Me.setTitle(ind.lang.UCLibraryLang.lbl_Title)
        'Me.lblSubTitle.Text = ind.lang.UCLibraryLang.lblSubTitle
        'Me.ToolTip1.SetToolTip(Me.btnCreateLib, ind.lang.UCLibraryLang.btnOpenLib_TT)
    End Sub

    Public Sub loadTree()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.TreeV.SuspendLayout()
            Dim mda1 As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
            Dim dtLibraries As LibraryDataSet.T_LibraryDataTable
            dtLibraries = mda1.GetData
            dtLibraries.DefaultView.Sort = "name"
            Me.TreeV.Nodes.Clear()

            'recent files library node
            Dim libnodePwd = TreeV.Nodes.Add(ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager.ToString, ind.lang.UCpwdMngLang.pwdManagerTabButton, 0, 0)
            libnodePwd.Tag = NodeTag.createNodeTag(NodeTag.nodeTypeEnum.basic, ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager)
            libnodePwd.ImageIndex = 1
            libnodePwd.SelectedImageIndex = 1


            'recent files library node
            Dim libnodeRecents = TreeV.Nodes.Add(ind.crypt.OCGlobal.libraryRecentFilesGlob.id.ToString, ind.crypt.OCGlobal.libraryRecentFilesGlob.Name, 0, 0)
            libnodeRecents.Tag = NodeTag.createNodeTag(NodeTag.nodeTypeEnum.libraryrow, ind.crypt.OCGlobal.libraryRecentFilesGlob)
            libnodeRecents.ImageIndex = 0
            libnodeRecents.SelectedImageIndex = 0

            'library node
            Dim libnode = TreeV.Nodes.Add(ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs.ToString, Global.ind.lang.UILang.frmMain_tsbProjectsMain, 0, 0)
            libnode.Tag = NodeTag.createNodeTag(NodeTag.nodeTypeEnum.basic, ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs.ToString)
            libnode.ImageIndex = 0
            libnode.SelectedImageIndex = 0


            For Each drv As DataRowView In dtLibraries.DefaultView
                Dim drl As LibraryDataSet.T_LibraryRow = CType(drv.Row, LibraryDataSet.T_LibraryRow)
                If drl.id <> ind.crypt.OCGlobal.libraryRecentFilesGlob.id Then
                    loadNode(libnode, drl:=drl)
                End If
            Next
            libnode.ExpandAll()
            'other libraries like onedrive, gmail


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Me.TreeV.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub loadNode(pnode As TreeNode, drl As LibraryDataSet.T_LibraryRow)

        Dim node = pnode.Nodes.Add(drl.id.ToString, drl.Name)
        With node
            .Tag = NodeTag.createNodeTag(NodeTag.nodeTypeEnum.libraryrow, drl)
            .ImageIndex = 0
            .SelectedImageIndex = 0
        End With
    End Sub

    ''' <summary>
    ''' init the control.
    ''' This method is called by main when app is loaded
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub initUClibraryRecentsAfterLoad()
        If Me.DesignMode = False Then
            'Me.addLibraryRecentFilesButton()
            Me.loadTree() 'load all the libraries from database
        End If


    End Sub

    Public Sub createNewLibrary()
        Try
            Dim ucl As New UcNewLibrary
            Dim sd As DialogResult = ucl.createNewLibraryDialog
            If sd = DialogResult.Cancel Then
                Debug.WriteLine("cancel")
            Else
                Me.loadTree()
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' delete a library
    ''' </summary>
    ''' <param name="libraryId"></param>
    ''' <param name="libraryName"></param>
    ''' <remarks></remarks>
    Public Sub LibraryDelete(libraryId As Integer, libraryName As String)
        Try

            Dim fDlg As frmDlg = ind.win.Msg.mQuestionDlg(ind.lang.UCLibraryLang.LibraryDeleteDlgMessage & vbCr & libraryName, ind.lang.UCLibraryLang.LibraryDeleteDlgTitle, ind.win.Msg.MessageIconEnum.library_Delete, frmDlg.FocusButtonEnum.cancelBtn)
            fDlg.setFocusOnBtn(frmDlg.FocusButtonEnum.cancelBtn)

            If fDlg.ShowDialog = DialogResult.OK Then
                'borrar
                Dim da As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
                da.DeleteLibraryByID(libraryId)
                Me.loadTree()
                ind.win.AppEventsClass.LibraryDeletedEventRaise(libraryIdKey:=libraryId)
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Dim nodePrevious As TreeNode = Nothing

    Private Function NodeIsLibrary(node As TreeNode) As LibraryDataSet.T_LibraryRow
        If node.Tag Is Nothing Then
            Return Nothing
        End If
        Dim nt As NodeTag = CType(node.Tag, NodeTag)
        If nt.nodeType = NodeTag.nodeTypeEnum.libraryrow Then
            Return CType(nt.tag, LibraryDataSet.T_LibraryRow)
        End If

        Return Nothing
    End Function

    Private Sub TreeV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeV.NodeMouseClick
        If IsNothing(e.Node) = True Then
            Exit Sub 'en teoria este evento solo se produce sobre un nodo, asi que no deberia ser null
        End If

        If e.Button = MouseButtons.Right Then
            'contextMenuSelectedNode = e.Node
            'ContextMenuStrip1.Show(TreeV, e.Location)
            Exit Sub
        End If

        nodePrevious = e.Node

        Dim nt As NodeTag = CType(e.Node.Tag, NodeTag)
        If nt.nodeType = NodeTag.nodeTypeEnum.basic Then
            Dim code As Integer = CType(nt.tag, Integer)
            Select Case code
                Case ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs
                    ind.win.AppEventsClass.ObjectOpenEventRaise(ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs)
                    Exit Sub
                Case ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager
                    ind.win.AppEventsClass.ObjectOpenEventRaise(ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager)
                    Exit Sub

            End Select

        ElseIf nt.nodeType = NodeTag.nodeTypeEnum.libraryrow Then
            Dim drLibrary As LibraryDataSet.T_LibraryRow = NodeIsLibrary(e.Node)
            If IsNothing(drLibrary) = False Then
                If ModifierKeys = Keys.Control Then
                    ind.win.AppEventsClass.LibraryOpenEventRaise(drLibrary, "openform")
                Else
                    ind.win.AppEventsClass.LibraryOpenEventRaise(drLibrary, "")
                End If

                Exit Sub
            End If
        End If



    End Sub

    Private Sub mniProjectSave_Click(sender As Object, e As EventArgs) Handles mniProjectSave.Click

        Try
            Cursor = Cursors.WaitCursor
            Dim nt As NodeTag = getNodeTag(contextMenuSelectedNode.Tag)
            ind.win.AppEventsClass.LibrarySaveEventRaise(CType(nt.tag, LibraryDataSet.T_LibraryRow))
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Dim backSelectedNode As TreeNode
    Dim contextMenuSelectedNode As TreeNode



    Private Sub TreeV_MouseUp(sender As Object, e As MouseEventArgs) Handles TreeV.MouseUp
        'If e.Button = MouseButtons.Right Then
        '    Dim p As Point = New Point(e.X, e.Y)
        '    contextMenuSelectedNode = TreeV.GetNodeAt(p)
        '    If Not contextMenuSelectedNode Is Nothing Then
        '        backSelectedNode = TreeV.SelectedNode
        '        'now select the node used by 
        '        TreeV.SelectedNode = contextMenuSelectedNode
        '        'ContextMenuStrip1.Show(TreeV, e.Location)
        '        TreeV.SelectedNode = backSelectedNode
        '        backSelectedNode = Nothing
        '        'contextMenuSelectedNode = Nothing
        '    End If
        'End If
    End Sub
End Class
