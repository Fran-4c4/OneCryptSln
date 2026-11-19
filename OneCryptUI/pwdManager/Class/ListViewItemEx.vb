Imports System.Data

Namespace ind.pwd

    Public Class ListViewItemEx
        Inherits Windows.Forms.ListViewItem

        Public id As Integer = 0
        Public itemState As itemStateEnum = itemStateEnum.none
        Public groupPwd As ind.pwd.pwdManagerGlobal.pwdGroupsEnum = pwdManagerGlobal.pwdGroupsEnum.generic
        Public PasswordShow As Boolean = False


        Public Enum itemStateEnum As Byte
            none = 0
            isNew = 1
            isDeleted = 2
            isModified = 3
            isOriginal = 4
        End Enum

        ''' <summary>
        ''' Enum with the position of each column in uccryptexplorer listview
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum lvFieldEnum As Byte
            entry = 0
            username = 1
            password = 2
            url = 3
            notes = 4
            creationDate = 5
            modificationDate = 6
            group = 7
            Autotype = 8
        End Enum

        Public Sub configByObject(ByRef entryObj As ind.pwd.PwdEntryObject)
            Me.configLViEmpty(entryObj.entry)
            Me.Name = entryObj.entry
            Me.Text = entryObj.entry
            Me.groupPwd = entryObj.groupListView

            Me.entryName = entryObj.entry
            Me.UserName = entryObj.username
            Me.passwordOriginal = entryObj.password
            Me.Password = entryObj.password
            Me.Url = entryObj.url
            Me.AutoTypeSequence = entryObj.AutoTypeSequence

            Me.Notes = entryObj.notes
            Me.creationDate = entryObj.creationDate
            Me.modificationDate = entryObj.modificationDate
            Me.SubItems(lvFieldEnum.group).Text = entryObj.groupListView.ToString
        End Sub

        Public Function configLViEmpty(name As String) As ind.pwd.ListViewItemEx
            Me.Name = name
            'we need to add the same number of items as columns
            Me.SubItems.Add("")    'entry
            Me.SubItems.Add("")    'user
            Me.SubItems.Add("")    'pwd
            Me.SubItems.Add("")    'url
            Me.SubItems.Add("")    'notes
            Me.SubItems.Add("")    'creation date
            Me.SubItems.Add("")    'modification date
            Me.SubItems.Add("")    'group
            Return Me
        End Function

        Public Shared Function CreateLViEmpty(name As String, entry As String) As ind.pwd.ListViewItemEx
            Dim lvi As New ind.pwd.ListViewItemEx
            lvi.Name = name
            lvi.Text = entry
            'we need to add the same number of items as columns
            lvi.SubItems.Add("")    'entry
            lvi.SubItems.Add("")    'user
            lvi.SubItems.Add("")    'pwd
            lvi.SubItems.Add("")    'url
            lvi.SubItems.Add("")    'notes
            lvi.SubItems.Add("")    'creation date
            lvi.SubItems.Add("")    'modification date
            Return lvi
        End Function


        Public Sub changePasswordVisualization()
            Me.PasswordShow = Not Me.PasswordShow
            If Me.PasswordShow = True Then
                Me.getSubItem(lvFieldEnum.password).Text = Me.passwordOriginal
            Else
                Me.getSubItem(lvFieldEnum.password).Text = "*****"
            End If

        End Sub

#Region "Easy access to fields"
        Public Function getSubItem(ByRef index As lvFieldEnum) As System.Windows.Forms.ListViewItem.ListViewSubItem
            Return Me.SubItems(index)
        End Function

        Public Property entryName As String
            Get
                Return Me.getSubItem(lvFieldEnum.entry).Text
            End Get
            Set(value As String)
                Me.getSubItem(lvFieldEnum.entry).Text = value
            End Set
        End Property

        Public Property UserName As String
            Get
                Return Me.getSubItem(lvFieldEnum.username).Text
            End Get
            Set(value As String)
                Me.getSubItem(lvFieldEnum.username).Text = value
            End Set
        End Property

        Friend passwordOriginal As String

        Public Property Password As String
            Get
                Return Me.passwordOriginal
            End Get
            Set(value As String)
                Me.passwordOriginal = value.Trim
                If Me.PasswordShow = True Then
                    Me.getSubItem(lvFieldEnum.password).Text = value.Trim
                Else
                    Me.getSubItem(lvFieldEnum.password).Text = "*****"
                End If

            End Set
        End Property
        Public Property Url As String
            Get
                Return Me.getSubItem(lvFieldEnum.url).Text
            End Get
            Set(value As String)
                Me.getSubItem(lvFieldEnum.url).Text = value
            End Set
        End Property

        Public Property AutoTypeSequence As String
            Get
                Return Me.getSubItem(lvFieldEnum.Autotype).Text
            End Get
            Set(value As String)
                Me.getSubItem(lvFieldEnum.Autotype).Text = value
            End Set
        End Property
        Public Property Notes As String
            Get
                Return Me.getSubItem(lvFieldEnum.notes).Text
            End Get
            Set(value As String)
                Me.getSubItem(lvFieldEnum.notes).Text = value
            End Set
        End Property

        Public Property modificationDate As Date
            Get
                Return CDate(Me.getSubItem(lvFieldEnum.modificationDate).Text)
            End Get
            Set(value As Date)
                Me.getSubItem(lvFieldEnum.modificationDate).Text = CStr(value)
            End Set
        End Property
        Public Property creationDate As Date
            Get
                Return CDate(Me.getSubItem(lvFieldEnum.creationDate).Text)
            End Get
            Set(value As Date)
                Me.getSubItem(lvFieldEnum.creationDate).Text = CStr(value)
            End Set
        End Property

        Public Property GroupPwdVal As ind.pwd.pwdManagerGlobal.pwdGroupsEnum
            Get
                Return Me.groupPwd
            End Get
            Set(value As ind.pwd.pwdManagerGlobal.pwdGroupsEnum)
                Me.groupPwd = value
            End Set
        End Property


#End Region

        Public Shared Function getLviExtByLvi(lviEstandar As ListViewItem) As ind.pwd.ListViewItemEx
            If Not lviEstandar Is Nothing Then
                Return DirectCast(lviEstandar, ind.pwd.ListViewItemEx)
            Else
                Return Nothing
            End If
        End Function

    End Class

End Namespace

