using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class IdProxyFactory: GmProxyFactory
	{
		public IdProxyFactory(Enum tableEnum, Type fieldsEnum): base(tableEnum, new string[]{GmFields.Id.ToString(), GmFields.Status.ToString()}, fieldsEnum)
		{
		}
		public T GetItem<T>(GmConnection conn, int id) where T : GmProxy, new() { return GetItem<T>(conn, "where Id=" + id); }
		public int Delete(GmConnection conn, int id)
		{
			string cmdText = string.Format("delete from {0} where Id=@Id", TableName);
			GmCommand cmd = conn.CreateCommand(cmdText);
			cmd.AddInt(GmFields.Id, id);
			return cmd.ExecuteNonQuery();
		}
	}
}
