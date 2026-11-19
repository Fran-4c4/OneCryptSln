''' <summary>
''' Represents a collection of <see cref="ControlWord">ControlWord</see> objects. This class cannot be inherited.
''' </summary>
<DebuggerDisplay("Count = {Count}")> _
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public NotInheritable Class ControlWordCollection
    Inherits ReadOnlyCollectionBase

    '//properties
    ''' <summary>
    ''' Gets the <see cref="ControlWord">ControlWord</see> at the specified index.
    ''' </summary>
    ''' <param name="index">The index of the element to retrieve.</param>
    Default Public ReadOnly Property Item(ByVal index As Integer) As ControlWord
        Get
            If index < 0 OrElse index > (MyBase.InnerList.Count - 1) Then
                Return Nothing
            End If
            Return DirectCast(MyBase.InnerList(index), ControlWord)
        End Get
    End Property

    '//constructors
    Friend Sub New(ByVal c As ICollection)
        MyBase.InnerList.AddRange(c)
    End Sub

    '//methods
    ''' <summary>
    ''' Determines whether the current collection contains the specified <see cref="ControlWord">ControlWord</see>.
    ''' </summary>
    ''' <param name="controlWord">The <see cref="ControlWord">ControlWord</see> to locate.</param>
    Public Function Contains(ByVal controlWord As ControlWord) As Boolean
        Return MyBase.InnerList.Contains(controlWord)
    End Function

    ''' <summary>
    ''' Determines whether the current collection contains the specified control word value.
    ''' </summary>
    ''' <param name="value">The value to locate.</param>
    Public Function Contains(ByVal value As String) As Boolean
        For Each cw As ControlWord In Me
            If cw.Value.Equals(value) Then
                Return True
            End If
        Next
        Return False
    End Function

End Class
