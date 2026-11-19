Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.


    Partial Friend Class MyApplication


        Private Sub MyApplication_StartupNextInstance(sender As Object, e As ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Debug.WriteLine("StartupNextInstance executed at " & Date.Now)
            Try
                ' Me.MainForm 'this a property of myapplication class that expose the mainform of the application used in configuration. Is exposed as form.

                ind.crypt.OCGlobal.mainForm.RestoreApplication()
                e.BringToForeground = True

                'ind.win.Msg.mInformation("  MyApplication_StartupNextInstance()" & Date.Now)
                Dim ac As New ind.crypt.ArgumentController
                ac.ArgumentSwitch(e.CommandLine)

            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try

        End Sub
    End Class


End Namespace

