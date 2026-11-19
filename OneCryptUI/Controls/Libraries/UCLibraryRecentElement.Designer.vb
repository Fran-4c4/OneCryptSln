<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCLibraryRecentElement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCLibraryRecentElement))
        Me.IndronButton1 = New IndronButton()
        Me.SuspendLayout()
        '
        'IndronButton1
        '
        Me.IndronButton1.BackColor = System.Drawing.Color.Transparent
        Me.IndronButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.IndronButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IndronButton1.defaultBackHighColor = System.Drawing.Color.WhiteSmoke
        Me.IndronButton1.defaultBackLowColor = System.Drawing.Color.SkyBlue
        Me.IndronButton1.defaultBorderColor = System.Drawing.Color.LightGray
        Me.IndronButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndronButton1.ForeColorHover = System.Drawing.Color.Black
        Me.IndronButton1.ForeColorLeave = System.Drawing.Color.DimGray
        Me.IndronButton1.HoverActivatedToViewBehavior = False
        Me.IndronButton1.Icon = CType(resources.GetObject("IndronButton1.Icon"), System.Drawing.Bitmap)
        Me.IndronButton1.Location = New System.Drawing.Point(0, 0)
        Me.IndronButton1.Margin = New System.Windows.Forms.Padding(0)
        Me.IndronButton1.Name = "IndronButton1"
        Me.IndronButton1.Size = New System.Drawing.Size(400, 44)
        Me.IndronButton1.TabIndex = 0
        Me.IndronButton1.Text = "IndronButton1"
        Me.IndronButton1.title = "Title"
        Me.IndronButton1.titleSub = "SubTitle"
        '
        'UCLibraryRecentElement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.IndronButton1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "UCLibraryRecentElement"
        Me.Size = New System.Drawing.Size(400, 44)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndronButton1 As IndronButton

End Class
