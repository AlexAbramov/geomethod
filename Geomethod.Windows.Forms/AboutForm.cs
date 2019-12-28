using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblCLRVersion;
		private System.Windows.Forms.Label lblProgName;
		private System.Windows.Forms.LinkLabel lblLink;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblVersion;
		private Label label1;
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{ 
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblLink = new System.Windows.Forms.LinkLabel();
			this.btnOk = new System.Windows.Forms.Button();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.lblProgName = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCLRVersion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLink
			// 
			this.lblLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblLink.AutoSize = true;
			this.lblLink.Location = new System.Drawing.Point(96, 54);
			this.lblLink.Name = "lblLink";
			this.lblLink.Size = new System.Drawing.Size(38, 13);
			this.lblLink.TabIndex = 0;
			this.lblLink.TabStop = true;
			this.lblLink.Text = "_gmurl";
			this.lblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLink_LinkClicked);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(136, 154);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "_ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::Geomethod.Windows.Forms.Properties.Resources.Logo;
			this.pbLogo.Location = new System.Drawing.Point(12, 12);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(67, 67);
			this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbLogo.TabIndex = 2;
			this.pbLogo.TabStop = false;
			// 
			// lblProgName
			// 
			this.lblProgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblProgName.Location = new System.Drawing.Point(96, 8);
			this.lblProgName.Name = "lblProgName";
			this.lblProgName.Size = new System.Drawing.Size(248, 24);
			this.lblProgName.TabIndex = 3;
			// 
			// lblCopyright
			// 
			this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCopyright.AutoSize = true;
			this.lblCopyright.Location = new System.Drawing.Point(96, 32);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(70, 13);
			this.lblCopyright.TabIndex = 4;
			this.lblCopyright.Text = "_gmcopyright";
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(96, 8);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(47, 13);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Text = "_version";
			// 
			// lblCLRVersion
			// 
			this.lblCLRVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCLRVersion.AutoSize = true;
			this.lblCLRVersion.Location = new System.Drawing.Point(96, 76);
			this.lblCLRVersion.Name = "lblCLRVersion";
			this.lblCLRVersion.Size = new System.Drawing.Size(75, 13);
			this.lblCLRVersion.TabIndex = 6;
			this.lblCLRVersion.Text = "_CLR_Version";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "_licensed_to";
			// 
			// AboutForm
			// 
			this.AcceptButton = this.btnOk;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(354, 189);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblCLRVersion);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblProgName);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblLink);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "_about";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			LocaleUtils.Localize(this);
			lblProgName.Text=Application.ProductName;
			lblVersion.Text+=" "+StringUtils.TrimVersion(Application.ProductVersion);
			lblCLRVersion.Text+=" "+StringUtils.TrimVersion(Environment.Version.ToString());
		}

		private void lblLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			lblLink.LinkVisited = true;
			System.Diagnostics.Process.Start(lblLink.Text);
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
