using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod.Windows.Forms;
using Geomethod.Data;
using Geomethod;
using System.Data.Common;
using System.Data;

namespace Geomethod.Data.Windows.Forms
{
    public partial class CreateDbForm : System.Windows.Forms.Form
    {
        #region Fields
		ConnectData connectData;
        bool fileProviderEnabled = false; 
        string fileExt = "";
        string fileFilter = "";
        DbCreationProperties props = new DbCreationProperties();
        Random random = new Random();
        #endregion

        #region Properties
        public bool FileProviderEnabled { get { return fileProviderEnabled; } }
        public string FileProviderName { get { return ConnectionInfo.fileProviderName; } }
        public DbCreationProperties DbCreationProperties { get { return props; } }
        public string FilePath { get { return props.filePath; } }
        public string ProviderName { get { return cbDataProvider.Text; } }
        public bool IsFileProvider { get { return ProviderName == FileProviderName; } }
        public bool IsDbProvider { get { return !IsFileProvider; } }
        public GmProviderFactory ProviderFactory { get { return IsDbProvider ? GmProviders.Get(ProviderName) : null; } }
        #endregion

        #region Construction
		public CreateDbForm(ConnectData connectData)
        {
            InitializeComponent();
			GmApplication.Initialize(this);
			this.connectData = connectData;
        }
        public void InitFileProvider(string fileExt, string fileFilter) 
        {
            this.fileExt = fileExt;
            this.fileFilter = fileFilter;
            fileProviderEnabled = true;
        }
        #endregion

        private void CreateDatabaseForm_Load(object sender, System.EventArgs e)
        {
			dlgFilePath.InitialDirectory = connectData.DataDirectory;
            InitDataProviderList();
            if (FileProviderEnabled) SetDefaultFileName();
			else tbName.Text = connectData.DefaultDbName;
//            DataProviderChanged();
            MinimumSize = Size;
        }

        private void SetDefaultFileName()
        {
			if (connectData.DataDirectory.Length > 0 && connectData.DefaultDbName.Length > 0)
            {
                for (int i = 1; i < 1000; i++)
                {
					string fileName = connectData.DefaultDbName + i.ToString();
					string filePath = connectData.DataDirectory + fileName + fileExt;
                    if (!File.Exists(filePath))
                    {
                        tbName.Text = fileName;
                        break;
                    }
                }
            }
        }

        void InitDataProviderList()
        {
            if(FileProviderEnabled) this.cbDataProvider.Items.Add(FileProviderName);
			foreach (string prName in connectData.DataProviders)
            {
                this.cbDataProvider.Items.Add(prName);
            }
            cbDataProvider.SelectedIndex = 0;
        }

        void DataProviderChanged()
        {
            string providerName = ProviderName;
            bool isDbProvider = IsDbProvider;
            foreach (Control ctl in tpDb.Controls) ctl.Enabled = isDbProvider;
            ucDbSize.Clear();
            ucAdminLogin.Clear();
            ucUserLogin.Clear();
            cbServerInstance.Items.Clear();
            cbServerInstance.Text = "";
            if (isDbProvider)
            {
                GmProviderFactory pr = ProviderFactory;
                ucDbSize.FileSizeEnabled = pr.SupportsProperty(GmProviderProperty.FileSize);
                ucDbSize.MaxSizeEnabled = pr.SupportsProperty(GmProviderProperty.FileMaxSize);
                ucDbSize.FileGrowthEnabled = pr.SupportsProperty(GmProviderProperty.FileGrowth);
                ucUserLogin.IntegratedSecurityEnabled = pr.SupportsProperty(GmProviderProperty.IntegratedSecurity);
                ucAdminLogin.IntegratedSecurityEnabled = pr.SupportsProperty(GmProviderProperty.IntegratedSecurity);
                ucAdminLogin.Enabled = pr.SupportsProperty(GmProviderProperty.Server);
                ucDbSize.Enabled = pr.SupportsProperty(GmProviderProperty.FileSize);
                cbServerInstance.Enabled = pr.SupportsProperty(GmProviderProperty.Server);
                btnUpdateServerInstanceList.Enabled = cbServerInstance.Enabled && pr.DbProviderFactory.CanCreateDataSourceEnumerator;
            }
            else
            {
                cbServerInstance.Enabled = false;
                btnUpdateServerInstanceList.Enabled = false;
            }
            lblServerInstance.Enabled = cbServerInstance.Enabled;            
            ucDbSize.UpdateControls();
            SetUserLogin();
            ValidateServerInstance();
            ValidateSecuritySettings();
            UpdateControls();
        }

        void EnumerateServerInstances()
        {
            try
            {
                using (WaitCursor wc = new WaitCursor())
                {
                    string providerName = ProviderName;
                    bool isDbProvider = IsDbProvider;
                    cbServerInstance.Items.Clear();
                    cbServerInstance.Text = "";
                    if (isDbProvider)
                    {
                        GmProviderFactory pr = ProviderFactory;
                        if (pr.DbProviderFactory.CanCreateDataSourceEnumerator)
                        {
                            DbDataSourceEnumerator en = pr.DbProviderFactory.CreateDataSourceEnumerator();
                            DataTable dt = en.GetDataSources();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string serverName = dr[0] as string;
                                string instanceName = dr[1] as string;
                                cbServerInstance.Items.Add(serverName + "\\" + instanceName);
                            }
                            if (cbServerInstance.Items.Count > 0 && cbServerInstance.Text.Trim().Length == 0)
                            {
                                cbServerInstance.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        private void UpdateControls()
        {
            bool autoFileCreation = false;
            if (IsDbProvider)
            {
                GmProviderFactory pr = ProviderFactory;
                autoFileCreation = pr.SupportsProperty(GmProviderProperty.AutoFileCreation);
            }
            bool hasName = tbName.Text.Trim().Length > 0;
            bool hasFilePath = tbFilePath.Text.Trim().Length > 0;
            this.btnFilePath.Enabled = hasName;
            tbFilePath.Enabled = btnFilePath.Enabled = !autoFileCreation;
            lblFilePath.Enabled = tbFilePath.Enabled;
            btnOk.Enabled = hasName && (hasFilePath || autoFileCreation) && !HasError(this);
        }

        void UpdateFilePath()
        {
            tbFilePath.Text = GetFilePath();
        }

        string GetFilePath()
        {
            string name = tbName.Text.Trim();
            if (name.Length > 0)
            {
                if(IsDbProvider)
                {
                    GmProviderFactory pr = ProviderFactory;
                    if (!pr.SupportsProperty(GmProviderProperty.AutoFileCreation))
                    {
						return connectData.DataDirectory + name + pr.FileExtension;
                    }
                }
				else return connectData.DataDirectory + name + fileExt;
            }
            return "";
        }

        string GetFileFilter()
        {
            if(IsDbProvider) 
            {
                GmProviderFactory pr = ProviderFactory;
                string ext = pr.FileExtension;
                if (!String.IsNullOrEmpty(ext))
                {
                    return string.Format("{0} files (*.{1})|*.{1}", pr.Name, ext);
                }
                else return "";
            }
            else return fileFilter;
        }

        private void tbName_TextChanged(object sender, System.EventArgs e)
        {
            SetUserLogin();
            UpdateFilePath();
            UpdateControls();
        }

        private void SetUserLogin()
        {
            if (IsDbProvider)
            {
                string dbName = tbName.Text.Trim();
                ucUserLogin.Login = dbName;
                int r=random.Next(1 << 12, 1 << 20);
                ucUserLogin.Password = IntToStr(r) + string.Format("{0:000}",(Math.Abs(dbName.GetHashCode())+r) % 1000);
            }
            else ucUserLogin.Clear();
        }

        private string IntToStr(int i)
        {
            string res="";
            for (int j = 0; j < 3; j++)
            {
                res += (char)((i % 26) + (int)'A');
                i /= 26;
            }
            return res;
        }

        /*		void CheckRequireServerName()
                {
                    this.cbServerInstance.Text="";
                    this.cbServerInstance.Items.Clear();
                    GmProviderFactory provider=GetProvider();
                    if (provider != null)
                    {
                        bool canCreateDataSourceEnumerator = provider.DbProviderFactory.CanCreateDataSourceEnumerator;
                        this.cbServerInstance.Enabled = provider.SupportsProperty(GmProviderProperty.Server);

                        if (canCreateDataSourceEnumerator)
                        {
                            string providerName=cbDataProvider.Text;
                            string si=serverInstances[providerName] as string;
                            if(si!=null && si.Length>0)
                            {
                                this.cbServerInstance.Items.Add(si);
                                this.cbServerInstance.Text=si;
                            }
        //                    ValidateServerInstance( );
                        }
                    }
                }*/

        private void cbDataProvider_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateFilePath();
            DataProviderChanged();
        }

        private void btnFilePath_Click(object sender, System.EventArgs e)
        {
            dlgFilePath.FileName = tbFilePath.Text.Trim().Length > 0 ? tbFilePath.Text.Trim() : GetFilePath();
            dlgFilePath.Filter = GetFileFilter();
            if (dlgFilePath.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = dlgFilePath.FileName.Trim();
                string fileName = Path.GetFileNameWithoutExtension(tbFilePath.Text);
                if (tbName.Text.Trim() != fileName)
                {
                    tbName.Text = fileName;
                }
                UpdateControls();
            }
        }

        private void tbFilePath_TextChanged(object sender, System.EventArgs e)
        {
            this.ValidateFilePath();
        }

        /*		void CheckValue(TextBox tb,System.ComponentModel.CancelEventArgs e)
                {
                    string msg="";
                    try
                    {
                        int i=GetInt(tb.Text);
                        if(i<0) msg=Locale.Get("_putpositivenumber");
                    }
                    catch
                    {
                        msg=Locale.Get("_wrongintegerformat");
                    }
                    if(msg.Length>0) e.Cancel=true;
                    errorProvider.SetError(tb,msg);
                }

                int GetInt(string s)
                {
                    s=s.Trim();
                    if(s.Length==0) return 0;
                    return int.Parse(s);
                }*/

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                props.providerName = ProviderName;
                props.dbName = this.tbName.Text.Trim();
                props.filePath = this.tbFilePath.Text.Trim();
                props.fileSize = ucDbSize.FileSize;
                props.fileMaxSize = ucDbSize.MaxSize;
                props.fileGrowth = ucDbSize.FileGrowth;
                props.serverName = this.cbServerInstance.Text.Trim();
                Init(props.userLogin, ucUserLogin, false);
                Init(props.adminLogin, ucAdminLogin, false);
            }
            catch (Exception ex)
            {
                base.DialogResult = DialogResult.Cancel;
                Log.Exception(ex);
            }
        }

        private void Init(LoginData loginData, DbLoginUserControl ucLogin, bool initControl)
        {
            if (initControl)
            {
                ucLogin.IntegratedSecurity = loginData.integratedSecurity;
                ucLogin.Login = loginData.login;
                ucLogin.Password = loginData.password;
            }
            else
            {
                loginData.integratedSecurity = ucLogin.IntegratedSecurity;
                loginData.login = ucLogin.Login;
                loginData.password = ucLogin.Password;
            }
        }

        bool HasError(Control ctl)
        {
            foreach (Control ctl2 in ctl.Controls)
            {
                if (HasError(ctl2)) return true;
            }
            return this.errorProvider.GetError(ctl).Length > 0;
        }

        void ValidateFilePath()
        {
            string msg = "";
            if (File.Exists(tbFilePath.Text))
            {
                msg = Locale.Get("_fileExists");
            }
            errorProvider.SetError(tbFilePath, msg);
        }

        void ValidateServerInstance()
        {
            string msg = "";
            if (cbServerInstance.Enabled && cbServerInstance.Text.Trim().Length == 0)
            {
                msg = Locale.Get("_requiredField");
            }
            errorProvider.SetError(this.cbServerInstance, msg);
        }

        private void cbServerInstance_TextChanged(object sender, System.EventArgs e)
        {
            ValidateServerInstance();
            UpdateControls();
        }

        private void SetEnabled(Control[] controls, bool enabled)
        {
            foreach (Control ctl in controls) ctl.Enabled = enabled;
        }

        private void btnUpdateServerInstanceList_Click(object sender, EventArgs e)
        {
            EnumerateServerInstances();
        }

        private void ucUserLogin_OnChanged(object sender, EventArgs e)
        {
            ValidateSecuritySettings();
        }

        private void ValidateSecuritySettings()
        {
            errorProvider.SetError(ucUserLogin, ucUserLogin.Enabled && !ucUserLogin.IsValid ? Locale.Get("_setSecuritySettings") : null);
            errorProvider.SetError(ucAdminLogin, ucAdminLogin.Enabled && !ucAdminLogin.IsValid ? Locale.Get("_setSecuritySettings") : null);
            UpdateControls();
        }

        private void ucAdminLogin_OnChanged(object sender, EventArgs e)
        {
            ValidateSecuritySettings();
        }

		public ConnectionInfo CreateDb()
		{
			return props.CreateDb();
		}
	}
}
