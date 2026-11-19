


Option Strict On

Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates

Namespace ind.crypt


    Public Class CryptByCertificate
        Inherits CryptoBase

        ''' <summary>
        ''' Decrypt a filestream to a another filestream
        ''' </summary>
        ''' <param name="fsInput">input file stream to decrypt</param>
        ''' <param name="fsOutput">output file stream where decrypted data will be writen</param>
        Public Sub enCryptFile(fsInput As FileStream, fsOutput As FileStream, cert As X509Certificate2)
            'Leer el texto del archivo en la matriz de bytes
            Dim bytearrayinput(CInt(fsInput.Length - 1)) As Byte
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)
            closeFileStream(fsInput)
            Dim resultBytes() As Byte = encryptWithCertificate(cert, bytearrayinput)
            fsOutput.Write(resultBytes, 0, resultBytes.Length - 1)
            closeFileStream(fsOutput)
        End Sub

        Public Function encryptWithCertificate(cert As X509Certificate2, bytearrayinput() As Byte) As Byte()

            'Dim rsaProvider As RSACryptoServiceProvider = DirectCast(cert.PublicKey.Key, RSACryptoServiceProvider)

            Dim encryptedBytes() As Byte
            Using csp As RSACryptoServiceProvider = DirectCast(cert.PublicKey.Key, RSACryptoServiceProvider)
                encryptedBytes = csp.Encrypt(bytearrayinput, False)
            End Using


            'Return binary as base64 string 
            Return encryptedBytes
        End Function


        Public Shared Function deCryptWithCertificate(cert As X509Certificate2, bytearrayinput() As Byte) As Byte()

            Dim rsaProvider As RSACryptoServiceProvider = DirectCast(cert.PublicKey.Key, RSACryptoServiceProvider)

            Dim resultBytes() As Byte = rsaProvider.Decrypt(bytearrayinput, False)

            'Return binary as base64 string 
            Return resultBytes
        End Function

        ''' <summary>
        ''' Decrypt a filestream to a another filestream
        ''' </summary>
        ''' <param name="fsInput">input file stream to decrypt</param>
        ''' <param name="fsOutput">output file stream where decrypted data will be writen</param>
        Public Sub decryptFile(fsInput As FileStream, fsOutput As FileStream, cert As X509Certificate2)

            'Leer el texto del archivo en la matriz de bytes
            Dim bytearrayinput(CInt(fsInput.Length - 1)) As Byte
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length)
            closeFileStream(fsInput)
            Dim resultBytes() As Byte = deCryptWithCertificate(cert, bytearrayinput)
            fsOutput.Write(resultBytes, 0, resultBytes.Length - 1)
            closeFileStream(fsOutput)

        End Sub


        Private Function EncryptString(cert As X509Certificate2, input As String) As String
            Dim output As String = String.Empty
            Using csp As RSACryptoServiceProvider = DirectCast(cert.PublicKey.Key, RSACryptoServiceProvider)
                Dim bytesData As Byte() = Encoding.UTF8.GetBytes(input)
                Dim bytesEncrypted As Byte() = csp.Encrypt(bytesData, False)
                output = Convert.ToBase64String(bytesEncrypted)
            End Using
            Return output
        End Function

        Public Function decryptString(cert As X509Certificate2, input As String) As String
            Dim text As String = String.Empty

            Using csp As RSACryptoServiceProvider = DirectCast(cert.PrivateKey, RSACryptoServiceProvider)
                    Dim bytesEncrypted As Byte() = Convert.FromBase64String(input)
                    Dim bytesDecrypted As Byte() = csp.Decrypt(bytesEncrypted, False)
                Text = Encoding.UTF8.GetString(bytesDecrypted)
            End Using
            Return text
        End Function




    End Class
End Namespace
