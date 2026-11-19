<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCLibraryRecentsTabPage
    Inherits IndronTabPage

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Me.components = New System.ComponentModel.Container()
        Dim AppEventsClass1 As ind.encrypter.AppEventsClass = New ind.encrypter.AppEventsClass()
        Me.UcLibraryRecents1 = New UcLibraryRecents()
        Me.SuspendLayout()
        '
        'UcLibraryRecents1
        '
        Me.UcLibraryRecents1.BackColor = System.Drawing.Color.White
        Me.UcLibraryRecents1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcLibraryRecents1.Location = New System.Drawing.Point(0, 0)
        Me.UcLibraryRecents1.myAppEvents = AppEventsClass1
        Me.UcLibraryRecents1.Name = "UcLibraryRecents1"
        Me.UcLibraryRecents1.Size = New System.Drawing.Size(678, 284)
        Me.UcLibraryRecents1.TabIndex = 0
        Me.UcLibraryRecents1.ucExplorer1 = Nothing
        '
        'UCLibraryRecentsTabPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.UcLibraryRecents1)
        Me.Name = "UCLibraryRecentsTabPage"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UcLibraryRecents1 As UcLibraryRecents
End Class
