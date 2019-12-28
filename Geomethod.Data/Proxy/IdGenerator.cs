using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public interface IIdGenerator
	{
		int GetId(int tableId);
		int GetId(Enum tableId);
	}

	public class IdGenerator : IIdGenerator
	{
/*		#region Static
		static IdGenerator instance;
		public static IdGenerator Instance { get { if (instance == null) throw new GeomethodDataException("IdGenerator not initialized."); return instance; } }
		public static void Init(IdGenerator instance) { IdGenerator.instance = instance; }
		#endregion*/

		#region Fields
		string tableName="Ids";
		public enum Fields {TableId, RecordId}
		int appPoolSize=10;
		int poolSize=1000000;
		List<int> poolIds=new List<int>(new int[]{0});
		Dictionary<int, int> ids = new Dictionary<int, int>(1 << 8);//tableId,recordId
		ConnectionFactory connFactory = null;
		#endregion

		#region Properties
		int FirstPoolId { get { return poolIds[0]; } }
		int MinRecordId { get { return poolIds[0] * poolSize; } }
		#endregion

		#region Construction
		public IdGenerator(ConnectionFactory connFactory)
		{
			this.connFactory = connFactory;
		}
		public IdGenerator(int appPoolSize, int poolSize, int[] poolIds, ConnectionFactory connFactory)
		{
			this.appPoolSize = appPoolSize;
			this.poolSize = poolSize;
			if(poolIds!=null) this.poolIds = new List<int>(poolIds);
			this.connFactory = connFactory;
			if (poolIds.Length == 0) throw new GmDataException("IdGenerator.IdGenerator empty poolIds array");
		}
		public void Clear()
		{
			ids.Clear();
		}
		#endregion

		#region Serialization
		public void Load(GmConnection conn)
		{
/*!!!			string query = string.Format("select * from {0}", tableName);
			using (DbDataReader dr = conn.ExecuteReader(query))
			{
				while (dr.Read())
				{
                    ids.Add(dr.GetInt32(0), dr.GetInt32(1));
				}
			}*/
		}
		public void Save(GmConnection conn)
		{
			int count = ids.Count;
			if (count > 0)
			{
				int[] keys = new int[count];
				int[] vals = new int[count];
				ids.Keys.CopyTo(keys, 0);
				ids.Values.CopyTo(vals, 0);
				string cmdText=string.Format("insert into {0} values ( @TableId, @RecordId)",tableName);
				GmCommand cmd = conn.CreateCommand(cmdText);
				for (int i = 0; i < count; i++)
				{
					cmd.AddInt(Fields.TableId, keys[i]);
					cmd.AddInt(Fields.RecordId, vals[i]);
					cmd.ExecuteNonQuery();
				}
			}
		}
		public void Write(BinaryWriter bw)
		{
			bw.Write(tableName);
			bw.Write(appPoolSize);
			bw.Write(poolSize);
			lock (poolIds)
			{
				bw.Write(poolIds.Count);
				if (poolIds.Count > 0) foreach (int poolId in poolIds) bw.Write(poolId);
			}
			lock (ids)
			{
				bw.Write(ids.Count);
				if (ids.Count > 0)
				{
					foreach(KeyValuePair<int,int> pair in ids)
					{
						bw.Write(pair.Key);
						bw.Write(pair.Value);
					}
				}
			}
		}
	    public void Read(BinaryReader br)
		{
/*				Clear();
			tableName = br.ReadString();
			appPoolSize = br.ReadInt32();
			poolSize = br.ReadInt32();
			for (int i = 0; i < count; i++)
				appPoolSize = br.ReadInt32();
			int count = br.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				int typeId = br.ReadInt32();
				int tableId = br.ReadInt32();
				ids.Add( typeId, tableId);
			}*/
		}
		#endregion

		#region Generation
		public int GetId(Enum tableId) {return GetId((int)(object)tableId);}
		public int GetId(int tableId)
		{
			int recordId = MinRecordId;
			bool hasKey=ids.ContainsKey(tableId);
			if(hasKey) recordId=ids[tableId];
			if (connFactory != null)
			{
				if (!hasKey || recordId % appPoolSize == 0)
				{
					using (GmConnection conn = connFactory.CreateConnection())
					{
						recordId = GetPool(conn, tableId);
					}
				}
			}
			recordId++;
			ids[tableId] = recordId;
			return recordId;
		}
		int GetPool(GmConnection conn, int tableId)
		{
			int recordId;
			using(GmTransaction trans=conn.BeginTransaction())
			{
                string query = string.Format( "select [RecordId] from {0} where [TableId] = @TableId", tableName );
				GmCommand cmd = trans.CreateCommand(query);
				cmd.AddInt(Fields.TableId, tableId);
				object obj = cmd.ExecuteScalar();
                if(obj==null || obj==DBNull.Value)
				{
					recordId = MinRecordId;
                    cmd.CommandText = string.Format( "insert into {0} values ( @TableId, @RecordId )", tableName );
				}
                else
				{
                    recordId = System.Convert.ToInt32( obj );
                    cmd.CommandText = string.Format( "update {0} set [RecordId] = @RecordId where [TableId] = @TableId", tableName );
				}

				cmd.AddInt("RecordId", recordId + appPoolSize);
				cmd.ExecuteNonQuery();
				trans.Commit();
			}
			return recordId;
		}
		#endregion 

    }
}
