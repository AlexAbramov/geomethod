using System;
using System.Data;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geomethod;
using Geomethod.Data;

namespace Test
{
    class Program
    {
        const string filePath = @"c:\temp\dataSchema.xml";
        static void Main(string[] args)
        {
            new Program();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        Program()
        {
//            WriteFile();
            ReadFile();
        }

        private void ReadFile()
        {

            var ds=XmlUtils.DeserializeFile(typeof(DataSchema), filePath);
            Console.WriteLine("File deserialized.");
        }

        private void WriteFile()
        {
            var dataSchema=CreateTestDataSchema();
            XmlUtils.Serialize(filePath, dataSchema);
        }

        private DataSchema CreateTestDataSchema()
        {
            var ds = new DataSchema();
            var ss = new DataSetSchema();
            var ts = new TableSchema("Customers");
            var cs = new ColumnSchema("id", ColType.Int);
            ts.Add(cs);
            cs = new ColumnSchema("name");
            ts.Add(cs);
            ss.Add(ts);
            ds.Add(ss);
            return ds;
        }
    }
}