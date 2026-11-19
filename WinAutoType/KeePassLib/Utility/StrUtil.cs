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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Globalization;
using System.Diagnostics;

using KeePassLib.Collections;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Native;
//using KeePassLib.Security;
//using KeePassLib.Resources;

namespace KeePassLib.Utility
{
	/// <summary>
	/// Character stream class.
	/// </summary>
	public sealed class CharStream
	{
		private string m_strString = string.Empty;
		private int m_nPos = 0;

		public CharStream(string str)
		{
			Debug.Assert(str != null);
			if(str == null) throw new ArgumentNullException("str");

			m_strString = str;
		}

		public void Seek(SeekOrigin org, int nSeek)
		{
			if(org == SeekOrigin.Begin)
				m_nPos = nSeek;
			else if(org == SeekOrigin.Current)
				m_nPos += nSeek;
			else if(org == SeekOrigin.End)
				m_nPos = m_strString.Length + nSeek;
		}

		public char ReadChar()
		{
			if(m_nPos < 0) return char.MinValue;
			if(m_nPos >= m_strString.Length) return char.MinValue;

			char chRet = m_strString[m_nPos];
			++m_nPos;
			return chRet;
		}

		public char ReadChar(bool bSkipWhiteSpace)
		{
			if(bSkipWhiteSpace == false) return ReadChar();

			while(true)
			{
				char ch = ReadChar();

				if((ch != ' ') && (ch != '\t') && (ch != '\r') && (ch != '\n'))
					return ch;
			}
		}

		public char PeekChar()
		{
			if(m_nPos < 0) return char.MinValue;
			if(m_nPos >= m_strString.Length) return char.MinValue;

			return m_strString[m_nPos];
		}

		public char PeekChar(bool bSkipWhiteSpace)
		{
			if(bSkipWhiteSpace == false) return PeekChar();

			int iIndex = m_nPos;
			while(true)
			{
				if(iIndex < 0) return char.MinValue;
				if(iIndex >= m_strString.Length) return char.MinValue;

				char ch = m_strString[iIndex];

				if((ch != ' ') && (ch != '\t') && (ch != '\r') && (ch != '\n'))
					return ch;

				++iIndex;
			}
		}
	}

	public enum StrEncodingType
	{
		Unknown = 0,
		Default,
		Ascii,
		Utf7,
		Utf8,
		Utf16LE,
		Utf16BE,
		Utf32LE,
		Utf32BE
	}

	public sealed class StrEncodingInfo
	{
		private readonly StrEncodingType m_type;
		public StrEncodingType Type
		{
			get { return m_type; }
		}

		private readonly string m_strName;
		public string Name
		{
			get { return m_strName; }
		}

		private readonly Encoding m_enc;
		public Encoding Encoding
		{
			get { return m_enc; }
		}

		private readonly uint m_cbCodePoint;
		/// <summary>
		/// Size of a character in bytes.
		/// </summary>
		public uint CodePointSize
		{
			get { return m_cbCodePoint; }
		}

		private readonly byte[] m_vSig;
		/// <summary>
		/// Start signature of the text (byte order mark).
		/// May be <c>null</c> or empty, if no signature is known.
		/// </summary>
		public byte[] StartSignature
		{
			get { return m_vSig; }
		}

		public StrEncodingInfo(StrEncodingType t, string strName, Encoding enc,
			uint cbCodePoint, byte[] vStartSig)
		{
			if(strName == null) throw new ArgumentNullException("strName");
			if(enc == null) throw new ArgumentNullException("enc");
			if(cbCodePoint <= 0) throw new ArgumentOutOfRangeException("cbCodePoint");

			m_type = t;
			m_strName = strName;
			m_enc = enc;
			m_cbCodePoint = cbCodePoint;
			m_vSig = vStartSig;
		}
	}

	/// <summary>
	/// A class containing various string helper methods.
	/// </summary>
	public static class StrUtil
	{
		public const StringComparison CaseIgnoreCmp = StringComparison.OrdinalIgnoreCase;

		public static StringComparer CaseIgnoreComparer
		{
			get { return StringComparer.OrdinalIgnoreCase; }
		}

		private static bool m_bRtl = false;
		public static bool RightToLeft
		{
			get { return m_bRtl; }
			set { m_bRtl = value; }
		}

		private static UTF8Encoding m_encUtf8 = null;
		public static UTF8Encoding Utf8
		{
			get
			{
				if(m_encUtf8 == null) m_encUtf8 = new UTF8Encoding(false, false);
				return m_encUtf8;
			}
		}

		private static List<StrEncodingInfo> m_lEncs = null;
		public static IEnumerable<StrEncodingInfo> Encodings
		{
			get
			{
				if(m_lEncs != null) return m_lEncs;

				List<StrEncodingInfo> l = new List<StrEncodingInfo>();

				l.Add(new StrEncodingInfo(StrEncodingType.Default,
#if KeePassRT
					StrUtil.Utf8.WebName, StrUtil.Utf8, 1, null));
#else
#if !KeePassLibSD
					Encoding.Default.EncodingName,
#else
					Encoding.Default.WebName,
#endif
					Encoding.Default,
					(uint)Encoding.Default.GetBytes("a").Length, null));
#endif
#if !KeePassRT
				l.Add(new StrEncodingInfo(StrEncodingType.Ascii,
					"ASCII", Encoding.ASCII, 1, null));
				l.Add(new StrEncodingInfo(StrEncodingType.Utf7,
					"Unicode (UTF-7)", Encoding.UTF7, 1, null));
#endif
				l.Add(new StrEncodingInfo(StrEncodingType.Utf8,
					"Unicode (UTF-8)", StrUtil.Utf8, 1, new byte[] { 0xEF, 0xBB, 0xBF }));
				l.Add(new StrEncodingInfo(StrEncodingType.Utf16LE,
					"Unicode (UTF-16 LE)", new UnicodeEncoding(false, false),
					2, new byte[] { 0xFF, 0xFE }));
				l.Add(new StrEncodingInfo(StrEncodingType.Utf16BE,
					"Unicode (UTF-16 BE)", new UnicodeEncoding(true, false),
					2, new byte[] { 0xFE, 0xFF }));
#if (!KeePassLibSD && !KeePassRT)
				l.Add(new StrEncodingInfo(StrEncodingType.Utf32LE,
					"Unicode (UTF-32 LE)", new UTF32Encoding(false, false),
					4, new byte[] { 0xFF, 0xFE, 0x0, 0x0 }));
				l.Add(new StrEncodingInfo(StrEncodingType.Utf32BE,
					"Unicode (UTF-32 BE)", new UTF32Encoding(true, false),
					4, new byte[] { 0x0, 0x0, 0xFE, 0xFF }));
#endif

				m_lEncs = l;
				return l;
			}
		}

		// public static string RtfPar
		// {
		//	// get { return (m_bRtl ? "\\rtlpar " : "\\par "); }
		//	get { return "\\par "; }
		// }

		// /// <summary>
		// /// Convert a string into a valid RTF string.
		// /// </summary>
		// /// <param name="str">Any string.</param>
		// /// <returns>RTF-encoded string.</returns>
		// public static string MakeRtfString(string str)
		// {
		//	Debug.Assert(str != null); if(str == null) throw new ArgumentNullException("str");
		//	str = str.Replace("\\", "\\\\");
		//	str = str.Replace("\r", string.Empty);
		//	str = str.Replace("{", "\\{");
		//	str = str.Replace("}", "\\}");
		//	str = str.Replace("\n", StrUtil.RtfPar);
		//	StringBuilder sbEncoded = new StringBuilder();
		//	for(int i = 0; i < str.Length; ++i)
		//	{
		//		char ch = str[i];
		//		if((int)ch >= 256)
		//			sbEncoded.Append(StrUtil.RtfEncodeChar(ch));
		//		else sbEncoded.Append(ch);
		//	}
		//	return sbEncoded.ToString();
		// }

		public static string RtfEncodeChar(char ch)
		{
			// Unicode character values must be encoded using
			// 16-bit numbers (decimal); Unicode values greater
			// than 32767 must be expressed as negative numbers
			short sh = (short)ch;
			return ("\\u" + sh.ToString(NumberFormatInfo.InvariantInfo) + "?");
		}

		/// <summary>
		/// Convert a string into a valid HTML sequence representing that string.
		/// </summary>
		/// <param name="str">String to convert.</param>
		/// <returns>String, HTML-encoded.</returns>
		public static string StringToHtml(string str)
		{
			Debug.Assert(str != null); if(str == null) throw new ArgumentNullException("str");

			str = str.Replace(@"&", @"&amp;");
			str = str.Replace(@"<", @"&lt;");
			str = str.Replace(@">", @"&gt;");
			str = str.Replace("\"", @"&quot;");
			str = str.Replace("\'", @"&#39;");

			str = NormalizeNewLines(str, false);
			str = str.Replace("\n", @"<br />" );

			return str;
		}

		public static string XmlToString(string str)
		{
			Debug.Assert(str != null); if(str == null) throw new ArgumentNullException("str");

			str = str.Replace(@"&amp;", @"&");
			str = str.Replace(@"&lt;", @"<");
			str = str.Replace(@"&gt;", @">");
			str = str.Replace(@"&quot;", "\"");
			str = str.Replace(@"&#39;", "\'");

			return str;
		}

		public static string ReplaceCaseInsensitive(string strString, string strFind,
			string strNew)
		{
			string str = strString;

			int nPos = 0;
			while(nPos < str.Length)
			{
				nPos = str.IndexOf(strFind, nPos, StringComparison.OrdinalIgnoreCase);
				if(nPos < 0) break;

				str = str.Remove(nPos, strFind.Length);
				str = str.Insert(nPos, strNew);

				nPos += strNew.Length;
			}

			return str;
		}


		// /// <summary>
		// /// Convert a simple HTML string to an RTF string.
		// /// </summary>
		// /// <param name="strHtmlString">Input HTML string.</param>
		// /// <returns>RTF string representing the HTML input string.</returns>
		// public static string SimpleHtmlToRtf(string strHtmlString)
		// {
		//	StringBuilder sb = new StringBuilder();
		//	StrUtil.InitRtf(sb, "Microsoft Sans Serif", 8.25f);
		//	sb.Append(" ");
		//	string str = MakeRtfString(strHtmlString);
		//	str = str.Replace("<b>", "\\b ");
		//	str = str.Replace("</b>", "\\b0 ");
		//	str = str.Replace("<i>", "\\i ");
		//	str = str.Replace("</i>", "\\i0 ");
		//	str = str.Replace("<u>", "\\ul ");
		//	str = str.Replace("</u>", "\\ul0 ");
		//	str = str.Replace("<br />", StrUtil.RtfPar);
		//	sb.Append(str);
		//	return sb.ToString();
		// }

	

		

		public static bool TryParseUShort(string str, out ushort u)
		{
#if !KeePassLibSD
			return ushort.TryParse(str, out u);
#else
			try { u = ushort.Parse(str); return true; }
			catch(Exception) { u = 0; return false; }
#endif
		}


#if !KeePassLibSD
		private static readonly char[] m_vPatternPartsSep = new char[]{ '*' };
		public static bool SimplePatternMatch(string strPattern, string strText,
			StringComparison sc)
		{
			if(strPattern == null) throw new ArgumentNullException("strPattern");
			if(strText == null) throw new ArgumentNullException("strText");

			if(strPattern.IndexOf('*') < 0) return strText.Equals(strPattern, sc);

			string[] vPatternParts = strPattern.Split(m_vPatternPartsSep,
				StringSplitOptions.RemoveEmptyEntries);
			
			if(vPatternParts.Length == 0) return true;

			if(strText.Length == 0) return false;

			if(!strPattern.StartsWith(@"*") && !strText.StartsWith(vPatternParts[0], sc))
			{
				return false;
			}

			if(!strPattern.EndsWith(@"*") && !strText.EndsWith(vPatternParts[
				vPatternParts.Length - 1], sc))
			{
				return false;
			}

			int iOffset = 0;
			for(int i = 0; i < vPatternParts.Length; ++i)
			{
				string strPart = vPatternParts[i];

				int iFound = strText.IndexOf(strPart, iOffset, sc);
				if(iFound < iOffset) return false;

				iOffset = iFound + strPart.Length;
				if(iOffset == strText.Length) return (i == (vPatternParts.Length - 1));
			}

			return true;
		}
#endif // !KeePassLibSD

		public static bool StringToBool(string str)
		{
			if(string.IsNullOrEmpty(str)) return false; // No assert

			string s = str.Trim().ToLower();
			if(s == "true") return true;
			if(s == "yes") return true;
			if(s == "1") return true;
			if(s == "enabled") return true;
			if(s == "checked") return true;

			return false;
		}

		public static bool? StringToBoolEx(string str)
		{
			if(string.IsNullOrEmpty(str)) return null;

			string s = str.Trim().ToLower();
			if(s == "true") return true;
			if(s == "false") return false;

			return null;
		}

		public static string BoolToString(bool bValue)
		{
			return (bValue ? "true" : "false");
		}

		public static string BoolToStringEx(bool? bValue)
		{
			if(bValue.HasValue) return BoolToString(bValue.Value);
			return "null";
		}

		/// <summary>
		/// Normalize new line characters in a string. Input strings may
		/// contain mixed new line character sequences from all commonly
		/// used operating systems (i.e. \r\n from Windows, \n from Unix
		/// and \r from Mac OS.
		/// </summary>
		/// <param name="str">String with mixed new line characters.</param>
		/// <param name="bWindows">If <c>true</c>, new line characters
		/// are normalized for Windows (\r\n); if <c>false</c>, new line
		/// characters are normalized for Unix (\n).</param>
		/// <returns>String with normalized new line characters.</returns>
		public static string NormalizeNewLines(string str, bool bWindows)
		{
			if(string.IsNullOrEmpty(str)) return str;

			str = str.Replace("\r\n", "\n");
			str = str.Replace("\r", "\n");

			if(bWindows) str = str.Replace("\n", "\r\n");

			return str;
		}

		
        
        

		public static string AlphaNumericOnly(string str)
		{
			if(string.IsNullOrEmpty(str)) return str;

			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < str.Length; ++i)
			{
				char ch = str[i];
				if(((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z')) ||
					((ch >= '0') && (ch <= '9')))
					sb.Append(ch);
			}

			return sb.ToString();
		}

      
		
	}
}
