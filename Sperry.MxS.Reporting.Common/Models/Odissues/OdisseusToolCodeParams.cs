using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models.Odisseus;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class OdisseusToolCodeParams : DataModelBase
    {
        private MxSLongScc _longSCC;
        private MxSDepthStation _depthStation;

        #region Constructor
        public OdisseusToolCodeParams()
        { }

        public OdisseusToolCodeParams(OdisseusToolCodesToolCode toolCode)
        {
            ToolCodeName = toolCode.name;
            Enum.TryParse(Convert.ToString(toolCode.Term.FirstOrDefault(k => string.Equals(k?.name, OdisseusToolCodeConstants.LongSCC))?.value), out _longSCC);
            Enum.TryParse(Convert.ToString(toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DepthStation))?.value), out _depthStation);
            DREFr_Fix = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DREFr_Fix)).value;
            DREF_Fix = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DREF_Fix)).value;
            DREFr_Float = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DREFr_Float)).value;
            DREF_Float = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DREF_Float)).value;
            DSF = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DSF)).value;
            DST = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DST)).value;
            DSTB = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DSTB)).value;
            SAG = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.SAG)).value;
            MXY1 = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MXY1)).value;
            MXY2 = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MXY2)).value;
            AB = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AB)).value;
            AS = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AS)).value;
            MB = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MB)).value;
            AMIC = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AMIC)).value;
            AMID = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AMID)).value;
            AMIL = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AMIL)).value;
            AMIB = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AMIB)).value;
            AZ = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AZ)).value;
            AZrandom = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.AZrandom)).value;
            DBH = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DBH)).value;
            DBHrandom = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.DBHrandom)).value;
            MFI = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MFI)).value;
            MFIrandom = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MFIrandom)).value;
            MDI = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MDI)).value;
            MDIrandom = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MDIrandom)).value;
            MM = toolCode.Term.FirstOrDefault(k => k.name.Equals(OdisseusToolCodeConstants.MM)).value;
        }

        #endregion

        #region Properties
        [JsonProperty]
        public OdisseusToolCode OdisseusToolCode { get; set; }

        [JsonProperty]
        public Guid FileId { get; set; }

        [NotMapped]
        [JsonProperty]
        public Well Well { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSUnitSystemEnum DepthUnits { get; set; }

        [NotMapped]
        [JsonProperty]
        public string TieinN { get; set; }

        [NotMapped]
        [JsonProperty]
        public string TieinE { get; set; }

        [NotMapped]
        [JsonProperty]
        public string TieinV { get; set; }


        [NotMapped]
        [JsonProperty]
        public int SurveyLegNo { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSModelType ModelType { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSToolType ToolType { get; set; }

        [JsonProperty]
        public MxSLongScc LongSCC { get; set; } = MxSLongScc.Long;

        [NotMapped]
        [JsonProperty]
        public bool SAGCorrected { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSErrorModel StdIFRIIFR { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSBiasSymmetric BiasSymmetric { get; set; }

        [JsonProperty]
        public MxSDepthStation DepthStation { get; set; } = MxSDepthStation.Station;

        [NotMapped]
        [JsonProperty]
        public bool MagneticMud { get; set; }

        [NotMapped]
        [JsonProperty]
        public double ISCWSA_ERRORS { get; set; }

        [JsonProperty]
        public string ToolCodeName { get; set; } = string.Empty;

        [JsonProperty]
        public double DREFr_Fix { get; set; }

        [JsonProperty]
        public double DREF_Fix { get; set; }

        [JsonProperty]
        public double DREFr_Float { get; set; }

        [JsonProperty]
        public double DREF_Float { get; set; }

        [JsonProperty]
        public double DSF { get; set; }

        [JsonProperty]
        public double DST { get; set; }

        [JsonProperty]
        public double DSTB { get; set; }

        [JsonProperty]
        public double SAG { get; set; }

        [JsonProperty]
        public double MXY1 { get; set; }

        [JsonProperty]
        public double MXY2 { get; set; }

        [JsonProperty]
        public double AB { get; set; }

        [JsonProperty]
        public double AS { get; set; }

        [JsonProperty]
        public double MB { get; set; }

        [JsonProperty]
        public double MS { get; set; }

        [JsonProperty]
        public double AMIC { get; set; }

        [JsonProperty]
        public double AMID { get; set; }

        [JsonProperty]
        public double AMIL { get; set; }

        [JsonProperty]
        public double AMIB { get; set; }

        [JsonProperty]
        public double AZ { get; set; }

        [JsonProperty]
        public double AZrandom { get; set; }

        [JsonProperty]
        public double DBH { get; set; }

        [JsonProperty]
        public double DBHrandom { get; set; }

        [JsonProperty]
        public double MFI { get; set; }

        [JsonProperty]
        public double MFIrandom { get; set; }

        [JsonProperty]
        public double MDI { get; set; }

        [JsonProperty]
        public double MDIrandom { get; set; }

        [JsonProperty]
        public double MM { get; set; }

        [NotMapped]
        [JsonProperty]
        public string ISCWSA_GYRO { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AccXYBias { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AccZBias { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AccSFE { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AccMis { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GravityB { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYrN { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYB1 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYB2 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYGdep1 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYGdep2 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYGdep3 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroXYGdep4 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroZrN { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroZB { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroZGdep1 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroZGdep2 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroSFE { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroMis { get; set; }

        [NotMapped]
        [JsonProperty]
        public string WOLFFdeWARDT_ERRORS { get; set; }

        [NotMapped]
        [JsonProperty]
        public double RelDepth { get; set; }

        [NotMapped]
        [JsonProperty]
        public double Misalign { get; set; }

        [NotMapped]
        [JsonProperty]
        public double TrueInc { get; set; }

        [NotMapped]
        [JsonProperty]
        public double CompassRef { get; set; }

        [NotMapped]
        [JsonProperty]
        public double DStringMag { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroError { get; set; }

        [NotMapped]
        [JsonProperty]
        public string BPTYPEERRORS { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AzimuthRef { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroCompass { get; set; }

        [NotMapped]
        [JsonProperty]
        public double ConeInclination { get; set; }

        [NotMapped]
        [JsonProperty]
        public double ConeAzimuth { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MassUnbalInput { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MassUnbalSpin { get; set; }

        [NotMapped]
        [JsonProperty]
        public double GyroDrift { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsStartDepthValid { get; set; }
        #endregion

    }
}
