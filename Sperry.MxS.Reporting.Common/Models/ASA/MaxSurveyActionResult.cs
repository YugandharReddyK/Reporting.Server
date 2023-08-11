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
    public class MaxSurveyActionResult : DataModelBase
    {
        public MaxSurveyActionResult()
        { }

        public MaxSurveyActionResult(MaxSurveyActionResult maxSurveyActionResult) : this()
        {
            Deleted = maxSurveyActionResult.Deleted;
            MaxSurveyRuleSetResponse = maxSurveyActionResult.MaxSurveyRuleSetResponse;
            ActionName = maxSurveyActionResult.ActionName;
            Result = maxSurveyActionResult.Result;
            Message = maxSurveyActionResult.Message;
        }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public Guid MaxSurveyRuleSetResponseId { get; set; }

        [JsonProperty]
        public MaxSurveyRuleSetResponse MaxSurveyRuleSetResponse { get; set; }

        [JsonProperty]
        public string ActionName { get; set; } = "";

        [JsonProperty]
        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;

        [JsonProperty]
        public string Message { get; set; } = "";  
    }
}
