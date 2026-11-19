Imports System.Data
Imports System.ComponentModel
Imports System.IO


Public Class UCpwdListView

#Region "1. Variables "
    Friend WithEvents myAppEvents As New ind.encrypter.AppEventsClass
    Private isConfigured As Boolean = False

#End Region

    Private Sub UCpwdListView_Load(sender As Object, e As EventArgs) Handles Me.Load
        If isConfigured = False Then
            Me.configureGroups()
            Me.ConfigureToolStripIcons()
            Me.myAppEvents_LanguageChangedEvent()
            Me.loadPwdRepository()
            isConfigured = True
        End If

    End Sub

#Region "Configuration"
    Private Sub ConfigureToolStripIcons()
        Me.btnAddEntry.Image = My.Resources.pwdMng_addentry_32
        Me.btnCopypwd.Image = My.Resources.pwdMng_copypwd_32
        Me.btnCopyUserName.Image = My.Resources.pwdMng_copyuser_32
        Me.btnOpenURL.Image = My.Resources.pwdMng_BrowserOpen_32
        Me.btnCopyURL.Image = My.Resources.pwdMng_copyBrowser_32
        Me.btnViewGroups.Image = My.Resources.grouplvi16
        Me.btnShowPwd.Image = My.Resources.pwdManager_ShowPwd
        Me.btnAutoType.Image = My.Resources.key_32
        Me.btnConfig.Image = My.Resources.pwdmng_options_32


        'menuitems
        Me.btnCopypwdMni.Image = My.Resources.pwdMng_copypwd_32
        Me.btnCopyUserNameMni.Image = My.Resources.pwdMng_copyuser_32
        Me.btnCopyUrlMni.Image = My.Resources.pwdMng_copyBrowser_32
        Me.btnOpenURLMni.Image = My.Resources.pwdMng_BrowserOpen_16
        Me.btnAutoTypeMni.Image = My.Resources.key_16

    End Sub




    ''' <summary>
    ''' Create datatable for repository of listview
    ''' </summary>
    ''' <remarks></remarks>
    Private Function configureDataRepository() As DataTable
        Dim dtPwds As New DataTable
        dtPwds.TableName = "OneCryptPwds"
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.entryName, GetType(System.String)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.username, GetType(System.String)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.password, GetType(System.String)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.url, GetType(System.String)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.notes, GetType(System.String)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.group, GetType(System.Int32)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.AutoTypeSequence, GetType(System.String)))

        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.creationDate, GetType(System.DateTime)))
        dtPwds.Columns.Add(New DataColumn(ind.pwd.PwdEntryObject.TableFields.modificationDate, GetType(System.DateTime)))

        ' Set the entry column as the primary key column.
        dtPwds.PrimaryKey = New DataColumn() {dtPwds.Columns(0)}

        'table.Rows.Add(New Object() {1, 1, #12/2/2003#})

        dtPwds.AcceptChanges()
        Return dtPwds
    End Function
    ''' <summary>
    ''' Configure groups in listview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configureGroups()
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.generic.ToString, ind.lang.UCpwdListView.listViewGroup_Generic))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Websites.ToString, ind.lang.UCpwdListView.listViewGroup_Website))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Email.ToString, ind.lang.UCpwdListView.listViewGroup_email))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.creditcard.ToString, ind.lang.UCpwdListView.listViewGroup_CreditCard))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.bank.ToString, ind.lang.UCpwdListView.listViewGroup_Bank))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.computer.ToString, ind.lang.UCpwdListView.listViewGroup_Computer))
        Me.lstViewEnt.Groups.Add(New ListViewGroup(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.other.ToString, ind.lang.UCpwdListView.listViewGroup_Other))
    End Sub
#End Region

#Region "Repository"
    ''' <summary>
    ''' load passwords stored in file
    ''' </summary>
    Private Sub loadPwdRepository()
        Try
            Dim dtPwds As DataTable = configureDataRepository()
            Dim filepath As String = IO.Path.Combine(ind.settings.SettingsLink.PwdDatabaseFilePath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
            If IO.File.Exists(filepath) = False Then
                Exit Sub
            End If

            Dim crypter As New ind.crypt.Crypto(ind.cte.AppVars.AppPassword)
            ''filepath = "F:\OneCrypt\Masive encription\Test Crypt3.pdf.ocpt"
            ''Dim memStream As New MemoryStream(crypter.deCryptFileAsStream(filepath))
            'Dim fs2decrypt As New FileStream(filepath, FileMode.Open, FileAccess.Read)
            'Dim bytearrayinput(CInt(fs2decrypt.Length - 1)) As Byte
            'fs2decrypt.Read(bytearrayinput, 0, bytearrayinput.Length)
            'fs2decrypt.Close()
            'Dim memStream As New MemoryStream(bytearrayinput)
            'memStream.Position = 0
            'Dim streamreader As New StreamReader(memStream)
            'Dim str As String = streamreader.ReadToEnd
            'Debug.WriteLine(str)
            'memStream.Position = 0
            'Using memStream
            '    dtPwds.ReadXml(memStream)
            '    memStream.Close()
            'End Using
            Try
                crypter.EncryptOrDecryptFileEX(filepath, filepath & ".temp", ind.crypt.Crypto.CryptoActionEnum.ActionDecrypt, ind.crypt.Crypto.DeleteOriginalEnum.NotDeleteOriginal)
            Catch ex As Exception
                saveRepositoryCopy(filepath)
                Dim msg As String = String.Format(ind.lang.UCpwdMngLang.errmsg_passwordrepositoryerror, vbCr & ex.Message, ind.pwd.pwdManagerGlobal.OneCryptpwdFile & "." & Date.Now.ToString("yyyMMdd"))
                ind.win.Msg.mError(msg)
                Exit Sub
            End Try

            dtPwds.ReadXml(filepath & ".temp")
            IO.File.Delete(filepath & ".temp")

            For Each dr As DataRow In dtPwds.Rows
                Try
                    Dim pwdEntryObj As New ind.pwd.PwdEntryObject(EntryDataRow:=dr)
                    Dim lvi As New ind.pwd.ListViewItemEx
                    lvi.configByObject(pwdEntryObj)
                    lvi.Group = Me.lstViewEnt.Groups(lvi.groupPwd)
                    lvi.ImageIndex = lvi.groupPwd
                    Me.lstViewEnt.Items.Add(lvi)
                Catch ex As Exception

                End Try

            Next
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try


    End Sub

    Private Sub saveRepositoryCopy(filepath As String)
        Try
            IO.File.Copy(filepath, filepath & "." & Date.Now.ToString("yyyyMMdd"))
        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' Save listview to xml based on a datatable. We asume that we set some pwd sometimes. If we are continuosly changing data or the volume of data is higher we need to change method to only sve changes.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub saveRepository()
        Dim dtPwds As DataTable = configureDataRepository()
        Dim SelectedItemsCount As Integer = Me.lstViewEnt.SelectedItems.Count
        Dim lviCol As ListView.ListViewItemCollection = lstViewEnt.Items
        Dim crypter As New ind.crypt.Crypto(ind.cte.AppVars.AppPassword)

        For Each lvi As ind.pwd.ListViewItemEx In lviCol
            Dim lviex As ind.pwd.ListViewItemEx = CType(lvi, ind.pwd.ListViewItemEx)
            Dim dr As DataRow = dtPwds.NewRow
            dr(ind.pwd.PwdEntryObject.TableFields.entryName) = lviex.entryName
            dr(ind.pwd.PwdEntryObject.TableFields.username) = lviex.UserName
            dr(ind.pwd.PwdEntryObject.TableFields.password) = lviex.Password
            dr(ind.pwd.PwdEntryObject.TableFields.url) = lviex.Url
            dr(ind.pwd.PwdEntryObject.TableFields.AutoTypeSequence) = lviex.AutoTypeSequence
            dr(ind.pwd.PwdEntryObject.TableFields.notes) = lviex.Notes
            dr(ind.pwd.PwdEntryObject.TableFields.group) = CInt(lviex.GroupPwdVal)

            dr(ind.pwd.PwdEntryObject.TableFields.modificationDate) = lviex.modificationDate
            dr(ind.pwd.PwdEntryObject.TableFields.creationDate) = lviex.creationDate

            dtPwds.Rows.Add(dr)
        Next
        dtPwds.AcceptChanges()
        Dim filepath As String = IO.Path.Combine(ind.settings.SettingsLink.PwdDatabaseFilePath, ind.pwd.pwdManagerGlobal.OneCryptpwdFile)
        
        dtPwds.WriteXml(filepath & ".temp")
        crypter.EncryptOrDecryptFileEX(filepath & ".temp", filepath, ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt, ind.crypt.Crypto.DeleteOriginalEnum.DeleteAfterOperations)


        'Dim swriter As New IO.StringWriter
        'dtPwds.WriteXml(swriter, XmlWriteMode.WriteSchema, True)
        ''If you need the XML only once, then perhaps no need to take it even to a local variable
        'Dim xml As String = swriter.ToString()
        'Debug.WriteLine(xml)

        'Dim memStream As New MemoryStream()
        'dtPwds.WriteXml(memStream)
        'memStream.Position = 0
        'Dim streamreader As New StreamReader(memStream)
        'Dim str As String = streamreader.ReadToEnd

        'Debug.WriteLine(str)
        'Dim memStream1 As New MemoryStream()
        'Dim streamWriter1 As New StreamWriter(memStream1)
        'streamWriter1.Write("<?xml version='1.0' standalone='yes'?>" & str)
        'streamWriter1.Flush()
        'memStream1.Position = 0
        'Dim streamreader1 As New StreamReader(memStream1)
        'str = streamreader1.ReadToEnd

        'Debug.WriteLine(str)
        ''streamreader.Close()
        'Using memStream1
        '    crypter.enCryptToFileByStream(memStream1, filepath)
        '    memStream1.Close()
        'End Using



        'Dim result As String = crypter.enCryptByStream(memStream)
        'Using outfile As New StreamWriter(filepath)
        '    outfile.Write(result)
        'End Using
    End Sub
#End Region



#Region "Listview methods"

#Region "Keyboard Events "

    Private Sub lstViewEnt_KeyDown(sender As Object, e As KeyEventArgs) Handles lstViewEnt.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.U 'username
                    Me.btnCopyUserName.PerformClick()
                Case Keys.P 'pwd
                    Me.btnCopypwd.PerformClick()
                Case Keys.W 'open website
                    Me.btnOpenURL.PerformClick()
                Case Keys.R 'copy website
                    Me.btnCopyURL.PerformClick()
                Case Keys.A 'Autotype
                    Me.btnAutoType.PerformClick()
            End Select

        End If
    End Sub
    ''' <summary>
    ''' Handles Delete Key processing for the ListView
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">a KeyEventsArgs</param>
    ''' <remarks>Modified version of Calum McLellan's code from ExpList.</remarks>
    Private Sub ExpList_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstViewEnt.KeyUp
        If e.KeyCode = Keys.Delete Then
            Me.removeEntry()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.editEntry()
        End If
    End Sub


    Private Sub btnRemoveEntryMni_Click(sender As Object, e As EventArgs) Handles btnRemoveEntryMni.Click
        Try
            Me.removeEntry()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub removeEntry()
        Try
            Dim elementToDelete As Integer = Me.lstViewEnt.SelectedItems.Count
            If elementToDelete = 0 Then
                Exit Sub
            End If

            Dim fd As frmDlg = ind.win.Msg.mQuestionDlg(ind.lang.UCPwdControllerLang.DeleteEntryMessage, "", ind.win.Msg.MessageIconEnum.document_delete)
            fd.OK_Button.Text = ind.lang.UCCryptExplorerLang.EncryptDecryptItem_questionFileNotfound_BtnDelete
            fd.Cancel_Button.Text = ind.lang.UCCryptExplorerLang.EncryptDecryptItem_questionFileNotfound_BtnNotDelete
            fd.setFocusOnBtn(frmDlg.FocusButtonEnum.cancelBtn)

            If fd.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If

            Me.lstViewEnt.BeginUpdate()
            Dim i As Integer

            For i = elementToDelete - 1 To 0 Step -1
                Dim lviEx As ind.pwd.ListViewItemEx = ind.pwd.ListViewItemEx.getLviExtByLvi(Me.lstViewEnt.SelectedItems(i))
                lviEx.Remove()
            Next
            Me.lstViewEnt.EndUpdate()
            Me.saveRepository()
            'RaiseEvent LvItemAddedCountEvent(listViewItemOperationEnum.removeItem, elementToDelete, Me.lvTreeExp.Items.Count)
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try


    End Sub

#End Region

    Private Sub lstView_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lstViewEnt.ItemSelectionChanged
        Dim lviListSelCount As Integer = Me.lstViewEnt.SelectedItems.Count
        If lviListSelCount = 1 Then
            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            If lvi.AutoTypeSequence = "" Then
                Me.btnAutoType.Enabled = False
            Else
                Me.btnAutoType.Enabled = True
            End If
            Me.btnCopyUserName.Enabled = True
            Me.btnCopypwd.Enabled = True
            Me.btnCopyURL.Enabled = True
            Me.btnOpenURL.Enabled = True
            Me.ShowEntryDetails(lvi)
        Else
            Me.ShowEntryDetails(Nothing)
            Me.btnCopyUserName.Enabled = False
            Me.btnCopypwd.Enabled = False
            Me.btnOpenURL.Enabled = False
            Me.btnCopyURL.Enabled = False
            Me.btnAutoType.Enabled = False
        End If
    End Sub

    Private Sub lstViewEnt_DoubleClick(sender As Object, e As EventArgs) Handles lstViewEnt.DoubleClick
        Me.editEntry()
    End Sub

    Private Sub editEntry()
        Dim lv As ListView = Me.lstViewEnt
        Dim lvi As ListView.SelectedListViewItemCollection = lv.SelectedItems

        Try
            If lvi.Count = 0 Then
                Exit Sub
            End If
            Dim entryAddCtrl As New UCEntryAdd
            entryAddCtrl.entryObj = CType(lvi(0), ind.pwd.ListViewItemEx)

            Dim f As Form = UCEntryAdd.OpenAsForm(entryAddCtrl)
            If f.ShowDialog <> DialogResult.OK Then
                Exit Sub
            Else
                Me.saveRepository()
            End If
            'Me.UCpwdListView1.addListViewItem(entryAddCtrl.entryObj)
        Catch ex As Exception
            ind.win.Msg.mError(ex)

        End Try
    End Sub

    Public Sub addEntry(pwdEntryObj As ind.pwd.PwdEntryObject)
        Dim lvi As New ind.pwd.ListViewItemEx
        lvi.configByObject(pwdEntryObj)
        addListViewItem(lvi)
        'save to repository
        Me.saveRepository()
    End Sub

    Public Sub addListViewItem(lvi As ind.pwd.ListViewItemEx)
        If Me.lstViewEnt.Items.ContainsKey(lvi.entryName) = False Then
            lvi.Group = Me.lstViewEnt.Groups(lvi.groupPwd)
            lvi.ImageIndex = lvi.groupPwd
            Me.lstViewEnt.Items.Add(lvi)
        Else
            'edit
        End If
    End Sub

    Private Sub lvTreeExp_MouseUp(sender As Object, e As MouseEventArgs) Handles lstViewEnt.MouseUp
        Dim lv As ListView = DirectCast(sender, ListView)
        'Checking the Mouse right Button
        If e.Button = MouseButtons.Right Then
            Dim SelectedItemsCount As Integer = lv.SelectedItems.Count
            Dim lvi As ListView.SelectedListViewItemCollection = lv.SelectedItems
            If SelectedItemsCount = 0 Then
                lv.ContextMenuStrip = Nothing
                Exit Sub
            Else
                lv.ContextMenuStrip = Me.ContextMenuStrip1
            End If
            If SelectedItemsCount = 1 Then
                'check if encrypted to avoid to show encrypt menu
                lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
            Else
                'show all
                lv.ContextMenuStrip.Show(lv, New System.Drawing.Point(e.X, e.Y))
            End If

        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        Try
            Dim lvCol As ListView.SelectedListViewItemCollection = Me.lstViewEnt.SelectedItems
            Dim count As Integer = lvCol.Count

            If count = 1 Then
                Me.btnCopyUserNameMni.Visible = True
                Me.btnCopypwdMni.Visible = True
                Me.btnCopyUrlMni.Visible = True
                Me.btnOpenURLMni.Visible = True
                Me.btnEditEntryMni.Visible = True
                Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
                If lvi.AutoTypeSequence = "" Then
                    Me.btnAutoTypeMni.Visible = False
                Else
                    Me.btnAutoTypeMni.Visible = True
                End If
            ElseIf count > 1 Then
                Me.btnCopyUserNameMni.Visible = False
                Me.btnCopypwdMni.Visible = False
                Me.btnCopyUrlMni.Visible = False
                Me.btnOpenURLMni.Visible = False
                Me.btnEditEntryMni.Visible = False
                Me.btnAutoTypeMni.Visible = False
            End If



        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub


#End Region


#Region "Buttons"
    Private Sub btnAddEntry_Click(sender As Object, e As EventArgs) Handles btnAddEntry.Click
        Try
            Dim entryAddCtrl As New UCEntryAdd
#If debug Then
    entryAddCtrl.test()
#End If

            Dim f As Form = UCEntryAdd.OpenAsForm(entryAddCtrl)
            If f.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If
            Dim eo As ind.pwd.ListViewItemEx = entryAddCtrl.entryObj
            Me.addListViewItem(eo)
            Me.saveRepository()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnViewGroups_Click(sender As Object, e As EventArgs) Handles btnViewGroups.Click
        Me.lstViewEnt.ShowGroups = Not Me.lstViewEnt.ShowGroups
    End Sub

    Private Sub btnShowPwd_Click(sender As Object, e As EventArgs) Handles btnShowPwd.Click
        Try
            Dim lviCol As ListView.SelectedListViewItemCollection = Me.lstViewEnt.SelectedItems
            If lviCol.Count > 0 Then
                For Each lvi As ListViewItem In lviCol
                    Dim lviEx As ind.pwd.ListViewItemEx = ind.pwd.ListViewItemEx.getLviExtByLvi(lvi)

                    lviEx.changePasswordVisualization()
                Next
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try


    End Sub


    Private Sub btnAutoType_Click(sender As Object, e As EventArgs) Handles btnAutoType.Click, btnAutoTypeMni.Click
        Try
            If Me.lstViewEnt.SelectedItems.Count = 0 Then
                Exit Sub
            End If

            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            KeePass.Util.HotKeyManager.Initialize(Me.ParentForm)
            Dim pe As New KeePassLib.PwEntryObj
            pe.AutoTypeSequence = lvi.AutoTypeSequence
            pe.Strings.Set(KeePassLib.PwDefs.UserNameField, New KeePassLib.Security.ProtectedString(lvi.UserName))
            pe.Strings.Set(KeePassLib.PwDefs.PasswordField, New KeePassLib.Security.ProtectedString(lvi.Password))


            KeePass.Util.AutoType.PerformIntoPreviousWindow(Me.ParentForm, pe, Nothing)

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnEditEntryMni_Click(sender As Object, e As EventArgs) Handles btnEditEntryMni.Click
        Me.editEntry()
    End Sub
#End Region



    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.btnAddEntry.ToolTipText = ind.lang.UCpwdMngLang.btnAddEntry_tt
        Me.btnCopyUserName.ToolTipText = ind.lang.UCpwdMngLang.btnCopyUserName_tt
        Me.btnCopypwd.ToolTipText = ind.lang.UCpwdMngLang.btnCopypwd_tt

        Me.btnOpenURL.ToolTipText = ind.lang.UCpwdMngLang.btnOpenWebsite_tt
        Me.btnCopyURL.ToolTipText = ind.lang.UCpwdMngLang.btnCopyURL
        Me.btnAutoType.ToolTipText = ind.lang.UCpwdMngLang.btnAutoTypeMni_TT

        Me.btnViewGroups.ToolTipText = ind.lang.UCpwdMngLang.btnViewGroups_TT
        Me.btnShowPwd.ToolTipText = ind.lang.UCpwdMngLang.btnShowPwd_TT
        Me.btnConfig.ToolTipText = ind.lang.UCpwdMngLang.btnConfig_TT

        'mnuitems
        Me.btnCopyUserNameMni.Text = ind.lang.UCpwdMngLang.btnCopyUserName_tt
        Me.btnCopypwdMni.Text = ind.lang.UCpwdMngLang.btnCopypwd_tt

        Me.btnCopyUrlMni.Text = ind.lang.UCpwdMngLang.btnCopyURL
        Me.btnOpenURLMni.Text = ind.lang.UCpwdMngLang.btnOpenWebsite_tt

        Me.btnAutoTypeMni.Text = ind.lang.UCpwdMngLang.btnAutoType
        Me.btnAutoTypeMni.ToolTipText = ind.lang.UCpwdMngLang.btnAutoTypeMni_TT

        'listview columns
        Me.clnEntry.Text = ind.lang.UCpwdListView.col_entry
        Me.clnUserName.Text = ind.lang.UCpwdListView.col_username
        Me.clnPassword.Text = ind.lang.UCpwdListView.col_password
        Me.clnURL.Text = ind.lang.UCpwdListView.col_url
        Me.clnNotes.Text = ind.lang.UCpwdListView.col_notes
        Me.clnDateModified.Text = ind.lang.UCpwdListView.col_dateModification
        Me.clnDateCreation.Text = ind.lang.UCpwdListView.col_dateCreation
        Me.clnAutotype.Text = ind.lang.UCpwdListView.col_autotype

        'listview groups
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.generic.ToString).Header = ind.lang.UCpwdListView.listViewGroup_Generic
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Websites.ToString).Header = ind.lang.UCpwdListView.listViewGroup_Website
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.Email.ToString).Header = ind.lang.UCpwdListView.listViewGroup_email
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.creditcard.ToString).Header = ind.lang.UCpwdListView.listViewGroup_CreditCard
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.bank.ToString).Header = ind.lang.UCpwdListView.listViewGroup_Bank
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.computer.ToString).Header = ind.lang.UCpwdListView.listViewGroup_Computer
        Me.lstViewEnt.Groups(ind.pwd.pwdManagerGlobal.pwdGroupsEnum.other.ToString).Header = ind.lang.UCpwdListView.listViewGroup_Other

    End Sub


#Region "Copy entry"
    Private Sub btnCopyUserName_Click(sender As Object, e As EventArgs) Handles btnCopyUserName.Click, btnCopyUserNameMni.Click
        Try
            If Me.lstViewEnt.SelectedItems.Count = 0 Then
                Exit Sub
            End If

            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            My.Computer.Clipboard.SetText(lvi.UserName)

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnCopypwd_Click(sender As Object, e As EventArgs) Handles btnCopypwd.Click, btnCopypwdMni.Click
        Try
            If Me.lstViewEnt.SelectedItems.Count = 0 Then
                Exit Sub
            End If

            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            My.Computer.Clipboard.SetText(lvi.Password)

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnCopyWebSiteMni_Click(sender As Object, e As EventArgs) Handles btnCopyURL.Click, btnCopyUrlMni.Click
        Try
            If Me.lstViewEnt.SelectedItems.Count = 0 Then
                Exit Sub
            End If

            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            My.Computer.Clipboard.SetText(lvi.Url)

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    Private Sub btnOpenURL_Click(sender As Object, e As EventArgs) Handles btnOpenURL.Click, btnOpenURLMni.Click
        Try
            If Me.lstViewEnt.SelectedItems.Count = 0 Then
                Exit Sub
            End If

            Dim lvi As ind.pwd.ListViewItemEx = CType(Me.lstViewEnt.SelectedItems(0), ind.pwd.ListViewItemEx)
            My.Computer.Clipboard.SetText(lvi.Url)
            Cursor = Cursors.AppStarting
            Process.Start(lvi.Url)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            ind.win.Msg.mError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region



#Region "View entry details"
    ''' <summary>
    ''' show entry details in bottom panel
    ''' </summary>
    ''' <param name="pe"></param>
    Private Sub ShowEntryDetails(pe As ind.pwd.ListViewItemEx)
        If pe Is Nothing Then
            rhEntryView.Text = String.Empty
            Return
        End If

        Using rb = New RtfBuilder(MyBase.Font, MyBase.ForeColor)

            With rb
                .Append(ind.lang.UCpwdListView.col_entry & ":", FontStyle.Bold)
                .Append(pe.Name, FontStyle.Regular)

                If pe.UserName <> "" Then
                    .Append("  ")
                    .Append(ind.lang.UCpwdListView.col_username & ":", FontStyle.Bold)
                    .Append(pe.UserName, FontStyle.Regular)
                End If
                'If pe.Password <> "" Then
                '    .Append("  ")
                '    .Append(ind.lang.UCpwdListView.col_password & ":", FontStyle.Bold)
                '    .Append(pe.Password, FontStyle.Regular)
                'End If
                If pe.Url <> "" Then
                    .Append("  ")
                    .Append(ind.lang.UCpwdListView.col_url & ":", FontStyle.Bold)
                    .Append(pe.Url, FontStyle.Regular)
                End If
                If pe.AutoTypeSequence <> "" Then
                    .Append("  ")
                    .Append(ind.lang.UCpwdListView.col_autotype & ":", FontStyle.Bold)
                    .Append(pe.AutoTypeSequence, FontStyle.Regular)
                End If
                If pe.Notes <> "" Then
                    .AppendLine(1)
                    .Append(ind.lang.UCpwdListView.col_notes & ":", FontStyle.Bold)
                    .AppendLine(1)
                    .Append(pe.Notes, FontStyle.Regular)
                End If


            End With

            Me.rhEntryView.Rtf = rb.ToString()
        End Using

        rhEntryView.[Select](0, 0)
    End Sub
#End Region



    Private Sub btnConfig_Click(sender As Object, e As EventArgs) Handles btnConfig.Click
        Try
            Dim f As New frmPwdConfig
            f.ShowDialog()
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub
End Class
