<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErrorVisor
    Inherits FrmAppEvents

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErrorVisor))
        Me.lvTreeExp = New System.Windows.Forms.ListView()
        Me.chErrNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chErrType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chErrorMessage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.imgMessage = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblInfoClickToGetMessage = New System.Windows.Forms.Label()
        Me.lblFilename = New System.Windows.Forms.Label()
        Me.lblTit_filename = New System.Windows.Forms.Label()
        Me.pnlErrorMessage = New System.Windows.Forms.Panel()
        Me.txtErrorMessage = New System.Windows.Forms.RichTextBox()
        Me.lbl_Title = New System.Windows.Forms.Label()
        CType(Me.imgMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlErrorMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvTreeExp
        '
        Me.lvTreeExp.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvTreeExp.AllowDrop = True
        Me.lvTreeExp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvTreeExp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chErrNumber, Me.chErrType, Me.chName, Me.chPath, Me.chType, Me.chErrorMessage})
        Me.lvTreeExp.FullRowSelect = True
        Me.lvTreeExp.Location = New System.Drawing.Point(12, 101)
        Me.lvTreeExp.MultiSelect = False
        Me.lvTreeExp.Name = "lvTreeExp"
        Me.lvTreeExp.ShowItemToolTips = True
        Me.lvTreeExp.Size = New System.Drawing.Size(886, 191)
        Me.lvTreeExp.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTreeExp.TabIndex = 5
        Me.lvTreeExp.UseCompatibleStateImageBehavior = False
        Me.lvTreeExp.View = System.Windows.Forms.View.Details
        '
        'chErrNumber
        '
        Me.chErrNumber.Text = "Number"
        Me.chErrNumber.Width = 30
        '
        'chErrType
        '
        Me.chErrType.Text = "Tipo Error"
        Me.chErrType.Width = 150
        '
        'chName
        '
        Me.chName.Text = "Nombre"
        Me.chName.Width = 200
        '
        'chPath
        '
        Me.chPath.Text = "Ruta del fichero"
        Me.chPath.Width = 300
        '
        'chType
        '
        Me.chType.Text = "Tipo"
        Me.chType.Width = 65
        '
        'chErrorMessage
        '
        Me.chErrorMessage.Text = "Mensaje de error"
        Me.chErrorMessage.Width = 400
        '
        'imgMessage
        '
        Me.imgMessage.Image = Global.My.Resources.Resources.document_warning
        Me.imgMessage.Location = New System.Drawing.Point(12, 30)
        Me.imgMessage.Name = "imgMessage"
        Me.imgMessage.Size = New System.Drawing.Size(64, 64)
        Me.imgMessage.TabIndex = 7
        Me.imgMessage.TabStop = False
        '
        'lblInfoClickToGetMessage
        '
        Me.lblInfoClickToGetMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfoClickToGetMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoClickToGetMessage.Location = New System.Drawing.Point(664, 26)
        Me.lblInfoClickToGetMessage.Name = "lblInfoClickToGetMessage"
        Me.lblInfoClickToGetMessage.Size = New System.Drawing.Size(232, 16)
        Me.lblInfoClickToGetMessage.TabIndex = 6
        Me.lblInfoClickToGetMessage.Text = "Pulsa sobre el error para visualizarlo"
        Me.lblInfoClickToGetMessage.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFilename
        '
        Me.lblFilename.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilename.Location = New System.Drawing.Point(87, 8)
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(428, 20)
        Me.lblFilename.TabIndex = 6
        Me.lblFilename.Text = "filename"
        '
        'lblTit_filename
        '
        Me.lblTit_filename.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTit_filename.Location = New System.Drawing.Point(5, 8)
        Me.lblTit_filename.Name = "lblTit_filename"
        Me.lblTit_filename.Size = New System.Drawing.Size(72, 20)
        Me.lblTit_filename.TabIndex = 6
        Me.lblTit_filename.Text = "Archivo"
        '
        'pnlErrorMessage
        '
        Me.pnlErrorMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlErrorMessage.Controls.Add(Me.txtErrorMessage)
        Me.pnlErrorMessage.Controls.Add(Me.lblTit_filename)
        Me.pnlErrorMessage.Controls.Add(Me.lblFilename)
        Me.pnlErrorMessage.Location = New System.Drawing.Point(82, 39)
        Me.pnlErrorMessage.Name = "pnlErrorMessage"
        Me.pnlErrorMessage.Size = New System.Drawing.Size(816, 56)
        Me.pnlErrorMessage.TabIndex = 8
        Me.pnlErrorMessage.Visible = False
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtErrorMessage.Location = New System.Drawing.Point(6, 23)
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.Size = New System.Drawing.Size(810, 32)
        Me.txtErrorMessage.TabIndex = 7
        Me.txtErrorMessage.Text = ""
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(12, -2)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(708, 29)
        Me.lbl_Title.TabIndex = 16
        Me.lbl_Title.Text = "Se han producido algunos errores con los archivos siguientes."
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmErrorVisor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(900, 304)
        Me.Controls.Add(Me.lbl_Title)
        Me.Controls.Add(Me.pnlErrorMessage)
        Me.Controls.Add(Me.imgMessage)
        Me.Controls.Add(Me.lblInfoClickToGetMessage)
        Me.Controls.Add(Me.lvTreeExp)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmErrorVisor"
        Me.Text = "Visor de errores"
        CType(Me.imgMessage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlErrorMessage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvTreeExp As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents imgMessage As System.Windows.Forms.PictureBox
    Friend WithEvents chErrNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents chErrType As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chErrorMessage As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblInfoClickToGetMessage As System.Windows.Forms.Label
    Friend WithEvents lblFilename As System.Windows.Forms.Label
    Friend WithEvents lblTit_filename As System.Windows.Forms.Label
    Friend WithEvents pnlErrorMessage As System.Windows.Forms.Panel
    Friend WithEvents txtErrorMessage As System.Windows.Forms.RichTextBox
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
End Class
