using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Resources;

namespace Geomethod
{
    [FlagsAttribute]
    public enum StringSetFlags { None = 0, IgnoreKeyNotFound = 1, VirtualMode = 2 }

	public class StringSetException : GmException { public StringSetException(string msg) : base(msg) { } }
	public class StringSetNotFoundException : StringSetException { public StringSetNotFoundException(string msg) : base(msg) { } }
	public class StringSetNotLoadedException : StringSetException { public StringSetNotLoadedException(string msg) : base(msg) { } }
	public class StringSetKeyNotFoundException : StringSetException { public StringSetKeyNotFoundException(string msg) : base(msg) { } }
	
/*	public interface IStringSet
    {
		bool IsLoaded { get; }
		string Name { get; }
        bool ContainsKey(string key);
		string this[string key]{get;set;}
        StringSetFlags StringSetFlags { get; set; }
    }*/

	public class StringSet// : IStringSet
	{
        public const byte binaryFileToken = byte.MaxValue;
        const string textFileNameToken = "locale.csv";
        const string defaultLangName = "en";
//		public const string fileToken = "Geomethod.StringSet";
		public const int version=1;
		public const int initialCapacity = 1<<16;

		#region Fields
		string name = null;
        Dictionary<string, string> items = new Dictionary<string, string>(initialCapacity);
        public StringSetFlags stringSetFlags = 0;
		#endregion

		#region Properties
		public bool IsLoaded { get { return name!=null && items!=null; } }
		public string Name { get { return name; } }
        public StringSetFlags StringSetFlags { get { return stringSetFlags; } set { stringSetFlags = value; } }
        public bool HasFlags(StringSetFlags flags) { return (this.stringSetFlags & flags) == flags; }
		#endregion

		#region Construction
		public StringSet() : this(null) { }//StringSetFlags.VirtualMode
		public StringSet(string name) : this(name, 0, StringSetFlags.IgnoreKeyNotFound) { }//StringSetFlags.VirtualMode
		public StringSet(StringSetFlags flags): this(null,0,flags) {}
		public StringSet(string name, int initialCapacity, StringSetFlags flags)
		{
            SetName(name);
            items = new Dictionary<string, string>(initialCapacity);
            this.stringSetFlags = flags;
		}
        public StringSet(string name, string filePath): this(name, filePath,Encoding.UTF8, 0){}
        public StringSet(string name, string filePath, StringSetFlags flags): this(name, filePath,Encoding.UTF8, flags){}
        public StringSet(string name, string filePath, Encoding encoding, StringSetFlags flags)
        {
            SetName(name);
            filePath = PathUtils.AbsFilePath(filePath);
            this.stringSetFlags = flags;
            Load(filePath, encoding);
        }
        public StringSet(string name, Assembly assembly, string resourceName) : this(name, assembly, resourceName, Encoding.UTF8, 0) { }
        public StringSet(string name, Assembly assembly, string resourceName, StringSetFlags flags) : this(name, assembly, resourceName, Encoding.UTF8, flags) { }
        public StringSet(string name, Assembly assembly, string resourceName, Encoding encoding, StringSetFlags flags)
        {
            SetName(name);
            this.stringSetFlags = flags;
            this.Load(assembly, resourceName, encoding);
        }

        private void SetName(string name)
        {
            if (String.IsNullOrEmpty(name) || name.Trim().Length == 0)
            {
                name = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            this.name = name;
        }

		#endregion

		#region Loading
        public void Load()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Load(assembly);
            }
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
        }

        private void Load(Assembly assembly)
        {
            if (!assembly.GlobalAssemblyCache)
            {
                foreach (string s in assembly.GetManifestResourceNames())
                {
                    if (s.EndsWith(textFileNameToken))
                    {
                        Load(assembly, s);
                    }
                }
            }
        }

        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Load(args.LoadedAssembly);
        }

        public void Load(string filePath, Encoding encoding)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                bool isBinary=fs.ReadByte()==binaryFileToken;
                fs.Seek(0,SeekOrigin.Begin);
                if (isBinary)
                {
                    using(BinaryReader br=new BinaryReader(fs, encoding))
                    {
                      Load(br);
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(fs, encoding))
                    {
                        Load(sr);
                    }
                }
            }
        }
        public void Load(BinaryReader br)
		{
            if (br.ReadByte() != binaryFileToken) throw new StringSetException("Wrong binary file token.");
			int serVer = br.ReadInt32();
			int count = br.ReadInt32();
            if (items.Count == 0)
            {
                items = new Dictionary<string, string>(count);
            }
			for (int i = 0; i < count; i++)
			{
				items.Add(br.ReadString(), br.ReadString());
			}
		}
        public void Load(Assembly assembly, string resourceName) { Load(assembly, resourceName, Encoding.UTF8); }
		public void Load(Assembly assembly, string resourceName, Encoding encoding)
		{
			if(assembly==null) assembly=Assembly.GetEntryAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null) throw new StringSetException("Resource not found: " + resourceName);
                Load(stream, encoding);
			}
		}

        public void Load(Stream stream, Encoding encoding)
		{
			using (StreamReader sr = new StreamReader(stream, encoding))
			{
                Load(sr);
			}
		}

        private void Load(TextReader sr)
        {
            CsvConverter csvConverter = new CsvConverter();
            List<string> row = new List<string>();
            int colIndex = -1;
            for (int lineIndex = 0; ; lineIndex++)
            {
                string line = sr.ReadLine();
                if (line == null) break;
                if (line.Length == 0) continue;
                csvConverter.Parse(line, row);
                if (lineIndex == 0)// csv file header
                {
                    colIndex = GetColIndex(row);
                    if (colIndex < 0)
                    {
                        throw new StringSetNotFoundException(string.Format("String set '{0}' not found in the header: {1}", name, line));
                    }
//                    for (int i = 1; i < row.Count; i++) names.Add(row[i]);
                }
                else
                {
                    if (items == null) items = new Dictionary<string, string>(initialCapacity);
                    if (colIndex >= row.Count)
                    {
                        throw new StringSetException("Value not found in the line: " + line);
                    }
                    string key = FixKey(row[0]);
                    if (key.Length > 0)
                    {
                        string val = row[colIndex];
                        items[key] = val;
                    }
                }
            }
        }
        int GetColIndex(List<string> row)
        {
            int colIndex = row.IndexOf(name);
            if(colIndex < 0)
            {
                int dashPos = name.IndexOf('-');
                if (dashPos > 0)
                {
                    string shortName = name.Substring(0, dashPos);
                    colIndex = row.IndexOf(shortName);
                }
                if (colIndex < 0)
                {
                    colIndex = row.IndexOf(defaultLangName);
                    if (colIndex < 0)
                    {
                        for (int i = 0; i < row.Count; i++)
                        {
                            string s = row[i];
                            if (s.StartsWith(defaultLangName))
                            {
                                colIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            return colIndex;
        }
		#endregion

        #region Methods
        public static string AmendString(string s)
		{
			StringBuilder sb = new StringBuilder(s.Length+5);
			char prevChar=char.MinValue;
			foreach (char c in s)
			{
				if(c!='_')
				{
					if(char.IsLower(prevChar) && char.IsUpper(c))
					{
						sb.Append(' ');
					}
					if (sb.Length == 0 && char.IsLower(c))
					{
						sb.Append(char.ToUpper(c));
					}
					else sb.Append(c);
				}
				prevChar = c;
			}
			return sb.ToString();
        }
        public string this[string key]
		{
			get
			{
                return Get(key);
			}
			set
			{
				key = FixKey(key);
                items[key] = value;
			}
		}

		private string FixKey(string key)
		{
			return key.Trim().ToLower();
		}

        public string Get(string key)
        {
            if (HasFlags(StringSetFlags.VirtualMode)) return AmendString(key);
            if (items == null) throw new StringSetNotLoadedException("String set not loaded. Requested key: " + key);
			string fkey = key.TrimStart();
			string postfix="";
			int pos = FindPos(fkey);
			if (pos >= 0)
			{
				postfix = fkey.Substring(pos);
				fkey = fkey.Substring(0, pos);
			}
			fkey = fkey.ToLower();
            if (fkey.Length>0 && items.ContainsKey(fkey))
            {
				return items[fkey] + postfix;
            }
            else
            {
                if (HasFlags(StringSetFlags.IgnoreKeyNotFound)) return AmendString(key);
                else throw new StringSetKeyNotFoundException("String set key not found: " + key);
            }
        }

		private int FindPos(string key)
		{
			for (int i = 0; i < key.Length; i++)
			{
				char c = key[i];
				if (!Char.IsLetterOrDigit(c) && c != '_' && c!='.') return i;
			}
			return -1;
		}

        public bool ContainsKey(string key)
        {
            return items.ContainsKey(FixKey(key));
        }

        public string GetExisting(string key)
        {
            key=FixKey(key);
            return ContainsKey(key) ? items[key] : null;
        }
        #endregion

    }

}
