




Imports System.Net
''' <summary>
''' class to manage p2p messages
''' </summary>
Partial Public Class frmMain

    Dim p2pMgr As New frms.p2p.P2PUDP

    Dim messageDelegate As frms.p2p.P2PUDP.MessageReceivedDelegate = AddressOf MessageReceived
    Private Sub MessageReceived(ByVal message As String)
        If InvokeRequired Then
            Debug.WriteLine("MessageReceived InvokeRequired")
            Invoke(messageDelegate, message)
        Else
            Debug.WriteLine("MessageReceived:: " & message)

        End If

    End Sub

    Private Sub initP2PReceiver()
        Try
            Dim receiverPort As Integer = CInt(ind.settings.SettingsLink.p2pudpReceiverPort)
            Dim ReceiverIPEndPoint As String = ind.settings.SettingsLink.p2pudpReceiverIPEndPoint
            Dim ipEndpoint As IPAddress
            If ReceiverIPEndPoint = "any" Then
                ipEndpoint = IPAddress.Any
            Else
                ipEndpoint = IPAddress.Parse(ReceiverIPEndPoint)
            End If

            RemoveHandler p2pMgr.MessageReceivedEvent, AddressOf MessageReceived
            AddHandler p2pMgr.MessageReceivedEvent, AddressOf MessageReceived
            p2pMgr.InitializeReceiver(ipEndpoint, receiverPort)

        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try

    End Sub


    Public Sub parseMessage(message As String)

    End Sub


End Class
