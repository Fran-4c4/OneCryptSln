

Friend Class IndBaseColors

    Public Function BackHighColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return Color.Transparent
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ToolStripGradientEnd
        Else
            Return ManagedColor
        End If
    End Function

    Public Function BackLowColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return Color.Transparent
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ToolStripGradientBegin
        Else
            Return ManagedColor
        End If
    End Function

    Public Function BorderColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.ControlDarkDark
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.GripDark
        Else
            Return ManagedColor
        End If
    End Function
    <DebuggerNonUserCode>
    Public Function BorderColorDisabled(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.ControlDark
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.SeparatorDark
        Else
            Return ManagedColor
        End If
    End Function

    <DebuggerNonUserCode>
    Public Function ControlButtonBackHighColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.ButtonHighlight
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ButtonSelectedGradientBegin
        Else
            Return ManagedColor
        End If
    End Function
    <DebuggerNonUserCode>
    Public Function ControlButtonBackLowColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.ButtonHighlight
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ButtonSelectedGradientEnd
        Else
            Return ManagedColor
        End If
    End Function
    <DebuggerNonUserCode>
    Public Function ControlButtonBorderColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.HotTrack
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ButtonPressedBorder
        Else
            Return ManagedColor
        End If
    End Function
    <DebuggerNonUserCode>
    Public Function TabBackHighColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.Control
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.MenuItemPressedGradientBegin
        Else
            Return ManagedColor
        End If
    End Function
 <DebuggerNonUserCode>
    Public Function TabBackLowColor(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.Control
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.MenuItemPressedGradientEnd
        Else
            Return ManagedColor
        End If
    End Function
    <DebuggerNonUserCode>
    Public Function TabBackHighColorDisabled(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.Control
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ToolStripDropDownBackground
        Else
            Return ManagedColor
        End If
    End Function

    Public Function TabBackLowColorDisabled(ByVal RenderMode As ToolStripRenderMode, ByVal ManagedColor As Color) As Color
        If RenderMode = ToolStripRenderMode.System Then
            Return SystemColors.Control
        ElseIf RenderMode = ToolStripRenderMode.Professional Then
            Return ProfessionalColors.ToolStripGradientMiddle
        Else
            Return ManagedColor
        End If
    End Function


End Class
