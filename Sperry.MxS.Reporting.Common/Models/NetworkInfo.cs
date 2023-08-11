using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class NetworkInfo
    {
        [JsonProperty]
        public string Address { get; set; } = string.Empty;

        [JsonProperty]
        public int Port { get; set; } = 0;
    }
}
