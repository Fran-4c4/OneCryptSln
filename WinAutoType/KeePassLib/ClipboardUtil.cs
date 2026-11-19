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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

using KeePass.App;
//using KeePass.Ecas;
//using KeePass.Forms;
using KeePass.Native;
//using KeePass.UI;
using KeePass.Util;
using KeePass.Util.Spr;

using KeePassLib;
using KeePassLib.Security;
using KeePassLib.Utility;

using NativeLib = KeePassLib.Native.NativeLib;

namespace KeePass.Util
{
    public static partial class ClipboardUtil
    {
        private static byte[] m_pbDataHash32 = null;
        

        private const string ClipboardIgnoreFormatName = "Clipboard Viewer Ignore";


        public static bool Copy(string strToCopy, bool bSprCompile, bool bIsEntryInfo,
            PwEntryObj peEntryInfo, IntPtr hOwner)
        {
            if (strToCopy == null) throw new ArgumentNullException("strToCopy");

            string strData = (bSprCompile ? SprEngine.Compile(strToCopy,
                new SprContext(peEntryInfo, SprCompileFlags.All)) : strToCopy);

            try
            {
                    if (!OpenW(hOwner, true))
                        throw new InvalidOperationException();

                    bool bFailed = false;
                    //if (!AttachIgnoreFormatW()) bFailed = true;
                    if (!SetDataW(null, strData, null)) bFailed = true;
                    CloseW();

                    if (bFailed) return false;
                
            }
            catch (Exception) { Debug.Assert(false); return false; }


            byte[] pbUtf8 = StrUtil.Utf8.GetBytes(strData);
            SHA256Managed sha256 = new SHA256Managed();
            m_pbDataHash32 = sha256.ComputeHash(pbUtf8);

            RaiseCopyEvent(bIsEntryInfo, strData);

            return true;
        }

    

        private static void RaiseCopyEvent(bool bIsEntryInfo, string strDesc)
        {
            if (bIsEntryInfo == false) return;

            //Program.TriggerSystem.RaiseEvent(EcasEventIDs.CopiedEntryInfo,
            //    EcasProperty.Text, strDesc);
        }

        /// <summary>
        /// Safely clear the clipboard. The clipboard clearing method of the
        /// .NET framework sets the clipboard to an empty <c>DataObject</c> when
        /// invoking the clearing method -- this might cause incompatibilities
        /// with other applications. Therefore, the <c>Clear</c> method of
        /// <c>ClipboardUtil</c> first tries to clear the clipboard using
        /// native Windows functions (which *really* clear the clipboard).
        /// </summary>
        public static void Clear()
        {
            bool bNativeSuccess = false;
            try
            {
                if (OpenW(IntPtr.Zero, true)) // Clears the clipboard
                    {
                        CloseW();
                        bNativeSuccess = true;
                    }

            }
            catch (Exception) { Debug.Assert(false); }

            if (bNativeSuccess) return;

            try { Clipboard.Clear(); } // Fallback to .NET framework method
            catch (Exception) { Debug.Assert(false); }
        }

        public static bool ContainsText()
        {
            return Clipboard.ContainsText();
           
        }
        public static string GetText()
        {
             return Clipboard.GetText();
        }
    }
}
