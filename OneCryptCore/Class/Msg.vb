Imports System.Windows.Forms

Namespace ind.win
    ''' <summary>
    ''' class to show diferent messages to user
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Msg

        Public Shared captionForMessage As String = ind.cte.AppConst.AppName

        Public Shared Sub shoMsg(message As String)
            MessageBox.Show(message)
        End Sub

        Public Shared Sub mError(ex As Exception, Optional caption As String = "")
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            ind.log.Logging.logExcep(ex)
            MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub
        Public Shared Sub mError(message As String, Optional caption As String = "")
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            ind.log.Logging.logErr(message)
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

        Public Shared Sub mWarning(message As String, Optional caption As String = "")
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

        Public Shared Sub mExclamation(message As String, Optional caption As String = "")
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub

        Public Shared Sub mInformation(message As String, Optional caption As String = "")
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Public Shared Function mQuestion(message As String, Optional caption As String = "") As DialogResult
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            Dim ret As DialogResult = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If ret = DialogResult.Yes Then
                ret = DialogResult.OK
            ElseIf ret = DialogResult.No Then
                ret = DialogResult.Cancel
            End If
            Return ret
        End Function

        Public Enum MessageIconEnum As Short
            Information
            Warning
            message
            notification_error
            notification_done
            document_warning
            document_delete
            library_Delete

        End Enum



        Public Shared Function mQuestionDlg(message As String, Optional caption As String = "" _
                , Optional msgIcon As MessageIconEnum = MessageIconEnum.Information _
                , Optional defaultButton As frmDlg.FocusButtonEnum = frmDlg.FocusButtonEnum.cancelBtn) As frmDlg
            If caption = "" Then
                caption = Msg.captionForMessage
            End If
            Dim fd As New frmDlg

            fd.Text = caption
            fd.Message = message
            fd.setFocusOnBtn(defaultButton)

            Select Case msgIcon
                Case MessageIconEnum.Information
                    fd.imgMessage.Image = My.Resources.info_blue
                Case MessageIconEnum.Warning
                    fd.imgMessage.Image = My.Resources.notification_warning
                Case MessageIconEnum.message
                    fd.imgMessage.Image = My.Resources.message

                Case MessageIconEnum.notification_error
                    fd.imgMessage.Image = My.Resources.notification_error
                Case MessageIconEnum.notification_done
                    fd.imgMessage.Image = My.Resources.notification_done


                Case MessageIconEnum.document_warning
                    fd.imgMessage.Image = My.Resources.document_warning
                Case MessageIconEnum.document_delete
                    fd.imgMessage.Image = My.Resources.document_delete
                Case MessageIconEnum.library_Delete
                    fd.imgMessage.Image = My.Resources.Library_delete1
                Case Else
                    fd.imgMessage.Image = My.Resources.info_blue
            End Select

            Return fd
        End Function

    End Class
End Namespace

