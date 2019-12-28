namespace Geomethod.Data.Windows.Forms
{
    partial class DbSizeUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.chkFileGrowth = new System.Windows.Forms.CheckBox();
            this.chkMaxSize = new System.Windows.Forms.CheckBox();
            this.chkSize = new System.Windows.Forms.CheckBox();
            this.nudFileGrowth = new System.Windows.Forms.NumericUpDown();
            this.nudMaxSize = new System.Windows.Forms.NumericUpDown();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.gbSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFileGrowth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            this.SuspendLayout();
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.chkFileGrowth);
            this.gbSize.Controls.Add(this.chkMaxSize);
            this.gbSize.Controls.Add(this.chkSize);
            this.gbSize.Controls.Add(this.nudFileGrowth);
            this.gbSize.Controls.Add(this.nudMaxSize);
            this.gbSize.Controls.Add(this.nudSize);
            this.gbSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSize.Location = new System.Drawing.Point(0, 0);
            this.gbSize.Name = "gbSize";
            this.gbSize.Size = new System.Drawing.Size(250, 100);
            this.gbSize.TabIndex = 32;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "_dbsizemb";
            // 
            // chkFileGrowth
            // 
            this.chkFileGrowth.AutoSize = true;
            this.chkFileGrowth.Location = new System.Drawing.Point(6, 46);
            this.chkFileGrowth.Name = "chkFileGrowth";
            this.chkFileGrowth.Size = new System.Drawing.Size(92, 17);
            this.chkFileGrowth.TabIndex = 37;
            this.chkFileGrowth.Text = "_dbfilegrowth:";
            this.chkFileGrowth.UseVisualStyleBackColor = true;
            this.chkFileGrowth.CheckedChanged += new System.EventHandler(this.chkFileGrowth_CheckedChanged);
            // 
            // chkMaxSize
            // 
            this.chkMaxSize.AutoSize = true;
            this.chkMaxSize.Location = new System.Drawing.Point(6, 72);
            this.chkMaxSize.Name = "chkMaxSize";
            this.chkMaxSize.Size = new System.Drawing.Size(84, 17);
            this.chkMaxSize.TabIndex = 36;
            this.chkMaxSize.Text = "_dbmaxsize:";
            this.chkMaxSize.UseVisualStyleBackColor = true;
            this.chkMaxSize.CheckedChanged += new System.EventHandler(this.chkMaxSize_CheckedChanged);
            // 
            // chkSize
            // 
            this.chkSize.AutoSize = true;
            this.chkSize.Location = new System.Drawing.Point(6, 20);
            this.chkSize.Name = "chkSize";
            this.chkSize.Size = new System.Drawing.Size(65, 17);
            this.chkSize.TabIndex = 35;
            this.chkSize.Text = "_dbsize:";
            this.chkSize.UseVisualStyleBackColor = true;
            this.chkSize.CheckedChanged += new System.EventHandler(this.chkSize_CheckedChanged);
            // 
            // nudFileGrowth
            // 
            this.nudFileGrowth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFileGrowth.Location = new System.Drawing.Point(174, 45);
            this.nudFileGrowth.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudFileGrowth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFileGrowth.Name = "nudFileGrowth";
            this.nudFileGrowth.Size = new System.Drawing.Size(70, 20);
            this.nudFileGrowth.TabIndex = 34;
            this.nudFileGrowth.ThousandsSeparator = true;
            this.nudFileGrowth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFileGrowth.ValueChanged += new System.EventHandler(this.nudFileGrowth_ValueChanged);
            // 
            // nudMaxSize
            // 
            this.nudMaxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxSize.Location = new System.Drawing.Point(174, 71);
            this.nudMaxSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMaxSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxSize.Name = "nudMaxSize";
            this.nudMaxSize.Size = new System.Drawing.Size(70, 20);
            this.nudMaxSize.TabIndex = 33;
            this.nudMaxSize.ThousandsSeparator = true;
            this.nudMaxSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxSize.ValueChanged += new System.EventHandler(this.nudMaxSize_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSize.Location = new System.Drawing.Point(174, 19);
            this.nudSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(70, 20);
            this.nudSize.TabIndex = 32;
            this.nudSize.ThousandsSeparator = true;
            this.nudSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // DbSizeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSize);
            this.Name = "DbSizeUserControl";
            this.Size = new System.Drawing.Size(250, 100);
            this.gbSize.ResumeLayout(false);
            this.gbSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFileGrowth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.CheckBox chkFileGrowth;
        private System.Windows.Forms.CheckBox chkMaxSize;
        private System.Windows.Forms.CheckBox chkSize;
        private System.Windows.Forms.NumericUpDown nudFileGrowth;
        private System.Windows.Forms.NumericUpDown nudMaxSize;
        private System.Windows.Forms.NumericUpDown nudSize;
    }
}
