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
	public partial class ConnectForm : System.Windows.Forms.Form
	{
		#region Fields
		#endregion

		#region Properties
		public bool CreateMode { get { return rbCreate.Checked; } set { rbCreate.Checked = value; rbConnect.Checked = !rbCreate.Checked; } }
		#endregion

		#region Construction
		public ConnectForm()
		{
			InitializeComponent();
			GmApplication.Initialize(this);
		}
		#endregion

		private void ConnectionStringForm_Load(object sender, System.EventArgs e)
		{
			if (!DesignMode)
			{
				//			toolTip.SetToolTip(lblConnect, Locale.Get());
				//			MinimumSize=Size;
				//			MaximumSize = new Size(1024, MinimumSize.Height);
				UpdateControls();
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
		}

		void UpdateControls()
		{
/*			bool connectMode=rbConnect.Checked;
			lblConnect.Enabled = connectMode;
			lblCreate.Enabled = !connectMode;*/
		}

		private void rbConnect_CheckedChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void rbCreate_CheckedChanged(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void toolTip_Popup(object sender, PopupEventArgs e)
		{
		}
	}
}
