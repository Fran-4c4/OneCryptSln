Imports System.ComponentModel


Public Class LabelMessageEx
    Dim showAutoVar As Boolean = True
    Public Property ShowAuto As Boolean
        Get
            Return Me.showAutoVar
        End Get
        Set(value As Boolean)
            Me.showAutoVar = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return Me.lblMessage.Text
        End Get
        Set(value As String)

            Me.lblMessage.Text = value
            If Me.showAutoVar = True Then
                If value.Trim = "" Then
                    Me.Visible = False
                Else
                    Me.Visible = True
                End If
            End If
        End Set
    End Property

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Message = ""
        Me.Visible = False
    End Sub
End Class
