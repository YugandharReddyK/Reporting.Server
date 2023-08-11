using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;

namespace Sperry.MxS.Core.Common.Models
{
    public static class BGSDataPointExtension
    {
        public static void SetPositionValues(this BGSDataPoint bgsDataPoint,double? latitude, double? longitude)
        {
            if (latitude.HasValue)
            {
                bgsDataPoint.Latitude = latitude.Value;
            }
            if (longitude.HasValue)
            {
                bgsDataPoint.Longitude = longitude.Value;
            }
        }

        public static bool Equals(this CorrectedSurvey survey,BGSDataPoint bgsDataPoint)
        {
            return survey.TVDss.CompareDouble(bgsDataPoint.TVDss) && survey.Latitude.CompareDouble(bgsDataPoint.Latitude) && survey.Longitude.CompareDouble(bgsDataPoint.Longitude);
        }
        public static void UpdateBgsDataPoint(this BGSDataPoint bgsDataPoint)
        {
            bgsDataPoint.TVDss = bgsDataPoint.TVDss;
            bgsDataPoint.Date = bgsDataPoint.Date;
            bgsDataPoint.Latitude = bgsDataPoint.Latitude;
            bgsDataPoint.Longitude = bgsDataPoint.Longitude;
            bgsDataPoint.Inclination = bgsDataPoint.Inclination;
            bgsDataPoint.Declination = bgsDataPoint.Declination;
            bgsDataPoint.TotalIntensity = bgsDataPoint.TotalIntensity;
        }
        public static void AddForeignKey(this BGSDataPoint bgsDataPoint, CorrectedSurvey correctedSurvey)
        {
            bgsDataPoint.CorrectedSurveyId = correctedSurvey.Id;
        }
    }
}
