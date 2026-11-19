Imports System.Security
Imports System.Data
Imports System.Globalization
Imports System.Reflection


Public Module AppGen


#Region "Initial configuration"
    ''' <summary>
    ''' Starting point
    ''' </summary>
    <STAThread()>
    Public Function Main(ByVal cmdArgs() As String) As Integer
        Try
            Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)
            'System.Threading.Thread.CurrentThread.Name = ThreadMainName
            'Dim eh As New CustomExceptionHandler
            'AddHandler Application.ThreadException, AddressOf eh.OnThreadException

            'Try
            '    cf.SplashScreen.SplashScreen.setBackgroundImage("ARI.jpg")
            '    cf.SplashScreen.SplashScreen.setStatusPosition(DockStyle.Bottom)
            '    cf.SplashScreen.SplashScreen.setStatusColor(Color.White)
            '    cf.SplashScreen.SplashScreen.ShowSplashScreen()
            '    cf.SplashScreen.SplashScreen.SetStatus("Loading application, wait please", "At " & [Assembly].GetExecutingAssembly.Location)

            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            '    Return 4
            'End Try


            Try

                ind.crypt.OCGlobal.mainForm = New frmMain
                CloseSplashScreenWithoutFade()
                Application.Run(ind.crypt.OCGlobal.mainForm)
                Return 0
            Catch ex As Exception
                CloseSplashScreenWithoutFade()
                MessageBox.Show(ex.Message)
                Return 1

            End Try


        Catch ex As SecurityException
            CloseSplashScreenWithoutFade()
            MessageBox.Show(ex.Message)
            'MsgAri.msgError(ex)

            'Catch exWeb As System.Web.Services.Protocols.SoapException
            'Dim msg As String = "Sorry, application can´t connect with server, please contact with your administrator and check server path. "
            'msg += vbCrLf & vbCrLf & "Detailed exception:" & vbCrLf & exWeb.Message
            'MsgAri.ShowMessageModal(msg, FrmMSG.modo.form, UCMessage.MessageTypeEnum.Alarm)
            Return 2
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MsgAri.ShowMessageModal(ex.Message, FrmMSG.modo.form, UCMessage.MessageTypeEnum.Alarm)
            Return 3
        Finally

        End Try
    End Function

    Public Sub CloseSplashScreenWithoutFade()
        ' signal the native process (that launched us) to close the splash screen
        '    Using (dim closeSplashEvent as New EventWaitHandle(False, EventResetMode.ManualReset, "CloseSplashScreenWithoutFadeEventSplashScreenStarter"))
        '    closeSplashEvent.Set()
        'End Using

        ' signal the native process (that launched us) to close the splash screen
        Using closeSplashEvent = New Threading.EventWaitHandle(False, Threading.EventResetMode.ManualReset, "CloseSplashScreenWithoutFadeEventSplashScreenStarter")
            closeSplashEvent.[Set]()
        End Using

    End Sub
    ''' <summary>
    ''' Obtiene las versiones de ari que se estan ejecutando.
    ''' </summary>
    Public Sub getServerVersion(Optional ByVal TimeOut As Integer = 120000)

        'Dim ws As ari.ws.admindbWS.AdminDBMgr
        'Try
        '    Dim wsServerVersion As String '= ws.getWebServerVersion
        '    Dim ServerVersion As String '= ws.getServerVersion
        '    ws = CType(WebServices.getWebService(WebServices.Service.AdminDB, TimeOut), ari.ws.admindbWS.AdminDBMgr)
        '    ws.getAllServerVersion(ServerVersion, wsServerVersion)

        '    If wsServerVersion.ToUpper <> ssf.ar.obj.AriCte.AriVersion.ToUpper Or ServerVersion <> ssf.ar.obj.AriCte.AriVersion.ToUpper Then
        '        Throw New Exception("Sorry, the server version is diferent from client version. Please contact with administrator to correct this.")
        '    End If

        'Finally
        '    WebServices.disposeWS(ws)
        'End Try
    End Sub

#End Region

End Module
