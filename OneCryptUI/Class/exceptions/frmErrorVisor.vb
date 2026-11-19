Imports System.Windows.Forms



Public Class frmErrorVisor
    Inherits FrmAppEvents

    
    Public errList As ind.tools.error.ErrorVisorHelper

    Public Shared Function ShowVisor(errList As ind.tools.error.ErrorVisorHelper) As frmErrorVisor
        If errList.errorList.Count > 0 Then
            Dim f As New frmErrorVisor
            f.errList = errList
            f.addErrors()
            Return f
        Else
            Return Nothing
        End If

    End Function

    Public Enum lvFieldErrEnum As Integer
        ''' <summary>
        ''' number for list only, not the code
        ''' </summary>
        err_number_forlist = 0
        err_type = 1
        name = 2
        ''' <summary>
        ''' file Full path
        ''' </summary>
        path = 3
        type = 4
        'Length = 3
        error_message = 5
        'dateCreated = 5
        'attributes = 10
    End Enum

    Private Sub frmErrorVisor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub


    Private Sub frmErrorVisor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        myAppEvents_LanguageChangedEvent()
    End Sub

    Public Sub addErrors()
        Dim lvItems As New ArrayList
        If Me.errList Is Nothing Then
            Exit Sub
        End If
        Dim errCount As Integer = 1
        For Each errElem As Object In Me.errList.errorList
            Dim ErrHelp As ind.tools.error.ErrorHelper = CType(errElem, ind.tools.error.ErrorHelper)
            ErrHelp.errNumber = CStr(errCount)
            lvItems.Add(Me.addItem(ErrHelp))
            errCount += 1
        Next

        If lvItems.Count > 0 Then
            Dim items(lvItems.Count - 1) As ListViewItem
            lvItems.CopyTo(items)
            Me.lvTreeExp.Items.AddRange(items)
            Me.lvTreeExp.Focus()

            Me.lvTreeExp.Items(0).Selected = True

            Me.setErrorOnView()
        End If

    End Sub

    Public Function addItem(cFileErrHelper As ind.tools.error.ErrorHelper) As ListViewItem

        'If Me.lvTreeExp.Items.ContainsKey(cFile.FullPath) = True Then
        '    Return Nothing
        'End If

        Dim lvi As New ListViewItem
        'lvi.Name = cFile.errNumber
        lvi.Text = cFileErrHelper.errNumber
        If Not cFileErrHelper.error_exception Is Nothing Then
            lvi.ToolTipText = cFileErrHelper.error_exception.Message
        End If

        For x As Integer = 1 To 6
            lvi.SubItems.Add("")
        Next

        lvi.SubItems(lvFieldErrEnum.err_number_forlist).Text = cFileErrHelper.errNumber
        lvi.SubItems(lvFieldErrEnum.err_type).Text = getErrorTypeForIU(cFileErrHelper.errType)
        If Not cFileErrHelper.fileObj Is Nothing Then
            lvi.SubItems(lvFieldErrEnum.name).Text = cFileErrHelper.fileObj.FileName
            lvi.SubItems(lvFieldErrEnum.path).Text = cFileErrHelper.fileObj.FullPath
            lvi.SubItems(lvFieldErrEnum.type).Text = cFileErrHelper.fileObj.extension
        End If

        If Not cFileErrHelper.error_exception Is Nothing Then
            lvi.SubItems(lvFieldErrEnum.error_message).Text = cFileErrHelper.error_exception.Message
        Else
            lvi.SubItems(lvFieldErrEnum.error_message).Text = cFileErrHelper.error_msg
        End If



        Return lvi

    End Function


    Private Sub lvTreeExp_Click(sender As Object, e As EventArgs) Handles lvTreeExp.Click
        
        Me.setErrorOnView()
    End Sub

    Private Sub setErrorOnView()
        Try
            Dim lvi As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
            If lvi.Count = 0 Then
                Me.lblFilename.Text = Me.lvTreeExp.Items(0).SubItems(lvFieldErrEnum.name).Text
                Me.txtErrorMessage.Text = Me.lvTreeExp.Items(0).SubItems(lvFieldErrEnum.error_message).Text
            Else
                Me.lblFilename.Text = getValueByField(lvi(0), lvFieldErrEnum.name)
                Me.txtErrorMessage.Text = getValueByField(lvi(0), lvFieldErrEnum.error_message)

            End If

            Me.pnlErrorMessage.Visible = True

        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try
    End Sub

    ''' <summary>
    ''' used to facilitate the access to the values
    ''' </summary>
    ''' <param name="lvi"></param>
    ''' <param name="field"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getValueByField(lvi As Windows.Forms.ListViewItem, field As lvFieldErrEnum) As String

        If lvi Is Nothing Then
            Throw New Exception("You send a listviewitem nothing")
        End If

        Return lvi.SubItems(field).Text

    End Function


    Private Function getErrorTypeForIU(errType As ind.tools.error.ErrorHelper.errorTypeEnum) As String
        Select Case errType
            Case ind.tools.error.ErrorHelper.errorTypeEnum.FileInUseException
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_FileInUseException
            Case ind.tools.error.ErrorHelper.errorTypeEnum.FileNotDecryptedException
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_FileNotDecryptedException
            Case ind.tools.error.ErrorHelper.errorTypeEnum.FileNotFoundException
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_FileNotFoundException
            Case ind.tools.error.ErrorHelper.errorTypeEnum.Other
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_Other
            Case ind.tools.error.ErrorHelper.errorTypeEnum.OperationCancelledByUser
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_OperationCancelledByUser
            Case ind.tools.error.ErrorHelper.errorTypeEnum.Unknown
                Return ind.lang.frmErrorVisorLang.ErrorHelper_errorTypeEnum_Unknown
            Case Else
                Return "---"
        End Select


    End Function

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.Text = Global.ind.lang.frmErrorVisorLang.frmErrorVisor_Text
        Me.lbl_Title.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_lbl_Title
        Me.lblTit_filename.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_lblTit_filename
        Me.lblInfoClickToGetMessage.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_lblInfoClickToGetMessage
        'columns
        Me.chName.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chName
        Me.chPath.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chPath
        Me.chType.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chType
        Me.chErrNumber.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chErrNumber
        Me.chErrorMessage.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chErrorMessage
        Me.chErrType.Text = ind.lang.frmErrorVisorLang.frmErrorVisor_chErrtype
    End Sub
End Class