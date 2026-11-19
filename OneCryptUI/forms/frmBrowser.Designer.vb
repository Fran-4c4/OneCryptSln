<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrowser
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
        Me.m_statusMain = New System.Windows.Forms.StatusStrip()
        Me.m_lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.m_webBrowser = New System.Windows.Forms.WebBrowser()
        Me.m_statusMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_statusMain
        '
        Me.m_statusMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_lblStatus})
        Me.m_statusMain.Location = New System.Drawing.Point(0, 482)
        Me.m_statusMain.Name = "m_statusMain"
        Me.m_statusMain.Size = New System.Drawing.Size(878, 22)
        Me.m_statusMain.TabIndex = 2
        '
        'm_lblStatus
        '
        Me.m_lblStatus.Name = "m_lblStatus"
        Me.m_lblStatus.Size = New System.Drawing.Size(863, 17)
        Me.m_lblStatus.Spring = True
        Me.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'm_webBrowser
        '
        Me.m_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_webBrowser.Location = New System.Drawing.Point(0, 0)
        Me.m_webBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.m_webBrowser.Name = "m_webBrowser"
        Me.m_webBrowser.ScriptErrorsSuppressed = True
        Me.m_webBrowser.Size = New System.Drawing.Size(878, 482)
        Me.m_webBrowser.TabIndex = 3
        '
        'frmBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(878, 504)
        Me.Controls.Add(Me.m_webBrowser)
        Me.Controls.Add(Me.m_statusMain)
        Me.Name = "frmBrowser"
        Me.Text = "frmBrowser"
        Me.m_statusMain.ResumeLayout(False)
        Me.m_statusMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents m_statusMain As System.Windows.Forms.StatusStrip
    Private WithEvents m_lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents m_webBrowser As System.Windows.Forms.WebBrowser
End Class
