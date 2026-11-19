<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCTreeExplorer
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
        Me.lvTreeExp = New System.Windows.Forms.ListView()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDateModified = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDateCreated = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mniAddToProject = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvTreeExp
        '
        Me.lvTreeExp.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvTreeExp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chType, Me.chSize, Me.chPath, Me.chDateModified, Me.chDateCreated})
        Me.lvTreeExp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTreeExp.LabelEdit = True
        Me.lvTreeExp.Location = New System.Drawing.Point(0, 0)
        Me.lvTreeExp.Name = "lvTreeExp"
        Me.lvTreeExp.Size = New System.Drawing.Size(700, 318)
        Me.lvTreeExp.TabIndex = 3
        Me.lvTreeExp.UseCompatibleStateImageBehavior = False
        Me.lvTreeExp.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "Nombre"
        Me.chName.Width = 150
        '
        'chType
        '
        Me.chType.Text = "Type"
        Me.chType.Width = 64
        '
        'chSize
        '
        Me.chSize.Text = "Size"
        Me.chSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.chSize.Width = 40
        '
        'chPath
        '
        Me.chPath.Text = "Ruta del fichero"
        Me.chPath.Width = 250
        '
        'chDateModified
        '
        Me.chDateModified.Text = "Ultima modificación"
        '
        'chDateCreated
        '
        Me.chDateCreated.Text = "Fecha creación"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniAddToProject})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(172, 26)
        '
        'mniAddToProject
        '
        Me.mniAddToProject.Name = "mniAddToProject"
        Me.mniAddToProject.Size = New System.Drawing.Size(171, 22)
        Me.mniAddToProject.Text = "Añadir a la libreria"
        '
        'UCTreeExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.lvTreeExp)
        Me.Name = "UCTreeExplorer"
        Me.Size = New System.Drawing.Size(700, 318)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvTreeExp As System.Windows.Forms.ListView
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDateModified As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDateCreated As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mniAddToProject As System.Windows.Forms.ToolStripMenuItem

End Class
