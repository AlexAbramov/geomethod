using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{
    [XmlType("Table")]
    public class TableSchema
    {
        [XmlAttribute]
        public string name;
        [XmlArray("Columns")]
        public List<ColumnSchema> columnSchemas = new List<ColumnSchema>();

        #region Construction
        public TableSchema() { }
        public TableSchema(string name)
        {
            this.name = name;
        }
        #endregion

        #region Methods
        public void Add(ColumnSchema columnSchema) { columnSchemas.Add(columnSchema); }
        public void Remove(ColumnSchema columnSchema) { columnSchemas.Remove(columnSchema); }
        #endregion
    }
}
