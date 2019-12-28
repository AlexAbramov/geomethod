using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public abstract class IdStatusProxy: GmProxy
	{

		#region Fields
		int id;
		RecordStatus status;
		#endregion

		#region Properties
		public int Id { get { return id; } }
		public RecordStatus Status { get { return status; } }
		public byte ByteStatus { get { return (byte)status; } /*set { status = (RecordStatus)value; }*/ }
		#endregion

		#region Construction
		protected IdStatusProxy()
		{
			id = 0;
			status = RecordStatus.Normal;
		}
/*		protected GmProxy(IIdGenerator gen)
		{
			this.id = gen.GetId(tableId);
		}
		protected GmProxy(DbDataReader dr)
		{
			newRecord = false;
			int i = 0;
			id = dr.GetInt();
			status = (RecordStatus)dr.GetByte(i++);
		}*/
		#endregion

		#region Methods
		protected internal override void Init(DbDataReader dr)
		{
			GmDataReader gr = new GmDataReader(dr);
			id = gr.GetInt();
			status = (RecordStatus)gr.GetByte();
			Init(gr);
		}
		public override int Save(GmConnection conn)
		{
			GmCommand cmd = conn.CreateCommand();
			GmProxyFactory factory = ProxyFactory;
			bool isNewRecord = id == 0;
			if (isNewRecord)
			{
				id=conn.ConnectionFactory.IdGenerator.GetId(TableId);
			}
			cmd.AddInt(GmFields.Id, id);
			cmd.AddByte(GmFields.Status, (byte)status);
			if(conn.HasSync) cmd.AddInt(GmFields.SyncId, conn.SyncId);
			AddDbParameters(cmd);
			if (isNewRecord)
			{
				cmd.CommandText = string.Format("insert into {0} ({1}) values ({2})", TableName, ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Select), ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Insert));
			}
			else
			{
				cmd.CommandText = string.Format("update {0} set {1} where Id=@Id", TableName, ProxyFactory.GetColumnList(cmd, GetColumnListOptions.Update));
			}
			int res= cmd.ExecuteNonQuery();
            return res;
		}
        #endregion

    }
}
