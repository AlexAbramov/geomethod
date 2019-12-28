using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{    
    [XmlRoot]
	public class DataSchema
    {
        #region Fields
        [XmlArray("DataSets")]
        public List<DataSetSchema> dataSetSchemas = new List<DataSetSchema>();
        #endregion

        #region Properties
        public int Count { get { return dataSetSchemas.Count; } }
        #endregion

        #region Construction
        public DataSchema(){}
        #endregion

        #region Methods
        public void Add(DataSetSchema dataSetSchema) { dataSetSchemas.Add(dataSetSchema); }
        public void Remove(DataSetSchema dataSetSchema) { dataSetSchemas.Remove(dataSetSchema); }        
        #endregion



        //#region Fields        
        //public string name = "";
        //public int cloneId = 0;
        //public int poolSize = 1000000;
        //public int appPoolSize = 10;
        //[XmlIgnore]
        //public List<int> poolIds = new List<int>();
        //#endregion

        //#region Properties
        //[XmlAttribute]
        //public string PoolIdsStr { get { return XmlUtils.ToString(poolIds); } set { XmlUtils.FromString(poolIds, value); } }
        //#endregion

        //#region Construction
        //public static DbXmlInfo DeserializeString(string xmlString)
        //{
        //    if (String.IsNullOrEmpty(xmlString)) return new DbXmlInfo();
        //    return (DbXmlInfo)XmlUtils.DeserializeString(typeof(DbXmlInfo), xmlString);
        //}
        //public DbXmlInfo()
        //{
        //    poolIds.Add(0);
        //}
        //#endregion

        //public string Serialize()
        //{
        //    return XmlUtils.Serialize(this);
        //}
	}

}
