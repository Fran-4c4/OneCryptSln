Option Strict On

Imports System.IO
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates

Namespace ind.crypt

    ''' <summary>
    ''' base class for crypto
    ''' </summary>
    Public Class CryptoBase
        ''' <summary>
        ''' encrypt method
        ''' </summary>
        Public encryptMethodType As cryptMethodEnum = cryptMethodEnum.rijndael
        ''' <summary>
        ''' encryption method enumeration
        ''' </summary>
        Public Enum cryptMethodEnum
            rijndael = 0
            certificate = 1

        End Enum
        ''' <summary>
        ''' Encrypt/Decrypt  action
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum CryptoActionEnum
            'Define the enumeration for CryptoAction.
            ''' <summary>
            ''' Encrypt
            ''' </summary>
            ActionEncrypt = 1
            ''' <summary>
            ''' Decrypt
            ''' </summary>
            ''' <remarks></remarks>
            ActionDecrypt = 2
            ''' <summary>
            ''' app will be act depending on extension
            ''' </summary>
            ''' <remarks></remarks>
            ActionChoose = 3
        End Enum
        ''' <summary>
        ''' State of the file
        ''' </summary>
        Public Enum CryptoStateEnum
            'Define the enumeration for CryptoAction.
            ''' <summary>
            ''' Encrypt
            ''' </summary>
            IsEncripted = 1
            ''' <summary>
            ''' Decrypt
            ''' </summary>
            ''' <remarks></remarks>
            IsDecrypted = 2
        End Enum

        Public Enum DeleteOriginalEnum As Byte
            DeleteAfterOperations = 0
            NotDeleteOriginal = 1
        End Enum

        Public Enum PasswordScore
            Blank = 0
            VeryWeak = 1
            Weak = 2
            Medium = 3
            GOOD = 4
            Strong = 5
            VeryStrong = 6
        End Enum

        Public Shared Sub closeFileStream(fs As FileStream)
            Try
                fs.Close()
                fs.Dispose()
            Catch ex As Exception
                Debug.WriteLine(ex)
            End Try
        End Sub

        Public Shared Sub deleteFileTrash(filename As String)
            Try
                Dim fileDelete As New FileInfo(filename) 'delete output file
                If fileDelete.Exists Then
                    fileDelete.Delete()
                End If
            Catch ex As Exception
                'nothing
            End Try
        End Sub

    End Class


End Namespace

