Public Class FrmDDBBInspector

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim da As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
            Try
                da.Connection.Open()

                Dim c As New System.Data.SqlServerCe.SqlCeCommand(Me.RichTextBox1.Text, da.Connection)
                Dim res As Integer = c.ExecuteNonQuery()
                da.Connection.Close()
                Me.RichTextBox2.AppendText("Result of query: " & res & vbCr)
            Catch ex As Exception
                Me.RichTextBox2.AppendText("Result of query: " & ex.Message & vbCr)
            Finally
                If Not da Is Nothing Then
                    If Not da.Connection Is Nothing Then
                        da.Connection.Close()
                    End If
                End If

            End Try
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub
End Class