
Namespace ind.log
    ''' <summary>
    ''' class for logging
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Logging
        Inherits NLog.Logger

        Public Shared logger As NLog.Logger = NLog.LogManager.GetCurrentClassLogger()

        'Public Shared Sub log(message As String, level As System.Diagnostics.TraceEventType)
        '    My.Application.Log.WriteEntry(message, level)
        'End Sub

        Public Shared Sub logInfo(message As String)
            logger.Info(message)

        End Sub

        Public Shared Sub logErr(message As String)
            logger.Error(message)
        End Sub

        Public Shared Sub logExcep(ex As Exception)
            logger.LogException(NLog.LogLevel.Error, ex.Message, ex)
        End Sub

    End Class
End Namespace

