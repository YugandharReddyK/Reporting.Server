using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class TimeOffsetHelper
    {
        public static int DefaultTimeZoneHours = 0;
        #region public methods

        public static DateTime UpdateTimeOffsetAndCalculateAtomicTime(CorrectedSurvey correctedSurvey)
        {
            DateTime time = correctedSurvey.DateTime;
            bool shouldApplyManualOFfset = ShouldApplyManualTimeOffset(correctedSurvey);
            CorrectRigTimeOffset(correctedSurvey, shouldApplyManualOFfset);
            return DoAtomicTimeCalculation(correctedSurvey);
        }
        public static bool ShouldApplyManualTimeOffset(CorrectedSurvey survey)
        {
            bool result = false;
            if (survey.Solution != null)
            {
                if (survey.Solution.ForceManualTimeOffset)
                {
                    result = true;
                }
                else
                {
                    if (survey.Solution.RigTimeOffset != null)
                    {
                        result = !IsAutoTimeOffsetExists(survey);
                    }
                }

            }
            return result;
        }
        public static bool IsAtomicCalculatePossible(this CorrectedSurvey survey)
        {
            bool result = false;
            if (ShouldApplyManualTimeOffset(survey))
            {
                result = IsManualTimeOffsetExists(survey);
            }
            else
            {
                result = IsAutoTimeOffsetExists(survey);
            }

            return result;
        }

        #endregion public methods
        #region private methods

        private static DateTime DoAtomicTimeCalculation(CorrectedSurvey correctedSurvey)
        {
            DateTime time = correctedSurvey.DateTime;
            if (correctedSurvey.RigTimeOffset != null)
            {
                TimeSpan timeOffset = TimeSpan.FromTicks(correctedSurvey.RigTimeOffset.Value);
                if (correctedSurvey.DateTime - DateTime.MinValue > -timeOffset)
                    time = correctedSurvey.DateTime + timeOffset;
            }
            return time;
        }

        private static void CorrectRigTimeOffset(CorrectedSurvey correctedSurvey, bool shouldAppplyManualOfset)
        {
            if (shouldAppplyManualOfset)
            {
                if (correctedSurvey.Solution != null)
                {
                    correctedSurvey.RigTimeOffset = correctedSurvey.Solution.RigTimeOffset.Value;
                    correctedSurvey.ManualTimeOffsetFlag = true;
                }
            }
            else
            {
                correctedSurvey.RigTimeOffset = correctedSurvey.RawSurvey.RigTimeOffset;
                correctedSurvey.ManualTimeOffsetFlag = false;
            }
        }

        private static bool IsAutoTimeOffsetExists(CorrectedSurvey survey)
        {
            if (survey.RawSurvey != null)
                return survey.RawSurvey.RigTimeOffset != null;
            return false;

        }

        private static bool IsManualTimeOffsetExists(CorrectedSurvey survey)
        {
            bool result = false;
            if (survey.Solution.RigTimeOffset == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        #endregion private methods
    }
}
