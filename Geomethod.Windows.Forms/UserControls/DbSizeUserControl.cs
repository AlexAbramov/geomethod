using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Geomethod.Windows.Forms.UserControls
{
    public partial class DbSizeUserControl : UserControl
    {
        public int FileSize { get { return (int)nudSize.Value; } set { nudSize.Value = value; } }
        public int FileGrowth { get { return (int)nudFileGrowth.Value; } set { nudFileGrowth.Value = value; } }
        public int MaxSize { get { return (int)nudMaxSize.Value; } set { nudMaxSize.Value = value; } }
        public bool FileSizeEnabled { get { return chkSize.Enabled; } set { chkSize.Enabled = value; } }
        public bool MaxSizeEnabled { get { return chkMaxSize.Enabled; } set { chkMaxSize.Enabled = value; } }
        public bool FileGrowthEnabled { get { return chkFileGrowth.Enabled; } set { chkFileGrowth.Enabled = value; } }

        public DbSizeUserControl()
        {
            InitializeComponent();
        }

        private void chkSize_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void chkFileGrowth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void chkMaxSize_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void nudFileGrowth_ValueChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void nudMaxSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        public void UpdateControls()
        {
            nudSize.Enabled = chkSize.Checked;
            nudMaxSize.Enabled = chkMaxSize.Checked;
            nudFileGrowth.Enabled = chkFileGrowth.Checked;
            nudMaxSize.Minimum = nudSize.Value;
            nudFileGrowth.Maximum = nudMaxSize.Value;
        }

        public void Clear()
        {
            chkSize.Checked = false;
            chkFileGrowth.Checked = false;
            chkMaxSize.Checked = false;
        }
    }
}
