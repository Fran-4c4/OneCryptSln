<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordStrengthControl
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
        Me.lbText = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lbText
        '
        Me.lbText.BackColor = System.Drawing.Color.Transparent
        Me.lbText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbText.Location = New System.Drawing.Point(0, 0)
        Me.lbText.Margin = New System.Windows.Forms.Padding(0)
        Me.lbText.Name = "lbText"
        Me.lbText.Size = New System.Drawing.Size(192, 16)
        Me.lbText.TabIndex = 1
        Me.lbText.Text = "Calidad"
        Me.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PasswordStrengthControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lbText)
        Me.Name = "PasswordStrengthControl"
        Me.Size = New System.Drawing.Size(192, 16)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lbText As System.Windows.Forms.Label

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
End Class
