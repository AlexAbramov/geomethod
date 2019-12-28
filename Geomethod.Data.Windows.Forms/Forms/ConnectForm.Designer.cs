using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Geomethod.Data.Windows.Forms
{
	/// <summary>
	/// Summary description for ConnectionStringForm.
	/// </summary>
	partial class ConnectForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblConnect = new System.Windows.Forms.Label();
			this.rbCreate = new System.Windows.Forms.RadioButton();
			this.rbConnect = new System.Windows.Forms.RadioButton();
			this.lblCreate = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(31, 108);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 9;
			this.btnOk.Text = "_ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(112, 108);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "_cancel";
			// 
			// lblConnect
			// 
			this.lblConnect.Location = new System.Drawing.Point(28, 77);
			this.lblConnect.Name = "lblConnect";
			this.lblConnect.Size = new System.Drawing.Size(316, 13);
			this.lblConnect.TabIndex = 0;
			this.lblConnect.Text = "_connStr:";
			this.lblConnect.Visible = false;
			// 
			// rbCreate
			// 
			this.rbCreate.AutoSize = true;
			this.rbCreate.Checked = true;
			this.rbCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbCreate.Location = new System.Drawing.Point(12, 12);
			this.rbCreate.Name = "rbCreate";
			this.rbCreate.Size = new System.Drawing.Size(109, 17);
			this.rbCreate.TabIndex = 18;
			this.rbCreate.TabStop = true;
			this.rbCreate.Text = "_createNewDb";
			this.rbCreate.UseVisualStyleBackColor = true;
			this.rbCreate.CheckedChanged += new System.EventHandler(this.rbCreate_CheckedChanged);
			// 
			// rbConnect
			// 
			this.rbConnect.AutoSize = true;
			this.rbConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbConnect.Location = new System.Drawing.Point(12, 57);
			this.rbConnect.Name = "rbConnect";
			this.rbConnect.Size = new System.Drawing.Size(153, 17);
			this.rbConnect.TabIndex = 17;
			this.rbConnect.Text = "_connectToExistingDb";
			this.rbConnect.UseVisualStyleBackColor = true;
			this.rbConnect.CheckedChanged += new System.EventHandler(this.rbConnect_CheckedChanged);
			// 
			// lblCreate
			// 
			this.lblCreate.Location = new System.Drawing.Point(31, 32);
			this.lblCreate.Name = "lblCreate";
			this.lblCreate.Size = new System.Drawing.Size(313, 13);
			this.lblCreate.TabIndex = 19;
			this.lblCreate.Text = "(may require database server administrator rights)";
			this.lblCreate.Visible = false;
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnHelp.Location = new System.Drawing.Point(193, 108);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(75, 23);
			this.btnHelp.TabIndex = 20;
			this.btnHelp.Text = "_help";
			this.btnHelp.Visible = false;
			// 
			// toolTip
			// 
			this.toolTip.Active = false;
			this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip_Popup);
			// 
			// ConnectForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(356, 143);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblCreate);
			this.Controls.Add(this.rbCreate);
			this.Controls.Add(this.rbConnect);
			this.Controls.Add(this.lblConnect);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectForm";
			this.ShowInTaskbar = false;
			this.Text = "_db";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.ConnectionStringForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblConnect;
		private System.Windows.Forms.Button btnCancel;
		private RadioButton rbCreate;
		private RadioButton rbConnect;
		private Label lblCreate;
		private IContainer components;
		private Button btnHelp;
		private ToolTip toolTip;
	}
}