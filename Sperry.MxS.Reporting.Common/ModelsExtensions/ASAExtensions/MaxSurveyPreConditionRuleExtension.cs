using Sperry.MxS.Core.Common.Models.RulesEngine;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    public static class MaxSurveyPreConditionRuleExtension
    {
        public static Rule MapMxSPreConditionRuleToRule(this MaxSurveyPreConditionRule preConditionRule)
        {
            return new Rule()
            {
                LHS = preConditionRule.LHS,
                RHS = preConditionRule.RHS,
                LogicalCondition = preConditionRule.LogicalCondition,
                Operator = preConditionRule.Operator,
                RuleName = preConditionRule.RuleName,
                IsEnabled = preConditionRule.IsEnabled
            };
        }
    }
}
