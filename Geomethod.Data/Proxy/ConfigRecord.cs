using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class ConfigRecord : IdProxy, ICloneable
    {
        #region Static
		static IdProxyFactory factory = new IdProxyFactory(GmTables.ConfigRecords, typeof(Fields));
		public static IdProxyFactory Factory { get { return factory; } }
		public enum Fields {RestoredId,UserId,Version,Time,Config,Comment}
		public static ConfigRecord GetItem(GmConnection conn, int id) { return factory.GetItem<ConfigRecord>(conn, id); }
		public static ConfigRecord GetLastItem(GmConnection conn)
		{
			GmCommand cmd = conn.CreateCommand("select top(1) * from ConfigRecords order by Id desc");
			return factory.GetItem<ConfigRecord>(cmd);
		}
		public static ConfigRecord Clone(ConfigRecord configRecord){return (ConfigRecord)configRecord.Clone();}
		#endregion 

        #region Fields
        public int restoredId;
        public int userId;
        public int version;
        public DateTime time;
		public BaseConfig config;
        public string comment;
        #endregion

        #region Properties
		public override GmProxyFactory ProxyFactory { get { return factory;}}
		#endregion

        #region Construction
		public ConfigRecord() {}
		public ConfigRecord(int id, BaseConfig config) : base(id) 
		{ 
			restoredId=0;
			userId=0;
			version = config.CurrentVersion;
			time=DateTime.Now;
			this.config = config;
			comment="";
		}
		#endregion

        #region Methods
		protected override void Init(GmDataReader dr)
		{
			restoredId = dr.GetInt();
			userId = dr.GetInt();
			version = dr.GetInt();
			time = dr.GetDateTime();
			config = BaseConfig.DeserializeString(this.GetType(),dr.GetString());
			comment = dr.GetString();
        }
		protected override void AddDbParameters(GmCommand cmd)
		{
			cmd.AddInt(Fields.RestoredId, restoredId);
			cmd.AddInt(Fields.UserId, userId);
			cmd.AddInt(Fields.Version, version);
			cmd.AddDateTime(Fields.Time, time);
			cmd.AddString(Fields.Config, config.Serialize());
			cmd.AddString(Fields.Comment, comment, MaxLength.ConfigRecords.Comment);
        }
		public bool SaveConsistent(GmConnection conn)
		{
			conn.Close();
			using (GmTransaction trans = conn.BeginTransaction())
			{
				string query = string.Format("select max(Id) from {0}", TableName);
				GmCommand cmd = trans.CreateCommand(query);
				int maxId = cmd.ExecuteScalarInt32();
				if (maxId != 0 && Id!=maxId+1) return false;
				return Save(trans) > 0;
			}
		}
		int Save(GmTransaction trans)
		{
			GmCommand cmd = trans.CreateCommand();
			cmd.AddInt(GmFields.Id, Id);
			AddDbParameters(cmd);
			cmd.CommandText = string.Format("insert into {0} ({1}) values ({2})", TableName, ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Select), ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Insert));
			return cmd.ExecuteNonQuery();
		}
		public override int Save(GmConnection conn)
		{
			throw new GmDataException("ConfigRecord.SaveConsistent should be used.");
		}
		#endregion

		#region ICloneable Members

		public object Clone()
		{
			ConfigRecord obj = (ConfigRecord)this.MemberwiseClone();
			obj.config = (BaseConfig)this.config.Clone();
			return obj;
		}

		#endregion

		public void SetId(int id)
		{
			this.id = id;
		}
	}
}