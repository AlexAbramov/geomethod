using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Windows.Forms
{
	public static class ComboBoxExtensions
	{
		public static DataRow GetSelectedRow(this ComboBox cb)
		{
			DataRowView drv = cb.SelectedItem as DataRowView;
			if (drv != null) return drv.Row;
			return null;
		}

		public static string GetSelectedText(this ComboBox cb)
		{
			DataRow dr = GetSelectedRow(cb);
			return dr == null ? "" : dr[1].ToString();
		}

        public static int GetSelectedValue(this ComboBox cb)
		{
			DataRow dr = GetSelectedRow(cb);
			return dr == null ? 0 : (int)dr[0];
		}

		public static int GetInt(this ComboBox cb)
		{
            object selValue = cb.SelectedValue;
			return selValue == null ? 0 : (int)selValue;
		}
	}
}
