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
using System.Diagnostics;

using KeePassLib.Cryptography;
using KeePassLib.Utility;

#if KeePassLibSD
using KeePassLibSD;
#endif

// SecureString objects are limited to 65536 characters, don't use

namespace KeePassLib.Security
{
	/// <summary>
	/// Represents an in-memory encrypted string.
	/// <c>ProtectedString</c> objects are immutable and thread-safe.
	/// </summary>
#if (DEBUG && !KeePassLibSD)
	[DebuggerDisplay(@"{ReadString()}")]
#endif
	public sealed class ProtectedString
	{

		private string m_strPlainText = null;


		private static readonly ProtectedString m_psEmpty = new ProtectedString();
		public static ProtectedString Empty
		{
			get { return m_psEmpty; }
		}
		public bool IsEmpty
		{
			get
			{
				return (m_strPlainText.Length == 0);
			}
		}

		
		
		/// <summary>
		/// Construct a new protected string object. Protection is
		/// disabled.
		/// </summary>
		public ProtectedString()
		{
			
		}

		public ProtectedString(string str)
		{
			m_strPlainText = str;
		}

		private void Init(string str)
		{
			// The string already is in memory and immutable,
			// protection would be useless
			m_strPlainText = str;
		}

		/// <summary>
		/// Convert the protected string to a normal string object.
		/// Be careful with this function, the returned string object
		/// isn't protected anymore and stored in plain-text in the
		/// process memory.
		/// </summary>
		/// <returns>Plain-text string. Is never <c>null</c>.</returns>
		public string ReadString()
		{
			 return m_strPlainText;
		}
  
	}
}
