using System;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class SqlQueries
	{
		static StringSet stringSet = new StringSet();
		public static StringSet StringSet { get { return stringSet; } set { stringSet = value; } }
		public static string Get(string key) { return stringSet[key]; }
	}
}
