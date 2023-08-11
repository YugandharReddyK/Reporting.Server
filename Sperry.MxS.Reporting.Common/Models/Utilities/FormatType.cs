using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Models.Utilities
{
    [XmlRoot("FormatType"), Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class FormatTypeProperties
    {
        [XmlIgnore()]
        public MxSFormatType Type { get; set; }

        [XmlElement("Format")]
        public List<Format> Formats { get; set; }
    }
}
