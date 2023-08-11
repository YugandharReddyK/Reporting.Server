using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Models.XmlDataDefinition
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ReportPlot
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path { get; set; }
    }
}
