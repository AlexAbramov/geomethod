using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Geomethod.Web;

namespace Geomethod.Web
{
	public class GridHelper : ITemplate
	{
        public static string sessionObjectTable = "DataTable";
		string idColName = "Id";
		string checkedColName = "Checked";
		string idCtlName = "hfId";
		string chkCtlName = "chkChecked";
		Control ctl;
		GridView gridView;
		DataTable dataTable;
		public DataTable DataTable { get { return dataTable; } }

		#region Construction
		public GridHelper(Control ctl, GridView gridView)
		{
			this.ctl = ctl;
			this.gridView = gridView;
			if (!ctl.Page.IsPostBack)
			{
				dataTable = new DataTable();
			}
			else
			{
				dataTable = (DataTable)ctl.Page.Session[sessionObjectTable];
			}
			gridView.PageIndexChanging += new GridViewPageEventHandler(gridView_PageIndexChanging);
			gridView.Sorting += new GridViewSortEventHandler(gridView_Sorting);
			gridView.Unload += new EventHandler(gridView_Unload);
			gridView.PreRender += new EventHandler(gridView_PreRender);
		}

		public GridHelper(Control ctl, GridView gridView, bool addCheckedColumn)
			: this(ctl, gridView)
		{
			if (addCheckedColumn)
			{
				if (!ctl.Page.IsPostBack)
				{
					dataTable.Columns.Add(checkedColName, typeof(bool)).DefaultValue = false;
				}
				else
				{
					SetCheckedItems();
				}
			}
		}
		#endregion

		void gridView_PreRender(object sender, EventArgs e)
		{
			try
			{
				BindData();
			}
			catch
			{ 
				//!!!
			}
		}

		private void BindData()
		{
			gridView.DataSource = dataTable;
			gridView.DataBind();
		}

		void gridView_Unload(object sender, EventArgs e)
		{
			ctl.Page.Session[sessionObjectTable] = dataTable;
		}

		void gridView_Sorting(object sender, GridViewSortEventArgs e)
		{
            try
            {
                dataTable.DefaultView.Sort = e.SortExpression;
            }
            catch (Exception ex)
            {
                //!!!
            }
		}

		void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gridView.PageIndex = e.NewPageIndex;
		}

		void SetCheckedItems()
		{
//			DataColumn dcId = dataTable.Columns[idColName];
			DataColumn dcChecked = dataTable.Columns[checkedColName];
			foreach (GridViewRow item in gridView.Rows)
			{
				CheckBox checkBox = item.FindControl(chkCtlName) as CheckBox;
				HiddenField hf = item.FindControl(idCtlName) as HiddenField;
				if (checkBox != null && hf != null)
				{
					int id = int.Parse(hf.Value);
					DataRow[] rows = dataTable.Select(idColName+"=" + id);
					if (rows.Length > 0)
					{
						DataRow row = rows[0];
						if ((bool)row[dcChecked] != checkBox.Checked)
						{
							row[dcChecked] = checkBox.Checked;
						}
					}
				}
			}
		}

		#region ITemplate Members

		public void InstantiateIn(Control container)
		{
			CheckBox chk = new CheckBox();
			chk.EnableViewState = true;
			chk.ID = chkCtlName;
			container.Controls.Add(chk);
			HiddenField hf = new HiddenField();
			hf.ID = idCtlName;
			container.Controls.Add(hf);
			container.EnableViewState = true;
		}

		#endregion

		public void ClearDataTable()
		{
			dataTable.DefaultView.Sort = "";
			dataTable.DefaultView.RowFilter = "";
			dataTable.Rows.Clear();
			dataTable.Columns.Clear();
			dataTable.Clear();
		}
	}

/*	class TemplateItem: ITemplate
	{
		#region ITemplate Members

		public void InstantiateIn(Control container)
		{

		}

		#endregion
	}*/
}
