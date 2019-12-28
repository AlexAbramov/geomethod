using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class GmDataColumn
	{
		DataColumn dataColumn;
		GmDataTable dataTable;
		public DataColumn DataColumn { get { return dataColumn; } }
		public GmDataTable DataTable { get { return dataTable; } }
//        internal SQLMappingStatus mapstatus;

		#region Construction
		internal GmDataColumn(DataColumn dataColumn, GmDataTable dataTable) 
		{
			this.dataColumn = dataColumn;
			this.dataTable = dataTable;

//            mapstatus = SQLMappingStatus.Done;

		}
		public GmDataColumn(DataColumn dataColumn): this(dataColumn,null)
		{
		}
		#endregion

		#region Restructurization
/*		public void SetMaxLength(RestructurizationTransaction rtrans, int maxLength)
		{
			if (rtrans.Transaction != null)
			{
				StringBuilder sb = new StringBuilder("alter table @TableName add column... ");
				string cmdText = sb.ToString();
				GmCommand cmd = rtrans.Transaction.CreateCommand(cmdText);
				cmd.AddString("TableName", dataTable.TableName);
				cmd.ExecuteNonQuery();
  
			}
			dataColumn.MaxLength = maxLength;
		}*/
		#endregion

		public static implicit operator DataColumn(GmDataColumn dataColumn)
		{
			return dataColumn.DataColumn;
		}

	}
}
