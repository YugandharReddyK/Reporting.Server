using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BillingReport
    {
        [JsonProperty]
        public MxSROCOptions ROC { get; set; }

        [JsonProperty]
        public string Region { get; set; }

        [JsonProperty]
        public string WellName { get; set; }

        [JsonProperty]
        public string JobNumber { get; set; }

        [JsonProperty]
        public string District { get; set; }

        [JsonProperty]
        public string Country { get; set; }

        [JsonProperty]
        public DateTime StartDateTime { get; set; }

        [JsonProperty]
        public DateTime EndDateTime { get; set; }

        [JsonProperty]
        public DateTime DateTime { get; set; }

        [JsonProperty]
        public int SurveyCount { get; set; }
    }
}
