using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MaxSurveyRuleSetResponse : DataModelBase
    {
        public MaxSurveyRuleSetResponse()
        { }

        public MaxSurveyRuleSetResponse(MaxSurveyRuleSetResponse maxSurveyRuleSetResponseToCopy) : this()
        {
            CorrectedSurvey = maxSurveyRuleSetResponseToCopy.CorrectedSurvey;

            foreach (MaxSurveyRuleResponse ruleResponse in maxSurveyRuleSetResponseToCopy.RulesResponse)
            {
                var ruleResponseToCopy = new MaxSurveyRuleResponse(ruleResponse);
                ruleResponseToCopy.MaxSurveyRuleSetResponse = this;
                RulesResponse.Add(ruleResponseToCopy);
            }

            foreach (MaxSurveyActionResult actionResult in maxSurveyRuleSetResponseToCopy.ActionResults)
            {
                var maxSurveyActionResultToCpoy = new MaxSurveyActionResult(actionResult);
                maxSurveyActionResultToCpoy.MaxSurveyRuleSetResponse = this;
                ActionResults.Add(maxSurveyActionResultToCpoy);
            }


            Message = maxSurveyRuleSetResponseToCopy.Message;
            Result = maxSurveyRuleSetResponseToCopy.Result;
        }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public Guid CorrectedSurveyId { get; set; }

        [JsonProperty]
        public virtual CorrectedSurvey CorrectedSurvey { get; set; }

        [JsonProperty]
        public virtual List<MaxSurveyRuleResponse> RulesResponse { get; set; } = new List<MaxSurveyRuleResponse>();

        [JsonProperty]
        public virtual List<MaxSurveyActionResult> ActionResults { get; set; } = new List<MaxSurveyActionResult>();

        [JsonProperty]
        public string Message { get; set; } = "";

        [JsonProperty]
        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;
    }
}
