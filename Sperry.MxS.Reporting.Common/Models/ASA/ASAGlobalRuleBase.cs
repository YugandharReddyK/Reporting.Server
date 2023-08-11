using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ASAGlobalRuleBase : RuleBase
    {
        [JsonProperty]
        public bool IsMandatory { get; set; } = false;

        [JsonProperty]
        public bool IsEnabled { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public bool? IsValidRule { get; set; } = false;
    }
}