Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml
Imports System.Xml.Linq

Namespace frms.autoupdate

    Public NotInheritable Class UpdateReleaseInfo
        Public Property Version As Version
        Public Property DownloadUri As Uri
        Public Property ReleaseNotes As String
    End Class

    ''' <summary>
    ''' Checks a small, trusted HTTPS feed and notifies the user about releases.
    ''' It never downloads or executes an installer.
    ''' </summary>
    Public NotInheritable Class AutoUpdate

        ''' <summary>
        ''' AppSettings key containing an absolute HTTPS feed URI. An empty value
        ''' disables checks. Invalid or non-HTTPS values are rejected. This class
        ''' is the authoritative owner of the key name and validation contract.
        ''' </summary>
        Public Const UpdateFeedSettingName As String = "OneCrypt.UpdateFeedUrl"

        ''' <summary>Maximum accepted update-feed size, in bytes.</summary>
        Public Const MaximumFeedBytes As Integer = 262144

        Private Sub New()
        End Sub

        Public Shared Async Sub showUpdateForm()
            Await CheckForUpdatesAsync(ind.crypt.OCGlobal.mainForm, True)
        End Sub

        Public Shared Async Sub checkUpdate(frm As Form)
            If frm Is Nothing Then
                Throw New ArgumentNullException(NameOf(frm))
            End If
            Await CheckForUpdatesAsync(frm, False)
        End Sub

        Private Shared Async Function CheckForUpdatesAsync(owner As Form, showCurrentResult As Boolean) As Task
            Try
                Dim feedUri = GetConfiguredFeedUri()
                If feedUri Is Nothing Then
                    If showCurrentResult Then
                        MessageBox.Show("No HTTPS update feed is configured.", "OneCrypt updates",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Return
                End If

                Dim release = Await Task.Run(Function() DownloadReleaseInfo(feedUri))
                If owner.IsDisposed Then
                    Return
                End If

                owner.BeginInvoke(New MethodInvoker(
                    Sub()
                        If release.Version > My.Application.Info.Version Then
                            If showCurrentResult Then
                                OpenReleasePrompt(owner, release)
                            Else
                                ind.crypt.OCGlobal.mainForm.showUpdateButton(release)
                            End If
                        ElseIf showCurrentResult Then
                            MessageBox.Show("This installation is up to date.", "OneCrypt updates",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Sub))
            Catch ex As Exception
                ind.log.Logging.logErr("Update check failed: " & ex.Message)
                If showCurrentResult Then
                    MessageBox.Show("The update feed could not be checked." & Environment.NewLine & ex.Message,
                                    "OneCrypt updates", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End Try
        End Function

        Private Shared Function GetConfiguredFeedUri() As Uri
            Return ParseConfiguredFeedUri(ConfigurationManager.AppSettings(UpdateFeedSettingName))
        End Function

        Public Shared Function ParseConfiguredFeedUri(configuredValue As String) As Uri
            If String.IsNullOrWhiteSpace(configuredValue) Then
                Return Nothing
            End If

            Dim feedUri As Uri = Nothing
            If Not Uri.TryCreate(configuredValue, UriKind.Absolute, feedUri) OrElse
               feedUri.Scheme <> Uri.UriSchemeHttps Then
                Throw New ConfigurationErrorsException(
                    UpdateFeedSettingName & " must be an absolute HTTPS URI or an empty value.")
            End If
            Return feedUri
        End Function

        Private Shared Function DownloadReleaseInfo(feedUri As Uri) As UpdateReleaseInfo
            Dim request = DirectCast(WebRequest.Create(feedUri), HttpWebRequest)
            request.Method = "GET"
            request.Timeout = 10000
            request.ReadWriteTimeout = 10000
            request.UserAgent = "OneCrypt/" & My.Application.Info.Version.ToString()

            Using response = DirectCast(request.GetResponse(), HttpWebResponse)
                If response.ContentLength > MaximumFeedBytes Then
                    Throw New InvalidDataException("The update feed exceeds the permitted size.")
                End If

                Using responseStream = response.GetResponseStream()
                    Using buffer As New MemoryStream()
                        Dim block(4095) As Byte
                        Do
                            Dim count = responseStream.Read(block, 0, block.Length)
                            If count = 0 Then
                                Exit Do
                            End If
                            buffer.Write(block, 0, count)
                            If buffer.Length > MaximumFeedBytes Then
                                Throw New InvalidDataException("The update feed exceeds the permitted size.")
                            End If
                        Loop
                        Return ParseUpdateFeed(Encoding.UTF8.GetString(buffer.ToArray()))
                    End Using
                End Using
            End Using
        End Function

        Public Shared Function ParseUpdateFeed(xml As String) As UpdateReleaseInfo
            If String.IsNullOrWhiteSpace(xml) Then
                Throw New InvalidDataException("The update feed is empty.")
            End If

            Dim settings As New XmlReaderSettings With {
                .DtdProcessing = DtdProcessing.Prohibit,
                .XmlResolver = Nothing
            }
            Dim document As XDocument
            Using textReader As New StringReader(xml)
                Using reader = XmlReader.Create(textReader, settings)
                    document = XDocument.Load(reader, LoadOptions.None)
                End Using
            End Using

            Dim root = document.Element("onecryptUpdate")
            If root Is Nothing Then
                Throw New InvalidDataException("The update feed root must be onecryptUpdate.")
            End If

            Dim versionValue = CStr(root.Element("version"))
            Dim version As Version = Nothing
            If Not Version.TryParse(versionValue, version) Then
                Throw New InvalidDataException("The update feed version is invalid.")
            End If

            Dim downloadValue = CStr(root.Element("downloadUrl"))
            Dim downloadUri As Uri = Nothing
            If Not Uri.TryCreate(downloadValue, UriKind.Absolute, downloadUri) OrElse
               downloadUri.Scheme <> Uri.UriSchemeHttps Then
                Throw New InvalidDataException("The release download URL must use HTTPS.")
            End If

            Return New UpdateReleaseInfo With {
                .Version = version,
                .DownloadUri = downloadUri,
                .ReleaseNotes = If(CStr(root.Element("releaseNotes")), String.Empty)
            }
        End Function

        Private Shared Sub OpenReleasePrompt(owner As IWin32Window, release As UpdateReleaseInfo)
            Dim message = "OneCrypt " & release.Version.ToString() & " is available."
            If Not String.IsNullOrWhiteSpace(release.ReleaseNotes) Then
                message &= Environment.NewLine & Environment.NewLine & release.ReleaseNotes
            End If
            message &= Environment.NewLine & Environment.NewLine & "Open the release page?"

            If MessageBox.Show(owner, message, "OneCrypt updates",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Process.Start(New ProcessStartInfo(release.DownloadUri.AbsoluteUri) With {.UseShellExecute = True})
            End If
        End Sub

    End Class

End Namespace
