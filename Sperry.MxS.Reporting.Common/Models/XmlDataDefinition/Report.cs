using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Models.XmlDataDefinition
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Report
    {
        public ReportFromApp From_App { get; set; } = new ReportFromApp();


        [System.Xml.Serialization.XmlArrayItemAttribute("Meta_Data", IsNullable = false)]
        public List<ReportMetaData> MetaData { get;set; } = new List<ReportMetaData>();
    }
}
