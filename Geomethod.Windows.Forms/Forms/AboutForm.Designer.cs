namespace Geomethod.Windows.Forms
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCompanyHyperlink = new System.Windows.Forms.LinkLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblProductHyperlink = new System.Windows.Forms.LinkLabel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.lblProductCopyright = new System.Windows.Forms.Label();
            this.lblProductVersion = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.BackColor = System.Drawing.Color.White;
            this.tbDescription.Location = new System.Drawing.Point(12, 6);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(310, 164);
            this.tbDescription.TabIndex = 29;
            this.tbDescription.TabStop = false;
            this.tbDescription.Text = "Description";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(247, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "_ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblCompanyHyperlink);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lblProductHyperlink);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 270);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 44);
            this.panel1.TabIndex = 34;
            // 
            // lblCompanyHyperlink
            // 
            this.lblCompanyHyperlink.AutoSize = true;
            this.lblCompanyHyperlink.Location = new System.Drawing.Point(9, 3);
            this.lblCompanyHyperlink.Name = "lblCompanyHyperlink";
            this.lblCompanyHyperlink.Size = new System.Drawing.Size(60, 13);
            this.lblCompanyHyperlink.TabIndex = 39;
            this.lblCompanyHyperlink.TabStop = true;
            this.lblCompanyHyperlink.Text = "geomethod";
            this.lblCompanyHyperlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCompanyHyperlink_LinkClicked);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(334, 1);
            this.panel4.TabIndex = 31;
            // 
            // lblProductHyperlink
            // 
            this.lblProductHyperlink.AutoSize = true;
            this.lblProductHyperlink.Location = new System.Drawing.Point(9, 22);
            this.lblProductHyperlink.Name = "lblProductHyperlink";
            this.lblProductHyperlink.Size = new System.Drawing.Size(93, 13);
            this.lblProductHyperlink.TabIndex = 35;
            this.lblProductHyperlink.TabStop = true;
            this.lblProductHyperlink.Text = "_productHyperlink";
            this.lblProductHyperlink.Visible = false;
            this.lblProductHyperlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblProductHyperlink_LinkClicked);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.LightYellow;
            this.pnlTop.Controls.Add(this.imgLogo);
            this.pnlTop.Controls.Add(this.lblProductCopyright);
            this.pnlTop.Controls.Add(this.lblProductVersion);
            this.pnlTop.Controls.Add(this.lblProductName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(334, 87);
            this.pnlTop.TabIndex = 36;
            // 
            // imgLogo
            // 
            this.imgLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgLogo.Image = global::Geomethod.Windows.Forms.Properties.Resources.Logo;
            this.imgLogo.Location = new System.Drawing.Point(255, 9);
            this.imgLogo.Margin = new System.Windows.Forms.Padding(0);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(67, 67);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgLogo.TabIndex = 34;
            this.imgLogo.TabStop = false;
            // 
            // lblProductCopyright
            // 
            this.lblProductCopyright.AutoSize = true;
            this.lblProductCopyright.Location = new System.Drawing.Point(13, 66);
            this.lblProductCopyright.Name = "lblProductCopyright";
            this.lblProductCopyright.Size = new System.Drawing.Size(71, 13);
            this.lblProductCopyright.TabIndex = 38;
            this.lblProductCopyright.Text = "_gmCopyright";
            // 
            // lblProductVersion
            // 
            this.lblProductVersion.AutoSize = true;
            this.lblProductVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProductVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblProductVersion.Location = new System.Drawing.Point(13, 42);
            this.lblProductVersion.Name = "lblProductVersion";
            this.lblProductVersion.Size = new System.Drawing.Size(99, 13);
            this.lblProductVersion.TabIndex = 37;
            this.lblProductVersion.Text = "_productVersion";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblProductName.ForeColor = System.Drawing.Color.Blue;
            this.lblProductName.Location = new System.Drawing.Point(12, 12);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(114, 20);
            this.lblProductName.TabIndex = 36;
            this.lblProductName.Text = "_productName";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightYellow;
            this.panel3.Controls.Add(this.tbDescription);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(334, 183);
            this.panel3.TabIndex = 38;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(334, 314);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "_about";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblProductCopyright;
        private System.Windows.Forms.Label lblProductVersion;
        private System.Windows.Forms.Label lblProductName;
        protected System.Windows.Forms.LinkLabel lblProductHyperlink;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox imgLogo;
        protected System.Windows.Forms.LinkLabel lblCompanyHyperlink;
    }
}
