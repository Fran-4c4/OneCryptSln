


''' <summary>
''' form to zip files
''' </summary>
Public Class frmZipFiles

    Private Sub form_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        Try

            Dim SaveFileDialog As New System.Windows.Forms.SaveFileDialog
            Try
                'Setup the open dialog.
                SaveFileDialog.FileName = ""
                SaveFileDialog.Title = ind.lang.UCCryptExplorerLang.OpenDialog_CompressFileNew
                SaveFileDialog.RestoreDirectory = True
                SaveFileDialog.Filter = String.Format("{0} |(*.*)|Zip |*.zip|Rar |*.rar|7z |*.7z", ind.lang.UILang.frmMain_mniProjectNew_Click_openfilter)
                SaveFileDialog.DefaultExt = "zip"
                SaveFileDialog.FilterIndex = 2

                'Find out if the user chose a file.
                If SaveFileDialog.ShowDialog = DialogResult.OK Then
                    Me.txtFilePath.TextBoxText = SaveFileDialog.FileName

                Else
                    Exit Sub

                End If

            Catch ex As Exception
                ind.win.Msg.mError(ex)
                Exit Sub
            Finally
                SaveFileDialog.Dispose()
            End Try


        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Try
            If Me.txtFilePath.TextBoxText.Trim.Length = 0 Then
                ind.win.Msg.mError(ind.lang.UILang.Zip_savepathNameRequired)
                DialogResult = DialogResult.None
                Exit Sub
            End If

            DialogResult = DialogResult.OK

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub frmZipFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.chkAutoexec.Text = ind.lang.UILang.Zip_chkAutoexec
        Me.chkopenpath.Text = ind.lang.UILang.Zip_chkopenpath
        Me.btnContinue.Text = ind.lang.UILang.Zip_btnContinue
        Me.Text = ind.lang.UILang.Zip_FormTitle
        Me.lbl_TitleH1.Text = ind.lang.UILang.Zip_MainTitle
        Me.lblFilePath.Text = ind.lang.UILang.Zip_savepathNameTitle

    End Sub
End Class