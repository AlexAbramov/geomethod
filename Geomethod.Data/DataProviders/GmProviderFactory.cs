using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
    public enum GmProviderProperty { IntegratedSecurity, Server, AutoFileCreation, FileSize, FileMaxSize, FileGrowth, Transaction, DDLRollback }

	public abstract class GmProviderFactory
	{
		DbProviderFactory dbProviderFactory;
		protected GmProviderFactory(DbProviderFactory dbProviderFactory) { this.dbProviderFactory = dbProviderFactory;}
		public DbProviderFactory DbProviderFactory { get { return dbProviderFactory; } }

        #region Methods
        public abstract bool SupportsProperty(GmProviderProperty prop);
		public abstract string Name { get; }
		public abstract string ParameterPrefix { get;}
		public abstract string FileExtension { get;}
		public abstract string CreateDatabase(DbCreationProperties props);
        public abstract string NameLeftEnclose{ get; }
        public abstract string NameRightEnclose{ get; }
        public abstract string PreProcessCommandText( string cmdText );
        public string GetEnclosedName( string name, bool enclose ){if( !enclose ) return name; return NameLeftEnclose + name + NameRightEnclose;}
        public GmConnection CreateConnection(string connStr) { return CreateConnectionFactory(connStr).CreateConnection(); }
        public ConnectionFactory CreateConnectionFactory(string connStr) { return new ConnectionFactory(this, connStr); }
        #endregion

        #region SQLMapping
        public abstract string SQLAlterTableAddColumn(DataTable dt, SQLMappingProperty props, GmDataColumn column);
        public abstract string SQLAlterTableRenameColumn(DataTable dt, SQLMappingProperty props, GmDataColumn column, string newname);
        public abstract string SQLAlterTableModifyColumn(DataTable dt, SQLMappingProperty props, GmDataColumn column);
        public abstract string MapString(int length);
        public abstract string MapInt32();
        public abstract string MapDecimal();
        public abstract string MapSingle();
        public abstract string MapDouble();
        public abstract string MapDateTime();
        public abstract string MapByte();
        #endregion

        #region SQLTypesMapping
        public string SQLCreateTable(DataTable dt, SQLMappingProperty props)
        {
            string sql = "CREATE TABLE ";

            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ( ";

            bool first = true;
            foreach( DataColumn dc in dt.Columns )
            {

                if( !first )
                    sql += ", ";
                else
                    first = false;

                sql += GetEnclosedName( dc.ColumnName, props.EncloseName );

                sql += " " + TypeMapping( dc.DataType, dc.MaxLength );
                //                if( dc.MaxLength > 0 )
                //                    str += "( " + dc.MaxLength.ToString() + ")";

                if( props.GenerateNotNull )
                    sql += " NOT NULL";

                if( props.GenerateUnique )
                    sql += " UNIQUE";
            }

            return sql + " )";
        }
        public string SQLDropTable( DataTable dt, SQLMappingProperty props )
        {
            string sql = "DROP TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            return sql;
        }
        public string SQLAlterTableDropColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " DROP COLUMN ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            return sql;
        }
        public string TypeMapping( Type type, int length )
        {

            if( type == System.Type.GetType( "System.String" ) )
                return MapString( length );

            if( type == System.Type.GetType( "System.Int32" ) )
                return MapInt32();

            if( type == System.Type.GetType( "System.Decimal" ) )
                return MapDecimal();

            if( type == System.Type.GetType( "System.Single" ) )
                return MapSingle();

            if( type == System.Type.GetType( "System.Double" ) )
                return MapDouble();

            if( type == System.Type.GetType( "System.DateTime" ) )
                return MapDateTime();

            if( type == System.Type.GetType( "System.Byte" ) )
                return MapByte();

            throw new SQLMappingException( "Type " + type.ToString( ) + " not mapped" );
        }

        #endregion


		public virtual DbType FixDbParameterType(DbType dbType)
		{
			return dbType;
		}
	}

}