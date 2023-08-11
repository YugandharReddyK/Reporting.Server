using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.CustomerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class SubReportPayload
    {
        public IEnumerable<CustomerReportTemplate> subreportTemplates { get; set; }

        public ReportParameters ReportParameters { get; set; }

    }
}
