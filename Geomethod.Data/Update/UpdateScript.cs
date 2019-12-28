using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Geomethod.Data
{
	public class UpdateScript : IComparable<UpdateScript>
	{
		public int id;
		public List<string> commands=new List<string>();

		public UpdateScript(int id)
		{
			this.id = id;
		}

		#region IComparable<Script> Members

		public int CompareTo(UpdateScript other)
		{
			return id - other.id;
		}

		#endregion


		public string GetText()
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in commands)
			{
				sb.AppendLine(s);
//				sb.AppendLine(UpdateScripts.goToken);
			}
			return sb.ToString();
		}
	}
}