using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Enums;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Survey
    {
        public Survey()
        {
            IsIIFRValid = true;
            CazExcludedStns = string.Empty;
            CazIgnoredStns = string.Empty;
            CazIncludedStns = string.Empty;
        }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public DateTime SurveyRecorded { get; set; }

        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public Guid RunId { get; set; }

        [JsonProperty]
        public DateTime DateTime { get; set; }

        [JsonProperty]
        public bool Enabled { get; set; }

        [JsonProperty]
        public double Gx { get; set; }

        [JsonProperty]
        public double Gy { get; set; }

        [JsonProperty]
        public double Gz { get; set; }

        [JsonProperty]
        public double Bx { get; set; }

        [JsonProperty]
        public double By { get; set; }

        [JsonProperty]
        public double Bz { get; set; }

        [JsonProperty]
        public double GxyzInclination { get; set; }

        [JsonProperty]
        public double MWDInclination { get; set; }

        [JsonProperty]
        public double ReferenceInclination { get; set; }

        [JsonProperty]
        public double MWDShortCollar { get; set; }

        [JsonProperty]
        public double MWDLongCollar { get; set; }

        [JsonProperty]
        public MxSAzimuthTypeEnum AzimuthType { get; set; }

        [JsonProperty]
        public double Temperature { get; set; }

        [JsonProperty]
        public bool? PumpsOn { get; set; }

        [JsonProperty]
        public string Source { get; set; }

        [JsonProperty]
        public string Run { get; set; }

        [JsonProperty]
        public string Status { get; set; }  // Enum ? Xiuchun YI 2012-12-24

        [JsonProperty]
        public double Btotal { get; set; }

        [JsonProperty]
        public double Dip { get; set; }

        [JsonProperty]
        public bool IsIIFRValid { get; set; }

        [JsonProperty]
        public double IIFRDip { get; set; }

        [JsonProperty]
        public double IIFRBt { get; set; }

        [JsonProperty]
        public double IIFRDec { get; set; }

        [JsonProperty]
        public double IFRDip { get; set; }

        [JsonProperty]
        public double IFRDec { get; set; }

        [JsonProperty]
        public double IFRBt { get; set; }

        [JsonProperty]
        public double IFRLCAzm { get; set; }

        [JsonProperty]
        public double IFRSCAzm { get; set; }

        [JsonProperty]
        public double IFRBz { get; set; }

        [JsonProperty]
        public double MFMLcAzm { get; set; }

        [JsonProperty]
        public double MFMScAzm { get; set; }

        [JsonProperty]
        public double MFMBz { get; set; }

        [JsonProperty]
        public double MFMDip { get; set; }

        [JsonProperty]
        public double MFMDec { get; set; }

        [JsonProperty]
        public double MFMBt { get; set; }

        [JsonProperty]
        public bool RunIcarus { get; set; }

        // The Following values were added by Hank, see presentation titled "Calculations Interface" for info
        // ****** THIS IS CURRENTLY AN INCOMPLETE LIST, AND NEEDS DOUBLE CHECKING ******
        // Properties were migrated from SurveyForCazandra, and SurveyForIcarus and were marked by a comment in those classes as they were moved.
        // -Hank 20131226
        #region Stations

        [JsonProperty]
        public int CalcStations { get; set; }

        [JsonProperty]
        public string ExcludedStations { get; set; }

        [JsonProperty]
        public string IgnoredStations { get; set; }

        [JsonProperty]
        public string IncludedStations { get; set; }

        #endregion

        #region DIY

        [JsonProperty]
        public double DIYDeltaGx { get; set; }

        [JsonProperty]
        public double DIYDeltaGy { get; set; }

        [JsonProperty]
        public double DIYDeltaGz { get; set; }

        [JsonProperty]
        public double DIYsGx { get; set; }

        [JsonProperty]
        public double DIYsGy { get; set; }

        [JsonProperty]
        public double DIYDeltaBx { get; set; }

        [JsonProperty]
        public double DIYDeltaBy { get; set; }

        [JsonProperty]
        public double DIYDeltaBz { get; set; }

        [JsonProperty]
        public double DIYsBx { get; set; }

        [JsonProperty]
        public double DIYsBy { get; set; }

        [JsonProperty]
        public double DIYBzMisAng { get; set; }

        [JsonProperty]
        public double DIYBzMisDir { get; set; }

        [JsonProperty]
        public double DIYGzMisAng { get; set; }

        [JsonProperty]
        public double DIYGzMisDir { get; set; }

        #endregion

        #region Cazandra

        [JsonProperty]
        public int CazCalcStns { get; set; }

        [JsonProperty]
        public double CazDIYdBx { get; set; }

        [JsonProperty]
        public double CazDIYdBy { get; set; }

        [JsonProperty]
        public double CazDIYdBz { get; set; }

        [JsonProperty]
        public double CazDIYsBx { get; set; }

        [JsonProperty]
        public double CazDIYsBy { get; set; }

        [JsonProperty]
        public double CazDIYBzMisAng { get; set; }

        [JsonProperty]
        public double CazDIYBzMisDir { get; set; }

        [JsonProperty]
        public bool CazIFRData { get; set; }

        [JsonProperty]
        public bool CazDoBzMisCalcs { get; set; }

        [JsonProperty]
        public bool CazApplyBGMisCalcs { get; set; }

        [JsonProperty]
        public double CazBGMis { get; set; }

        [JsonProperty]
        public double CazBzOffset { get; set; }

        [JsonProperty]
        public MxSQuadrant CazRawQuad { get; set; }

        [JsonProperty]
        public MxSQuadrant CazTfcQuad { get; set; }

        [JsonProperty]
        public MxSQuadrant CazSfeQuad { get; set; }

        [JsonProperty]
        public MxSQuadrant CazTxyQuad { get; set; }

        [JsonProperty]
        public MxSQuadrant CazDiyQuad { get; set; }

        [JsonProperty]
        public MxSNorthReference NorthRef { get; set; }

        [JsonProperty]
        public double GridConvergence { get; set; }

        [JsonProperty]
        public double Declination { get; set; }

        [JsonProperty]
        public string CazExcludedStns { get; set; }

        [JsonProperty]
        public string CazIgnoredStns { get; set; }

        [JsonProperty]
        public string CazIncludedStns { get; set; }

        [JsonProperty]
        public MxSCazandraMagneticValsType CazMagValsType { get; set; }

        [JsonProperty]
        public bool CazApplyIcarus { get; set; }

        [JsonProperty]
        public MxSCazandraSolution CazSolution { get; set; }  // Can this be an enum?  -Hank 20131213

        [JsonProperty]
        public double CazDec { get; set; }

        [JsonProperty]
        public double CazBt { get; set; }

        [JsonProperty]
        public double CazDip { get; set; }

        [JsonProperty]
        public double CazHsd { get; set; }

        [JsonProperty]
        public double CazAzm { get; set; }

        [JsonProperty]
        public double CazBTotalDip { get; set; }

        [JsonProperty]
        public double CazBx { get; set; }
        [JsonProperty]
        public double CazBy { get; set; }
        [JsonProperty]
        public double CazBz { get; set; }
        [JsonProperty]
        public double CazBv { get; set; }
        [JsonProperty]
        public double CazBh { get; set; }
        [JsonProperty]
        public double CazBoxy { get; set; }

        [JsonProperty]
        public double CazTVD { get; set; }
        [JsonProperty]
        public double CazNSDeparture { get; set; }
        [JsonProperty]
        public double CazEWDeparture { get; set; }

        #endregion

        #region Icarus

        [JsonProperty]
        public string IcaVersion { get; set; }

        [JsonProperty]
        public MxSIcarusSolution IcaSolution { get; set; }

        [JsonProperty]
        public double IcaGravity { get; set; }

        [JsonProperty]
        public bool IcaDoGzMisCalcs { get; set; }

        [JsonProperty]
        public int IcaCalcStns { get; set; }

        [JsonProperty]
        public double IcaDIYdGx { get; set; }

        [JsonProperty]
        public double IcaDIYdGy { get; set; }

        [JsonProperty]
        public double IcaDIYdGz { get; set; }

        [JsonProperty]
        public double IcaDIYsGx { get; set; }

        [JsonProperty]
        public double IcaDIYsGy { get; set; }

        [JsonProperty]
        public double IcaDIYGzMisAng { get; set; }

        [JsonProperty]
        public double IcaDIYGzMisDir { get; set; }

        [JsonProperty]
        public string IcaExcludedStns { get; set; }

        [JsonProperty]
        public string IcaIgnoredStns { get; set; }

        [JsonProperty]
        public string IcaIncludedStns { get; set; }

        [JsonProperty]
        public double IcaGx { get; set; }

        [JsonProperty]
        public double IcaGy { get; set; }

        [JsonProperty]
        public double IcaGz { get; set; }

        [JsonProperty]
        public double IcaHsd { get; set; }

        [JsonProperty]
        public double IcaInc { get; set; }

        [JsonProperty]
        public double IcaGTotal { get; set; }

        [JsonProperty]
        public double IcaGoxy { get; set; }

        [JsonProperty]
        public double IcaGxyzInclination { get; set; }

        [JsonProperty]
        public bool UseIcarusGz { get; set; }

        #endregion

        #region AziError

        [JsonProperty]
        public double AziErrorDipe { get; set; }

        [JsonProperty]
        public double AziErrorBe { get; set; }

        [JsonProperty]
        public double AziErrordDipe { get; set; }

        [JsonProperty]
        public double AziErrordBe { get; set; }

        [JsonProperty]
        public double AziErrordDecle { get; set; }

        [JsonProperty]
        public double AziErrordBz { get; set; }

        [JsonProperty]
        public double AziErrorSxy { get; set; }

        [JsonProperty]
        public double AziErrorBGm { get; set; }

        [JsonProperty]
        public double AziErrorAzimuth { get; set; }

        [JsonProperty]
        public double AziErrorAzimuthMagnetic { get; set; }

        [JsonProperty]
        public double AziErrorInclination { get; set; }

        #endregion AziError

        [JsonProperty]
        public double Azimuth
        {

            get
            {
                if (AzimuthType == MxSAzimuthTypeEnum.LongCollar)
                    return MWDLongCollar;
                return MWDShortCollar;
            }
        }
    }
}
