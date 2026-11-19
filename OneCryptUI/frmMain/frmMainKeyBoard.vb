Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Partial Public Class frmMain

    Private keyboardHookHandle As IntPtr = IntPtr.Zero
    Private keyboardHookCallback As NativeMethods.LowLevelKeyboardProc
    Private ReadOnly controlDoublePressDetector As New ControlDoublePressDetector()

    Public Sub installRemoveKeyboardHook(install As Boolean, Optional throwError As Boolean = False)
        If install Then
            installKeyBoardHook(throwError)
        Else
            uninstallHook(throwError)
        End If
    End Sub

    Public Sub installKeyBoardHook(Optional throwError As Boolean = False)
        If Not ind.settings.SettingsLink.chkOpenAppWithCtrolKey OrElse keyboardHookHandle <> IntPtr.Zero Then
            Return
        End If

        Try
            keyboardHookCallback = AddressOf KeyboardHookProcedure
            Dim currentProcess = Process.GetCurrentProcess()
            Try
                Dim moduleHandle = NativeMethods.GetModuleHandle(currentProcess.MainModule.ModuleName)
                keyboardHookHandle = NativeMethods.SetWindowsHookEx(
                    NativeMethods.WH_KEYBOARD_LL,
                    keyboardHookCallback,
                    moduleHandle,
                    0UI)
            Finally
                currentProcess.Dispose()
            End Try

            If keyboardHookHandle = IntPtr.Zero Then
                Throw New Win32Exception(Marshal.GetLastWin32Error(), "Unable to install the keyboard shortcut hook.")
            End If

            ind.log.Logging.logInfo("Global Control-key shortcut installed.")
        Catch ex As Exception
            keyboardHookCallback = Nothing
            keyboardHookHandle = IntPtr.Zero
            ind.log.Logging.logErr("Unable to install the global keyboard shortcut: " & ex.Message)
            If throwError Then
                Throw
            End If
        End Try
    End Sub

    Public Sub uninstallHook(Optional throwError As Boolean = False)
        If keyboardHookHandle = IntPtr.Zero Then
            Return
        End If

        Try
            If Not NativeMethods.UnhookWindowsHookEx(keyboardHookHandle) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error(), "Unable to remove the keyboard shortcut hook.")
            End If

            ind.log.Logging.logInfo("Global Control-key shortcut removed.")
        Catch ex As Exception
            ind.log.Logging.logErr("Unable to remove the global keyboard shortcut: " & ex.Message)
            If throwError Then
                Throw
            End If
        Finally
            keyboardHookHandle = IntPtr.Zero
            keyboardHookCallback = Nothing
            controlDoublePressDetector.Reset()
        End Try
    End Sub

    Private Function KeyboardHookProcedure(code As Integer, message As IntPtr, dataPointer As IntPtr) As IntPtr
        If code >= 0 AndAlso
           (message = CType(NativeMethods.WM_KEYUP, IntPtr) OrElse
            message = CType(NativeMethods.WM_SYSKEYUP, IntPtr)) Then

            Dim data = CType(Marshal.PtrToStructure(dataPointer, GetType(NativeMethods.KeyboardHookData)),
                             NativeMethods.KeyboardHookData)

            If (data.flags And NativeMethods.LLKHF_INJECTED) = 0UI AndAlso
               controlDoublePressDetector.RegisterKeyUp(CInt(data.virtualKey), DateTime.UtcNow.Ticks) Then
                BeginInvoke(New MethodInvoker(AddressOf RestoreApplication))
            End If
        End If

        Return NativeMethods.CallNextHookEx(keyboardHookHandle, code, message, dataPointer)
    End Function

    Private NotInheritable Class NativeMethods
        Public Const WH_KEYBOARD_LL As Integer = 13
        Public Const WM_KEYUP As Integer = &H101
        Public Const WM_SYSKEYUP As Integer = &H105
        Public Const LLKHF_INJECTED As UInteger = &H10UI

        <StructLayout(LayoutKind.Sequential)>
        Public Structure KeyboardHookData
            Public virtualKey As UInteger
            Public scanCode As UInteger
            Public flags As UInteger
            Public timestamp As UInteger
            Public extraInfo As UIntPtr
        End Structure

        Public Delegate Function LowLevelKeyboardProc(code As Integer, message As IntPtr, data As IntPtr) As IntPtr

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function SetWindowsHookEx(hookId As Integer,
                                                callback As LowLevelKeyboardProc,
                                                moduleHandle As IntPtr,
                                                threadId As UInteger) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function UnhookWindowsHookEx(hookHandle As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll")>
        Public Shared Function CallNextHookEx(hookHandle As IntPtr,
                                              code As Integer,
                                              message As IntPtr,
                                              data As IntPtr) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
        Public Shared Function GetModuleHandle(moduleName As String) As IntPtr
        End Function
    End Class

End Class
