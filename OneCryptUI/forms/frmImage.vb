Public Class frmImage
    Private Sub frmImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub showImage(filePath As String)
        Try
            Me.Text = filePath

            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.WaitOnLoad = False

            PictureBox1.Image = Image.FromFile(filePath)
            checkResolution(PictureBox1.Image.Size)
            Me.Show()
        Catch ex As Exception
            Me.Label1.Text = "Error: " & ex.Message
            Me.Label1.Visible = True
        End Try
    End Sub

    Private Sub checkResolution(imgSize As Size)
        Dim intH As Integer = Screen.PrimaryScreen.Bounds.Height
        Dim intW As Integer = Screen.PrimaryScreen.Bounds.Width

        Me.Height = CInt(intH * 0.666)
        Me.Width = CInt(intW * 0.666)

        Dim formSize As Size = Me.Size

        Dim newSize As Size = frms.imgtools.ImageTools.GetImageToNewSize(imgSize, formSize, True)
        Me.Height = newSize.Height
        Me.Width = newSize.Width



    End Sub

End Class