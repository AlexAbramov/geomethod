using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class GmConnection: IDisposable
	{
		#region Fields
		ConnectionFactory connectionFactory;
		DbConnection dbConnection;
		#endregion

		#region Properties
		public GmProviderFactory ProviderFactory { get { return connectionFactory.ProviderFactory; } }
		public ConnectionFactory ConnectionFactory { get { return connectionFactory; } }
		public string ConnectionString { get { return dbConnection.ConnectionString; } }
		public DbConnection DbConnection { get { return dbConnection; } }
		public bool HasSync { get { return connectionFactory.HasSync; } }
		public int SyncId { get { return connectionFactory.SyncId; } }
		#endregion

		#region Construction
		public GmConnection(ConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
			dbConnection = connectionFactory.ProviderFactory.DbProviderFactory.CreateConnection();
			dbConnection.ConnectionString = connectionFactory.ConnectionString;
		}
/*        public GmConnection(GmProviderFactory providerFactory, string connStr)
        {
            this.connectionFactory = providerFactory;
            dbConnection = providerFactory.DbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = connStr;
        }*/
		public void Open() { if (dbConnection != null) dbConnection.Open(); }
		public void Close() { if (dbConnection != null) dbConnection.Close(); }
		#endregion

		#region Commands
		public GmCommand CreateCommand() { return new GmCommand(this); }
		public GmCommand CreateCommand(DbTransaction trans) { return new GmCommand(this, trans); }
		public GmCommand CreateCommand(string cmdText) { return CreateCommand(cmdText,null); }
		public GmCommand CreateCommand(string cmdText, DbTransaction trans)
		{
			GmCommand cmd = new GmCommand(this, cmdText, trans);
			cmd.DbTransaction = trans;
			return cmd;
		}
		public GmCommand CreateCommandById(string cmdId) { return CreateCommandById(cmdId, null); }
		public GmCommand CreateCommandById(string cmdId, DbTransaction trans)
		{
			GmCommand cmd = null;
			if(CommandCache.enabled)
			{
				cmd = CommandCache.Get(cmdId);
				if(cmd!=null)
				{
					cmd.Connection=this;
				}
			}
			if (cmd == null)
			{
				string cmdText = SqlQueries.Get(cmdId);
				cmd = new GmCommand(this, cmdText);
				if (CommandCache.enabled) CommandCache.Save(cmdId, cmd);
			}
			cmd.DbTransaction = trans;
			return cmd;
		}
		public DbCommandBuilder CreateCommandBuilder()
		{
			return ProviderFactory.DbProviderFactory.CreateCommandBuilder();
		}
		#endregion

		#region Execute
		public int ExecuteNonQuery(string cmdText) { return CreateCommand(cmdText).ExecuteNonQuery(); }
		public int ExecuteNonQuery(string cmdText, DbTransaction trans) { return CreateCommand(cmdText, trans).ExecuteNonQuery(); }
		public DbDataReader ExecuteReader(GmCommand cmd) { return cmd.ExecuteReader(); }
		public DbDataReader ExecuteReader(string cmdText) { return CreateCommand(cmdText).ExecuteReader(); }
		public DbDataReader ExecuteReader(string cmdText, CommandBehavior behavior) { return CreateCommand(cmdText).ExecuteReader(behavior); }
		public object ExecuteScalar(string cmdText) { return CreateCommand(cmdText).ExecuteScalar(); }
		public object ExecuteScalar(GmCommand cmd) { return cmd.ExecuteScalar(); }
		public object ExecuteScalar(string cmdText, DbTransaction trans) { return CreateCommand(cmdText, trans).ExecuteScalar(); }
		public DbDataReader ExecuteReaderById(string cmdId) { return CreateCommandById(cmdId).ExecuteReader(); }
		public DbDataReader ExecuteReaderById(string cmdId, CommandBehavior behavior) { return CreateCommandById(cmdId).ExecuteReader(behavior); }
		public object ExecuteScalarById(string cmdId) { return CreateCommandById(cmdId).ExecuteScalar(); }
		public DbDataAdapter CreateDataAdapter() 
		{
			return ProviderFactory.DbProviderFactory.CreateDataAdapter();
		}
		public DbDataAdapter CreateDataAdapter(string cmdText, DbTransaction trans) 
		{
			GmCommand cmd = CreateCommand(cmdText, trans);
			return CreateDataAdapter(cmd);
		}
		public DbDataAdapter CreateDataAdapter(string cmdText) { return CreateDataAdapter(cmdText, null); }
		public DbDataAdapter CreateDataAdapter(GmCommand cmd)
		{
			DbDataAdapter da = ProviderFactory.DbProviderFactory.CreateDataAdapter();
			da.SelectCommand = cmd.DbCommand;
			return da;
		}
		public DbDataAdapter Fill(DataTable dt, string cmdText)
		{
			DbDataAdapter da = CreateDataAdapter(cmdText);
			da.Fill(dt);
			return da;
		}
		public DbDataAdapter Fill(DataTable dt, GmCommand cmd)
		{
			DbDataAdapter da = CreateDataAdapter(cmd);
			da.Fill(dt);
			return da;
		}
		#endregion

		#region Utils
/*		public int GetId(int tableId)
		{
			return IdGenerator.Instance.Get(this,tableId);
		}*/

        public void PreProcessCommandText( ref string cmdText )
        {
			cmdText = ProviderFactory.PreProcessCommandText(cmdText);
        }
		#endregion

        

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			Close();
		}

		#endregion

		public GmTransaction BeginTransaction(){return BeginTransaction(IsolationLevel.Serializable);}
		public GmTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return new GmTransaction(this, isolationLevel);
		}
		public void Fill(Hashtable ht, string cmdText)
		{
			using (DbDataReader dr = ExecuteReader(cmdText))
			{
				while (dr.Read())
				{
					ht.Add(dr[0], dr[1]);
				}
			}
		}
	}
}
