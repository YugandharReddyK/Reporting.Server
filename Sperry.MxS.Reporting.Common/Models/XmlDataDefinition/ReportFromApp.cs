using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Models.XmlDataDefinition
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ReportFromApp
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Plot", IsNullable = false)]
        public List<ReportPlot> Plots { get;set; }
    }
}
