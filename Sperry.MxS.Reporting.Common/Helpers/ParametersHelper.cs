using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Infrastructure.Helper
{
    public class ParametersHelper
    {
        public static void CheckSurveys(IList<Survey> referenceSurveys, Survey correctedSurvey)
        {
            CheckSurveysIsNotNull(referenceSurveys, correctedSurvey);
            //CheckRefSurveysCountRequired(referenceSurveys);
        }

        public static void Degree2Radian(IList<Survey> refSurveys, Survey correctedSurvey)
        {
            refSurveys.Degree2Radian();
            if (!refSurveys.Contains(correctedSurvey))
                correctedSurvey.Degree2Radian();
        }

        public static void Radian2Degree(IList<Survey> refSurveys, Survey correctedSurvey)
        {
            refSurveys.Radian2Degree();
            if (!refSurveys.Contains(correctedSurvey))
                correctedSurvey.Radian2Degree();
        }

        public static void Radian2Degree(Survey correctedSurvey)
        {
            correctedSurvey.Radian2Degree();
        }

        public static void Radian2Degree(IList<CorrectedSurveyValues> values)
        {
            if (values == null)
                return;
            foreach (var value in values)
            {
                value.Radian2Degree();
            }

        }

        private static void CheckRefSurveysCountRequired(IList<Survey> refSurveys)
        {
            //todo define a new exception type.
            if (refSurveys.Count <= 2)
            {
                throw new InvalidDataException("The count of surveys should be greater than two.");
            }
        }

        private static void CheckSurveysIsNotNull(IList<Survey> referenceSurveys, Survey correctedSurvey)
        {
            CheckItemIsNotNull(referenceSurveys, "referenceSurveys");
            CheckItemIsNotNull(correctedSurvey, "correctedSurvey");
        }

        public static void CheckItemIsNotNull(object item, string name = null)
        {
            if (item == null)
            {
                throw string.IsNullOrEmpty(name) ? new ArgumentNullException() : new ArgumentNullException(name);
            }
        }
    }
}
