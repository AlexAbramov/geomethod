namespace Geomethod.Windows.Forms
{
    partial class MessageForm
    {
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnIgnore;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnReport = new System.Windows.Forms.Button();
			this.btnDetails = new System.Windows.Forms.Button();
			this.btnIgnore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbMessage
			// 
			this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbMessage.CausesValidation = false;
			this.tbMessage.Location = new System.Drawing.Point(8, 8);
			this.tbMessage.Multiline = true;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(344, 168);
			this.tbMessage.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(8, 184);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "_ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnReport
			// 
			this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReport.Location = new System.Drawing.Point(184, 184);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(80, 23);
			this.btnReport.TabIndex = 2;
			this.btnReport.Text = "_report";
			this.btnReport.Visible = false;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// btnDetails
			// 
			this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDetails.Location = new System.Drawing.Point(96, 184);
			this.btnDetails.Name = "btnDetails";
			this.btnDetails.Size = new System.Drawing.Size(80, 23);
			this.btnDetails.TabIndex = 3;
			this.btnDetails.Text = "_details";
			this.btnDetails.Visible = false;
			this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
			// 
			// btnIgnore
			// 
			this.btnIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnIgnore.Location = new System.Drawing.Point(272, 184);
			this.btnIgnore.Name = "btnIgnore";
			this.btnIgnore.Size = new System.Drawing.Size(80, 23);
			this.btnIgnore.TabIndex = 4;
			this.btnIgnore.Text = "_ignore";
			this.btnIgnore.Visible = false;
			this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
			// 
			// MessageForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(360, 214);
			this.Controls.Add(this.btnIgnore);
			this.Controls.Add(this.btnDetails);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tbMessage);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(368, 248);
			this.Name = "MessageForm";
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.MessageForm_Load);
			this.Resize += new System.EventHandler(this.MessageForm_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion
    }
}