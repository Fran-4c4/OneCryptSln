Imports System.Drawing
Imports System.Drawing.Drawing2D

Namespace frms.imgtools
    ''' <summary>
    ''' image tools
    ''' </summary>
    Public Class ImageTools

        ''' <summary>
        ''' resize image to new size
        ''' usage: Dim original As Image = Image.FromFile("C:\path\to\some.jpg")
        ''' Dim resized As Image = ResizeImage(original, New Size(1024, 768))
        ''' Dim memStream As MemoryStream = New MemoryStream()
        ''' resized.Save(memStream, ImageFormat.Jpeg)
        ''' </summary>
        ''' <param name="image">the image to be resized</param>
        ''' <param name="size"></param>
        ''' <param name="preserveAspectRatio">optional third parameter specifies whether to preserve the image's original aspect ratio</param>
        ''' <returns></returns>
        Public Shared Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image

            Dim newWidth As Integer
            Dim newHeight As Integer

            Dim newSize As Size = GetImageToNewSize(image.Size, size, preserveAspectRatio)
            newWidth = newSize.Width
            newHeight = newSize.Height


            Dim newImage As Image = New Bitmap(newWidth, newHeight)
            Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
            End Using

            Return newImage

        End Function

        ''' <summary>
        ''' resize image to new size
        ''' </summary>
        ''' <param name="image">the image to be resized</param>
        ''' <param name="size"></param>
        ''' <param name="preserveAspectRatio">optional third parameter specifies whether to preserve the image's original aspect ratio</param>
        ''' <returns></returns>
        Public Shared Function GetImageToNewSize(ByVal imageSize As Size, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Size

            Dim newWidth As Integer
            Dim newHeight As Integer
            If preserveAspectRatio Then
                Dim originalWidth As Integer = imageSize.Width
                Dim originalHeight As Integer = imageSize.Height
                Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
                Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
                Dim percent As Single = If(percentHeight < percentWidth,
                        percentHeight, percentWidth)
                newWidth = CInt(originalWidth * percent)
                newHeight = CInt(originalHeight * percent)
            Else
                newWidth = size.Width
                newHeight = size.Height
            End If

            Return New Size(newWidth, newHeight)



        End Function


    End Class
End Namespace

