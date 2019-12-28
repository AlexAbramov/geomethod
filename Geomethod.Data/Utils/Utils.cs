using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Geomethod.Data
{
	public class GmDataException : GmException
	{
		public GmDataException(string message) : base(message) { }
		public GmDataException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class DataUtils
	{
		public static string GetColumnList(IEnumerable<string> colNames, GetColumnListOptions opt)
		{
			string format=null;
			switch (opt)
			{
				case GetColumnListOptions.Insert: format = "@{0}"; break;
				case GetColumnListOptions.Update: format = "{0}=@{0}"; break;
			}

			StringBuilder sb = new StringBuilder(1 << 10);
			foreach (string s in colNames)
			{
				if (sb.Length > 0) sb.Append(',');
				if (format == null) sb.Append(s);
				else sb.AppendFormat(format, s);			
			}

			return sb.ToString();
		}
	}
}
