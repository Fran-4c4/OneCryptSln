
''' <summary>
''' class to show recent libraries loaded
''' </summary>
''' <remarks></remarks>
Public Class UCLibraryRecentElement

    Public WithEvents ucLibraryRecents As UCLibraryRecents

    Public WithEvents libraryObj As LibraryDataSet.T_LibraryRow

    Public Sub New(ucLibraryRecents As UCLibraryRecents, libraryObj As LibraryDataSet.T_LibraryRow)

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.IndronButton1.title = libraryObj.Name
        Me.libraryObj = libraryObj

        If libraryObj.IsLastAccessDateNull = False Then
            Me.IndronButton1.titleSub = libraryObj.LastAccessDate.ToLongDateString & " " & libraryObj.LastAccessDate.ToLongTimeString
        Else
            Me.IndronButton1.titleSub = ""
        End If

        Me.ucLibraryRecents = ucLibraryRecents
    End Sub

    Private Sub openLink(options As String)
        Try
            ind.win.AppEventsClass.LibraryOpenEventRaise(Me.libraryObj, options)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub IndronButton1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles IndronButton1.buttonClickEvent

        If e.Button = MouseButtons.Left And ModifierKeys = Keys.Control Then

            Me.openLink("openform")
        ElseIf e.Button = MouseButtons.Left Then

            Me.openLink("")
        End If

    End Sub

    Private Sub btnLnkDelete_clicked(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles IndronButton1.ButtonCloseClickEvent
        If e.Button = MouseButtons.Left Then
            Me.ucLibraryRecents.LibraryDelete(Me.libraryObj.id, Me.libraryObj.Name)
        End If

    End Sub
End Class
