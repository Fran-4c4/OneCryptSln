Imports System.Windows.Forms


''' <summary>
''' Form showed to the user when a project need to be saved before exit or load another operation
''' </summary>
''' <remarks></remarks>
Public Class frmDlgSaveProject
    Inherits FrmAppEvents

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.Text = Global.ind.lang.UILang.frmOptions_text

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = OK_Button.DialogResult
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = Cancel_Button.DialogResult
        Me.Close()
    End Sub

    Private Sub btn_IgnoreChanges_Click(sender As Object, e As EventArgs) Handles btn_IgnoreChanges.Click
        Me.DialogResult = btn_IgnoreChanges.DialogResult
        Me.Close()
    End Sub

    Private Sub frmDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myAppEvents_LanguageChangedEvent()
    End Sub

    Public Enum FocusButton As Byte
        cancelBtn = 0
        OkBtn = 1
    End Enum

    Public Sub setFocusOnBtn(focusOn As FocusButton)
        If focusOn = FocusButton.cancelBtn Then
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

