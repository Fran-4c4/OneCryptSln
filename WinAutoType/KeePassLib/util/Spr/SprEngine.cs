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
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using KeePassLib;
using KeePassLib.Security;
using KeePassLib.Utility;
//using KeePass.App.Configuration;

namespace KeePass.Util.Spr
{
    /// <summary>
    ///     String placeholders and field reference replacement engine.
    /// </summary>
    public static partial class SprEngine
    {
        private const uint MaxRecursionDepth = 12;
        private const StringComparison ScMethod = StringComparison.OrdinalIgnoreCase;
        private const string StrRemStart = @"{C:";
        private const string StrRemEnd = @"}";
        internal const string StrRefStart = @"{REF:";
        internal const string StrRefEnd = @"}";
        private static string m_strAppExePath = string.Empty;
        private static readonly List<string> m_lFilterPlh = new List<string>();
        // See the events above
        public static List<string> FilterPlaceholderHints
        {
            get { return m_lFilterPlh; }
        }

        // private static readonly char[] m_vPlhEscapes = new char[] { '{', '}', '%' };

        // Important notes for plugin developers subscribing to the following events:
        // * If possible, prefer subscribing to FilterCompile instead of
        //   FilterCompilePre.
        // * If your plugin provides an active transformation (e.g. replacing a
        //   placeholder that changes some state or requires UI interaction), you
        //   must only perform the transformation if the ExtActive bit is set in
        //   args.Context.Flags of the event arguments object args provided to the
        //   event handler.
        // * Non-active transformations should only be performed if the ExtNonActive
        //   bit is set in args.Context.Flags.
        // * If your plugin provides a placeholder (like e.g. {EXAMPLE}), you
        //   should add this placeholder to the FilterPlaceholderHints list
        //   (e.g. add the string "{EXAMPLE}"). Please remove your strings from
        //   the list when your plugin is terminated.
        public static event EventHandler<SprEventArgs> FilterCompilePre;
        public static event EventHandler<SprEventArgs> FilterCompile;

        private static void InitializeStatic()
        {
            m_strAppExePath = WinUtil.GetExecutable();
        }

        //[Obsolete]
        //public static string Compile(string strText, bool bIsAutoTypeSequence,
        //    PwEntryObj pwEntry, PwDatabase pwDatabase, bool bEscapeForAutoType,
        //    bool bEscapeQuotesForCommandLine)
        //{
        //    SprContext ctx = new SprContext(pwEntry, SprCompileFlags.All,
        //        bEscapeForAutoType, bEscapeQuotesForCommandLine);
        //    return Compile(strText, ctx);
        //}

        public static string Compile(string strText, SprContext ctx)
        {
            //rellenar la cadena con la clave y usuario 
            if (strText == null)
            {
                Debug.Assert(false);
                return string.Empty;
            }
            if (strText.Length == 0) return string.Empty;

            InitializeStatic();

            if (ctx == null) ctx = new SprContext();
            ctx.RefsCache.Clear();

            var str = CompileInternal(strText, ctx, 0);

            // if(bEscapeForAutoType && !bIsAutoTypeSequence)
            //	str = SprEncoding.MakeAutoTypeSequence(str);

            return str;
        }

        private static string CompileInternal(string strText, SprContext ctx,
            uint uRecursionLevel)
        {
            if (strText == null)
            {
                Debug.Assert(false);
                return string.Empty;
            }
            if (ctx == null)
            {
                Debug.Assert(false);
                ctx = new SprContext();
            }

            if (uRecursionLevel >= MaxRecursionDepth)
            {
                Debug.Assert(false); // Most likely a recursive reference
                return string.Empty; // Do not return strText (endless loop)
            }

            var str = strText;

            var bExt = ((ctx.Flags & (SprCompileFlags.ExtActive |
                                      SprCompileFlags.ExtNonActive)) != SprCompileFlags.None);
            if (bExt && (FilterCompilePre != null))
            {
                var args = new SprEventArgs(str, ctx.Clone());
                FilterCompilePre(null, args);
                str = args.Text;
            }

            if ((ctx.Flags & SprCompileFlags.Comments) != SprCompileFlags.None)
                str = RemoveComments(str);

            if ((ctx.Flags & SprCompileFlags.TextTransforms) != SprCompileFlags.None)
                str = PerformTextTransforms(str, ctx, uRecursionLevel);

            //if((ctx.Flags & SprCompileFlags.AppPaths) != SprCompileFlags.None)
            //    str = AppLocator.FillPlaceholders(str, ctx);

            if (ctx.Entry != null)
            {
                //if ((ctx.Flags & SprCompileFlags.PickChars) != SprCompileFlags.None)
                //    str = ReplacePickPw(str, ctx, uRecursionLevel);

                if ((ctx.Flags & SprCompileFlags.EntryStrings) != SprCompileFlags.None)
                    str = FillEntryStrings(str, ctx, uRecursionLevel);

                //if((ctx.Flags & SprCompileFlags.EntryStringsSpecial) != SprCompileFlags.None)
                //{
                //    // ctx.UrlRemoveSchemeOnce = true;
                //    // str = SprEngine.FillIfExists(str, @"{URL:RMVSCM}",
                //    //	ctx.Entry.Strings.GetSafe(PwDefs.UrlField), ctx, uRecursionLevel);
                //    // Debug.Assert(!ctx.UrlRemoveSchemeOnce);

                //    str = FillEntryStringsSpecial(str, ctx, uRecursionLevel);
                //}

                //if(((ctx.Flags & SprCompileFlags.PasswordEnc) != SprCompileFlags.None) &&
                //    (str.IndexOf(@"{PASSWORD_ENC}", SprEngine.ScMethod) >= 0))
                //{
                //    string strPwCmp = SprEngine.FillIfExists(@"{PASSWORD}",
                //        @"{PASSWORD}", ctx.Entry.Strings.GetSafe(PwDefs.PasswordField),
                //        ctx.WithoutContentTransformations(), uRecursionLevel);

                //    str = SprEngine.FillPlaceholder(str, @"{PASSWORD_ENC}",
                //        StrUtil.EncryptString(strPwCmp), ctx);
                //}

                //if(((ctx.Flags & SprCompileFlags.Group) != SprCompileFlags.None) &&
                //    (ctx.Entry.ParentGroup != null))
                //{
                //    str = SprEngine.FillIfExists(str, @"{GROUP}", new ProtectedString(
                //        false, ctx.Entry.ParentGroup.Name), ctx, uRecursionLevel);

                //    str = SprEngine.FillIfExists(str, @"{GROUPPATH}", new ProtectedString(
                //        false, ctx.Entry.ParentGroup.GetFullPath()), ctx, uRecursionLevel);
                //}
            }

            //if((ctx.Flags & SprCompileFlags.Paths) != SprCompileFlags.None)
            //    str = SprEngine.FillIfExists(str, @"{APPDIR}", new ProtectedString(
            //        false, UrlUtil.GetFileDirectory(m_strAppExePath, false, false)),
            //        ctx, uRecursionLevel);


            //if((ctx.Flags & SprCompileFlags.Paths) != SprCompileFlags.None)
            //{
            //    str = SprEngine.FillIfExists(str, @"{ENV_DIRSEP}", new ProtectedString(
            //        false, Path.DirectorySeparatorChar.ToString()), ctx, uRecursionLevel);

            //    string strPF86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            //    if(string.IsNullOrEmpty(strPF86))
            //        strPF86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            //    if(strPF86 != null)
            //        str = SprEngine.FillIfExists(str, @"{ENV_PROGRAMFILES_X86}",
            //            new ProtectedString(false, strPF86), ctx, uRecursionLevel);
            //    else { Debug.Assert(false); }
            //}

            if ((ctx.Flags & SprCompileFlags.AutoType) != SprCompileFlags.None)
            {
                // Use Bksp instead of Del (in order to avoid Ctrl+Alt+Del);
                // https://sourceforge.net/p/keepass/discussion/329220/thread/4f1aa6b8/
                str = StrUtil.ReplaceCaseInsensitive(str, @"{CLEARFIELD}",
                    @"{HOME}+({END}){BKSP}{DELAY 50}");
            }

            if ((ctx.Flags & SprCompileFlags.References) != SprCompileFlags.None)
                str = FillRefPlaceholders(str, ctx, uRecursionLevel);

            //if(((ctx.Flags & SprCompileFlags.EnvVars) != SprCompileFlags.None) &&
            //    (str.IndexOf('%') >= 0))
            //{
            //    // Replace environment variables
            //    foreach(DictionaryEntry de in Environment.GetEnvironmentVariables())
            //    {
            //        string strKey = (de.Key as string);
            //        string strValue = (de.Value as string);

            //        if((strKey != null) && (strValue != null))
            //            str = SprEngine.FillIfExists(str, @"%" + strKey + @"%",
            //                new ProtectedString(false, strValue), ctx, uRecursionLevel);
            //        else { Debug.Assert(false); }
            //    }
            //}

            if ((ctx.Flags & SprCompileFlags.Env) != SprCompileFlags.None)
                str = FillUriSpecial(str, ctx, @"{BASE", (ctx.Base ?? string.Empty),
                    ctx.BaseIsEncoded, uRecursionLevel);


            if (bExt && (FilterCompile != null))
            {
                var args = new SprEventArgs(str, ctx.Clone());
                FilterCompile(null, args);
                str = args.Text;
            }

            if (ctx.EncodeAsAutoTypeSequence)
            {
                str = StrUtil.NormalizeNewLines(str, false);
                str = str.Replace("\n", @"{ENTER}");
            }

            return str;
        }

        private static string FillIfExists(string strData, string strPlaceholder, ProtectedString psParsable,
            SprContext ctx, uint uRecursionLevel)
        {
            // // The UrlRemoveSchemeOnce property of ctx must be cleared
            // // before this method returns and before any recursive call
            // bool bRemoveScheme = false;
            // if(ctx != null)
            // {
            //	bRemoveScheme = ctx.UrlRemoveSchemeOnce;
            //	ctx.UrlRemoveSchemeOnce = false;
            // }

            if (strData == null)
            {
                Debug.Assert(false);
                return string.Empty;
            }
            if (strPlaceholder == null)
            {
                Debug.Assert(false);
                return strData;
            }
            if (strPlaceholder.Length == 0)
            {
                Debug.Assert(false);
                return strData;
            }

            if (strData.IndexOf(strPlaceholder, ScMethod) >= 0)
            {
                var strReplacement = CompileInternal(
                    psParsable.ReadString(), ctx.WithoutContentTransformations(),
                    uRecursionLevel + 1);

                // if(bRemoveScheme)
                //	strReplacement = UrlUtil.RemoveScheme(strReplacement);

                return FillPlaceholder(strData, strPlaceholder,
                    strReplacement, ctx);
            }

            return strData;
        }

        private static string FillPlaceholder(string strData, string strPlaceholder,
            string strReplaceWith, SprContext ctx)
        {
            if (strData == null)
            {
                Debug.Assert(false);
                return string.Empty;
            }
            if (strPlaceholder == null)
            {
                Debug.Assert(false);
                return strData;
            }
            if (strPlaceholder.Length == 0)
            {
                Debug.Assert(false);
                return strData;
            }
            if (strReplaceWith == null)
            {
                Debug.Assert(false);
                return strData;
            }

            return StrUtil.ReplaceCaseInsensitive(strData, strPlaceholder,
                TransformContent(strReplaceWith, ctx));
        }

        public static string TransformContent(string strContent, SprContext ctx)
        {
            if (strContent == null)
            {
                Debug.Assert(false);
                return string.Empty;
            }

            var str = strContent;

            if (ctx != null)
            {
                if (ctx.EncodeQuotesForCommandLine)
                    str = SprEncoding.MakeCommandQuotes(str);

                if (ctx.EncodeAsAutoTypeSequence)
                    str = SprEncoding.MakeAutoTypeSequence(str);
            }

            return str;
        }

        private static string FillEntryStrings(string str, SprContext ctx,
            uint uRecursionLevel)
        {
            var vKeys = ctx.Entry.Strings.GetKeys();

            // Ensure that all standard field names are in the list
            // (this is required in order to replace the standard placeholders
            // even if the corresponding standard field isn't present in
            // the entry)
            var vStdNames = PwDefs.GetStandardFields();
            foreach (var strStdField in vStdNames)
            {
                if (!vKeys.Contains(strStdField)) vKeys.Add(strStdField);
            }

            // Do not directly enumerate the strings in ctx.Entry.Strings,
            // because strings might change during the Spr compilation
            foreach (var strField in vKeys)
            {
                var strKey = (PwDefs.IsStandardField(strField)
                    ? (@"{" + strField + @"}")
                    : (@"{" + PwDefs.AutoTypeStringPrefix + strField + @"}"));

                //if (!ctx.ForcePlainTextPasswords && strKey.Equals(@"{" +
                //    PwDefs.PasswordField + @"}", StrUtil.CaseIgnoreCmp) &&
                //    Program.Config.MainWindow.IsColumnHidden(AceColumnType.Password))
                //{
                //    str = SprEngine.FillIfExists(str, strKey, new ProtectedString(
                //        false, PwDefs.HiddenPassword), ctx, uRecursionLevel);
                //    continue;
                //}

                // Use GetSafe because the field doesn't necessarily exist
                // (might be a standard field that has been added above)
                str = FillIfExists(str, strKey, ctx.Entry.Strings.GetSafe(
                    strField), ctx, uRecursionLevel);
            }

            return str;
        }

        private static string FillEntryStringsSpecial(string str, SprContext ctx,
            uint uRecursionLevel)
        {
            return FillUriSpecial(str, ctx, @"{URL", ctx.Entry.Strings.ReadSafe(
                PwDefs.UrlField), false, uRecursionLevel);
        }

        private static string FillUriSpecial(string strText, SprContext ctx,
            string strPlhInit, string strData, bool bDataIsEncoded,
            uint uRecursionLevel)
        {
            Debug.Assert(strPlhInit.StartsWith(@"{") && !strPlhInit.EndsWith(@"}"));
            Debug.Assert(strData != null);

            string[] vPlhs =
            {
                strPlhInit + @"}",
                strPlhInit + @":RMVSCM}",
                strPlhInit + @":SCM}",
                strPlhInit + @":HOST}",
                strPlhInit + @":PORT}",
                strPlhInit + @":PATH}",
                strPlhInit + @":QUERY}",
                strPlhInit + @":USERINFO}",
                strPlhInit + @":USERNAME}",
                strPlhInit + @":PASSWORD}"
            };

            var str = strText;
            string strDataCmp = null;
            Uri uri = null;
            for (var i = 0; i < vPlhs.Length; ++i)
            {
                var strPlh = vPlhs[i];
                if (str.IndexOf(strPlh, ScMethod) < 0) continue;

                if (strDataCmp == null)
                {
                    var ctxData = (bDataIsEncoded
                        ? ctx.WithoutContentTransformations()
                        : ctx);
                    strDataCmp = CompileInternal(strData, ctxData,
                        uRecursionLevel + 1);
                }

                string strRep = null;
                if (i == 0) strRep = strDataCmp;
                else if (i == 1) strRep = UrlUtil.RemoveScheme(strDataCmp);
                else
                {
                    try
                    {
                        if (uri == null) uri = new Uri(strDataCmp);

                        int t;
                        switch (i)
                        {
                            case 2:
                                strRep = uri.Scheme;
                                break;
                            case 3:
                                strRep = uri.Host;
                                break;
                            case 4:
                                strRep = uri.Port.ToString(
                                    NumberFormatInfo.InvariantInfo);
                                break;
                            case 5:
                                strRep = uri.AbsolutePath;
                                break;
                            case 6:
                                strRep = uri.Query;
                                break;
                            case 7:
                                strRep = uri.UserInfo;
                                break;
                            case 8:
                                strRep = uri.UserInfo;
                                t = strRep.IndexOf(':');
                                if (t >= 0) strRep = strRep.Substring(0, t);
                                break;
                            case 9:
                                strRep = uri.UserInfo;
                                t = strRep.IndexOf(':');
                                if (t < 0) strRep = string.Empty;
                                else strRep = strRep.Substring(t + 1);
                                break;
                            default:
                                Debug.Assert(false);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                    } // Invalid URI
                }
                if (strRep == null) strRep = string.Empty; // No assert

                str = StrUtil.ReplaceCaseInsensitive(str, strPlh, strRep);
            }

            return str;
        }

        private static string RemoveComments(string strSeq)
        {
            var str = strSeq;

            while (true)
            {
                var iStart = str.IndexOf(StrRemStart, ScMethod);
                if (iStart < 0) break;
                var iEnd = str.IndexOf(StrRemEnd, iStart + 1, ScMethod);
                if (iEnd <= iStart) break;

                str = (str.Substring(0, iStart) + str.Substring(iEnd + StrRemEnd.Length));
            }

            return str;
        }

        private static string FillRefPlaceholders(string strSeq, SprContext ctx,
            uint uRecursionLevel)
        {
            return strSeq;
        }

        private static string FillRefsUsingCache(string strText, SprContext ctx)
        {
            var str = strText;

            foreach (var kvp in ctx.RefsCache)
            {
                str = StrUtil.ReplaceCaseInsensitive(str, kvp.Key, kvp.Value);
            }

            return str;
        }

        /// <summary>
        ///     Parse and remove a placeholder of the form
        ///     <c>{PLH:/Param1/Param2/.../}</c>.
        /// </summary>
        internal static bool ParseAndRemovePlhWithParams(ref string str,
            SprContext ctx, uint uRecursionLevel, string strPlhStart,
            out int iStart, out List<string> lParams, bool bSprCmpParams)
        {
            Debug.Assert(strPlhStart.StartsWith(@"{") && !strPlhStart.EndsWith(@"}"));

            iStart = str.IndexOf(strPlhStart, StrUtil.CaseIgnoreCmp);
            if (iStart < 0)
            {
                lParams = null;
                return false;
            }

            lParams = new List<string>();

            try
            {
                var p = iStart + strPlhStart.Length;
                if (p >= str.Length) throw new FormatException();

                var chSep = str[p];

                while (true)
                {
                    if ((p + 1) >= str.Length) throw new FormatException();

                    if (str[p + 1] == '}') break;

                    var q = str.IndexOf(chSep, p + 1);
                    if (q < 0) throw new FormatException();

                    lParams.Add(str.Substring(p + 1, q - p - 1));
                    p = q;
                }

                Debug.Assert(str[p + 1] == '}');
                str = str.Remove(iStart, (p + 1) - iStart + 1);
            }
            catch (Exception)
            {
                str = str.Substring(0, iStart);
            }

            if (bSprCmpParams && (ctx != null))
            {
                var ctxSub = ctx.WithoutContentTransformations();
                for (var i = 0; i < lParams.Count; ++i)
                    lParams[i] = CompileInternal(lParams[i], ctxSub, uRecursionLevel);
            }

            return true;
        }

        private static string PerformTextTransforms(string strText, SprContext ctx,
            uint uRecursionLevel)
        {
            var str = strText;
            int iStart;
            List<string> lParams;

            while (ParseAndRemovePlhWithParams(ref str, ctx, uRecursionLevel,
                @"{T-REPLACE-RX:", out iStart, out lParams, true))
            {
                if (lParams.Count < 2) continue;
                if (lParams.Count == 2) lParams.Add(string.Empty);

                try
                {
                    var strNew = Regex.Replace(lParams[0], lParams[1], lParams[2]);
                    strNew = TransformContent(strNew, ctx);
                    str = str.Insert(iStart, strNew);
                }
                catch (Exception)
                {
                }
            }

            while (ParseAndRemovePlhWithParams(ref str, ctx, uRecursionLevel,
                @"{T-CONV:", out iStart, out lParams, true))
            {
                if (lParams.Count < 2) continue;

                try
                {
                    var strNew = lParams[0];
                    var strCmd = lParams[1].ToLower();

                    if ((strCmd == "u") || (strCmd == "upper"))
                        strNew = strNew.ToUpper();
                    else if ((strCmd == "l") || (strCmd == "lower"))
                        strNew = strNew.ToLower();
                    else if (strCmd == "base64")
                    {
                        var pbUtf8 = StrUtil.Utf8.GetBytes(strNew);
                        strNew = Convert.ToBase64String(pbUtf8);
                    }
                    else if (strCmd == "hex")
                    {
                        var pbUtf8 = StrUtil.Utf8.GetBytes(strNew);
                        strNew = MemUtil.ByteArrayToHexString(pbUtf8);
                    }
                    else if (strCmd == "uri")
                        strNew = Uri.EscapeDataString(strNew);
                    else if (strCmd == "uri-dec")
                        strNew = Uri.UnescapeDataString(strNew);

                    strNew = TransformContent(strNew, ctx);
                    str = str.Insert(iStart, strNew);
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }
            }

            return str;
        }
    }
}