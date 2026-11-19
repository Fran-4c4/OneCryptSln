<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCPwdManagerTabPage
    Inherits IndronTabPage

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.UcPwdManagerMain1 = New UCPwdManagerMain()
        Me.SuspendLayout()
        '
        'UcPwdManagerMain1
        '
        Me.UcPwdManagerMain1.BackColor = System.Drawing.Color.White
        Me.UcPwdManagerMain1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcPwdManagerMain1.Location = New System.Drawing.Point(0, 0)
        Me.UcPwdManagerMain1.Name = "UcPwdManagerMain1"
        Me.UcPwdManagerMain1.Size = New System.Drawing.Size(678, 388)
        Me.UcPwdManagerMain1.TabIndex = 0
        '
        'UCPwdManagerTabPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.UcPwdManagerMain1)
        Me.Name = "UCPwdManagerTabPage"
        Me.Size = New System.Drawing.Size(678, 388)
        Me.ResumeLayout(False)

    End Sub


    Friend WithEvents UcPwdManagerMain1 As UCPwdManagerMain

    Private Sub btnTab_buttonDoubleClickEvent(tab As IndronTabPage) Handles btnTab.buttonDoubleClickEvent
        FrmPwdMgr.openPwdMger(tab)
        Me.tabCtrolContainer.TabPageRemoveBase(tab)
        tab.Visible = True
        Me.TabButtonRemove()
    End Sub


    Private Sub btnTab_buttonMoveOutEvent(tab As IndronTabPage) Handles btnTab.buttonMoveOutEvent
        FrmPwdMgr.openPwdMger(tab)
        Me.tabCtrolContainer.TabPageRemoveBase(tab)
        tab.Visible = True
        Me.TabButtonRemove()
    End Sub


End Class
