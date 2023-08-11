using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Models.XmlDataDefinition
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ReportMetaData
    {
        private int _precision = int.MinValue;
        private int _decimal_places = int.MinValue;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Table { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Field { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Label { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Unit_Label { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }
        public bool PrecisionSpecified
        {
            get
            {
                return _precision != int.MinValue;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Decimal_Places
        {
            get { return _decimal_places; }
            set { _decimal_places = value; }
        }

        public bool Decimal_PlacesSpecified
        {
            get
            {
                return _decimal_places != int.MinValue;
            }
        }
    }
}
