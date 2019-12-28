using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using Geomethod;
using Geomethod.Data;

namespace Geomethod.Data
{
    public class ConnectionsInfo //: IEnumerable<ConnectionInfo>
    {
        #region Fields
		[XmlAttribute]
		public int defaultId = 0;
        [XmlArray("items")]
        public List<ConnectionInfo> items = new List<ConnectionInfo>();
        #endregion

        #region Properties
		public int Count { get { return items.Count; } }
		#endregion

        #region Construction
        public ConnectionsInfo(){}
        #endregion

        #region Methods
/*        public ConnectionString this[int index]{get{return items[index];}}
        public ConnectionString GetItemByIndex(int index){return items[index]; }*/

		public ConnectionInfo GetDefaultItem()
		{
			if (defaultId > 0)
			{
				foreach (ConnectionInfo connectionInfo in items)
				{
					if (connectionInfo.id == defaultId) return connectionInfo;
				}
			}
			return null;
		}
		public ConnectionInfo GetItem(int id)
		{
			foreach (ConnectionInfo connectionInfo in items)
			{
				if (connectionInfo.id == id) return connectionInfo;
			}
			return null;
		}
		public ConnectionInfo GetItem(string name)
		{
			foreach (ConnectionInfo connectionInfo in items)
			{
				if (connectionInfo.name == name) return connectionInfo;
			}
			return null;
		}
		public bool HasName(string name)
		{
			foreach (ConnectionInfo connectionInfo in items)
			{
				if (connectionInfo.name == name) return true;
			}
			return false;
		}
		int GetMaxId()
		{
			int maxId = 0;
			foreach (ConnectionInfo connectionInfo in items)
			{
				if (connectionInfo.id > maxId) maxId = connectionInfo.id;
			}
			return maxId;
		}

		int GenerateId() { return GetMaxId() + 1; }

		public void Add(ConnectionInfo connectionInfo)
		{
			if (connectionInfo.id == 0) connectionInfo.id = GenerateId();
            items.Add(connectionInfo);

		}

        public void Remove(ConnectionInfo connectionInfo)
		{
            items.Remove(connectionInfo);
			if (defaultId == connectionInfo.id) defaultId = 0;
        }
        #endregion

        #region IEnumerable<ConnectionString> Members

 /*       IEnumerator<ConnectionInfo> IEnumerable<ConnectionInfo>.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }*/

        #endregion
    }

    [XmlRootAttribute]
    public class ConnectionsConfig : BaseConfig
    {
        public override int CurrentVersion { get { return 1; } }

        #region Static
        public static ConnectionsConfig DeserializeFile(string filePath) { return (ConnectionsConfig)BaseConfig.DeserializeFile(typeof(ConnectionsConfig), filePath); }
        #endregion

        public ConnectionsInfo connectionsInfo = new ConnectionsInfo();

		public override object Clone()
		{
			return this.MemberwiseClone();
		}
    }

}
