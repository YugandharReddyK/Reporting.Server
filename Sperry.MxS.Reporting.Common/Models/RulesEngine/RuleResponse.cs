using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    [Serializable]
    public class RuleResponse
    {
        public string RuleName { get; set; } = string.Empty;

        public Guid RuleId { get; set; } = Guid.Empty;

        public string RuleExpression { get; set; } = string.Empty;

        public object RuleResultObject { get; set; } = null;

        public string PreConditionExpression { get; set; } = string.Empty;

        public bool PreConditionResult { get; set; } = false;

        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;

        public string Message { get; set; } = string.Empty;

    }

}
