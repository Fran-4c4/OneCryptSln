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
using System.Security;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

//using KeePass.UI;
using KeePass.Util;

using KeePassLib.Utility;

using NativeLib = KeePassLib.Native.NativeLib;

namespace KeePass.Native
{
    public static partial class NativeMethods
	{
		internal static string GetWindowText(IntPtr hWnd, bool bTrim)
		{
			int nLength = GetWindowTextLength(hWnd);
			if(nLength <= 0) return string.Empty;

			StringBuilder sb = new StringBuilder(nLength + 1);
			GetWindowText(hWnd, sb, sb.Capacity);
			string strWindow = sb.ToString();

			return (bTrim ? strWindow.Trim() : strWindow);
		}

		internal static IntPtr GetForegroundWindowHandle()
		{
			if(!NativeLib.IsUnix())
				return GetForegroundWindow(); // Windows API

			try
			{
				return new IntPtr(int.Parse(RunXDoTool("getactivewindow")));
			}
			catch(Exception) { Debug.Assert(false); }
			return IntPtr.Zero;
		}

		private static readonly char[] m_vWindowTrim = { '\r', '\n' };

	    public static void GetForegroundWindowInfo(out IntPtr hWnd,
			out string strWindowText, bool bTrimWindow)
		{
			hWnd = GetForegroundWindowHandle();

			if(!NativeLib.IsUnix()) // Windows
				strWindowText = GetWindowText(hWnd, bTrimWindow);
			else // Unix
			{
				strWindowText = RunXDoTool("getactivewindow getwindowname");
				if(!string.IsNullOrEmpty(strWindowText))
				{
					if(bTrimWindow) strWindowText = strWindowText.Trim();
					else strWindowText = strWindowText.Trim(m_vWindowTrim);
				}
			}
		}

		internal static bool IsWindowEx(IntPtr hWnd)
		{
			if(!NativeLib.IsUnix()) // Windows
				return IsWindow(hWnd);

			return true;
		}

		internal static int GetWindowStyle(IntPtr hWnd)
		{
			return GetWindowLong(hWnd, GWL_STYLE);
		}

		internal static bool SetForegroundWindowEx(IntPtr hWnd)
		{
			if(!NativeLib.IsUnix())
				return SetForegroundWindow(hWnd);

			return (RunXDoTool("windowactivate " +
				hWnd.ToInt64().ToString()).Trim().Length == 0);
		}

		internal static bool EnsureForegroundWindow(IntPtr hWnd)
		{
			if(IsWindowEx(hWnd) == false) return false;

			if(SetForegroundWindowEx(hWnd) == false)
			{
				Debug.Assert(false);
				return false;
			}

			int nStartMS = Environment.TickCount;
			while((Environment.TickCount - nStartMS) < 1000)
			{
				IntPtr h = GetForegroundWindowHandle();
				if(h == hWnd) return true;

				Application.DoEvents();
			}

			return false;
		}

        internal static IntPtr FindWindow(string strTitle)
        {
            if (strTitle == null) { Debug.Assert(false); return IntPtr.Zero; }

            if (!NativeLib.IsUnix())
                return FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, strTitle);

            // Not --onlyvisible (due to not finding minimized windows)
            string str = RunXDoTool("search --name \"" + strTitle + "\"").Trim();
            if (str.Length == 0) return IntPtr.Zero;

            long l;
            if (long.TryParse(str, out l)) return new IntPtr(l);
            return IntPtr.Zero;
        }

	    public static bool LoseFocus(Form fCurrent)
        {
            try
            {
                IntPtr hCurrentWnd = ((fCurrent != null) ? fCurrent.Handle : IntPtr.Zero);
                IntPtr hWnd = GetWindow(hCurrentWnd, GW_HWNDNEXT);

                while (true)
                {
                    if (hWnd != hCurrentWnd)
                    {
                        int nStyle = GetWindowStyle(hWnd);
                        if (((nStyle & WS_VISIBLE) != 0) &&
                            (GetWindowTextLength(hWnd) > 0))
                        {
                            // Skip the taskbar window (required for Windows 7,
                            // when the target window is the only other window
                            // in the taskbar)
                            if (!IsTaskBar(hWnd)) break;
                        }
                    }

                    hWnd = GetWindow(hWnd, GW_HWNDNEXT);
                    if (hWnd == IntPtr.Zero) break;
                }

                if (hWnd == IntPtr.Zero) return false;

                Debug.Assert(GetWindowText(hWnd, true) != "Start");
                return EnsureForegroundWindow(hWnd);
            }
            catch (Exception) { Debug.Assert(false); }

            return false;
        }

        internal static bool IsTaskBar(IntPtr hWnd)
        {
            Process p = null;
            try
            {
                string strText = GetWindowText(hWnd, true);
                if (strText == null) return false;
                if (!strText.Equals("Start", StrUtil.CaseIgnoreCmp)) return false;

                uint uProcessId;
                NativeMethods.GetWindowThreadProcessId(hWnd, out uProcessId);

                p = Process.GetProcessById((int)uProcessId);
                string strExe = UrlUtil.GetFileName(p.MainModule.FileName).Trim();

                return strExe.Equals("Explorer.exe", StrUtil.CaseIgnoreCmp);
            }
            catch (Exception) { Debug.Assert(false); }
            finally
            {
                try { if (p != null) p.Dispose(); }
                catch (Exception) { Debug.Assert(false); }
            }

            return false;
        }

        internal static uint? GetLastInputTime()
        {
            try
            {
                LASTINPUTINFO lii = new LASTINPUTINFO();
                lii.cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO));

                if (!GetLastInputInfo(ref lii)) { Debug.Assert(false); return null; }

                return lii.dwTime;
            }
            catch (Exception)
            {
                //Debug.Assert(NativeLib.IsUnix() || WinUtil.IsWindows9x);
            }

            return null;
        }
	}
}
