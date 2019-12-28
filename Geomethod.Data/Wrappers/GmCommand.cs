using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class GmCommand
	{
		#region Fields
		GmConnection conn;
		DbCommand cmd;
		#endregion

		#region Properties
		public GmConnection Connection { get { return conn;} set { conn = value;} }
		public DbCommand DbCommand{get{return cmd;}}
		public string CommandText{get{return cmd.CommandText;}set{SetCommandText(value);}}
		public DbTransaction DbTransaction { get { return cmd.Transaction;} set { cmd.Transaction = value;} }
		#endregion

		#region Construction
		public GmCommand(GmConnection conn)
		{
			this.conn = conn;
			this.cmd = conn.DbConnection.CreateCommand();
		}
		public GmCommand(GmConnection conn, DbTransaction trans)
		{
			this.conn = conn;
			this.cmd = conn.DbConnection.CreateCommand();
			cmd.Transaction = trans;
		}
		public GmCommand(GmConnection conn, string cmdText)
		{
			this.conn = conn;
			this.cmd = conn.DbConnection.CreateCommand();
			SetCommandText(cmdText);
		}
		public GmCommand(GmConnection conn, string cmdText, DbTransaction trans)
		{
			this.conn = conn;
			this.cmd = conn.DbConnection.CreateCommand();
			SetCommandText(cmdText);
			cmd.Transaction = trans;
		}
		#endregion

		#region Utils
		void SetCommandText(string cmdText)
		{
			cmdText=cmdText.Trim();
			if (SqlQueries.StringSet.IsLoaded && cmdText.StartsWith("_")) cmdText = SqlQueries.Get(cmdText);
			if(IsStoredProcedure(cmdText))
			{
				cmd.CommandType=CommandType.StoredProcedure;
			}
			else 
                conn.PreProcessCommandText(ref cmdText);
			cmd.CommandText=cmdText;
		}
		static bool IsStoredProcedure(string s)
		{
			bool breakOpened=false;
			bool symbolFound=false;
			for(int i=0;i<s.Length;i++)
			{
				switch(s[i])
				{
					case '[':
						if(i>0) return false;
						breakOpened=true;
						break;
					case ']':
						if(!breakOpened) return false;
						breakOpened=false;
						break;
					case ' ':
						if(!breakOpened) return false;
						break;
					case '(':
					case ')':
						return false;
					default:
						symbolFound=true;
						break;
				}
			}
			return symbolFound && !breakOpened;
		}
		void CheckConn()
		{
			if (conn.DbConnection.State == ConnectionState.Closed) conn.DbConnection.Open();
		}
		#endregion

		#region Execute
		public int ExecuteNonQuery() { CheckConn(); return cmd.ExecuteNonQuery();}
		public DbDataReader ExecuteReader() { CheckConn(); return cmd.ExecuteReader();}
		public DbDataReader ExecuteReader(CommandBehavior behavior) { CheckConn(); return cmd.ExecuteReader(behavior);}
		public object ExecuteScalar() { CheckConn(); return cmd.ExecuteScalar();}
		public DbDataAdapter CreateDataAdapter(){	return conn.ProviderFactory.DbProviderFactory.CreateDataAdapter();}
        public int ExecuteScalarInt32( )
        {
            CheckConn();
			object obj = cmd.ExecuteScalar();			
            return obj==DBNull.Value? 0 : Convert.ToInt32(obj);
        }
		public void Fill(DataTable dataTable)
		{
			conn.Fill(dataTable, this);
		}
		#endregion

		#region Parameters
		DbParameter CreateParameter(DbType dbType, string parName)
		{
			string prefix=conn.ProviderFactory.ParameterPrefix;
			if (!parName.StartsWith(prefix))
				parName = prefix + parName;

			DbParameter par = cmd.Parameters.Contains(parName)?cmd.Parameters[parName]:null;
			if (par == null)
			{
				par = cmd.CreateParameter();
                par.DbType = conn.ProviderFactory.FixDbParameterType(dbType);
				par.ParameterName = parName;
				cmd.Parameters.Add(par);
			}
			return par;
		}
		public DbParameter CreateParameter(DbType dbType, string parName, object val)
		{
			DbParameter par = CreateParameter(dbType, parName);
			par.Value = val;
			return par;
		}
		public DbParameter CreateParameter(DbType dbType, string parName, object val, int size)
		{
			DbParameter par = CreateParameter(dbType,parName,val);
			par.Size = size;
			return par;
		}
        public DbParameter AddByte(Enum parName, byte val) { return AddByte(parName.ToString(), val); }
        public DbParameter AddByte(string parName, byte val) { return CreateParameter(DbType.Byte, parName, val); }
        public DbParameter AddInt(Enum parName, int val) { return AddInt(parName.ToString(), val); }
		public DbParameter AddInt(string parName, int val) { return CreateParameter(DbType.Int32, parName, val);}
		public DbParameter AddNullableInt(Enum parName, int val) { return AddNullableInt(parName.ToString(), val);}
		public DbParameter AddNullableInt(string parName, int val) { return CreateParameter(DbType.Int32, parName, val == 0 ? (object)DBNull.Value : (object)val);}
		public DbParameter AddDecimal(Enum parName, decimal val) { return AddDecimal(parName.ToString(), val);}
		public DbParameter AddDecimal(string parName, decimal val) { return CreateParameter(DbType.Decimal, parName, val);}
		public DbParameter AddNullableBoolean(Enum parName, bool val) { return AddNullableBoolean(parName.ToString(), val);}
		public DbParameter AddNullableBoolean(string parName, bool val) { return CreateParameter(DbType.Boolean, parName, val == false ? (object)DBNull.Value : (object)val);}
		public DbParameter AddBoolean(Enum parName, bool val) { return AddBoolean(parName.ToString(), val);}
		public DbParameter AddBoolean(string parName, bool val) { return CreateParameter(DbType.Boolean, parName, val);}
		public DbParameter AddString(Enum parName, string val) { return AddString(parName.ToString(), val);}
		public DbParameter AddString(string parName, string val) { return CreateParameter(DbType.AnsiString, parName, val);}
		public DbParameter AddString(Enum parName, string val, int size) { return AddString(parName.ToString(), val, size);}
		public DbParameter AddString(string parName, string val, int size) { return CreateParameter(DbType.AnsiString, parName, val, size);}
		public DbParameter AddNullableString(Enum parName, string val) { return AddNullableString(parName.ToString(), val);}
		public DbParameter AddNullableString(string parName, string val) { return CreateParameter(DbType.AnsiString, parName, val.Length == 0 ? (object)DBNull.Value : (object)val);}
		public DbParameter AddNullableString(Enum parName, string val, int size) { return AddNullableString(parName.ToString(), val, size);}
		public DbParameter AddNullableString(string parName, string val, int size) { return CreateParameter(DbType.AnsiString, parName, val.Length == 0 ? (object)DBNull.Value : (object)val, size);}
		public DbParameter AddDateTime(Enum parName, DateTime val) { return AddDateTime(parName.ToString(), val);}
		public DbParameter AddDateTime(string parName, DateTime val)
        {
//      DZ      30.01.09,   17.10.09
            if( conn.ProviderFactory.Name.Contains( "Access" ) )
                return AddDateTime( parName, val.ToString() );//!!!
            
            return CreateParameter(DbType.DateTime,parName,val);

        }
        public DbParameter AddDateTime( string parName, string val )
        {
            return CreateParameter( DbType.String , parName, val );
        }
		public DbParameter AddNullableDateTime(Enum parName, DateTime val) { return AddNullableDateTime(parName.ToString(), val);}
		public DbParameter AddNullableDateTime(string parName, DateTime val) { return CreateParameter(DbType.DateTime, parName, val == DateTime.MinValue ? (object)DBNull.Value : (object)val);}
		public DbParameter AddGuid(Enum parName, Guid val) { return AddGuid(parName.ToString(), val);}
		public DbParameter AddGuid(string parName, Guid val) { return CreateParameter(DbType.Guid, parName, val);}
		public DbParameter AddBinary(Enum parName) { return AddBinary(parName.ToString());}
		public DbParameter AddBinary(string parName) { return CreateParameter(DbType.Binary, parName);}
		public DbParameter AddBinary(Enum parName, byte[] val) { return AddBinary(parName.ToString(), val);}
		public DbParameter AddBinary(string parName, byte[] val) { return CreateParameter(DbType.Binary, parName, val, val.Length);}
		#endregion
	}
}
