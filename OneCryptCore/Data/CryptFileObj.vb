

Namespace ind.crypt
    ''' <summary>
    ''' class to encapsulate the data of file used in listview
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FileObject
        ''' <summary>
        ''' raise when status change(for example in encrypt/decrypt)
        ''' </summary>
        ''' <param name="fileObj"></param>
        Public Event fileStatusEvent(fileObj As FileObject)
        ''' <summary>
        ''' raised when file change
        ''' </summary>
        ''' <param name="fileObj"></param>
        Public Event fileChangedEvent(fileObj As FileObject)

        Public Enum checkFileStatusEnum As Integer
            none = -1
            exist = 0
            notExist = 1
            existEncrypted = 2
            existDecrypted = 3
        End Enum
        ''' <summary>
        ''' id in database
        ''' </summary>
        ''' <remarks></remarks>
        Public id As Integer
        ''' <summary>
        ''' file name
        ''' </summary>
        ''' <remarks></remarks>
        Public FileName As String
        ''' <summary>
        ''' return filename without Crypt extension
        ''' WARNING: Only without cryptextension. Original extension remains
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileNameWithoutExtension As String
            Get
                Return getFileNameWithoutCryptExtension(Me.FileName)
            End Get
        End Property

        Private _fullPath As String
        ''' <summary>
        ''' fullpath of the file
        ''' </summary>
        ''' <remarks></remarks>
        Public Property FullPath As String
            Get
                Return _fullPath
            End Get
            Set(value As String)
                _fullPath = value
            End Set
        End Property

        ''' <summary>
        ''' return full path wiothout crypt extension
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property FullPathWithoutCryptExtension As String
            Get

                Dim FullPathWithoutCryptExtension_m As String = Me._fullPath.Replace("." & ind.cte.AppConst.fileCryptExtension, "")

                Return FullPathWithoutCryptExtension_m
            End Get
        End Property


        Private Extension_m As String
        ''' <summary>
        ''' original extension of the file. Ocpt extension never is showed except in fullpath
        ''' </summary>
        ''' <remarks></remarks>
        Public Property extension As String
            Get
                Return Me.Extension_m
            End Get
            Set(value As String)
                If value Is Nothing Then
                    Me.Extension_m = ""
                Else
                    If value.Length > 0 And value.StartsWith(".") Then
                        Me.Extension_m = value.Remove(0, 1)
                    Else
                        Me.Extension_m = "" & value
                    End If

                End If
            End Set
        End Property

        ''' <summary>
        ''' kb size
        ''' </summary>
        ''' <remarks></remarks>
        Private mLength As Long
        ''' <summary>
        ''' last acces of the user
        ''' </summary>
        ''' <remarks></remarks>
        Public dateLastAccess As Date
        ''' <summary>
        ''' computer where the file was added
        ''' </summary>
        ''' <remarks></remarks>
        Public ComputerLastAccess As String
        ''' <summary>
        ''' indicate that the file was not found in the location.
        ''' Used in datebase, but not used yet
        ''' </summary>
        Public FileNotFound As Boolean
        ''' <summary>
        ''' Enum with the position of each column in uccryptexplorer listview
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum lvFieldEnum As Byte
            name = 0
            ''' <summary>
            ''' file Full path
            ''' </summary>
            path = 1
            type = 2
            Length = 3
            dateLastAccess = 4
            ComputerLastAccess = 5
            FileNotFound = 6
            datemodified = 7
            dateCreated = 8
            attributes = 9
        End Enum

        Public state As ind.crypt.Crypto.CryptoStateEnum


        Public Sub New()

        End Sub

        Public Sub New(fileName As String, filePath As String, Extension As String, Length As Long)
            Me.FileName = fileName
            Me.configureFullPath(filePath)
            Me.mLength = Length

        End Sub

        Public Sub New(listViewItemSource As Windows.Forms.ListViewItem)
            Me.id = CInt(listViewItemSource.Tag)
            Me.FileName = listViewItemSource.SubItems(ind.crypt.FileObject.lvFieldEnum.name).Text
            Me.configureFullPath(listViewItemSource.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text)

            setLenghtValueValidator(listViewItemSource.SubItems(ind.crypt.FileObject.lvFieldEnum.Length).Text)
            Me.dateLastAccess = CDate(listViewItemSource.SubItems(ind.crypt.FileObject.lvFieldEnum.dateLastAccess).Text)
            Me.ComputerLastAccess = listViewItemSource.SubItems(ind.crypt.FileObject.lvFieldEnum.ComputerLastAccess).Text
        End Sub

        Public Sub New(id As Integer, name As String, FileNamePathQualified As String, size As Long, lastAccess As Date, Computer As String)
            Me.id = id
            Me.FileName = name
            Me.configureFullPath(FileNamePathQualified)

            Me.length = CLng(size)
            Me.dateLastAccess = lastAccess
            Me.ComputerLastAccess = Computer
            'cf.FileNotFound = datos(ind.crypt.CryptFileObj.lvFieldEnum.FileNotFound)
        End Sub

        Public Sub New(fileFI As IO.FileInfo)
            Me.createByFile(fileFI)
        End Sub
        Public Sub configureFullPath(value As String)
            If _fullPath <> value Then
                _fullPath = value
                CheckIFileisCrypted(_fullPath)
            End If
        End Sub
        ''' <summary>
        ''' create the object based on file on disk
        ''' </summary>
        ''' <param name="file"></param>
        ''' <remarks></remarks>
        Public Sub createByFile(file As IO.FileInfo)

            Me.FileName = file.Name.Replace("." & ind.cte.AppConst.fileCryptExtension, "")
            Me.configureFullPath(file.FullName)


            Me.mLength = file.Length
            Me.dateLastAccess = file.LastAccessTime
            If FullPath.StartsWith("\\") = True Then
                'It´s in a net
                Dim f As String = file.FullName.Remove(0, 2)
                Dim searchBar As Integer = f.IndexOf("\")
                f = f.Substring(0, searchBar)
                Me.ComputerLastAccess = f
            Else
                Me.ComputerLastAccess = My.Computer.Name
            End If
        End Sub

        ''' <summary>
        ''' check if a file is encrypted
        ''' </summary>
        ''' <param name="fullpath"></param>
        Public Sub CheckIFileisCrypted(fullpath As String)
            Dim ext As String = IO.Path.GetExtension(fullpath)
            If ext = "." & ind.cte.AppConst.fileCryptExtension Then
                Dim nameWithoutExtension As String = IO.Path.GetFileNameWithoutExtension(fullpath)
                Dim realFileextension As String = IO.Path.GetExtension(nameWithoutExtension)
                Me.extension = realFileextension
                Me.state = Crypto.CryptoStateEnum.IsEncripted
            Else
                Me.extension = ext
                Me.state = Crypto.CryptoStateEnum.IsDecrypted
            End If
        End Sub

        Public Function fullpathIsCrypted() As Boolean
            If _fullPath.EndsWith("." & ind.cte.AppConst.fileCryptExtension) Then
                Return True
            Else
                Return False
            End If

        End Function
        ''' <summary>
        ''' check if file exist in disk. Check with actual name and then with or without encryption extension
        ''' </summary>
        ''' <returns></returns>
        Public Function checkFileStatusOnDisk() As checkFileStatusEnum
            Dim status As checkFileStatusEnum = checkFileStatusEnum.none
            Dim fileExist As Boolean = IO.File.Exists(_fullPath)
            If fileExist = True Then
                status = checkFileStatusEnum.exist
                Me.FileNotFound = False
                'file exist as is in _fullPath. Everything is ok
                Return status
            Else
                'not exist as is in _fullpath
                status = checkFileStatusEnum.notExist
                If fullpathIsCrypted() = True Then
                    'check if existObj without crypt extension
                    fileExist = IO.File.Exists(FullPathWithoutCryptExtension)
                    If fileExist = True Then
                        'file existObj with diferent extension so we need to change 
                        status = checkFileStatusEnum.existDecrypted
                        Me.FileNotFound = False
                        Return status
                    Else
                        status = checkFileStatusEnum.notExist
                        Me.FileNotFound = True
                        Return status
                    End If
                Else
                    'check with encrypt extension
                    fileExist = IO.File.Exists(_fullPath & "." & ind.cte.AppConst.fileCryptExtension)
                    If fileExist = True Then
                        status = checkFileStatusEnum.existEncrypted
                        Me.FileNotFound = False
                        Return status
                    Else
                        status = checkFileStatusEnum.notExist
                        Me.FileNotFound = True
                        Return status
                    End If

                End If
            End If

            Return status
        End Function
        ''' <summary>
        ''' check if file exist in path and if exist with diferent status then change to new status.
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Function checkIfFileExist() As checkFileStatusEnum
            If Me._fullPath.Length = 0 Then
                Return checkFileStatusEnum.none
            End If

            Dim status As checkFileStatusEnum = checkFileStatusOnDisk()
            If status = checkFileStatusEnum.exist Then
                If Me.state = Crypto.CryptoStateEnum.IsEncripted And fullpathIsCrypted() Then
                    'state is sync
                Else
                    'state is diferent
                    Me.state = Crypto.CryptoStateEnum.IsDecrypted
                    status = checkFileStatusEnum.existDecrypted
                End If
            End If
            If status = checkFileStatusEnum.existEncrypted Then
                'file existObj with diferent extension so we need to change 
                _fullPath = _fullPath & "." & ind.cte.AppConst.fileCryptExtension
                Me.state = Crypto.CryptoStateEnum.IsEncripted
                Return status
            ElseIf status = checkFileStatusEnum.existDecrypted Then
                'file existObj with diferent extension so we need to change 
                _fullPath = FullPathWithoutCryptExtension
                Me.state = Crypto.CryptoStateEnum.IsDecrypted
                Return status
            Else
                Return status
            End If

            Return status

        End Function


        ''' <summary>
        ''' update this object using other fileobject
        ''' </summary>
        ''' <param name="fileObj"></param>
        Public Sub UpdateByFileObject(fileObj As FileObject)
            'Me.FileName = fileObj.FileName
            Me.FullPath = fileObj.FullPath
            Me.length = fileObj.length
            Me.dateLastAccess = fileObj.dateLastAccess
            Me.ComputerLastAccess = fileObj.ComputerLastAccess
            'RaiseEvent fileChangedEvent(Me) 'do not raise event here without check crossthread
        End Sub
        ''' <summary>
        ''' update data using a fileinfo
        ''' </summary>
        ''' <param name="file"></param>
        Public Sub UpdateByFileInfo(file As IO.FileInfo)

            Me.FullPath = file.FullName
            Me.mLength = file.Length
            Me.dateLastAccess = file.LastAccessTime
            If FullPath.StartsWith("\\") = True Then
                'It´s in a net
                Dim f As String = file.FullName.Remove(0, 2)
                Dim searchBar As Integer = f.IndexOf("\")
                f = f.Substring(0, searchBar)
                Me.ComputerLastAccess = f
            Else
                Me.ComputerLastAccess = My.Computer.Name
            End If

            'RaiseEvent fileChangedEvent(Me) 'do not raise event here without check crossthread
        End Sub

        ''' <summary>
        ''' raise change in file event
        ''' </summary>
        Public Sub RaiseChangedEvent()
            RaiseEvent fileChangedEvent(Me)
        End Sub
        ''' <summary>
        ''' raise change status event
        ''' </summary>
        Public Sub RaisefileStatusEvent()
            RaiseEvent fileStatusEvent(Me)
        End Sub
        ''' <summary>
        ''' length of file. To set the value use <see cref=" setLenghtValueValidator">setLenghtValueValidator</see>
        ''' </summary>
        ''' <value></value>
        Public Property length As Long
            Get
                Return Me.mLength
            End Get
            Set(value As Long)
                Me.mLength = value
            End Set
        End Property
        ''' <summary>
        ''' validate lenght value
        ''' </summary>
        ''' <param name="lenStr"></param>
        Public Sub setLenghtValueValidator(lenStr As String)

            Dim lng As Long
            Dim lenStrUpper As String = lenStr.ToUpper

            'basic value is bytes
            If lenStrUpper.EndsWith("BYTES") Then
                lenStrUpper = lenStrUpper.Replace("BYTES", "").Trim
                lng = CLng(lenStrUpper)
            ElseIf lenStrUpper.EndsWith("KB") Then
                lenStrUpper = lenStrUpper.Replace("KB", "").Trim
                lng = CLng(lenStrUpper)
                lng = lng * 1024 'convert to bytes

            ElseIf lenStrUpper.EndsWith("MB") Then
                lenStrUpper = lenStrUpper.Replace("MB", "").Trim
                lng = CLng(lenStrUpper)
                lng = lng * 1048576 'convert to bytes
            Else
                lng = CLng(lenStrUpper)
            End If

            Me.mLength = lng

        End Sub
        ''' <summary>
        ''' used to facilitate the access to the values
        ''' </summary>
        ''' <param name="lvi"></param>
        ''' <param name="field"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getValueByField(lvi As Windows.Forms.ListViewItem, field As lvFieldEnum) As String
            If lvi Is Nothing Then
                Throw New Exception("You send a listviewitem nothing")
            End If

            Return lvi.SubItems(field).Text
        End Function
        ''' <summary>
        ''' return string without Crypt extension
        ''' WARNING: Only remove cryptextension
        ''' </summary>
        ''' <param name="FileNameWithExtension"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getFileNameWithoutCryptExtension(FileNameWithExtension As String) As String
            Dim extension As String = IO.Path.GetExtension(FileNameWithExtension)
            If extension <> ind.cte.AppConst.fileCryptExtension Then
                Return FileNameWithExtension
            Else
                Dim fileWithouExtension As String = FileNameWithExtension.Replace(extension, "")
                Return fileWithouExtension
            End If
        End Function

        ''' <summary>
        ''' Check if a file is encrypted based only in final extension. 
        ''' </summary>
        ''' <param name="FileNamePath"></param>
        ''' <returns>True if file has ocpt extension otherwise false</returns>
        ''' <remarks></remarks>
        Public Shared Function fileIsEncrypted(FileNamePath As String) As Boolean
            Dim extension As String = IO.Path.GetExtension(FileNamePath) 'returns with "." before
            If extension.ToLower = "." & ind.cte.AppConst.fileCryptExtension Then
                Return True
            Else
                Return False
            End If

        End Function

    End Class
End Namespace