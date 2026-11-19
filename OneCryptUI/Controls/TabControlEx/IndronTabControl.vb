
''' <summary>
''' class to host control as Winform tabcontrol
''' </summary>
''' <remarks></remarks>
Public Class IndronTabControl
    ''' <summary>
    ''' colection with tabpages
    ''' </summary>
    ''' <remarks></remarks>
    Public tabCol As New IndTabInfoCollection
    ''' <summary>
    ''' selected tabpage
    ''' </summary>
    ''' <remarks></remarks>
    Public SelectedTab As IndronTabPage
    ''' <summary>
    ''' Event raised to control if the tab can be added or not.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="tabSelected"></param>
    ''' <param name="canAdd">True to add, false to not add</param>
    ''' <remarks></remarks>
    Public Event tabBeforeAddEvent(sender As IndronTabControl, tabSelected As IndronTabPage, canAdd As Boolean)
    ''' <summary>
    ''' Raise when a tab is effectively added
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="tabSelected"></param>
    ''' <remarks></remarks>
    Public Event tabAddedEvent(sender As IndronTabControl, tabSelected As IndronTabPage)
    ''' <summary>
    ''' Raise when a tab is selected
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="tabSelected"></param>
    ''' <remarks></remarks>
    Public Event tabSelectedEvent(sender As IndronTabControl, tabSelected As IndronTabPage)
    ''' <summary>
    ''' raise when a tab is removed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="tabSelected"></param>
    ''' <remarks></remarks>
    Public Event tabRemovedEvent(sender As IndronTabControl, tabSelected As IndronTabPage)
    ''' <summary>
    ''' Tab hosted in tabcontrol
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property tabCount As Integer
        Get
            Return Me.tabCol.Count
        End Get
    End Property


    Private Sub IndronTabControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.pnlFlowButtons.tabControl = Me
    End Sub


#Region "   Behavior of buttons, colors, etc"
    Public tabPageBackColorSelected As Color = Color.IndianRed
    Public tabPageBackColorUnSelected As Color = Color.Gainsboro

#End Region



    Public Function AddTabPageBase(key As Integer, tabToHost As IndronTabPage, Optional IsTabSelected As Boolean = True, Optional keyUnique As String = "") As IndTabInfo
        Try
            Me.Cursor = Cursors.WaitCursor

            If tabToHost Is Nothing Then
                Return Nothing
            End If
            Me.pnlTab.SuspendLayout()
            Dim addOption As Boolean = True
            RaiseEvent tabBeforeAddEvent(Me, tabToHost, addOption)
            If addOption = False Then
                Return Nothing
            End If
            tabToHost.tabCtrolContainer = Me

            Dim tbi As New IndTabInfo
            tbi.keyIndex = key
            tbi.tab = tabToHost
            tabToHost.tbi = tbi

            Me.tabCol.Add(tbi)
            If tabToHost.tabType = IndronTabPage.tabTypeEnum.normal Then
                tabToHost.btnTab.AllowDrop = True
            End If
            Me.pnlFlowButtons.buttonAdd(tabToHost.btnTab)

            If IsTabSelected = True Then
                Me.tabSelect(tabToHost)
            Else
                'tabpage is loaded on tabcol colection and button is loaded but not mark as selected
                'Me.tabSetToPanel(tabToHost)
            End If
            RaiseEvent tabAddedEvent(Me, tabToHost)
            Return tbi

        Catch ex As Exception
            Throw ex
        Finally
            Me.pnlTab.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try

    End Function
    ''' <summary>
    ''' Select a tab and show in tabcontrol
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Sub tabSelect(tab As IndronTabPage)
        If Not Me.SelectedTab Is Nothing Then
            If SelectedTab.tbi.keyIndex = tab.tbi.keyIndex Then
                Exit Sub
            Else
                Me.SelectedTab.IsSelected = False
            End If
        End If

        tab.IsSelected = True
        Me.tabSetToPanel(tab)
        Me.SelectedTab = tab
        Me.pnlFlowButtons.showButton(tab.btnTab)
        RaiseEvent tabSelectedEvent(Me, tab)
    End Sub
    ''' <summary>
    ''' Set a tab in the tabcontrol
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Private Sub tabSetToPanel(tab As IndronTabPage)
        Try
            Me.pnlTab.SuspendLayout()
            Me.pnlTab.Controls.Clear()
            tab.Parent = Me.pnlTab
            tab.Dock = DockStyle.Fill
            tab.Visible = True
            Me.pnlFlowButtons.buttonAdd(tab.btnTab)

            'Me.pnlTab.ResumeLayout()
        Catch ex As Exception
            Throw ex
        Finally
            Me.pnlTab.ResumeLayout()

        End Try

    End Sub
    ''' <summary>
    ''' remove tab
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Sub TabPageRemoveBase(tab As IndronTabPage)

        If tab Is Nothing Then
            Exit Sub
        End If

        Dim keyIndex2Remove As Integer = tab.tbi.keyIndex
        Dim isSelectedTab As Boolean = False
        'check if is selected tab
        If Not Me.SelectedTab Is Nothing Then
            If Me.SelectedTab.tbi.keyIndex = keyIndex2Remove Then
                isSelectedTab = True
            End If
        End If

        tab.tbi.tab = Nothing 'todo: important because it has a self reference and its not deleted by garbage collector.
        Me.tabCol.Remove(keyIndex2Remove)
        RaiseEvent tabRemovedEvent(Me, tab)
        Me.pnlFlowButtons.buttonRemove(tab.btnTab)
        tab.Visible = False

        If isSelectedTab = True Then
            Me.pnlTab.Controls.Clear()
            'set one
            If Me.tabCol.Count > 0 Then
                For Each tbi As IndTabInfo In Me.tabCol.Values
                    Me.tabSelect(tbi.tab)
                    Exit For
                Next
            End If
        End If

    End Sub




End Class
