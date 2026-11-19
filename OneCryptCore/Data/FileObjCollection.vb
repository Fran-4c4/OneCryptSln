

Namespace ind.crypt


    ''' Project	 : ARObj
    ''' Class	 : ar.obj.AccountPeriodCollection
    ''' <summary>
    ''' collection for Accounts periods
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    <Serializable()> _
    Public Class FileObjCollection
        Inherits DictionaryBase



#Region "Events"
        Public Event fileRemovedEvent(value As ind.crypt.FileObject)
        Public Event fileAddedEvent(value As ind.crypt.FileObject)
        Public Event fileChangedEvent(value As ind.crypt.FileObject)
#End Region
        ''' <summary>
        ''' return a fileObject. Key must set without .ocpt extension
        ''' </summary>
        ''' <param name="keyFilePath"></param>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Default Public Property Item(ByVal keyFilePath As String) As ind.crypt.FileObject
            Get
                Dim keyFilePathClean As String = getkeyFilePathClean(keyFilePath)
                Return DirectCast(Dictionary(keyFilePathClean), ind.crypt.FileObject)

            End Get
            Set(ByVal Value As ind.crypt.FileObject)

                Dictionary(Value.FullPathWithoutCryptExtension) = Value
            End Set
        End Property

        Public ReadOnly Property Keys() As ICollection
            Get
                Return Dictionary.Keys
            End Get
        End Property

        Public ReadOnly Property Values() As ICollection
            Get
                Return Dictionary.Values
            End Get
        End Property


        ''' <summary>
        ''' Same as add method but for clarify code
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub AddIfNotExist(ByVal value As ind.crypt.FileObject)
            Me.Add(value)
        End Sub 'Add

        ''' <summary>
        ''' add a file if not exists in collection and raise an event
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub Add(ByVal value As ind.crypt.FileObject)
            If Me.Contains(value.FullPathWithoutCryptExtension) = True Then
                Exit Sub
            End If
            Dictionary.Add(value.FullPathWithoutCryptExtension, value)
            RaiseEvent fileAddedEvent(value)
        End Sub 'Add

        ''' <summary>
        ''' Add a file to the collection without check if exists
        ''' </summary>
        ''' <param name="value"></param>
        ''' <remarks></remarks>
        Public Sub AddDirect(ByVal value As ind.crypt.FileObject)
            Dictionary.Add(value.FullPathWithoutCryptExtension, value)
            RaiseEvent fileAddedEvent(value)
        End Sub 'Add

        Public Function Contains(ByVal keyFilePath As String) As Boolean
            Dim keyFilePathClean As String = getkeyFilePathClean(keyFilePath)
            Return Dictionary.Contains(keyFilePathClean)
        End Function 'Contains
        ''' <summary>
        ''' remove a file from collection
        ''' </summary>
        ''' <param name="keyFilePath"></param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal keyFilePath As String)
            Dim keyFilePathClean As String = getkeyFilePathClean(keyFilePath)
            Dim fio As ind.crypt.FileObject = Me.Item(keyFilePathClean)
            If fio Is Nothing Then
                Exit Sub
            Else
                Dictionary.Remove(keyFilePathClean)
                'delete from database?
                RaiseEvent fileRemovedEvent(fio)
            End If



        End Sub 'Remove

        Public Function getkeyFilePathClean(keyFilePath As String) As String
            If keyFilePath.EndsWith("." & ind.cte.AppConst.fileCryptExtension) Then
                Dim keyFilePathClean As String = keyFilePath.Replace("." & ind.cte.AppConst.fileCryptExtension, "")
                Return keyFilePathClean
            Else
                Return keyFilePath
            End If

        End Function



    End Class
End Namespace