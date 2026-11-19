<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCLibraryCrypt123
    Inherits AppBaseUC

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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppEventsClass1 As ind.encrypter.AppEventsClass = New ind.encrypter.AppEventsClass()
        Me.UcCryptExplorer1 = New UCCryptExplorer()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tssStatusExplorer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssFileName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlCryptExplorerTop = New System.Windows.Forms.Panel()
        Me.lblListViewItemCount = New System.Windows.Forms.Label()
        Me.lblEncryptedLib = New System.Windows.Forms.Label()
        Me.tssFileCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.pnlCryptExplorerTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'UcCryptExplorer1
        '
        Me.UcCryptExplorer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.UcCryptExplorer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcCryptExplorer1.LibraryLoaded = Nothing
        Me.UcCryptExplorer1.Location = New System.Drawing.Point(0, 34)
        Me.UcCryptExplorer1.myAppEvents = AppEventsClass1
        Me.UcCryptExplorer1.Name = "UcCryptExplorer1"
        Me.UcCryptExplorer1.Size = New System.Drawing.Size(732, 287)
        Me.UcCryptExplorer1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssStatusExplorer, Me.tssFileName, Me.tssFileCount})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 321)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.ShowItemToolTips = True
        Me.StatusStrip1.Size = New System.Drawing.Size(732, 25)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 29
        '
        'tssStatusExplorer
        '
        Me.tssStatusExplorer.AutoSize = False
        Me.tssStatusExplorer.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssStatusExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssStatusExplorer.Name = "tssStatusExplorer"
        Me.tssStatusExplorer.Size = New System.Drawing.Size(220, 20)
        Me.tssStatusExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssFileName
        '
        Me.tssFileName.AutoToolTip = True
        Me.tssFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssFileName.Name = "tssFileName"
        Me.tssFileName.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.tssFileName.Size = New System.Drawing.Size(0, 0)
        Me.tssFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCryptExplorerTop
        '
        Me.pnlCryptExplorerTop.BackColor = System.Drawing.Color.IndianRed
        Me.pnlCryptExplorerTop.Controls.Add(Me.lblListViewItemCount)
        Me.pnlCryptExplorerTop.Controls.Add(Me.lblEncryptedLib)
        Me.pnlCryptExplorerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCryptExplorerTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlCryptExplorerTop.Name = "pnlCryptExplorerTop"
        Me.pnlCryptExplorerTop.Size = New System.Drawing.Size(732, 34)
        Me.pnlCryptExplorerTop.TabIndex = 30
        '
        'lblListViewItemCount
        '
        Me.lblListViewItemCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListViewItemCount.BackColor = System.Drawing.Color.IndianRed
        Me.lblListViewItemCount.ForeColor = System.Drawing.Color.White
        Me.lblListViewItemCount.Location = New System.Drawing.Point(590, 2)
        Me.lblListViewItemCount.Name = "lblListViewItemCount"
        Me.lblListViewItemCount.Size = New System.Drawing.Size(140, 18)
        Me.lblListViewItemCount.TabIndex = 6
        Me.lblListViewItemCount.Text = "{0} Elementos añadidos"
        Me.lblListViewItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEncryptedLib
        '
        Me.lblEncryptedLib.BackColor = System.Drawing.Color.IndianRed
        Me.lblEncryptedLib.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblEncryptedLib.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEncryptedLib.ForeColor = System.Drawing.Color.White
        Me.lblEncryptedLib.Location = New System.Drawing.Point(0, 0)
        Me.lblEncryptedLib.Name = "lblEncryptedLib"
        Me.lblEncryptedLib.Size = New System.Drawing.Size(732, 34)
        Me.lblEncryptedLib.TabIndex = 4
        Me.lblEncryptedLib.Text = "libreria"
        Me.lblEncryptedLib.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssFileCount
        '
        Me.tssFileCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tssFileCount.Name = "tssFileCount"
        Me.tssFileCount.Size = New System.Drawing.Size(121, 15)
        Me.tssFileCount.Text = "ToolStripStatusLabel1"
        '
        'UCLibraryCrypt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.UcCryptExplorer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnlCryptExplorerTop)
        Me.Name = "UCLibraryCrypt"
        Me.Size = New System.Drawing.Size(732, 346)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnlCryptExplorerTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UcCryptExplorer1 As UCCryptExplorer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tssStatusExplorer As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pnlCryptExplorerTop As System.Windows.Forms.Panel
    Friend WithEvents lblListViewItemCount As System.Windows.Forms.Label
    Friend WithEvents lblEncryptedLib As System.Windows.Forms.Label
    Friend WithEvents tssFileCount As System.Windows.Forms.ToolStripStatusLabel

End Class
