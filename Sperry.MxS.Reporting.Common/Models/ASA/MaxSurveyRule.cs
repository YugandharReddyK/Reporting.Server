using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Sperry.MxS.Core.Common.Ex.Helpers;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MaxSurveyRule : RuleBase
    {
        public MaxSurveyRule()
        { }

        public MaxSurveyRule(MaxSurveyRule maxSurveyRuleToCopy)
            : this()
        {
            LHS = maxSurveyRuleToCopy.LHS;
            RHS = maxSurveyRuleToCopy.RHS;
            LogicalCondition = maxSurveyRuleToCopy.LogicalCondition;
            Operator = maxSurveyRuleToCopy.Operator;
            RuleName = maxSurveyRuleToCopy.RuleName;
            Solution = maxSurveyRuleToCopy.Solution;
            UnitSystem = maxSurveyRuleToCopy.UnitSystem;
            IsMandatory = maxSurveyRuleToCopy.IsMandatory;
            PreConditionRule = maxSurveyRuleToCopy.PreConditionRule;
            IsValidRule = maxSurveyRuleToCopy.IsValidRule;
            Deleted = maxSurveyRuleToCopy.Deleted;
        }

        public MaxSurveyRule(ASAGlobalRule globalRule)
        {
            LHS = globalRule.LHS;
            RHS = globalRule.RHS;
            IsMandatory = globalRule.IsMandatory;
            RuleName = globalRule.RuleName;
            Operator = globalRule.Operator;
            LogicalCondition = globalRule.LogicalCondition;
            IsValidRule = true;
        }

        [JsonProperty]
        public Guid SolutionId { get; set; }

        [JsonProperty]
        public virtual Solution Solution { get; set; }

        [JsonProperty]
        public bool IsMandatory { get; set; } = false;

        [JsonProperty]
        public bool IsEnabled { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public MaxSurveyPreConditionRule PreConditionRule { get; set; }

        [JsonProperty]
        public Guid PreConditionMapId { get; set; } = Guid.Empty;

        //[NotMapped]
        // TODO Sandeep - Because of IsValidRule is getting false (in old model [ignorechange] attribute used,didn't get the proper replacement for new one so kept the same)
        [IgnoreChange]
        [JsonProperty]
        public bool? IsValidRule { get; set; } = false;
    }
}
