using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Geomethod.Windows.Forms;

namespace Geomethod.Data.Windows.Forms
{
	public interface IConfigRecordApp
	{
		ConfigRecord ConfigRecord { get; }
		ConnectionFactory ConnectionFactory { get; }
		bool SetConfig(ConfigRecord configRecord);
		DialogResult OpenConfigForm(ConfigRecord configRecord, bool readOnly);
	}

	public partial class ConfigRecordsForm : Form
	{
		int maxId = 0;
		DataTable dataTable;
		DbDataAdapter dataAdapter;
		IConfigRecordApp app;

		DataRow SelectedRow { get { return GridViewUtils.GetSelectedRow(gridView); } }
		int SelectedId { get { DataRow dr = SelectedRow; return dr!=null? (int)dr[0]: 0; } }

		public ConfigRecordsForm(IConfigRecordApp app)
		{
			InitializeComponent();
			this.app = app;
		}

		private void ConfigRecordsForm_Load(object sender, EventArgs e)
		{
			if (DesignMode) return;
			LoadData();
			UpdateControls();
		}

		private void UpdateControls()
		{
			int selId=SelectedId;
			btnRestore.Enabled = selId > 0 && selId < maxId;
		}

		void LoadData()
		{
			dataTable = new DataTable();
			using (GmConnection conn = app.ConnectionFactory.CreateConnection())
			{
				dataAdapter = conn.CreateDataAdapter("select ConfigRecords.Id, ConfigRecords.RestoredId, Users.Name, ConfigRecords.Version, ConfigRecords.Time from ConfigRecords left join Users on Users.Id=UserId");
				dataAdapter.Fill(dataTable);
			}
			foreach (DataRow dr in dataTable.Rows)
			{
				int id = (int)dr[0];
				if (maxId < id) maxId = id;
			}
			gridView.DataSource = dataTable;
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			Open();
		}

		private void Open()
		{
			try
			{
				int id = SelectedId;
				if (id != 0)
				{
					ConfigRecord configRecord = null;
					using (GmConnection conn = app.ConnectionFactory.CreateConnection())
					{
						configRecord = ConfigRecord.GetItem(conn, id);
					}
					if (configRecord != null)
					{
						app.OpenConfigForm(configRecord, true);
					}
				}
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
		}

/*		private void UpdateRow(DataRow dr, ConfigRecordForm form)
		{
			ConfigRecord configRecord = form.ConfigRecord;
			dr["Id"] = configRecord.Id;
			dr["Number"] = configRecord.number;
			dr["NumberOfBeds"] = configRecord.numberOfBeds;
			dr["ConfigRecordTypeName"] = form.ConfigRecordTypeName;
		}*/

		private void btnRestore_Click(object sender, EventArgs e)
		{
			try
			{
				if (maxId == app.ConfigRecord.Id)
				{
					int selId = SelectedId;
					//				btnRestore.Enabled = selId > 0 && selId < maxId;
					if (MessageBoxUtils.AskLocalized("_areYouSureToRestoreConfig"))
					{
						bool restored = false;
						using (WaitCursor wc = new WaitCursor())
						{
							using (GmConnection conn = app.ConnectionFactory.CreateConnection())
							{
								ConfigRecord configRecord = ConfigRecord.GetItem(conn, selId);
								configRecord.SetId(maxId+1);
								configRecord.comment=Locale.Get("_configRestored");
								restored = app.SetConfig(configRecord);
							}
						}
						if (restored)
						{
							LoadData();
							UpdateControls();
							MessageBoxUtils.ShowLocalized("_configRestored");
						}
						else MessageBoxUtils.ShowLocalized("_configNotRestored");
					}
				}
				else MessageBoxUtils.ShowLocalized("_newConfigFound");
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

		private void btnExportToXML_Click(object sender, EventArgs e)
		{
			try
			{
				int id = SelectedId;
				if (id != 0)
				{
					dlgSaveFile.FileName = string.Format("HosDepConfig{0}.xml", id);
					if (dlgSaveFile.ShowDialog() == DialogResult.OK)
					{
						using (WaitCursor wc = new WaitCursor())
						{
							ConfigRecord configRecord = null;
							using (GmConnection conn = app.ConnectionFactory.CreateConnection())
							{
								configRecord = ConfigRecord.GetItem(conn, id);
							}
							if (configRecord != null)
							{
								configRecord.config.Serialize(dlgSaveFile.FileName);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}
		}

	}
}