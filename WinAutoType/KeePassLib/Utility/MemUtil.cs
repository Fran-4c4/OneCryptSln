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
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

#if !KeePassLibSD
using System.IO.Compression;
#else
using KeePassLibSD;
#endif

namespace KeePassLib.Utility
{
	/// <summary>
	/// Contains static buffer manipulation and string conversion routines.
	/// </summary>
	public static class MemUtil
	{
		private static readonly uint[] m_vSBox = new uint[256] {
			0xCD2FACB3, 0xE78A7F5C, 0x6F0803FC, 0xBCF6E230,
			0x3A321712, 0x06403DB1, 0xD2F84B95, 0xDF22A6E4,
			0x07CE9E5B, 0x31788A0C, 0xF683F6F4, 0xEA061F49,
			0xFA5C2ACA, 0x4B9E494E, 0xB0AB25BA, 0x767731FC,
			0x261893A7, 0x2B09F2CE, 0x046261E4, 0x41367B4B,
			0x18A7F225, 0x8F923C0E, 0x5EF3A325, 0x28D0435E,
			0x84C22919, 0xED66873C, 0x8CEDE444, 0x7FC47C24,
			0xFCFC6BA3, 0x676F928D, 0xB4147187, 0xD8FB126E,
			0x7D798D17, 0xFF82E424, 0x1712FA5B, 0xABB09DD5,
			0x8156BA63, 0x84E4D969, 0xC937FB9A, 0x2F1E5BFC,
			0x178ECA11, 0x0E71CD5F, 0x52AAC6F4, 0x71EEFC8F,
			0x7090D749, 0x21CACA31, 0x92996378, 0x0939A8A8,
			0xE9EE1934, 0xD2718616, 0xF2500543, 0xB911873C,
			0xD3CB3EEC, 0x2BA0DBEB, 0xB42D0A27, 0xECE67C0F,
			0x302925F0, 0x6114F839, 0xD39E6307, 0xE28970D6,
			0xEB982F99, 0x941B4CDF, 0xC540E550, 0x8124FC45,
			0x98B025C7, 0xE2BF90EA, 0x4F57C976, 0xCF546FE4,
			0x59566DC8, 0xE3F4360D, 0xF5F9D231, 0xD6180B22,
			0xB54E088A, 0xB5DFE6A6, 0x3637A36F, 0x056E9284,
			0xAFF8FBC5, 0x19E01648, 0x8611F043, 0xDAE44337,
			0xF61B6A1C, 0x257ACD9E, 0xDD35F507, 0xEF05CAFA,
			0x05EB4A83, 0xFC25CA92, 0x0A4728E6, 0x9CF150EF,
			0xAEEF67DE, 0xA9472337, 0x57C81EFE, 0x3E5E009F,
			0x02CB03BB, 0x2BA85674, 0xF21DC251, 0x78C34A34,
			0xABB1F5BF, 0xB95A2FBD, 0x1FB47777, 0x9A96E8AC,
			0x5D2D2838, 0x55AAC92A, 0x99EE324E, 0x10F6214B,
			0x58ABDFB1, 0x2008794D, 0xBEC880F0, 0xE75E5341,
			0x88015C34, 0x352D8FBF, 0x622B7F6C, 0xF5C59EA2,
			0x1F759D8E, 0xADE56159, 0xCC7B4C25, 0x5B8BC48C,
			0xB6BD15AF, 0x3C5B5110, 0xE74A7C3D, 0xEE613161,
			0x156A1C67, 0x72C06817, 0xEA0A6F69, 0x4CECF993,
			0xCA9D554C, 0x8E20361F, 0x42D396B9, 0x595DE578,
			0x749D7955, 0xFD1BA5FD, 0x81FC160E, 0xDB97E28C,
			0x7CF148F7, 0x0B0B3CF5, 0x534DE605, 0x46421066,
			0xD4B68DD1, 0x9E479CE6, 0xAE667A9D, 0xBC082082,
			0xB06DD6EF, 0x20F0F23F, 0xB99E1551, 0xF47A2E3A,
			0x71DA50C6, 0x67B65779, 0x2A8CB376, 0x1EA71EEE,
			0x29ABCD50, 0xB6EB0C6B, 0x23C10511, 0x6F3F2144,
			0x6AF23012, 0xF696BD9E, 0xB94099D8, 0xAD5A9C81,
			0x7A0794FA, 0x7EDF59D6, 0x1E72E574, 0x8561913C,
			0x4E4D568F, 0xEECB9928, 0x9C124D2E, 0x0848B82C,
			0xF1CA395F, 0x9DAF43DC, 0xF77EC323, 0x394E9B59,
			0x7E200946, 0x8B811D68, 0x16DA3305, 0xAB8DE2C3,
			0xE6C53B64, 0x98C2D321, 0x88A97D81, 0xA7106419,
			0x8E52F7BF, 0x8ED262AF, 0x7CCA974E, 0xF0933241,
			0x040DD437, 0xE143B3D4, 0x3019F56F, 0xB741521D,
			0xF1745362, 0x4C435F9F, 0xB4214D0D, 0x0B0C348B,
			0x5051D189, 0x4C30447E, 0x7393D722, 0x95CEDD0B,
			0xDD994E80, 0xC3D22ED9, 0x739CD900, 0x131EB9C4,
			0xEF1062B2, 0x4F0DE436, 0x52920073, 0x9A7F3D80,
			0x896E7B1B, 0x2C8BBE5A, 0xBD304F8A, 0xA993E22C,
			0x134C41A0, 0xFA989E00, 0x39CE9726, 0xFB89FCCF,
			0xE8FBAC97, 0xD4063FFC, 0x935A2B5A, 0x44C8EE83,
			0xCB2BC7B6, 0x02989E92, 0x75478BEA, 0x144378D0,
			0xD853C087, 0x8897A34E, 0xDD23629D, 0xBDE2A2A2,
			0x581D8ECC, 0x5DA8AEE8, 0xFF8AAFD0, 0xBA2BCF6E,
			0x4BD98DAC, 0xF2EDB9E4, 0xFA2DC868, 0x47E84661,
			0xECEB1C7D, 0x41705CA4, 0x5982E4D4, 0xEB5204A1,
			0xD196CAFB, 0x6414804D, 0x3ABD4B46, 0x8B494C26,
			0xB432D52B, 0x39C5356B, 0x6EC80BF7, 0x71BE5483,
			0xCEC4A509, 0xE9411D61, 0x52F341E5, 0xD2E6197B,
			0x4F02826C, 0xA9E48838, 0xD1F8F247, 0xE4957FB3,
			0x586CCA99, 0x9A8B6A5B, 0x4998FBEA, 0xF762BE4C,
			0x90DFE33C, 0x9731511E, 0x88C6A82F, 0xDD65A4D4
		};

		/// <summary>
		/// Convert a byte array to a hexadecimal string.
		/// </summary>
		/// <param name="pbArray">Input byte array.</param>
		/// <returns>Returns the hexadecimal string representing the byte
		/// array. Returns <c>null</c>, if the input byte array was <c>null</c>. Returns
		/// an empty string, if the input byte array has length 0.</returns>
		public static string ByteArrayToHexString(byte[] pbArray)
		{
			if(pbArray == null) return null;

			int nLen = pbArray.Length;
			if(nLen == 0) return string.Empty;

			StringBuilder sb = new StringBuilder();

			byte bt, btHigh, btLow;
			for(int i = 0; i < nLen; ++i)
			{
				bt = pbArray[i];
				btHigh = bt; btHigh >>= 4;
				btLow = (byte)(bt & 0x0F);

				if(btHigh >= 10) sb.Append((char)('A' + btHigh - 10));
				else sb.Append((char)('0' + btHigh));

				if(btLow >= 10) sb.Append((char)('A' + btLow - 10));
				else sb.Append((char)('0' + btLow));
			}

			return sb.ToString();
		}

	/// <summary>
		/// Convert a 32-bit unsigned integer to 4 bytes using Little-Endian
		/// encoding.
		/// </summary>
		/// <param name="uValue">32-bit input word.</param>
		/// <returns>Four bytes representing the 32-bit value.</returns>
		public static byte[] UInt32ToBytes(uint uValue)
		{
			byte[] pb = new byte[4];

			unchecked
			{
				pb[0] = (byte)uValue;
				pb[1] = (byte)(uValue >> 8);
				pb[2] = (byte)(uValue >> 16);
				pb[3] = (byte)(uValue >> 24);
			}

			return pb;
		}

		/// <summary>
		/// Convert a 64-bit unsigned integer to 8 bytes using Little-Endian
		/// encoding.
		/// </summary>
		/// <param name="uValue">64-bit input word.</param>
		/// <returns>Eight bytes representing the 64-bit value.</returns>
		public static byte[] UInt64ToBytes(ulong uValue)
		{
			byte[] pb = new byte[8];

			unchecked
			{
				pb[0] = (byte)uValue;
				pb[1] = (byte)(uValue >> 8);
				pb[2] = (byte)(uValue >> 16);
				pb[3] = (byte)(uValue >> 24);
				pb[4] = (byte)(uValue >> 32);
				pb[5] = (byte)(uValue >> 40);
				pb[6] = (byte)(uValue >> 48);
				pb[7] = (byte)(uValue >> 56);
			}

			return pb;
		}

		public static bool ArraysEqual(byte[] x, byte[] y)
		{
			// Return false if one of them is null (not comparable)!
			if((x == null) || (y == null)) { Debug.Assert(false); return false; }

			if(x.Length != y.Length) return false;

			for(int i = 0; i < x.Length; ++i)
			{
				if(x[i] != y[i]) return false;
			}

			return true;
		}

	public static void CopyStream(Stream sSource, Stream sTarget)
		{
			Debug.Assert((sSource != null) && (sTarget != null));
			if(sSource == null) throw new ArgumentNullException("sSource");
			if(sTarget == null) throw new ArgumentNullException("sTarget");

			const int nBufSize = 4096;
			byte[] pbBuf = new byte[nBufSize];

			while(true)
			{
				int nRead = sSource.Read(pbBuf, 0, nBufSize);
				if(nRead == 0) break;

				sTarget.Write(pbBuf, 0, nRead);
			}

			// Do not close any of the streams
		}

		public static byte[] Read(Stream s, int nCount)
		{
			if(s == null) throw new ArgumentNullException("s");
			if(nCount < 0) throw new ArgumentOutOfRangeException("nCount");

			byte[] pb = new byte[nCount];
			int iOffset = 0;
			while(nCount > 0)
			{
				int iRead = s.Read(pb, iOffset, nCount);
				if(iRead == 0) break;

				iOffset += iRead;
				nCount -= iRead;
			}

			if(iOffset != pb.Length)
			{
				byte[] pbPart = new byte[iOffset];
				Array.Copy(pb, pbPart, iOffset);
				return pbPart;
			}

			return pb;
		}

		public static void Write(Stream s, byte[] pbData)
		{
			if(s == null) { Debug.Assert(false); return; }
			if(pbData == null) { Debug.Assert(false); return; }

			s.Write(pbData, 0, pbData.Length);
		}

		public static byte[] Compress(byte[] pbData)
		{
			if(pbData == null) throw new ArgumentNullException("pbData");
			if(pbData.Length == 0) return pbData;

			MemoryStream msCompressed = new MemoryStream();
			GZipStream gz = new GZipStream(msCompressed, CompressionMode.Compress);
			MemoryStream msSource = new MemoryStream(pbData, false);
			MemUtil.CopyStream(msSource, gz);
			gz.Close();
			msSource.Close();

			byte[] pbCompressed = msCompressed.ToArray();
			msCompressed.Close();
			return pbCompressed;
		}

		public static byte[] Decompress(byte[] pbCompressed)
		{
			if(pbCompressed == null) throw new ArgumentNullException("pbCompressed");
			if(pbCompressed.Length == 0) return pbCompressed;

			MemoryStream msCompressed = new MemoryStream(pbCompressed, false);
			GZipStream gz = new GZipStream(msCompressed, CompressionMode.Decompress);
			MemoryStream msData = new MemoryStream();
			MemUtil.CopyStream(gz, msData);
			gz.Close();
			msCompressed.Close();

			byte[] pbData = msData.ToArray();
			msData.Close();
			return pbData;
		}

        
	}
}
