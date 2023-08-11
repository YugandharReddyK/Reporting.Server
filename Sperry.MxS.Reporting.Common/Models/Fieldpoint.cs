using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute("field-point", Namespace = "", IsNullable = false)]
    public class Fieldpoint
    {
        [NotMapped]
        [JsonProperty]
        [XmlElement(ElementName = "depth")]
        public double TVDss { get; set; }

        [NotMapped]
        [JsonProperty]
        [XmlElementAttribute(DataType = "date")]

        public DateTime Date { get; set; }

        [NotMapped]
        [JsonProperty]
        [XmlElement(ElementName = "latitude")]
        public double Latitude { get; set; }

        [NotMapped]
        [JsonProperty]
        [XmlElement(ElementName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "declination")]
        public double Declination { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "inclination")]
        public double Inclination { get; set; }

        [JsonProperty]
        [XmlElement(ElementName = "totalIntensity")]
        public double TotalIntensity { get; set; }
    }
}
