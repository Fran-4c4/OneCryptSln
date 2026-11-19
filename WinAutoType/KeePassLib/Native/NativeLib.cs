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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Diagnostics;

using KeePassLib.Utility;

namespace KeePassLib.Native
{
	/// <summary>
	/// Interface to native library (library containing fast versions of
	/// several cryptographic functions).
	/// </summary>
	public static class NativeLib
	{
		

		private static bool? m_bIsUnix = null;
		public static bool IsUnix()
		{
			if(m_bIsUnix.HasValue) return m_bIsUnix.Value;

			PlatformID p = GetPlatformID();

			// Mono defines Unix as 128 in early .NET versions
#if !KeePassLibSD
			m_bIsUnix = ((p == PlatformID.Unix) || (p == PlatformID.MacOSX) ||
				((int)p == 128));
#else
			m_bIsUnix = (((int)p == 4) || ((int)p == 6) || ((int)p == 128));
#endif
			return m_bIsUnix.Value;
		}

		private static PlatformID? m_platID = null;
		public static PlatformID GetPlatformID()
		{
			if(m_platID.HasValue) return m_platID.Value;

#if KeePassRT
			m_platID = PlatformID.Win32NT;
#else
			m_platID = Environment.OSVersion.Platform;
#endif

//#if (!KeePassLibSD && !KeePassRT)
//            // Mono returns PlatformID.Unix on Mac OS X, workaround this
//            if(m_platID.Value == PlatformID.Unix)
//            {
//                if((RunConsoleApp("uname", null) ?? string.Empty).Trim().Equals(
//                    "Darwin", StrUtil.CaseIgnoreCmp))
//                    m_platID = PlatformID.MacOSX;
//            }
//#endif

			return m_platID.Value;
		}

#if (!KeePassLibSD && !KeePassRT)
        public static string RunConsoleApp(string strAppPath, string strParams)
        {
            return RunConsoleApp(strAppPath, strParams, null);
        }

        public static string RunConsoleApp(string strAppPath, string strParams,
            string strStdInput)
        {
            return RunConsoleApp(strAppPath, strParams, strStdInput,
                (AppRunFlags.GetStdOutput | AppRunFlags.WaitForExit));
        }

		private delegate string RunProcessDelegate();

        public static string RunConsoleApp(string strAppPath, string strParams,
            string strStdInput, AppRunFlags f)
        {
            if (strAppPath == null) throw new ArgumentNullException("strAppPath");
            if (strAppPath.Length == 0) throw new ArgumentException("strAppPath");

            bool bStdOut = ((f & AppRunFlags.GetStdOutput) != AppRunFlags.None);

            RunProcessDelegate fnRun = delegate()
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();

                    psi.CreateNoWindow = true;
                    psi.FileName = strAppPath;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.UseShellExecute = false;
                    psi.RedirectStandardOutput = bStdOut;

                    if (strStdInput != null) psi.RedirectStandardInput = true;

                    if (!string.IsNullOrEmpty(strParams)) psi.Arguments = strParams;

                    Process p = Process.Start(psi);

                    if (strStdInput != null)
                    {
                        // Workaround for Mono Process StdIn BOM bug;
                        // https://sourceforge.net/p/keepass/bugs/1219/
                        EnsureNoBom(p.StandardInput);

                        p.StandardInput.Write(strStdInput);
                        p.StandardInput.Close();
                    }

                    string strOutput = string.Empty;
                    if (bStdOut) strOutput = p.StandardOutput.ReadToEnd();

                    if ((f & AppRunFlags.WaitForExit) != AppRunFlags.None)
                        p.WaitForExit();
                    else if ((f & AppRunFlags.GCKeepAlive) != AppRunFlags.None)
                    {
                        Thread th = new Thread(delegate()
                        {
                            try { p.WaitForExit(); }
                            catch (Exception) { Debug.Assert(false); }
                        });
                        th.Start();
                    }

                    return strOutput;
                }
                catch (Exception) { Debug.Assert(false); }

                return null;
            };

            if ((f & AppRunFlags.DoEvents) != AppRunFlags.None)
            {
                List<Form> lDisabledForms = new List<Form>();
                if ((f & AppRunFlags.DisableForms) != AppRunFlags.None)
                {
                    foreach (Form form in Application.OpenForms)
                    {
                        if (!form.Enabled) continue;

                        lDisabledForms.Add(form);
                        form.Enabled = false;
                    }
                }

                IAsyncResult ar = fnRun.BeginInvoke(null, null);

                while (!ar.AsyncWaitHandle.WaitOne(0))
                {
                    Application.DoEvents();
                    Thread.Sleep(2);
                }

                string strRet = fnRun.EndInvoke(ar);

                for (int i = lDisabledForms.Count - 1; i >= 0; --i)
                    lDisabledForms[i].Enabled = true;

                return strRet;
            }

            return fnRun();
        }

		private static void EnsureNoBom(StreamWriter sw)
		{
			if(sw == null) { Debug.Assert(false); return; }
			if(!NativeLib.IsUnix()) return;

			try
			{
				Encoding enc = sw.Encoding;
				
				byte[] pbBom = enc.GetPreamble();
				if((pbBom == null) || (pbBom.Length == 0)) return;

				FieldInfo fi = typeof(StreamWriter).GetField("preamble_done",
					BindingFlags.Instance | BindingFlags.NonPublic);
				if(fi != null) fi.SetValue(sw, true);
			}
			catch(Exception) { Debug.Assert(false); }
		}
#endif
	}
}
