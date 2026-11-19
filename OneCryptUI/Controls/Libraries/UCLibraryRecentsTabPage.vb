

''' <summary>
''' Menu recents libraries tabpage
''' </summary>
''' <remarks></remarks>
Public Class UCLibraryRecentsTabPage


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Me.DesignMode = False Then
            Me.initConfigBase(ind.lang.UCLibraryLang.lbl_Title, "")

        End If

    End Sub

End Class
