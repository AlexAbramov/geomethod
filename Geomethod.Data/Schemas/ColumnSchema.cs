using System;
using System.ComponentModel;
using System.Data;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geomethod.Data
{
    [XmlType("Column")]
    public class ColumnSchema
    {        
        [XmlAttribute]
        public string name;
        [XmlIgnore]
        public ColType type = ColType.String;
        [XmlAttribute("type")]
        public string ColTypeStr { get { return type.ToString().ToLower(); } set { type = (ColType)Enum.Parse(typeof(ColType),value,true);} } 
        [XmlAttribute]
        [DefaultValue(0)]
        public int length = 0;

        public ColumnSchema(){}
        public ColumnSchema(string name, ColType type) 
        {
            this.name = name;
            this.type = type;
        }

        public ColumnSchema(string name, int length)
        {
            this.name = name;
            this.length = length;
        }
        public ColumnSchema(string name)
        {
            this.name = name;
            this.length = 50;
        }
    }
}
