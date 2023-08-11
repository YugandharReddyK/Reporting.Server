using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    [Serializable]
    public class MxSServerInfo
    {
        [JsonProperty]
        public bool IsCloudDeployment { get; set; } = true;

        [JsonProperty]
        public NetworkInfo NetworkInfo { get; set; } = new NetworkInfo();

        [JsonProperty]
        public string HubName { get; set; } = string.Empty;
    }
}
