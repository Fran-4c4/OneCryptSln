

Namespace ind.crypt


    ''' Project	 : ARObj
    ''' Class	 : ar.obj.AccountPeriodCollection
    ''' <summary>
    ''' collection for Accounts periods
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    <Serializable()> _
    Public Class LibraryCollection
        Inherits DictionaryBase

        Default Public Property Item(ByVal key As String) As UCCryptExplorer
            Get
                Return DirectCast(Dictionary(key), UCCryptExplorer)
            End Get
            Set(ByVal Value As UCCryptExplorer)
                Dictionary(key) = Value
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

        Public Sub Add(ByVal key As String, ByVal value As UCCryptExplorer)
            Dictionary.Add(key, value)
        End Sub 'Add

        Public Sub Add(ByVal value As UCCryptExplorer)
            Dictionary.Add(value.LibraryLoaded.libraryID, value)
        End Sub 'Add


        Public Function Contains(ByVal key As String) As Boolean
            Return Dictionary.Contains(key)
        End Function 'Contains

        Public Sub Remove(ByVal key As String)
            Dictionary.Remove(key)
        End Sub 'Remove


    End Class
End Namespace