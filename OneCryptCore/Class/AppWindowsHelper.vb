Imports Microsoft
Imports Microsoft.Win32
Imports Microsoft.Win32.Registry

Namespace ind.tools
    ''' <summary>
    ''' tools to use with windows
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppWindowsHelper
        ''' <summary>
        ''' configure one application to start with windows start in registry
        ''' </summary>
        ''' <param name="bCrear">True to activate, false to delete</param>
        ''' <param name="applicationName">The name for the registry</param>
        ''' <param name="Application_ExecutablePath">The complete path to the application</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddApplicationToWindowsStart(bCrear As Boolean, applicationName As String, Application_ExecutablePath As String) As Boolean

            ' clave del registro para   
            ' colocar el path del ejecutable para iniciar con windows  
            Const CLAVE As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"

            'ProductName : el nombre del programa.  
            Dim subClave As String = applicationName

            Try
                ' Abre la clave del usuario actual (CurrentUser) para poder extablecer el dato  
                ' si la clave CurrentVersion\Run no existe la crea  
                Dim Registro As RegistryKey = CurrentUser.CreateSubKey(CLAVE, RegistryKeyPermissionCheck.ReadWriteSubTree)

                With Registro

                    .OpenSubKey(CLAVE, True)

                    Select Case bCrear
                        Case True
                            ' Escribe el path con SetValue   
                            'Valores : ProductName el nombre del programa y ExecutablePath : la ruta del exe  
                            .SetValue(subClave, Application_ExecutablePath) '   Application.ExecutablePath.ToString)

                        Case False
                            If .GetValue(subClave, "").ToString <> "" Then
                                .DeleteValue(subClave) ' eliminar  

                            Else
                                'clave no existe
                                'error?
                            End If
                    End Select
                End With
                'all ok
                Return True

            Catch ex As Exception
                Throw ex
            End Try

        End Function
    End Class

End Namespace