


Imports System.ComponentModel
''' <summary>
''' class to show password manager in form not in tab
''' </summary>
Public Class FrmPwdMgr
    ''' <summary>
    ''' only one instance
    ''' </summary>
    Public Shared frmPwdMgrContainer As FrmPwdMgr
    ''' <summary>
    ''' open password mgr
    ''' </summary>
    ''' <param name="tab"></param>
    Public Shared Sub openPwdMger(tab As IndronTabPage)
        If frmPwdMgrContainer Is Nothing Then
            frmPwdMgrContainer = New FrmPwdMgr
            'frmPwdMgrContainer.oldHost = uclibTab.Parent 'not used yet
            Dim xp As Integer = System.Windows.Forms.Cursor.Position.X '+  uclibTab.ParentForm.Left + 
            Dim yp As Integer = System.Windows.Forms.Cursor.Position.Y ' uclibTab.ParentForm.Top + 
            'Debug.WriteLine(String.Format("Xp,YP= {0} / {1}  Form XY={2} / {3} Mouse Location= {4} / {5}", xp, yp, uclibTab.ParentForm.Left, uclibTab.ParentForm.Top, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y))
            FrmPwdMgr.StartPosition = FormStartPosition.Manual 'if not take other location
            FrmPwdMgr.Left = xp
            FrmPwdMgr.Top = yp
        End If
        frmPwdMgrContainer.Text = tab.title
        tab.Parent = frmPwdMgrContainer
        showFrmPwdMgr()
    End Sub

    Private Sub FrmPwdMgr_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        FrmPwdMgr.frmPwdMgrContainer = Nothing
    End Sub

    Public Shared Sub showFrmPwdMgr()
        If frmPwdMgrContainer.WindowState = FormWindowState.Minimized Then
            frmPwdMgrContainer.WindowState = FormWindowState.Normal
        End If
        frmPwdMgrContainer.Show()
        frmPwdMgrContainer.BringToFront()
    End Sub
End Class