Imports System.Drawing

''' <summary>
''' Represents the font table control group ( \fonttbl ) in RTF. This class cannot be inherited.
''' </summary>
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public NotInheritable Class FontTable
    Inherits TableBase(Of Font)
    Implements IDisposable

    '//fields
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private disposed As Boolean

    '//constructors
    Friend Sub New(ByVal header As RtfHeader)
        MyBase.New(header)
    End Sub

    '//methods
    ''' <summary>
    ''' Adds the specified font to the end of the current collection.
    ''' </summary>
    ''' <param name="value">The font to add.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Overrides Function Add(ByVal value As System.Drawing.Font) As Integer
        Return MyBase.Add(DirectCast(value.Clone(), Font))
    End Function

    ''' <summary>
    ''' Releases all resources used by the current instance.
    ''' </summary>
    Friend Sub Dispose() Implements IDisposable.Dispose
        If Not Me.disposed Then
            For Each font As Font In Me
                font.Dispose()
                font = Nothing
            Next
            Me.disposed = True

            MyBase.InnerList.Clear()
            MyBase.Header.Builder.Dispose()
        End If
    End Sub

End Class