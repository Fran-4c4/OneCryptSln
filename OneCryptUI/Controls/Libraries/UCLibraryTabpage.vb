

''' <summary>
''' class to encapsulate a <see cref="UCCryptExplorer ">the tab with library</see>
''' </summary>
''' <remarks></remarks>
Public Class UcLibraryTabpage
    Inherits IndronTabPage
    ''' <summary>
    ''' UCCryptExplorer control hosted
    ''' </summary>
    Public WithEvents ucLib As UCCryptExplorer

    Public Sub InitConfig(ByVal ucLib As UCCryptExplorer)
        Me.ucLib = ucLib
        Me.initConfigBase(ucLib.LibraryLoaded.LibraryName, ind.lang.UILang.tabbutton_TT)
        Me.SetTitle(Me.ucLib)
        Me.Controls.Add(ucLib)
        Me.ucLib.TabPageContainer = Me
        Me.ucLib.Dock = DockStyle.Fill
    End Sub

    Public Const topTitle As String = "  {0}(V{1}) {2}  "

    Public Sub SetTitle(ucLib As UCCryptExplorer)
        Me.Text = ucLib.LibraryLoaded.LibraryName

        Dim tiptext As String = ""
        'Me.ToolTipTex = tiptext
    End Sub
    ''' <summary>
    ''' If sender object is typeof selected control then return hosted control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsUCLibraryTabpage(sender As Object) As UCCryptExplorer
        If sender Is Nothing Then
            Return Nothing
        End If

        If sender.GetType Is GetType(UCLibraryTabpage) Then
            Dim EntTab As UCLibraryTabpage = DirectCast(sender, UCLibraryTabpage)
            Return EntTab.ucLib
        Else
            Return Nothing
        End If
    End Function

    Private Sub UCLibraryTabpage_tabPageClosingEvent(tab As IndronTabPage, cancel As Boolean) Handles Me.tabPageClosingEvent
        Try
            If Me.ucLib.LibraryLoaded.libraryID <> ind.crypt.OCGlobal.libraryRecentFilesGlob.id Then 'todo: ver si aqui se puede cambiar por el campo de automaticsave
                Cursor = Cursors.WaitCursor
                'Me.Refresh()
                Me.ucLib.LibrarySaveWithCheck()
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnTab_buttonDoubleClickEvent(tab As IndronTabPage) Handles btnTab.buttonDoubleClickEvent
        FrmLibrary.openLibraryInNewForm(Me)
        Me.tabCtrolContainer.TabPageRemoveBase(tab)
        tab.Visible = True
        Me.TabButtonRemove()
    End Sub


    Private Sub btnTab_buttonMoveOutEvent(tab As IndronTabPage) Handles btnTab.buttonMoveOutEvent
        FrmLibrary.openLibraryInNewForm(Me)
        Me.tabCtrolContainer.TabPageRemoveBase(tab)
        tab.Visible = True
        Me.TabButtonRemove()
    End Sub


End Class
