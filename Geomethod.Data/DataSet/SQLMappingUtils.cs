using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Geomethod.Data
{
/*
    public enum SQLMappingProperty
    {
        EncloseName,
        GenerateNotNull,
        GenerateUnique

    }

    public class SQLMappingProperties
    {
        Dictionary<SQLMappingProperty, string> props = new Dictionary<SQLMappingProperty, string>( );
        public string this[ SQLMappingProperty p ]
        {
            get
            {
                return props.ContainsKey( p ) ? props[ p ] : null;
            }
            set
            {
                props[ p ] = value;
            }
        }
        public bool HasProperty( SQLMappingProperty p )
        {
            return props.ContainsKey( p );
        }
    }
 */

    public class SQLMappingProperty
	{
        public bool EncloseName;
        public bool GenerateNotNull;
        public bool GenerateUnique;

        public SQLMappingProperty( )
        {
            EncloseName = false;
            GenerateNotNull = true;
            GenerateUnique = true;
        }
	}

    public class SQLMappingException : Exception
    {
        public string errormessage;
        public SQLMappingException( string text )
        {
            errormessage = text;
        }
    }

    /*
            public enum SQLMappingStatus
            {
                New,
                Drop,
                Add,
                Done,
                Nothing
            }
        */
/*    public class SQLMapping
    {
        Dictionary< string , SQLMappingStatus> stati = new Dictionary<string, SQLMappingStatus>( );
        public SQLMappingStatus this[ string p ]
        {
            get
            {
                return stati.ContainsKey( p ) ? stati[ p ] : SQLMappingStatus.Nothing;
            }
            set
            {
                stati[ p ] = value;
            }
        }
        public bool HasProperty(  string p )
        {
            return stati.ContainsKey( p );
        }
        public void Clear( )
        {
            stati.Clear( );
        }
    }
*/
    public class SQLMappingLog:IDisposable
    {
        StreamWriter sw = null;
        List<string> sqlcommands;
        bool sync;


        public SQLMappingLog( )
        {
            sqlcommands = new List<string>();
        }

        public SQLMappingLog( string filename, bool sync )
        {
            sqlcommands = new List<string>();
            sw = new StreamWriter( filename, false );
            this.sync = sync;

        }

        public void Write( string str )
        {
            sqlcommands.Add( str );
            sw.WriteLine( str );
            if( sync )
                sw.Flush( );
        }

        public void Dispose( )
        {
            sqlcommands.Clear( );
            if( sw != null )
                sw.Close( );
        }
    }
}