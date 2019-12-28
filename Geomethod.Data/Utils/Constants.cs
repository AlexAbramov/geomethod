using System;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public enum GmTables { Log = -1, DbInfo = -2, ConfigRecords=-3}
	public enum GmFields { Id, Status, SyncId}
	public enum RecordStatus:byte {Removed=0, Normal=1, Hidden=2, New=244, All=255}
	public enum GetColumnListOptions { Select, Insert, Update }

	public class MaxLength
	{
		public class LogTypes
		{
			public const int Name = 50;
		}
		public class Log
		{
			public const int Header = 200;
			public const int Message = 4000;
		}
		public class Users
		{
			public const int Name = 50;
			public const int Login = 50;
			public const int Password = 50;
		}
		public class ConfigRecords
		{
			public const int Comment = 4000;
		}
	}

}
