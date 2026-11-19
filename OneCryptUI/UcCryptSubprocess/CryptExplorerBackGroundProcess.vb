


Public Class CryptExplorerBackGroundProcess

    Public Sub config(parenControl As Control, lvTreeExp As ListView)


    End Sub
#Region "Vars"
    Friend parenControl As Control
    Friend lvTreeExp As ListView
    Friend WithEvents BackgroundWorker1 As New System.ComponentModel.BackgroundWorker
#End Region

#Region "Enum"
    Public Enum DecryptOpenActionEnum As Short
        None = -1
        ''' <summary>
        ''' Used as an option to indicate if app must open after decrypt
        ''' </summary>
        DecryptAndOpen = 0
        ''' <summary>
        ''' Used as an option to indicate if app must open after decrypt
        ''' </summary>
        DecryptAndNoOpen = 1
    End Enum
#End Region

#Region "BackgroundWorker"
    Dim progress As UCCryptProgressB


    Friend Sub ConfigureBackGroundWorkItemsToParams(lviTemsForOper As ListView.ListViewItemCollection, encryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, openAfterDecrypt As DecryptOpenActionEnum)
        'Dim lvColSelected As ListView.SelectedListViewItemCollection = Me.lvTreeExp.SelectedItems
        Me.bgwp = New BackGroundWorkItemsToParams
        bgwp.lvColSelected = lviTemsForOper
        bgwp.EncryptOrDecrypt = encryptOrDecrypt

        'load list of selected files in array list
        For Each lvi As ind.win.ListViewItemEx In Me.bgwp.lvColSelected
            Dim itemToEncrypt As New ItemBagCls
            itemToEncrypt.lviIndex = lvi.Index
            itemToEncrypt.lviOriginal = lvi
            itemToEncrypt.FilePathTo = lvi.SubItems(ind.crypt.FileObject.lvFieldEnum.path).Text
            itemToEncrypt.co = lvi.coFile
            itemToEncrypt.openAfterDecrypt = openAfterDecrypt 'when encrypt we need to use this because the same method use this for decrypt
            itemToEncrypt.CryptoAction = encryptOrDecrypt
            Me.bgwp.lvSelCol.Add(itemToEncrypt)
        Next

        Me.ConfigureProgress(encryptOrDecrypt, bgwp.lvColSelected.Count)
        Me.lvTreeExp.Enabled = False
        Me.lvTreeExp.Cursor = Cursors.WaitCursor
    End Sub

    Private Sub ConfigureProgress(EncryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum, maxItems As Integer)
        Me.progress = New UCCryptProgressB
        Me.progress.Parent = Me.parenControl
        Me.progress.BringToFront()
        Me.progress.Location = New System.Drawing.Point(Me.parenControl.Width - Me.progress.Width, Me.parenControl.Height - Me.progress.Height)
        Me.progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        Me.progress.ProgressBar1.Maximum = maxItems
        Me.progress.ProgressBar1.Minimum = 0
        Me.progress.BackgroundWorker1 = Me.BackgroundWorker1
        If EncryptOrDecrypt = ind.crypt.Crypto.CryptoActionEnum.ActionEncrypt Then
            Me.progress.lbl_Title.Text = ind.lang.UCCryptProgressLang.lbl_Title_encrypt
        Else
            Me.progress.lbl_Title.Text = ind.lang.UCCryptProgressLang.lbl_Title_decrypt
        End If

        Me.progress.Show()
    End Sub

    Dim bgwp As BackGroundWorkItemsToParams

    Class BackGroundWorkItemsToParams
        Public EncryptOrDecrypt As ind.crypt.Crypto.CryptoActionEnum
        ''' <summary>
        ''' original selected items, do not use in threads for loop
        ''' </summary>
        ''' <remarks></remarks>
        Public lvColSelected As ListView.ListViewItemCollection
        Public errorList As New ind.tools.error.ErrorVisorHelper
        Public worker As System.ComponentModel.BackgroundWorker
        Public e As System.ComponentModel.DoWorkEventArgs
        ''' <summary>
        ''' arraylist with listviewitems selected to avoid cross-thread problems when loop SelectedListViewItemCollection
        ''' </summary>
        ''' <remarks></remarks>
        Public lvSelCol As New ArrayList
        ''' <summary>
        ''' for more than 5 files the opena
        ''' </summary>
        ''' <remarks></remarks>
        Public DecryptOpenAction As DecryptOpenActionEnum = DecryptOpenActionEnum.None
    End Class

    Class ItemBagCls
        Public lviIndex As Integer
        Public lviOriginal As ind.win.ListViewItemEx
        Public FilePathTo As String
        Public co As ind.crypt.FileObject
        Public openAfterDecrypt As DecryptOpenActionEnum = DecryptOpenActionEnum.DecryptAndNoOpen
        Public CryptoAction As ind.crypt.Crypto.CryptoActionEnum
    End Class
#End Region

End Class
