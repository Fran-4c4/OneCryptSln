using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;  
using KeePassLib.Collections;

namespace KeePassLib
{
    public class PwEntryObj
    {
        private bool _autoTypeEnabled = true;
        public bool AutoTypeEnabled
        {
            get { return _autoTypeEnabled; }
            set { _autoTypeEnabled = value; }
        }

        public bool GetAutoTypeEnabled()
        {
            return _autoTypeEnabled;
        }

        //private AutoTypeConfig m_listAutoType = new AutoTypeConfig();
        ///// <summary>
        ///// Get or set all auto-type window/keystroke sequence associations.
        ///// </summary>
        //public AutoTypeConfig AutoType
        //{
        //    get { return m_listAutoType; }
        //    set
        //    {
        //        m_listAutoType = value;
        //    }
        //}

        private string _autoTypeSequence="{USERNAME}{TAB}{PASSWORD}{ENTER}";
        public string AutoTypeSequence
        {
            get { return _autoTypeSequence; }
            set { _autoTypeSequence = value; }
        }

        public string GetAutoTypeSequence()
        {
            return _autoTypeSequence;

        }

        private ProtectedStringDictionary m_listStrings = new ProtectedStringDictionary();
        /// <summary>
        /// Get or set all entry strings.
        /// </summary>
        public ProtectedStringDictionary Strings
        {
            get { return m_listStrings; }
            set
            {
                m_listStrings = value;
            }
        }


    }


}
