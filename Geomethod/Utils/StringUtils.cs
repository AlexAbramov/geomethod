using System;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Geomethod
{
    public class StringUtils
    {
        public static string TrimVersion(string s)
        {
            int i = s.LastIndexOf('.');
            if (i > 0) s = s.Substring(0, i);
            return s;
        }
        public static string TrimAndCap(string s)
        {
            s = s.Trim();
            if (s.Length > 0)
            {
                return Char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return s;
        }
        public static void RemoveHotKey(ref string val)
        {
            int pos = val.IndexOf('&');
            if (pos >= 0) val = val.Remove(pos, 1);
        }
        public static void RemoveLastChar(ref string s)
        {
            if (s.Length > 0) s = s.Substring(0, s.Length - 1);
        }
        public static void RemoveFirstChar(ref string s)
        {
            if (s.Length > 0) s = s.Substring(1);
        }
        public static string NotNullString(string s) { return s == null ? "" : s; }
    }

}
