Public Class UCLeftMenu
    ''' <summary>
    ''' event raised when option is clicked in left panel toolbox
    ''' </summary>
    ''' <param name="open"></param>
    ''' <param name="sender"></param>
    Public Event optionClicked(open As Boolean)

    Public controlList As New Hashtable
    Dim topActual As Integer = 0

    Dim lastOptionClicked As String = ""

    Friend Class TheControl
        ''' <summary>
        ''' title to show on top control
        ''' </summary>
        Public TitleTop As String
        Public theControl As Object
        Public theMethod As Func(Of Object)

        Public Sub New(TitleTop As String, theMethod As Func(Of Object))
            Me.TitleTop = TitleTop
            Me.theMethod = theMethod
        End Sub

        Public Function getControl() As Control
            If theControl Is Nothing Then
                theControl = theMethod()
            End If
            Return CType(theControl, Control)
        End Function
    End Class

    Private Sub UCLeftMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolStrip1.Renderer = New LeftMenuToolStripRender

    End Sub


    Friend Function addOption(key As String, labelTit As String, theControl As TheControl) As Object

        controlList.Add(key, theControl)
        'Dim ctrl As Control
        ' ctrl = configureLabelWithStandarBehavior(labelTit, key, topActual + 2)

        'configureButtonWithStandarBehavior(labelTit, key)

        Return Nothing
    End Function

#Region "Vertical label"
    Private Sub LabelVertical_Click(sender As Object, e As EventArgs)
        Dim verticalLabel As LabelVertical = CType(sender, LabelVertical)
        resetVerticalLabelSelection()
        verticalLabel.isSelected = True
        verticalLabel.BackColor = Color.SteelBlue
        Dim key As String = CType(verticalLabel.Tag, String)
        If key = lastOptionClicked Then
            lastOptionClicked = ""
            RaiseEvent optionClicked(False) 'close menu, don´t clean anything
            Exit Sub
        End If
        showOption(key)
    End Sub

    Private Sub resetVerticalLabelSelection()
        For Each ctrl As Control In Me.pnlLeft.Controls
            If ctrl.GetType Is GetType(LabelVertical) Then
                Dim verticalLabel As LabelVertical = CType(ctrl, LabelVertical)
                If verticalLabel.isSelected = True Then
                    verticalLabel.isSelected = False
                    ctrl.BackColor = Color.Silver
                    Exit Sub
                End If
            End If

        Next

    End Sub
    Private Function configureLabelWithStandarBehavior(text As String, tag As String, top As Integer) As Control
        Dim ctrl As New LabelVertical
        ctrl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ctrl.Padding = New System.Windows.Forms.Padding(2)
        'verticalLabel.TabIndex = 2
        ctrl.upToDown = True
        ctrl.caption = text
        ctrl.Tag = tag
        ctrl.Top = top
        ctrl.Visible = True

        Me.pnlLeft.Controls.Add(ctrl)
        topActual += 2 + ctrl.Height
        AddHandler ctrl.Click, AddressOf LabelVertical_Click
        Return ctrl
    End Function

#End Region

#Region "toolbar"
    Private Sub toolbar_Click(sender As Object, e As EventArgs)
        Dim btn As System.Windows.Forms.ToolStripButton = CType(sender, System.Windows.Forms.ToolStripButton)
        For Each btndis As System.Windows.Forms.ToolStripItem In Me.ToolStrip1.Items
            If btndis.GetType Is GetType(ToolStripButton) Then
                Dim b As ToolStripButton = CType(btndis, ToolStripButton)
                b.Checked = False
            End If
        Next
        btn.Checked = True

        Dim key As String = CType(btn.Tag, String)
        If key = lastOptionClicked Then
            lastOptionClicked = ""
            RaiseEvent optionClicked(False) 'close menu, don´t clean anything
            Exit Sub
        End If
        showOption(key)
    End Sub

    Private Function configureButtonWithStandarBehavior(text As String, tag As String) As System.Windows.Forms.ToolStripButton
        Dim ctrl = New System.Windows.Forms.ToolStripButton
        'ToolStripButton1.Image = CType(Resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        ctrl.Image = Global.My.Resources.Resources.project_menu
        ctrl.ImageTransparentColor = System.Drawing.Color.Magenta
        'ToolStripButton1.Name = name
        ctrl.Size = New System.Drawing.Size(29, 117)
        ctrl.Text = text
        ctrl.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270
        ctrl.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        ctrl.Tag = tag
        Me.ToolStrip1.Items.Add(ctrl)
        AddHandler ctrl.Click, AddressOf toolbar_Click
        Return ctrl
    End Function

#End Region



    Public Sub showOption(key As String)
        Try
            If lastOptionClicked <> key Then
                pnlRight.Controls.Clear()
                Dim thectrl As TheControl = CType(controlList(key), TheControl)
                lblTitleTop.Text = thectrl.TitleTop
                Dim ctrl As Control = thectrl.getControl
                ctrl.Parent = pnlRight
                ctrl.Dock = DockStyle.Fill
            End If

            RaiseEvent optionClicked(True)
            lastOptionClicked = key
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        lastOptionClicked = ""
        RaiseEvent optionClicked(False)
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub btnExplorer_Click(sender As Object, e As EventArgs) Handles btnExplorer.Click
        toolbar_Click(sender, e)
    End Sub

    Private Sub btnLibraries_Click(sender As Object, e As EventArgs) Handles btnLibraries.Click
        toolbar_Click(sender, e)
    End Sub




    '    {
    '    Protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
    '        var btn = e.Item As ToolStripButton;
    '        If (btn! = null && btn.CheckOnClick && btn.Checked) {
    '            Rectangle bounds = New Rectangle(Point.Empty, e.Item.Size);
    '            e.Graphics.FillRectangle(Brushes.Black, bounds);
    '        }
    '        Else base.OnRenderButtonBackground(e);
    '    }
    '}

End Class
