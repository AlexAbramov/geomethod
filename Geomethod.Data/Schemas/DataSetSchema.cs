using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{
    [XmlType("DataSet")]
    public class DataSetSchema
    {
        [XmlAttribute]
        public string name;
        [XmlArray("Tables")]
        public List<TableSchema> tableSchemas = new List<TableSchema>();

        #region Methods
        public void Add(TableSchema tableSchema) { tableSchemas.Add(tableSchema); }
        public void Remove(TableSchema tableSchema) { tableSchemas.Remove(tableSchema); }
        #endregion
    }
}
