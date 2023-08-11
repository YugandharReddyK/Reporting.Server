using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.RulesEngine
{
    [Serializable]
    public class ActionResult
    {
        public string ActionName { get; set; } = string.Empty;
 
        public MxSRuleResultEnum Result { get; set; } = MxSRuleResultEnum.NotExecuted;

        public string Message { get; set; } = string.Empty;

    }

}
