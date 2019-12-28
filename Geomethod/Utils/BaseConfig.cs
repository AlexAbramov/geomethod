using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Geomethod
{
	public abstract class BaseConfig: ICloneable
	{
		#region Static
		protected static BaseConfig DeserializeFile(Type type, string filePath) 
        {
			BaseConfig cfg = (BaseConfig)XmlUtils.DeserializeFile(type, filePath);
			cfg.OnDeserialized();
            return cfg;
        }
		public static BaseConfig DeserializeString(Type type, string xmlString)
        {
			BaseConfig cfg = (BaseConfig)XmlUtils.DeserializeString(type, xmlString);
			cfg.OnDeserialized();
			return cfg;
		}
		#endregion

		#region Fields
		public int version;
		public DateTime date;
		public string checksum;
		#endregion

		#region Construction
		protected BaseConfig()
		{
			version = CurrentVersion;
            date = DateTime.Now;
            checksum = "";
		}
		#endregion

		#region Abstract/Virtual
		public abstract int CurrentVersion { get;}
		public abstract object Clone();
		protected virtual void OnDeserialized() { }
		#endregion

		#region Serialization
		public string Serialize() { return Serialize(false); }
		public string Serialize(bool setChecksum) 
		{
			SetChecksum(setChecksum);
			return XmlUtils.Serialize(this); 
		}
		public void Serialize(string filePath) { Serialize(filePath, false); }
		public void Serialize(string filePath, bool setChecksum)
		{
			SetChecksum(setChecksum);
			XmlUtils.Serialize(filePath, this);
		}

		private void SetChecksum(bool setChecksum)
		{
            version = CurrentVersion;
			date = DateTime.Now;
			checksum =setChecksum ? GetCheckSum(): "";
		}
		#endregion

		#region Aux
		public string GetCheckSum()
		{
			XmlSerializer xs = new XmlSerializer(this.GetType());
			using (MemoryStream ms = new MemoryStream(1 << 16))
			{
				using (TextWriter writer = new StreamWriter(ms))
				{
					xs.Serialize(writer, this);
					long count = ms.Length;
					long sum = 0;
					ms.Position = 0;
					for (long i = 0; i < count; i++) sum += ms.ReadByte();
					return string.Format("{0:X}", sum);
				}
			}
		}
		#endregion

	}
}
