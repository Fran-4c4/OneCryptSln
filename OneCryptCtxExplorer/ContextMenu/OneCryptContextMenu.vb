Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports SharpShell.Attributes
Imports SharpShell.SharpContextMenu
Imports System.Windows.Forms

Namespace onecrypt

    <ComVisible(True)>
    <COMServerAssociation(AssociationType.AllFiles) _
    , COMServerAssociation(AssociationType.Directory) _
    >
    Public Class OneCryptContextMenu
        Inherits SharpShell.SharpContextMenu.SharpContextMenu

        ''' <summary>
        ''' Get selected items with full path
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        Public ReadOnly Property SelectedItems As System.Collections.Generic.IEnumerable(Of String)
            Get
                Return Me.SelectedItemPaths
            End Get
        End Property

        Protected Overrides Function CanShowMenu() As Boolean
            If Me.SelectedItems.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        Protected Overrides Function CreateMenu() As ContextMenuStrip
            Try
                ind.settings.SettingsLink.LoadProperties()
                My.Application.ChangeUICulture(ind.settings.SettingsLink.LanguageSelected)
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try

            Dim menu As New ContextMenuStrip
            Dim tsiRoot As New ToolStripMenuItem(ind.lang.UILang.WindowsContextMenu_Root)
            tsiRoot.Image = My.Resources.security_lock_16

            'childs
            Dim tsiEncrypt As New ToolStripMenuItem()
            tsiEncrypt.Text = ind.lang.UILang.WindowsContextMenu_Encrypt
            tsiEncrypt.Image = My.Resources.security_lock_16
            AddHandler tsiEncrypt.Click, AddressOf EncryptMenu

            Dim tsiDecrypt As New ToolStripMenuItem()
            tsiDecrypt.Text = ind.lang.UILang.WindowsContextMenu_Decrypt
            tsiDecrypt.Image = My.Resources.security_unlock_16
            AddHandler tsiDecrypt.Click, AddressOf DecryptMenu

            Dim tsiDecryptAndOpen As New ToolStripMenuItem()
            tsiDecryptAndOpen.Text = ind.lang.UILang.WindowsContextMenu_DecryptAndOpen
            tsiDecryptAndOpen.Image = My.Resources.file_DecryptAndOpen_16
            AddHandler tsiDecryptAndOpen.Click, AddressOf DecryptAndOpenMenu

            Dim tsiUseUI As New ToolStripMenuItem()
            tsiUseUI.Text = ind.lang.UILang.WindowsContextMenu_OpenUI
            tsiUseUI.Image = My.Resources.oneCrypt_16
            AddHandler tsiUseUI.Click, AddressOf OpenInUICommand

            tsiRoot.DropDownItems.Add(tsiEncrypt)
            tsiRoot.DropDownItems.Add(tsiDecrypt)
            tsiRoot.DropDownItems.Add(tsiDecryptAndOpen)
            tsiRoot.DropDownItems.Add(tsiUseUI)

            menu.Items.Add(tsiRoot)

            Return menu
        End Function
        ''' <summary>
        ''' Open files in user interface. Todo this we create a file with all the files that the UI open later.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="args"></param>
        Public Sub OpenInUICommand(sender As Object, args As EventArgs)
            Cursor.Current = Cursors.WaitCursor
            Dim filesList As New ArrayList
            Me.configureSelectedFilesByUser(SelectedItems, filesList)
            If filesList.Count = 0 Then
                Exit Sub
            End If

            Try
                Dim fileName As String = "OC_Ctx_{0}.ocLibctx" 'the file with selected paths
                Dim dt As String = Date.Now.ToString
                dt = dt.Replace("/", "_")
                dt = dt.Replace(":", "_")
                dt = dt.Replace(" ", "_")

                fileName = String.Format(fileName, dt)

                Dim FileStr() As String

                ReDim FileStr(filesList.Count - 1)
                filesList.CopyTo(FileStr)
                filesList.Clear()
                filesList = Nothing
                fileName = IO.Path.Combine(My.Application.Info.DirectoryPath, fileName)
                IO.File.WriteAllLines(fileName, FileStr)

                Dim appExe As String = IO.Path.Combine(My.Application.Info.DirectoryPath, ind.cte.AppConst.AppName & ".exe")
                Dim p As New System.Diagnostics.ProcessStartInfo()
                p.Arguments = "/open """ & fileName & """"
                p.WindowStyle = ProcessWindowStyle.Normal
                p.FileName = appExe
                p.UseShellExecute = True
                System.Diagnostics.Process.Start(p)


            Catch ex As Exception
                ind.win.Msg.mError(ex)
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub
        ''' <summary>
        ''' load fileslist object with all the files selected by user
        ''' </summary>
        ''' <param name="SelectedItemPathsToEncrypt"></param>
        ''' <param name="filesList"></param>
        Private Sub configureSelectedFilesByUser(SelectedItemPathsToEncrypt As System.Collections.Generic.IEnumerable(Of String), filesList As ArrayList)

            For Each filePath As String In SelectedItemPathsToEncrypt

                Dim fa As IO.FileAttributes = IO.File.GetAttributes(filePath)
                If (fa And IO.FileAttributes.Directory) = IO.FileAttributes.Directory Then
                    'recursive
                    Dim files As String() = IO.Directory.GetFiles(filePath)
                    configureSelectedFilesByUser(files, filesList)
                    Dim folders As String() = IO.Directory.GetDirectories(filePath)
                    configureSelectedFilesByUser(folders, filesList)

                Else

                    filesList.Add(filePath)

                End If

            Next
        End Sub



        Dim pwdForDecrytp As String = ""
        Dim errorList As ind.tools.error.ErrorVisorHelper

        Public Sub EncryptMenu(sender As Object, args As EventArgs)
            Try

                Dim fd As New frmCtxMenuDecrypt
                fd.configure(ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)

                Dim res As DialogResult = fd.ShowDialog
                If res <> DialogResult.OK Then
                    Exit Sub
                End If

                pwdForDecrytp = fd.txtPassDecrypt.Text.Trim
                errorList = New ind.tools.error.ErrorVisorHelper
                encryptFiles(SelectedItems)

                pwdForDecrytp = ""
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try

            pwdForDecrytp = ""
        End Sub

        Public Sub encryptFiles(SelectedItemPathsToEncrypt As System.Collections.Generic.IEnumerable(Of String))


            For Each filePath As String In SelectedItemPathsToEncrypt

                Dim fa As IO.FileAttributes = IO.File.GetAttributes(filePath)
                If (fa And IO.FileAttributes.Directory) = IO.FileAttributes.Directory Then
                    'recursive
                    Dim files As String() = IO.Directory.GetFiles(filePath)
                    encryptFiles(files)
                    Dim folders As String() = IO.Directory.GetDirectories(filePath)
                    encryptFiles(folders)

                Else
                    Try

                        Dim strFileTo As String = filePath
                        Dim fi2Encrypt As New IO.FileInfo(strFileTo)
                        Dim extension As String = fi2Encrypt.Extension
                        fi2Encrypt = Nothing

                        If extension = "." & ind.cte.AppConst.fileCryptExtension Then
                            'file is encrypted
                            Dim fileWithoutExtension As String = IO.Path.Combine(
                                IO.Path.GetDirectoryName(strFileTo),
                                IO.Path.GetFileNameWithoutExtension(strFileTo))
                            ind.p2p.IPCPipe.sendMessageToServer("E|" & fileWithoutExtension)
                            Continue For
                        End If

                        'Start the encryption.
                        Dim outputFile As String = strFileTo & "." & ind.cte.AppConst.fileCryptExtension

                        Dim crypter As New ind.crypt.Crypto(pwdForDecrytp)
                        Dim res As Boolean = crypter.EncryptOrDecryptFileEX(strFileTo, outputFile, ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt)
                        If res = True Then
                            ind.p2p.IPCPipe.sendMessageToServer("E|" & strFileTo)
                        End If
                    Catch ex As Exception
                        errorList.add(ex)
                    End Try

                End If

            Next
        End Sub


        Protected Sub DecryptMenu(sender As Object, args As EventArgs)
            Try
                Me.decryptFiles(False)
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try

        End Sub

        Protected Sub DecryptAndOpenMenu(sender As Object, args As EventArgs)
            Try
                Me.decryptFiles(True)
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try
        End Sub

        Private Function decryptFiles(Optional openAfterdecrypt As Boolean = False) As Boolean

            Me.pwdForDecrytp = ""
            Dim fd As New frmCtxMenuDecrypt
            fd.configure(ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt)

            Dim res As DialogResult = fd.ShowDialog
            If res <> DialogResult.OK Then
                Return False
            End If
            Me.pwdForDecrytp = fd.txtPassDecrypt.Text.Trim
            'errorList = New ind.tools.error.ErrorVisorHelper
            Me.decryptFiles(SelectedItems, openAfterdecrypt)

            pwdForDecrytp = ""

            Return True
        End Function

        Public Sub decryptFiles(SelectedItemPathsToDecrypt As System.Collections.Generic.IEnumerable(Of String), openAfterdecrypt As Boolean)


            For Each filePath As String In SelectedItemPathsToDecrypt

                Dim fa As IO.FileAttributes = IO.File.GetAttributes(filePath)
                If (fa And IO.FileAttributes.Directory) = IO.FileAttributes.Directory Then
                    'recursive
                    Dim files As String() = IO.Directory.GetFiles(filePath)
                    Me.decryptFiles(files, openAfterdecrypt)
                    Dim folders As String() = IO.Directory.GetDirectories(filePath)
                    Me.decryptFiles(folders, openAfterdecrypt)

                Else
                    Try
                        Dim fileFullPathEncryptedToDeBeDecrypted As String = filePath

                        Dim outputFile As New IO.FileInfo(fileFullPathEncryptedToDeBeDecrypted)
                        Dim outputName As String = ""
                        If outputFile.Extension = "." & ind.cte.AppConst.fileCryptExtension Then
                            'is encrypted
                            outputName = outputFile.Name.Replace(outputFile.Extension, "")
                            Dim outputFullPathWithoutDecryptExtension As String = IO.Path.Combine(outputFile.DirectoryName, outputName)

                            Dim crypter As New ind.crypt.Crypto(pwdForDecrytp)
                            Dim res As Boolean = crypter.EncryptOrDecryptFileEX(fileFullPathEncryptedToDeBeDecrypted, outputFullPathWithoutDecryptExtension, ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt)
                            If res = True Then
                                ind.p2p.IPCPipe.sendMessageToServer("D|" & fileFullPathEncryptedToDeBeDecrypted)
                            End If
                            If res = True And openAfterdecrypt = True Then
                                ind.tools.FileDataHelper.openFile(outputFullPathWithoutDecryptExtension)
                            End If

                        Else
                            'is in decrypted state
                            outputName = outputFile.Name
                            ind.p2p.IPCPipe.sendMessageToServer("D|" & fileFullPathEncryptedToDeBeDecrypted)

                            If openAfterdecrypt = True Then
                                ind.tools.FileDataHelper.openFile(fileFullPathEncryptedToDeBeDecrypted)
                            End If
                        End If

                    Catch ex As Exception
                        'errorList.add(ex, lvi)
                    End Try

                End If

            Next
        End Sub




    End Class

End Namespace
