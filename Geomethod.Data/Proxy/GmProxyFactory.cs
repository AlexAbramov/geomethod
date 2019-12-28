using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class GmProxyFactory
	{
		#region Fields
		int tableId;
		string tableName;
		protected List<string> fields=new List<string>();
		#endregion

		#region Properties
		public int TableId { get { return tableId; } }
		public string TableName { get { return tableName; } }
		#endregion

		#region Construction
		public GmProxyFactory(Enum tableEnum, string[] fields, Type fieldsEnum) 
		{ 
			tableId=(int)(object)tableEnum;
			tableName=tableEnum.ToString();
			this.fields.AddRange(fields);
			this.fields.AddRange(Enum.GetNames(fieldsEnum));
		}
		public GmProxyFactory(Enum tableEnum, Type fieldsEnum)
		{
			tableId = (int)(object)tableEnum;
			tableName = tableEnum.ToString();
			this.fields.AddRange(Enum.GetNames(fieldsEnum));
		}
		#endregion

		#region Methods
		public T GetItem<T>(GmCommand cmd) where T : GmProxy, new()
		{
			using (DbDataReader dr = cmd.ExecuteReader())
			{
				if (dr.Read())
				{
					return CreateItem<T>(dr);
				}
			}
			return null;
		}
		public T GetItem<T>(GmConnection conn, string cond) where T : GmProxy, new()
		{
			GmCommand cmd = CreateSelectCommand(conn, cond);
			return GetItem<T>(cmd);
		}
		public List<T> GetItems<T>(GmConnection conn, string cond) where T : GmProxy, new()
		{
			GmCommand cmd = CreateSelectCommand(conn, cond);
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
		public T CreateItem<T>(DbDataReader dr) where T : GmProxy, new()
		{
			T t = new T();
			t.Init(dr);
			return t;
		}
		public string GetColumnList(GetColumnListOptions opt)
		{
			return DataUtils.GetColumnList(fields, opt);
		}
		public string GetColumnList(GmCommand cmd, GetColumnListOptions opt)
		{
            List<string> list = new List<string>();
			string prefix=cmd.Connection.ProviderFactory.ParameterPrefix;
			foreach (DbParameter par in cmd.DbCommand.Parameters)
			{
				string parName = par.ParameterName;
				if (parName.StartsWith(prefix)) parName = parName.Substring(prefix.Length);
				list.Add(parName);
			}
			if (opt != GetColumnListOptions.Select && cmd.Connection.HasSync)
			{
				list.Add(GmFields.SyncId.ToString());
			}
            return DataUtils.GetColumnList(list, opt);
        }
		public GmCommand CreateSelectCommand(GmConnection conn, string cond)
		{
			string cmdText = string.Format("select {0} from {1}", GetColumnList(GetColumnListOptions.Select), TableName);
			if (!string.IsNullOrEmpty(cond)) cmdText += ' ' + cond;
			GmCommand cmd = conn.CreateCommand(cmdText);
			return cmd;
		}
		#endregion


    }
}
