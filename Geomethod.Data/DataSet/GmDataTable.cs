using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class GmDataTable
	{
		DataTable dataTable;
		GmDataSet dataSet;
		public DataTable DataTable { get { return dataTable; } }
		public string TableName { get { return dataTable.TableName; } }
		public GmDataSet DataSet { get { return dataSet; } }

//        /*internal*/ public  SQLMappingStatus mapstatus;

		#region Construction
        internal GmDataTable( DataTable dataTable, GmDataSet dataSet/*, SQLMappingStatus mapstatus*/ )
        {
            this.dataTable = dataTable;
            this.dataSet = dataSet;

            //            this.mapstatus = mapstatus;
        }
/*
        internal GmDataTable( DataTable dataTable, GmDataSet dataSet, SQLMappingStatus mapstatus )
        {
            this.dataTable = dataTable;
            this.dataSet = dataSet;

//            this.mapstatus = mapstatus;
        }
 */ 
        public GmDataTable( DataTable dataTable /*, SQLMappingStatus mapstatus*/ )
            : this( dataTable, null /*, mapstatus*/ )
        {
        }
/*
        public GmDataTable( SQLMappingStatus mapstatus )
            : this( new DataTable( ), null, mapstatus )
        {
        }
 */ 
        public GmDataTable(  )
            : this( new DataTable( ), null /*, SQLMappingStatus.New*/ )
        {
        }
        #endregion

		#region Restructurization
        public void AddColumn( RestructurizationTransaction rtrans, GmDataColumn dataColumn,
                    SQLMappingProperty props, SQLMappingLog log )
        {
            if( rtrans.Transaction != null )
                SQLAlterTableAddColumn( rtrans, props, log, dataColumn );

            dataTable.Columns.Add( dataColumn );
        }

        public void RemoveColumn( RestructurizationTransaction rtrans, GmDataColumn dataColumn,
                    SQLMappingProperty props, SQLMappingLog log )
        {
            if( rtrans.Transaction != null )
                SQLAlterTableDropColumn( rtrans, props, log, dataColumn );

            dataTable.Columns.Remove( dataColumn );
        }

        public void RenameColumn( RestructurizationTransaction rtrans, ref GmDataColumn dataColumn, string newname, 
            SQLMappingProperty props, SQLMappingLog log )
        {
            if( rtrans.Transaction != null )
                SQLAlterTableRenameColumn( rtrans, props, log, ref dataColumn, newname  );

            dataColumn.DataColumn.ColumnName = newname;
//            dataTable.Columns.Remove( dataColumn );
        }

		#endregion

		#region Methods
		public IEnumerable<GmDataColumn> GetGmDataColumns()
		{
			List<GmDataColumn> list = new List<GmDataColumn>();
			foreach (DataColumn dataColumn in dataTable.Columns)
			{
				list.Add(new GmDataColumn(dataColumn, this));
			}
			return list;
		}
		#endregion
/*		public static implicit operator DataTable(GmDataTable dataTable)
		{
			return dataTable.DataTable;
        }
*/

        #region SQLGeneration

/*        public string SQLCreateTable( GmProviderFactory provider, SQLMappingProperty props )
        {
            string sql = "CREATE TABLE ";

            sql += provider.GetEnclosedName( TableName, props.EncloseName );
            sql += " ( ";

            bool first = true;
            foreach( DataColumn dc in this.DataTable.Columns )
            {

                if( !first )
                    sql += ", ";
                else
                    first = false;

                sql += provider.GetEnclosedName( dc.ColumnName, props.EncloseName );

                sql += " " + provider.TypeMapping( dc.DataType, dc.MaxLength );
//                if( dc.MaxLength > 0 )
//                    str += "( " + dc.MaxLength.ToString() + ")";

                if( props.GenerateNotNull )
                    sql += " NOT NULL";

                if( props.GenerateUnique )
                    sql += " UNIQUE";
            }
            return sql + " )";
        }
*/

/*        public string SQLDropTable( GmProviderFactory provider, SQLMappingProperty props )
        {
            string sql = "DROP TABLE ";
            sql += provider.GetEnclosedName( TableName, props.EncloseName );
            return sql;
        }
*/
/*        public string SQLAlterTableAddColumn( GmProviderFactory provider, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += provider.GetEnclosedName( TableName, props.EncloseName );
            sql += " ADD ( ";
            sql += provider.GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + provider.TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";
            sql += " ) ";
            return sql;
        }
*/
/*        public string SQLAlterTableDropColumn( GmProviderFactory provider, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += provider.GetEnclosedName( TableName, props.EncloseName );
            sql += " DROP COLUMN ";
            sql += provider.GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            return sql;
        }
*/
/*
        public string SQLAlterTableRenameColumn( GmProviderFactory provider, SQLMappingProperty props, GmDataColumn column, 
                string newname )
        {
            string sql = "ALTER TABLE ";
            sql += provider.GetEnclosedName( TableName, props.EncloseName );
            sql += " RENAME COLUMN ";
            sql += provider.GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " TO ";
            sql += provider.GetEnclosedName( newname, props.EncloseName );

            return sql;
        }
*/
        public void SQLCreateTable( RestructurizationTransaction tr, SQLMappingProperty props, SQLMappingLog log )
        {
            string sql = tr.Connection.ProviderFactory.SQLCreateTable( DataTable, props );
            log.Write( sql );
            GmCommand cmd = tr.Transaction.CreateCommand( sql );         
            cmd.ExecuteNonQuery( );


            if (!tr.Connection.ProviderFactory.SupportsProperty(GmProviderProperty.DDLRollback))
            {
                tr.Transaction.rollbackCommand.Push( "DROP TABLE " + 
                    tr.Connection.ProviderFactory.GetEnclosedName( DataTable.TableName, props.EncloseName ) );
            }

        }

        public void SQLDropTable( RestructurizationTransaction tr, SQLMappingProperty props, SQLMappingLog log )
        {
            string sql = tr.Connection.ProviderFactory.SQLDropTable( DataTable, props );
            log.Write( sql );
            GmCommand cmd = tr.Transaction.CreateCommand( sql );
            cmd.ExecuteNonQuery( );
        }
        public void SQLAlterTableAddColumn( RestructurizationTransaction tr, SQLMappingProperty props, SQLMappingLog log, GmDataColumn column )
        {
            string sql = tr.Connection.ProviderFactory.SQLAlterTableAddColumn( DataTable, props, column );
            log.Write( sql );
            GmCommand cmd = tr.Transaction.CreateCommand( sql );
            cmd.ExecuteNonQuery( );
        }
        public void SQLAlterTableDropColumn( RestructurizationTransaction tr, SQLMappingProperty props, SQLMappingLog log, GmDataColumn column )
        {
            string sql = tr.Connection.ProviderFactory.SQLAlterTableDropColumn( DataTable, props, column );
            log.Write( sql );
            GmCommand cmd = tr.Transaction.CreateCommand( sql );
            cmd.ExecuteNonQuery( );
        }

        public void SQLAlterTableRenameColumn( RestructurizationTransaction tr, SQLMappingProperty props, SQLMappingLog log, ref GmDataColumn column, string newname )
        {
            string sql = tr.Connection.ProviderFactory.SQLAlterTableRenameColumn( DataTable, props, column, newname );
            log.Write( sql );
            GmCommand cmd = tr.Transaction.CreateCommand( sql );
            cmd.ExecuteNonQuery( );

            column.DataColumn.ColumnName = newname;
        }

        /*        public void SQLMap( GmConnection conn, SQLMappingProperty props )
                        {
                            if( mapstatus == SQLMappingStatus.New )
                            {
                                SQLCreateTable( conn, props );
                                mapstatus = SQLMappingStatus.Done;
                                return;
                            }
                            if( mapstatus == SQLMappingStatus.Add )
                            {
                                SQLAlterTable( conn, props );
                                mapstatus = SQLMappingStatus.Done;
                                return; 
                            }
                            if( mapstatus == SQLMappingStatus.Drop )
                            {
                                SQLDropTable( conn, props );
                                mapstatus = SQLMappingStatus.Done;
                                return;
                            }
                        }
                */

        #endregion
    }
}
