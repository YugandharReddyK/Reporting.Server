using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public abstract class RuleBase : DataModelBase
    {
        public RuleBase()
        { }

        [JsonProperty]
        public string LHS { get; set; } = string.Empty;

        [JsonProperty] 
        public string RHS { get;set; } = string.Empty;

        [JsonProperty]
        public MxSLogicalConditionEnum LogicalCondition { get; set; } = MxSLogicalConditionEnum.And;

        [JsonProperty]
        public MxSOperatorsEnum Operator { get; set; } = MxSOperatorsEnum.Equals;

        [JsonProperty]
        public string RuleName { get; set; } = string.Empty;
        
        [JsonProperty]
        public string UnitSystem { get; set; } = string.Empty;

        [JsonProperty]
        public bool Deleted { get; set; }

    }
}
