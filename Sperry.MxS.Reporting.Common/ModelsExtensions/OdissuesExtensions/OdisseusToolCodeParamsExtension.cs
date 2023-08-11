using Sperry.MxS.Core.Common.Enums;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    public static class OdisseusToolCodeParamsExtension
    {
        public static void ResetToDefaultForInParam(this OdisseusToolCodeParams odisseusToolCodeParams)
        {
            odisseusToolCodeParams.DREFr_Float = 0;
            odisseusToolCodeParams.DREF_Float = 0;
            odisseusToolCodeParams.DREFr_Fix = 0;
            odisseusToolCodeParams.DREF_Fix = 0;
            odisseusToolCodeParams.DSF = 0;
            odisseusToolCodeParams.DST = 0;
            odisseusToolCodeParams.DSTB = 0;
            odisseusToolCodeParams.SAG = 0;
            odisseusToolCodeParams.MXY1 = 0;
            odisseusToolCodeParams.MXY2 = 0;
            odisseusToolCodeParams.AB = 0;
            odisseusToolCodeParams.AS = 0;
            odisseusToolCodeParams.MB = 0;
            odisseusToolCodeParams.MS = 0;
            odisseusToolCodeParams.AMIC = 0;
            odisseusToolCodeParams.AMID = 0;
            odisseusToolCodeParams.AMIL = 0;
            odisseusToolCodeParams.AMIB = 0;
            odisseusToolCodeParams.AZ = 0;
            odisseusToolCodeParams.AZrandom = 0;
            odisseusToolCodeParams.DBH = 0;
            odisseusToolCodeParams.DBHrandom = 0;
            odisseusToolCodeParams.MFI = 0;
            odisseusToolCodeParams.MFIrandom = 0;
            odisseusToolCodeParams.MDI = 0;
            odisseusToolCodeParams.MDIrandom = 0;
            odisseusToolCodeParams.MM = 0;
            odisseusToolCodeParams.ISCWSA_GYRO = string.Empty;

            // 54 to 72
            odisseusToolCodeParams.AccXYBias = 0;
            odisseusToolCodeParams.AccZBias = 0;
            odisseusToolCodeParams.AccSFE = 0;
            odisseusToolCodeParams.AccMis = 0;
            odisseusToolCodeParams.GravityB = 0;
            odisseusToolCodeParams.GyroXYB1 = 0;
            odisseusToolCodeParams.GyroXYB2 = 0;
            odisseusToolCodeParams.GyroXYGdep1 = 0;
            odisseusToolCodeParams.GyroXYGdep2 = 0;
            odisseusToolCodeParams.GyroXYGdep3 = 0;
            odisseusToolCodeParams.GyroXYGdep4 = 0;
            odisseusToolCodeParams.GyroZrN = 0;
            odisseusToolCodeParams.GyroZB = 0;
            odisseusToolCodeParams.GyroZGdep1 = 0;
            odisseusToolCodeParams.GyroZGdep2 = 0;
            odisseusToolCodeParams.GyroSFE = 0;
            odisseusToolCodeParams.GyroMis = 0;
            odisseusToolCodeParams.WOLFFdeWARDT_ERRORS = string.Empty;

            //73 to 79
            odisseusToolCodeParams.RelDepth = 0;
            odisseusToolCodeParams.Misalign = 0;
            odisseusToolCodeParams.TrueInc = 0;
            odisseusToolCodeParams.CompassRef = 0;
            odisseusToolCodeParams.DStringMag = 0;
            odisseusToolCodeParams.GyroError = 0;
            odisseusToolCodeParams.BPTYPEERRORS = string.Empty;

            //80 to 86
            odisseusToolCodeParams.AzimuthRef = 0;
            odisseusToolCodeParams.GyroCompass = 0;
            odisseusToolCodeParams.ConeInclination = 0;
            odisseusToolCodeParams.ConeAzimuth = 0;
            odisseusToolCodeParams.MassUnbalInput = 0;
            odisseusToolCodeParams.MassUnbalSpin = 0;
            odisseusToolCodeParams.GyroDrift = 0;
        }

        public static void ResetSelectedParamToDefault(this OdisseusToolCodeParams odisseusToolCodeParams)
        {
            odisseusToolCodeParams.LongSCC = MxSLongScc.Long;
            odisseusToolCodeParams.SAGCorrected = false;//Changed
            odisseusToolCodeParams.StdIFRIIFR = 0;
            odisseusToolCodeParams.BiasSymmetric = 0;
            odisseusToolCodeParams.DepthStation = MxSDepthStation.Station;
            odisseusToolCodeParams.MagneticMud = false; //Changed
            odisseusToolCodeParams.RelDepth = 0;
            odisseusToolCodeParams.Misalign = 0;
            odisseusToolCodeParams.TrueInc = 0;
            odisseusToolCodeParams.CompassRef = 0;
            odisseusToolCodeParams.DStringMag = 0;
            odisseusToolCodeParams.GyroError = 0;
        }

        public static bool IsParamEnviromentalDataEmpty(this OdisseusToolCodeParams odisseusToolCodeParams)
        {
            if (odisseusToolCodeParams.DepthUnits == 0
                || odisseusToolCodeParams.TieinN == string.Empty
                || odisseusToolCodeParams.TieinE == string.Empty
                || odisseusToolCodeParams.TieinV == string.Empty
               )
            {
                return true;
            }
            return false;
        }
    }
}
