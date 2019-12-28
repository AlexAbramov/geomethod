using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Geomethod.Windows.Forms
{
	public partial class TimeRangeUserControl : UserControl
	{
		public DateTime FromTime { get { return dpFromTime.Value; } }
		public DateTime ToTime { get { return dpToTime.Value; } }
		public bool FromTimeChecked { get { return dpFromTime.Checked; } }
		public bool ToTimeChecked { get { return dpToTime.Checked; } }

		public TimeRangeUserControl()
		{
			InitializeComponent();
			dpFromTime.Value = DateTime.Today;
			dpToTime.Value = DateTime.Today;
		}

		private void TimeSpanUserControl_Load(object sender, EventArgs e)
		{
            if (!base.DesignMode)
            {
            }
		}
	}
}
