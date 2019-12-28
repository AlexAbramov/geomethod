using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public abstract class IdProxy: GmProxy
	{
		#region Static
		protected static GmProxyFactory Init(Enum tableEnum, Type fieldsEnum)
		{
			string[] fields = { GmFields.Id.ToString()};
			return new GmProxyFactory(tableEnum, fields, fieldsEnum);
		}
		#endregion

		#region Fields
		internal protected int id;
		#endregion

		#region Properties
		public int Id { get { return id; } }
		#endregion

		#region Construction
		protected IdProxy()
		{
			id = 0;
		}
		protected IdProxy(int id)
		{
			this.id = id;
		}
		#endregion

		#region Methods
		protected internal override void Init(DbDataReader dr)
		{
			GmDataReader gr = new GmDataReader(dr);
			id = gr.GetInt();
			Init(gr);
		}
		public override int Save(GmConnection conn)
		{
			GmCommand cmd = conn.CreateCommand();
			bool isNewRecord = id == 0;
			if (isNewRecord)
			{
				id=conn.ConnectionFactory.IdGenerator.GetId(TableId);
			}
			cmd.AddInt(GmFields.Id, id);
			AddDbParameters(cmd);
			if (isNewRecord)
			{
				cmd.CommandText = string.Format("insert into {0} ({1}) values ({2})", TableName, ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Select), ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Insert));
			}
			else
			{
				cmd.CommandText = string.Format("update {0} set {1} where Id=@Id", TableName, ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Update));
			}
			return cmd.ExecuteNonQuery();
		}
        #endregion

    }
}
