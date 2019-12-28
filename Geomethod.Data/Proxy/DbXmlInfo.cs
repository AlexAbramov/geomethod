using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Geomethod.Data;
using Geomethod;
using System.IO;
using System.Xml.Serialization;
 
namespace Geomethod.Data
{
	[XmlRootAttribute]
	public class DbXmlInfo
	{
		#region Fields
//		public Guid guid;
//		public string appName = "";
		public int cloneId = 0;
		public int poolSize = 1000000;
		public int appPoolSize = 10;
		[XmlIgnore]
		public List<int> poolIds = new List<int>();
		#endregion

		#region Properties
		[XmlAttribute]
		public string PoolIdsStr { get { return XmlUtils.ToString(poolIds); } set { XmlUtils.FromString(poolIds, value); } }
		#endregion

		#region Construction
		public static DbXmlInfo DeserializeString(string xmlString)
		{
			if (String.IsNullOrEmpty(xmlString)) return new DbXmlInfo();
			return (DbXmlInfo)XmlUtils.DeserializeString(typeof(DbXmlInfo), xmlString);
		}
		public DbXmlInfo()
		{
			poolIds.Add(0);
		}
		#endregion

		public string Serialize()
		{
			return XmlUtils.Serialize(this);
		}
	}
}