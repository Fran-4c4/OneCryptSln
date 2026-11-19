''' <summary>
''' Represents a control word in an RTF document. This class cannot be inherited.
''' </summary>
<DebuggerDisplay("Index = {Index}, Value = {Value}")> _
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public NotInheritable Class ControlWord

    '//properties
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _length As Integer
    ''' <summary>
    ''' Gets the length of the current <see cref="ControlWord">ControlWord</see>.
    ''' </summary>
    Public ReadOnly Property Length() As Integer
        Get
            Return Me._length
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _index As Integer
    ''' <summary>
    ''' Gets the index of the current <see cref="ControlWord">ControlWord</see>.
    ''' </summary>
    Public ReadOnly Property Index() As Integer
        Get
            Return Me._index
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _value As String
    ''' <summary>
    ''' Gets the value of the current <see cref="ControlWord">ControlWord</see>.
    ''' </summary>
    Public ReadOnly Property Value() As String
        Get
            If Me._value Is Nothing Then
                Me._value = String.Empty
            End If
            Return Me._value
        End Get
    End Property

    '//constructors
    Friend Sub New(ByVal value As String, ByVal length As Integer, ByVal index As Integer)
        Me._value = value
        Me._length = length
        Me._index = index
    End Sub

    '//methods
    ''' <summary>
    ''' Determines whether the specified object equals the current instance.
    ''' </summary>
    ''' <param name="obj">The object to test.</param>
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If Not TypeOf obj Is ControlWord Then
            Return False
        End If
        Return Me.Equals(DirectCast(obj, ControlWord))
    End Function

    ''' <summary>
    ''' Determines whether the specified <see cref="ControlWord">ControlWord</see> equals the current instance.
    ''' </summary>
    ''' <param name="controlWord">The <see cref="ControlWord">ControlWord</see> to test.</param>
    Public Overloads Function Equals(ByVal controlWord As ControlWord) As Boolean
        If controlWord.Value.Equals(Me.Value) Then
            If controlWord.Index.Equals(Me.Index) Then
                If controlWord.Length.Equals(Me.Length) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Returns the hash code for this <see cref="ControlWord">ControlWord</see>.
    ''' </summary>
    Public Overrides Function GetHashCode() As Integer
        Return Me.Value.GetHashCode() Xor _
               Me.Index.GetHashCode() Xor _
               Me.Length.GetHashCode()
    End Function

    ''' <summary>
    ''' Returns a <see cref="String">String</see> object that represents the current <see cref="ControlWord">ControlWord</see> object.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return Me.Value
    End Function

End Class
