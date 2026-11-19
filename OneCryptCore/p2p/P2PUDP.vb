Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Runtime.InteropServices


Namespace frms.p2p


    ''' <summary>
    ''' Class to manage UDP connections
    ''' </summary>
    ''' <remarks></remarks>
    Public Class P2PUDP
        ''' <summary>
        ''' Message received by udp channel
        ''' </summary>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Delegate Sub MessageReceivedDelegate(ByVal message As String)
        ''' <summary>
        ''' use "255.255.255.255" to send everywhere
        ''' </summary>
        ''' <remarks></remarks>
        Private Const broadcastAddress As String = "255.255.255.255"
        Private receivingThread As Thread
        Private user As String
        Private UserName As String = My.Computer.Name

        Public Sub New()

        End Sub


#Region "Receiver"

        Dim messageDelegate As MessageReceivedDelegate
        Public Event MessageReceivedEvent(message As String)
        Friend receivingClient As System.Net.Sockets.UdpClient
        Friend receiverPort As Integer = 54545
        Friend ReceiverIPEndPoint As IPAddress = IPAddress.Any
        Public stopReceiver As Boolean = False

        ''' <summary>
        ''' initialize Reciever at selected port
        ''' </summary>
        ''' <param name="IPEndPoint"></param>
        ''' <param name="port"></param>
        ''' <remarks></remarks>
        Public Sub InitializeReceiver(IPEndPoint As String, port As Integer)
            Dim ip As IPAddress = New IPAddress(IPEndPoint)
            InitializeReceiver(ip, port)
        End Sub

        ''' <summary>
        ''' initialize Reciever at selected port
        ''' </summary>
        ''' <param name="IPEndPoint"></param>
        ''' <param name="port"></param>
        ''' <remarks></remarks>
        Public Sub InitializeReceiver(IPEndPoint As IPAddress, port As Integer)
            ReceiverIPEndPoint = IPEndPoint
            receiverPort = port
            receivingClient = New UdpClient(port)

            Dim start As ThreadStart = New ThreadStart(AddressOf Receiver)
            receivingThread = New Thread(start)
            receivingThread.IsBackground = True
            receivingThread.Start()

        End Sub


        Private Sub Receiver()
            Dim endPoint As IPEndPoint = New IPEndPoint(ReceiverIPEndPoint, receiverPort)

            Debug.WriteLine("Receiver initiated at port: " & receiverPort)
            While (True)
                If stopReceiver = True Then
                    Exit While
                End If
                Dim data() As Byte
                data = receivingClient.Receive(endPoint)

                Dim message As String = Encoding.ASCII.GetString(data)
                'Debug.WriteLine(message)
                RaiseEvent MessageReceivedEvent(message)

            End While
        End Sub

#End Region

#Region "Sender"

        Friend sendingClient As UdpClient


        Public Sub InitializeSender(targetIP As String, port As Integer)
            sendingClient = New UdpClient(targetIP, port)
            'sendingClient = New UdpClient(port)'crash
            sendingClient.EnableBroadcast = True
        End Sub

        Public Sub SendMessage(message As String)

            If (Not String.IsNullOrEmpty(message)) Then

                Dim data() As Byte = Encoding.ASCII.GetBytes(message)
                sendingClient.Send(data, data.Length)
            End If

        End Sub

        Public Sub closeSendingClient()
            sendingClient.Close()
        End Sub

#End Region

        Public Function getWhoAmI()
            'get internet ip
            'Dim WC As New WebClient
            'txtInternetIP.Text = WC.DownloadString("http://automation.whatismyip.com/n09230945.asp")
            'get mac ip
            Dim tmpHostName As String = System.Net.Dns.GetHostName()
            Dim hostEntry = Dns.GetHostEntry(tmpHostName)
            Dim ipv4Address = Array.Find(hostEntry.AddressList,
                                         Function(address) address.AddressFamily = AddressFamily.InterNetwork)
            If ipv4Address Is Nothing Then
                Throw New InvalidOperationException("No IPv4 address is available for this computer.")
            End If
            Return ipv4Address.ToString()
        End Function


    End Class


End Namespace
