using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Geomethod.Data
{
	/// <summary>
	/// Summary description for Sql.
	/// </summary>
	public class SqlServerProvider : GmProviderFactory
	{
        public const string name = "MS SQL Server";
        const string selIdentity = " SELECT @@IDENTITY";

		public SqlServerProvider()
			: base(SqlClientFactory.Instance)
		{
        }

		protected SqlServerProvider(DbProviderFactory dbProviderFactory)
			: base(dbProviderFactory)
		{
		}

        public override string Name { get { return name; } }
		public override string ParameterPrefix{ get { return "@"; } }
		public override string FileExtension{get { return ".mdb"; }	}
        public override string NameLeftEnclose{get{ return "[";}}
        public override string NameRightEnclose{get{return "]";}}

		public override bool SupportsProperty(GmProviderProperty prop)
		{
			return true;
		}

        #region SQLTypesMapping
        public override string MapString( int length )
        {
            return "varchar" + "(" + length + ")";
        }
        public override string MapInt32( )
        {
            return "int";
        }
        public override string MapDecimal( )
        {
            return "decimal";
        }
        public override string MapSingle( )
        {
            return "real";            
        }
        public override string MapDouble( )
        {
            return "float";
        }
        public override string MapDateTime( )
        {
            return "DateTime";
        }
        public override string MapByte( )
        {
            return "TinyInt";
        }

        #endregion

        public static string GetConnectionString(LoginData loginData)
        {
            if (loginData.integratedSecurity)
            {
                return "Integrated Security=true;";
            }
            else
            {
                return string.Format("User ID={0};Password={1};", loginData.login,loginData.password);
            }
        }

		public override string CreateDatabase(DbCreationProperties props)
		{
            // create admin connection string
//            SqlConnectionStringBuilder adminConnStr = new SqlConnectionStringBuilder();
	        StringBuilder sb = new StringBuilder(1024);
            sb.Append(GetConnectionString(props.adminLogin));
            sb.AppendFormat("Server={0};", props.serverName);
            string adminConnStr = sb.ToString();
            props.adminConnStr = adminConnStr;
            using (GmConnection conn = CreateConnection(adminConnStr))
			{
                // create the database
                GmCommand cmd = conn.CreateCommand();
                sb.Length = 0;
                sb.AppendFormat("create database {0}",props.dbName);
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();

                // create login
                if (!props.userLogin.integratedSecurity)
                {
                    cmd = conn.CreateCommand();
                    sb.Length = 0;
                    sb.AppendFormat("CREATE LOGIN {0} WITH PASSWORD='{1}', DEFAULT_DATABASE = {2};", 
                        props.userLogin.login, props.userLogin.password, props.dbName);
                    sb.AppendLine();
                    sb.AppendFormat("USE {0};",props.dbName);
                    sb.AppendLine();
                    sb.AppendFormat("CREATE USER {0};", props.userLogin.login);
                    sb.AppendLine();
                    sb.AppendFormat("EXEC sp_addrolemember N'db_owner', N'{0}';", props.userLogin.login);
                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();
                }
			}
            // create user connection string
            sb.Length = 0;
            sb.Append(GetConnectionString(props.userLogin));
            sb.AppendFormat ("Server={0};", props.serverName);
            sb.AppendFormat("Initial Catalog={0};", props.dbName);
            string userConnStr = sb.ToString();
            return userConnStr;
		}

        public override string PreProcessCommandText( string cmdText )
        {
            return cmdText;
        }


        #region SQLMapping
        
        
        public override string SQLAlterTableAddColumn( DataTable dt, SQLMappingProperty props, 
                GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ADD ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";
            return sql;
        }
        public override string SQLAlterTableRenameColumn( DataTable dt, SQLMappingProperty props,
                GmDataColumn column, string newname )
        {
            string sql = "EXEC sp_rename ";
            sql += GetEnclosedName( dt.TableName + "." + column.DataColumn.ColumnName, props.EncloseName );
            sql += " , ";
            sql += GetEnclosedName( newname, props.EncloseName );
            sql += " , 'COLUMN'";

            return sql;
        }

        public override string SQLAlterTableModifyColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ALTER COLUMN ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";

            //ALTER TABLE MyTable ALTER COLUMN NullCOl NVARCHAR(20) NOT NULL
            return sql;
        }
        #endregion
    }
}
