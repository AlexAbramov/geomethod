using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Geomethod.Windows.Forms
{
    public partial class AboutForm : Form
    {
        AssemblyInfo assemblyInfo = new AssemblyInfo();
        public AboutForm()
        {
            InitializeComponent();
			GmApplication.Initialize(this);

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
/*            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;*/
        }

/*        bool SetLocalizedValue(string key)
        {
            if (Locale.StringSet.ContainsKey(key))
            {
            }
        }*/

        private void AboutBox_Load(object sender, EventArgs e)
        {			
            if (!DesignMode)
            {
                 btnOk.Focus();
				lblProductVersion.Text += ": " + (assemblyInfo.AssemblyVersion);//StringUtils.TrimVersion
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", Locale.Get("_clrVersion"), assemblyInfo.CLRVersion);
                string s = Locale.StringSet.GetExisting("_descriptionInfo");
                if (s != null)
                {
                    s = Environment.NewLine + s;
                    sb.AppendLine();
                    sb.Append(s);
                }
//                sb.AppendFormat("{0}: {1}", Locale.Get("_licensedTo"),Locale.Get("_licensee"));
                tbDescription.Text = sb.ToString();
                if (!Locale.StringSet.ContainsKey("_productName")) lblProductName.Text = assemblyInfo.AssemblyProduct;
//                lblCLRVersion.Text += " " + StringUtils.TrimVersion(Environment.Version.ToString());
            }
        }

        private void lblProductHyperlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void lblCompanyHyperlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblCompanyHyperlink.LinkVisited = true;
            System.Diagnostics.Process.Start(lblCompanyHyperlink.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
