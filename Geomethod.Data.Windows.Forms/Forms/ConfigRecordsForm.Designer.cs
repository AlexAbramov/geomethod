namespace Geomethod.Data.Windows.Forms
{
	partial class ConfigRecordsForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.gridView = new System.Windows.Forms.DataGridView();
			this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRestoredId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnRestore = new System.Windows.Forms.Button();
			this.btnExportToXML = new System.Windows.Forms.Button();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(428, 354);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "_close";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(428, 12);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(100, 23);
			this.btnOpen.TabIndex = 2;
			this.btnOpen.Text = "_open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// gridView
			// 
			this.gridView.AllowUserToAddRows = false;
			this.gridView.AllowUserToDeleteRows = false;
			this.gridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colRestoredId,
            this.colName,
            this.colVersion,
            this.colTime});
			this.gridView.Location = new System.Drawing.Point(12, 12);
			this.gridView.MultiSelect = false;
			this.gridView.Name = "gridView";
			this.gridView.ReadOnly = true;
			this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridView.Size = new System.Drawing.Size(410, 365);
			this.gridView.TabIndex = 3;
			this.gridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellDoubleClick);
			this.gridView.SelectionChanged += new System.EventHandler(this.gridView_SelectionChanged);
			// 
			// colId
			// 
			this.colId.DataPropertyName = "Id";
			this.colId.HeaderText = "_id";
			this.colId.Name = "colId";
			this.colId.ReadOnly = true;
			// 
			// colRestoredId
			// 
			this.colRestoredId.DataPropertyName = "RestoredId";
			this.colRestoredId.HeaderText = "_restored";
			this.colRestoredId.Name = "colRestoredId";
			this.colRestoredId.ReadOnly = true;
			// 
			// colName
			// 
			this.colName.DataPropertyName = "Name";
			this.colName.HeaderText = "_user";
			this.colName.Name = "colName";
			this.colName.ReadOnly = true;
			// 
			// colVersion
			// 
			this.colVersion.DataPropertyName = "Version";
			this.colVersion.HeaderText = "_version";
			this.colVersion.Name = "colVersion";
			this.colVersion.ReadOnly = true;
			// 
			// colTime
			// 
			this.colTime.DataPropertyName = "Time";
			this.colTime.HeaderText = "_date";
			this.colTime.Name = "colTime";
			this.colTime.ReadOnly = true;
			// 
			// btnRestore
			// 
			this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRestore.Location = new System.Drawing.Point(428, 41);
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = new System.Drawing.Size(100, 23);
			this.btnRestore.TabIndex = 4;
			this.btnRestore.Text = "_restore";
			this.btnRestore.UseVisualStyleBackColor = true;
			this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
			// 
			// btnExportToXML
			// 
			this.btnExportToXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExportToXML.Location = new System.Drawing.Point(428, 70);
			this.btnExportToXML.Name = "btnExportToXML";
			this.btnExportToXML.Size = new System.Drawing.Size(100, 23);
			this.btnExportToXML.TabIndex = 5;
			this.btnExportToXML.Text = "_exportToXML";
			this.btnExportToXML.UseVisualStyleBackColor = true;
			this.btnExportToXML.Click += new System.EventHandler(this.btnExportToXML_Click);
			// 
			// dlgSaveFile
			// 
			this.dlgSaveFile.DefaultExt = "xml";
			this.dlgSaveFile.Filter = "XML files (*.xml)|*.xml";
			// 
			// ConfigRecordsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 389);
			this.Controls.Add(this.btnExportToXML);
			this.Controls.Add(this.btnRestore);
			this.Controls.Add(this.gridView);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.btnOk);
			this.Name = "ConfigRecordsForm";
			this.Text = "_configurations";
			this.Load += new System.EventHandler(this.ConfigRecordsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.DataGridView gridView;
		private System.Windows.Forms.Button btnRestore;
		private System.Windows.Forms.Button btnExportToXML;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn colId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colRestoredId;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
	}
}