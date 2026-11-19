Imports System.Drawing
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Runtime.InteropServices

''' <summary>
''' Represents a mutable string of rich text. This class cannot be inherited.
''' </summary>
''' <remarks>
'''   The <see cref="RtfBuilder">RtfBuilder</see> class always creates a copy of the <see cref="Font">Font</see> objects added.
'''   You must explicity dispose the font objects you use with the builder. You must also call <see cref="RtfBuilder.Dispose">RtfBuilder.Dispose</see>
'''   on the builder to ensure that the font copies are disposed properly.
''' </remarks>
'<DebuggerNonUserCode()> _
'<DebuggerStepThrough()> _
Public NotInheritable Class RtfBuilder
    Implements IDisposable

    '//fields
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private contentBuilder As StringBuilder

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private plainTextBuilder As StringBuilder

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private headerBuilder As StringBuilder

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private Shared ReadOnly DefaultCodePage As String = "ansicpg1252"

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private Shared ReadOnly DefaultFontIndex As Integer = 0

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private Shared ReadOnly DefaultColorIndex As Integer = 0

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private Shared ReadOnly ControlWordRegex As Regex

    ''' <summary>
    ''' Indicates the RTF version the <see cref="RtfBuilder">RtfBuilder</see> will generate. This field is read-only.
    ''' </summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Public Shared ReadOnly RtfVersion As Integer = 1

    ''' <summary>
    ''' Indicates the character set used by the <see cref="RtfBuilder">RtfBuilder</see>. This field is read-only.
    ''' </summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Public Shared ReadOnly CharacterSet As String = "ansi"

    '//properties
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _disposed As Boolean
    ''' <summary>
    ''' Gets a value that indicates whether the current instance is disposed.
    ''' </summary>
    Public ReadOnly Property Disposed() As Boolean
        Get
            Return Me._disposed
        End Get
    End Property

    <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
    Private _header As RtfHeader
    ''' <summary>
    ''' Gets the <see cref="RtfHeader">RtfHeader</see> of the current instance.
    ''' </summary>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see> if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see> property is true.</exception>
    Public ReadOnly Property Header() As RtfHeader
        Get
            If Me.Disposed Then
                Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
            End If
            If Me._header Is Nothing Then
                Me._header = New RtfHeader(Me)
            End If
            Return Me._header
        End Get
    End Property

    ''' <summary>
    ''' Gets the length of the entire RTF string in the current <see cref="RtfBuilder">RtfBuilder</see>.
    ''' </summary>
    Public ReadOnly Property Length() As Integer
        Get
            Return Me.ToString().Length
        End Get
    End Property

    ''' <summary>
    ''' Gets the plain text representation of the RTF.
    ''' </summary>
    Public ReadOnly Property Text() As String
        Get
            Return Me.ToString(RtfFormat.PlainText)
        End Get
    End Property

    '//constructors
    Shared Sub New()
        RtfBuilder.ControlWordRegex = New Regex("(?<!\\)\\[a-z0-9\*\~\-\:\|\`\'_]+(-[0-9]+)?", _
                                                RegexOptions.Compiled Or RegexOptions.IgnoreCase)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="RtfBuilder">RtfBuilder</see> class.
    ''' </summary>
    ''' <param name="defaultFont">Specifies the default <see cref="Font">Font</see> object for the builder.</param>
    ''' <param name="defaultColor">Specifies the default <see cref="Color">Color</see> object for the builder.</param>
    Public Sub New(ByVal defaultFont As Font, ByVal defaultColor As Color)
        If defaultFont Is Nothing Then
            Throw New ArgumentNullException("defaultFont")
        End If

        Me.Header.Fonts.Add(defaultFont)
        Me.Header.Colors.Add(defaultColor)

        Me.contentBuilder = New StringBuilder()
        Me.plainTextBuilder = New StringBuilder()
    End Sub

    '//methods
    ''' <summary>
    ''' Appends a <see cref="Char">Char</see> object to the end of the builder.
    ''' </summary>
    ''' <param name="value">The character to append.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As Char) As RtfBuilder
        Return Me.Append(value, 1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="Char">Char</see> object a specified amount of times to the end of the builder.
    ''' </summary>
    ''' <param name="value">The character to append.</param>
    ''' <param name="count">The amount of characters to create.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As Char, ByVal count As Integer) As RtfBuilder
        Return Me.Append(New String(value, count))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the default styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String) As RtfBuilder
        Return Me.Append(value, RtfBuilder.DefaultFontIndex)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified font to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal font As Font) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(font))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the font at the specified index to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontIndex As Integer) As RtfBuilder
        Return Me.Append(value, fontIndex, -1, -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified font styling to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontStyle As FontStyle) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(fontStyle))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified fore color to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal foreColor As Color) As RtfBuilder
        Return Me.Append(value, RtfBuilder.DefaultFontIndex, _
                         Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color) As RtfBuilder
        Return Me.Append(value, RtfBuilder.DefaultFontIndex, _
                         Me.EnsureColor(foreColor), Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal font As Font, _
                           ByVal foreColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontIndex As Integer, _
                           ByVal foreColor As Color) As RtfBuilder
        Return Me.Append(value, fontIndex, Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontStyle As FontStyle, _
                           ByVal foreColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal font As Font, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontIndex As Integer, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color) As RtfBuilder
        Return Me.Append(value, fontIndex, Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontStyle As FontStyle, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <param name="highlightColor">The highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal font As Font, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color, _
                           ByVal highlightColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), Me.EnsureColor(highlightColor))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <param name="highlightColor">The highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontIndex As Integer, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color, _
                           ByVal highlightColor As Color) As RtfBuilder
        Return Me.Append(value, fontIndex, Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), Me.EnsureColor(highlightColor))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <param name="highlightColor">The highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontStyle As FontStyle, _
                           ByVal foreColor As Color, _
                           ByVal backColor As Color, _
                           ByVal highlightColor As Color) As RtfBuilder
        Return Me.Append(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), _
                         Me.EnsureColor(backColor), Me.EnsureColor(highlightColor))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object with the specified styles to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColorIndex">The index of the fore color to use.</param>
    ''' <param name="backColorIndex">The index of the background color to use.</param>
    ''' <param name="highlightColorIndex">The index of the highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function Append(ByVal value As String, _
                           ByVal fontIndex As Integer, _
                           ByVal foreColorIndex As Integer, _
                           ByVal backColorIndex As Integer, _
                           ByVal highlightColorIndex As Integer) As RtfBuilder
        If Me.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If

        If value Is Nothing Then
            Throw New ArgumentNullException("value")
        End If

        Dim fN = If(fontIndex = -1, RtfBuilder.DefaultFontIndex, fontIndex)
        Dim font = Me.Header.Fonts(fN)
        Dim fS = CInt(Math.Ceiling(font.Size * 2.0F))
        Dim bold = font.Bold
        Dim italic = font.Italic
        Dim underline = font.Underline
        Dim strikeout = font.Strikeout
        Dim highlight = (highlightColorIndex > -1)
        Dim cF = If(foreColorIndex = -1, RtfBuilder.DefaultColorIndex, foreColorIndex)
        Dim cB = If(backColorIndex = -1, Me.Header.Colors.Add(Color.White), backColorIndex)

        Me.contentBuilder.AppendFormat("\frmPwdMgrContainer{0}\fs{1}\cf{2}\cb{3}", fN, fS, cF, cB)
        If bold Then
            Me.contentBuilder.Append("\b")
        Else
            Me.contentBuilder.Append("\b0")
        End If
        If italic Then
            Me.contentBuilder.Append("\i")
        Else
            Me.contentBuilder.Append("\i0")
        End If
        If underline Then
            Me.contentBuilder.Append("\ul")
        Else
            Me.contentBuilder.Append("\ulnone")
        End If
        If strikeout Then
            Me.contentBuilder.Append("\strike")
        Else
            Me.contentBuilder.Append("\strike0")
        End If
        If highlight Then
            Me.contentBuilder.AppendFormat("\highlight{0}", highlightColorIndex)
        Else
            Me.contentBuilder.AppendFormat("\highlight{0}", cB)
        End If
        Me.contentBuilder.AppendFormat(" {0}", Me.EnsureSafeRtf(value))
        Me.plainTextBuilder.Append(value)
        Return Me
    End Function

    ''' <summary>
    ''' Appends a formatted <see cref="String">String</see> object to the end of the builder.
    ''' </summary>
    ''' <param name="format">A composite format string.</param>
    ''' <param name="args">An <see cref="System.Object">System.Object</see> array containing zero or more objects to format.</param>
    Public Function AppendFormat(ByVal format As String, ByVal ParamArray args As Object()) As RtfBuilder
        Return Me.Append(String.Format(format, args))
    End Function

    ''' <summary>
    ''' Appends a formatted <see cref="String">String</see> object using the specified <see cref="IFormatProvider">IFormatProvider</see> to the end of the builder.
    ''' </summary>
    ''' <param name="provider">An <see cref="IFormatProvider">IFormatProvider</see> that supplies culture specific formatting information.</param>
    ''' <param name="format">A composite format string.</param>
    ''' <param name="args">An <see cref="System.Object">System.Object</see> array containing zero or more objects to format.</param>
    Public Function AppendFormat(ByVal provider As IFormatProvider, ByVal format As String, ByVal ParamArray args As Object()) As RtfBuilder
        Return Me.Append(String.Format(provider, format, args))
    End Function

    ''' <summary>
    ''' Appends the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine() As RtfBuilder
        Return Me.AppendLine(1)
    End Function

    ''' <summary>
    ''' Appends the default line terminator specified for RTF a specified amount of times to the end of the builder.
    ''' </summary>
    ''' <param name="count">The amount of new lines to append.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal count As Integer) As RtfBuilder
        If Me.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If
        Me.contentBuilder.AppendLine()
        For i = 0 To count - 1
            Me.contentBuilder.Append("\par")
        Next
        Me.plainTextBuilder.AppendLine()
        Return Me
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the default styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String) As RtfBuilder
        Return Me.AppendLine(value, RtfBuilder.DefaultFontIndex)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified font and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal font As Font) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(font))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified font styling and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontStyle As FontStyle) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(fontStyle))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the font at the specified index and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontIndex As Integer) As RtfBuilder
        Return Me.AppendLine(value, fontIndex, -1, -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified fore color and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal foreColor As Color) As RtfBuilder
        Return Me.AppendLine(value, RtfBuilder.DefaultFontIndex, _
                             Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal foreColor As Color, _
                               ByVal backColor As Color) As RtfBuilder
        Return Me.AppendLine(value, RtfBuilder.DefaultFontIndex, _
                             Me.EnsureColor(foreColor), Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontIndex As Integer, _
                               ByVal foreColor As Color) As RtfBuilder
        Return Me.AppendLine(value, fontIndex, Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal font As Font, _
                               ByVal foreColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontStyle As FontStyle, _
                               ByVal foreColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), -1, -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal font As Font, _
                               ByVal foreColor As Color, _
                               ByVal backColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), _
                             Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontStyle As FontStyle, _
                               ByVal foreColor As Color, _
                               ByVal backColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), _
                             Me.EnsureColor(backColor), -1)
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="font">The font to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <param name="highlightColor">The highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal font As Font, _
                               ByVal foreColor As Color, _
                               ByVal backColor As Color, _
                               ByVal highlightColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(font), Me.EnsureColor(foreColor), _
                             Me.EnsureColor(backColor), Me.EnsureColor(highlightColor))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontStyle">The font styling to apply to the string.</param>
    ''' <param name="foreColor">The fore color to use.</param>
    ''' <param name="backColor">The background color to use.</param>
    ''' <param name="highlightColor">The highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontStyle As FontStyle, _
                               ByVal foreColor As Color, _
                               ByVal backColor As Color, _
                               ByVal highlightColor As Color) As RtfBuilder
        Return Me.AppendLine(value, Me.EnsureFont(fontStyle), Me.EnsureColor(foreColor), _
                             Me.EnsureColor(backColor), Me.EnsureColor(highlightColor))
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object using the specified styles and the default line terminator specified for RTF to the end of the builder.
    ''' </summary>
    ''' <param name="value">The string to append.</param>
    ''' <param name="fontIndex">The index of the font to use.</param>
    ''' <param name="foreColorIndex">The index of the fore color to use.</param>
    ''' <param name="backColorIndex">The index of the background color to use.</param>
    ''' <param name="highlightColorIndex">The index of the highlight color to use.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Function AppendLine(ByVal value As String, _
                               ByVal fontIndex As Integer, _
                               ByVal foreColorIndex As Integer, _
                               ByVal backColorIndex As Integer, _
                               ByVal highlightColorIndex As Integer) As RtfBuilder
        Me.Append(value, fontIndex, foreColorIndex, backColorIndex, highlightColorIndex)
        Return Me.AppendLine()
    End Function

    ''' <summary>
    ''' Appends a <see cref="String">String</see> object that represents RTF to the end of the builder.
    ''' </summary>
    ''' <param name="rtf">The RTF string to append.</param>
    Public Function AppendRtf(ByVal rtf As String) As RtfBuilder
        Me.contentBuilder.Append(rtf)
        Using converter = New System.Windows.Forms.RichTextBox()
            converter.Rtf = String.Format("{{\rtf{0}\{1}\ansicpg1252\deff{2}\deflang4105\uc1 {3}\par}}", _
                                          RtfBuilder.RtfVersion, RtfBuilder.CharacterSet, RtfBuilder.DefaultFontIndex, rtf)

            Me.plainTextBuilder.Append(converter.Text)
        End Using
        Return Me
    End Function

    ''' <summary>
    ''' Appends a formatted <see cref="String">String</see> object that represents RTF to the end of the builder.
    ''' </summary>
    ''' <param name="format">A composite format string.</param>
    ''' <param name="args">An <see cref="System.Object">System.Object</see> array containing zero or more objects to format.</param>
    Public Function AppendRtf(ByVal format As String, ByVal ParamArray args As Object()) As RtfBuilder
        Return Me.AppendRtf(String.Format(format, args))
    End Function

    ''' <summary>
    ''' Gets all instances of the RTF control words in current builder.
    ''' </summary>
    Public Function GetControlWords() As ControlWordCollection
        Return RtfBuilder.GetControlWords(Me)
    End Function

    ''' <summary>
    ''' Saves the current builder to the specified file path.
    ''' </summary>
    ''' <param name="filePath">The file to save to.</param>
    Public Function SaveFile(ByVal filePath As String) As IO.FileInfo
        Return RtfBuilder.SaveFile(Me, filePath, RtfFormat.Rtf)
    End Function

    ''' <summary>
    ''' Saves the current builder to the specified file path.
    ''' </summary>
    ''' <param name="filePath">The file to save to.</param>
    ''' <param name="format">One of the <see cref="RtfFormat">RtfFormat</see> values.</param>
    Public Function SaveFile(ByVal filePath As String, ByVal format As RtfFormat) As IO.FileInfo
        Return RtfBuilder.SaveFile(Me, filePath, format)
    End Function

    ''' <summary>
    ''' Clears the current instance to the default state.
    ''' </summary>
    Public Sub Clear()
        Me.contentBuilder = Nothing
        Me.plainTextBuilder = Nothing

        Me.contentBuilder = New StringBuilder()
        Me.plainTextBuilder = New StringBuilder()
    End Sub

    ''' <summary>
    ''' Returns the <see cref="String">String</see> representation of the current <see cref="RtfBuilder">RtfBuilder</see>.
    ''' </summary>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Overrides Function ToString() As String
        If Me.Disposed Then
            Return GetType(RtfBuilder).FullName
        End If
        Me.headerBuilder = New StringBuilder("{"c)

        With Me.headerBuilder

            '//build initial header values
            .AppendFormat("\rtf{0}", RtfBuilder.RtfVersion)
            .AppendFormat("\{0}", RtfBuilder.CharacterSet)
            .AppendFormat("\{0}", RtfBuilder.DefaultCodePage)
            .AppendFormat("\deff{0}", RtfBuilder.DefaultFontIndex)
            .Append("\deflang4105")

            '//build font table
            Dim fonts = Me.Header.Fonts
            If fonts.Count > 0 Then
                .Append("{\fonttbl")
                Dim i = 0
                For Each font As Font In fonts
                    .AppendFormat("{{\frmPwdMgrContainer{0}\fnil\fcharset0 {1};}}", i, font.Name)
                    i += 1
                Next
                .Append("}"c)
            End If

            '//build color table
            Dim colors = Me.Header.Colors
            If colors.Count > 0 Then
                .Append("{\colortbl")
                For Each color As Color In colors
                    .AppendFormat("\red{0}\green{1}\blue{2};", _
                                  color.R, color.G, color.B)
                Next
                .Append("}"c)
            End If
            .Append(Me.contentBuilder.ToString())
            .Append("}"c)

            Return .ToString()
        End With
    End Function

    ''' <summary>
    ''' Returns the <see cref="String">String</see> representation of the current <see cref="RtfBuilder">RtfBuilder</see>.
    ''' </summary>
    ''' <param name="format">One of the <see cref="RtfFormat">RtfFormat</see> values.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Overloads Function ToString(ByVal format As RtfFormat) As String
        If Not [Enum].IsDefined(GetType(RtfFormat), format) Then
            Throw New InvalidEnumArgumentException(String.Format("'format' is not a valid {0} value.", _
                                                                 GetType(RtfFormat).Name))
        End If
        If Me.Disposed Then
            Return GetType(RtfBuilder).FullName
        End If
        Dim plainText = If(format = RtfFormat.Rtf, _
                           False, True)
        If plainText Then

            Return Me.plainTextBuilder.ToString()
        End If
        Return Me.ToString()
    End Function

    ''' <summary>
    ''' Releases all resources used by the current instance.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        If Not Me.Disposed Then
            If Me._header IsNot Nothing Then
                Me._header.Dispose()
            End If
            Me._header = Nothing
            Me.contentBuilder = Nothing
            Me.headerBuilder = Nothing
            Me.plainTextBuilder = Nothing
            Me._disposed = True
        End If
    End Sub

    ''' <summary>
    ''' Attempts to convert the specified <see cref="String">String</see> object that represents RTF to its plain text equivalent.
    ''' </summary>
    ''' <param name="rtf">The RTF to convert.</param>
    Public Shared Function ConvertRtf(ByVal rtf As String) As String
        Using converter = New System.Windows.Forms.RichTextBox()
            Try
                converter.Rtf = String.Format("{{\rtf{0}\{1}\ansicpg1252\deff{2}\deflang4105\uc1 {3}\par}}", _
                                          RtfBuilder.RtfVersion, RtfBuilder.CharacterSet, RtfBuilder.DefaultFontIndex, rtf)
            Catch ex As ArgumentException
                Return Nothing
            End Try
            Return converter.Text
        End Using
    End Function

    ''' <summary>
    ''' Creates an instance of the default <see cref="RtfBuilder">RtfBuilder</see>.
    ''' </summary>
    Public Shared Function CreateDefault() As RtfBuilder
        Using f = New Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
            Return New RtfBuilder(f, Color.Black)
        End Using
    End Function

    ''' <summary>
    ''' Gets all instances of the RTF control words in the specified builder.
    ''' </summary>
    ''' <param name="builder">The <see cref="RtfBuilder">RtfBuilder</see> to obtain the control words from.</param>
    ''' <exception cref="ObjectDisposedException">Throws an <see cref="ObjectDisposedException">ObjectDisposedException</see>
    '''   if the <see cref="RtfBuilder">RtfBuilder</see>'s <see cref="RtfBuilder.Disposed">RtfBuilder.Disposed</see>
    '''   property is true.
    ''' </exception>
    Public Shared Function GetControlWords(ByVal builder As RtfBuilder) As ControlWordCollection
        If builder Is Nothing Then
            Throw New ArgumentNullException("builder")
        End If
        If builder.Disposed Then
            Throw New ObjectDisposedException(GetType(RtfBuilder).Name)
        End If

        Dim matches = RtfBuilder.ControlWordRegex.Matches(builder.ToString())
        Dim count = matches.Count - 1
        Dim collection(count) As ControlWord
        For i = 0 To count
            Dim current = matches(i)
            collection(i) = New ControlWord(current.Value, current.Length, current.Index)
        Next

        Return New ControlWordCollection(collection)
    End Function

    ''' <summary>
    ''' Saves the specified builder to the specified file path.
    ''' </summary>
    ''' <param name="builder">The <see cref="RtfBuilder">RtfBuilder</see> to save.</param>
    ''' <param name="filePath">The file to save to.</param>
    Public Shared Function SaveFile(ByVal builder As RtfBuilder, ByVal filePath As String) As IO.FileInfo
        Return RtfBuilder.SaveFile(builder, filePath, RtfFormat.Rtf)
    End Function

    ''' <summary>
    ''' Saves the specified builder to the specified file path.
    ''' </summary>
    ''' <param name="builder">The <see cref="RtfBuilder">RtfBuilder</see> to save.</param>
    ''' <param name="filePath">The file to save to.</param>
    ''' <param name="format">One of the <see cref="RtfFormat">RtfFormat</see> values.</param>
    Public Shared Function SaveFile(ByVal builder As RtfBuilder, ByVal filePath As String, ByVal format As RtfFormat) As IO.FileInfo
        If builder Is Nothing Then
            Throw New ArgumentNullException("builder")
        End If
        If String.IsNullOrEmpty(filePath) Then
            Throw New ArgumentNullException("filePath")
        End If
        Try
            IO.File.WriteAllText(filePath, builder.ToString(format))
            Return New IO.FileInfo(filePath)
        Catch ex As IO.IOException
            Return Nothing
        End Try
    End Function

    Private Function EnsureFont(ByVal font As Font) As Integer
        If Me.Header.Fonts.Contains(font) Then
            Return Me.Header.Fonts.IndexOf(font)
        End If
        Return Me.Header.Fonts.Add(font)
    End Function

    Private Function EnsureFont(ByVal fontStyle As FontStyle) As Integer
        Dim defaultFont = Me.Header.Fonts(RtfBuilder.DefaultFontIndex)
        Dim newFont = New Font(defaultFont, fontStyle)
        If Me.Header.Fonts.Contains(newFont) Then
            Dim index = Me.Header.Fonts.IndexOf(newFont)
            newFont.Dispose()
            newFont = Nothing
            Return index
        End If
        Return Me.EnsureFont(newFont)
    End Function

    Private Function EnsureColor(ByVal color As Color) As Integer
        If Me.Header.Colors.Contains(color) Then
            Return Me.Header.Colors.IndexOf(color)
        End If
        Return Me.Header.Colors.Add(color)
    End Function

    Private Function EnsureSafeRtf(ByVal value As String) As String
        Dim newValue = value.Replace("\", "\\")
        newValue = newValue.Replace("}", "\}")
        newValue = newValue.Replace("{", "\{")
        Return newValue
    End Function


    Public Shared Sub RtfSetSelectionLink(richTextBox As RichTextBox)
        Try
            Dim cf As New CHARFORMAT2()
            cf.cbSize = CUInt(Marshal.SizeOf(cf))

            cf.dwMask = CFM_LINK
            cf.dwEffects = CFE_LINK

            Dim wParam As IntPtr = CType(SCF_SELECTION, IntPtr)
            Dim lParam As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
            Marshal.StructureToPtr(cf, lParam, False)

            SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, wParam, lParam)

            Marshal.FreeCoTaskMem(lParam)
        Catch generatedExceptionName As Exception
            Debug.Assert(False)
        End Try
    End Sub


#Region "links"

    Private Const CFM_LINK As UInteger = &H20
    Private Const CFE_LINK As UInteger = &H20
    Private Const SCF_SELECTION As Integer = &H1
    Private Const EM_GETCHARFORMAT As Integer = WM_USER + 58
    Private Const EM_SETCHARFORMAT As Integer = WM_USER + 68
    Private Const WM_USER As Integer = &H400

    <DllImport("User32.dll")> _
    Private Shared Function SendMessage(hWnd As IntPtr, nMsg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Private Structure CHARFORMAT2
        Public cbSize As UInt32
        Public dwMask As UInt32
        Public dwEffects As UInt32
        Public yHeight As Int32
        Public yOffset As Int32
        Public crTextColor As UInt32
        Public bCharSet As [Byte]
        Public bPitchAndFamily As [Byte]

        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> _
        Public szFaceName As String

        Public wWeight As UInt16
        Public sSpacing As UInt16
        Public crBackColor As Int32
        Public lcid As Int32
        Public dwReserved As UInt32
        Public sStyle As Int16
        Public wKerning As Int16
        Public bUnderlineType As [Byte]
        Public bAnimation As [Byte]
        Public bRevAuthor As [Byte]
        Public bReserved1 As [Byte]
    End Structure
#End Region


    'Private Shared Function RtfGetCharFormat(rtb As RichTextBox) As KeyBNativeMethods.CHARFORMAT2
    '    Dim cf As New KeyBNativeMethods.CHARFORMAT2()
    '    cf.cbSize = CUInt(Marshal.SizeOf(cf))

    '    Try
    '        Dim wParam As IntPtr = DirectCast(KeyBNativeMethods.SCF_SELECTION, IntPtr)
    '        Dim lParam As IntPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf))
    '        Marshal.StructureToPtr(cf, lParam, False)

    '        KeyBNativeMethods.SendMessage(rtb.Handle, KeyBNativeMethods.EM_GETCHARFORMAT, wParam, lParam)

    '        cf = DirectCast(Marshal.PtrToStructure(lParam, GetType(KeyBNativeMethods.CHARFORMAT2)), KeyBNativeMethods.CHARFORMAT2)
    '        Marshal.FreeCoTaskMem(lParam)
    '    Catch generatedExceptionName As Exception
    '        Debug.Assert(False)
    '    End Try

    '    Return cf
    'End Function


End Class