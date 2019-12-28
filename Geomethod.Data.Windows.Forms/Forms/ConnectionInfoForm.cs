using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod.Windows.Forms;
using Geomethod;
using Geomethod.Data;

namespace Geomethod.Data.Windows.Forms
{
	/// <summary>
	/// Summary description for ConnectionStringForm.
	/// </summary>
	public partial class ConnectionInfoForm : System.Windows.Forms.Form
	{
		#region Fields
		ConnectData connectData;
		ConnectionInfo connectionInfo;
		IEnumerable<string> dataProviders;
		string checkTable = "";
		bool formLoaded=false;
		#endregion

		#region Properties
		public ConnectionInfo ConnectionInfo { get { return connectionInfo; } }
		public string ConnectionString { get { return tbConnStr.Text; } set { tbConnStr.Text = value; } }
		public string ProviderName { get { return cbDataProvider.Text; } set { cbDataProvider.Text = value; } }
		public GmProviderFactory ProviderFactory { get { return GmProviders.Get(ProviderName); } }
		public string CheckTable { get { return checkTable; } set { checkTable = value; } }
		#endregion

		public ConnectionInfoForm(ConnectData connectData) : this(connectData, new ConnectionInfo()) { }
		public ConnectionInfoForm(ConnectData connectData, ConnectionInfo connectionInfo)
		{
			InitializeComponent();
			GmApplication.Initialize(this);
			this.connectData = connectData;
			this.connectionInfo=connectionInfo;
			dataProviders=connectData.DataProviders;
		}

		private void ConnectionStringForm_Load(object sender, System.EventArgs e)
		{
			tbName.Text = connectionInfo.name;
			tbConnStr.Text = connectionInfo.connectionString;
			InitDataProviderList();
			cbDataProvider.Text = connectionInfo.providerName;
			tbOptions.Text = connectionInfo.options;
			formLoaded = true;
			UpdateControls();
			MinimumSize = Size;
		}

		void InitDataProviderList()
		{

			foreach (string provName in dataProviders)
			{
				this.cbDataProvider.Items.Add(provName);
			}
			cbDataProvider.SelectedIndex = 0;
		}

		private void tbName_TextChanged(object sender, System.EventArgs e)
		{
			UpdateControls();
		}

		private void tbConnStr_TextChanged(object sender, System.EventArgs e)
		{
			UpdateControls();		
		}

		void UpdateControls()
		{
            if (formLoaded)
            {
                string name = tbName.Text.Trim();
				string connStr = tbConnStr.Text.Trim();
                bool enabled = name.Length > 0 && connStr.Length > 0;
/*                 if (enabled)
                {
                   if (!editing || connectionString.name != name)
                    {
                        enabled = !connectionStrings.HasName(name);
                    }
                }*/
                btnOk.Enabled = enabled;
            }
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			connectionInfo.name = tbName.Text.Trim();
			connectionInfo.providerName = this.cbDataProvider.Text;
			connectionInfo.connectionString = tbConnStr.Text.Trim();
			connectionInfo.options = tbOptions.Text.Trim();
/*				try
				{
					// Connection test
					using (WaitCursor wc = new WaitCursor())
					{
						GmProviderFactory fact = ProviderFactory;
						using (GmConnection conn = fact.CreateConnection(ConnectionString))
						{
							conn.DbConnection.Open();
							if (checkTable != null && checkTable.Trim().Length > 0)
							{
								try
								{
									conn.ExecuteScalar(string.Format("select count(*) from [{0}]", checkTable));
								}
								catch (Exception ex)
								{
									throw new GmDataException(Locale.Get("_unexpectedDbSchema"), ex);
								}
							}
						}
					}
					DialogResult = DialogResult.OK;
					Close();
				}
				catch (Exception ex)
				{
					string msg = Locale.Get("_connFailed");
					MessageBox.Show(msg + "\r\n\r\n" + ex.Message);
				}
*/
		}

	}
}