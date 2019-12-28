using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Geomethod.Data;
using Geomethod.Windows.Forms;

namespace Geomethod.Windows.Forms
{
	public static class ComboBoxExtensions
	{
		static bool useCache = false;
		static Dictionary<string, DataTable> cache = new Dictionary<string, DataTable>();
		static DataTable GetTable(GmConnection conn, string cmdText)
		{
			DataTable dt = null;
			lock (cache)
			{
				if (useCache) dt = cache[cmdText] as DataTable;
				if (dt == null)
				{
					dt = new DataTable();
					conn.Fill(dt, cmdText);
					if (useCache) cache[cmdText] = dt;
				}
			}
			return dt;
		}
		public static DataTable Fill(this ComboBox cb, GmConnection conn, string cmdText) { return Fill(cb, conn, cmdText, null); }
		public static DataTable Fill(this ComboBox cb, GmConnection conn, string cmdText, string zeroText)
		{
			DataTable dt = GetTable(conn,cmdText);
			if(zeroText!=null)
			{
				DataRow dr=dt.NewRow();
				dr[0]=0;
				dr[1] = zeroText;
				dt.Rows.InsertAt(dr, 0);
			}
			cb.DisplayMember = dt.Columns[1].ColumnName;
			cb.ValueMember = dt.Columns[0].ColumnName;
			cb.DataSource = dt;
			return dt;
		}
		public static DataTable Fill(this DataGridViewComboBoxColumn cb, GmConnection conn, string cmdText, string zeroText)
		{
			DataTable dt = GetTable(conn, cmdText);
			if (zeroText != null)
			{
				DataRow dr = dt.NewRow();
				dr[0] = 0;
				dr[1] = zeroText;
				dt.Rows.InsertAt(dr, 0);
			}
			cb.DisplayMember = dt.Columns[1].ColumnName;
			cb.ValueMember = dt.Columns[0].ColumnName;
			cb.DataSource = dt;
			return dt;
		}
		public static DataTable Fill(this ComboBox cb, GmConnection conn, string cmdText, int selectedValue) { return Fill(cb, conn, cmdText, selectedValue, null); }
		public static DataTable Fill(this ComboBox cb, GmConnection conn, string cmdText, int selectedValue, string zeroText)
		{
			DataTable dt = Fill(cb, conn, cmdText, zeroText);
			cb.SelectedValue = selectedValue;
			return dt;
		}
	}
}
