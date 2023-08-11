using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Interfaces;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CorrectedInsiteSurvey : IMxSInsiteSurvey
    {
        public CorrectedInsiteSurvey()
        {    }

        [JsonProperty]
        public string AzimuthSolution { get; set; }

        [JsonProperty]
        public MxSAzimuthTypeEnum AzimuthType { get; set; }

        [JsonProperty]
        public double? Bx { get; set; }

        [JsonProperty]
        public double? By { get; set; }

        [JsonProperty]
        public double? Bz { get; set; }

        [JsonProperty]
        public string CazandraSolution { get; set; }

        [JsonProperty]
        public int CazUsed { get; set; }

        [JsonProperty]
        public DateTime DateTime { get; set; }

        [JsonProperty]
        public double? Declination { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public double? DistanceToBit { get; set; }

        [JsonProperty]
        public bool Enabled { get; set; }

        [JsonProperty]
        public double? GravityToolFace { get; set; }

        [JsonProperty]
        public double? Gx { get; set; }

        [JsonProperty]
        public double? Gy { get; set; }

        [JsonProperty]
        public double? Gz { get; set; }

        [JsonProperty]
        public string IcarusSolution { get; set; }

        [JsonProperty]
        public int IcaUsed { get; set; }

        [JsonProperty]
        public string Ipm { get; set; }

        [JsonProperty]
        public DateTime LastEditedTime { get; set; }

        [JsonProperty]
        public double? NomGrid { get; set; }

        [JsonProperty]
        public MxSNorthReference? NorthReference { get; set; }

        [JsonProperty]
        public MxSSurveyPumpStatus PumpStatus { get; set; }

        [JsonProperty]
        public string RunNumber { get; set; }

        [JsonProperty]
        public string Service { get; set; }

        [JsonProperty]
        public double? Sigma { get; set; }

        [JsonProperty]
        public MxSSurveyStatus SurveyStatus { get; set; }

        [JsonProperty]
        public MxSSurveyType SurveyType { get; set; }

        [JsonProperty]
        public double? Temperature { get; set; }

        [JsonProperty]
        public double? ToolfaceOffset { get; set; }

        #region Inclination

        [JsonProperty]
        public MxSInclinationSolutionType InclinationSolution { get; set; }

        [JsonProperty]
        public double? SagInclination { get; set; }

        [JsonProperty]
        public double? RawInclination { get; set; }

        #endregion

        #region Magnetics

        [JsonProperty]
        public double? BtMeasured { get; set; }

        [JsonProperty]
        public double? DipMeasured { get; set; }

        [JsonProperty]
        public double? BvMeasured { get; set; }

        [JsonProperty]
        public double? BhMeasured { get; set; }

        #endregion

        #region Solution picks

        [JsonProperty]
        public double? SolAzm { get; set; }

        [JsonProperty]
        public double? SolAzmSc { get; set; }

        [JsonProperty]
        public double? SolAzmLc { get; set; }

        [JsonProperty]
        public double? SolDec { get; set; }

        [JsonProperty]
        public double? SolGridConv { get; set; }

        [JsonProperty]
        public double? SolInc { get; set; }

        [JsonProperty]
        public double? SolBz { get; set; }

        [JsonProperty]
        public double? SolGt { get; set; }

        [JsonProperty]
        public double? SolBt { get; set; }

        [JsonProperty]
        public double? SolDip { get; set; }

        [JsonProperty]
        public double? SolBtDip { get; set; }

        #endregion

        #region Nominal Values

        [JsonProperty]
        public double? NomGt { get; set; }

        [JsonProperty]
        public double? NomBt { get; set; }

        [JsonProperty]
        public double? NomDip { get; set; }

        [JsonProperty]
        public double? NomBtDip { get; set; }

        #endregion

        #region QC

        [JsonProperty]
        public double? GtQcDelta { get; set; }

        [JsonProperty]
        public double? DipQcDelta { get; set; }

        [JsonProperty]
        public double? BtQcDelta { get; set; }

        [JsonProperty]
        public double? GtQcLimit { get; set; }

        [JsonProperty]
        public double? BtQcLimit { get; set; }

        [JsonProperty]
        public double? DipQcLimit { get; set; }

        #endregion
    }
}
