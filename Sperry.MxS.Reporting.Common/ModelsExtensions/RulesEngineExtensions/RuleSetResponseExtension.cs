using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.Surveys;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    public static class RuleSetResponseExtension
    {
        public static MaxSurveyRuleSetResponse MapRuleSetToMxSRuleSetResponse(this RuleSetResponse response)
        {
            return new MaxSurveyRuleSetResponse()
            {
                RulesResponse = response?.RulesResponse?.Select(rule => rule.MapRuleResponseToMxSRuleResponse()).ToList() ?? new List<MaxSurveyRuleResponse>(),
                ActionResults = response?.ActionResults?.Select(action => action.MapActionToMxSAction()).ToList() ?? new List<MaxSurveyActionResult>(),
                Message = response?.Message,
                Result = (MxSRuleResultEnum)(response?.Result)
            };
        }
        public static void AddForeignKey(this MaxSurveyRuleSetResponse maxSurvey, CorrectedSurvey survey)
        {
            maxSurvey.CorrectedSurveyId = survey.Id;
            foreach (var maxSurveyAction in maxSurvey.ActionResults)
            {
                maxSurveyAction.AddForeignKey(maxSurvey);
            }
            foreach (var maxSurveyRule in maxSurvey.RulesResponse)
            {
                maxSurveyRule.AddForeignKey(maxSurvey);
            }
        }
    }
}
