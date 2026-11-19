


Public Class IndTabInfo
    Public keyIndex As Integer
    'Public keyUnique As String

    Public WithEvents tab As IndronTabPage

    Public Function GetUcLibraryTabpageHasChanges() As Boolean

        If Me.tab.GetType Is GetType(UcLibraryTabpage) Then
            Dim EntTab As UcLibraryTabpage = DirectCast(Me.tab, UcLibraryTabpage)
            Return EntTab.ucLib.checkChanges = True
        Else
            Return False
        End If


    End Function
End Class