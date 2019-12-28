using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
    public class LoginData
    {
        public bool integratedSecurity=false;
        public string login="";
        public string password="";
        public LoginData()
        {
        }
    }

	public class DbCreationProperties
	{
        public string providerName = "";
        public LoginData userLogin = new LoginData();
        public LoginData adminLogin = new LoginData();
        public string dbName = "";
        public string serverName = "";
        public int fileSize=0;
        public int fileMaxSize=0;
        public int fileGrowth = 0;
        public string filePath = "";
        public string adminConnStr = "";
        public DbCreationProperties()
        {
        }
/*		public ConnectionFactory CreateDb()
		{
			GmProviderFactory pr = GmProviders.Get(providerName);
			string connStr = pr.CreateDatabase(this);
			//            UpdateDb(pr);
			return pr.CreateConnectionFactory(connStr);
		}*/
		public ConnectionInfo CreateDb()
		{
			GmProviderFactory pr = GmProviders.Get(providerName);
			string connectionString = pr.CreateDatabase(this);
			//            UpdateDb(pr);
			return new ConnectionInfo(dbName, pr.Name, connectionString, "");
		}
	}
}