using System;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class SqlLog : ILogHandler
	{
		public enum Fields { Id, Time, Ip, UserId, LogType, Header, Message}

		#region Fields
        LogSystem logSystem;
		ConnectionFactory connectionFactory=null;
		#endregion

		#region Construction
        public SqlLog(ConnectionFactory connectionFactory) { this.connectionFactory = connectionFactory; }
		#endregion

        #region ILogHandler Members

        public void Init(LogSystem logSystem)
        {
            this.logSystem=logSystem;
        }

        public void Put(LogRecord lr)
        {
            if (connectionFactory != null)
            {
                using (GmConnection conn = connectionFactory.CreateConnection())
                {
                    GmCommand cmd = conn.CreateCommand();
					int id = conn.ConnectionFactory.IdGenerator.GetId(GmTables.Log);
                    string cmdText="insert into Log values (@Id,@Time";
					cmd.AddInt(Fields.Id,id);
                    cmd.AddDateTime("Time", DateTime.Now);
                    if(logSystem.HasFlag(LogSystemFlags.UseIp))
                    { 
                        cmdText+=",@Ip";
                        cmd.AddInt("Ip", logSystem.Ip);
                    }
                    if(logSystem.HasFlag(LogSystemFlags.UseUserId))
                    {
                        cmdText+=",@UserId";
                        cmd.AddInt("UserId", logSystem.UserId);
                    }
                    cmdText+=",@LogType,@Header,@Message)";
                    cmd.AddInt("LogType", (int)lr.logType);
                    cmd.AddString("Header", lr.header);
                    cmd.AddString("Message", lr.message);
                    cmd.CommandText=cmdText;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Close()
        {
            connectionFactory = null;
        }

        #endregion
    }

}
