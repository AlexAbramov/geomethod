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
	public class UpdateScripts
	{
		#region Constants
		const string endLine = "\r\n";
		const string commentToken = "--";
		const string versionToken = "scriptid";
		const string commandSepToken = "--";
        static readonly string[] newSqlCmdTokens ={ "create", "drop", "alter", "insert", "update" };
        #endregion

		#region Fields
		private List<UpdateScript> scripts = new List<UpdateScript>();
		#endregion

		#region Properties
		public IEnumerable<UpdateScript> Scripts { get { return scripts; } }
		public int Version{get{return scripts.Count>0?scripts[scripts.Count-1].id:-1;}}
		#endregion

		#region Static
        public static bool UpdateDb(ConnectionFactory connectionFactory, Assembly assembly, string resourceName){using(StreamReader sr=GetStreamReader(assembly,resourceName)){return UpdateDb(connectionFactory,sr);}}
        public static bool UpdateDb(ConnectionFactory connectionFactory, string filePath){using(StreamReader sr=GetStreamReader(filePath)){return UpdateDb(connectionFactory,sr);}}
        public static bool UpdateDb(ConnectionFactory connectionFactory, StreamReader sr)
        {
            long pos = sr.BaseStream.Position;
            int scriptVer = ReadVersion(sr);
            if (scriptVer > -1)
            {
                int dbVer = -1;
                using (GmConnection conn = connectionFactory.CreateConnection())
                {
                    dbVer = GetDbVersion(conn);
                }
                UpdateScripts us = new UpdateScripts();
                if (dbVer < scriptVer)
                {
                   sr.BaseStream.Position = pos;
//                    sr.BaseStream.Seek(pos, SeekOrigin.Begin);
                    sr = new StreamReader(sr.BaseStream);
                    us.Load(dbVer + 1, sr);
					using (GmConnection conn = connectionFactory.CreateConnection())
					{
						us.UpdateDb(conn);
					}
                }
				else if (dbVer > scriptVer)
				{
					return false;
				}
            }
			return true;
        }
		public static int ReadVersion(StreamReader sr)
		{
			string line;
			while ((line = sr.ReadLine()) != null)
			{
				string s = line.Trim();
				if (s.StartsWith(commentToken))
				{
					s = s.Remove(0, commentToken.Length).Trim().ToLower();
					if (s.StartsWith(versionToken))
					{
						s = s.Remove(0, versionToken.Length).TrimStart();
						string[] ss = s.Split(';');
						int id = int.Parse(ss[0].Replace("=", ""));
						return id;
					}
				}
			}
			return -1;
		}
		public static int GetDbVersion(GmConnection conn)
		{
			object obj=null;
			Exception ex = null;
			string cmdText = "select max(Id) from UpdateScripts";
			try
			{
				obj = conn.ExecuteScalar(cmdText);
			}
            //catch (SystemException e)//SqlCeException
            //{
            //    ex = e;
            //}
			catch (DbException e)
			{
				ex = e;
			}
			if (ex != null)
			{
				CreateUpdateScriptsTable(conn);
				obj = conn.ExecuteScalar(cmdText);
			}
            if(obj==null || obj==DBNull.Value )
                return -1;
            else
            {
                return System.Convert.ToInt32(obj);
            }
 
		}
		public static void CreateUpdateScriptsTable(GmConnection conn)
		{
            string cmdText = "create table UpdateScripts( Id int not null PRIMARY KEY, [Date] datetime not null, [Text] varchar(max) not null)";         
			conn.ExecuteNonQuery(cmdText);
		}
        static StreamReader GetStreamReader(string fileName) { return new StreamReader(fileName, Encoding.UTF8); }
        static StreamReader GetStreamReader(Assembly assembly, string resourceName) 
        {
			if(assembly==null) assembly=Assembly.GetEntryAssembly();
            Stream stream=assembly.GetManifestResourceStream(resourceName);
            return new StreamReader(stream);
        }
        #endregion

		#region Construction
		public UpdateScripts()
		{
		}
		public void Load(int startId, string fileName){using (StreamReader sReader = GetStreamReader(fileName)) { Load(startId, sReader); }}
		public void Load(int startId, Assembly assembly, string resourceName){using(StreamReader sr=GetStreamReader(assembly, resourceName)){ Load(startId, sr);}}
		public void Load(int startId, StreamReader sReader)
		{
			UpdateScript script = null;
			StringBuilder sb = new StringBuilder();
			string line;
			while((line=sReader.ReadLine())!=null)
			{
				string s = line.Trim();
				if (s.StartsWith(commentToken))
				{
					s = s.Remove(0, commentToken.Length).Trim().ToLower();
					if (s.StartsWith(versionToken))
					{
						s = s.Remove(0, versionToken.Length).TrimStart();
						string[] ss = s.Split(';');
						int id = int.Parse(ss[0].Replace("=", ""));
						if (script != null)
						{
							if (sb.Length > 0)
							{
								script.commands.Add(sb.ToString());
								sb.Length = 0;
							}
							scripts.Add(script);
							script = null;
						}
						if (id < startId) break;
						script = new UpdateScript(id);
						sb.Length = 0;
					}
				}
				else if (s.Length==0)
				{
					if (script != null && sb.Length > 0)
					{
						script.commands.Add(sb.ToString());
						sb.Length = 0;
					}
				}
                else if (script != null)
                {
                    if (IsNewSqlCommand(s))
                    {
                        if (script != null && sb.Length > 0)
                        {
                            script.commands.Add(sb.ToString());
                            sb.Length = 0;
                        }
                    }
                    sb.AppendLine(line);
                }
			}
			if (script != null)
			{
				if (sb.Length > 0)
				{
					script.commands.Add(sb.ToString());
					sb.Length = 0;
				}
				scripts.Add(script);
			}
			scripts.Sort();
		}

        private bool IsNewSqlCommand(string line)
        {
            line = line.ToLower();
            foreach (string token in newSqlCmdTokens) if (line.StartsWith(token)) return true;
            return false;
        }

		public void Clear()
		{
			scripts.Clear();
		}
		#endregion

		#region Update Database
		public void UpdateDb(GmConnection conn)
		{
			foreach (UpdateScript script in scripts)
			{
				UpdateDb(conn, script);
			}
		}
		void UpdateDb(GmConnection conn, UpdateScript script)
		{
			using(DbTransaction trans = conn.BeginTransaction())
			{
				int cmdId=1;
                try
                {
                    foreach (string cmdText in script.commands)
                    {
                        if (cmdText.Length == 0) continue;
                        conn.ExecuteNonQuery(cmdText, trans);
						cmdId++;
                    }

                    GmCommand cmd = conn.CreateCommand("insert into UpdateScripts values ( @Id, @Date2, @Text )", trans);

                    cmd.AddInt("Id", script.id);
                    cmd.AddDateTime("Date2", DateTime.Now);
                    cmd.AddString("Text", script.GetText().Replace('\n', ' '));

                    cmd.ExecuteNonQuery();

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
					string s = string.Format("UpdateDb failed: scriptId={0} cmdId={1}\r\nDetails: {2}",script.id,cmdId,ex.Message);
                    throw new GmDataException(s, ex);
                }
                finally
                {
                    conn.Close();
                }
			}
		}
		#endregion
	}
}