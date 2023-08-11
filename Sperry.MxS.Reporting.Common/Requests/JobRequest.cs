using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Requests
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class JobRequest
    {
        [JsonProperty]
        public string Value { get; set; }

        [JsonProperty]
        public byte[] Payload { get; set; }

        [JsonProperty]
        public Dictionary<string, byte[]> Files { get; set; } = new Dictionary<string, byte[]>();

        [JsonProperty]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
