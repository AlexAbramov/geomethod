using System;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data.Windows.Forms
{
	/*	public interface IConnectData
		{
			IEnumerable<string> DataProviders{get;}
			string DataDirectory { get; }
			string DefaultDbName{get;}
		}*/

	public class ConnectData
	{
		#region Fields
		string dataDirectory;
		string defaultDbName;
		IEnumerable<string> dataProviders;
		ConnectionsInfo connectionsInfo;
		#endregion

		#region Properties
		public string DataDirectory { get { return dataDirectory; } }
		public string DefaultDbName { get { return defaultDbName; } }
		public IEnumerable<string> DataProviders { get { return dataProviders; } }
		public ConnectionsInfo ConnectionsInfo { get { return connectionsInfo; } }
		#endregion

		public ConnectData(string dataDirectory, string defaultDbName, IEnumerable<string> dataProviders, ConnectionsInfo connectionsInfo)
		{
			this.dataDirectory = dataDirectory;
			this.defaultDbName = defaultDbName;
			this.dataProviders = dataProviders;
			this.connectionsInfo = connectionsInfo;
		}
	}

}
