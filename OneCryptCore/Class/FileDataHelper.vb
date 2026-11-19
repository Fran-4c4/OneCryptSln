
Namespace ind.tools
    ''' <summary>
    ''' tools for manipulation of files
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FileDataHelper

        Public Enum fileTypeEnum As Integer
            unKnown = -1
            image = 0
            other = 1
            pdf = 2
        End Enum

        ''' <summary>
        ''' Open a file using asociated application. There is no validation
        ''' </summary>
        ''' <param name="filepath"></param>
        ''' <remarks></remarks>
        Public Shared Sub openFile(filepath As String)

            If filepath Is Nothing OrElse filepath = "" Then
                Throw New Exception("Filepath can not be empty")
            End If

            'check file extension
            Dim fileType As fileTypeEnum = fileTypeEnum.other
            If HasImageExtension(filepath) = True Then
                fileType = fileTypeEnum.image
            End If
            Select Case fileType
                Case fileTypeEnum.image
                    openFileBySystemProcess(filepath)
                    Exit Select
                Case fileTypeEnum.other
                    openFileBySystemProcess(filepath)
                    Exit Select
                Case Else
                    openFileBySystemProcess(filepath)
            End Select


        End Sub

        ''' <summary>
        ''' return the image or nothing
        ''' </summary>
        ''' <param name="filename"></param>
        ''' <returns></returns>
        Function IsValidImage(filename As String) As System.Drawing.Image
            Try
                Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(filename)
                Return img
            Catch generatedExceptionName As OutOfMemoryException
                ' Image.FromFile throws an OutOfMemoryException  
                ' if the file does not have a valid image format or 
                ' GDI+ does not support the pixel format of the file. 
                ' 
                Return Nothing
            End Try

        End Function

        Public Shared Sub openFileBySystemProcess(filepath As String)
            Dim p As New System.Diagnostics.ProcessStartInfo()
            p.WindowStyle = ProcessWindowStyle.Hidden
            p.FileName = filepath
            p.UseShellExecute = True
            System.Diagnostics.Process.Start(p)
        End Sub

        Public Shared Function HasImageExtension(source As String) As Boolean
            source = source.ToLower
            If source.EndsWith(".png") _
                OrElse source.EndsWith(".jpg") OrElse source.EndsWith(".jpeg") OrElse source.EndsWith(".jfif") _
                OrElse source.EndsWith(".gif") _
                OrElse source.EndsWith(".bmp") _
                OrElse source.EndsWith(".tif") OrElse source.EndsWith(".tiff") Then
                Return True
            Else
                Return False
            End If

        End Function

    End Class
End Namespace

