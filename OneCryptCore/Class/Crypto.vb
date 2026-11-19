Option Strict On

Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates

Namespace ind.crypt
    ''' <summary>
    ''' Class to encrypt/decrypt data or files
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Crypto
        Inherits CryptoBase

        '*******************************************************************
        'DECLARACIONES GLOBALES A NIVEL DE CLASE
        Dim key() As Byte
        Dim IV() As Byte
        '*******************************************************************

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


#Region "   Decrypt data 2 obsolete"
        ' ''' <summary>
        ' ''' Función EncryptData. Función que encripta un texto que se le pasa a
        ' ''' la función por valor.
        ' ''' </summary>
        ' ''' <param name="inp">Cadena a encriptar</param>
        ' ''' <returns>Devuelve una cadena con el texto encriptado</returns>
        ' ''' <remarks></remarks>
        'Public Function EncryptData2(ByVal inp As String) As String

        '    Dim sOut As String 'cadena a retornar con el texto encriptado
        '    Dim stream As New MemoryStream   'flujo de datos
        '    'Create a CryptoStream, pass it the stream, and encrypt it.
        '    Dim crypto As New CryptoStream(stream, crpEnc, CryptoStreamMode.Write)

        '    'creamos un StreamWriter para escribir en el stream
        '    Dim writer As New StreamWriter(crypto)
        '    'escribimos en el stream.
        '    writer.Write(inp)
        '    writer.Flush()
        '    crypto.FlushFinalBlock()

        '    Dim cipherTextBytes As Byte() = stream.ToArray()

        '    'cerramos las conexiones abiertas
        '    writer.Close()
        '    crypto.Close()
        '    stream.Close()

        '    'convertimos a base64 la cadena a devolver
        '    Dim cipherText As String = Convert.ToBase64String(cipherTextBytes)
        '    sOut = cipherText
        '    Return sOut

        'End Function

        ' ''' <summary>
        ' ''' Funcion DecryptData. Función que desencripta un texto que se le pasa a
        ' ''' la función por valor.
        ' ''' </summary>
        ' ''' <param name="inp">Cadena a desencriptar</param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Function DecryptData2(ByVal inp As String) As String
        '    Dim sOut As String 'cadena a retornar con el texto encriptado
        '    Dim cipherTextBytes As Byte() = Convert.FromBase64String(inp)
        '    Dim stream As New MemoryStream(cipherTextBytes) 'lujo de datos
        '    'RedimBytes()
        '    'Create a CryptoStream, pass it the stream, and decrypt it.
        '    Dim crypto As New CryptoStream(stream, crpDec, CryptoStreamMode.Read)

        '    Dim plainTextBytes As Byte()
        '    ReDim plainTextBytes(cipherTextBytes.Length)

        '    Dim decryptedByteCount As Integer = crypto.Read(plainTextBytes, 0, plainTextBytes.Length)
        '    'Close all the connections.
        '    crypto.Close()
        '    stream.Close()

        '    sOut = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
        '    Return sOut
        'End Function
#End Region



#Region "Encrypt/Decrypt string"

        Public Function enCryptData(ByVal string2Encrypt As String) As String
            Dim crypt As New Rijndael(Me.key, Me.IV)
            Return crypt.enCryptString(string2Encrypt)
        End Function

        Public Function DecryptData(ByVal string2Decrypt As String) As String
            Dim crypt As New Rijndael(Me.key, Me.IV)
            Return crypt.DecryptString(string2Decrypt)
        End Function

#End Region



#Region "Encrypt/Decrypt file"




#Region "Encryption V1"
        ''' <summary>
        ''' Encrypt or decrypt a file
        ''' </summary>
        ''' <param name="strInputFile">file to encrypt or decrypt</param>
        ''' <param name="strOutputFile">file name for the result</param>
        ''' <param name="Direction">action to do, encrypt or decrypt</param>
        ''' <param name="DeleteOriginal">option to delete the original when operation is finished</param>
        ''' <remarks></remarks>
        Public Function EncryptOrDecryptFileEX(ByVal strInputFile As String,
                                         ByVal strOutputFile As String,
                                         ByVal Direction As CryptoActionEnum _
                                         , Optional DeleteOriginal As DeleteOriginalEnum = DeleteOriginalEnum.DeleteAfterOperations) As Boolean

            Try
                Dim strOutputFileTemp As Boolean = False
                If strInputFile = strOutputFile Then
                    strOutputFile = strOutputFile & "." & ind.cte.AppConst.fileCryptExtension & "temp"
                    strOutputFileTemp = True
                End If


                'Determine if ecryption or decryption and setup CryptoStream.
                Select Case Direction
                    Case CryptoActionEnum.ActionEncrypt
                        encryptFileEX(strInputFile, strOutputFile)

                    Case CryptoActionEnum.ActionDecrypt
                        deCryptFileEX(strInputFile, strOutputFile)
                End Select


                'If encrypting then delete the original unencrypted file.
                If Direction = CryptoActionEnum.ActionEncrypt Then
                    Try
                        Dim fileOriginal As New FileInfo(strInputFile)
                        fileOriginal.Delete()
                    Catch exIO As IO.IOException
                        Throw New ind.exceptions.FileInUseException(ind.lang.CoreLang.Crypto_OutputFileInUseByAnotherProcessException, exIO)
                    Catch ex As Exception
                        'todo: after encryption file can not delete? Why? attributes?
                        Debug.WriteLine(ex)
                        Throw ex
                    End Try

                End If

                'If decrypting then delete the encrypted file.
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    If DeleteOriginal = DeleteOriginalEnum.DeleteAfterOperations Then
                        Dim fileEncrypted As New FileInfo(strInputFile)
                        Dim originalNameForOutputFileTemp As String = fileEncrypted.Name
                        fileEncrypted.Delete()
                        If strOutputFileTemp = True Then
                            My.Computer.FileSystem.RenameFile(strOutputFile, originalNameForOutputFileTemp)
                        End If
                    End If

                End If

                Return True
                'Catch file not found error.
            Catch When Err.Number = 53 'if file not found
                Throw New FileNotFoundException(ind.lang.CoreLang.Crypto_OutputFileNotFoundDuringEncriptionException)

            Catch exFile As ind.exceptions.FileInUseException
                If Direction = CryptoActionEnum.ActionEncrypt Then
                    Throw exFile
                End If
                'Catch all other errors. And delete partial files.
            Catch exCrypt1 As System.Security.Cryptography.CryptographicException
                Debug.WriteLine(exCrypt1)
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    deleteFileTrash(strOutputFile)
                    Throw New ind.exceptions.FileNotDecryptedException(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileNotEncryptedException)
                ElseIf Direction = CryptoActionEnum.ActionEncrypt Then
                    deleteFileTrash(strOutputFile)
                    Throw New Exception(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileCanNotEncryptedException)
                End If


            Catch ex As Exception
                Debug.WriteLine(ex)
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    deleteFileTrash(strOutputFile)
                    Throw New Exception(ind.lang.CoreLang.Crypto_PwdDecryptionException)
                ElseIf Direction = CryptoActionEnum.ActionEncrypt Then
                    deleteFileTrash(strOutputFile)
                    Throw New Exception(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileCanNotEncryptedException)
                End If

            End Try
            Return False
        End Function

        Public Sub encryptFileEX(ByVal strInputFile As String,
                               ByVal strOutputFile As String)

            Dim fsInput As System.IO.FileStream = Nothing
            Dim fsOutput As System.IO.FileStream = Nothing
            Try
                'Setup file streams to handle input and output.
                fsInput = New System.IO.FileStream(strInputFile, FileMode.Open, FileAccess.Read)

                'todo: lock files 

            Catch exFile As ind.exceptions.FileInUseException
                Throw exFile
            Catch ex As Exception
                Throw ex
            End Try


            Try
                fsOutput = New System.IO.FileStream(strOutputFile, FileMode.OpenOrCreate, FileAccess.Write)

                Select Case encryptMethodType
                    Case cryptMethodEnum.rijndael
                        Dim crypt As New Rijndael(Me.key, Me.IV)
                        crypt.encryptWithRijndael(fsInput, fsOutput:=fsOutput)

                    Case cryptMethodEnum.certificate
                        Throw New NotSupportedException("Certificate-based file encryption requires an explicit certificate and is not available through this API.")

                End Select


            Catch ex As Exception
                Throw ex
            Finally
                closeFileStream(fsInput)
                closeFileStream(fsOutput)
            End Try

        End Sub

        Public Sub deCryptFileEX(ByVal strInputFile As String,
                               ByVal strOutputFile As String)

            Dim fsInput As System.IO.FileStream = Nothing
            Dim fsOutput As System.IO.FileStream = Nothing
            Try
                'Setup file streams to handle input and output.
                fsInput = New System.IO.FileStream(strInputFile, FileMode.Open, FileAccess.Read)
                'todo: lock files 

            Catch exFile As ind.exceptions.FileInUseException
                Throw exFile
            Catch ex As Exception
                Throw ex
            End Try

            Try
                fsOutput = New System.IO.FileStream(strOutputFile, FileMode.OpenOrCreate, FileAccess.Write)


                Select Case encryptMethodType
                    Case cryptMethodEnum.rijndael
                        Dim crypt As New Rijndael(Me.key, Me.IV)
                        crypt.decryptFile(fsInput, fsOutput)
                    Case cryptMethodEnum.certificate
                        Throw New NotSupportedException("Certificate-based file decryption requires an explicit certificate and is not available through this API.")

                End Select

            Catch ex As Exception
                Throw ex
            Finally
                closeFileStream(fsInput)
                closeFileStream(fsOutput)

            End Try

        End Sub


#End Region


#Region "Encryption v0"



        ''' <summary>
        ''' Encrypt or decrypt a file
        ''' </summary>
        ''' <param name="strInputFile">file to encrypt or decrypt</param>
        ''' <param name="strOutputFile">file name for the result</param>
        ''' <param name="Direction">action to do, encrypt or decrypt</param>
        ''' <param name="DeleteOriginal">option to delete the original when operation is finished</param>
        ''' <remarks></remarks>
        <Obsolete>
        Private Sub EncryptOrDecryptFile(ByVal strInputFile As String,
                                         ByVal strOutputFile As String,
                                         ByVal Direction As CryptoActionEnum _
                                         , Optional DeleteOriginal As DeleteOriginalEnum = DeleteOriginalEnum.DeleteAfterOperations)

            Dim fsInput As System.IO.FileStream = Nothing
            Dim fsOutput As System.IO.FileStream = Nothing

            Try
                Dim strOutputFileTemp As Boolean = False
                If strInputFile = strOutputFile Then
                    strOutputFile = strOutputFile & "." & ind.cte.AppConst.fileCryptExtension & "temp"
                    strOutputFileTemp = True
                End If


                'Setup file streams to handle input and output.
                fsInput = New System.IO.FileStream(strInputFile, FileMode.Open, FileAccess.Read)
                'todo: lock files 
                fsOutput = New System.IO.FileStream(strOutputFile, FileMode.OpenOrCreate, FileAccess.Write)
                fsOutput.SetLength(0) 'make sure fsOutput is empty

                'Declare variables for encrypt/decrypt process.
                Dim bytBuffer(4096) As Byte 'holds a block of bytes for processing
                Dim lngBytesProcessed As Long = 0 'running count of bytes processed
                Dim lngFileLength As Long = fsInput.Length 'the input file's length
                Dim intBytesInCurrentBlock As Integer 'current bytes being processed
                Dim csCryptoStream As CryptoStream = Nothing

                'Declare your CryptoServiceProvider.
                Dim cspRijndael As New System.Security.Cryptography.RijndaelManaged

                'Determine if ecryption or decryption and setup CryptoStream.
                Select Case Direction
                    Case CryptoActionEnum.ActionEncrypt
                        csCryptoStream = New CryptoStream(fsOutput,
                        cspRijndael.CreateEncryptor(key, IV),
                        CryptoStreamMode.Write)

                    Case CryptoActionEnum.ActionDecrypt
                        csCryptoStream = New CryptoStream(fsOutput,
                        cspRijndael.CreateDecryptor(key, IV),
                        CryptoStreamMode.Write)
                End Select

                'Use While to loop until all of the file is processed.
                While lngBytesProcessed < lngFileLength
                    'Read file with the input filestream.
                    intBytesInCurrentBlock = fsInput.Read(bytBuffer, 0, 4096)
                    'Write output file with the cryptostream.
                    csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock)
                    'Update lngBytesProcessed
                    lngBytesProcessed = lngBytesProcessed + CLng(intBytesInCurrentBlock)

                End While


                'Close FileStreams and CryptoStream.
                csCryptoStream.Close()
                fsInput.Close()
                fsOutput.Close()

                'If encrypting then delete the original unencrypted file.
                If Direction = CryptoActionEnum.ActionEncrypt Then
                    Try
                        Dim fileOriginal As New FileInfo(strInputFile)
                        fileOriginal.Delete()
                    Catch exIO As IO.IOException
                        Throw New ind.exceptions.FileInUseException(ind.lang.CoreLang.Crypto_OutputFileInUseByAnotherProcessException, exIO)
                    Catch ex As Exception
                        'todo: after encryption file can not delete? Why? attributes?
                        Debug.WriteLine(ex)
                        Throw ex
                    End Try

                End If

                'If decrypting then delete the encrypted file.
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    If DeleteOriginal = DeleteOriginalEnum.DeleteAfterOperations Then
                        Dim fileEncrypted As New FileInfo(strInputFile)
                        Dim originalNameForOutputFileTemp As String = fileEncrypted.Name
                        fileEncrypted.Delete()
                        If strOutputFileTemp = True Then
                            My.Computer.FileSystem.RenameFile(strOutputFile, originalNameForOutputFileTemp)
                        End If
                    End If

                End If


                'Catch file not found error.
            Catch When Err.Number = 53 'if file not found
                Throw New FileNotFoundException(ind.lang.CoreLang.Crypto_OutputFileNotFoundDuringEncriptionException)

            Catch exFile As ind.exceptions.FileInUseException
                If Direction = CryptoActionEnum.ActionEncrypt Then
                    Throw exFile
                End If
                'Catch all other errors. And delete partial files.
            Catch exCrypt1 As System.Security.Cryptography.CryptographicException
                Debug.WriteLine(exCrypt1)
                fsInput.Close()
                fsOutput.Close()
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    Dim fileDelete As New FileInfo(strOutputFile)
                    fileDelete.Delete()
                    Throw New ind.exceptions.FileNotDecryptedException(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileNotEncryptedException & vbCr & strInputFile)
                Else
                    Dim fileDelete As New FileInfo(strInputFile)
                    fileDelete.Delete()
                    Throw New Exception(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileCanNotEncryptedException)
                End If


            Catch ex As Exception

                fsInput.Close()
                fsOutput.Close()
                Debug.WriteLine(ex)
                If Direction = CryptoActionEnum.ActionDecrypt Then
                    Dim fileDelete As New FileInfo(strOutputFile)
                    fileDelete.Delete()
                    Throw New Exception(ind.lang.CoreLang.Crypto_PwdDecryptionException)
                Else
                    Dim fileDelete As New FileInfo(strOutputFile)
                    fileDelete.Delete()
                    Throw New Exception(ind.lang.CoreLang.Crypto_EncryptOrDecryptFile_FileCanNotEncryptedException)
                End If

            End Try
        End Sub

#End Region

#End Region


    End Class
End Namespace
