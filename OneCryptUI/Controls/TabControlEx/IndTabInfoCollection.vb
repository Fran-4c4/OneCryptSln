


<Serializable()> _
Public Class IndTabInfoCollection
    Inherits DictionaryBase

    Default Public Property Item(ByVal key As Integer) As IndTabInfo
        Get
            Return DirectCast(Dictionary(key), IndTabInfo)
        End Get
        Set(ByVal Value As IndTabInfo)
            Dictionary(key) = Value.keyIndex
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

    Public Sub Add(ByVal value As IndTabInfo)
        Dictionary.Add(value.keyIndex, value)
    End Sub 'Add

    Public Sub Add(ByVal key As Integer, ByVal value As IndTabInfo)
        Dictionary.Add(key, value)
    End Sub 'Add




    Public Function Contains(ByVal key As Integer) As Boolean
        Return Dictionary.Contains(key)
    End Function 'Contains

    Public Sub Remove(ByVal key As Integer)
        Dictionary.Remove(key)
    End Sub 'Remove
    
End Class
