using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Geomethod
{
	public struct StringPair
	{
		[XmlAttribute]
		public string key;
		[XmlAttribute]
		public string value;
		public StringPair(string key, string value) { this.key = key; this.value = value; }
		public StringPair(KeyValuePair<string, string> pair){key = pair.Key; value = pair.Value;}
	}

	public class XmlUtils
	{
		public static string ToString(List<int> list){ StringBuilder sb = new StringBuilder(list.Count * sizeof(int) * 2); foreach (int i in list) { sb.Append(i); sb.Append(';'); }; return sb.ToString();}
		public static void FromString(List<int> list, string str) { string[] ss = str.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries); if (list.Capacity < ss.Length) list.Capacity = ss.Length; foreach (string s in ss) list.Add(int.Parse(s)); }
		public static StringPair[] GetStringPairs(Dictionary<string, string> dict) 
		{
			StringPair[] pairs = new StringPair[dict.Count];
			int i = 0;
			foreach (KeyValuePair<string, string> pair in dict) pairs[i++] = new StringPair(pair);
			return pairs;
		}
		public static void InitDictionary(Dictionary<string, string> dict,StringPair[] pairs)
		{
			foreach (StringPair pair in pairs) dict.Add(pair.key, pair.value); 
		}
		public static string Serialize(object obj) { return Serialize(obj, Encoding.UTF8); }
		public static string Serialize(object obj, Encoding encoding)
		{
			XmlSerializer xs = new XmlSerializer(obj.GetType());
			MemoryStream ms = new MemoryStream();
			using (TextWriter writer = new StreamWriter(ms, encoding))
			{
				xs.Serialize(writer, obj, GetEmptyXSN());
			}
			return encoding.GetString(ms.GetBuffer());
		}
		public static void Serialize(string filePath, object obj) { Serialize(filePath, obj, Encoding.UTF8); }
		public static void Serialize(string filePath, object obj, Encoding encoding)
		{
			XmlSerializer xs = new XmlSerializer(obj.GetType());
			using (TextWriter writer = new StreamWriter(filePath, false, encoding))
			{
				xs.Serialize(writer, obj, GetEmptyXSN());
			}
		}
		public static XmlSerializerNamespaces GetEmptyXSN() 
		{ 
			XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
			xsn.Add(String.Empty, String.Empty);
			return xsn;
		}
		public static object DeserializeString(Type type, string xmlString) { return DeserializeString(type, xmlString, Encoding.UTF8); }
		public static object DeserializeString(Type type, string xmlString, Encoding encoding)
		{
			byte[] buf = encoding.GetBytes(xmlString);
			MemoryStream ms = new MemoryStream(buf);
			XmlSerializer xs = new XmlSerializer(type);
			using (TextReader reader = new StreamReader(ms, encoding))
			{
				return xs.Deserialize(reader);
			}
		}
		public static object DeserializeFile(Type type, string filePath) { return DeserializeFile(type, filePath, Encoding.UTF8); }
		public static object DeserializeFile(Type type, string filePath, Encoding encoding)
		{
			XmlSerializer xs = new XmlSerializer(type);
			using (TextReader reader = new StreamReader(filePath, encoding))
			{
				return xs.Deserialize(reader);
			}
		}
	}
}
