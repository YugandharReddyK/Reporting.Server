using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class PlanSurvey : DataModelBase, IComparable<PlanSurvey>
    {
        private DateTime _dateTime;
        private Solution _solution;

        public PlanSurvey(RawSurvey rawSurvey) : this()
        {
            Depth = rawSurvey.Depth;
            MWDInclination = rawSurvey.MWDInclination;
            MWDShortCollar = rawSurvey.MWDShortCollar;
        }

        public PlanSurvey()
        {
            NomBt = 0;
            NomDip = 0;
            NomDeclination = 0;
            NomGrid = 0;
            _dateTime = DateTime.Now;
        }

        public PlanSurvey(PlanSurvey planSurveyToCopy) : this()
        {
            _dateTime = planSurveyToCopy.DateTime;
            Depth = planSurveyToCopy.Depth;
            MWDInclination = planSurveyToCopy.MWDInclination;
            MWDShortCollar = planSurveyToCopy.MWDShortCollar;
            NomBt = planSurveyToCopy.NomBt;
            NomDeclination = planSurveyToCopy.NomDeclination;
            NomDip = planSurveyToCopy.NomDip;
            NomGrid = planSurveyToCopy.NomGrid;

        }

        [JsonProperty]
        public DateTime DateTime
        {
            get
            {
                if (_dateTime < MxSConstants.MinDateTime)
                {
                    return MxSConstants.MinDateTime;
                }
                return _dateTime;
            }
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                }
            }
        }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public double? MWDInclination { get; set; }

        [JsonProperty]
        public double? MWDShortCollar { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorAzimuth { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorInclination { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? NomBt { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? NomDeclination { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? NomDip { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? NomGrid { get; set; }

        [JsonProperty]
        public Guid SolutionId { get; set; }

        [JsonProperty]
        public Solution Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                //if (_solution != null)
                //    _solution.PropertyChanged -= _solution_PropertyChanged;
                _solution = value;
                if (value != null)
                {
                    //_solution.PropertyChanged -= _solution_PropertyChanged;
                    //_solution.PropertyChanged += _solution_PropertyChanged;
                    SolutionId = value.Id;
                }
                else
                {
                    SolutionId = Guid.Empty;
                }
            }
        }

        [NotMapped]
        [JsonProperty]
        public double AziErrorBe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorDecle { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorConve { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrordDecle { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorDipe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorAzimuthMagnetic { get; set; }

        [JsonProperty]
        public double? SolInc { get; set; }

        [JsonProperty]
        public double? SolAzm { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        //[IgnoreDataMember]
        public string RunNo
        {
            get
            {
                if (Solution != null && Solution.Run != null)
                {
                    return Solution.Run.RunNumber;
                }
                return null;
            }
        }

        public static implicit operator CorrectedSurvey(PlanSurvey planSurvey)
        {
            //TODO: Suhail - Removed TypeConverter
            //return TypeConverter.DynamicMap<CorrectedSurvey>(planSurvey);
            return planSurvey.MapPlanSurveyToCorrectedSurvey();
        }

        public static implicit operator PlanSurvey(CorrectedSurvey correctedSurvey)
        {
            //TODO: Suhail - Removed TypeConverter
            //return TypeConverter.DynamicMap<PlanSurvey>(correctedSurvey);
            return correctedSurvey.MapCorrectedSurveyToPlanSurvey();
        }

        public int CompareTo(PlanSurvey other)
        {
            if (other.Depth == Depth)
            {
                return other.Depth.CompareTo(Depth);
            }
            return Depth.CompareTo(other.Depth);
        }

        private void _solution_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Id"))
            {
                SolutionId = Solution.Id;
            }
        }
    }
}
