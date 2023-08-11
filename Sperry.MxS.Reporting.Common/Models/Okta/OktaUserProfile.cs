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
    public class OktaUserProfile
    {
        [JsonProperty]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty]
        public string MobilePhone { get; set; } = string.Empty;

        [JsonProperty]
        public string Organization { get; set; } = string.Empty;

        [JsonProperty]
        public string SecondEmail { get; set; } = string.Empty;

        [JsonProperty]
        public string Justification { get; set; } = string.Empty;

        [JsonProperty]
        public string Login { get; set; } = string.Empty;

        [JsonProperty]
        public string Email { get; set; } = string.Empty;
    }
}
