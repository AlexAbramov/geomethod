using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Geomethod.Data
{
	public class DataTableUtils
	{
/*		public static void RemoveItems(DataTable dataTable, List<int> removedItems)
		{
			List<DataRow> removedRows = new List<DataRow>();
			foreach (DataRow row in dataTable.Rows)
			{
				int id = (int)row[0];
				if (removedItems.Contains(id)) removedRows.Add(row);
			}
			foreach (DataRow row in removedRows)
			{
				dataTable.Rows.Remove(row);
			}
		}*/

		#region CSV
		public static void CsvExport(DataTable dataTable, StreamWriter sw, char sepToken)
		{
			List<DataColumn> exportedColumns = new List<DataColumn>();
			foreach (DataColumn dataColumn in dataTable.Columns) exportedColumns.Add(dataColumn);
			CsvExport(dataTable, sw, exportedColumns, sepToken);
		}
		public static void CsvExport(DataTable dataTable, StreamWriter sw, List<DataColumn> exportedColumns, char sepToken)
		{
			CsvConverter cc = new CsvConverter(sepToken);
			foreach (DataColumn dataColumn in exportedColumns)
			{
				sw.Write(cc.ToCsvString(dataColumn.ColumnName));
				sw.Write(sepToken);//Convert.ToChar(9)
			}
			sw.WriteLine();

			foreach (DataRow dataRow in dataTable.Rows)
			{
				if (dataRow.RowState == DataRowState.Deleted) continue;
				foreach (DataColumn dataColumn in exportedColumns)
				{
					object obj = dataRow[dataColumn];
					string s = obj.ToString();
					sw.Write(cc.ToCsvString(s));
					sw.Write(sepToken);//Convert.ToChar(9)
				}
				sw.WriteLine();
			}
		}
		#endregion

/*		public static DataTableReader CreateDataReader(DataTable dataTable, DataRow dataRow)
		{
			DataTable dt = new DataTable();
			ArrayList columns = new ArrayList(dataTable.Columns);
			dt.Columns.AddRange((DataColumn[])columns.ToArray(typeof(DataColumn)));
			dt.Rows.Add(dataRow);
			return dt.CreateDataReader();			
		}*/

		public static void Clear(DataTable dataTable)
		{
			dataTable.DefaultView.RowFilter = "";
			dataTable.DisplayExpression = "";
			dataTable.PrimaryKey = new DataColumn[] { };
			dataTable.Constraints.Clear();
			dataTable.ParentRelations.Clear();
			dataTable.ChildRelations.Clear();
			dataTable.Rows.Clear();
			dataTable.Columns.Clear();
			dataTable.Clear();

		}
	}
}
