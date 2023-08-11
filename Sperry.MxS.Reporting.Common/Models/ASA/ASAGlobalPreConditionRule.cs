using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sperry.MxS.Core.Common.Models.ASA;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ASAGlobalPreConditionRule : ASAGlobalRuleBase
    {
        public List<ASAGlobalRule> ASAGlobalRule { get; protected set; }
    }
}
 