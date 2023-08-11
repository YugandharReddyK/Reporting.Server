using Sperry.MxS.Core.Common.Models.ASA;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    public static class ActionResultExtension
    {
        public static MaxSurveyActionResult MapActionToMxSAction(this ActionResult action) 
        {
            return new MaxSurveyActionResult()
            {
                ActionName = action.ActionName,
                Result = action.Result,
                Message = action.Message,
            };
        }
        public static void AddForeignKey(this MaxSurveyActionResult maxSurveyAction, MaxSurveyRuleSetResponse maxSurvey)
        {
            maxSurveyAction.MaxSurveyRuleSetResponseId = maxSurvey.Id;
        }
    }
}
