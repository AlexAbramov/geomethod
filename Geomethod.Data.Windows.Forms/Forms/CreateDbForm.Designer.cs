using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Geomethod.Data.Windows.Forms
{
	public partial class CreateDbForm
	{
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateDbForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tpMain = new System.Windows.Forms.TabPage();
			this.btnUpdateServerInstanceList = new System.Windows.Forms.Button();
			this.cbServerInstance = new System.Windows.Forms.ComboBox();
			this.lblServerInstance = new System.Windows.Forms.Label();
			this.btnFilePath = new System.Windows.Forms.Button();
			this.tbFilePath = new System.Windows.Forms.TextBox();
			this.lblFilePath = new System.Windows.Forms.Label();
			this.cbDataProvider = new System.Windows.Forms.ComboBox();
			this.lblDataProvider = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.tpDb = new System.Windows.Forms.TabPage();
			this.ucAdminLogin = new Geomethod.Data.Windows.Forms.DbLoginUserControl();
			this.ucUserLogin = new Geomethod.Data.Windows.Forms.DbLoginUserControl();
			this.ucDbSize = new Geomethod.Data.Windows.Forms.DbSizeUserControl();
			this.tcMain = new System.Windows.Forms.TabControl();
			this.dlgFilePath = new System.Windows.Forms.OpenFileDialog();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.tpMain.SuspendLayout();
			this.tpDb.SuspendLayout();
			this.tcMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(304, 264);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "_ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(385, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "_cancel";
			// 
			// tpMain
			// 
			this.tpMain.Controls.Add(this.btnUpdateServerInstanceList);
			this.tpMain.Controls.Add(this.cbServerInstance);
			this.tpMain.Controls.Add(this.lblServerInstance);
			this.tpMain.Controls.Add(this.btnFilePath);
			this.tpMain.Controls.Add(this.tbFilePath);
			this.tpMain.Controls.Add(this.lblFilePath);
			this.tpMain.Controls.Add(this.cbDataProvider);
			this.tpMain.Controls.Add(this.lblDataProvider);
			this.tpMain.Controls.Add(this.tbName);
			this.tpMain.Controls.Add(this.lblName);
			this.tpMain.Location = new System.Drawing.Point(4, 22);
			this.tpMain.Name = "tpMain";
			this.tpMain.Size = new System.Drawing.Size(464, 232);
			this.tpMain.TabIndex = 0;
			this.tpMain.Text = "_mainParams";
			this.tpMain.UseVisualStyleBackColor = true;
			// 
			// btnUpdateServerInstanceList
			// 
			this.btnUpdateServerInstanceList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdateServerInstanceList.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateServerInstanceList.Image")));
			this.btnUpdateServerInstanceList.Location = new System.Drawing.Point(424, 54);
			this.btnUpdateServerInstanceList.Name = "btnUpdateServerInstanceList";
			this.btnUpdateServerInstanceList.Size = new System.Drawing.Size(32, 23);
			this.btnUpdateServerInstanceList.TabIndex = 31;
			this.btnUpdateServerInstanceList.Click += new System.EventHandler(this.btnUpdateServerInstanceList_Click);
			// 
			// cbServerInstance
			// 
			this.cbServerInstance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbServerInstance.Enabled = false;
			this.cbServerInstance.ItemHeight = 13;
			this.cbServerInstance.Location = new System.Drawing.Point(133, 56);
			this.cbServerInstance.Name = "cbServerInstance";
			this.cbServerInstance.Size = new System.Drawing.Size(285, 21);
			this.cbServerInstance.TabIndex = 30;
			this.cbServerInstance.TextChanged += new System.EventHandler(this.cbServerInstance_TextChanged);
			// 
			// lblServerInstance
			// 
			this.lblServerInstance.AutoSize = true;
			this.lblServerInstance.Location = new System.Drawing.Point(8, 59);
			this.lblServerInstance.Name = "lblServerInstance";
			this.lblServerInstance.Size = new System.Drawing.Size(59, 13);
			this.lblServerInstance.TabIndex = 29;
			this.lblServerInstance.Text = "_dbServer:";
			// 
			// btnFilePath
			// 
			this.btnFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFilePath.Location = new System.Drawing.Point(424, 81);
			this.btnFilePath.Name = "btnFilePath";
			this.btnFilePath.Size = new System.Drawing.Size(32, 23);
			this.btnFilePath.TabIndex = 28;
			this.btnFilePath.Text = "...";
			this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
			// 
			// tbFilePath
			// 
			this.tbFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbFilePath.Location = new System.Drawing.Point(133, 83);
			this.tbFilePath.Name = "tbFilePath";
			this.tbFilePath.ReadOnly = true;
			this.tbFilePath.Size = new System.Drawing.Size(285, 20);
			this.tbFilePath.TabIndex = 27;
			this.tbFilePath.TextChanged += new System.EventHandler(this.tbFilePath_TextChanged);
			// 
			// lblFilePath
			// 
			this.lblFilePath.AutoSize = true;
			this.lblFilePath.Location = new System.Drawing.Point(8, 83);
			this.lblFilePath.Name = "lblFilePath";
			this.lblFilePath.Size = new System.Drawing.Size(50, 13);
			this.lblFilePath.TabIndex = 26;
			this.lblFilePath.Text = "_filepath:";
			// 
			// cbDataProvider
			// 
			this.cbDataProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.cbDataProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataProvider.ItemHeight = 13;
			this.cbDataProvider.Location = new System.Drawing.Point(133, 29);
			this.cbDataProvider.Name = "cbDataProvider";
			this.cbDataProvider.Size = new System.Drawing.Size(285, 21);
			this.cbDataProvider.TabIndex = 24;
			this.cbDataProvider.SelectedIndexChanged += new System.EventHandler(this.cbDataProvider_SelectedIndexChanged);
			// 
			// lblDataProvider
			// 
			this.lblDataProvider.AutoSize = true;
			this.lblDataProvider.Location = new System.Drawing.Point(8, 32);
			this.lblDataProvider.Name = "lblDataProvider";
			this.lblDataProvider.Size = new System.Drawing.Size(72, 13);
			this.lblDataProvider.TabIndex = 23;
			this.lblDataProvider.Text = "_datastorage:";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbName.Location = new System.Drawing.Point(133, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(285, 20);
			this.tbName.TabIndex = 7;
			this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(8, 6);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(42, 13);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "_name:";
			// 
			// tpDb
			// 
			this.tpDb.Controls.Add(this.ucAdminLogin);
			this.tpDb.Controls.Add(this.ucUserLogin);
			this.tpDb.Controls.Add(this.ucDbSize);
			this.tpDb.Location = new System.Drawing.Point(4, 22);
			this.tpDb.Name = "tpDb";
			this.tpDb.Size = new System.Drawing.Size(464, 232);
			this.tpDb.TabIndex = 1;
			this.tpDb.Text = "_db";
			this.tpDb.UseVisualStyleBackColor = true;
			// 
			// ucAdminLogin
			// 
			this.errorProvider.SetIconAlignment(this.ucAdminLogin, System.Windows.Forms.ErrorIconAlignment.TopRight);
			this.errorProvider.SetIconPadding(this.ucAdminLogin, -15);
			this.ucAdminLogin.IntegratedSecurity = false;
			this.ucAdminLogin.IntegratedSecurityEnabled = true;
			this.ucAdminLogin.Location = new System.Drawing.Point(8, 3);
			this.ucAdminLogin.Login = "";
			this.ucAdminLogin.Name = "ucAdminLogin";
			this.ucAdminLogin.Password = "";
			this.ucAdminLogin.Size = new System.Drawing.Size(200, 100);
			this.ucAdminLogin.TabIndex = 2;
			this.ucAdminLogin.Title = "_admin";
			this.ucAdminLogin.OnChanged += new System.EventHandler(this.ucAdminLogin_OnChanged);
			// 
			// ucUserLogin
			// 
			this.errorProvider.SetIconAlignment(this.ucUserLogin, System.Windows.Forms.ErrorIconAlignment.TopRight);
			this.errorProvider.SetIconPadding(this.ucUserLogin, -15);
			this.ucUserLogin.IntegratedSecurity = false;
			this.ucUserLogin.IntegratedSecurityEnabled = true;
			this.ucUserLogin.Location = new System.Drawing.Point(214, 3);
			this.ucUserLogin.Login = "";
			this.ucUserLogin.Name = "ucUserLogin";
			this.ucUserLogin.Password = "";
			this.ucUserLogin.Size = new System.Drawing.Size(200, 100);
			this.ucUserLogin.TabIndex = 1;
			this.ucUserLogin.Title = "_user";
			this.ucUserLogin.OnChanged += new System.EventHandler(this.ucUserLogin_OnChanged);
			// 
			// ucDbSize
			// 
			this.ucDbSize.FileGrowth = 1;
			this.ucDbSize.FileGrowthEnabled = true;
			this.ucDbSize.FileSize = 1;
			this.ucDbSize.FileSizeEnabled = true;
			this.ucDbSize.Location = new System.Drawing.Point(8, 109);
			this.ucDbSize.MaxSize = 1000;
			this.ucDbSize.MaxSizeEnabled = true;
			this.ucDbSize.Name = "ucDbSize";
			this.ucDbSize.Size = new System.Drawing.Size(250, 100);
			this.ucDbSize.TabIndex = 0;
			// 
			// tcMain
			// 
			this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tcMain.Controls.Add(this.tpMain);
			this.tcMain.Controls.Add(this.tpDb);
			this.tcMain.ItemSize = new System.Drawing.Size(42, 18);
			this.tcMain.Location = new System.Drawing.Point(0, 0);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(472, 258);
			this.tcMain.TabIndex = 13;
			// 
			// dlgFilePath
			// 
			this.dlgFilePath.CheckFileExists = false;
			// 
			// errorProvider
			// 
			this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider.ContainerControl = this;
			// 
			// CreateDbForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 299);
			this.Controls.Add(this.tcMain);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CreateDbForm";
			this.Text = "_createDb";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.CreateDatabaseForm_Load);
			this.tpMain.ResumeLayout(false);
			this.tpMain.PerformLayout();
			this.tpDb.ResumeLayout(false);
			this.tcMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tpMain;
		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.ComboBox cbDataProvider;
		private System.Windows.Forms.Label lblDataProvider;
		private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnFilePath;
		private System.Windows.Forms.TextBox tbFilePath;
		private System.Windows.Forms.Label lblFilePath;
		private System.Windows.Forms.OpenFileDialog dlgFilePath;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.TabPage tpDb;
		private System.Windows.Forms.Label lblServerInstance;
        private System.Windows.Forms.ComboBox cbServerInstance;
        private IContainer components;
        private Button btnUpdateServerInstanceList;
        private Geomethod.Data.Windows.Forms.DbLoginUserControl ucUserLogin;
        private Geomethod.Data.Windows.Forms.DbSizeUserControl ucDbSize;
        private Geomethod.Data.Windows.Forms.DbLoginUserControl ucAdminLogin;
        private FolderBrowserDialog dlgFolderBrowser;
	}
}