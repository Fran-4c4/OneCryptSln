<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IndronTabButtonContainer
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
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pnlButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlButtons.Location = New System.Drawing.Point(0, 0)
        Me.pnlButtons.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(62, 26)
        Me.pnlButtons.TabIndex = 1
        '
        'btnLeft
        '
        Me.btnLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnLeft.Image = Global.My.Resources.Resources.left
        Me.btnLeft.Location = New System.Drawing.Point(562, 0)
        Me.btnLeft.Margin = New System.Windows.Forms.Padding(0)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(20, 20)
        Me.btnLeft.TabIndex = 2
        Me.btnLeft.UseVisualStyleBackColor = True
        Me.btnLeft.Visible = False
        '
        'btnRight
        '
        Me.btnRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRight.Image = Global.My.Resources.Resources.right
        Me.btnRight.Location = New System.Drawing.Point(581, 0)
        Me.btnRight.Margin = New System.Windows.Forms.Padding(0)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(20, 20)
        Me.btnRight.TabIndex = 2
        Me.btnRight.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnRight.UseVisualStyleBackColor = True
        Me.btnRight.Visible = False
        '
        'IndronTabButtonContainer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnRight)
        Me.Controls.Add(Me.btnLeft)
        Me.Controls.Add(Me.pnlButtons)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "IndronTabButtonContainer"
        Me.Size = New System.Drawing.Size(600, 26)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents btnLeft As System.Windows.Forms.Button

End Class
