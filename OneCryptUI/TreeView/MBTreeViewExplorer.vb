Option Strict Off

#Region "   About Auhor"
'
'   UserControl Name    :   MBTreeViewExplorer Control
'   Created             :   17 Dec 2011
'   Purpose             :   To Browse Folders and Files 
'   Vision              :   1.0.4368.33361
'   IDE                 :   Visual Basic .Net 2008
'   Author              :   Manoj K Bhoir
'
'   You can not:
'   Sell or redistribute this code or the binary for profit.
'   Use this in spyware, malware, or any generally acknowledged form of malicious software.
'   Remove or alter the above author accreditation, or this disclaimer.
'
'   You can:
'   Use this code in your applications in any way you like.
'   Use this in a published program, (a credit to MBUserControls would be nice)
'
'   I will not:
'   Except any responsibility for this code whatsoever. 
'   There is no guarantee of fitness, nor should you have any expectation of support. 
'   
'                                                                   Manoj K Bhoir
'                                                                   manojbhoir28@gmail.com    
#End Region

#Region "   Import"
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

#End Region

Namespace MBTreeViewExplorer


#Region "   MBTreeViewExplorer"

    ''' <summary>
    ''' MBTreeViewExplorer Class © Manoj K Bhoir
    ''' </summary>
    ''' <remarks>Version 1.0</remarks>
    <ToolboxItem(True), ToolboxBitmap(GetType(MBTreeViewExplorer), "MBTreeViewExplorer.MBTreeViewExplorer.bmp"), ToolboxItemFilter("System.Windows.Forms"), Description("Explore Folders for Browse.")> _
    Public Class MBTreeViewExplorer
        Inherits System.Windows.Forms.TreeView
        Public Delegate Sub MBTreeViewExplorer_Clcik()
        Friend WithEvents MBTreeView As System.Windows.Forms.TreeView
        ''' <summary>
        ''' Initialize Components of MBExplorer
        ''' </summary>
        Private Sub InitializeComponent()
            Me.MBTreeView = New System.Windows.Forms.TreeView
            Me.SuspendLayout()
            '
            'MBTreeView
            '
            Me.MBTreeView.BorderStyle = Windows.Forms.BorderStyle.None
            Me.MBTreeView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.MBTreeView.ForeColor = System.Drawing.SystemColors.WindowText
            Me.MBTreeView.Location = New System.Drawing.Point(0, 0)
            Me.MBTreeView.Name = "MBTreeView"
            Me.MBTreeView.Size = New System.Drawing.Size(188, 307)
            Me.MBTreeView.TabIndex = 0
            '
            'MBTreeViewExplorer
            '
            Me.Controls.Add(Me.MBTreeView)
            Me.Name = "MBTreeViewExplorer"
            Me.Size = New System.Drawing.Size(209, 353)
            Me.ResumeLayout(False)

        End Sub
        ''' <summary>
        ''' Constructor of MBTreeViewExplorer
        ''' </summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SystemImageList.SetTreeViewImageList(MBTreeView, False)
            LoadNodes()
            AddHandler MBTreeView.AfterSelect, AddressOf MBTreeViewExplorer_AfterSelect
            AddHandler Me.ForeColorChanged, AddressOf MBTreeViewExplorer_ForeColorChanged
        End Sub

        Private Sub MBTreeViewExplorer_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MBTreeView.AfterSelect

        End Sub



        ''' <summary>
        ''' Handles MBTreeViewExplorer ForeColor Property
        ''' </summary>
        Private Sub MBTreeViewExplorer_ForeColorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ForeColorChanged
            MBTreeView.ForeColor = Me.ForeColor
        End Sub
        Public Overloads ReadOnly Property SelectedNode() As TreeNode
            Get
                Return MBTreeView.SelectedNode
            End Get
        End Property
        ''' <summary>
        ''' Load Nodes for MBTreeViewExplorer
        ''' </summary>
        Private Sub LoadNodes()
            'Set Treeview Image List for MBTreeViewExplorer
            SystemImageList.SetTreeViewImageList(MBTreeView, False)
            'New ShellItem to Load Desktop
            Dim m_shDesktop As ShellItem = New ShellItem()
            Dim tvwRoot As TreeNode = New TreeNode()
            tvwRoot.Name = m_shDesktop.Path
            tvwRoot.Text = m_shDesktop.DisplayName
            tvwRoot.ImageIndex = m_shDesktop.IconIndexNormal
            tvwRoot.SelectedImageIndex = m_shDesktop.IconIndexOpen
            tvwRoot.Tag = m_shDesktop

            Dim arrChildren As ArrayList = m_shDesktop.GetAllDirectories
            For Each shChild As ShellItem In arrChildren
                Dim tvwChild As TreeNode = New TreeNode()
                tvwChild.Name = shChild.Path
                tvwChild.Text = shChild.DisplayName
                tvwChild.ImageIndex = shChild.IconIndexNormal
                tvwChild.SelectedImageIndex = shChild.IconIndexOpen
                tvwChild.Tag = shChild
                If shChild.IsFolder And shChild.HasSubFolders Then tvwChild.Nodes.Add("PH")
                tvwRoot.Nodes.Add(tvwChild)
            Next
            MBTreeView.Nodes.Clear()
            MBTreeView.Nodes.Add(tvwRoot)
            tvwRoot.Expand()
            MBTreeView.SelectedNode = MBTreeView.Nodes(0)
        End Sub
        ''' <summary>
        ''' Handles All Events When Node Gets Selected.
        ''' </summary>
        Private Sub MBTreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles MBTreeView.AfterSelect
            _SelectedFile = e.Node.Tag
        End Sub
        ''' <summary>
        ''' Load Sub Nodes for Selected Node
        ''' </summary>
        Private Sub MBTreeView_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles MBTreeView.BeforeExpand
            e.Node.Nodes.Clear()
            Dim shNode As ShellItem = e.Node.Tag
            Dim arrSub As ArrayList = shNode.GetAllDirectories()
            For Each shChild As ShellItem In arrSub
                Dim tvwChild As TreeNode = New TreeNode()
                tvwChild.Name = shChild.Path
                tvwChild.Text = shChild.DisplayName
                tvwChild.ImageIndex = shChild.IconIndexNormal
                tvwChild.SelectedImageIndex = shChild.IconIndexOpen
                tvwChild.Tag = shChild
                If shChild.IsFolder And shChild.HasSubFolders Then tvwChild.Nodes.Add("PH")
                e.Node.Nodes.Add(tvwChild)
            Next
        End Sub
        ''' <summary>
        ''' Get or Set CheckBox for MBTreeViewExplorer
        ''' </summary>
        Private _showCheckBox As Boolean = False
        Public Overloads Property CheckBoxes() As Boolean
            Get
                Return _showCheckBox
            End Get
            Set(ByVal value As Boolean)
                _showCheckBox = value
                MBTreeView.CheckBoxes = _showCheckBox
                MBTreeViewExplorer_Validating(Me, EventArgs.Empty)
            End Set
        End Property
        ''' <summary>
        ''' Handles Reloading of Nodes for MBTreeViewExplorer
        ''' </summary>
        Private Sub MBTreeViewExplorer_Validating(ByVal sender As Object, ByVal e As System.EventArgs)
            LoadNodes()
        End Sub

        Private _SelectedFile As ShellItem = Nothing
        Public Property SelectedFile() As ShellItem
            Get
                Return _SelectedFile
            End Get
            Set(ByVal value As ShellItem)
                _SelectedFile = value
            End Set
        End Property
    End Class

#End Region
End Namespace