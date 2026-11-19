Imports System.Windows.Forms


''' <summary>
''' class to show dialogs
''' Usage: Use class ind.win.msg to configure
''' </summary>
''' <remarks></remarks>
Public Class frmDlg
    Inherits FrmAppEvents

    Public closeAfterClick As Boolean = True

    Dim originalMessage As String
    Public Property Message As String
        Get
            Return Me.originalMessage
        End Get
        Set(value As String)
            originalMessage = value
            If value.Length < 100 Then

            End If
            'Me.lblMessage.Text = value
            'Me.lblMessage.SelectionAlignment = HorizontalAlignment.Center
            Me.lblMessage.SelectedText = value

        End Set
    End Property


    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.OK_Button.Text = Global.ind.lang.frmDlgLang.ok_button
        Me.Cancel_Button.Text = Global.ind.lang.frmDlgLang.NO_button
        Me.chkDoNotShowMoremessages.Text = Global.ind.lang.frmDlgLang.chkDoNotShowMoremessages
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = OK_Button.DialogResult
        If closeAfterClick = True Then
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = Cancel_Button.DialogResult
        If closeAfterClick = True Then
            Me.Close()
        End If

    End Sub

    Private Sub frmDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myAppEvents_LanguageChangedEvent()
        'Me.DialogResult = Cancel_Button.DialogResult
    End Sub

    Public Enum FocusButtonEnum As Byte
        cancelBtn = 0
        OkBtn = 1
    End Enum

    Public Sub setFocusOnBtn(focusOn As FocusButtonEnum)
        If focusOn = FocusButtonEnum.cancelBtn Then
            Me.Cancel_Button.Focus()
            Me.Cancel_Button.TabIndex = 0
            Me.OK_Button.TabIndex = 1
        Else
            Me.OK_Button.Focus()
            Me.OK_Button.TabIndex = 0
            Me.Cancel_Button.TabIndex = 1
        End If
    End Sub
End Class

