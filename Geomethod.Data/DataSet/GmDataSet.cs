using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class GmDataSet
	{
		#region Fields
//		DataSet dataSet;
        public List<GmDataTable> tables = new List<GmDataTable>( );

//        List<SQLMappingStatus> tablestati;

//        private SQLMapping tablestati;

        public SQLMappingLog log = null;

		#endregion

		#region Properties
//		public DataSet DataSet { get { return dataSet; } }
		#endregion

		#region Construction
		public GmDataSet()
		{
//			dataSet = new DataSet();
//            tablestati = new SQLMapping( );
		}
		public GmDataSet(DataSet dataSet)
		{
//			this.dataSet = dataSet;
//            tablestati = new SQLMapping( );
		}
		#endregion 

		#region Methods
		public IEnumerable<GmDataTable> GetDataTables()
		{
            return tables;
/*
			List<GmDataTable> list = new List<GmDataTable>();
			foreach (DataTable dataTable in dataSet.Tables)
//            for( int i = 0; i < dataSet.Tables.Count; i++ )
            {
                list.Add(new GmDataTable(dataTable, this ));
//                list.Add( new GmDataTable( dataTable, this, tablestati[ dataTable.TableName ] )  );
            }
			return list;
 */ 
		}
		public void Clear()
		{
//			dataSet.Clear();
//			dataSet.Tables.Clear();
//			dataSet.Relations.Clear();
//            tablestati.Clear( );
            tables.Clear( );
		}
		#endregion

		#region Serialization
		public void WriteXmlSchema(Stream stream)
		{
            
//			dataSet.WriteXmlSchema(stream);
		}
		public void ReadXmlSchema(Stream stream)
		{
//			dataSet.ReadXmlSchema(stream);
		}
		#endregion

		#region Restructurization
        public void AddTable( RestructurizationTransaction rtrans, SQLMappingProperty props,
                ref GmDataTable dataTable )
        {
            if( rtrans.Transaction != null )
            {
                dataTable.SQLCreateTable( rtrans, props, log );
            }
            //			dataSet.Tables.Add(dataTable.DataTable);
            tables.Add( dataTable );

//            dataTable.mapstatus = SQLMappingStatus.Done;

        }


        public void RemoveTable( RestructurizationTransaction rtrans, SQLMappingProperty props,
            ref GmDataTable dataTable 
            
            )
		{
			if (rtrans.Transaction != null)
			{
                dataTable.SQLDropTable( rtrans, props, log );
			}
			tables.Remove( dataTable );

//            dataTable.mapstatus = SQLMappingStatus.Drop;
		}


        public void SQLMap( GmConnection conn, SQLMappingProperty props )
        {
//            foreach( GmDataTable gdt in  GetDataTables( )  )
//            for( int i  = 0; i < dataSet.Tables.Count; i++ )
            {

//                gdt.SQLMap( conn, props );
/*                if( gdt.mapstatus == SQLMappingStatus.Drop )
                {
//                    dataSet.Tables.Remove( gdt );
                    tables.Remove( gdt );
                }
 */ 
            }
      
        }
		#endregion

	}
}

