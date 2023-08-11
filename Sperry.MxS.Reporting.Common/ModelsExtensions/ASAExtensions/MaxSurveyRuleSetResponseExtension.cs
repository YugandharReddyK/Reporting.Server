namespace Sperry.MxS.Core.Common.Models.ASA
{
    public static class MaxSurveyRuleSetResponseExtension
    {
        public static void AddMaxSurveyRuleRespone(this MaxSurveyRuleSetResponse maxSurveyRuleSetResponse, MaxSurveyRuleResponse ruleResponse)
        {
            ruleResponse.MaxSurveyRuleSetResponse = maxSurveyRuleSetResponse;
            maxSurveyRuleSetResponse.RulesResponse.Add(ruleResponse);
        }

        public static void AddMaxSurveyRuleActionResult(this MaxSurveyRuleSetResponse maxSurveyRuleSetResponse, MaxSurveyActionResult actionResult)
        {
            actionResult.MaxSurveyRuleSetResponse = maxSurveyRuleSetResponse;
            maxSurveyRuleSetResponse.ActionResults.Add(actionResult);
        }
    }
}
