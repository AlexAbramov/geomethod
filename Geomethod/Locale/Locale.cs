using System;
using System.Collections.Generic;

namespace Geomethod
{
	public class Locale
	{
		static StringSet stringSet = new StringSet();
		public static StringSet StringSet { get { return stringSet; } set { stringSet = value; } }
		public static string Get(string key) { return stringSet[key]; }
	}
}
