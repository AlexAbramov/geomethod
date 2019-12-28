using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Geomethod.Data.Windows.Forms
{
	/// <summary>
	/// Summary description for ConnectionsInfoForm.
	/// </summary>
	public partial class ConnectionsInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionsInfoForm));
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmConnnections = new System.Windows.Forms.ContextMenu();
            this.miOpen = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miCreate = new System.Windows.Forms.MenuItem();
            this.miAdd = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miRemove = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miSetDefault = new System.Windows.Forms.MenuItem();
            this.miClearDefault = new System.Windows.Forms.MenuItem();
            this.gridView = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProviderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConnStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOptions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkEditing = new System.Windows.Forms.CheckBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(542, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(110, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "_open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(542, 281);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "_close";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmConnnections
            // 
            this.cmConnnections.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miOpen,
            this.menuItem1,
            this.miCreate,
            this.miAdd,
            this.miEdit,
            this.miRemove,
            this.menuItem2,
            this.miSetDefault,
            this.miClearDefault});
            // 
            // miOpen
            // 
            this.miOpen.Index = 0;
            this.miOpen.Text = "_open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // miCreate
            // 
            this.miCreate.Index = 2;
            this.miCreate.Text = "_create";
            this.miCreate.Click += new System.EventHandler(this.miCreate_Click);
            // 
            // miAdd
            // 
            this.miAdd.Index = 3;
            this.miAdd.Text = "_add";
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miEdit
            // 
            this.miEdit.Index = 4;
            this.miEdit.Text = "_edit";
            this.miEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // miRemove
            // 
            this.miRemove.Index = 5;
            this.miRemove.Text = "_remove";
            this.miRemove.Click += new System.EventHandler(this.miRemove_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            this.menuItem2.Text = "-";
            // 
            // miSetDefault
            // 
            this.miSetDefault.Index = 7;
            this.miSetDefault.Text = "_setDefault";
            this.miSetDefault.Click += new System.EventHandler(this.miSetDefault_Click);
            // 
            // miClearDefault
            // 
            this.miClearDefault.Index = 8;
            this.miClearDefault.Text = "_clearDefault";
            this.miClearDefault.Click += new System.EventHandler(this.miClearDefault_Click);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AllowUserToResizeRows = false;
            this.gridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colProviderName,
            this.colConnStr,
            this.colOptions});
            this.gridView.Location = new System.Drawing.Point(12, 12);
            this.gridView.MultiSelect = false;
            this.gridView.Name = "gridView";
            this.gridView.ReadOnly = true;
            this.gridView.RowHeadersVisible = false;
            this.gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridView.Size = new System.Drawing.Size(524, 292);
            this.gridView.TabIndex = 7;
            this.gridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellContentClick);
            this.gridView.SelectionChanged += new System.EventHandler(this.gridView_SelectionChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "_id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "_name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colProviderName
            // 
            this.colProviderName.DataPropertyName = "ProviderName";
            this.colProviderName.HeaderText = "_providerName";
            this.colProviderName.Name = "colProviderName";
            this.colProviderName.ReadOnly = true;
            // 
            // colConnStr
            // 
            this.colConnStr.DataPropertyName = "ConnStr";
            this.colConnStr.HeaderText = "_connStr";
            this.colConnStr.Name = "colConnStr";
            this.colConnStr.ReadOnly = true;
            // 
            // colOptions
            // 
            this.colOptions.DataPropertyName = "Options";
            this.colOptions.HeaderText = "_options";
            this.colOptions.Name = "colOptions";
            this.colOptions.ReadOnly = true;
            // 
            // chkEditing
            // 
            this.chkEditing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEditing.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEditing.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chkEditing.FlatAppearance.BorderSize = 0;
            this.chkEditing.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.chkEditing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEditing.ForeColor = System.Drawing.Color.Blue;
            this.chkEditing.Image = global::Geomethod.Data.Windows.Forms.Properties.Resources.Down;
            this.chkEditing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkEditing.Location = new System.Drawing.Point(542, 41);
            this.chkEditing.Margin = new System.Windows.Forms.Padding(0);
            this.chkEditing.Name = "chkEditing";
            this.chkEditing.Size = new System.Drawing.Size(113, 20);
            this.chkEditing.TabIndex = 12;
            this.chkEditing.Text = "_editing";
            this.chkEditing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEditing.UseVisualStyleBackColor = false;
            this.chkEditing.CheckedChanged += new System.EventHandler(this.chkEditing_CheckedChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreate.Location = new System.Drawing.Point(542, 70);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(110, 23);
            this.btnCreate.TabIndex = 16;
            this.btnCreate.Text = "_createDb";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(542, 157);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(110, 23);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "_remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(542, 99);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "_add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(542, 128);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 23);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "_edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // chkDefault
            // 
            this.chkDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDefault.Location = new System.Drawing.Point(542, 186);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(110, 24);
            this.chkDefault.TabIndex = 17;
            this.chkDefault.Text = "_default";
            this.chkDefault.UseVisualStyleBackColor = true;
            this.chkDefault.CheckedChanged += new System.EventHandler(this.chkDefault_CheckedChanged);
            // 
            // ConnectionsInfoForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 316);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.chkDefault);
            this.Controls.Add(this.chkEditing);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionsInfoForm";
            this.ShowInTaskbar = false;
            this.Text = "_connections";
            this.Closed += new System.EventHandler(this.ConnectionsInfoForm_Closed);
            this.Load += new System.EventHandler(this.ConnectionsInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ContextMenu cmConnnections;
		private System.Windows.Forms.MenuItem miOpen;
		private System.Windows.Forms.MenuItem miEdit;
		private System.Windows.Forms.MenuItem miAdd;
        private System.Windows.Forms.MenuItem miRemove;
        private DataGridView gridView;
		private IContainer components;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn providerNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn connectionStringDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn optionsDataGridViewTextBoxColumn;
		private MenuItem menuItem1;
		private MenuItem miCreate;
		private MenuItem menuItem2;
		private MenuItem miSetDefault;
		private MenuItem miClearDefault;
		private DataGridViewTextBoxColumn colId;
		private DataGridViewTextBoxColumn colName;
		private DataGridViewTextBoxColumn colProviderName;
		private DataGridViewTextBoxColumn colConnStr;
		private DataGridViewTextBoxColumn colOptions;
		private CheckBox chkEditing;
		private Button btnCreate;
		private Button btnRemove;
		private Button btnAdd;
		private Button btnEdit;
		private CheckBox chkDefault;
	}
}