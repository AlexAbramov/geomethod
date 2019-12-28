using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
    public class ConnectionFactory
    {
        #region Fields
        GmProviderFactory providerFactory;
        string connectionString="";
		int syncId = 0;
		IIdGenerator idGenerator = null;
		#endregion

        #region Properties
        public GmProviderFactory ProviderFactory { get { return providerFactory; }}
        public string ConnectionString { get { return connectionString; } set { connectionString = value; } }
		public IIdGenerator IdGenerator { get { return idGenerator; } set { idGenerator = value; } }
		public int SyncId { get { return syncId; } }
		public bool HasSync { get { return syncId > 0; } }
		#endregion

        #region Construction
        public ConnectionFactory(GmProviderFactory providerFactory)
        {
            this.providerFactory = providerFactory;
        }
        public ConnectionFactory(GmProviderFactory providerFactory, string connectionString)
        {
            this.providerFactory = providerFactory;
            this.connectionString = connectionString;
        }
        public ConnectionFactory(string providerName, string connectionString)
        {
            providerFactory = GmProviders.Get(providerName);
            this.connectionString = connectionString;
        }
        #endregion

        #region Methods
        public GmConnection CreateConnection() { return new GmConnection(this);}
        #endregion

    }
}
