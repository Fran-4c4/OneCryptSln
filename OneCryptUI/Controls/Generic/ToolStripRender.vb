Public Class LeftMenuToolStripRender
    Inherits ToolStripProfessionalRenderer

    Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
        Dim ts As ToolStripItem = e.Item
        Dim btn As ToolStripButton = CType(ts, ToolStripButton)
        If (Not btn Is Nothing And btn.CheckOnClick And btn.Checked) Then

            Dim bounds As New Rectangle(Point.Empty, e.Item.Size)
            e.Graphics.FillRectangle(Brushes.Silver, bounds)

        Else
            MyBase.OnRenderButtonBackground(e)
        End If
    End Sub

End Class
