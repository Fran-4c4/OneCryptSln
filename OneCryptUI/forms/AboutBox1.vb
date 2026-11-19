Public NotInheritable Class AboutBox1



    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            ' Set the title of the form.
            Me.Text = String.Format(ind.lang.UILang.aboutbox_about, ind.cte.AppConst.AppName)
            Me.LabelProductName.Text = ind.cte.AppConst.AppName
            Me.LabelVersion.Text = String.Format(ind.lang.UILang.aboutbox_Version, My.Application.Info.Version.ToString)
            Me.LabelCopyright.Text = My.Application.Info.Copyright
            Me.TextBoxDescription.Text = My.Application.Info.Description
            Me.lblAutor.Text = ind.lang.UILang.aboutbox_Autor
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally

            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
