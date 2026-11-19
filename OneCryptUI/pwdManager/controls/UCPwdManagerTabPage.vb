



Public Class UCPwdManagerTabPage
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Me.DesignMode = False Then
            Me.initConfigBase(ind.lang.UCpwdMngLang.pwdManagerTabButton, ind.lang.UILang.tabbutton_TT)
            Me.btnTab.SelectedBackHighColor = Color.Orange
            Me.btnTab.SelectedBackLowColor = Color.OrangeRed
            Me.btnTab.Icon = My.Resources.pwdmgr_tab
        End If

    End Sub

    Private Sub UcPwdManagerMain1_Load(sender As Object, e As EventArgs) Handles UcPwdManagerMain1.Load

    End Sub
End Class
