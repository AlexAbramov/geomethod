using System;
using System.Collections;
using System.Text;
using System.Data;
using Geomethod.Data;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace Geomethod.Data
{
    public class SqlServerCEProvider : SqlServerProvider
	{
		public new const string name = "MS SQL Server Compact Edition";

		public SqlServerCEProvider()
			: base(SqlCeProviderFactory.Instance)
		{
        }

        public override string Name { get { return name; } }
		public override string FileExtension {get { return ".sdf"; }}

		public override bool SupportsProperty(GmProviderProperty prop)
		{
			switch (prop)
			{
				case GmProviderProperty.IntegratedSecurity: return false;
				case GmProviderProperty.Server: return false;
				case GmProviderProperty.FileSize: return false;
				case GmProviderProperty.FileGrowth: return false;
				case GmProviderProperty.FileMaxSize: return false;
				case GmProviderProperty.AutoFileCreation: return false;
//				case GmProviderProperty.DDLRollback: return false;
				default: return true;
			}
		}

		public override string CreateDatabase(DbCreationProperties props)
		{
			StringBuilder sb = new StringBuilder(1024);
			sb.AppendFormat("Data Source={0};", props.filePath);
			sb.AppendFormat("Password={0};", props.userLogin.password);
			string connStr = sb.ToString();
			SqlCeEngine ceEngine = new SqlCeEngine(connStr);
			ceEngine.CreateDatabase();
			return connStr;
		}

		public override string PreProcessCommandText(string cmdText)
		{
			for (; ; )
			{
				int start = cmdText.IndexOf("/*");
				if (start == -1)
					break;

				int end = cmdText.IndexOf("*/", start);
				cmdText = cmdText.Remove(start, end - start + 2);
			}

			char[] tokens = "\n\r\t ,;".ToCharArray();
			StringBuilder sb = new StringBuilder(cmdText.Length);
			int prevTokenIndex = -1;
			while (true)
			{
				string word;
				int tokenIndex = cmdText.IndexOfAny(tokens, prevTokenIndex + 1);
				if (tokenIndex >= 0)
				{
					word = cmdText.Substring(prevTokenIndex + 1, tokenIndex - prevTokenIndex - 1);
				}
				else
				{
					word = cmdText.Substring(prevTokenIndex + 1);
				}

				if (word.Length > 0)
				{
					word = FixWord(word);
					sb.Append(word);
				}
				if (tokenIndex >= 0)
				{
					char token = cmdText[tokenIndex];
					sb.Append(token);
					prevTokenIndex = tokenIndex;
				}
				else break;
			}
			string res = sb.ToString();

			return res;
		}

		private string FixWord(string word)
		{
			if (word.StartsWith("varchar(max)", StringComparison.OrdinalIgnoreCase)) return "ntext";
			if (word.StartsWith("varbinary", StringComparison.OrdinalIgnoreCase)) return "image";
			if (word.StartsWith("varchar", StringComparison.OrdinalIgnoreCase))
			{
				string[] words = word.Split("\n\r\t ()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				if (words.Length > 1)
				{
					int size = 0;
					bool rc = Int32.TryParse(words[1], out size);
					if (rc && size > 510)
						return "ntext";
				}
				return word.Replace("varchar","nvarchar");
			}

			return word;
		}

		public override DbType FixDbParameterType(DbType dbType)
		{
			switch (dbType)
			{
				case DbType.AnsiString: return DbType.String;
				case DbType.AnsiStringFixedLength: return DbType.StringFixedLength;
			}
			return dbType;
		}

	}
}
