using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;


namespace Geomethod.Data
{
    public class OracleProvider : GmProviderFactory
    {
        public const string name = "Oracle Database";
        const string TableSpaceName = "GISSTORAGE";

        public OracleProvider()
            : base(  OracleClientFactory.Instance )
		{
		}

        public override string Name { get { return name; } }

        public override bool SupportsProperty( GmProviderProperty prop )
        {
            switch(prop)
            {
                case GmProviderProperty.IntegratedSecurity: return false;
                default: return true;
            }
        }
        public override string ParameterPrefix
        {
            get
            {
                return ":";
            }
        }
        public override string FileExtension
        {
            get
            {
                return ".dbf";
            }
        }
        public override string NameLeftEnclose
        {
            get
            {
                return "\"";
            }
        }
        public override string NameRightEnclose
        {
            get
            {
                return "\"";
            }
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
            return "number";
        }
        public override string MapSingle( )
        {
            return "number";
        }
        public override string MapDouble( )
        {
            return "number";
        }
        public override string MapDateTime( )
        {
            return "Date";
        }
        public override string MapByte( )
        {
            return "BLOB";
        }
        #endregion


        private bool CreateTableSpace( GmConnection conn )
        {
            GmCommand cmd = conn.CreateCommand( );
            cmd.CommandText = "CREATE TABLESPACE " + TableSpaceName +
                    " DATAFILE 'GISSTORAGE.dbf' " +
                    "SIZE 30M AUTOEXTEND ON NEXT 5M MAXSIZE UNLIMITED ONLINE";
            cmd.ExecuteNonQuery( );
            return true;
        }

        private bool CheckTableSpace( GmConnection conn )
        {
            GmCommand cmd = conn.CreateCommand( );
            cmd.CommandText = "select count(*) from dba_tablespaces " +
                "where TABLESPACE_NAME = upper( \'" + TableSpaceName + "\' )";
            decimal cnt = (decimal)cmd.ExecuteScalar( );
            return  cnt == 1;
            
        }

        private bool CheckUser( GmConnection conn, string username  )
        {
            GmCommand cmd = conn.CreateCommand( );
            cmd.CommandText = "select count(*) from all_users " +
                "where USERNAME = upper( \'" + username + "\' )";
            decimal cnt = (decimal)cmd.ExecuteScalar( );
            return cnt == 1;
        }

        private static void CreateUser( DbCreationProperties props, GmConnection conn )
        {
            StringBuilder sb1 = new StringBuilder( );
            sb1.AppendFormat(
                "create user {0} identified by {1} DEFAULT TABLESPACE " + TableSpaceName,
                props.userLogin.login, props.userLogin.password );

            GmCommand cmd = conn.CreateCommand( );
            cmd.CommandText = sb1.ToString( );
            cmd.ExecuteNonQuery( );

            cmd.CommandText = "grant resource, create session to " + props.userLogin.login;
            cmd.ExecuteNonQuery( );
        }

        public static string GetConnectionString( LoginData loginData )
        {
            if( loginData.integratedSecurity )
            {
                return "Integrated Security=true;";
            }
            else
            {
                return string.Format( "User ID={0};Password={1};", loginData.login, loginData.password );
            }
        }

        private string CreateUser( DbCreationProperties props )
        {
            // create admin connection string
	        StringBuilder sb = new StringBuilder(1024);
            sb.Append(GetConnectionString(props.adminLogin));
            sb.AppendFormat("Server={0};", props.serverName);
//            ConnectionString = sb.ToString();
            string adminConnStr = sb.ToString( );

/*            OracleConnectionStringBuilder osb = new OracleConnectionStringBuilder( );
            osb.DataSource = props.serverName;
            osb.UserID = props.adminLogin.login;
            osb.Password = props.adminLogin.password;
*/
            using( GmConnection conn = CreateConnection( adminConnStr  ) )
            {
                if( !CheckTableSpace( conn ) )
                    CreateTableSpace( conn );

                if( !CheckUser( conn, props.userLogin.login ) )
                    CreateUser( props, conn );
            }

            OracleConnectionStringBuilder osb1 = new OracleConnectionStringBuilder( );
            osb1.DataSource = props.serverName;
            osb1.UserID = props.userLogin.login;
            osb1.Password = props.userLogin.password;

//            ConnectionString = osb1.ConnectionString;
            return osb1.ConnectionString;


            
/*            OracleConnectionStringBuilder osb = new OracleConnectionStringBuilder( );
            osb.DataSource = props.serverName;
            osb.UserID = props.adminLogin.login;
            osb.Password = props.adminLogin.password;

//      Сначала создадим TABLESPACE
            try
            {
  
                    DbConnection conn = DbProviderFactory.CreateConnection( ) )
                {
                    conn.ConnectionString = osb.ConnectionString;
                    conn.Open( );
                    IDbCommand cmd = conn.CreateCommand( );
                    cmd.CommandText = "CREATE TABLESPACE " + TableSpaceName +
                                        "DATAFILE 'GISSTORAGE.dbf' " +
                                        "SIZE 30M AUTOEXTEND ON NEXT 5M MAXSIZE UNLIMITED ONLINE";
                    cmd.ExecuteNonQuery( );
                }
            }
            catch( OracleException ex )
            {
                if( ex.Code != 1543 )       //      TB уже существует
                    throw ex;
            }

            using( DbConnection conn = DbProviderFactory.CreateConnection( ) )
            {
                // Create a user 
                StringBuilder sb = new StringBuilder( );
                sb.AppendFormat( 
                    "create user {0} identified by {1} DEFAULT TABLESPACE " + TableSpaceName,
                    props.userLogin.login, props.userLogin.password );

                conn.ConnectionString = osb.ConnectionString;
                conn.Open( );
                IDbCommand cmd = conn.CreateCommand( );
                cmd.CommandText = sb.ToString( );
                cmd.ExecuteNonQuery( );

                cmd.CommandText = "grant resource, create session to " + props.userLogin.login;
                cmd.ExecuteNonQuery( );

            }
            OracleConnectionStringBuilder osb1= new OracleConnectionStringBuilder( );
            using( DbConnection conn = DbProviderFactory.CreateConnection( ) )
            {
                osb1.DataSource = props.serverName;
                osb1.UserID = props.userLogin.login;
                osb1.Password = props.userLogin.password;

                conn.ConnectionString = osb1.ConnectionString;
                conn.Open( );
            }

            return osb1.ConnectionString;
*/
        }

        public override string CreateDatabase( DbCreationProperties props )
        {
            return CreateUser( props );
        }
        public override string PreProcessCommandText( string cmdText )
        {
            char[] tokens = "\n\r\t ,;".ToCharArray();
            StringBuilder sb = new StringBuilder(cmdText.Length);
            int prevTokenIndex=-1;
            while (true)
            {
                string word;
                int tokenIndex = cmdText.IndexOfAny(tokens, prevTokenIndex + 1);
                if (tokenIndex >= 0)
                {
                    word = cmdText.Substring(prevTokenIndex + 1, tokenIndex - prevTokenIndex-1);
                }
                else
                {
                    word = cmdText.Substring(prevTokenIndex + 1);
                }

                if (word.Length > 0)
                {
                    word = FixWord(word);
                    sb.Append(word);
                }
                if (tokenIndex >= 0)
                {
                    char token = cmdText[tokenIndex];
                    sb.Append(token);
                    prevTokenIndex = tokenIndex;
                }
                else break;
            }
            string res = sb.ToString();
            return res;
        }

        private string FixWord(string word)
        {
            if (word.StartsWith("varchar(max)", StringComparison.OrdinalIgnoreCase)) return "CLOB";
            if (word.StartsWith("varbinary", StringComparison.OrdinalIgnoreCase)) return "BLOB";
            if (word.StartsWith("binary", StringComparison.OrdinalIgnoreCase)) return "BLOB";
            if (word.StartsWith("datetime", StringComparison.OrdinalIgnoreCase)) return "DATE";
            if (word.StartsWith("@")) return ":" + word.Substring(1);
            if (word.StartsWith("[") && word.EndsWith("]")) return '"' +word.Substring(1,word.Length-2)+ '"';
            return word;
        }

        #region SQLMapping

        public override string SQLAlterTableAddColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ADD ( ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";
            sql += " ) ";
            return sql;
        }
        public override string SQLAlterTableRenameColumn( DataTable dt, SQLMappingProperty props, 
                GmDataColumn column, string newname )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " RENAME COLUMN ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " TO ";
            sql += GetEnclosedName( newname, props.EncloseName );

            return sql;
        }

        public override string SQLAlterTableModifyColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " MODIFY ( ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";
            sql += " ) ";
            //  ALTER TABLE countries MODIFY (duty_pct NUMBER(3,2));
            return sql;
        }
        #endregion 


    }

}
