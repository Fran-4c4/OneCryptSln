<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IndronTabControl
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
        Me.pnlTab = New System.Windows.Forms.Panel()
        Me.pnlButtonsTop = New System.Windows.Forms.Panel()
        Me.pnlBackColor = New System.Windows.Forms.Panel()
        Me.pnlFlowButtons = New IndronTabButtonContainer()
        Me.pnlButtonsTop.SuspendLayout()
        Me.pnlBackColor.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTab
        '
        Me.pnlTab.BackColor = System.Drawing.Color.White
        Me.pnlTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab.Location = New System.Drawing.Point(0, 3)
        Me.pnlTab.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlTab.Name = "pnlTab"
        Me.pnlTab.Size = New System.Drawing.Size(600, 283)
        Me.pnlTab.TabIndex = 0
        '
        'pnlButtonsTop
        '
        Me.pnlButtonsTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlButtonsTop.BackColor = System.Drawing.Color.Tan
        Me.pnlButtonsTop.Controls.Add(Me.pnlFlowButtons)
        Me.pnlButtonsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButtonsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlButtonsTop.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlButtonsTop.Name = "pnlButtonsTop"
        Me.pnlButtonsTop.Size = New System.Drawing.Size(600, 26)
        Me.pnlButtonsTop.TabIndex = 2
        '
        'pnlBackColor
        '
        Me.pnlBackColor.BackColor = System.Drawing.Color.IndianRed
        Me.pnlBackColor.Controls.Add(Me.pnlTab)
        Me.pnlBackColor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBackColor.Location = New System.Drawing.Point(0, 26)
        Me.pnlBackColor.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlBackColor.Name = "pnlBackColor"
        Me.pnlBackColor.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlBackColor.Size = New System.Drawing.Size(600, 286)
        Me.pnlBackColor.TabIndex = 0
        '
        'pnlFlowButtons
        '
        Me.pnlFlowButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlFlowButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFlowButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlFlowButtons.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFlowButtons.Name = "pnlFlowButtons"
        Me.pnlFlowButtons.Size = New System.Drawing.Size(600, 26)
        Me.pnlFlowButtons.tabControl = Nothing
        Me.pnlFlowButtons.TabIndex = 0
        '
        'IndronTabControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlBackColor)
        Me.Controls.Add(Me.pnlButtonsTop)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "IndronTabControl"
        Me.Size = New System.Drawing.Size(600, 312)
        Me.pnlButtonsTop.ResumeLayout(False)
        Me.pnlBackColor.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTab As System.Windows.Forms.Panel
    Friend WithEvents pnlButtonsTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBackColor As System.Windows.Forms.Panel
    Friend WithEvents pnlFlowButtons As IndronTabButtonContainer

End Class
