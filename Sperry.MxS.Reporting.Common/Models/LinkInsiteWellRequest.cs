using Newtonsoft.Json;
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
    public class LinkInsiteWellRequest
    {
        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public string UserName { get; set; }

        [JsonProperty]
        public bool ForceRelink { get; set; }
    }
}
