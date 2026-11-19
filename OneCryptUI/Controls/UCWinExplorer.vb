Public Class UCWinExplorer

    ''' <summary>
    ''' True indicates to expand. false otherwise
    ''' </summary>
    ''' <param name="expand"></param>
    Public Event expandEvent(expand As Boolean)
    ''' <summary>
    ''' indicates that a node was clicked
    ''' </summary>
    ''' <param name="pathName"></param>
    Public Event ExpTreeNodeSelectedEvent(pathName As String)
    Dim expandTreeSelectionWidth As Boolean = False



    Public Sub expand(expand As Boolean)
        If expand = True Then
            Me.ExpTree1.Visible = True
            Me.btnCollapseExplorer.Visible = True
        Else
            Me.ExpTree1.Visible = False
            Me.btnCollapseExplorer.Visible = False
        End If
    End Sub

    Private Sub ExpTree1_Load(sender As Object, e As EventArgs) Handles ExpTree1.Load
        expandTreeSelectionWidth = True
        If ind.settings.SettingsLink.ExplorerLastFolder <> "" Then

        End If
    End Sub

    Private Sub ExpTree1_ExpTreeNodeSelected(pathName As String, CSI As ExpTreeLib.CShItem) Handles ExpTree1.ExpTreeNodeSelected
        If expandTreeSelectionWidth = True Then
            Me.lblExplorerTop.Text = pathName
            ind.settings.SettingsLink.ExplorerLastFolder = pathName
            RaiseEvent ExpTreeNodeSelectedEvent(pathName)
            expandTreeSelectionWidth = False
        End If

    End Sub


    Private Sub UCWinExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.DesignMode = False Then
            Me.UcTreeExplorer1.expTree = Me.ExpTree1

        End If
    End Sub

    Private Sub btnCollapseExplorer_Click_1(sender As Object, e As EventArgs) Handles btnCollapseExplorer.Click
        RaiseEvent expandEvent(False)
    End Sub

    Private Sub imgExplorerTop_Click_1(sender As Object, e As EventArgs) Handles imgExplorerTop.Click
        RaiseEvent expandEvent(True)
    End Sub
End Class
