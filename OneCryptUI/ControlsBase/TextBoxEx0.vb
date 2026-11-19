Imports System.ComponentModel



Public Class TextBoxEx0

#Region "Vars"
    ''' <summary>
    ''' color of the external line when focus
    ''' </summary>
    ''' <value></value>
    Private FocusColor As Color = Color.GreenYellow
#End Region

#Region "Events"

    Public Event textBoxEnter(sender As Object, e As EventArgs)
    Public Event textBoxLeave(sender As Object, e As EventArgs)
#End Region

    Private Sub TextBoxEx0_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.pnColor.BackColor = Me.BackColor

    End Sub

    ''' <summary>
    ''' data in text box
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True)>
    Public ReadOnly Property TextBoxObject As TextBox
        Get
            Return Me.TextBox1
        End Get
    End Property
    ''' <summary>
    ''' data in text box
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TextBoxText As String
        Get
            Return Me.TextBox1.Text
        End Get
        Set(value As String)
            Me.TextBox1.Text = value
        End Set
    End Property

 
    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        Me.pnColor.BackColor = FocusColor
        RaiseEvent textBoxEnter(sender, e)
    End Sub


    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        Me.pnColor.BackColor = Me.BackColor
        RaiseEvent textBoxLeave(sender, e)

    End Sub

End Class
