<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPwdManagerMain
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.UCpwdListView1 = New UCpwdListView()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.UCpwdListView1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(812, 378)
        Me.pnlMain.TabIndex = 0
        '
        'UCpwdListView1
        '
        Me.UCpwdListView1.BackColor = System.Drawing.Color.White
        Me.UCpwdListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCpwdListView1.Location = New System.Drawing.Point(0, 0)
        Me.UCpwdListView1.Name = "UCpwdListView1"
        Me.UCpwdListView1.Size = New System.Drawing.Size(812, 378)
        Me.UCpwdListView1.TabIndex = 0
        '
        'UCPwdManagerMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "UCPwdManagerMain"
        Me.Size = New System.Drawing.Size(812, 378)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents UCpwdListView1 As UCpwdListView

End Class
