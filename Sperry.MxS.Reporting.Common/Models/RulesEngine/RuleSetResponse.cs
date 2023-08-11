using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    [Serializable]
    public class RuleSetResponse
    {
        public List<RuleResponse> RulesResponse { get; set; } = new List<RuleResponse>();

        public List<ActionResult> ActionResults { get; set; } = new List<ActionResult>();

        public string Message { get; set; } = string.Empty;

        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;

    }
}
