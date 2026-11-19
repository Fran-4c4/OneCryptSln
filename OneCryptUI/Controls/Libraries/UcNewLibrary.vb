

''' <summary>
''' class to get the new name for a library
''' </summary>
''' <remarks></remarks>
Public Class UcNewLibrary

    Friend drLibrary As LibraryDataSet.T_LibraryRow

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Try
            If Me.txtLibName.Text.Trim = "" Then
                ind.win.Msg.mExclamation(ind.lang.UcNewLibraryLang.lblLibName_fillname)
                Exit Sub
            End If
            Dim mda As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
            Dim res As Integer = mda.Insert(Me.txtLibName.Text.Trim, 0, Date.Now, Date.Now)
            Dim dt As LibraryDataSet.T_LibraryDataTable = mda.GetDtLibraryByLibraryName(Me.txtLibName.Text.Trim)
            If dt.Count > 0 Then
                Me.drLibrary = DirectCast(dt.Rows(0), LibraryDataSet.T_LibraryRow)
            End If

            Me.ParentForm.DialogResult = DialogResult.OK
        Catch exCe As Data.SqlServerCe.SqlCeException
            Me.ParentForm.DialogResult = DialogResult.None
            If exCe.NativeError = 25016 Then
                ind.win.Msg.mError(ind.lang.UcNewLibraryLang.ExcepDuplicateName)
            End If
        Catch ex As Exception
            Me.ParentForm.DialogResult = DialogResult.None
            ind.win.Msg.mError(ex)
        End Try
    End Sub


    Private Sub UcNewLibrary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.lbl_Title.Text = ind.lang.UcNewLibraryLang.lbl_Title
        Me.lblLibName.Text = ind.lang.UcNewLibraryLang.lblLibName
        Me.btnContinue.Text = ind.lang.UcNewLibraryLang.SaveButton
    End Sub


    Public Function createNewLibraryDialog() As DialogResult
        Try
            Dim f As New Form
            f.Text = ind.lang.UcNewLibraryLang.lbl_Title
            Dim ucl As UcNewLibrary = Me
            f.Icon = My.Resources.lock_server
            f.MaximizeBox = False
            f.MinimizeBox = False
            f.Width = ucl.Width + 10
            f.Height = ucl.Height + 40
            f.StartPosition = FormStartPosition.CenterParent
            ucl.Parent = f
            ucl.Dock = DockStyle.Fill
            Dim sd As DialogResult = f.ShowDialog

            Return sd

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally

        End Try
        Return DialogResult.Cancel

    End Function



    Private Sub txtLibName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtLibName.KeyUp
        If e.KeyCode = Keys.Enter Then
            Me.btnContinue_Click(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' creates a new message dialog to create a new library
    ''' </summary>
    Public Shared Sub createNewLibraryMessageDialog()
        Try
            Dim ucl As New UcNewLibrary
            Dim sd As DialogResult = ucl.createNewLibraryDialog
            If sd = DialogResult.Cancel Then
                Debug.WriteLine("cancel")
            Else
                ind.win.AppEventsClass.LibraryNewCreatedEventRaise(ucl.drLibrary)
            End If

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub


End Class
