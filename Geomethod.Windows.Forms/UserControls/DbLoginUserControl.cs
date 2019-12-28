using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Geomethod.GeoLib.Windows.Forms.UserControls
{
    public partial class DbLoginUserControl : UserControl
    {
        public string Title { get { return gbSecurity.Text; } set { gbSecurity.Text = value; } }
        public string Login { get { return tbLogin.Text.Trim(); } set { tbLogin.Text = value; } }
        public string Password { get { return tbPassword.Text.Trim(); } set { tbPassword.Text = value; } }
        public bool IntegratedSecurity { get { return chkIntegratedSecurity.Checked; } set { chkIntegratedSecurity.Checked = value; } }
        public bool IntegratedSecurityEnabled { get { return chkIntegratedSecurity.Enabled; } set { chkIntegratedSecurity.Enabled = value; } }
        public bool IsValid{get {return IntegratedSecurityEnabled && IntegratedSecurity ? true :Login.Length > 0 && Password.Length > 0;}}
        public event EventHandler OnChanged;
        public DbLoginUserControl()
        {
            InitializeComponent();
        }

        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            SetChanged();
            UpdateControls();
        }

        private void SetChanged()
        {
            if(OnChanged!=null) OnChanged(this,null);
        }

        private void UpdateControls()
        {
            Control[] controls ={ lblLogin, lblPassword, tbLogin, tbPassword };
            SetEnabled(controls, chkIntegratedSecurity.Enabled && !chkIntegratedSecurity.Checked);
        }

        private void SetEnabled(Control[] controls, bool enabled)
        {
            foreach (Control ctl in controls) ctl.Enabled = enabled;
        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            SetChanged();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            SetChanged();
        }

        private void DbLoginUserControl_Load(object sender, EventArgs e)
        {
        }
    }
}
