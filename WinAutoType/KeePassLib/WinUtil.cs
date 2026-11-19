using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using KeePassLib.Utility;

namespace KeePass.Util
{
    public static class WinUtil
    {


        private static string m_strExePath = null;

        public static bool IsAtLeastWindows2000 = false;


        public static string GetExecutable()
        {
            if (m_strExePath != null) return m_strExePath;

            try { m_strExePath = Assembly.GetExecutingAssembly().Location; }
            catch (Exception) { }

            if (string.IsNullOrEmpty(m_strExePath))
            {
                m_strExePath = Assembly.GetExecutingAssembly().GetName().CodeBase;
                m_strExePath = UrlUtil.FileUrlToPath(m_strExePath);
            }

            return m_strExePath;
        }
    }
}
