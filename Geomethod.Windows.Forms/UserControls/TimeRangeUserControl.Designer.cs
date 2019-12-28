namespace Geomethod.Windows.Forms
{
	partial class TimeRangeUserControl
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
			this.dpFromTime = new System.Windows.Forms.DateTimePicker();
			this.dpToTime = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// dpFromTime
			// 
			this.dpFromTime.Location = new System.Drawing.Point(103, 3);
			this.dpFromTime.Name = "dpFromTime";
			this.dpFromTime.ShowCheckBox = true;
			this.dpFromTime.Size = new System.Drawing.Size(145, 20);
			this.dpFromTime.TabIndex = 0;
			this.dpFromTime.Value = new System.DateTime(2006, 12, 8, 0, 0, 0, 0);
			// 
			// dpToTime
			// 
			this.dpToTime.Checked = false;
			this.dpToTime.Location = new System.Drawing.Point(103, 29);
			this.dpToTime.Name = "dpToTime";
			this.dpToTime.ShowCheckBox = true;
			this.dpToTime.Size = new System.Drawing.Size(145, 20);
			this.dpToTime.TabIndex = 1;
			this.dpToTime.Value = new System.DateTime(2006, 12, 8, 0, 0, 0, 0);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "_startDate: ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "_endDate:";
			// 
			// TimeRangeUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dpToTime);
			this.Controls.Add(this.dpFromTime);
			this.Name = "TimeRangeUserControl";
			this.Size = new System.Drawing.Size(251, 52);
			this.Load += new System.EventHandler(this.TimeSpanUserControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dpFromTime;
		private System.Windows.Forms.DateTimePicker dpToTime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}
