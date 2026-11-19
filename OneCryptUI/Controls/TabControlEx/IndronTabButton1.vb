Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' button associated to a tab
''' </summary>
Public Class IndronTabButton1

#Region "Events"
    ''' <summary>
    ''' only for debug purposes in mouse events
    ''' </summary>
    Private debugMouse As Boolean = False
    ''' <summary>
    ''' When click on button raise an event to indicate the tab selected
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Event buttonClickEvent(tab As IndronTabPage)
    ''' <summary>
    ''' When click on button raise an event to indicate the tab selected
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Event buttonDoubleClickEvent(tab As IndronTabPage)
    ''' <summary>
    ''' raised when the button is moved outside as drag
    ''' </summary>
    ''' <param name="tab"></param>
    Public Event buttonMoveOutEvent(tab As IndronTabPage)
    ''' <summary>
    ''' Event raised when click close button 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ButtonCloseClickEvent(tab As IndronTabPage)

    Private toolTip As ToolTip
    Private toolTipText As String

#End Region
    Public Enum actionClickedEnum As Byte
        none = 0
        click = 1
        close = 2
    End Enum

    ''' <summary>
    ''' the tabpage this control belongs
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents tab As IndronTabPage

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.SuspendLayout()
        Me.btnClose_image_hover = My.Resources.closeTab
        Me.btnClose_image_Deactivated = My.Resources.closeDeactivated
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        MyBase.BackColor = Color.Transparent
        MyBase.DoubleBuffered = True
        ResetColor()
        'Me.AllowDrop = True

        Me.ResumeLayout()
    End Sub


#Region "   Visual Configuration"

#Region "       Visual Behavior"
    Private m_BackHighColor As Color
    Private m_BackLowColor As Color
    Private m_BorderColor As Color
    Private m_ForeColorDisabled As Color = Color.DimGray
    Private m_ForeColorSelected As Color = Color.White
    Private m_isSelected As Boolean = False
    Public Property IsSelected As Boolean

        Get
            Return Me.m_isSelected
        End Get
        Set(value As Boolean)
            Me.m_isSelected = value
            Me.ResetColor()
            Invalidate()
        End Set
    End Property
    Private m_Hover As Boolean = False

    Private m_IconSize As New Size(0, 0)
    Private m_RenderMode As ToolStripRenderMode = ToolStripRenderMode.ManagerRenderMode

    Private _icon As Bitmap
    'Clsoe button
    Private m_CloseButtonVisible As Boolean = True
    Private m_CloseButtonSize As New Size(16, 16)
    Public Property btnClose_image_hover As Bitmap
    Public Property btnClose_image_Deactivated As Bitmap
#End Region

#Region "Color vars"
    Friend RenderColors As New IndBaseColors
    ''' <summary>
    ''' Top back color gradient when deselected
    ''' </summary>
    Public defaultBackHighColor As Color = SystemColors.ControlLightLight
    ''' <summary>
    '''  Bottom back color gradient when deselected
    ''' </summary>
    Public defaultBackLowColor As Color = SystemColors.ControlDark
    Public defaultBorderColor As Color = SystemColors.ControlDarkDark
    ''' <summary>
    ''' Top back color gradient when selected
    ''' </summary>
    ''' <returns></returns>
    Public Property SelectedBackHighColor As Color = Color.Salmon
    ''' <summary>
    ''' Bottom back color gradient when selected
    ''' </summary>
    ''' <returns></returns>
    Public Property SelectedBackLowColor As Color = Color.IndianRed
    Public Property SelectedBorderColor As Color = SystemColors.ControlDarkDark

    Public Property Icon As Bitmap
        Get
            Return _icon
        End Get
        Set(value As Bitmap)
            _icon = value
            If value Is Nothing Then
                m_IconSize.Width = 0
                m_IconSize.Height = 0
            Else
                m_IconSize.Width = 16 ' value.Width
                m_IconSize.Height = 16 ' value.Height
            End If

        End Set
    End Property

    Public Property ToolTipTextP As String
        Get
            Return Me.toolTipText
        End Get
        Set(value As String)
            If Not value Is Nothing AndAlso value.Trim.Length > 0 Then
                Me.toolTip = New ToolTip
                Me.toolTip.Active = True
                Me.toolTip.SetToolTip(Me, value)
            Else
                Me.toolTip = Nothing
            End If
            toolTipText = value
        End Set
    End Property
#End Region


    Public Sub ResetColor()
        If m_isSelected Then
            m_BackHighColor = Me.SelectedBackHighColor
            m_BackLowColor = Me.SelectedBackLowColor
            m_BorderColor = Me.SelectedBorderColor
        Else
            m_BackHighColor = Me.defaultBackHighColor
            m_BackLowColor = Me.defaultBackLowColor
            m_BorderColor = Me.defaultBorderColor
        End If

    End Sub


    <DebuggerStepThrough()>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Debug.WriteLine("indrontabbutton onpaint")
        Dim rec As New Rectangle
        rec.Size = New Size(Me.Width - 1, Me.Height - 1)

        If m_isSelected Then
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.FillRectangle(New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, Me.Height), RenderColors.ControlButtonBackHighColor(m_RenderMode, m_BackHighColor), RenderColors.ControlButtonBackLowColor(m_RenderMode, m_BackLowColor)), rec)
            'Dim pen1 As New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BorderColor))
            'Dim p1 As New Point(0, 0)
            Dim pen2 As New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BackLowColor))
            e.Graphics.DrawLine(pen2, x1:=0, y1:=Me.Height - 1, x2:=Me.Width, y2:=Me.Height - 1) 'bottom line equal to tabcontrol top
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.Default

        ElseIf m_Hover Then

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.FillRectangle(New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, Me.Height), RenderColors.ControlButtonBackHighColor(m_RenderMode, m_BackHighColor), RenderColors.ControlButtonBackLowColor(m_RenderMode, m_BackLowColor)), rec)
            e.Graphics.DrawRectangle(New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BorderColor)), rec)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.Default
        Else
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.DrawRectangle(New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BorderColor)), rec)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.Default
        End If

        CloseIconDraw(e.Graphics)
        Me.LeftIconDraw(e.Graphics)
        Me.DrawText(e.Graphics)

    End Sub
    <DebuggerStepThrough()>
    Private Sub CloseIconDraw(ByRef eGraphics As Graphics)
        Dim iconPos As PointF = getCloseIconPosition()

        If IsMouseOverCloseButton Then
            eGraphics.DrawImage(btnClose_image_hover, New RectangleF(iconPos.X, iconPos.Y, 16, 16))
        Else
            eGraphics.DrawImage(btnClose_image_Deactivated, New RectangleF(iconPos.X, iconPos.Y, 16, 16))
        End If
    End Sub
    <DebuggerStepThrough()>
    Private Sub LeftIconDraw(ByRef eGraphics As Graphics)
        If _icon Is Nothing Then
            Exit Sub
        End If
        eGraphics.DrawImage(_icon, New RectangleF(1, 5, 16, 16))

    End Sub

    <DebuggerStepThrough()>
    Private Sub DrawText(ByVal g As Graphics)
        Dim f As Font = New Font(Font, CType(IIf(m_isSelected, FontStyle.Bold, FontStyle.Regular), FontStyle))
        Dim b As Brush = New SolidBrush(CType(IIf(m_isSelected Or m_Hover, m_ForeColorSelected, m_ForeColorDisabled), Color))
        Dim bounds As RectangleF = New RectangleF(m_IconSize.Width + 2, 1, Width - m_IconSize.Height - 5 - m_CloseButtonSize.Width, Me.DisplayRectangle.Height)
        Dim MyFormat As StringFormat = New StringFormat
        MyFormat.FormatFlags = StringFormatFlags.NoWrap
        MyFormat.LineAlignment = StringAlignment.Center
        MyFormat.Trimming = StringTrimming.EllipsisCharacter
        g.DrawString(Me.Text, f, b, bounds, MyFormat)
        MyFormat.Dispose()
        b.Dispose()
        f.Dispose()
        MyFormat = Nothing
        b = Nothing
        f = Nothing
    End Sub
    <DebuggerStepThrough()>
    Private Sub ControlButton_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        m_Hover = False
        IsMouseOverCloseButton = False
        invalidateCloseButton()
    End Sub
    <DebuggerStepThrough()>
    Private Sub MdiTab_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        m_Hover = True
        Invalidate()
    End Sub
    <DebuggerStepThrough()>
    Private Sub MdiTab_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        m_Hover = False
        IsMouseOverCloseButton = False
        Invalidate()
    End Sub
    <DebuggerStepThrough()>
    Private Sub invalidateCloseButton()
        Dim iconPos As PointF = getCloseIconPosition()
        Dim r As New Region(New RectangleF(iconPos.X, iconPos.Y, m_CloseButtonSize.Width, m_CloseButtonSize.Height))
        Me.Invalidate(r)
        r.Dispose()
        r = Nothing
    End Sub



    ''' <summary>
    ''' the minimum number in pixels to raise that the control is moved outside tabcontrol
    ''' </summary>
    Public distanceMinToRaiseMove As Integer = 20

    Private Function calculateDistanceToRaiseDragDropMove(nextLocation As System.Drawing.Point) As Boolean

        Dim distX As Integer = Math.Abs(ctrlOffset.X - nextLocation.X)
        Dim distY As Integer = Math.Abs(ctrlOffset.Y - nextLocation.Y)
        If distX > distanceMinToRaiseMove Or distY > Me.Height Then
            Return True
        End If
        Return False

    End Function

    <DebuggerStepThrough()>
    Private Function getCloseIconPosition() As PointF
        Dim p As PointF
        p.X = Me.Width - m_CloseButtonSize.Width - 2
        p.Y = CSng(Me.Height / 2 - m_CloseButtonSize.Height / 2)
        Return p
    End Function

    ''' <summary>
    ''' indicates that mouse is over close button
    ''' </summary>
    Private IsMouseOverCloseButton As Boolean = False

    Dim dragStart As Boolean = False
    Dim dragInProcess As Boolean = False
    Private mouseOffset As System.Drawing.Point
    Private ctrlOffset As System.Drawing.Point

    Dim mouseClickTwice As Boolean = False

    <DebuggerStepThrough()>
    Private Sub UCTabControl_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        If debugMouse = True Then
            Debug.WriteLine("MouseDoubleClick ")
        End If

        mouseClickTwice = True
    End Sub

    Private Sub ControlButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If debugMouse = True Then
            Debug.WriteLine("MouseDown ")
        End If
        Me.Cursor = Cursors.Default
        If Me.AllowDrop = True And e.Button = MouseButtons.Left Then
            If debugMouse = True Then
                Debug.WriteLine("MouseDown  x,y " & e.Location.ToString)
            End If
            mouseOffset = New System.Drawing.Point(e.Location.X, e.Location.Y)
            ctrlOffset.X = Me.Left
            ctrlOffset.Y = Me.Top
            Me.dragStart = True
        Else
            Me.dragStart = False
            Me.dragInProcess = False
            Me.Cursor = Cursors.Default
        End If


    End Sub


    <DebuggerStepThrough()>
    Private Sub ControlButton_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If debugMouse = True Then
            Debug.WriteLine("MouseMove dragStart:" & dragStart & " location:" & e.Location.ToString & "  e.Button: " & e.Button)
        End If

        If dragStart = True And e.Button = MouseButtons.Left And m_Hover = True Then
            If e.X = mouseOffset.X And e.Y = mouseOffset.Y Then
                If debugMouse = True Then
                    Debug.WriteLine("MouseMove same location, exit")
                End If

                Exit Sub
            End If

            Me.Cursor = Cursors.NoMove2D
            dragInProcess = True
            If debugMouse = True Then
                Debug.WriteLine("MouseMove dragStart" & dragStart & " mousoffset: " & mouseOffset.ToString & " x,y " & e.X & " " & e.Y & " e.Location: " & e.Location.ToString)
            End If

            ' calculations of control's new position
            Dim newLocationOffset As New System.Drawing.Point(e.Location.X - mouseOffset.X, e.Location.Y - mouseOffset.Y)

            Dim ctrl As Control = CType(sender, Control)
            ctrl.Left += newLocationOffset.X
            ctrl.Top += newLocationOffset.Y
            ' Debug.WriteLine("MouseMove dragStart " & Me.ctrlOffset.ToString & " new pos: " & Me.Location.ToString)
            Dim dist As Boolean = calculateDistanceToRaiseDragDropMove(ctrl.Location)
            If dist = True Then
                dragStart = False
                Me.Cursor = Cursors.Default
                RaiseEvent buttonMoveOutEvent(Me.tab)
            End If
            Exit Sub

        Else
            dragInProcess = False
            dragStart = False
            Me.Cursor = Cursors.Default
        End If

        Static State As Boolean
        If m_CloseButtonVisible = True And m_Hover = True Then
            ' verify if the mouse is over the close button
            Dim iconPos As PointF = getCloseIconPosition()

            IsMouseOverCloseButton = e.X >= iconPos.X And e.X <= iconPos.X + m_CloseButtonSize.Width - 1 And e.Y >= iconPos.Y And e.Y <= iconPos.Y + m_CloseButtonSize.Height - 1
            If debugMouse = True Then
                Debug.WriteLine("State==IsMouseOverCloseButton " & IsMouseOverCloseButton)
            End If


            If State <> IsMouseOverCloseButton Then
                State = IsMouseOverCloseButton
                invalidateCloseButton()
                If debugMouse = True Then
                    Debug.WriteLine("IsMouseOverCloseButton" & IsMouseOverCloseButton)
                End If

                If IsMouseOverCloseButton = True Then
                    tooltipShow(False)
                Else
                    tooltipShow()
                End If
            Else

                If IsMouseOverCloseButton = True Then
                    tooltipShow(False)
                Else
                    tooltipShow()
                End If
            End If

        ElseIf m_Hover = True Then
            tooltipShow()

        End If
    End Sub

    Public Sub tooltipShow(Optional show As Boolean = True)
        If Me.toolTip Is Nothing Then
            Exit Sub
        End If
        If debugMouse = True Then
            Debug.WriteLine("tooltipShow show=" & show & " Me.toolTip.Active  " & Me.toolTip.Active)
        End If

        If Me.toolTip.Active = False And show = True Then
            Me.toolTip.Active = True
        ElseIf Me.toolTip.Active = True And show = False Then

            Me.toolTip.Active = False
        End If
    End Sub

    ''' <summary>
    ''' when the user click on the tabpage header on tabcontrol
    ''' </summary>
    ''' <param name="e"></param>
    <DebuggerStepThrough()>
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If debugMouse = True Then
            Debug.WriteLine("OnMouseUp: " & e.Location.ToString)
        End If

        If dragInProcess = True And e.Button = MouseButtons.Left Then
            Me.Cursor = Cursors.Default
            If Me.calculateDistanceToRaiseDragDropMove(Me.Location) = False Then
                'reset button position to original location
                Me.Left = Me.ctrlOffset.X
                Me.Top = Me.ctrlOffset.Y
            End If
            Me.dragStart = False
            Me.dragInProcess = False
            Exit Sub
        End If

        Me.dragStart = False
        dragInProcess = False

        If e.Button <> MouseButtons.Left Then
            Exit Sub
        End If

        If mouseClickTwice = True Then
            RaiseEvent buttonDoubleClickEvent(Me.tab)
            mouseClickTwice = False
            Exit Sub
        ElseIf e.Clicks > 1 Then
            mouseClickTwice = False
            Exit Sub
        End If


        Dim actionClicked As actionClickedEnum = Me.controlCloseActionClick(e.Location)
        If actionClicked = actionClickedEnum.none Then 'if none we asume other zone of the control so it is a click event
            'Debug.WriteLine("indrontabbutton m_isSelected=" & m_isSelected)
            If m_isSelected = False Then
                m_isSelected = True
                Me.ResetColor()
            End If
            RaiseEvent buttonClickEvent(Me.tab)

        ElseIf actionClicked = actionClickedEnum.close Then

            RaiseEvent ButtonCloseClickEvent(Me.tab)
        End If
    End Sub
#End Region

#Region "tab operations"

    Private Function controlCloseActionClick(ByVal pointclicked As Point) As actionClickedEnum
        'the position where the close icon was drawed
        Dim recClicked As New Rectangle
        recClicked.Width = 16
        recClicked.Height = 16
        recClicked.Offset(Me.Width - recClicked.Width - 2, 2)

        If recClicked.Contains(pointclicked) Then
            Return actionClickedEnum.close
        End If
        Return actionClickedEnum.none
    End Function


#End Region

End Class



