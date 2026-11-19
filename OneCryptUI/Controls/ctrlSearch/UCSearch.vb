Imports System.Data

Public Class UCSearch


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            Dim searchString As String = Me.cboSearch.Text.Trim
            If searchString.Length < 4 Then
                ind.win.Msg.mExclamation("necesitas incluir mas de 3 caracteres")
            End If
            'search
            Dim DTInicio As DateTime = Date.Now
            Dim libraryDs As New LibraryDataSet
            Dim dtFiles As LibraryDataSet.T_FilesDataTable = ind.crypt.LibraryDataHelper.getResultsBySearchString("%" & searchString & "%")
            Debug.WriteLine("Time to search: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & dtFiles.Count & " elements")
            For Each dr As LibraryDataSet.T_FilesRow In dtFiles.Rows
                Debug.WriteLine(dr.Name)
            Next
            Debug.WriteLine("Time to load: " & Date.Now.Subtract(DTInicio).Duration().TotalMilliseconds & " for " & dtFiles.Count & " elements")
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnSearch_TextChanged(sender As Object, e As EventArgs) Handles btnSearch.TextChanged

    End Sub

    Private Sub btnSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles btnSearch.KeyUp
        AutoCompleteCombo_KeyUp(DirectCast(sender, ComboBox), e)

    End Sub

    Public Sub AutoCompleteCombo_KeyUp(ByVal cbo As ComboBox, ByVal e As KeyEventArgs)
        Dim sTypedText As String
        Dim iFoundIndex As Integer
        Dim oFoundItem As Object
        Dim sFoundText As String
        Dim sAppendText As String

        'Allow select keys without Autocompleting
        Select Case e.KeyCode
            Case Keys.Back, Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.Down
                Return
        End Select

        'Get the Typed Text and Find it in the list
        sTypedText = cbo.Text
        iFoundIndex = cbo.FindString(sTypedText)

        'If we found the Typed Text in the list then Autocomplete
        If iFoundIndex >= 0 Then

            'Get the Item from the list (Return Type depends if Datasource was bound 
            ' or List Created)
            oFoundItem = cbo.Items(iFoundIndex)

            'Use the ListControl.GetItemText to resolve the Name in case the Combo 
            ' was Data bound
            sFoundText = cbo.GetItemText(oFoundItem)

            'Append then found text to the typed text to preserve case
            sAppendText = sFoundText.Substring(sTypedText.Length)
            cbo.Text = sTypedText & sAppendText

            'Select the Appended Text
            cbo.SelectionStart = sTypedText.Length
            cbo.SelectionLength = sAppendText.Length

        End If

    End Sub


    Public Sub AutoCompleteCombo_Leave(ByVal cbo As ComboBox)
        Dim iFoundIndex As Integer

        iFoundIndex = cbo.FindStringExact(cbo.Text)

        cbo.SelectedIndex = iFoundIndex

    End Sub
End Class
