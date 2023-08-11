using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sperry.MxS.Core.Common.Utilities
{
    [Serializable]
    public class Format
    {
        [XmlAttribute("unitName")]
        public string UnitName { get; set; }

        [XmlAttribute("precision")]
        public int Precision { get; set; }
    }
}
