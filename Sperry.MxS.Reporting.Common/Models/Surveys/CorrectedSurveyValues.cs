using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CorrectedSurveyValues : DataModelBase, IMxSDataAvailable
    {
        private CorrectedSurvey _correctedSurvey;

        public CorrectedSurveyValues()
        { }

        public CorrectedSurveyValues(CorrectedSurveyValues correctedSurveyValues)
        {
            Gx = correctedSurveyValues.Gx;
            Gy = correctedSurveyValues.Gy;
            Gz = correctedSurveyValues.Gz;
            Bx = correctedSurveyValues.Bx;
            By = correctedSurveyValues.By;
            Bz = correctedSurveyValues.Bz;
            GTotal = correctedSurveyValues.GTotal;
            GxyzInclination = correctedSurveyValues.GxyzInclination;
            Goxy = correctedSurveyValues.Goxy;
            Boxy = correctedSurveyValues.Boxy;
            BTotal = correctedSurveyValues.BTotal;
            BTotalDip = correctedSurveyValues.BTotalDip;
            BTotalCalc = correctedSurveyValues.BTotalCalc;
            BTotalBzCalc = correctedSurveyValues.BTotalBzCalc;
            BTotalDipBzCalc = correctedSurveyValues.BTotalDipBzCalc;
            BTotalDipBzMeasured = correctedSurveyValues.BTotalDipBzMeasured;
            Bv = correctedSurveyValues.Bv;
            BvBzCalc = correctedSurveyValues.BvBzCalc;
            BvCalc = correctedSurveyValues.BvCalc;
            Bh = correctedSurveyValues.Bh;
            BhCalc = correctedSurveyValues.BhCalc;
            BhBzCalc = correctedSurveyValues.BhBzCalc;
            BzCalc = correctedSurveyValues.BzCalc;
            Dip = correctedSurveyValues.Dip;
            DipCalc = correctedSurveyValues.DipCalc;
            DipBzCalc = correctedSurveyValues.DipBzCalc;
            Azimuth = correctedSurveyValues.Azimuth;
            LongCollarAzimuth = correctedSurveyValues.LongCollarAzimuth;
            ShortCollarAzimuth = correctedSurveyValues.ShortCollarAzimuth;
            NSDeparture = correctedSurveyValues.NSDeparture;
            EWDeparture = correctedSurveyValues.EWDeparture;
            TVD = correctedSurveyValues.TVD;
            HighSide = correctedSurveyValues.HighSide;
            Declination = correctedSurveyValues.Declination;
            Inclination = correctedSurveyValues.Inclination;
            GridConvergence = correctedSurveyValues.GridConvergence;
            CalculationType = correctedSurveyValues.CalculationType;
            Expired = correctedSurveyValues.Expired;
            CorrectedSurvey = correctedSurveyValues.CorrectedSurvey;

        }

        [JsonProperty]
        public MxSCalculationType CalculationType { get; set; }

        // TODO: Modified by Naveen Kumar
        [JsonProperty]
        public CorrectedSurvey CorrectedSurvey { get; set; }
        //public CorrectedSurvey CorrectedSurvey
        //{
        //    get
        //    {
        //        return _correctedSurvey;
        //    }
        //    set
        //    {
        //        //if (_correctedSurvey != null)
        //        //    _correctedSurvey.PropertyChanged -= _correctedSurvey_PropertyChanged;
        //        _correctedSurvey = value;
        //        if (value != null)
        //        {
        //            //_correctedSurvey.PropertyChanged -= _correctedSurvey_PropertyChanged;
        //            //_correctedSurvey.PropertyChanged += _correctedSurvey_PropertyChanged;
        //            CorrectedSurveyId = value.Id;
        //        }
        //        else
        //        {
        //            CorrectedSurveyId = Guid.Empty;
        //        }
        //    }
        //}

        [JsonProperty]
        public Guid CorrectedSurveyId { get; set; }

        [NotMapped]
        [JsonProperty]
        public DateTime DateTime { get; set; }

        [NotMapped]
        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public double? Gx { get; set; }

        [JsonProperty]
        public double? Gy { get; set; }

        [JsonProperty]
        public double? Gz { get; set; }

        [JsonProperty]
        public double? Bx { get; set; }

        [JsonProperty]
        public double? By { get; set; }

        [JsonProperty]
        public double? Bz { get; set; }

        [JsonProperty]
        public double GTotal { get; set; }

        [JsonProperty]
        public double GxyzInclination { get; set; }

        [JsonProperty]
        public double Goxy { get; set; }

        [JsonProperty]
        public double Boxy { get; set; }

        [JsonProperty]
        public double BTotal { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalMFMQCMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalMFMQCMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalIFRQCMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalIFRQCMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalIIFRQCMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalIIFRQCMin { get; set; }

        [JsonProperty]
        public double BTotalDip { get; set; }

        [JsonProperty]
        public double BTotalCalc { get; set; }

        [JsonProperty]
        public double BTotalBzCalc { get; set; }

        [JsonProperty]
        public double BTotalDipBzCalc { get; set; }

        [JsonProperty]
        public double BTotalDipBzMeasured { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AzRSSMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AzRSSMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalNomMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BTotalNomMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipNomMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipNomMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double BtDipNomMax { get; set; }

        [JsonProperty]
        public double Bv { get; set; }

        [JsonProperty]
        public double BvBzCalc { get; set; }

        [JsonProperty]
        public double BvCalc { get; set; }

        [JsonProperty]
        public double Bh { get; set; }

        [JsonProperty]
        public double BhCalc { get; set; }

        [JsonProperty]
        public double BhBzCalc { get; set; }

        [JsonProperty]
        public double BzCalc { get; set; }

        [JsonProperty]
        public double Dip { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipMFMQCMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipMFMQCMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipIFRQCMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipIFRQCMax { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipIIFRQCMin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DipIIFRQCMax { get; set; }

        [JsonProperty]
        public double DipCalc { get; set; }

        [JsonProperty]
        public double DipBzCalc { get; set; }

        [JsonProperty]
        public double Azimuth { get; set; }

        [JsonProperty]
        public double LongCollarAzimuth { get; set; }

        [JsonProperty]
        public double ShortCollarAzimuth { get; set; }

        [JsonProperty]
        public double NSDeparture { get; set; }

        [JsonProperty]
        public double EWDeparture { get; set; }

        [JsonProperty]
        public double TVD { get; set; }

        [JsonProperty]
        public double HighSide { get; set; }

        [JsonProperty]
        public double Declination { get; set; }

        [JsonProperty]
        public double Inclination { get; set; }

        [JsonProperty]
        public double GridConvergence { get; set; }

        [JsonProperty]
        public bool Expired { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        public new void Dispose()
        {
            //if (_correctedSurvey != null)
            //{
            //    _correctedSurvey.PropertyChanged -= _correctedSurvey_PropertyChanged;
            //}
            base.Dispose();
        }

        #region Need To Ask Kiran Sir

        private void _correctedSurvey_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Id"))
            {
                CorrectedSurveyId = CorrectedSurvey.Id;
            }
        }

        #endregion

    }
}
