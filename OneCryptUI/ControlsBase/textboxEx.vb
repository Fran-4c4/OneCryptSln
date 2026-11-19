Imports System.ComponentModel

''' <summary>
''' Control with label and textbox
''' </summary>
''' <remarks></remarks>
Public Class textboxEx


#Region "Events"
    Public Event GotFocusEx(sender As Object, e As EventArgs)
    Public Event LostFocusEx(sender As Object, e As EventArgs)
#End Region
    ''' <summary>
    ''' color of the external line when focus
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private FocusColor As Color = Color.GreenYellow
    ''' <summary>
    ''' data in text box
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True)>
    Public ReadOnly Property TxtBoxObj As TextBox
        Get
            Return Me.TxtBox.TextBox1
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
            Return Me.TxtBox.TextBox1.Text
        End Get
        Set(value As String)
            Me.TxtBox.TextBox1.Text = value
        End Set
    End Property

    Public Property labelBox As String
        Get
            Return Me.lbl.Text
        End Get
        Set(value As String)
            Me.lbl.Text = value
        End Set
    End Property

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TxtBox.textBoxEnter
        Me.lbl.ForeColor = Color.Black
        RaiseEvent GotFocusEx(Me, e)
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TxtBox.textBoxLeave
        Me.lbl.ForeColor = Color.DimGray
        RaiseEvent LostFocusEx(Me, e)
    End Sub


End Class
