<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLockScreenPwd
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppEventsClass1 As ind.encrypter.AppEventsClass = New ind.encrypter.AppEventsClass()
        Me.UcPwdController1 = New UCPwdController()
        Me.SuspendLayout()
        '
        'UcPwdController1
        '
        Me.UcPwdController1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcPwdController1.Location = New System.Drawing.Point(0, 0)
        Me.UcPwdController1.myAppEvents = AppEventsClass1
        Me.UcPwdController1.Name = "UcPwdController1"
        Me.UcPwdController1.Size = New System.Drawing.Size(728, 258)
        Me.UcPwdController1.TabIndex = 1
        '
        'FrmLockScreenPwd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 258)
        Me.Controls.Add(Me.UcPwdController1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmLockScreenPwd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcPwdController1 As UCPwdController
End Class
