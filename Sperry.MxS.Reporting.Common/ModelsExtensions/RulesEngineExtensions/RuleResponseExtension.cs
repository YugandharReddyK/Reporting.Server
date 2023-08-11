using Sperry.MxS.Core.Common.Models.ASA;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    public static class RuleResponseExtension
    {
        public static MaxSurveyRuleResponse MapRuleResponseToMxSRuleResponse(this RuleResponse response)
        {
            return new MaxSurveyRuleResponse()
            {
                RuleName = response.RuleName,
                RuleExpression = response.RuleExpression,
                RuleResultObject = response.RuleResultObject,
                PreConditionExpression = response.PreConditionExpression,
                PreConditionResult = response.PreConditionResult,
                Result = response.Result,
                Message = response.Message
            };
        }
        public static void AddForeignKey(this MaxSurveyRuleResponse maxSurveyRule, MaxSurveyRuleSetResponse maxSurvey)
        {
            maxSurveyRule.MaxSurveyRuleSetResponseId = maxSurvey.Id;
        }
    }
}
