using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Models.Surveys;

namespace Sperry.MxS.Core.Common.Helpers.ASA
{
    public class ASASurveyTypeAction : IMxSEngineAction
    {
        public string Name
        {
            get { return "ASASurveyTypeAction"; }
        }

        public string Description
        {
            get { return "The Action sets the Survey Type Column in Corrected Survey to definitive if rules passed."; }
        }

        public bool RunInSimulation
        {
            get { return false; }
        }

        public bool Execute(RuleSet ruleSet, RuleSetResponse ruleSetResponse, ref List<object> objects)
        {
            foreach (object obj in objects)
            {
                CorrectedSurvey correctedSurvey = obj as CorrectedSurvey;
                if (correctedSurvey != null)
                {
                    //TODO: Suhail - Removed TypeConverter
                    //var rulesetResponse = TypeConverter.DynamicMap<MaxSurveyRuleSetResponse>(ruleSetResponse);
                    MaxSurveyRuleSetResponse rulesetResponse = ruleSetResponse.MapRuleSetToMxSRuleSetResponse();
                    if (rulesetResponse.Result == MxSRuleResultEnum.Pass)
                    {
                        correctedSurvey.SurveyType = MxSSurveyType.Definitive;
                        correctedSurvey.TypeEditedBy = MxSConstant.ASALastEditedBy;
                    }
                    correctedSurvey.ASAResult = rulesetResponse.Result;
                    break;
                }
            }
            return true;
        }
    }

}
