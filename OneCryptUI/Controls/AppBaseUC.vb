Public Class AppBaseUC


    Private WithEvents mainForm_var As frmMain
    ''' <summary>
    ''' access to application events
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents myAppEvents As New ind.encrypter.AppEventsClass

    Protected controlIsInitialized As Boolean = False


    Friend Overridable Sub MainForm_Closing() Handles mainForm_var.FormClosing

    End Sub

    Private Sub AppBaseUC_ParentChanged(sender As Object, e As EventArgs) Handles Me.ParentChanged
        If Not Me.ParentForm Is Nothing Then
            If TypeOf (Me.ParentForm) Is frmMain Then
                mainForm_var = DirectCast(Me.ParentForm, frmMain)
            End If
        End If
    End Sub


    Public ReadOnly Property mainForm As frmMain
        Get
            If Me.DesignMode = True Then
                Return Nothing
            End If
            If Not Me.ParentForm Is Nothing Then
                Return ind.crypt.OCGlobal.mainForm
            End If
            Return Nothing
        End Get
    End Property
    ''' <summary>
    ''' Application password for encrypted files
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AppPassword As String
        Get
            Return ind.cte.AppVars.AppPassword
        End Get
    End Property



End Class
