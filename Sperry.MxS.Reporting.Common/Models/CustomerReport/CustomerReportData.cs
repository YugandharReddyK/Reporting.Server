using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public partial class CustomerReportData : DataModelBase
    {
        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public byte[] ReportData { get; set; }
    }
}
