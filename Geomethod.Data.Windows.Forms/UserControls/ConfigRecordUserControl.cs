using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Geomethod.Data;

namespace Geomethod.Data.Windows.Forms
{
	public partial class ConfigRecordUserControl : UserControl
	{
		bool readOnly=false;
		public string Comment { get { return tbComment.Text; } set { tbComment.Text = value; } }
		ConfigRecord configRecord;
		public void Init(ConfigRecord configRecord, bool readOnly) { this.configRecord = configRecord; this.readOnly = readOnly; }
		public ConfigRecordUserControl()
		{
			InitializeComponent();
		}

		private void ConfigRecordUserControl_Load(object sender, EventArgs e)
		{
            if (!DesignMode)
            {
				LoadData();
                UpdateControls();
            }
		}

		private void LoadData()
		{
			StringBuilder sb = new StringBuilder(200);
			sb.AppendLine(Locale.Get("_configRecordId: ") + configRecord.Id);
			sb.AppendLine(Locale.Get("_restoredId: ") + configRecord.restoredId);
//!!!			sb.AppendLine("Пользователь: " + App.Instance.AppCache.GetUserName(configRecord.userId));
			sb.AppendLine(Locale.Get("_configVersion: ") + configRecord.version);
			sb.AppendLine(Locale.Get("_configDate: ") + configRecord.time.ToString());
			if (readOnly)
			{
				tbComment.ReadOnly = true;
				tbComment.Text = configRecord.comment;
			}
			else sb.AppendLine(Locale.Get("_comment: ") + configRecord.comment);
			tbInfo.Text = sb.ToString();
		}

		private void UpdateControls()
		{
		}

	}
}
