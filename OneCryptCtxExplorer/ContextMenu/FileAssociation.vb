Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Namespace ind.win.tools


    '    Public Class FileAssociation


    '#Region "Register .ocpt extension"


    '#End Region


    '        ''' <summary>
    '        ''' Associate file extension with progID, description, icon and application
    '        ''' </summary>
    '        ''' <param name="extension"></param>
    '        ''' <param name="progID"></param>
    '        ''' <param name="description"></param>
    '        ''' <param name="icon"></param>
    '        ''' <param name="application"></param>
    '        ''' <remarks></remarks>
    '        Public Shared Sub ExtensionAssociate(extension As String, progID As String, description As String, icon As String, application As String)

    '            If extension.IndexOf(".") = -1 Then
    '                extension = "." & extension
    '            End If

    '            Using key As RegistryKey = Registry.ClassesRoot.CreateSubKey("*")
    '                key.CreateSubKey("Shell\OneCrypt\Command").SetValue("", System.Windows.Forms.Application.ExecutablePath & " ""%1""")
    '            End Using

    '            Registry.ClassesRoot.CreateSubKey(extension).SetValue("", progID)
    '            If progID IsNot Nothing AndAlso progID.Length > 0 Then
    '                Using key As RegistryKey = Registry.ClassesRoot.CreateSubKey(progID)
    '                    If description IsNot Nothing Then
    '                        key.SetValue("", description)
    '                    End If
    '                    If icon IsNot Nothing Then
    '                        key.CreateSubKey("DefaultIcon").SetValue("", ToShortPathName(icon))
    '                    End If
    '                    If application IsNot Nothing Then
    '                        'key.CreateSubKey("Shell\Open\Command").SetValue("", System.Windows.Forms.Application.ExecutablePath & " ""%1""")
    '                        key.CreateSubKey("Shell\Decrypt with OneCrypt\Command").SetValue("", System.Windows.Forms.Application.ExecutablePath & " ""%1""")
    '                    End If
    '                End Using
    '            End If
    '        End Sub

    '        ''' <summary>
    '        ''' Quita la extensión indicada de los tipos de archivos registrados
    '        ''' </summary>
    '        ''' <param name="extension">
    '        ''' La extensión a quitar del registro
    '        ''' </param>
    '        ''' <returns>
    '        ''' Devuelve un valor verdadero o falso según se haya quitado 
    '        ''' correctamente o no la extensión
    '        ''' </returns>
    '        ''' <remarks>
    '        ''' ATENCIÓN, este método elimina totalmente la extensión del registro
    '        ''' </remarks>
    '        Public Shared Function ExtensionRemove(ByVal extension As String) As Boolean
    '            ' Si no se especifica el punto
    '            If extension.IndexOf(".") = -1 Then
    '                extension = "." & extension
    '            End If

    '            Dim progId As String = GetProgId(extension)

    '            Try
    '                If String.IsNullOrEmpty(progId) = False _
    '                AndAlso progId.Length > 0 Then

    '                    ' Eliminar la clave
    '                    Registry.ClassesRoot.DeleteSubKeyTree(extension)
    '                    Registry.ClassesRoot.DeleteSubKeyTree(progId)
    '                    Registry.ClassesRoot.DeleteSubKeyTree("*\Shell\OneCrypt")

    '                    Return True
    '                End If

    '            Catch ex As Exception

    '            End Try

    '            Return False
    '        End Function

    '        ''' <summary>
    '        ''' Método para obtener el progId de una extensión.
    '        ''' </summary>
    '        ''' <param name="ext">
    '        ''' Extensión de la que se quiere obtener el progId
    '        ''' </param>
    '        ''' <returns>
    '        ''' Devuelve una cadena con el progId de la extensión 
    '        ''' o una cadena vacía si no existe
    '        ''' </returns>
    '        ''' <remarks></remarks>
    '        Public Shared Function GetProgId(ByVal ext As String) As String
    '            Dim progId As String = ""

    '            Try
    '                Using rk As RegistryKey = Registry.ClassesRoot.OpenSubKey(ext)
    '                    If rk IsNot Nothing Then
    '                        progId = rk.GetValue("").ToString
    '                        rk.Close()
    '                    End If
    '                End Using

    '            Catch ex As Exception
    '                Return ""
    '            End Try

    '            Return progId
    '        End Function




    '        ''' <summary>
    '        ''' Return true if extension already associated in registry
    '        ''' </summary>
    '        ''' <param name="extension"></param>
    '        ''' <returns></returns>
    '        ''' <remarks></remarks>
    '        Public Shared Function IsAssociated(extension As String) As Boolean
    '            Return (Registry.ClassesRoot.OpenSubKey(extension, False) IsNot Nothing)
    '        End Function

    '        <DllImport("Kernel32.dll")> _
    '        Private Shared Function GetShortPathName(lpszLongPath As String, <Out> lpszShortPath As System.Text.StringBuilder, cchBuffer As UInteger) As UInteger
    '        End Function

    '        ' Return short path format of a file name
    '        Private Shared Function ToShortPathName(longName As String) As String
    '            Dim s As New System.Text.StringBuilder(1000)
    '            Dim iSize As UInteger = CUInt(s.Capacity)
    '            Dim iRet As UInteger = GetShortPathName(longName, s, iSize)
    '            Return s.ToString()
    '        End Function
    '    End Class

End Namespace