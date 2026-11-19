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

using System.Text;

using KeePassLib.Utility;

namespace KeePass.Util.Spr
{
	/// <summary>
	/// String placeholders and field reference replacement engine.
	/// </summary>
	public static partial class SprEngine
	{
		private static string ConvertToDownArrows(string str, int iOffset,
			string strLayout)
		{
			if(string.IsNullOrEmpty(str)) return string.Empty;

			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < str.Length; ++i)
			{
				

				char ch = str[i];

				int? iDowns = null;
				if(strLayout.Length == 0)
				{
					if((ch >= '0') && (ch <= '9')) iDowns = (int)ch - '0';
					else if((ch >= 'a') && (ch <= 'z')) iDowns = (int)ch - 'a';
					else if((ch >= 'A') && (ch <= 'Z')) iDowns = (int)ch - 'A';
				}
				else if(strLayout.Equals("0a", StrUtil.CaseIgnoreCmp))
				{
					if((ch >= '0') && (ch <= '9')) iDowns = (int)ch - '0';
					else if((ch >= 'a') && (ch <= 'z')) iDowns = (int)ch - 'a' + 10;
					else if((ch >= 'A') && (ch <= 'Z')) iDowns = (int)ch - 'A' + 10;
				}
				else if(strLayout.Equals("a0", StrUtil.CaseIgnoreCmp))
				{
					if((ch >= '0') && (ch <= '9')) iDowns = (int)ch - '0' + 26;
					else if((ch >= 'a') && (ch <= 'z')) iDowns = (int)ch - 'a';
					else if((ch >= 'A') && (ch <= 'Z')) iDowns = (int)ch - 'A';
				}
				else if(strLayout.Equals("1a", StrUtil.CaseIgnoreCmp))
				{
					if((ch >= '1') && (ch <= '9')) iDowns = (int)ch - '1';
					else if(ch == '0') iDowns = 9;
					else if((ch >= 'a') && (ch <= 'z')) iDowns = (int)ch - 'a' + 10;
					else if((ch >= 'A') && (ch <= 'Z')) iDowns = (int)ch - 'A' + 10;
				}
				else if(strLayout.Equals("a1", StrUtil.CaseIgnoreCmp))
				{
					if((ch >= '1') && (ch <= '9')) iDowns = (int)ch - '1' + 26;
					else if(ch == '0') iDowns = 9 + 26;
					else if((ch >= 'a') && (ch <= 'z')) iDowns = (int)ch - 'a';
					else if((ch >= 'A') && (ch <= 'Z')) iDowns = (int)ch - 'A';
				}

				if(!iDowns.HasValue) continue;

				for(int j = 0; j < (iOffset + iDowns); ++j) sb.Append(@"{DOWN}");
			}

			return sb.ToString();
		}
	}
}
