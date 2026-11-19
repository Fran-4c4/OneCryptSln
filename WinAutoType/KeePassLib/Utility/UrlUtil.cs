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
using System.IO;
using System.Text;
using System.Diagnostics;


namespace KeePassLib.Utility
{
	/// <summary>
	/// A class containing various static path utility helper methods (like
	/// stripping extension from a file, etc.).
	/// </summary>
	public static class UrlUtil
	{
		private static readonly char[] m_vDirSeps = new char[] {
			'\\', '/', UrlUtil.LocalDirSepChar };
		private static readonly char[] m_vPathTrimCharsWs = new char[] {
			'\"', ' ', '\t', '\r', '\n' };

		public static char LocalDirSepChar
		{
#if KeePassRT
			get { return '\\'; }
#else
			get { return Path.DirectorySeparatorChar; }
#endif
		}

		/// <summary>
		/// Get the directory (path) of a file name. The returned string may be
		/// terminated by a directory separator character. Example:
		/// passing <c>C:\\My Documents\\My File.kdb</c> in <paramref name="strFile" />
		/// and <c>true</c> to <paramref name="bAppendTerminatingChar"/>
		/// would produce this string: <c>C:\\My Documents\\</c>.
		/// </summary>
		/// <param name="strFile">Full path of a file.</param>
		/// <param name="bAppendTerminatingChar">Append a terminating directory separator
		/// character to the returned path.</param>
		/// <param name="bEnsureValidDirSpec">If <c>true</c>, the returned path
		/// is guaranteed to be a valid directory path (for example <c>X:\\</c> instead
		/// of <c>X:</c>, overriding <paramref name="bAppendTerminatingChar" />).
		/// This should only be set to <c>true</c>, if the returned path is directly
		/// passed to some directory API.</param>
		/// <returns>Directory of the file.</returns>
		public static string GetFileDirectory(string strFile, bool bAppendTerminatingChar,
			bool bEnsureValidDirSpec)
		{
			Debug.Assert(strFile != null);
			if(strFile == null) throw new ArgumentNullException("strFile");

			int nLastSep = strFile.LastIndexOfAny(m_vDirSeps);
			if(nLastSep < 0) return string.Empty; // No directory

			if(bEnsureValidDirSpec && (nLastSep == 2) && (strFile[1] == ':') &&
				(strFile[2] == '\\')) // Length >= 3 and Windows root directory
				bAppendTerminatingChar = true;

			if(!bAppendTerminatingChar) return strFile.Substring(0, nLastSep);
			return EnsureTerminatingSeparator(strFile.Substring(0, nLastSep),
				(strFile[nLastSep] == '/'));
		}

		/// <summary>
		/// Gets the file name of the specified file (full path). Example:
		/// if <paramref name="strPath" /> is <c>C:\\My Documents\\My File.kdb</c>
		/// the returned string is <c>My File.kdb</c>.
		/// </summary>
		/// <param name="strPath">Full path of a file.</param>
		/// <returns>File name of the specified file. The return value is
		/// an empty string (<c>""</c>) if the input parameter is <c>null</c>.</returns>
		public static string GetFileName(string strPath)
		{
			Debug.Assert(strPath != null); if(strPath == null) throw new ArgumentNullException("strPath");

			int nLastSep = strPath.LastIndexOfAny(m_vDirSeps);

			if(nLastSep < 0) return strPath;
			if(nLastSep >= (strPath.Length - 1)) return string.Empty;

			return strPath.Substring(nLastSep + 1);
		}

		/// <summary>
		/// Strip the extension of a file.
		/// </summary>
		/// <param name="strPath">Full path of a file with extension.</param>
		/// <returns>File name without extension.</returns>
		public static string StripExtension(string strPath)
		{
			Debug.Assert(strPath != null); if(strPath == null) throw new ArgumentNullException("strPath");

			int nLastDirSep = strPath.LastIndexOfAny(m_vDirSeps);
			int nLastExtDot = strPath.LastIndexOf('.');

			if(nLastExtDot <= nLastDirSep) return strPath;

			return strPath.Substring(0, nLastExtDot);
		}

		/// <summary>
		/// Get the extension of a file.
		/// </summary>
		/// <param name="strPath">Full path of a file with extension.</param>
		/// <returns>Extension without prepending dot.</returns>
		public static string GetExtension(string strPath)
		{
			Debug.Assert(strPath != null); if(strPath == null) throw new ArgumentNullException("strPath");

			int nLastDirSep = strPath.LastIndexOfAny(m_vDirSeps);
			int nLastExtDot = strPath.LastIndexOf('.');

			if(nLastExtDot <= nLastDirSep) return string.Empty;
			if(nLastExtDot == (strPath.Length - 1)) return string.Empty;

			return strPath.Substring(nLastExtDot + 1);
		}

		/// <summary>
		/// Ensure that a path is terminated with a directory separator character.
		/// </summary>
		/// <param name="strPath">Input path.</param>
		/// <param name="bUrl">If <c>true</c>, a slash (<c>/</c>) is appended to
		/// the string if it's not terminated already. If <c>false</c>, the
		/// default system directory separator character is used.</param>
		/// <returns>Path having a directory separator as last character.</returns>
		public static string EnsureTerminatingSeparator(string strPath, bool bUrl)
		{
			Debug.Assert(strPath != null); if(strPath == null) throw new ArgumentNullException("strPath");

			int nLength = strPath.Length;
			if(nLength <= 0) return string.Empty;

			char chLast = strPath[nLength - 1];

			for(int i = 0; i < m_vDirSeps.Length; ++i)
			{
				if(chLast == m_vDirSeps[i]) return strPath;
			}

			if(bUrl) return (strPath + '/');
			return (strPath + UrlUtil.LocalDirSepChar);
		}

	
		public static string GetQuotedAppPath(string strPath)
		{
			if(strPath == null) { Debug.Assert(false); return string.Empty; }

			string str = strPath.Trim();
			if(str.Length <= 1) return str;
			if(str[0] != '\"') return str;

			int iSecond = str.IndexOf('\"', 1);
			if(iSecond <= 0) return str;

			return str.Substring(1, iSecond - 1);
		}

		public static string FileUrlToPath(string strUrl)
		{
			Debug.Assert(strUrl != null);
			if(strUrl == null) throw new ArgumentNullException("strUrl");

			string str = strUrl;
			if(str.StartsWith(@"file:///", StrUtil.CaseIgnoreCmp))
				str = str.Substring(8, str.Length - 8);

			str = str.Replace('/', UrlUtil.LocalDirSepChar);

			return str;
		}

		public static bool UnhideFile(string strFile)
		{
#if (KeePassLibSD || KeePassRT)
			return false;
#else
			if(strFile == null) throw new ArgumentNullException("strFile");

			try
			{
				FileAttributes fa = File.GetAttributes(strFile);
				if((long)(fa & FileAttributes.Hidden) == 0) return false;

				return HideFile(strFile, false);
			}
			catch(Exception) { }

			return false;
#endif
		}

		public static bool HideFile(string strFile, bool bHide)
		{
#if (KeePassLibSD || KeePassRT)
			return false;
#else
			if(strFile == null) throw new ArgumentNullException("strFile");

			try
			{
				FileAttributes fa = File.GetAttributes(strFile);

				if(bHide) fa = ((fa & ~FileAttributes.Normal) | FileAttributes.Hidden);
				else // Unhide
				{
					fa &= ~FileAttributes.Hidden;
					if((long)fa == 0) fa |= FileAttributes.Normal;
				}

				File.SetAttributes(strFile, fa);
				return true;
			}
			catch(Exception) { }

			return false;
#endif
		}

		public static string RemoveScheme(string strUrl)
		{
			if(string.IsNullOrEmpty(strUrl)) return string.Empty;

			int nNetScheme = strUrl.IndexOf(@"://", StrUtil.CaseIgnoreCmp);
			int nShScheme = strUrl.IndexOf(@":/", StrUtil.CaseIgnoreCmp);
			int nSmpScheme = strUrl.IndexOf(@":", StrUtil.CaseIgnoreCmp);

			if((nNetScheme < 0) && (nShScheme < 0) && (nSmpScheme < 0))
				return strUrl; // No scheme

			int nMin = Math.Min(Math.Min((nNetScheme >= 0) ? nNetScheme : int.MaxValue,
				(nShScheme >= 0) ? nShScheme : int.MaxValue),
				(nSmpScheme >= 0) ? nSmpScheme : int.MaxValue);

			if(nMin == nNetScheme) return strUrl.Substring(nMin + 3);
			if(nMin == nShScheme) return strUrl.Substring(nMin + 2);
			return strUrl.Substring(nMin + 1);
		}

		public static string ConvertSeparators(string strPath)
		{
			return ConvertSeparators(strPath, UrlUtil.LocalDirSepChar);
		}

		public static string ConvertSeparators(string strPath, char chSeparator)
		{
			if(string.IsNullOrEmpty(strPath)) return string.Empty;

			strPath = strPath.Replace('/', chSeparator);
			strPath = strPath.Replace('\\', chSeparator);

			return strPath;
		}

		public static bool IsUncPath(string strPath)
		{
			if(strPath == null) throw new ArgumentNullException("strPath");

			return (strPath.StartsWith("\\\\") || strPath.StartsWith("//"));
		}

		public static string FilterFileName(string strName)
		{
			if(strName == null) { Debug.Assert(false); return string.Empty; }

			string str = strName;

			str = str.Replace('/', '-');
			str = str.Replace('\\', '-');
			str = str.Replace(":", string.Empty);
			str = str.Replace("*", string.Empty);
			str = str.Replace("?", string.Empty);
			str = str.Replace("\"", string.Empty);
			str = str.Replace(@"'", string.Empty);
			str = str.Replace('<', '(');
			str = str.Replace('>', ')');
			str = str.Replace('|', '-');

			return str;
		}

		/// <summary>
		/// Get the host component of an URL.
		/// This method is faster and more fault-tolerant than creating
		/// an <code>Uri</code> object and querying its <code>Host</code>
		/// property.
		/// </summary>
		/// <example>
		/// For the input <code>s://u:p@d.tld:p/p?q#f</code> the return
		/// value is <code>d.tld</code>.
		/// </example>
		public static string GetHost(string strUrl)
		{
			if(strUrl == null) { Debug.Assert(false); return string.Empty; }

			StringBuilder sb = new StringBuilder();
			bool bInExtHost = false;
			for(int i = 0; i < strUrl.Length; ++i)
			{
				char ch = strUrl[i];
				if(bInExtHost)
				{
					if(ch == '/')
					{
						if(sb.Length == 0) { } // Ignore leading '/'s
						else break;
					}
					else sb.Append(ch);
				}
				else // !bInExtHost
				{
					if(ch == ':') bInExtHost = true;
				}
			}

			string str = sb.ToString();
			if(str.Length == 0) str = strUrl;

			// Remove the login part
			int nLoginLen = str.IndexOf('@');
			if(nLoginLen >= 0) str = str.Substring(nLoginLen + 1);

			// Remove the port
			int iPort = str.LastIndexOf(':');
			if(iPort >= 0) str = str.Substring(0, iPort);

			return str;
		}


#if !KeePassLibSD
		// Structurally mostly equivalent to UrlUtil.GetFileInfos
		public static List<string> GetFilePaths(string strDir, string strPattern,
			SearchOption opt)
		{
			List<string> l = new List<string>();
			if(strDir == null) { Debug.Assert(false); return l; }
			if(strPattern == null) { Debug.Assert(false); return l; }

			string[] v = Directory.GetFiles(strDir, strPattern, opt);
			if(v == null) { Debug.Assert(false); return l; }

			// Only accept files with the correct extension; GetFiles may
			// return additional files, see GetFiles documentation
			string strExt = GetExtension(strPattern);
			if(!string.IsNullOrEmpty(strExt) && (strExt.IndexOf('*') < 0) &&
				(strExt.IndexOf('?') < 0))
			{
				strExt = "." + strExt;

				foreach(string strPathRaw in v)
				{
					if(strPathRaw == null) { Debug.Assert(false); continue; }
					string strPath = strPathRaw.Trim(m_vPathTrimCharsWs);
					if(strPath.Length == 0) { Debug.Assert(false); continue; }
					Debug.Assert(strPath == strPathRaw);

					if(!strPath.EndsWith(strExt, StrUtil.CaseIgnoreCmp))
						continue;

					l.Add(strPathRaw);
				}
			}
			else l.AddRange(v);

			return l;
		}

		// Structurally mostly equivalent to UrlUtil.GetFilePaths
		public static List<FileInfo> GetFileInfos(DirectoryInfo di, string strPattern,
			SearchOption opt)
		{
			List<FileInfo> l = new List<FileInfo>();
			if(di == null) { Debug.Assert(false); return l; }
			if(strPattern == null) { Debug.Assert(false); return l; }

			FileInfo[] v = di.GetFiles(strPattern, opt);
			
			// Only accept files with the correct extension; GetFiles may
			// return additional files, see GetFiles documentation
			string strExt = GetExtension(strPattern);
			if(!string.IsNullOrEmpty(strExt) && (strExt.IndexOf('*') < 0) &&
				(strExt.IndexOf('?') < 0))
			{
				strExt = "." + strExt;

				foreach(FileInfo fi in v)
				{
					if(fi == null) { Debug.Assert(false); continue; }
					string strPathRaw = fi.FullName;
					
					string strPath = strPathRaw.Trim(m_vPathTrimCharsWs);
					if(strPath.Length == 0) { Debug.Assert(false); continue; }
					Debug.Assert(strPath == strPathRaw);

					if(!strPath.EndsWith(strExt, StrUtil.CaseIgnoreCmp))
						continue;

					l.Add(fi);
				}
			}
			else l.AddRange(v);

			return l;
		}
#endif
	}
}
