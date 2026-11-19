<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UcNewLibrary
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
        Me.btnOpenLib = New System.Windows.Forms.Button()
        Me.lbl_Title = New System.Windows.Forms.Label()
        Me.txtLibName = New System.Windows.Forms.TextBox()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.lblLibName = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpenLib
        '
        Me.btnOpenLib.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOpenLib.FlatAppearance.BorderSize = 0
        Me.btnOpenLib.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.btnOpenLib.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenLib.Image = Global.My.Resources.Resources.project_add
        Me.btnOpenLib.Location = New System.Drawing.Point(10, 10)
        Me.btnOpenLib.Name = "btnOpenLib"
        Me.btnOpenLib.Size = New System.Drawing.Size(39, 42)
        Me.btnOpenLib.TabIndex = 36
        Me.btnOpenLib.UseVisualStyleBackColor = True
        '
        'lbl_Title
        '
        Me.lbl_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Title.ForeColor = System.Drawing.Color.DimGray
        Me.lbl_Title.Location = New System.Drawing.Point(67, 14)
        Me.lbl_Title.Name = "lbl_Title"
        Me.lbl_Title.Size = New System.Drawing.Size(358, 29)
        Me.lbl_Title.TabIndex = 34
        Me.lbl_Title.Text = "Nueva libreria"
        Me.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLibName
        '
        Me.txtLibName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLibName.Location = New System.Drawing.Point(74, 64)
        Me.txtLibName.MaxLength = 500
        Me.txtLibName.Name = "txtLibName"
        Me.txtLibName.Size = New System.Drawing.Size(426, 31)
        Me.txtLibName.TabIndex = 0
        '
        'btnContinue
        '
        Me.btnContinue.BackColor = System.Drawing.Color.LimeGreen
        Me.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnContinue.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.ForeColor = System.Drawing.Color.White
        Me.btnContinue.Image = Global.My.Resources.Resources.Security_Approved
        Me.btnContinue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnContinue.Location = New System.Drawing.Point(321, 113)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(179, 50)
        Me.btnContinue.TabIndex = 38
        Me.btnContinue.Text = "&Guardar"
        Me.btnContinue.UseVisualStyleBackColor = False
        '
        'lblLibName
        '
        Me.lblLibName.ForeColor = System.Drawing.Color.DimGray
        Me.lblLibName.Location = New System.Drawing.Point(71, 48)
        Me.lblLibName.Name = "lblLibName"
        Me.lblLibName.Size = New System.Drawing.Size(262, 12)
        Me.lblLibName.TabIndex = 39
        Me.lblLibName.Text = "Introduce el nombre de la nueva libreria"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lbl_Title)
        Me.Panel1.Controls.Add(Me.txtLibName)
        Me.Panel1.Controls.Add(Me.btnOpenLib)
        Me.Panel1.Controls.Add(Me.btnContinue)
        Me.Panel1.Controls.Add(Me.lblLibName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(518, 182)
        Me.Panel1.TabIndex = 40
        '
        'UcNewLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkGray
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UcNewLibrary"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(524, 188)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpenLib As System.Windows.Forms.Button
    Friend WithEvents lbl_Title As System.Windows.Forms.Label
    Friend WithEvents txtLibName As System.Windows.Forms.TextBox
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents lblLibName As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
