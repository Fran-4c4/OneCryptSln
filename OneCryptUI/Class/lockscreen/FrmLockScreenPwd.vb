
''' <summary>
''' form to show the password controller when lock computer/screen
''' </summary>
''' <remarks></remarks>
Public Class FrmLockScreenPwd
    Implements UCPwdController.IPwdSender


    Dim bolExit As Boolean = False  '~~~ Used to detect whether the form is being closed only after entering the correct password and not by pressing "Alt" + "F4"

    Private Sub FrmLockScreenPwd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'password usercontrol
        'password
        'Dim ucPwdController1 As New UCPwdController
        'ucPwdController1.Parent = Me
        'ucPwdController1.Location = New Point(0, 0)
        'ucPwdController1.Dock = DockStyle.Fill
        'ucPwdController1.Visible = True
        UcPwdController1.senderController = Me
        UcPwdController1.configForPwdOnly() 'configure control data
    End Sub

    Private Sub Form2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bolExit = False Then     '~~~ When closing the form, checks whether it is being closed by "Alt" + "F4" or by entering the correct password.
            e.Cancel = True         '~~~ If the closing is done without entering the correct password, then prevent the form from being closed
        End If
    End Sub

    ''' <summary>
    ''' handles password is correct
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub passwordIScorrect(sender As Object, isCorrect As Boolean) Implements UCPwdController.IPwdSender.passwordIsCorrect
        If Me.InvokeRequired Then
            Dim args As Object() = {isCorrect}
            Me.BeginInvoke(New passwordIScorrectCallBackDelegate(AddressOf passwordIScorrectCallBack), args)
            Exit Sub
        End If
        If isCorrect = True Then
            bolExit = True                          '~~~ If so, we got the green signal to close the form :-)
            Me.Close()                              '~~~ Close it
        End If

    End Sub

    Private Delegate Sub passwordIScorrectCallBackDelegate(isCorrect As Boolean)
    Private Sub passwordIScorrectCallBack(isCorrect As Boolean)
        If isCorrect = True Then
            bolExit = True                          '~~~ If so, we got the green signal to close the form :-)
            Me.close()                              '~~~ Close it
        End If

    End Sub


End Class