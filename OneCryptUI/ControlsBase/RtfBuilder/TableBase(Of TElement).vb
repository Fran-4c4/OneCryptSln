''' <summary>
''' Represents the base class of tables contained in RTF headers. This class must be inherited.
''' </summary>
''' <typeparam name="TElement">The type of element.</typeparam>
<DebuggerDisplay("Count = {Count}")> _
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public MustInherit Class TableBase(Of TElement)
    Inherits ReadOnlyCollectionBase

    '//properties
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _header As RtfHeader
    ''' <summary>
    ''' Gets the parent <see cref="RtfHeader">RtfHeader</see> of the current instance.
    ''' </summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Friend ReadOnly Property Header() As RtfHeader
        Get
            Return Me._header
        End Get
    End Property

    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="index">The index of the element to retrieve.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Default Public ReadOnly Property Item(ByVal index As Integer) As TElement
        Get
            If Me.Header.Builder.Disposed Then
                Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
            End If
            If index < 0 OrElse index > (MyBase.InnerList.Count - 1) Then
                Return Nothing
            End If
            Return DirectCast(MyBase.InnerList(index), TElement)
        End Get
    End Property

    '//constructors
    Friend Sub New(ByVal header As RtfHeader)
        Me._header = header
    End Sub

    '//methods
    ''' <summary>
    ''' Adds the specified element to the end of the current collection.
    ''' </summary>
    ''' <param name="value">The element to add.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Overridable Function Add(ByVal value As TElement) As Integer
        If Me.Header.Builder.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If
        If Me.Contains(value) Then
            Return Me.IndexOf(value)
        End If
        Return MyBase.InnerList.Add(value)
    End Function

    ''' <summary>
    ''' Determines whether the current collection contains the specified element.
    ''' </summary>
    ''' <param name="value">The value to locate.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Overridable Function Contains(ByVal value As TElement) As Boolean
        If Me.Header.Builder.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If
        Return MyBase.InnerList.Contains(value)
    End Function

    ''' <summary>
    ''' Determines the index of the specified element in the collection.
    ''' </summary>
    ''' <param name="value">The element to locate.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function IndexOf(ByVal value As TElement) As Integer
        If Me.Header.Builder.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If
        Return MyBase.InnerList.IndexOf(value)
    End Function

End Class