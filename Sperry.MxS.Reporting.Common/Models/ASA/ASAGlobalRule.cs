using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ASAGlobalRule : ASAGlobalRuleBase
    {
        [JsonProperty]
        public virtual ASAGlobalPreConditionRule ASAGlobalPreConditionRule { get; set; }

        [JsonProperty]
        public Guid? PreConditionRuleId { get; set; } = Guid.Empty;
    }
}