''' <summary>
''' Represents the header in RTF. This class cannot be inherited.
''' </summary>
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public NotInheritable Class RtfHeader
    Implements IDisposable

    '//fields
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private disposed As Boolean

    '//properties
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _builder As RtfBuilder
    ''' <summary>
    ''' Gets the parent <see cref="RtfBuilder">RtfBuilder</see> of the current instance.
    ''' </summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Friend ReadOnly Property Builder() As RtfBuilder
        Get
            Return Me._builder
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _colors As ColorTable
    ''' <summary>
    ''' Gets the colors contained in the header.
    ''' </summary>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public ReadOnly Property Colors() As ColorTable
        Get
            If Me.Builder.Disposed Then
                Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
            End If
            If Me._colors Is Nothing Then
                Me._colors = New ColorTable(Me)
            End If
            Return Me._colors
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _fonts As FontTable
    ''' <summary>
    ''' Gets the fonts contained in the header.
    ''' </summary>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public ReadOnly Property Fonts() As FontTable
        Get
            If Me.Builder.Disposed Then
                Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
            End If
            If Me._fonts Is Nothing Then
                Me._fonts = New FontTable(Me)
            End If
            Return Me._fonts
        End Get
    End Property

    '//constructors
    Friend Sub New(ByVal builder As RtfBuilder)
        Me._builder = builder
    End Sub

    '//methods
    ''' <summary>
    ''' Releases all resources used by the current instance.
    ''' </summary>
    Friend Sub Dispose() Implements IDisposable.Dispose
        If Not Me.disposed Then
            If Me._fonts IsNot Nothing Then
                Me._fonts.Dispose()
            End If
            Me.disposed = True

            Me.Builder.Dispose()
        End If
    End Sub

End Class