using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace Geomethod.Data
{
    public class AccessProvider: GmProviderFactory
    {
        public const string name = "MS Access";
        private string templatePath;
        public AccessProvider(string templatePath)
            : base( OleDbFactory.Instance)
        {
            this.templatePath = templatePath;
        }
        public AccessProvider()
            : base( OleDbFactory.Instance )
        {
//            templatePath = templatepath;
        }
        public override string Name{get{return name;}}
        public override bool SupportsProperty( GmProviderProperty prop )
        {
            switch (prop)
            {
                case GmProviderProperty.IntegratedSecurity: return false;
                case GmProviderProperty.Server: return false;
                case GmProviderProperty.FileSize: return false;
                case GmProviderProperty.FileGrowth: return false;
                case GmProviderProperty.FileMaxSize: return false;
                case GmProviderProperty.AutoFileCreation: return false;
                case GmProviderProperty.Transaction: return false;
                case GmProviderProperty.DDLRollback: return false;
                default: return true;
            }            
        }
        public override string ParameterPrefix
        {
            get
            {
                return "@";
            }
        }
        public override string NameLeftEnclose
        {
            get
            {
                return "[";
            }
        }
        public override string NameRightEnclose
        {
            get
            {
                return "]";
            }
        }
        public override string FileExtension
        {
            get
            {
                return ".mdb";
            }
        }

        #region SQLTypesMapping
        public override string MapString( int length )
        {
            return "varchar" + "(" + length + ")";
        }
        public override string MapInt32( )
        {
            return "int";
        }
        public override string MapDecimal( )
        {
            return "decimal";
        }
        public override string MapSingle( )
        {
            return "real";
        }
        public override string MapDouble( )
        {
            return "float";
        }
        public override string MapDateTime( )
        {
            return "DateTime";
        }
        public override string MapByte( )
        {
            return "TinyInt";
        }
        #endregion

        public override string CreateDatabase( DbCreationProperties props )
        {
            File.Copy(templatePath, props.filePath, true);
            OleDbConnectionStringBuilder olecs = new OleDbConnectionStringBuilder( );
            olecs.DataSource = props.filePath;
            olecs.Provider = "Microsoft.Jet.OLEDB.4.0";
            return olecs.ConnectionString;
        }

        public override string PreProcessCommandText( string cmdText )
        {
//      DZ      30.010.09
//      Удалим комментарии
            for( ; ; )
            {
                int start = cmdText.IndexOf( "/*" );
                if( start == -1 )
                    break;

                int end = cmdText.IndexOf( "*/", start );
                cmdText = cmdText.Remove( start, end - start + 2);
            }

            char[] tokens = "\n\r\t ,;".ToCharArray();
            StringBuilder sb = new StringBuilder(cmdText.Length);
            int prevTokenIndex=-1;
            while (true)
            {
                string word;
                int tokenIndex = cmdText.IndexOfAny(tokens, prevTokenIndex + 1);
                if (tokenIndex >= 0)
                {
                    word = cmdText.Substring(prevTokenIndex + 1, tokenIndex - prevTokenIndex-1);
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

//            if( word.StartsWith( "@" ) )
//                return "?";
            if (word.StartsWith("varchar(max)", StringComparison.OrdinalIgnoreCase)) return "Text";

//            if (word.StartsWith("@")) return ":" + word.Substring(1);
//            if (word.StartsWith("[") && word.EndsWith("]")) return '"' +word.Substring(1,word.Length-2)+ '"';

/*            if( word.StartsWith( "varbinary", StringComparison.OrdinalIgnoreCase ) )
            {
//              varbinary(1000)
                string[]    words = word.Split( "\n\r\t ()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries );
                
                int size = 0;
                bool rc = Int32.TryParse( words[ 1 ], out size );
                if( rc && size >= 1000 )
                    return  "Image";
            }
 */ 
            if( word.StartsWith( "varbinary", StringComparison.OrdinalIgnoreCase ) )
                return "Image";
            if( word.StartsWith( "varchar", StringComparison.OrdinalIgnoreCase ) )
            {
                string[]    words = word.Split( "\n\r\t ()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries );
                
                int size = 0;
                bool rc = Int32.TryParse( words[ 1 ], out size );
                if( rc && size >= 500 )
                    return  "Text";
            }
 
            return word;
        }

        public override string SQLAlterTableAddColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ADD ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";

            return sql;
        }

        public override string SQLAlterTableRenameColumn( DataTable dt, SQLMappingProperty props,
                GmDataColumn column, string newname )
        {
            throw new SQLMappingException( "Function not supported" );
        }

        public override string SQLAlterTableModifyColumn( DataTable dt, SQLMappingProperty props, GmDataColumn column )
        {
            string sql = "ALTER TABLE ";
            sql += GetEnclosedName( dt.TableName, props.EncloseName );
            sql += " ALTER ";
            sql += GetEnclosedName( column.DataColumn.ColumnName, props.EncloseName );
            sql += " " + TypeMapping( column.DataColumn.DataType, column.DataColumn.MaxLength );

            if( props.GenerateNotNull )
                sql += " NOT NULL";

            if( props.GenerateUnique )
                sql += " UNIQUE";

            return sql;
        }
    }
}