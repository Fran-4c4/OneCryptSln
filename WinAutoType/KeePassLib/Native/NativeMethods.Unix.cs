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
using System.Windows.Forms;
using System.Diagnostics;


using NativeLib = KeePassLib.Native.NativeLib;

namespace KeePass.Native
{
    public static partial class NativeMethods
	{
		internal static string RunXDoTool(string strParams)
		{
			try
			{
				Application.DoEvents(); // E.g. for clipboard updates
				string strOutput = NativeLib.RunConsoleApp("xdotool", strParams);
				Application.DoEvents(); // E.g. for clipboard updates
				return (strOutput ?? string.Empty);
			}
			catch(Exception) { Debug.Assert(false); }

			return string.Empty;
		}
     }
}
