Imports System.Runtime.InteropServices

Imports SharpShell.Attributes

Namespace onecrypt

    ''' <summary>
    ''' Class to show the icon asociated to ocpt files
    ''' </summary>

    <ComVisible(True)>
    <COMServerAssociation(AssociationType.FileExtension, ".ocpt")>
    Public Class OneCryptIconHandler
        Inherits SharpShell.SharpIconHandler.SharpIconHandler


        Protected Overrides Function GetIcon(smallIcon As Boolean, iconSize As UInteger) As Drawing.Icon
            'The icon we'll return.
            Dim icon As Drawing.Icon = Nothing
            icon = My.Resources.lock_server
            '  Return the icon with the correct size. Use the SharpIconHandler 'GetIconSpecificSize'
            '  function to extract the icon of the required size.
            Return GetIconSpecificSize(icon, New System.Drawing.Size(CInt(iconSize), CInt(iconSize)))
        End Function


    End Class
End Namespace