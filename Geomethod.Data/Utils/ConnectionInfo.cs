using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using Geomethod;
using Geomethod.Data;

namespace Geomethod.Data
{
	public class ConnectionInfo
    {
        public const string fileProviderName = "File";
        const string optionsFileToken = "file";

        #region Fields
		[XmlAttribute]
		public int id = 0;
        [XmlAttribute]
        public string name="";
        [XmlAttribute]
		public string providerName="";
        [XmlAttribute]
		public string connectionString="";
        [XmlAttribute]
		public string options="";
        #endregion

        #region Properties
//        [XmlAttribute]
//        public int Id { get { return id; } set { id = value; } }
 /*       public string Name { get { return name; } set { name = Geomethod.StringUtils.NotNullString(value).Trim(); } }
        public string ProviderName{get{return providerName;}set{providerName=Geomethod.StringUtils.NotNullString(value).Trim();}}
        public string ConnectionString{get{return connStr;}set{connStr=Geomethod.StringUtils.NotNullString(value).Trim();}}
        public string Options{get{return options;}set{options=Geomethod.StringUtils.NotNullString(value);ParseOptions();}}*/
        [XmlIgnore]
        public GmProviderFactory ProviderFactory { get { return IsDbConnection ? GmProviders.Get(providerName) : null; } }
        [XmlIgnore]
        bool IsDbConnection { get { return GmProviders.HasProvider(providerName); } }
        [XmlIgnore]
        bool IsFileConnection { get { return providerName == fileProviderName; } }
        [XmlIgnore]
        public string FilePath { get { return IsFileConnection ? connectionString : ""; } set { if (IsFileConnection) connectionString = StringUtils.NotNullString(value).Trim(); } }
        #endregion

        #region Construction
        public ConnectionInfo(){}
		public ConnectionInfo(string name) { this.name = name; }
		public ConnectionInfo(string name, string filePath)
		{
			this.name = name;
			this.options = optionsFileToken+"=" + filePath;
		}
		public ConnectionInfo(string name, string providerName, string connectionString, string options)
		{
			this.name=name;
			this.providerName=providerName;
			this.connectionString = connectionString;
			this.options=options;
        }
        #endregion

        #region Methods
        public GmConnection CreateConnection()
        {
            if (connectionString.Length == 0 || providerName.Length == 0) return null;
            GmProviderFactory prov = GmProviders.Get(providerName);
            return prov.CreateConnection(connectionString);
        }
        public ConnectionFactory CreateConnectionFactory()
		{
			if(connectionString.Length==0 || providerName.Length==0) return null;
			GmProviderFactory prov=GmProviders.Get(providerName);
			return prov.CreateConnectionFactory(connectionString);
	    }
		public List<StringPair> ParseOptions()
		{
			List<StringPair> list=new List<StringPair>();
			foreach(string p in options.Split(';'))
			{
				int i=p.IndexOf('=');				
				if(i>=0) list.Add(new StringPair(p.Substring(0,i).Trim().ToLower(),p.Substring(i+1).Trim()));
				else list.Add(new StringPair(p.Trim().ToLower(), null));
			}
			return list;
		}
        public List<string> GetFileList()
        {
            List<string> fileList = new List<string>();
			foreach (StringPair p in ParseOptions())
            {
                if (p.key==optionsFileToken) fileList.Add(p.value);
            }
            return fileList;
        }
		public string GetDbName()
		{
			//!!!
			string s = connectionString.ToLower();
			int i = s.IndexOf("initial catalog");
			if (i >= 0)
			{
				i = s.IndexOf("=", i);
				if (i >= 0)
				{
					int j = s.IndexOf(';', i);
					if (j < 0) j = s.Length;
					return s.Substring(i + 1, j - i - 1);
				}
			}
			return "";
		}
		#endregion

    }

}
