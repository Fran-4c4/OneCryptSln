
''' <summary>
''' user control with the list of recent libraries
''' </summary>
''' <remarks></remarks>
Public Class UcLibraryRecents

    Public WithEvents ucExplorer1 As UCExplorer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler ind.win.AppEventsClass.LibraryNewCreatedEvent, AddressOf LibraryNewCreatedHandler
        AddHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
    End Sub

    Private Sub UCLibraryRecents_Load(sender As Object, e As EventArgs) Handles Me.Load
        myAppEvents_LanguageChangedEvent()

    End Sub

    Private Sub LibraryNewCreatedHandler(drLibrary As LibraryDataSet.T_LibraryRow)
        loadRecentLibraries()
    End Sub
    Public Sub LibraryDeletedEventHandler(libraryIdKey As Integer)
        loadRecentLibraries()
    End Sub
    ''' <summary>
    ''' init the control.
    ''' This method is called by main when app is loaded
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub initUClibraryRecentsAfterLoad()
        If Me.DesignMode = False Then
            If Me.controlIsInitialized = False Then
                Me.addLibraryRecentFilesButton()
                Me.loadRecentLibraries() 'load all the libraries from database
            End If
        End If

        'Me.pnlFlowMain.Focus()
    End Sub

    Private Sub myAppEvents_LanguageChangedEvent() Handles myAppEvents.LanguageChangedEvent
        Me.lbl_Title.Text = ind.lang.UCLibraryLang.lbl_Title
        Me.lblSubTitle.Text = ind.lang.UCLibraryLang.lblSubTitle
        Me.ToolTip1.SetToolTip(Me.btnCreateLib, ind.lang.UCLibraryLang.btnOpenLib_TT)
    End Sub
    ''' <summary>
    ''' load recent libraries from database
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadRecentLibraries()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.pnlFlowRecent1.SuspendLayout()
            Dim mda1 As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
            Dim dtLibraries As LibraryDataSet.T_LibraryDataTable
            dtLibraries = mda1.GetTableOrderByLastAccess
            Me.pnlFlowRecent1.Controls.Clear()
            Me.pnlFlowRecent1.Height = 0

            For Each drl As LibraryDataSet.T_LibraryRow In dtLibraries.Rows
                If drl.id <> ind.crypt.OCGlobal.libraryRecentFilesGlob.id Then
                    addLibraryRecent(drl)
                End If
            Next
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        Finally
            Me.pnlFlowRecent1.ResumeLayout()
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub addLibraryRecent(libraryToLoad As LibraryDataSet.T_LibraryRow)
        If libraryToLoad Is Nothing Then Exit Sub

        Dim libName As String = libraryToLoad.Name  ' IO.Path.GetFileNameWithoutExtension(libraryToLoad)
        'check if existObj?
        Dim uclibButton As New UCLibraryRecentElement(Me, libraryToLoad)
        MoveDownControls(uclibButton.Height)
        uclibButton.Top = 0
        uclibButton.Left = 0
        uclibButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        uclibButton.Width = Me.pnlFlowRecent1.Width
        uclibButton.Visible = True
        uclibButton.IndronButton1.RemoveItemToolTip = ind.lang.UCLibraryLang.LibraryDeleteToolTipMessage & " " & libName
        uclibButton.IndronButton1.ttip = Me.ToolTip1


        Me.pnlFlowRecent1.Controls.Add(uclibButton)
    End Sub

    Private Sub MoveDownControls(heigthAdd As Integer)

        Me.pnlFlowRecent1.Height += heigthAdd
        For Each c As Control In Me.pnlFlowRecent1.Controls
            c.Top += heigthAdd
            c.Width = Me.pnlFlowRecent1.Width
        Next

        'not need to set the scroll. It is set by the control
        'If Me.pnlFlowRecent1.Height > Me.pnlFlowMain.Height Then
        '    Me.pnlFlowMain.VerticalScroll.Visible = True
        'Else
        '    Me.pnlFlowMain.VerticalScroll.Visible = False
        'End If
    End Sub

    ''' <summary>
    ''' add the button to open recent files accesed by user above the list of recent libraries
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addLibraryRecentFilesButton()

        Dim uclibRecentButton As New UCLibraryRecentElement(Me, ind.crypt.OCGlobal.libraryRecentFilesGlob)
        uclibRecentButton.IndronButton1.titleSub = ""
        uclibRecentButton.Top = Me.pnlFlowMain.Top - uclibRecentButton.Height - 4
        uclibRecentButton.Left = Me.pnlFlowMain.Left
        uclibRecentButton.Width = Me.pnlFlowMain.Width
        uclibRecentButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Left Or AnchorStyles.Top), System.Windows.Forms.AnchorStyles)
        uclibRecentButton.Visible = True
        uclibRecentButton.IndronButton1.CloseButtonVisible = False
        uclibRecentButton.IndronButton1.ttip = Me.ToolTip1
        uclibRecentButton.IndronButton1.Icon = My.Resources.project_menu_recentfiles_32

        Me.Controls.Add(uclibRecentButton)
    End Sub
    ''' <summary>
    ''' delete a library
    ''' </summary>
    ''' <param name="libraryId"></param>
    ''' <param name="libraryName"></param>
    ''' <remarks></remarks>
    Public Sub LibraryDelete(libraryId As Integer, libraryName As String)
        Try

            Dim fDlg As frmDlg = ind.win.Msg.mQuestionDlg(ind.lang.UCLibraryLang.LibraryDeleteDlgMessage & vbCr & libraryName, ind.lang.UCLibraryLang.LibraryDeleteDlgTitle, ind.win.Msg.MessageIconEnum.library_Delete)
            fDlg.setFocusOnBtn(frmDlg.FocusButtonEnum.cancelBtn)

            If fDlg.ShowDialog = DialogResult.OK Then
                'borrar
                Dim da As New LibraryDataSetTableAdapters.T_LibraryTableAdapter
                da.DeleteLibraryByID(libraryId)
                ind.win.AppEventsClass.LibraryDeletedEventRaise(libraryId)
            End If
        Catch ex As Exception
            ind.win.Msg.mError(ex)
        End Try

    End Sub

    ''' <summary>
    ''' creates a new library
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateLib_Click(sender As Object, e As EventArgs) Handles btnCreateLib.Click
        UcNewLibrary.createNewLibraryMessageDialog()
    End Sub


End Class
