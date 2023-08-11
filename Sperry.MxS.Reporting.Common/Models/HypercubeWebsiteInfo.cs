using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class HypercubeWebsiteInfo : DataModelBase
    {
        [JsonProperty]
        public string HypercubeSiteName { get; set; }

        [JsonProperty]
        public string Websiteurl { get; set; }

        [JsonProperty]
        public string Username { get; set; }

        [JsonProperty]
        public string Password { get; set; }

        [JsonProperty]
        public string SiteStatus { get; set; }

        [JsonProperty]
        public DateTime? MinDate { get; set; }

        [JsonProperty]
        public DateTime? MaxDate { get; set; }

        [JsonProperty]
        public double? MinDepth { get; set; }

        [JsonProperty]
        public double? MaxDepth { get; set; }

        [JsonProperty]
        public double? MinLatitude { get; set; }

        [JsonProperty]
        public double? MaxLatitude { get; set; }

        [JsonProperty]
        public double? MinLongitude { get; set; }

        [JsonProperty]
        public double? MaxLongitude { get; set; }
    }

}
