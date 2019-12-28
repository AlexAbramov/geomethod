using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Windows.Forms
{
	public class GridViewUtils
	{
		public static DataRow GetSelectedRow(DataGridView gridView)
		{
			if (gridView.SelectedRows.Count > 0)
			{ 
				DataRowView drv = gridView.SelectedRows[0].DataBoundItem as DataRowView;
				if (drv != null) return drv.Row;
			}
			return null;
		}
        public static object GetSelectedDataBoundItem(DataGridView gridView)
        {
            if (gridView.SelectedRows.Count > 0)
            {
                return gridView.SelectedRows[0].DataBoundItem;
            }
            return null;
        }
        public static DataTable GetDataTable(DataGridView gridView)
		{
			if (gridView.DataSource is DataTable) return gridView.DataSource as DataTable;
			if (gridView.DataSource is DataView) return (gridView.DataSource as DataView).Table;
			return null;
		}
		public static bool SetCurrentRow(DataGridView gridView, DataRow dataRow)
		{
			foreach (DataGridViewRow r in gridView.Rows)
			{
				DataRowView drv=r.DataBoundItem as DataRowView;
				if (drv != null && drv.Row == dataRow)
				{
					gridView.CurrentCell = r.Cells[0];
					return true;
				}
			}
			return false;
		}
		public static bool SetCurrentRow(DataGridView gridView, int id)
		{
			foreach (DataGridViewRow r in gridView.Rows)
			{
				DataRowView drv = r.DataBoundItem as DataRowView;
				if (drv != null && (int)drv.Row["Id"] == id)
				{
					gridView.CurrentCell = r.Cells[0];
					return true;
				}
			}
			return false;
		}
		public static bool SetCurrentRow(DataGridView gridView, string filterExpression)
		{
			DataTable dataTable = GetDataTable(gridView);
			if (dataTable != null)
			{
				DataRow[] dataRows = dataTable.Select(filterExpression);
				if (dataRows.Length > 0)
				{
					return SetCurrentRow(gridView, dataRows[0]);
				}
			}
			return false;
		}
		public static object GetSelectedItem(DataGridView gridView)
		{
			if (gridView.SelectedRows.Count > 0)
			{
				return gridView.SelectedRows[0].DataBoundItem;
			}
			return null;
		}

	}
}
