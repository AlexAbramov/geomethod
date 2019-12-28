using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
    public class ImportTable
    {
        public static void ImportData(ConnectionFactory fact, string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                if (!dirPath.EndsWith("\\")) dirPath += "\\";
                foreach (string filePath in Directory.GetFiles(dirPath, "*.csv"))
                {
                    try
                    {
                        ImportTable importTable = new ImportTable(fact);
                        importTable.ImportData(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw new GmDataException("Failed to import data: " + filePath, ex);
                    }
                }
            }
        }

        ConnectionFactory fact;
        DbDataAdapter dataAdapter;
        DataTable dataTable = new DataTable();
        public ImportTable(ConnectionFactory fact)
        {
            this.fact = fact;
        }
        public void ImportData(string filePath)
        {
            string tableName = Path.GetFileNameWithoutExtension(filePath);
            using (GmConnection conn = fact.CreateConnection())
            {
                dataAdapter = conn.CreateDataAdapter("select top(0) * from " + tableName);
                dataAdapter.Fill(dataTable);
            }
            ReadData(filePath);
            using (GmConnection conn = fact.CreateConnection())
            {
                WriteData(conn);
            }
        }
        void ReadData(string filePath)
        {
            CsvConverter cc = new CsvConverter();
            int lineCount = 0;
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                List<string> header=null;
                List<string> items = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineCount++;
                    try
                    {
                        cc.Parse(line, items);
                        if (lineCount == 1)// header
                        {
                            header = new List<string>(items);
                        }
                        else if (items.Count == header.Count)
                        {
                            DataRow dr = dataTable.NewRow();
                            for (int i = 0; i < header.Count; i++)
                            {
                                string col = header[i];
                                DataColumn dc = dataTable.Columns[col];
                                SetValue(dr, dc, items[i]);
                            }
                            dataTable.Rows.Add(dr);
                        }
                    }
                    catch (Exception ex)
                    {
                        string s = string.Format("Failed to parse line {0}: {1}",lineCount,line);
                        throw new GmDataException(s, ex);
                    }
                }
            }
        }

        private void SetValue(DataRow dr, DataColumn dc, string val)
        {
            object obj=GetValue(dc.DataType,val);
            dr[dc] = obj;
        }

        private object GetValue(Type type, string val)
        {
            switch (type.Name.ToLower())
            {
                case "int32": return int.Parse(val);
                case "int64": return long.Parse(val);
                case "decimal": return decimal.Parse(val);
                case "single": return float.Parse(val);
                case "double": return double.Parse(val);
                case "datetime": return DateTime.Parse(val);
                case "byte": return byte.Parse(val);
                default: return val;
            }
        }

        void WriteData(GmConnection conn)
        {
            DbCommandBuilder bld = conn.CreateCommandBuilder();
            bld.DataAdapter = dataAdapter;
            dataAdapter.Update(dataTable);
        }
    }
}
