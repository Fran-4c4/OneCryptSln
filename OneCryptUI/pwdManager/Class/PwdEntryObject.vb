


Namespace ind.pwd

    Public Class PwdEntryObject
        Public id As Integer
        Public entry As String
        Public groupListView As ind.pwd.pwdManagerGlobal.pwdGroupsEnum = pwdManagerGlobal.pwdGroupsEnum.generic
        Public username As String
        Public password As String
        Public url As String
        Public notes As String
        Public creationDate As Date
        Public modificationDate As Date
        Public AutoTypeSequence As String = "{USERNAME}{TAB}{PASSWORD}{ENTER}"



        ''' <summary>
        ''' Name for columns in datatable asociated to this object
        ''' </summary>
        ''' <remarks></remarks>
        Public Class TableFields
            Public Const entryName As String = "Entry"
            Public Const username As String = "username"
            Public Const password As String = "password"
            Public Const url As String = "url"
            Public Const notes As String = "notes"
            Public Const group As String = "group"
            Public Const creationDate As String = "creationDate"
            Public Const modificationDate As String = "modificationDate"
            Public Const AutoTypeSequence As String = "AutoTypeSequence"
        End Class
        Public Sub New()

        End Sub

        Public Sub New(lvi As ListViewItemEx)
            Me.entry = lvi.entryName
            Me.username = lvi.UserName
            Me.password = lvi.Password
            Me.url = lvi.Url
            Me.AutoTypeSequence = lvi.AutoTypeSequence
            Me.notes = lvi.Notes


            Me.modificationDate = lvi.modificationDate
            Me.creationDate = lvi.creationDate
            Me.groupListView = lvi.GroupPwdVal
        End Sub

        Public Sub New(EntryDataRow As Data.DataRow)
            Me.entry = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.entryName))
            Me.username = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.username))
            Me.password = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.password))
            Me.url = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.url))
            Me.AutoTypeSequence = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.AutoTypeSequence))
            Me.notes = ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.notes))

            Me.modificationDate = CDate(EntryDataRow(TableFields.modificationDate))
            Me.creationDate = CDate(EntryDataRow(TableFields.creationDate))
            Me.groupListView = CType(EntryDataRow(TableFields.group), ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
        End Sub

        Public Sub New(EntryDataRow As Data.DataRow, crypter As ind.crypt.Crypto)
            Me.entry = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.entryName)))
            Me.username = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.username)))
            Me.password = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.password)))
            Me.url = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.url)))
            Me.AutoTypeSequence = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.AutoTypeSequence)))
            Me.notes = crypter.DecryptData(ind.string.StrUtil.StringAvoidNull(EntryDataRow(TableFields.notes)))

            Me.modificationDate = CDate(EntryDataRow(TableFields.modificationDate))
            Me.creationDate = CDate(EntryDataRow(TableFields.creationDate))
            Me.groupListView = CType(EntryDataRow(TableFields.group), ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
        End Sub


    End Class

End Namespace

