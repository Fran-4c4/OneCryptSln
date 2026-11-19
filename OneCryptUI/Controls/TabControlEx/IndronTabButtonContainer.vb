
''' <summary>
''' class to control buttons for tab
''' </summary>
Public Class IndronTabButtonContainer

    ''' <summary>
    ''' main control
    ''' </summary>
    ''' <remarks></remarks>
    Public WithEvents tabControl As IndronTabControl
    ''' <summary>
    ''' Scroll for buttons
    ''' </summary>
    Dim scb As New scrollClass
    Private Class scrollClass
        Public Value As Integer = 200
        Public Maximum As Integer
    End Class
    ''' <summary>
    ''' Return the maximum value for the right position of the panelbuttons when arrows are visible
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property pnlButtonsRigthMax As Integer
        Get
            Return Me.btnLeft.Left
        End Get
    End Property

    Private firstLoad As Boolean = True


    Private Sub IndronTabButtonContainer_Load(sender As Object, e As EventArgs) Handles Me.Load
        If firstLoad = True Then
            Me.pnlButtons.Width = 0
            Me.pnlButtons.Left = 0
            Me.pnlButtons.Dock = DockStyle.None
            firstLoad = False
            Debug.WriteLine("IndronTabButtonContainer_Load first")
        Else
            Debug.WriteLine("IndronTabButtonContainer_Load second")
        End If
    End Sub

    ''' <summary>
    ''' Add a button to collection
    ''' </summary>
    ''' <param name="btn2Add"></param>
    ''' <remarks></remarks>
    Public Sub buttonAdd(btn2Add As IndronTabButton1)
        Try
            If Not btn2Add.Parent Is Nothing Then
                Exit Sub
            End If
            Me.SuspendLayout()
            Dim widthToAdd As Integer = btn2Add.Width
            Dim newposition As Integer = moveButtonsToRight(btn2Add)
            btn2Add.Parent = Me.pnlButtons
            btn2Add.Left = newposition
            btn2Add.Top = 0
            btn2Add.Visible = True

            Me.pnlButtons.Width += widthToAdd
            'pnlButtonsReposition()
            Me.configScroll()

        Catch ex As Exception
            Throw ex
        Finally
            Me.ResumeLayout()
        End Try

    End Sub

    ''' <summary>
    ''' move all buttons after button passed width. We suposse that the button is inserted after fixed buttons.
    ''' </summary>
    ''' <param name="btn2Add"></param>
    ''' <returns>return the position where is going to be inserted</returns>
    Private Function moveButtonsToRight(btn2Add As IndronTabButton1) As Integer
        Dim widthToAdd As Integer = btn2Add.Width
        Dim newPos As Integer = 0

        For Each ctrlHosted As Control In Me.pnlButtons.Controls
            If ctrlHosted.GetType Is GetType(IndronTabButton1) Then
                Dim btnMove As IndronTabButton1 = DirectCast(ctrlHosted, IndronTabButton1)
                If btnMove.tab.tabType <> IndronTabPage.tabTypeEnum.fixed Then
                    btnMove.Left = btnMove.Left + widthToAdd
                Else
                    'return last position where new button will be positionated
                    newPos = btnMove.Left + btnMove.Width
                End If
            End If
        Next
        Return newPos
    End Function

    ''' <summary>
    ''' remove a button
    ''' </summary>
    ''' <param name="btn2Remove"></param>
    ''' <remarks></remarks>
    Public Sub buttonRemove(btn2Remove As IndronTabButton1)
        Try
            Me.SuspendLayout()
            Dim widthToSubstract As Integer = btn2Remove.Width
            Dim bLeft As Integer = btn2Remove.Left
            Me.pnlButtons.Controls.Remove(btn2Remove)

            For Each ctrlHosted As Control In Me.pnlButtons.Controls
                If ctrlHosted.GetType Is GetType(IndronTabButton1) Then
                    Dim btnMove As IndronTabButton1 = DirectCast(ctrlHosted, IndronTabButton1)
                    If btnMove.Left > bLeft Then
                        btnMove.Left = btnMove.Left - widthToSubstract
                    End If
                End If
            Next

            Me.pnlButtons.Width = Me.pnlButtons.Width - widthToSubstract  'panel has new width 
            'check new position for panel
            'Dim pnlBtnVisibleWidth As Integer = Me.pnlButtons.Left + Me.pnlButtons.Width 'left-Width equals to visible width of the panel
            'If (pnlBtnVisibleWidth) <= Me.pnlButtonsRigthMax Then
            '    Me.pnlButtons.Left += widthToSubstract
            '    If Me.pnlButtons.Left > 0 Then
            '        Me.pnlButtons.Left = 0
            '    End If
            'End If
            pnlButtonsReposition()
            Me.configScroll()

        Catch ex As Exception
            Throw ex
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub pnlButtonsReposition()
        Dim pnlBtnVisibleWidthRigth As Integer = Me.pnlButtons.Left + Me.pnlButtons.Width 'left-Width equals to visible width of the panel same as X-right side
        Dim distanceToMove As Integer = Me.pnlButtonsRigthMax - pnlBtnVisibleWidthRigth
        If distanceToMove > 0 Then
            'if positive then there is a gap between right side and the arrow button, else there is negative and
            If (Me.pnlButtons.Left + distanceToMove) > 0 Then
                Me.pnlButtons.Left = 0
            Else
                Me.pnlButtons.Left += distanceToMove
            End If

        End If
    End Sub


    Private Sub pnlButtons_SizeChanged(sender As Object, e As EventArgs) Handles pnlButtons.SizeChanged
        configScroll()
    End Sub

    Private Sub configScroll()
        If Me.pnlButtons.Width > Me.Width Then
            Me.scb.Maximum = Me.pnlButtons.Width + 42
            Me.btnLeft.Visible = True
            Me.btnRight.Visible = True
        Else
            Me.btnLeft.Visible = False
            Me.btnRight.Visible = False
        End If
    End Sub

    Private Sub IndronTabButtonContainer_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Debug.WriteLine(" Me.SizeChanged Me.pnlButtons.Width:" & Me.pnlButtons.Width & "  Me.Width:" & Me.Width & " pnlButtons.left: " & Me.pnlButtons.Left)
        Me.SuspendLayout()
        If Me.Width > Me.pnlButtons.Width Then
            'Me.pnlButtons.Width = Me.Width
            Me.pnlButtons.Left = 0
            configScroll()
        ElseIf Me.Width < Me.pnlButtons.Width Then
            Me.pnlButtons.Left = 0
            configScroll()
        End If
        Me.ResumeLayout()

    End Sub


#Region "buttons left &  right"
    ''' <summary>
    ''' button right, moves buttons to left to see buttons at the right side
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        moveButtonsToRight()
    End Sub
    ''' <summary>
    ''' button left, moves buttons to right to see buttons at the left side or initial side
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click
        moveButtonsToLeft()
    End Sub
#End Region
    ''' <summary>
    ''' moves buttons to left to see buttons at the right side
    ''' </summary>
    Public Sub moveButtonsToRight()
        'Debug.WriteLine("btnRight 1 Me.pnlButtons.Width:" & Me.pnlButtons.Width & "  Me.Width:" & Me.Width & "  scb.Value/Max:" & scb.Value & "/" & scb.Maximum & " pnlButtons.left: " & Me.pnlButtons.Left)
        Dim newPos As Integer = Me.pnlButtons.Left - scb.Value

        If (newPos + Me.pnlButtons.Width) <= Me.btnLeft.Left Then
            Dim lastCtrl As Control = getLastButton()
            Dim dista As Integer = Me.pnlButtons.Left + (lastCtrl.Left + lastCtrl.Width)
            If (dista) < Me.btnLeft.Left Then
                Exit Sub
            End If
        End If
        Me.pnlButtons.Left = Me.pnlButtons.Left - scb.Value
    End Sub

    Private Function getLastButton() As Control
        Dim lastCtrl As Control = Nothing
        If Me.pnlButtons.Controls.Count = 0 Then
            Return Nothing
        Else
            lastCtrl = Me.pnlButtons.Controls(0)
        End If
        For Each ctrlHosted As Control In Me.pnlButtons.Controls
            If lastCtrl.Left < ctrlHosted.Left Then
                lastCtrl = ctrlHosted
            End If
        Next
        Return lastCtrl
    End Function



    ''' <summary>
    ''' moves buttons to right to see buttons at the left side or initial side
    ''' </summary>
    Public Sub moveButtonsToLeft()
        'Debug.WriteLine("btnLeft 1 Me.pnlButtons.Width:" & Me.pnlButtons.Width & "  Me.Width:" & Me.Width & "  scb.Value/Max:" & scb.Value & "/" & scb.Maximum & " pnlButtons.left: " & Me.pnlButtons.Left)
        Dim newPos As Integer = Me.pnlButtons.Left + scb.Value
        If newPos > 0 Then
            Me.pnlButtons.Left = 0
            'Me.btnLeft.Enabled = False
            Exit Sub
        End If
        Me.pnlButtons.Left = Me.pnlButtons.Left + scb.Value
        'Me.btnRight.Enabled = True
    End Sub

    Public Sub showButton(btn2 As IndronTabButton1)
        Dim bLeft As Integer = btn2.Left
        Debug.WriteLine("showButton ContainerWidth:" & Me.Width & " btn.left: " & btn2.Left & " btn2.Width: " & btn2.Width)
        Debug.WriteLine("Me.pnlButtons.Width:" & Me.pnlButtons.Width & "  Me.Width:" & Me.Width & "  scb.Value/Max:" & scb.Value & "/" & scb.Maximum & " pnlButtons.left: " & Me.pnlButtons.Left)

        'If -bLeft < -Me.pnlButtons.Left Then
        '    moveButtonsToRight()
        'End If

        Dim dist2move As Integer = Me.pnlButtons.Left + btn2.Left

        'Me.pnlButtons.Left = Me.pnlButtons.Left + btn2.Left
        If dist2move >= 0 AndAlso (dist2move + btn2.Width) <= Me.Width Then
            'it´s visible
            Exit Sub
        End If
        If dist2move > Me.Width Then
            Dim abswidth As Double = Math.Floor(Me.Width / btn2.Width) * btn2.Width
            Dim newDist As Integer = dist2move - CInt(abswidth)

            dist2move = newDist + btn2.Width

            Debug.WriteLine("dist2move > Me.Width:" & dist2move)
        End If

        Me.pnlButtons.Left = Me.pnlButtons.Left - dist2move
    End Sub
End Class
