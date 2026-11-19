/*
  KeePass Password Safe - The Open-Source Password Manager
  Copyright (C) 2003-2014 Dominik Reichl <dominik.reichl@t-online.de>

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Text;
using System.Runtime.InteropServices;

namespace KeePass.Native
{
    public static partial class NativeMethods
	{


		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindow(IntPtr hWnd);
        
		[DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr FindWindowEx(IntPtr hwndParent,
			IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("User32.dll")]
		private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd); //todo: used

		[DllImport("User32.dll")]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll", EntryPoint = "GetClassLong")]
		private static extern IntPtr GetClassLongPtr32(IntPtr hWnd, int nIndex);

		[DllImport("User32.dll", EntryPoint = "GetClassLongPtr")]
		private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("User32.dll", SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		private static extern int GetWindowText(IntPtr hWnd,
			StringBuilder lpString, int nMaxCount);

		[DllImport("User32.dll")]
		private static extern IntPtr GetForegroundWindow(); // Private, is wrapped

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
        
		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RegisterHotKey(IntPtr hWnd, int id,
			uint fsModifiers, uint vk);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		[DllImport("User32.dll", EntryPoint = "SendInput", SetLastError = true)]
		internal static extern uint SendInput32(uint nInputs, INPUT32[] pInputs,
			int cbSize);

		[DllImport("User32.dll", EntryPoint = "SendInput", SetLastError = true)]
		internal static extern uint SendInput64Special(uint nInputs,
			SpecializedKeyboardINPUT64[] pInputs, int cbSize);

		[DllImport("User32.dll")]
		internal static extern IntPtr GetMessageExtraInfo();

		[DllImport("User32.dll")]
		internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern ushort VkKeyScan(char ch);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern ushort VkKeyScanEx(char ch, IntPtr hKL);

		[DllImport("User32.dll")]
		internal static extern ushort GetKeyState(int vKey);

		[DllImport("User32.dll")]
		internal static extern ushort GetAsyncKeyState(int vKey);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)]
			bool fBlockIt);

		[DllImport("User32.dll")]
		internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ChangeClipboardChain(IntPtr hWndRemove,
			IntPtr hWndNewNext);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EmptyClipboard();

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClipboard();

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr GetClipboardData(uint uFormat);

		[DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern uint RegisterClipboardFormat(string lpszFormat);

		[DllImport("User32.dll")]
		internal static extern uint GetClipboardSequenceNumber();

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalFree(IntPtr hMem);

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalLock(IntPtr hMem);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("Kernel32.dll")]
		internal static extern UIntPtr GlobalSize(IntPtr hMem);

        [DllImport("User32.dll")]
		internal static extern IntPtr GetKeyboardLayout(uint idThread);

		[DllImport("User32.dll")]
		internal static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint uFlags);

		[DllImport("User32.dll")]
		internal static extern uint GetWindowThreadProcessId(IntPtr hWnd,
			[Out] out uint lpdwProcessId);

        [DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        
		[DllImport("User32.dll")]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

	}
}
