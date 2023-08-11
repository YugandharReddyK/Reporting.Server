using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.ASA;
using System.ComponentModel.DataAnnotations.Schema;
using Sperry.MxS.Core.Common.Ex.Helpers;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MaxSurveyPreConditionRule : RuleBase
    {
        public MaxSurveyPreConditionRule()
        {
            PreConditionMapId = Guid.NewGuid();
        }

        public MaxSurveyPreConditionRule(MaxSurveyPreConditionRule maxSurveyRuleToCopy)
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
            IsValidRule = maxSurveyRuleToCopy.IsValidRule;
            Deleted = maxSurveyRuleToCopy.Deleted;
        }

        public MaxSurveyPreConditionRule(ASAGlobalPreConditionRule maxSurveyRuleToCopy)
            : this()
        {
            LHS = maxSurveyRuleToCopy.LHS;
            RHS = maxSurveyRuleToCopy.RHS;
            LogicalCondition = maxSurveyRuleToCopy.LogicalCondition;
            Operator = maxSurveyRuleToCopy.Operator;
            RuleName = maxSurveyRuleToCopy.RuleName;
            UnitSystem = maxSurveyRuleToCopy.UnitSystem;
            IsMandatory = maxSurveyRuleToCopy.IsMandatory;
            IsValidRule = maxSurveyRuleToCopy.IsValidRule;
            Deleted = maxSurveyRuleToCopy.Deleted;
        }

        [JsonProperty]
        public Guid SolutionId { get; set; }

        [JsonProperty]
        public virtual Solution Solution { get; set; }

        [JsonProperty]
        public bool IsMandatory { get; set; } = false;

        [JsonProperty]
        public Guid PreConditionMapId { get; set; } = Guid.Empty;

        [JsonProperty]
        public bool IsEnabled { get; set; } = true;

        //[NotMapped]
        // TODO Sandeep - Because of IsValidRule is getting false (in old model [ignorechange] attribute used,didn't get the proper replacement for new one so kept the same)
        [IgnoreChange]
        [JsonProperty]
        public bool? IsValidRule { get; set; } = false; 

    }
}
