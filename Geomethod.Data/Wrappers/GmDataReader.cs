using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class GmDataReader : IDisposable
	{
		DbDataReader dataReader;
		int ordinal = 0;
		public int Ordinal { get { return ordinal; } set { ordinal = value; } }
		public bool IsDBNull { get { return dataReader.IsDBNull(ordinal); } }
		bool IncNull { get { if (IsDBNull) { ordinal++; return true; } else return false; } }
		public GmDataReader(DbDataReader dataReader)
		{
			this.dataReader = dataReader; 
		}
		public bool Read() { ordinal = 0; return dataReader.Read(); }
		public int GetOrdinal(string name) { return dataReader.GetOrdinal(name);}
        public bool GetBoolean() { return IncNull ? false : dataReader.GetBoolean(ordinal++); }
		public byte GetByte() { return IncNull ? Byte.MinValue : dataReader.GetByte(ordinal++); }
        public int GetInt() { return IncNull ? 0 : dataReader.GetInt32(ordinal++); }
        public int GetInt32() { return GetInt(); }
		public string GetString() { return IncNull ? "" : dataReader.GetString(ordinal++);}
//        public string GetString(string name) { int ord = GetOrdinal(name); return dataReader.IsDBNull(ord) ?"": dataReader.GetString(ord);}
        public decimal GetDecimal() { return IncNull ? 0 : dataReader.GetDecimal(ordinal++); }
		public DateTime GetDateTime() { return IncNull ? DateTime.MinValue : dataReader.GetDateTime(ordinal++);}
		public byte[] GetBytes() { return IncNull ? new byte[0] : (byte[])dataReader.GetValue(ordinal++);}
        public static implicit  operator GmDataReader(DbDataReader dr) {return new GmDataReader(dr);}

        public void Dispose()
        {
            if (dataReader != null)
            {
                dataReader.Dispose();
                dataReader = null;
            }
        }
    }
}
