using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.ModelsExtensions.SurveysExtensions
{
    public static class UncertainitiesExtension
    {
        public static void AddForeignKey(this Uncertainty uncertaintyValue,CorrectedSurvey survey)
        {
            uncertaintyValue.CorrectedSurveyId = survey.Id;
        }
    }
}
