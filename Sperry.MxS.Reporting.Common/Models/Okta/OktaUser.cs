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
    public class OktaUser
    {
        [JsonProperty]
        public string Id { get; set; } = string.Empty;

        [JsonProperty]
        public string Status { get; set; } = string.Empty;

        [JsonProperty]
        public DateTime Created { get; set; }

        [JsonProperty]
        public DateTime? Activated { get; set; }

        [JsonProperty]
        public DateTime? StatusChanged { get; set; }

        [JsonProperty]
        public DateTime LastLogin { get; set; }

        [JsonProperty]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty]
        public DateTime? PasswordChanged { get; set; }

        [JsonProperty]
        public OktaUserProfile Profile { get; set; }
        
    }
}
