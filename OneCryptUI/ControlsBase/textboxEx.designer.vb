<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class textboxEx
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
        Me.TxtBox = New TextBoxEx0()
        Me.lbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TxtBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtBox.BackColor = System.Drawing.Color.White
        Me.TxtBox.Location = New System.Drawing.Point(124, 0)
        Me.TxtBox.Name = "TextBox1"
        Me.TxtBox.Size = New System.Drawing.Size(228, 28)
        Me.TxtBox.TabIndex = 6
        Me.TxtBox.TextBoxText = ""
        '
        'lbl
        '
        Me.lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.Color.DimGray
        Me.lbl.Location = New System.Drawing.Point(0, 3)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(123, 22)
        Me.lbl.TabIndex = 4
        Me.lbl.Text = "Entry name"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'textboxEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.TxtBox)
        Me.Controls.Add(Me.lbl)
        Me.Name = "textboxEx"
        Me.Size = New System.Drawing.Size(352, 28)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents TxtBox As TextBoxEx0

End Class
