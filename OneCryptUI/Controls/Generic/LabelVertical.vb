Public Class LabelVertical
    Inherits Control

    Public isSelected As Boolean = False

    Private _caption As String
    Public Property caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
            reDraw()
        End Set
    End Property
    Public Overrides Property Text() As String
        Get
            Return Me.caption
        End Get
        Set(ByVal value As String)
            _caption = value
            reDraw()
        End Set
    End Property


    Private _rotatedSize As Size
        Private Property rotatedSize() As Size
            Get
                Return _rotatedSize
            End Get
            Set(ByVal value As Size)
                _rotatedSize = value
            End Set
        End Property

    Private _upToDown As Boolean = True
    Public Property upToDown() As Boolean
            Get
                Return _upToDown
            End Get
            Set(ByVal value As Boolean)
                _upToDown = value
                MyBase.Invalidate()
            End Set
        End Property

        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
                reDraw()
            End Set
        End Property

        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
                reDraw()
            End Set
        End Property

        Public Sub New()
        InitializeComponent()
        Me.Cursor = Cursors.Hand
    End Sub

    Dim padd As Integer = 10

    Private Sub reDraw()
            Dim s As Size = Me.CreateGraphics.MeasureString(caption, Me.Font).ToSize
        rotatedSize = New Size(s.Height + padd, s.Width + padd)
        Me.MinimumSize = rotatedSize
        Me.MaximumSize = rotatedSize
        MyBase.Invalidate()
        End Sub

        Protected Overrides Sub OnCreateControl()
            MyBase.OnCreateControl()
            caption = If(caption = "", Me.Name, caption)
            reDraw()
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            If upToDown Then
                e.Graphics.RotateTransform(90)
            e.Graphics.DrawString(caption, Me.Font, New SolidBrush(MyBase.ForeColor), 0, -rotatedSize.Width + padd)
        Else
                e.Graphics.RotateTransform(270)
            e.Graphics.DrawString(caption, Me.Font, New SolidBrush(MyBase.ForeColor), -rotatedSize.Height - padd, padd)
        End If

            e.Graphics.ResetTransform()
            MyBase.OnPaint(e)
        End Sub

    Private Sub LabelVertical_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Me.BackColor = Color.SteelBlue
    End Sub

    Private Sub LabelVertical_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        If isSelected = True Then
            'do not reset color
            Exit Sub
        End If
        Me.BackColor = Color.Silver
    End Sub

    Private Sub LabelVertical_Click(sender As Object, e As EventArgs) Handles Me.Click

    End Sub
End Class
