using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.RulesEngine;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    public static class MaxSurveyRuleExtension
    {
        public static Rule MapMxSRuleToRule(this MaxSurveyRule rule)
        {
            return new Rule()
            {
                IsEnabled = rule.IsEnabled,
                PreConditionRule = rule.PreConditionRule?.MapMxSPreConditionRuleToRule(),
                LHS = rule.LHS,
                RHS = rule.RHS,
                LogicalCondition = rule.LogicalCondition,
                Operator = rule.Operator,
                RuleName = rule.RuleName,   
            };
        }
    }
}
