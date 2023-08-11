using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MaxSurveyRuleResponse : DataModelBase
    {
        public MaxSurveyRuleResponse()
        { }

        public MaxSurveyRuleResponse(MaxSurveyRuleResponse maxSurveyRuleResponse)
            : this()
        {
            Deleted = maxSurveyRuleResponse.Deleted;
            MaxSurveyRuleSetResponse = maxSurveyRuleResponse.MaxSurveyRuleSetResponse;
            RuleName = maxSurveyRuleResponse.RuleName;
            RuleExpression = maxSurveyRuleResponse.RuleExpression;
            EvaluatedRuleExpression = maxSurveyRuleResponse.EvaluatedRuleExpression;
            RuleResultObject = maxSurveyRuleResponse.RuleResultObject;
            Result = maxSurveyRuleResponse.Result;
            Message = maxSurveyRuleResponse.Message;
            Unit = maxSurveyRuleResponse.Unit;
            PreConditionExpression = maxSurveyRuleResponse.PreConditionExpression;
            PreConditionResult = maxSurveyRuleResponse.PreConditionResult;
            PreConditionUnit = maxSurveyRuleResponse.PreConditionUnit;
            EvaluatedPreConditionExpression = maxSurveyRuleResponse.EvaluatedPreConditionExpression;
        }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public Guid MaxSurveyRuleSetResponseId { get; set; }

        [JsonProperty]
        public virtual MaxSurveyRuleSetResponse MaxSurveyRuleSetResponse { get; set; }

        [JsonProperty]
        public string RuleName { get; set; } = string.Empty;

        [JsonProperty]
        public string RuleExpression { get; set; } = string.Empty;

        [JsonProperty]
        public string EvaluatedRuleExpression { get; set; } = string.Empty;

        [JsonProperty]
        public object RuleResultObject { get; set; } = null;

        [JsonProperty]
        public string PreConditionExpression { get; set; } = string.Empty;

        [JsonProperty]
        public string EvaluatedPreConditionExpression { get; set; } = string.Empty;

        [JsonProperty]
        public bool PreConditionResult { get; set; } = false;

        [JsonProperty]
        public string PreConditionUnit { get; set; } = string.Empty;

        [JsonProperty]
        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;

        [JsonProperty]
        public string Message { get; set; } = string.Empty;

        [JsonProperty]
        public string Unit { get; set; } = string.Empty;
       
    }
}
