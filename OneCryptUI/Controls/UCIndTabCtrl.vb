
''' <summary>
''' Class to host <see cref=" UCLibraryTabpage "></see>
''' in partial class Inherits IndronTabControl 
''' </summary>
''' <remarks></remarks>
Public Class UCIndTabCtrl

#Region "Public methods"

    ''' <summary>
    ''' load UCCryptExplorer on <see cref=" UCLibraryTabpage "></see>
    ''' </summary>
    ''' <param name="libraryToOpen"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadUCLibraryTabPageByLibrary(ByVal libraryToOpen As LibraryDataSet.T_LibraryRow, Optional mustSelect As Boolean = True) As UcLibraryTabpage
        Try
            'check if exists
            Dim f As FrmLibrary = FrmLibrary.getFormUCLibrary(libraryToOpen)
            If Not f Is Nothing Then
                'Debug.WriteLine("FrmLibrary open")
                If f.WindowState = FormWindowState.Minimized Then
                    f.WindowState = FormWindowState.Normal
                End If

                'frmPwdMgrContainer.Show()
                f.Activate()
                f.BringToFront()
                Return f.ucLibTab
            End If
            'as tab
            Dim uclibtab As UcLibraryTabpage = Me.GetUcTabByKey(libraryToOpen.id)
            If Not uclibtab Is Nothing Then
                If mustSelect Then
                    Me.tabSelect(uclibtab)
                End If

                ind.crypt.LibraryDataHelper.updateLibraryLastAccess(libraryToOpen.id, Date.Now)
                Return uclibtab
            End If
            'not existObj so start load
            Cursor = Cursors.WaitCursor
            Dim ucLibExp As New UCCryptExplorer
            ucLibExp.initConfig(libraryToOpen)
            ind.crypt.LibraryDataHelper.updateLibraryLastAccess(libraryToOpen.id, Date.Now)

            Dim tp As New UcLibraryTabpage
            tp.InitConfig(ucLib:=ucLibExp)
            Me.AddTabPageBase(ucLibExp.LibraryLoaded.libraryID, tp, mustSelect)
            Return tp

        Catch ex As Exception
            Throw ex
        Finally
            Cursor = Cursors.Default
        End Try

    End Function



    Public Function GetUcTabByKey(ByVal libraryIdKey As Integer) As UcLibraryTabpage

        Dim tbi1 As IndTabInfo = Me.tabCol(libraryIDKey)
        If Not tbi1 Is Nothing Then
            If tbi1.tab.GetType Is GetType(UcLibraryTabpage) Then
                Dim EntTab As UcLibraryTabpage = DirectCast(tbi1.tab, UcLibraryTabpage)
                If EntTab.ucLib.LibraryLoaded.libraryID = libraryIDKey Then
                    Return EntTab
                End If
            End If
        End If

        Return Nothing

    End Function
    ''' <summary>
    ''' get the tab with library with recent files
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUcLibraryRecentFilesTab() As UcLibraryTabpage
        Dim tbi As IndTabInfo = Me.tabCol(ind.crypt.OCGlobal.libraryRecentFilesGlob.id)
        If tbi Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(tbi.tab, UcLibraryTabpage)
        End If
    End Function
    ''' <summary>
    ''' Get Recent libraries tab
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUcLibraryRecentsTab() As UCLibraryRecentsTabPage

        Dim tbi As IndTabInfo = Me.tabCol(ind.crypt.OCGlobal.tabFixedCodes.tabRecentLibs)
        If tbi Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(tbi.tab, UCLibraryRecentsTabPage)
        End If

    End Function

    Public Function GetUcPwdManagerTabPage() As UCPwdManagerTabPage

        Dim tbi As IndTabInfo = Me.tabCol(ind.crypt.OCGlobal.tabFixedCodes.tabPasswordManager)
        If tbi Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(tbi.tab, UCPwdManagerTabPage)
        End If

    End Function







#End Region

End Class
