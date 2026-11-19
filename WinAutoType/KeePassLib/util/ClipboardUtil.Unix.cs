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
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

using KeePassLib;
using KeePassLib.Native;

namespace KeePass.Util
{
	public static partial class ClipboardUtil
	{
		// https://sourceforge.net/p/keepass/patches/84/
		// https://sourceforge.net/p/keepass/patches/85/
		private const AppRunFlags XSelFlags = (AppRunFlags.GetStdOutput |
			AppRunFlags.GCKeepAlive | AppRunFlags.DoEvents |
			AppRunFlags.DisableForms);

        //private static string GetStringM()
        //{
        //    // string strGtk = GtkGetString();
        //    // if(strGtk != null) return strGtk;

        //    return (NativeLib.RunConsoleApp("pbpaste", "-pboard general") ??
        //        string.Empty);
        //}

        //private static void SetStringM(string str)
        //{
        //    // if(GtkSetString(str)) return;

        //    NativeLib.RunConsoleApp("pbcopy", "-pboard general", str);
        //}

        //private static string GetStringU()
        //{
        //    // string strGtk = GtkGetString();
        //    // if(strGtk != null) return strGtk;

        //    // string str = NativeLib.RunConsoleApp("xclip",
        //    //	"-out -selection clipboard");
        //    // if(str != null) return str;

        //    string str = NativeLib.RunConsoleApp("xsel",
        //        "--output --clipboard", null, XSelFlags);
        //    if(str != null) return str;

        //    if(Clipboard.ContainsText())
        //        return (Clipboard.GetText() ?? string.Empty);

        //    return string.Empty;
        //}

        //private static void SetStringU(string str)
        //{
        //    // if(GtkSetString(str)) return;

        //    // string r = NativeLib.RunConsoleApp("xclip",
        //    //	"-in -selection clipboard", str);
        //    // if(r != null) return;

        //    if(string.IsNullOrEmpty(str))
        //    {
        //        // xsel with an empty input can hang, thus use --clear
        //        if(NativeLib.RunConsoleApp("xsel", "--clear --primary",
        //            null, XSelFlags) != null)
        //        {
        //            NativeLib.RunConsoleApp("xsel", "--clear --clipboard",
        //                null, XSelFlags);
        //            return;
        //        }

        //        try { Clipboard.Clear(); }
        //        catch(Exception) { Debug.Assert(false); }
        //        return;
        //    }

        //    // xsel does not support --primary and --clipboard together
        //    if(NativeLib.RunConsoleApp("xsel", "--input --primary",
        //        str, XSelFlags) != null)
        //    {
        //        NativeLib.RunConsoleApp("xsel", "--input --clipboard",
        //            str, XSelFlags);
        //        return;
        //    }

        //    try { Clipboard.SetText(str); }
        //    catch(Exception) { Debug.Assert(false); }
        //}
	}
}
