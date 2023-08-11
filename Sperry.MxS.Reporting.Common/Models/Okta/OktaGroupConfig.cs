using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Okta
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class OktaGroupConfig
    {
        [JsonProperty]
        public string BaseUrl { get; set; } = string.Empty;

        [JsonProperty]
        public string StaticToken { get; set; } = string.Empty;
    }
}
