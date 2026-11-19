


Option Strict On

Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates

Namespace ind.crypt
    Public Class Rijndael
        Inherits CryptoBase

        Dim key() As Byte
        Dim IV() As Byte

        ''' <summary>
        ''' Constructor de la Clase
        ''' </summary>
        ''' <param name="hash">Clave hash para encriptar/desencriptar</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal hash As String)
            If hash Is Nothing OrElse hash = "" Then
                Throw New Exception(ind.lang.CoreLang.Crypto_PwdEmptyException)
            End If
            'redimensionamos los arrays de bytes con la clave hash
            RedimBytes(hash)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="InicializationVector"></param>
        Public Sub New(key() As Byte, InicializationVector() As Byte)
            Me.key = key
            Me.IV = InicializationVector
        End Sub

        ''' <summary>
        ''' Procedimiento RedimBytes redimensiona los arrays de bytes en funcion del hash creado.
        ''' </summary>
        ''' <param name="hash">hash. Cadena con la password encriptada</param>
        ''' <remarks></remarks>
        Private Sub RedimBytes(ByVal hash As String)
            Dim ms As New MemoryStream
            Dim bs As New StreamWriter(ms)
            ReDim key(15)
            ReDim IV(15)

            bs.Write(hash)
            bs.Flush()
            ms.Position = 0
            ms.Read(key, 0, key.Length)
            ms.Read(IV, 0, IV.Length)
            ms.Close()
            bs.Close()
        End Sub


        ''' <summary>
        ''' Encrypt an array to a file
        ''' </summary>
        ''' <param name="bytearrayinput">bytes to encrypt</param>
        ''' <param name="fsOutput">result file encrypted</param>
        Public Sub encryptWithRijndael(bytearrayinput() As Byte, fsOutput As FileStream)

            ' Initialize a new instance of the CryptoStream class with the destination file stream,
            ' the cryptographic transformation to use, and the mode of the stream.
            Dim CryptoWriteStream As New CryptoStream(fsOutput, getEncriptor, CryptoStreamMode.Write)
            'Escribir el archivo cifrado
            CryptoWriteStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            CryptoWriteStream.Flush()
            CryptoWriteStream.Close()

        End Sub

        ''' <summary>
        ''' Encrypt a file
        ''' </summary>
        ''' <param name="fsInput">file stream to encrypt</param>
        ''' <param name="fsOutput">result file encrypted</param>
        Public Sub encryptWithRijndael(fsInput As FileStream, fsOutput As FileStream)
            'Leer el texto del archivo en la matriz de bytes
            Dim bytearrayinput(CInt(fsInput.Length - 1)) As Byte
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)
            closeFileStream(fsInput)
            encryptWithRijndael(bytearrayinput, fsOutput)
        End Sub


        ''' <summary>
        ''' Decrypt a filestream to a another filestream
        ''' </summary>
        ''' <param name="fsInput">input file stream to decrypt</param>
        ''' <param name="fsOutput">output file stream where decrypted data will be writen</param>
        Public Sub decryptFile(fsInput As FileStream, fsOutput As FileStream)

            ' Initialize a new instance of the CryptoStream class with the destination file stream,
            ' the cryptographic transformation to use, and the mode of the stream.
            Dim CryptoWriteStream As New CryptoStream(fsOutput, getDecriptor, CryptoStreamMode.Write)
            'Leer el texto del archivo en la matriz de bytes
            Dim bytearrayinput(CInt(fsInput.Length - 1)) As Byte
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)
            closeFileStream(fsInput)
            'Escribir el archivo cifrado 
            CryptoWriteStream.Write(bytearrayinput, 0, bytearrayinput.Length)
            CryptoWriteStream.Flush()
            CryptoWriteStream.Close()

        End Sub


        Private Function getEncriptor() As ICryptoTransform
            ' Create a symmetric Rijndael object for encryption.
            ' You are specifying the default values for the secret key and the initialization vector.
            ' These default values are generated when you create the first instance of the RijndaelManaged class.
            Dim cspRijndael As New System.Security.Cryptography.RijndaelManaged
            Dim Encryptor As ICryptoTransform = cspRijndael.CreateEncryptor(Me.key, Me.IV)
            Return Encryptor
        End Function

        Friend Function getDecriptor() As ICryptoTransform
            ' Create a symmetric Rijndael object for encryption.
            ' You are specifying the default values for the secret key and the initialization vector.
            ' These default values are generated when you create the first instance of the RijndaelManaged class.
            Dim cspRijndael As New System.Security.Cryptography.RijndaelManaged
            Dim Encryptor As ICryptoTransform = cspRijndael.CreateDecryptor(Me.key, Me.IV)
            Return Encryptor
        End Function


#Region "Encrypt Strings"
        Public Function enCryptString(ByVal string2Encrypt As String) As String

            Dim memStream As New MemoryStream() 'flujo de datos
            ' Initialize a new instance of the CryptoStream class with the destination file stream,
            ' the cryptographic transformation to use, and the mode of the stream.
            Dim CryptoWriteStream As New CryptoStream(memStream, getEncriptor, CryptoStreamMode.Write)
            ' Initialize a new stream writer for writing to the CryptoStream object.
            Dim MyStreamWriter As New StreamWriter(CryptoWriteStream)
            ' Write data to the CryptoStream object.
            MyStreamWriter.WriteLine(string2Encrypt) 'todo: check use of write line on stream
            ' Close all streams and writers.
            MyStreamWriter.Close()
            CryptoWriteStream.Close()
            Dim cipherTextBytes As Byte() = memStream.ToArray
            memStream.Close()
            Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
            Return cipherText
        End Function

        Public Function enCryptBytes(ByRef BytesToCypher As Byte()) As Byte()
            Dim bytesEncrypted() As Byte

            Using memStream As New MemoryStream()
                Using CryptoWriteStream As New CryptoStream(memStream, getEncriptor, CryptoStreamMode.Write)
                    'Escribir el archivo cifrado con DES
                    CryptoWriteStream.Write(BytesToCypher, 0, BytesToCypher.Length)
                    CryptoWriteStream.FlushFinalBlock()
                    bytesEncrypted = memStream.ToArray
                    memStream.Close()
                    CryptoWriteStream.Close()
                End Using
            End Using

            Return bytesEncrypted

        End Function

        Public Function enCryptStream(ByRef streamToCipher As MemoryStream) As String
            Dim BytesToCypher As Byte()
            Dim bytesEncrypted() As Byte
            BytesToCypher = streamToCipher.ToArray
            Dim cipherText1 As String
            ' Initialize a new instance of the CryptoStream class with the destination file stream,
            ' the cryptographic transformation to use, and the mode of the stream.
            Using memStream As New MemoryStream()
                Using CryptoWriteStream As New CryptoStream(memStream, getEncriptor, CryptoStreamMode.Write)
                    'Escribir el archivo cifrado con DES
                    CryptoWriteStream.Write(BytesToCypher, 0, BytesToCypher.Length)
                    CryptoWriteStream.Flush()
                    memStream.Position = 0
                    Dim myStreamReader As New StreamReader(memStream)
                    cipherText1 = myStreamReader.ReadToEnd
                    bytesEncrypted = memStream.ToArray

                    memStream.Close()
                    CryptoWriteStream.Close()
                End Using
            End Using

            Dim cipherText As String = Convert.ToBase64String(bytesEncrypted)
            Return cipherText1

        End Function



        Public Function DecryptString(ByVal string2Decrypt As String) As String
            ' Use a file stream to open the file that you had written the encrypted data to.
            If string2Decrypt Is Nothing Then
                Return ""
            ElseIf string2Decrypt.Length = 0 Then
                Return ""
            End If
            Dim cipherTextBytes As Byte() = Convert.FromBase64String(string2Decrypt)
            Return DecryptString(cipherTextBytes)
        End Function

        Public Function DecryptString(cipherTextBytes As Byte()) As String

            Dim stream2Dec As New MemoryStream(cipherTextBytes) 'flujo de datos

            ' Initialize a new instance of the CryptoStream class with the source file stream,
            ' the cryptographic transformation to use, and the mode of the stream.
            Dim cryptoReadStream As New CryptoStream(stream2Dec, getDecriptor, CryptoStreamMode.Read)

            ' Initialize a new stream reader for reading from the CryptoStream object.
            Dim myStreamReader As New StreamReader(cryptoReadStream)

            Dim cipherText As String = myStreamReader.ReadToEnd

            ' Close all streams and writers.
            myStreamReader.Close()
            cryptoReadStream.Close()
            stream2Dec.Close()

            Return cipherText.Trim

        End Function


#End Region



    End Class
End Namespace

