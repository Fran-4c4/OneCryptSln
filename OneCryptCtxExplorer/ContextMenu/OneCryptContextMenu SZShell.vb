Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports SharpShell.Attributes
Imports SharpShell.SharpContextMenu
Imports System.Windows.Forms
Imports LogicNP.EZShellExtensions
Imports System.Drawing

Namespace onecrypt

    '<Guid("390628C0-5F80-4F6D-B915-826F9473D6F2")>
    '<Guid("AD6E16BA-1F06-47DE-8103-A8C18936BF2B")>
    '<Guid("192E4116-3860-4515-9CC3-15C585EE6B90")>
    <Guid("51F768F6-C280-41E4-9ED6-D5E050C92F74") _
    , ComVisible(True) _
    , TargetExtension(SpecialProgIDTargets.AllFiles, False) _
    , TargetExtension(SpecialProgIDTargets.AllFolders, False)> _
    Public Class OneCryptContextMenu
        Inherits ContextMenuExtension

        ''' <summary>
        ''' return selected items used by shell explorer
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SelectedItems As String()

            Get
                Return Me.TargetFiles
            End Get

        End Property


        'Override this method to perform initialzation specific to your contextmenu extension. 
        Protected Overrides Function OnInitialize() As Boolean

            ' Add your initialization code here

            If Me.SelectedItems.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "ComRegisterFunction"
        ' Your assembly should have one static method marked with the 
        ' ComRegisterFunction attribute. The function should return void and take 
        ' one parameter whose type is System.Type.
        ' 
        ' This method is used to register the extension on the system by calling the
        ' RegisterExtension method.
        '
        <ComRegisterFunction()> _
        Public Shared Sub Register(ByVal t As System.Type)
            ContextMenuExtension.RegisterExtension(GetType(OneCryptContextMenu))
        End Sub

        ' Your assembly should have one static method marked with the 
        ' ComUnregisterFunction attribute. The function should return void and take 
        ' one parameter whose type is System.Type.
        ' 
        ' This method is used to register the extension on the system by calling the
        ' UnRegisterExtension method.
        '
        <ComUnregisterFunction()> _
        Public Shared Sub UnRegister(ByVal t As System.Type)
            ContextMenuExtension.UnRegisterExtension(GetType(OneCryptContextMenu))
        End Sub
#End Region

        Private Const verbparent As String = "mni_parent"
        Private Const verbEncrypt As String = "mni_Encrypt"
        Private Const verbdecrypt As String = "mni_decrypt"
        Private Const verbdecryptAndOpen As String = "mni_decryptAndOpen"

        Protected Overrides Sub OnGetMenuItems(ByVal e As LogicNP.EZShellExtensions.GetMenuitemsEventArgs)

            Try
                ind.settings.SettingsLink.LoadProperties()
                My.Application.ChangeUICulture(ind.settings.SettingsLink.LanguageSelected)
            Catch ex As Exception
#If DEBUG Then
                ind.win.Msg.mError(ex)
#End If

            End Try

            ' The .SetBitmap(Nothing) calls below cause the OnDrawMenuItem method to be called when the item bitmap needs drawing
            Dim parent As ShellMenuItem
            parent = e.Menu.AddItem(ind.lang.UILang.WindowsContextMenu_Root, verbparent, "OneCrypt")
            parent.HasSubMenu = True
            parent.SetBitmap(Nothing)

            Dim submenuitem As ShellMenuItem
            submenuitem = parent.SubMenu.InsertItem(ind.lang.UILang.WindowsContextMenu_Encrypt, verbEncrypt, verbEncrypt, 0)
            submenuitem.SetBitmap(Nothing)

            submenuitem = parent.SubMenu.InsertItem(ind.lang.UILang.WindowsContextMenu_Decrypt, verbdecrypt, verbEncrypt, 1)
            submenuitem.SetBitmap(Nothing)

            submenuitem = parent.SubMenu.InsertItem(ind.lang.UILang.WindowsContextMenu_DecryptAndOpen, verbdecryptAndOpen, verbdecryptAndOpen, 2)
            submenuitem.SetBitmap(Nothing)

        End Sub


        ' Override this method to perform your own tasks when any of the 
        ' menu items provided by your contextmenu extension is selected by the user.
        Protected Overrides Function OnExecuteMenuItem(ByVal e As LogicNP.EZShellExtensions.ExecuteItemEventArgs) As Boolean

            'Add your code here
            If e.MenuItem.Verb = verbEncrypt Then
                Me.EncryptMenu()
            ElseIf e.MenuItem.Verb = verbdecrypt Then
                Me.DecryptMenu()
            ElseIf e.MenuItem.Verb = verbdecryptAndOpen Then
                Me.DecryptAndOpenMenu()
            End If

            ' Return value is ignored.
            Return True
        End Function

        Protected Overrides Sub OnDrawMenuItem(ByVal e As LogicNP.EZShellExtensions.EZSDrawItemEventArgs)
            ' Called due to the .SetBitmap(Nothing) calls above
            ' You are only responsible for drawing the bitmaps, the text/background, etc is drawn by the system

            If e.MenuItem.Verb = verbparent Then
                Dim bmp As Bitmap = My.Resources.security_lock_16
                e.Graphics.DrawImage(bmp, 0, 0)
            ElseIf e.MenuItem.Verb = verbEncrypt Then
                Dim bmp As Bitmap = My.Resources.security_unlock_16
                e.Graphics.DrawImage(bmp, 0, 0)
            ElseIf e.MenuItem.Verb = verbdecrypt Then
                Dim bmp As Bitmap = My.Resources.security_lock_16
                e.Graphics.DrawImage(bmp, 0, 0)
            ElseIf e.MenuItem.Verb = verbdecryptAndOpen Then
                Dim bmp As Bitmap = My.Resources.file_DecryptAndOpen_16
                e.Graphics.DrawImage(bmp, 0, 0)
            End If
        End Sub



        Dim pwdForDecrytp As String = ""
        Dim errorList As ind.tools.error.ErrorVisorHelper
        Public Sub EncryptMenu()
            Try

                Dim fd As New frmCtxMenuDecrypt
                fd.configure(ind.crypt.Crypto.CryptoAction.ActionEncrypt)

                Dim res As DialogResult = fd.ShowDialog
                If res = DialogResult.OK Then
                    pwdForDecrytp = fd.txtPassDecrypt.Text.Trim
                Else
                    Exit Sub
                End If

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
                If fa = IO.FileAttributes.Directory Then
                    'recursive
                    Dim files As String() = IO.Directory.GetFiles(filePath)
                    encryptFiles(files)
                    Dim folders As String() = IO.Directory.GetDirectories(filePath)
                    encryptFiles(folders)

                ElseIf fa = IO.FileAttributes.Archive Then
                    Try
                        Dim strFileTo As String = filePath
                        Dim fi2Encrypt As New IO.FileInfo(strFileTo)
                        Dim extension As String = fi2Encrypt.Extension
                        fi2Encrypt = Nothing

                        If extension = "." & ind.cte.AppConst.fileCryptExtension Then
                            'file is encrypted
                            Continue For
                        End If

                        'Start the encryption.
                        Dim outputFile As String = strFileTo & "." & ind.cte.AppConst.fileCryptExtension

                        Dim crypter As New ind.crypt.Crypto(pwdForDecrytp)
                        crypter.EncryptOrDecryptFile(strFileTo, outputFile, ind.crypt.Crypto.CryptoAction.ActionEncrypt)
                        Dim fiResult As New IO.FileInfo(outputFile)
                        ind.encrypter.AppEventsClass.raiseFileEncryptedEvent(fiResult.Name, outputFile, ind.crypt.Crypto.CryptoAction.ActionEncrypt)
                    Catch ex As Exception
                        'errorList.add(ex, lvi)
                    End Try

                End If

            Next
        End Sub


        Protected Sub DecryptMenu()
            Try
                Me.decryptFiles(False)
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try

        End Sub

        Protected Sub DecryptAndOpenMenu()
            Try
                Me.decryptFiles(True)
            Catch ex As Exception
                ind.win.Msg.mError(ex)
            End Try
        End Sub

        Private Function decryptFiles(Optional openAfterdecrypt As Boolean = False) As Boolean

            Me.pwdForDecrytp = ""
            Dim fd As New frmCtxMenuDecrypt
            fd.configure(ind.crypt.Crypto.CryptoAction.ActionDecrypt)

            Dim res As DialogResult = fd.ShowDialog
            If res = DialogResult.OK Then
                Me.pwdForDecrytp = fd.txtPassDecrypt.Text.Trim
            Else
                Return False
            End If

            errorList = New ind.tools.error.ErrorVisorHelper
            Me.decryptFiles(SelectedItems, openAfterdecrypt)

            pwdForDecrytp = ""

            Return True
        End Function

        Public Sub decryptFiles(SelectedItemPathsToDecrypt As System.Collections.Generic.IEnumerable(Of String), openAfterdecrypt As Boolean)


            For Each filePath As String In SelectedItemPathsToDecrypt

                Dim fa As IO.FileAttributes = IO.File.GetAttributes(filePath)
                If fa = IO.FileAttributes.Directory Then
                    'recursive
                    Dim files As String() = IO.Directory.GetFiles(filePath)
                    Me.decryptFiles(files, openAfterdecrypt)
                    Dim folders As String() = IO.Directory.GetDirectories(filePath)
                    Me.decryptFiles(folders, openAfterdecrypt)

                ElseIf fa = IO.FileAttributes.Archive Then
                    Try
                        Dim strFileTo As String = filePath

                        Dim outputFile As New IO.FileInfo(strFileTo)
                        Dim outputName As String = ""
                        If outputFile.Extension = "." & ind.cte.AppConst.fileCryptExtension Then
                            outputName = outputFile.Name.Replace(outputFile.Extension, "")
                        Else
                            outputName = outputFile.Name
                        End If

                        Dim outputFull As String = IO.Path.Combine(outputFile.DirectoryName, outputName)

                        Dim crypter As New ind.crypt.Crypto(pwdForDecrytp)
                        crypter.EncryptOrDecryptFile(strFileTo, outputFull, ind.crypt.Crypto.CryptoAction.ActionDecrypt)
                        ind.encrypter.AppEventsClass.raiseFileEncryptedEvent(outputName, outputFull, ind.crypt.Crypto.CryptoAction.ActionDecrypt)

                        If openAfterdecrypt = True Then
                            ind.tools.FileDataHelper.openFile(outputFull)
                        End If
                    Catch ex As Exception
                        'errorList.add(ex, lvi)
                    End Try

                End If

            Next
        End Sub




    End Class

End Namespace