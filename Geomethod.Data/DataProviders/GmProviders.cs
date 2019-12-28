using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
    /// <summary>
    /// Singleton class enumerating data providers
    /// </summary>
    public class GmProviders // 
    {
        static Dictionary<string, GmProviderFactory> providers = new Dictionary<string, GmProviderFactory>();
        static GmProviders()
        {
            Add(new AccessProvider(PathUtils.BaseDirectory + @"Template/Empty.mdb"));
			Add(new SqlServerProvider());
//			Add(new SqlServerCEProvider());
//			Add(new OracleProvider());

        }
        public static void Add(GmProviderFactory pr) { providers.Add(pr.Name, pr); }
        public static GmProviderFactory Get(string providerName) 
        { 
            if(!HasProvider(providerName)) throw new GmDataException("Provider not found: " + providerName);
            return providers[providerName]; 
        }
        public static bool HasProvider(string providerName) { return providers.ContainsKey(providerName); }
        public static int UpdateList()
        {
            DataTable dataTable=DbProviderFactories.GetFactoryClasses();
            return 0;
        }

        #region IEnumerable Members
        public static ICollection<GmProviderFactory> Items { get { return providers.Values; } }
        #endregion

    }
}
