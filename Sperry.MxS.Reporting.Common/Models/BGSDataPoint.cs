using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BGSDataPoint : DataModelBase, IEquatable<BGSDataPoint>
    {
        public BGSDataPoint()
        {
            Id = Guid.NewGuid();
        }

        public BGSDataPoint(CorrectedSurvey survey, DateTime dateTime)
            : this()
        {
            TVDss = survey.TVDss.HasValue ? survey.TVDss.Value : double.MinValue;
            Date = dateTime;
            Latitude = survey.Latitude.HasValue ? survey.Latitude.Value : double.MinValue;
            Longitude = survey.Longitude.HasValue ? survey.Longitude.Value : double.MinValue;
            CorrectedSurvey = survey;
            CorrectedSurveyId = survey.Id;
        }

        public BGSDataPoint(CorrectedSurvey survey)
            : this(survey, survey.DateTime)
        { }

        public BGSDataPoint(Fieldpoint fieldpoint) : this()
        {
            TVDss = fieldpoint.TVDss;
            Date = fieldpoint.Date;
            Latitude = fieldpoint.Latitude;
            Longitude = fieldpoint.Longitude;
            Inclination = fieldpoint.Inclination;
            Declination = fieldpoint.Declination;
            TotalIntensity = fieldpoint.TotalIntensity;
        }

        public BGSDataPoint(BGSDataPoint bgsDataPoint) :
             this()
        {
            TVDss = bgsDataPoint.TVDss;
            Date = bgsDataPoint.Date;
            Latitude = bgsDataPoint.Latitude;
            Longitude = bgsDataPoint.Longitude;
            Inclination = bgsDataPoint.Inclination;
            Declination = bgsDataPoint.Declination;
            TotalIntensity = bgsDataPoint.TotalIntensity;
            CorrectedSurvey = bgsDataPoint.CorrectedSurvey;
            CorrectedSurveyId = bgsDataPoint.CorrectedSurveyId;
        }

        [JsonProperty]
        public Guid CorrectedSurveyId { get; set; }

        [JsonProperty]
        public CorrectedSurvey CorrectedSurvey { get; set; }

        [NotMapped]
        [JsonProperty]
        public double TVDss { get; set; }

        [NotMapped]
        [JsonProperty]
        public DateTime Date { get; set; }

        [NotMapped]
        [JsonProperty]
        public double Latitude { get; set; }

        [NotMapped]
        [JsonProperty]
        public double Longitude { get; set; }

        [JsonProperty]
        public double Declination { get; set; }

        [JsonProperty]
        public double Inclination { get; set; }

        [JsonProperty]
        public double TotalIntensity { get; set; }

        //ToDo Sandeep Becuse of IEquatable<BGSDataPoint> can't move to extension method
        public bool Equals(BGSDataPoint bgsDataPoint)
        {
            return bgsDataPoint != null && (bgsDataPoint.TVDss.CompareDouble(TVDss) && bgsDataPoint.Latitude.CompareDouble(Latitude) && bgsDataPoint.Longitude.CompareDouble(Longitude));
        }
    }
}
