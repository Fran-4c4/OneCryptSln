Imports System.ComponentModel
Imports System.Drawing

Public Class IndronButton

    Dim m_Title As String = "Title"
    Dim m_TitleSub As String = "SubTitle"
    Public RemoveItemToolTip As String = ""

    Public Property title As String

        Get
            Return Me.m_Title
        End Get
        Set(value As String)
            Me.m_Title = value
            Me.Invalidate()
        End Set
    End Property

    Public Property titleSub As String

        Get
            Return Me.m_TitleSub
        End Get
        Set(value As String)
            Me.m_TitleSub = value
            Me.Invalidate()
        End Set
    End Property


    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.SuspendLayout()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        MyBase.BackColor = Color.Transparent
        Me.m_icon = My.Resources.project_menu
        If Not Me.m_icon Is Nothing Then
            Me.m_IconSize = Me.m_icon.Size
        End If
        ResetColor()
        Me.Cursor = Cursors.Hand
        Me.ResumeLayout()
    End Sub


    ''' <summary>
    ''' When click on button raise an event to indicate the tab selected
    ''' </summary>
    ''' <param name="tab"></param>
    ''' <remarks></remarks>
    Public Event buttonClickEvent(sender As Object, e As System.Windows.Forms.MouseEventArgs)
    Public ttip As ToolTip

#Region "   Visual Configuration"



#Region "       Visual Behavior"
    Private m_BackHighColor As Color
    Private m_BackLowColor As Color
    Private m_BorderColor As Color
    Private m_ForeColorLeave As Color = Color.DimGray
    Private m_ForeColorHover As Color = Color.DarkGray
    Private m_Hover As Boolean = False

    Private m_FontBoldOnSelect As Boolean
    Private m_IconSize As Size
    Private m_RenderMode As ToolStripRenderMode = ToolStripRenderMode.ManagerRenderMode

    Public m_icon As Bitmap
    Public Property Icon As Bitmap

        Get
            Return Me.m_icon
        End Get
        Set(value As Bitmap)
            Me.m_icon = value
            If Not Me.m_icon Is Nothing Then
                Me.m_IconSize = Me.m_icon.Size
            Else
                Me.m_IconSize.Width = 0
                Me.m_IconSize.Height = 0
            End If
            Me.Invalidate()
        End Set
    End Property

#End Region

    Friend RenderColors As New IndBaseColors

    Public Property defaultBackHighColor As Color = Color.WhiteSmoke
    Public Property defaultBackLowColor As Color = Color.LightSteelBlue
    Public Property defaultBorderColor As Color = Color.LightGray
    Public Property ForeColorLeave As Color = Color.DimGray
    Public Property ForeColorHover As Color = Color.Black
    Public Property HoverActivatedToViewBehavior As Boolean

        Get
            Return Me.m_Hover
        End Get
        Set(value As Boolean)
            Me.m_Hover = value
            Me.ResetColor()
            Me.Invalidate()
        End Set
    End Property

    <DebuggerStepThrough()> _
    Public Sub ResetColor()
        m_BackHighColor = Me.defaultBackHighColor
        m_BackLowColor = Me.defaultBackLowColor
        m_BorderColor = Me.defaultBorderColor
        m_ForeColorLeave = Me.ForeColorLeave
        m_ForeColorHover = Me.ForeColorHover
    End Sub

    <DebuggerStepThrough()> _
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        Dim rec As New Rectangle
        rec.Size = New Size(Me.Width - 1, Me.Height - 1)

        If m_Hover Then
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.FillRectangle(New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, Me.Height), RenderColors.ControlButtonBackHighColor(m_RenderMode, m_BackHighColor), RenderColors.ControlButtonBackLowColor(m_RenderMode, m_BackLowColor)), rec)
            e.Graphics.DrawRectangle(New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BorderColor)), rec)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.Default
        Else
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            'e.Graphics.DrawRectangle(New Pen(RenderColors.ControlButtonBorderColor(m_RenderMode, m_BorderColor)), rec)
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.Default
        End If

        drawIcon(e.Graphics)
        Me.DrawTitleText(e.Graphics, Me.m_Title)
        Me.DrawSubTitleText(e.Graphics, Me.m_TitleSub)
        CloseIconDraw(e.Graphics)
    End Sub
    <DebuggerStepThrough()> _
    Private Function drawIcon(ByVal g As Graphics) As Point
        If Me.m_icon Is Nothing Then
            Exit Function
        End If
        Dim p As New Point
        p.X = 2
        p.Y = CInt(Me.Height / 2 - Me.m_icon.Height / 2)
        g.DrawImage(Me.m_icon, New Rectangle(p.X, p.Y, Me.m_IconSize.Width, Me.m_IconSize.Height))
    End Function
    ''' <summary>
    ''' True if we want to show the closebutton
    ''' </summary>
    ''' <remarks></remarks>
    Public CloseButtonVisible As Boolean = True
    Private CloseIconMouseOver As Boolean = False
    Private CloseIconMouseOverPrev As Boolean = False
    Dim CloseIcon_hover As Image = My.Resources.removeItem32
    Dim CloseIcon_gray As Image = My.Resources.removeItem_gray_32
    <DebuggerStepThrough()> _
    Private Sub CloseIconDraw(ByRef eGraphics As Graphics)
        If CloseButtonVisible = False Then
            Exit Sub
        End If
        Dim iconPos As PointF = CloseIconGetPosition()

        If CloseIconMouseOver Then
            eGraphics.DrawImage(CloseIcon_hover, New RectangleF(iconPos.X, iconPos.Y, 32, 32))
        Else
            eGraphics.DrawImage(CloseIcon_gray, New RectangleF(iconPos.X, iconPos.Y, 32, 32))
        End If
    End Sub
    <DebuggerStepThrough()> _
    Private Sub CloseButtonInvalidate()
        Dim iconPos As PointF = CloseIconGetPosition()
        Dim r As New Region(New RectangleF(iconPos.X, iconPos.Y, 32, 32))
        Me.Invalidate(r)
        r.Dispose()
        r = Nothing
    End Sub
    <DebuggerStepThrough()> _
    Private Function CloseIconGetPosition() As PointF
        Dim p As New PointF
        p.X = Me.Width - 34
        p.Y = CSng(Me.Height / 2 - 16)
        Return p
    End Function
    <DebuggerStepThrough()> _
    Private Sub ControlButton_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If CloseButtonVisible = False Then
            Exit Sub
        End If
        Static State As Boolean = False
        If m_Hover = True Then
            ' verify if the mouse is over the close button
            Dim iconPos As PointF = Me.CloseIconGetPosition

            CloseIconMouseOver = e.X >= iconPos.X And e.X <= iconPos.X + 32 And e.Y >= iconPos.Y And e.Y <= iconPos.Y + 32
            If CloseIconMouseOver And CloseIconMouseOverPrev = False Then
                CloseIconMouseOverPrev = True
                'State = CloseIconMouseOver
                Me.CloseButtonInvalidate()
                If Not Me.ttip Is Nothing Then

                    ttip.SetToolTip(Me, Me.RemoveItemToolTip)
                End If

            ElseIf CloseIconMouseOver = False And CloseIconMouseOverPrev = True Then
                CloseIconMouseOverPrev = False
                If Not ttip Is Nothing Then
                    ttip.RemoveAll()
                End If
            End If
        Else
            CloseIconMouseOver = False
        End If
    End Sub



    Dim titleMaxHeight As Integer = 26
    <DebuggerStepThrough()> _
    Private Sub DrawTitleText(ByVal g As Graphics, ttext As String)
        Dim f As Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Dim b As Brush = New SolidBrush(CType(IIf(m_Hover, m_ForeColorHover, m_ForeColorLeave), Color))
        Dim bounds As RectangleF = New RectangleF(CInt(IIf(Me.m_icon Is Nothing, 0, m_IconSize.Width)) + 2, 0, Width - CInt(IIf(Me.m_icon Is Nothing, 0, m_IconSize.Height)) - 5, titleMaxHeight)
        Dim MyFormat As StringFormat = New StringFormat
        MyFormat.FormatFlags = StringFormatFlags.NoWrap
        MyFormat.LineAlignment = StringAlignment.Center
        MyFormat.Trimming = StringTrimming.EllipsisCharacter
        g.DrawString(ttext, f, b, bounds, MyFormat)
        MyFormat.Dispose()
        b.Dispose()
        f.Dispose()
        MyFormat = Nothing
        b = Nothing
        f = Nothing
    End Sub
    <DebuggerStepThrough()> _
    Private Sub DrawSubTitleText(ByVal g As Graphics, ttext As String)
        Dim f As Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Dim b As Brush = New SolidBrush(CType(IIf(m_Hover, m_ForeColorHover, m_ForeColorLeave), Color))
        Dim bounds As RectangleF = New RectangleF(CInt(IIf(Me.m_icon Is Nothing, 0, m_IconSize.Width)) + 2, titleMaxHeight, Width - CInt(IIf(Me.m_icon Is Nothing, 0, m_IconSize.Height)) - 5, Me.Height - titleMaxHeight - 4)
        Dim MyFormat As StringFormat = New StringFormat
        MyFormat.FormatFlags = StringFormatFlags.NoWrap
        MyFormat.LineAlignment = StringAlignment.Center
        MyFormat.Trimming = StringTrimming.EllipsisCharacter
        g.DrawString(ttext, f, b, bounds, MyFormat)
        MyFormat.Dispose()
        b.Dispose()
        f.Dispose()
        MyFormat = Nothing
        b = Nothing
        f = Nothing
    End Sub

    <DebuggerStepThrough()> _
    Private Sub ControlButton_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        If m_Hover = True Then
            CloseIconMouseOver = False
            m_Hover = False

            Invalidate()
        End If

    End Sub
    <DebuggerStepThrough()> _
    Private Sub Me_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        m_Hover = True
        Invalidate()
    End Sub
    <DebuggerStepThrough()> _
    Private Sub Me_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        m_Hover = False
        CloseIconMouseOver = False
        Invalidate()
    End Sub




#Region "close by pic"
    ''' <summary>
    ''' Event raised when click close button 
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ButtonCloseClickEvent(sender As Object, e As System.Windows.Forms.MouseEventArgs)

    Dim mouseClickTwice As Boolean = False
    Private Sub UCTabControl_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        mouseClickTwice = True
    End Sub
    ''' <summary>
    ''' when the user click on the tabpage header on tabcontrol
    ''' </summary>
    ''' <param name="e"></param>
    <DebuggerStepThrough()> _
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Clicks > 1 Or mouseClickTwice = True Then
            mouseClickTwice = False
            Exit Sub
        End If

        Dim pointclicked As Point = New Point(e.X, e.Y)
        Dim actionClicked As actionClickedEnum
        actionClicked = Me.controlCloseActionClick(pointclicked)
        If actionClicked = actionClickedEnum.none Then 'if none we asume other zone of the control so it is a click event
            RaiseEvent buttonClickEvent(Me, e)
        ElseIf actionClicked = actionClickedEnum.close Then
            RaiseEvent ButtonCloseClickEvent(Me, e)
        End If
    End Sub
    Public Enum actionClickedEnum As Byte
        none = 0
        click = 1
        close = 2

    End Enum
    <DebuggerStepThrough()> _
    Private Function controlCloseActionClick(ByVal pointclicked As Point) As actionClickedEnum
        'the position where the close icon was drawed
        Dim recClicked As New Rectangle
        recClicked.Width = 32
        recClicked.Height = 32
        recClicked.Offset(Me.Width - recClicked.Width - 2, 2)

        If recClicked.Contains(pointclicked) Then
            Return actionClickedEnum.close
        End If
        Return actionClickedEnum.none
    End Function

    Private Sub IndronButton_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

    End Sub
#End Region


#End Region

End Class

