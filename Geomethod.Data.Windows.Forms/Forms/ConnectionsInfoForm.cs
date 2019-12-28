using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Geomethod.Windows.Forms;


namespace Geomethod.Data.Windows.Forms
{
	/// <summary>
	/// Summary description for ConnectionsInfoForm.
	/// </summary>
	public partial class ConnectionsInfoForm : System.Windows.Forms.Form
	{
		#region Fields
		ConnectionsInfo connectionsInfo;
		DataTable dataTable = new DataTable("ConnectionsInfo");
		DataColumn dcId = new DataColumn("Id", typeof(int));
		DataColumn dcName = new DataColumn("Name");
		DataColumn dcProviderName = new DataColumn("ProviderName");
		DataColumn dcConnStr = new DataColumn("ConnStr");
		DataColumn dcOptions = new DataColumn("Options");
		bool connectionsUpdated=false;
		ConnectData connectData;
		bool isLoaded = false;
		#endregion

		#region Properties
		public bool ConnectionsUpdated { get { return connectionsUpdated; } }
		DataRow SelectedRow { get { return GridViewUtils.GetSelectedRow(gridView); } }
		int SelectedId { get { DataRow dr = SelectedRow; return dr != null ? (int)dr[0] : 0; } }
		#endregion

		public ConnectionsInfoForm(ConnectionsInfo connectionsInfo, ConnectData connectData)
		{
			InitializeComponent();
			GmApplication.Initialize(this);
			this.connectionsInfo=connectionsInfo;
			this.connectData = connectData;
		}

		public ConnectionInfo GetSelectedConnection()
		{
			return connectionsInfo.GetItem(SelectedId);
		}

		private void ConnectionsInfoForm_Load(object sender, System.EventArgs e)
		{
			if (!DesignMode)
			{
				DataColumn[] columns = { dcId, dcName, dcProviderName, dcConnStr, dcOptions };
				dataTable.Columns.AddRange(columns);
				LoadData();
				gridView.DataSource=dataTable;
				SetCurrentRow();
				MinimumSize = Size;
				OnEditing();
				UpdateControls();
				isLoaded = true;
			}
		}

		private void SetCurrentRow()
		{
			if (connectionsInfo.defaultId != 0)
			{
				GridViewUtils.SetCurrentRow(gridView, connectionsInfo.defaultId);
			}
			else gridView.ClearSelection();
		}

        private void LoadData()
        {
            foreach (ConnectionInfo connectionInfo in connectionsInfo.items)
            {
                AddRow(connectionInfo);
            }
        }

        DataRow AddRow(ConnectionInfo connectionInfo)
        {
			DataRow dr=dataTable.NewRow();
			FillRow(dr, connectionInfo);
			dataTable.Rows.Add(dr);
            return dr;
        }

        private void FillRow(DataRow dr, ConnectionInfo connectionInfo)
        {
			dr[dcId] = connectionInfo.id;
            dr[dcName] = connectionInfo.name;
            dr[dcProviderName] = connectionInfo.providerName;
            dr[dcConnStr] = connectionInfo.connectionString;
            dr[dcOptions] = connectionInfo.options;
        }

		private void addButton_Click(object sender, System.EventArgs e)
		{
			Add();
		}

		void Add()
		{
			try
			{
				ConnectionInfoForm form = new ConnectionInfoForm(connectData);
				if (form.ShowDialog(this) == DialogResult.OK)
				{
					ConnectionInfo connectionInfo = form.ConnectionInfo;
					connectionsInfo.Add(connectionInfo);
					AddRow(connectionInfo);
					connectionsUpdated = true;
				}
				gridView.Focus();
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
        }

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			Edit();
		}

		void Edit()
		{
			try
			{
				DataRow dr = SelectedRow;
				if (dr != null)
				{
					int id = (int)dr[dcId];
					ConnectionInfo connectionInfo = connectionsInfo.GetItem(id);
					if (connectionInfo != null)
					{
						ConnectionInfoForm connForm = new ConnectionInfoForm(connectData, connectionInfo);
						if (connForm.ShowDialog(this) == DialogResult.OK)
						{
							FillRow(dr, connectionInfo);
							connectionsUpdated = true;
						}
						gridView.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			Open();
		}

		void Open()
		{
            if (SelectedId != 0)
            {
                base.DialogResult = DialogResult.OK;
                Close();
            }
        }

		private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            UpdateControls();		
		}

		void UpdateControls()
		{
			int selId=SelectedId;
			bool selected=selId!=0;
			btnOpen.Enabled=selected;			
			btnRemove.Enabled=selected;
			btnEdit.Enabled=selected;
			bool isSelectedDefault=connectionsInfo.defaultId == selId;
			if (chkDefault.Checked != isSelectedDefault)
			{
				chkDefault.Checked = isSelectedDefault;
			}
/*			chkDefault.Checked=
			miRemove.Enabled=selected;
			miEdit.Enabled=selected;
			miOpen.Enabled=selected;
			bool editMode = EditMode;
			btnAdd.Visible = editMode;
			btnRemove.Visible = editMode;
			btnEdit.Visible = editMode;*/
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			Remove();
		}

		void Remove()
		{
			try
			{
				DataRow row = SelectedRow;
				if (row != null)
				{
					int id = (int)row[dcId];
					ConnectionInfo conn = connectionsInfo.GetItem(id);
					if (conn != null)
					{
						connectionsInfo.Remove(conn);
						dataTable.Rows.Remove(row);
						connectionsUpdated = true;
						UpdateControls();
					}
					gridView.Focus();
				}
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
		}

		private void listView_DoubleClick(object sender, System.EventArgs e)
		{
			Open();		
		}

		private void cmConnections_Popup(object sender, System.EventArgs e)
		{
//			bool en=SelectedItem!=null;
//			miEdit.Enabled=en;
		}

		private void miEdit_Click(object sender, System.EventArgs e)
		{
			Edit();
		}

		private void ConnectionsInfoForm_Closed(object sender, System.EventArgs e)
		{
		}

		private void miAdd_Click(object sender, System.EventArgs e)
		{
			Add();
		}

		private void miRemove_Click(object sender, System.EventArgs e)
		{
			Remove();
		}

		private void miOpen_Click(object sender, System.EventArgs e)
		{
			Open();
		}

        private void bindingSource_CurrentChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Open();
        }

		private void chkEditMode_CheckedChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void btnCreate_Click(object sender, EventArgs e)
		{
			CreateDb();
		}

		private void CreateDb()
		{
			try
			{
				CreateDbForm form = new CreateDbForm(connectData);
				if (form.ShowDialog() == DialogResult.OK)
				{
					ConnectionInfo connectionInfo = form.CreateDb();
					connectionsInfo.Add(connectionInfo);
					AddRow(connectionInfo);
					connectionsUpdated = true;
				}
				gridView.Focus();
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
		}

		private void gridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void chkDefault_CheckedChanged(object sender, EventArgs e)
		{
			if (isLoaded)
			{
				int defaultId = chkDefault.Checked ? SelectedId : 0;
				if (connectionsInfo.defaultId != defaultId)
				{
					connectionsInfo.defaultId = defaultId;
					connectionsUpdated = true;
				}
			}
		}

		private void chkEditing_CheckedChanged(object sender, EventArgs e)
		{
			OnEditing();
		}

		private void OnEditing()
		{
			bool vis = chkEditing.Checked;
			btnAdd.Visible = vis;
			btnEdit.Visible = vis;
			btnRemove.Visible = vis;
			btnCreate.Visible = vis;
			chkDefault.Visible = vis;
			this.chkEditing.Image = vis ? global::Geomethod.Data.Windows.Forms.Properties.Resources.Up : global::Geomethod.Data.Windows.Forms.Properties.Resources.Down;
		}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
        }



        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void miCreate_Click(object sender, EventArgs e)
        {
            CreateDb();
        }

        private void miSetDefault_Click(object sender, EventArgs e)
        {

        }

        private void miClearDefault_Click(object sender, EventArgs e)
        {

        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


	}
}
