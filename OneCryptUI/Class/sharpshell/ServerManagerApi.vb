Imports System
Imports System.Collections.Generic


Imports System.Linq
Imports System.Windows.Forms
Imports sharpshell
Imports System.ComponentModel.Composition.Hosting
Imports System.IO
Imports System.Reflection

Namespace sharpshell.tools


    Public Class ServerManagerApi



        Public Shared Sub installServer(assemblyPath As String, registrationType As sharpshell.ServerRegistration.RegistrationType)
            Dim servers As IEnumerable(Of ServerEntry) = sharpshell.tools.ServerManagerApi.LoadServers(assemblyPath)
            For Each srv As ServerEntry In servers
                sharpshell.tools.ServerManagerApi.installServer(srv.Server, registrationType)
            Next
        End Sub

        Public Shared Sub installServer(srv As ISharpShellServer, registrationType As sharpshell.ServerRegistration.RegistrationType)
            sharpshell.ServerRegistration.ServerRegistrationManager.InstallServer(srv, registrationType, True)
            sharpshell.ServerRegistration.ServerRegistrationManager.RegisterServer(srv, registrationType)
        End Sub

        Public Shared Sub uninstallServer(assemblyPath As String, registrationType As sharpshell.ServerRegistration.RegistrationType)
            Dim servers As IEnumerable(Of ServerEntry) = sharpshell.tools.ServerManagerApi.LoadServers(assemblyPath)
            For Each srv As ServerEntry In servers
                sharpshell.tools.ServerManagerApi.uninstallServer(srv.Server, registrationType)
            Next
        End Sub
        Public Shared Sub uninstallServer(srv As ISharpShellServer, registrationType As sharpshell.ServerRegistration.RegistrationType)
            sharpshell.ServerRegistration.ServerRegistrationManager.UnregisterServer(srv, registrationType)
            sharpshell.ServerRegistration.ServerRegistrationManager.UninstallServer(srv, registrationType)
        End Sub


        Public Shared Function LoadServers(ByVal path As String) As IEnumerable(Of ServerEntry)
            Dim servers = New List(Of ServerEntry)()
            Try
                Dim catalog = New AssemblyCatalog(IO.Path.GetFullPath(path))
                Dim container = New CompositionContainer(catalog)
                Dim serverTypes = container.GetExports(Of ISharpShellServer)()
                For Each serverType In serverTypes
                    Dim server As ISharpShellServer = Nothing
                    Try
                        server = serverType.Value
                    Catch __unusedException1__ As Exception
                        servers.Add(New ServerEntry With {.ServerName = "Invalid", .ServerPath = path, .ServerType = sharpshell.ServerType.None, .ClassId = New Guid(), .Server = Nothing, .IsInvalid = True})
                        Continue For
                    End Try

                    servers.Add(New ServerEntry With {.ServerName = server.DisplayName, .ServerPath = path, .ServerType = server.ServerType, .ClassId = server.ServerClsid, .Server = server})
                Next
            Catch __unusedException1__ As Exception
                MessageBox.Show("The file '" & IO.Path.GetFileName(path) & "' is not a SharpShell Server.", "Warning")
            End Try

            Return servers
        End Function

    End Class

    Public Class ServerEntry

        Public Property ServerName As String

        Public Property ServerPath As String

        Public Property ServerType As ServerType

        Public Property ClassId As Guid

        Public Property Server As ISharpShellServer

        Public Property IsInvalid As Boolean

        Public Function GetSecurityStatus() As String
            Dim asmName As AssemblyName = AssemblyName.GetAssemblyName(ServerPath)
            Dim key = asmName.GetPublicKey()
            Return If(key IsNot Nothing AndAlso key.Length > 0, "Signed", "Not Signed")
        End Function
    End Class
End Namespace


