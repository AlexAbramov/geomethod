using System;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class CommandCache
	{
		public static bool enabled = false;
		static Dictionary<string, GmCommand> commands = new Dictionary<string, GmCommand>(1<<10);
		public static void Save(string cmdId, GmCommand cmd) 
		{
			commands[cmdId]=cmd;
		}
		public static GmCommand Get(string cmdId)
		{
			return commands.ContainsKey(cmdId) ? commands[cmdId] : null;
		}
	}
}
