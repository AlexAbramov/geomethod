using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class IdStatusProxyFactory: GmProxyFactory
	{
		public IdStatusProxyFactory(Enum tableEnum, Type fieldsEnum): base(tableEnum, new string[] { GmFields.Id.ToString(), GmFields.Status.ToString() }, fieldsEnum)
		{
		}
		public T GetItem<T>(GmConnection conn, int id) where T : GmProxy, new() { return GetItem<T>(conn, "where Id=" + id); }
		public GmCommand CreateSelectCommand(GmConnection conn, RecordStatus status)
		{
			string cmdText = string.Format("select {0} from {1}", GetColumnList(GetColumnListOptions.Select), TableName);
			if (status != RecordStatus.All) cmdText += string.Format(" where Status={0}", (byte)status);
			GmCommand cmd = conn.CreateCommand(cmdText);
			return cmd;
		}
		public int SetStatus(GmConnection conn, int id, RecordStatus status)
		{
			string cmdText = string.Format("update {0} set Status={1} where Id=@Id", TableName, (byte)status);
			GmCommand cmd = conn.CreateCommand(cmdText);
			cmd.AddInt(GmFields.Id, id);
			return cmd.ExecuteNonQuery();
		}
		public List<T> GetItems<T>(GmConnection conn, RecordStatus status) where T : IdStatusProxy, new()
		{
			GmCommand cmd = CreateSelectCommand(conn, status);
			List<T> list = new List<T>();
			using (DbDataReader dr = cmd.ExecuteReader())
			{
				while (dr.Read())
				{
					list.Add(CreateItem<T>(dr));
				}
			}
			return list;
		}
	}
}
