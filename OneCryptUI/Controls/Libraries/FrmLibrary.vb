


Imports System.ComponentModel
''' <summary>
''' form to host tabs opened in new window
''' </summary>
Public Class FrmLibrary

    Public Shared formsList As New Hashtable

    Public ucLibTab As UcLibraryTabpage
    ''' <summary>
    ''' used to return to tabcontrol. Not used yet
    ''' </summary>
    Dim oldHost As Control

    Private Shared handlersConfigurated As Boolean = False
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Shared Sub configureHandlers()
        If handlersConfigurated = False Then
            handlersConfigurated = True
            AddHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
        End If
    End Sub

    ''' <summary>
    ''' remove tab from tabcontrol when the library is deleted
    ''' </summary>
    ''' <param name="libraryIdKey"></param>
    Public Shared Sub LibraryDeletedEventHandler(libraryIdKey As Integer)
        Dim f As FrmLibrary = CType(formsList(libraryIdKey), FrmLibrary)
        'removeFormLibrary(libraryIdKey)
        Debug.WriteLine("FrmLibrary LibraryDeletedEventHandler " & libraryIdKey)
        If Not f Is Nothing Then
            Debug.WriteLine("FrmLibrary LibraryDeletedEventHandler found " & f.ucLibTab.ucLib.LibraryLoaded.libraryID & " - " & f.ucLibTab.ucLib.LibraryLoaded.LibraryName)
            f.Close()
        End If
    End Sub
    Public Shared Function getFormUCLibrary(ByVal libraryToOpen As LibraryDataSet.T_LibraryRow) As FrmLibrary
        Return getFormUCLibrary(libraryToOpen.id)
    End Function

    Public Shared Function getFormUCLibrary(libraryIdKey As Integer) As FrmLibrary
        Dim f As FrmLibrary = CType(formsList(libraryIdKey), FrmLibrary)
        Return f
    End Function
    ''' <summary>
    ''' remove a library from the list of open libraries in form
    ''' </summary>
    ''' <param name="libraryIdKey"></param>
    Public Shared Sub removeFormLibrary(libraryIdKey As Integer)
        formsList.Remove(libraryIdKey)
    End Sub
    ''' <summary>
    ''' Open the library in a new form
    ''' </summary>
    ''' <param name="uclibTab"></param>
    Public Shared Sub openLibraryInNewForm(uclibTab As IndronTabPage)
        configureHandlers()

        Dim f As New FrmLibrary
        Dim id As Integer = -9999
        If uclibTab.GetType Is GetType(UcLibraryTabpage) Then
            Dim uc As UcLibraryTabpage = DirectCast(uclibTab, UcLibraryTabpage)
            id = uc.ucLib.LibraryLoaded.libraryID
            FrmLibrary.formsList.Add(id, f)
            f.Text = uclibTab.title
            'frmPwdMgrContainer.oldHost = uclibTab.Parent 'not used yet
            Dim xp As Integer = System.Windows.Forms.Cursor.Position.X '+  uclibTab.ParentForm.Left + 
            Dim yp As Integer = System.Windows.Forms.Cursor.Position.Y ' uclibTab.ParentForm.Top + 
            'Debug.WriteLine(String.Format("Xp,YP= {0} / {1}  Form XY={2} / {3} Mouse Location= {4} / {5}", xp, yp, uclibTab.ParentForm.Left, uclibTab.ParentForm.Top, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y))
            f.StartPosition = FormStartPosition.Manual 'if not take other location
            f.Left = xp
            f.Top = yp
            uclibTab.Parent = f
            uclibTab.TabButtonRemove()
            f.ucLibTab = uc
        End If

        f.Show()
    End Sub

    Private Sub FrmLibrary_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Not Me.oldHost Is Nothing Then
            'Me.ucLib.Parent = Me.oldHost
        End If
        Debug.WriteLine("FrmLibrary_Closing " & Me.ucLibTab.ucLib.LibraryLoaded.libraryID)
        removeFormLibrary(Me.ucLibTab.ucLib.LibraryLoaded.libraryID)
        '        RemoveHandler ind.win.AppEventsClass.LibraryDeletedEvent, AddressOf LibraryDeletedEventHandler
    End Sub
End Class