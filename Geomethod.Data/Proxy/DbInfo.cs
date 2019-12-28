using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Geomethod.Data;

namespace Geomethod.Data
{
	public class DbInfo : IdProxy
    {
        #region Static
		static IdProxyFactory factory = new IdProxyFactory(GmTables.DbInfo, typeof(Fields));
		public static IdProxyFactory Factory { get { return factory; } }
		public enum Fields {Info}
		public static DbInfo GetItem(GmConnection conn, int id) { return factory.GetItem<DbInfo>(conn, id); }
        #endregion 

        #region Fields
		DbXmlInfo info;
        #endregion

        #region Properties
		public override GmProxyFactory ProxyFactory { get { return factory;}}
		public int CloneId { get { return info.cloneId; } }
		public int PoolSize { get { return info.poolSize; } }
		public int AppPoolSize { get { return info.appPoolSize; } }
		#endregion

        #region Construction
		public DbInfo() {}
		public DbInfo(int id) : base(id) { info = new DbXmlInfo(); }
		#endregion

        #region Methods
		protected override void Init(GmDataReader dr)
		{
			info = DbXmlInfo.DeserializeString(dr.GetString());
        }
		protected override void AddDbParameters(GmCommand cmd)
		{
			cmd.AddString(Fields.Info, info.Serialize());
        }
        #endregion
    }
}