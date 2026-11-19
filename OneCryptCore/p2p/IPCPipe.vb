Imports System
Imports System.IO
Imports System.IO.Pipes
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ind.p2p

    Public Delegate Sub DelegateMessage(ByVal Reply As String)

    ''' <summary>
    ''' Class to handle pipe messages
    ''' </summary>
    Public Class IPCPipe

        Public Shared srvPipe As IPCPipe

        Public Shared Event PipeMessage As DelegateMessage

        Private _pipeName As String

        ''' <summary>
        ''' send a message to pipe
        ''' </summary>
        ''' <param name="input"></param>
        ''' <returns></returns>
        Public Shared Function sendMessageToServer(input As String, Optional pipe As String = "onecrypt") As String
            Dim client As NamedPipeClientStream = Nothing
            Try
                If String.IsNullOrEmpty(input) Then
                    Throw New Exception("message to send is empty")
                End If
                Debug.WriteLine("Sending Message")
                client = New NamedPipeClientStream(pipe)
                client.Connect(1000)
                'Dim reader As StreamReader = New StreamReader(client)
                Dim writer As StreamWriter = New StreamWriter(client)

                writer.WriteLine(input)
                writer.Flush()
                writer.Dispose()

                'Dim res As String = reader.ReadLine
                'reader.Dispose()

                Return "Message Sended"
            Catch ext As TimeoutException
                Debug.WriteLine("input" & input & " " & ext.Message)
                Throw ext
            Catch ex As Exception
                Debug.WriteLine("input" & input & " " & ex.Message)
                Throw ex
            Finally
                If Not client Is Nothing Then
                    client.Dispose()
                End If

            End Try
        End Function


        ''' <summary>
        ''' Start pipe server listening in the selected uri
        ''' </summary>
        ''' <param name="pipeURI"></param>
        Public Shared Sub StartPipeServer(Optional pipeURI As String = "onecrypt")
            srvPipe = New IPCPipe
            srvPipe.ListenURIAsynchronous(pipeURI)
            ind.log.Logging.logInfo("StartPipeServer")
            'Task.Factory.StartNew(Sub()
            '                          Dim server = New NamedPipeServerStream("PipesOfPiece")
            '                          server.WaitForConnection()
            '                          Dim reader As StreamReader = New StreamReader(server)
            '                          Dim writer As StreamWriter = New StreamWriter(server)
            '                          While True
            '                              Dim line = reader.ReadLine()
            '                              writer.WriteLine(String.Join("", line.Reverse()))
            '                              writer.Flush()
            '                          End While
            '                      End Sub)
        End Sub
        ''' <summary>
        ''' Listen in the uri in async mode
        ''' </summary>
        ''' <param name="PipeName"></param>
        Public Sub ListenURIAsynchronous(ByVal PipeName As String)
            Try
                _pipeName = PipeName
                Dim pipeServer As NamedPipeServerStream = New NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous)

                pipeServer.BeginWaitForConnection(New AsyncCallback(AddressOf WaitForConnectionCallBack), pipeServer)

            Catch oEX As Exception
                'todo: Add retry
                Debug.WriteLine(oEX.Message)
            End Try
        End Sub

        Private Sub WaitForConnectionCallBack(ByVal iar As IAsyncResult)
            Dim pipeServer As NamedPipeServerStream = Nothing
            Try
                pipeServer = CType(iar.AsyncState, NamedPipeServerStream)
                pipeServer.EndWaitForConnection(iar)
                Dim reader As StreamReader = New StreamReader(pipeServer)
                'Dim writer As StreamWriter = New StreamWriter(pipeServer)
                'While True
                Dim line = reader.ReadLine()
                Debug.WriteLine("PIPE Server reading/raising event: " & line)
                'writer.WriteLine(String.Join("", "Server: " & line))
                'writer.Flush()
                'End While

                'Dim buffer As Byte() = New Byte(254) {}
                'pipeServer.Read(buffer, 0, 255)
                'Dim stringData As String = Encoding.UTF8.GetString(buffer, 0, buffer.Length)
                'Debug.WriteLine(stringData & Environment.NewLine)
                RaiseEvent PipeMessage(line)
                'PipeMessage.Invoke(stringData)
                pipeServer.Close()
                pipeServer = Nothing
                Debug.WriteLine("PIPE Server close ok")
            Catch ex As Exception
                Debug.WriteLine(ex)

            Finally
                If Not pipeServer Is Nothing Then
                    pipeServer.Close()
                    pipeServer = Nothing
                End If
            End Try

            pipeServer = New NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous)
            pipeServer.BeginWaitForConnection(New AsyncCallback(AddressOf WaitForConnectionCallBack), pipeServer)
        End Sub


        'Public Shared Sub startPipe()
        '    StartPipeServer()

        '    Dim client = New NamedPipeClientStream("PipesOfPiece")
        '    client.Connect()
        '    Dim reader As StreamReader = New StreamReader(client)
        '    Dim writer As StreamWriter = New StreamWriter(client)
        '    While True
        '        Dim input As String = Console.ReadLine()
        '        If String.IsNullOrEmpty(input) Then Exit While
        '        writer.WriteLine(input)
        '        writer.Flush()
        '        Console.WriteLine(reader.ReadLine())
        '    End While
        'End Sub

    End Class
End Namespace



'Namespace ConsoleApplication1

'    Class Program

'        Private Shared Sub Main(ByVal args As String())
'            StartPipeServer()
'            Task.Delay(1000).Wait()
'            Dim client = New NamedPipeClientStream("PipesOfPiece")
'            client.Connect()
'            Dim reader As StreamReader = New StreamReader(client)
'            Dim writer As StreamWriter = New StreamWriter(client)
'            While True
'                Dim input As String = Console.ReadLine()
'                If String.IsNullOrEmpty(input) Then Exit While
'                writer.WriteLine(input)
'                writer.Flush()
'                Console.WriteLine(reader.ReadLine())
'            End While
'        End Sub


'    End Class
'End Namespace
