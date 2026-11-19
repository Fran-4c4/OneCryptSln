Imports System.Drawing

''' <summary>
''' Represents the color table control group ( \colortbl ) in RTF. This class cannot be inherited.
''' </summary>
<DebuggerNonUserCode()> _
<DebuggerStepThrough()> _
Public NotInheritable Class ColorTable
    Inherits TableBase(Of Color)

    '//constructors
    Friend Sub New(ByVal header As RtfHeader)
        MyBase.New(header)
    End Sub

End Class