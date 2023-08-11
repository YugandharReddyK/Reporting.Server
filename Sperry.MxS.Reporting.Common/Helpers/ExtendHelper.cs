using Sperry.MxS.Core.Common.Models.Workbench;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Models.MFM;
using Sperry.MxS.Core.Infrastructure.Helper;

namespace Sperry.MxS.Core.Infrastructure.Helper
{
    public static class ExtendHelper
    {
        #region Survey

        public static void Degree2Radian(this IList<Survey> surveys)
        {
            if (CheckSurveysIsEmpty(surveys)) return;
            var factor = GetFactor();
            surveys.ToList().ForEach(item => SurveyAngle(item, factor));
        }

        public static Survey Degree2Radian(this Survey survey)
        {
            if (!CheckSurveyIsNull(survey))
            {
                SurveyAngle(survey, GetFactor());
            }
            return survey;
        }

        public static void Radian2Degree(this IList<Survey> surveys)
        {
            if (CheckSurveysIsEmpty(surveys)) return;
            var factor = GetFactor(false);
            surveys.ToList().ForEach(item => SurveyAngle(item, factor));
        }

        public static Survey Radian2Degree(this Survey survey)
        {
            if (!CheckSurveyIsNull(survey))
            {
                SurveyAngle(survey, GetFactor(false));
            }
            return survey;
        }

        private static void SurveyAngle(Survey survey, double factor)
        {
            survey.MWDLongCollar *= factor;
            survey.MWDShortCollar *= factor;
            survey.IcaInc *= factor;
            survey.MWDInclination *= factor;
            survey.GridConvergence *= factor;
            survey.Dip *= factor;
            survey.CazDip *= factor;
            survey.IFRDip *= factor;
            survey.IIFRDip *= factor;
            survey.MFMDip *= factor;
            survey.Declination *= factor;
            survey.CazDec *= factor;
            survey.IFRDec *= factor;
            survey.IIFRDec *= factor;
            survey.MFMDec *= factor;
            survey.CazAzm *= factor;
            survey.IFRLCAzm *= factor;
            survey.IFRSCAzm *= factor;
            survey.MFMLcAzm *= factor;
            survey.MFMScAzm *= factor;

            survey.IcaHsd *= factor;
            survey.CazHsd *= factor;

            survey.GxyzInclination *= factor;
            survey.IcaGxyzInclination *= factor;
        }


        public static CorrectedSurveyValues Radian2Degree(this CorrectedSurveyValues survey)
        {
            if (survey != null)
            {
                SurveyAngle(survey, GetFactor(false));
            }
            return survey;
        }

        private static void SurveyAngle(CorrectedSurveyValues survey, double factor)
        {
            survey.GxyzInclination *= factor;
            survey.Dip *= factor;
            survey.Azimuth *= factor;
        }

        private static bool CheckSurveysIsEmpty(IEnumerable<Survey> surveys)
        {
            return CheckIEnumerableIsEmpty(surveys);
        }

        private static bool CheckSurveyIsNull(Survey survey)
        {
            return survey == null;
        }

        #endregion

        #region SandboxCalculationResult

        public static void Radian2Degree(this WorkBenchCalculationResult item)
        {
            if (item == null) return;
            item.CazandraCentreData = item.CazandraCentreData.Radian2Degree();
            item.CazandraDiyData = item.CazandraDiyData.Radian2Degree();
            item.CazandraRawData = item.CazandraRawData.Radian2Degree();
            item.CazandraSfeData = item.CazandraSfeData.Radian2Degree();
            item.CazandraTfcData = item.CazandraTfcData.Radian2Degree();
            item.CazandraTxyData = item.CazandraTxyData.Radian2Degree();

            item.CazandraRawAveData = item.CazandraRawAveData.Radian2Degree();
            item.CazandraRawStdData = item.CazandraRawStdData.Radian2Degree();
            item.CazandraTfcAveData = item.CazandraTfcAveData.Radian2Degree();
            item.CazandraTfcStdData = item.CazandraTfcStdData.Radian2Degree();
            item.CazandraTxyAveData = item.CazandraTxyAveData.Radian2Degree();
            item.CazandraTxyStdData = item.CazandraTxyStdData.Radian2Degree();
            item.CazandraSfeAveData = item.CazandraSfeAveData.Radian2Degree();
            item.CazandraSfeStdData = item.CazandraSfeStdData.Radian2Degree();
            item.CazandraDiyAveData = item.CazandraDiyAveData.Radian2Degree();
            item.CazandraDiyStdData = item.CazandraDiyStdData.Radian2Degree();

            item.IcarusCentreData = item.IcarusCentreData.Radian2Degree();
            item.IcarusDiyData = item.IcarusDiyData.Radian2Degree();
            item.IcarusRawData = item.IcarusRawData.Radian2Degree();
            item.IcarusSfeData = item.IcarusSfeData.Radian2Degree();
            item.IcarusTfcData = item.IcarusTfcData.Radian2Degree();
            item.IcarusTxyData = item.IcarusTxyData.Radian2Degree();
        }

        private static IList<T> Radian2Degree<T>(this IList<T> items) where T : class
        {
            if (!CheckIEnumerableIsEmpty(items))
            {
                var factor = GetFactor(false);
                items.ToList().ForEach(item => item.Radian2Degree(factor));
            }
            return items;
        }

        private static T Radian2Degree<T>(this T item, double factor = double.NaN) where T : class
        {
            if (item == null)
                return item;
            if (double.IsNaN(factor))
                factor = GetFactor(false);
            SandboxCalculationResultAngle(item, factor);
            return item;
        }

        public static readonly dynamic _helper = new SandboxCalculationResultHelper();
        public static void SandboxCalculationResultAngle<T>(T item, double factor)
        {
            _helper.SandboxCalculationResultAngle(item, factor);
        }


        #endregion

        #region common&other

        public static void Radian2Degree(this MFMInputParameter item)
        {
            MfmInputParameterAngle(item, false);
        }

        public static void Degree2Radian(this MFMInputParameter item)
        {
            MfmInputParameterAngle(item);
        }

        private static void MfmInputParameterAngle(MFMInputParameter item, bool isDegree2Radian = true)
        {
            if (item == null) return;
            var factor = GetFactor(isDegree2Radian);
            item.Declination *= factor;
            item.Dip *= factor;
            item.Inclination *= factor;
            item.ReferenceInclination *= factor;
            item.TotalCorrection *= factor;
            item.Survey = isDegree2Radian ? item.Survey.Degree2Radian() : item.Survey.Radian2Degree();
            item.ReferenceSurvey = isDegree2Radian ? item.ReferenceSurvey.Degree2Radian() : item.ReferenceSurvey.Radian2Degree();
        }

        public static double Radian2Degree(this double item)
        {
            return item * GetFactor(false);
        }

        public static double Degree2Radian(this double item)
        {
            return item * GetFactor(true);
        }

        private static bool CheckIEnumerableIsEmpty<T>(IEnumerable<T> items)
        {
            if (items == null || !items.Any()) return true;
            return false;
        }

        private static double GetFactor(bool isDegree2Radian = true)
        {
            return isDegree2Radian ? Math.PI / 180.0 : 180.0 / Math.PI;
        }




        #endregion
    }
}
