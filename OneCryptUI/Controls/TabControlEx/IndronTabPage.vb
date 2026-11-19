
''' <summary>
''' Class to host control as a tabpage
''' </summary>
''' <remarks></remarks>
Public Class IndronTabPage

    Public title As String = ""
    ''' <summary>
    ''' width of the tab button. In this version is 200
    ''' </summary>
    Public tabButtonWidth As Integer = 200 'pixels

    Public Enum tabTypeEnum As Byte
        normal = 0
        ''' <summary>
        ''' when fixed the tab is drawed at first position
        ''' </summary>
        fixed = 1
        ''' <summary>
        ''' do not dispose or something like that. Only hide.
        ''' </summary>
        ''' <remarks></remarks>
        hideOnClose = 2
    End Enum
    ''' <summary>
    ''' default tabtype
    ''' </summary>
    Public tabType As tabTypeEnum = tabTypeEnum.normal
    Public comments As String
    ''' <summary>
    ''' Button asociated with this tabpage hosted in tabcontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents btnTab As New IndronTabButton1
    ''' <summary>
    ''' main tabcontrol that host this tabpage
    ''' </summary>
    ''' <remarks></remarks>
    Public tabCtrolContainer As IndronTabControl
    ''' <summary>
    ''' Tabinfo class on colection in tabcontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public tbi As IndTabInfo
    Dim m_isSelected As Boolean = False
    Public Property IsSelected As Boolean
        Get
            Return Me.m_isSelected
        End Get
        Set(value As Boolean)
            Me.m_isSelected = value
            Me.btnTab.IsSelected = value
        End Set
    End Property

#Region "Events"

    ''' <summary>
    ''' Event raised when click close button 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event tabPageClosingEvent(tab As IndronTabPage, cancel As Boolean)
    ''' <summary>
    ''' When click on button raise an event to indicate the tab selected
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Event tabSelectedEvent(tab As IndronTabPage)
#End Region

    Public Sub initConfigBase(title As String, tooltiptext As String)
        Me.title = title
        Me.btnTab.tab = Me
        Me.configTabButton(tooltiptext)
    End Sub

    Private Sub configTabButton(tooltiptext As String)
        Me.btnTab.Text = Me.title
        Me.btnTab.Width = Me.tabButtonWidth
        Me.btnTab.Height = 26
        Me.btnTab.Margin = New Padding(0)
        Me.btnTab.ToolTipTextP = tooltiptext

    End Sub


    Private Sub btnTab_CloseEvent(tab As IndronTabPage) Handles btnTab.ButtonCloseClickEvent
        Dim cancel As Boolean = False
        RaiseEvent tabPageClosingEvent(Me, cancel)

        If cancel = True Then
            'cancel close tab
            Exit Sub
        End If
        Me.tabCtrolContainer.TabPageRemoveBase(Me)

        If Me.tabType = tabTypeEnum.normal Then
            TabButtonRemove()
            Me.Dispose()
        ElseIf Me.tabType <> tabTypeEnum.hideOnClose Then
            TabButtonRemove()
            Me.Dispose()
        ElseIf Me.tabType = tabTypeEnum.hideOnClose Then
            'button continues
            Me.Hide()
        End If


    End Sub


    Private Sub btnTab_tabSelectedEvent(tab As IndronTabPage) Handles btnTab.buttonClickEvent
        Me.tabCtrolContainer.tabSelect(Me)
        'this is controlled by inherited class
    End Sub
    Private Sub btnTab_buttonDoubleClickEvent(tab As IndronTabPage) Handles btnTab.buttonDoubleClickEvent
        'this is controlled by inherited class
    End Sub

    Public Sub TabButtonRemove()
        Me.btnTab.Visible = False
        Me.btnTab.Dispose()
    End Sub

    Private Sub btnTab_buttonMoveOutEvent(tab As IndronTabPage) Handles btnTab.buttonMoveOutEvent
        'this is controlled by inherited class
    End Sub
End Class
