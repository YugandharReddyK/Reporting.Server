using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.ASA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ModelsExtensions.RulesEngineExtensions
{
    public static class MaxSurveyPreconditionRuleExtension
    {
        public static void AddForeignKey(this MaxSurveyPreConditionRule maxSurveyPreConditionRule,Solution solution)
        {
            maxSurveyPreConditionRule.SolutionId = solution.Id;
        }
    }
}
