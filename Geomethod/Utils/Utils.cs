using System;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Principal;

namespace Geomethod
{
	public class GmException: Exception
	{
		public GmException() : base() { }
		public GmException(string message) : base(message) { }
		public GmException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class BufferUtils
	{
		public static int FloatToInt(float f)
		{
			float[] ff={f};
			int[] ii=new int[1];
			Buffer.BlockCopy(ff,0,ii,0,4);
			return ii[0];
		}
		public static float IntToFloat(int i)
		{
			int[] ii={i};
			float[] ff=new float[1];
			Buffer.BlockCopy(ii,0,ff,0,4);
			return ff[0];
		}
	}

	public class PathUtils
	{
		public const string dataDirectoryToken = "DataDirectory";
		static readonly string[] tokens ={ "\\bin\\", "\\bin\\release\\", "\\bin\\debug\\" };
		static string baseDir = null;
		static string dataDir = null;
		public static string BaseDirectory
		{
			get
			{
				if (baseDir != null) return baseDir;
				baseDir = AppDomain.CurrentDomain.BaseDirectory;
				string s = baseDir;
				foreach (string token in tokens)
				{
					if (s.EndsWith(token,StringComparison.OrdinalIgnoreCase))
					{
						baseDir = baseDir.Substring(0, baseDir.Length - token.Length + 1);
						break;
					}
				}
				return baseDir;
			}
		}
		public static string DataDirectory
		{
			get
			{
				if (dataDir != null) return dataDir;
				object obj = AppDomain.CurrentDomain.GetData(dataDirectoryToken);
				if (obj is string)
				{
					dataDir = obj.ToString();
				}
				else
				{
					dataDir = BaseDirectory+"Data\\";
				}
				return dataDir;
			}
			set
			{
				if (Path.IsPathRooted(value)) dataDir = value;
				else
				{
					dataDir = BaseDirectory + value;
					AppDomain.CurrentDomain.SetData(dataDirectoryToken, dataDir);
				}
			}
		}
/*		public static string CommonApplicationDataDirectory { get { return System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); } }
		public static string ApplicationDataDirectory { get { return System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); } }
		public static string PersonalDirectory { get { return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal); } }*/
		public static string GetExtension(string path)
		{
			string ext = Path.GetExtension(path);
			StringUtils.RemoveFirstChar(ref ext);
			return ext;
		}
		public static string AbsFilePath(string filePath)
		{
			if (Path.IsPathRooted(filePath)) return filePath;
			if (filePath.StartsWith("\\")) filePath = filePath.Remove(0, 1);
			return Path.Combine(BaseDirectory,filePath);
		}
		public static string TrimFilePath(string filePath)
		{
			string s1 = filePath.ToLower();
			string s2 = BaseDirectory.ToLower();
			int pos = s1.IndexOf(s2);
			if (pos >= 0)
			{
				pos += s2.Length;
				return filePath.Substring(pos);
			}
			return filePath;
		}
	}

	public class DateTimeUtils
	{
		public static DateTime Parse(string str)
		{
			string [] ss=str.Split("/ :.".ToCharArray());
			if(ss.Length!=7)
			{
				throw new Exception("Wrong DateTime format :"+str);
			}
			int[] ii=new int[7];
			for(int i=0;i<ii.Length;i++) ii[i]=int.Parse(ss[i]);
			return new DateTime(ii[0],ii[1],ii[2],ii[3],ii[4],ii[5],ii[6]);
		}
        public static bool TryParse(string str, out DateTime dt)
        {
            try
            {
                dt = Parse(str);
                return true;
            }
            catch
            {
                dt = DateTime.Now;
                return false;
            }

        }
        public static string ToString(DateTime dt)
		{
			return string.Format("{0}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second,dt.Millisecond);
		}
		public static string ToString(DateTime dt, string format)
		{
			if (dt == DateTime.MinValue) return "";
			return dt.ToString(format);
		}
		public static object GetNullableTime(DateTime dt)
		{
			if (dt == DateTime.MinValue) return DBNull.Value;
			return dt;
		}
		public static DateTime GetNullableTime(object obj)
		{
			if (obj == DBNull.Value || obj == null) return DateTime.MinValue;
			return (DateTime)obj;
		}
		public static string Now
		{
			get{return ToString(DateTime.Now);}
		}

		public static int Age(DateTime birthday) { return Age(birthday, DateTime.Today); }
		public static int Age(DateTime birthday, DateTime dateTime)
		{
			if (birthday == DateTime.MinValue || dateTime == DateTime.MinValue) return -1;
			int age= dateTime.Year - birthday.Year;
			DateTime thisYearBirthday=new DateTime(dateTime.Year,birthday.Month,birthday.Day);
			return dateTime>thisYearBirthday?age:age-1;
		}
    }

	public class CollectionUtils
	{
		public static List<T> Clone<T>(List<T> ar)
		{
			List<T> res = new List<T>(ar.Count);
			foreach (ICloneable c in ar) res.Add((T)c.Clone());
			return res;
		}
        public static string GetCommaSeparatedList<T>(IEnumerable<T> list) { return GetCommaSeparatedList(list, ","); }
        public static string GetCommaSeparatedList<T>(IEnumerable<T> list, string sepToken)
        {
            StringBuilder sb = new StringBuilder(1<<10);
            foreach (T t in list)
            {
                if (sb.Length > 0) sb.Append(sepToken);
                sb.Append(t.ToString());
            }
            return sb.ToString();
        }
    }

	public class ParsingUtils
	{
		static NumberFormatInfo nfi = new NumberFormatInfo();
		static ParsingUtils()
		{
			nfi.NumberDecimalSeparator = ".";
		}
		public static double ParseDouble(string s)
		{
			if (s.Contains(",")) s = s.Replace(',', '.');
			return double.Parse(s, nfi);
		}
        public static float ParseFloat(string s)
        {
            if (s.Contains(",")) s = s.Replace(',', '.');
            return float.Parse(s, nfi);
        }
    }

	public class ReflectionUtils
	{
		public static bool HasData(object obj)
		{
			Type type = obj.GetType();
			PropertyInfo pi = type.GetProperty("Id");
			if (pi != null)
			{
				object val = pi.GetValue(obj, null);
				if (val is int && ((int)val) != 0) return true;
			}
			foreach (FieldInfo fi in type.GetFields())
			{
				if (fi.IsPublic && !fi.IsStatic)
				{
					if (fi.FieldType == typeof(string))
					{
						string s = fi.GetValue(obj) as string;
						if (s != null && s.Trim().Length > 0) return true;
					}
					else if (fi.FieldType == typeof(int))
					{
						int val = (int)fi.GetValue(obj);
						if (val != 0) return true;
					}
				}
			}
			return false;
		}
	}

}
