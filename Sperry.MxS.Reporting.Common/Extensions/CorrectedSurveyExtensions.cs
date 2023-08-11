using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class CorrectedSurveyExtensions
    {
        public static DateTime GetQueryObservatoryReadingDateTime(this CorrectedSurvey correctedSurvey)
        {
            if (correctedSurvey == null || correctedSurvey.Solution == null || correctedSurvey.Solution.Run == null || correctedSurvey.Solution.Run == null)
                throw new ArgumentNullException();
            DateTime localTime = TimeOffsetHelper.UpdateTimeOffsetAndCalculateAtomicTime(correctedSurvey);
            DateTime utcTime = new DateTime(localTime.Year, localTime.Month, localTime.Day, localTime.Hour, localTime.Minute, localTime.Second, DateTimeKind.Utc);
            return utcTime;
        }
    }
}
