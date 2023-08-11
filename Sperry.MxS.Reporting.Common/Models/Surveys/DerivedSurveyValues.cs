using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Helpers;
using Sperry.MxS.Core.Common.Extensions;
using System;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    public class DerivedSurveyValues
    {
        public DerivedSurveyValues(CorrectedSurvey correctedSurvey)
        {
            //Rick, this is going to throw null reference excpetion when the ctor creates a new solution.
            //the solution will not have any of the properties which are being accessed in the getRelateWaypoints method.
            //probe better to throw null arguement exception.
            CorrectedSurvey = correctedSurvey ?? new CorrectedSurvey();
        }

        public CorrectedSurvey CorrectedSurvey { get; set; } = null;

        public double? Bx
        {
            get { return CorrectedSurvey.Bx; }
            set
            {
                if (CorrectedSurvey.Bx.CompareDoubleByFormat(value, MxSFormatType.BxByBz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.Bx = value;
            }
        }

        public double? By
        {
            get { return CorrectedSurvey.By; }
            set
            {
                if (CorrectedSurvey.By.CompareDoubleByFormat(value, MxSFormatType.BxByBz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.By = value;
            }
        }

        public double? Bz
        {
            get { return CorrectedSurvey.Bz; }
            set
            {
                if (CorrectedSurvey.Bz.CompareDoubleByFormat(value, MxSFormatType.BxByBz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.Bz = value;
            }
        }

        public double? Gx
        {
            get { return CorrectedSurvey.Gx; }
            set
            {
                if (CorrectedSurvey.Gx.CompareDoubleByFormat(value, MxSFormatType.GxGyGz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.Gx = value;
            }
        }

        public double? Gy
        {
            get { return CorrectedSurvey.Gy; }
            set
            {
                if (CorrectedSurvey.Gy.CompareDoubleByFormat(value, MxSFormatType.GxGyGz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.Gy = value;
            }
        }

        public double? Gz
        {
            get { return CorrectedSurvey.Gz; }
            set
            {
                if (CorrectedSurvey.Gz.CompareDoubleByFormat(value, MxSFormatType.GxGyGz,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.Gz = value;
            }
        }

        public double? MWDInclination
        {
            get { return CorrectedSurvey.MWDInclination; }
            set
            {
                if (CorrectedSurvey.MWDInclination.CompareDoubleByFormat(value, MxSFormatType.Inclination,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.MWDInclination = value;
            }
        }

        public double? SagInclination
        {
            get { return CorrectedSurvey.SagInclination; }
            set
            {
                if (CorrectedSurvey.SagInclination.CompareDoubleByFormat(value, MxSFormatType.Inclination,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.SagInclination = value;
            }
        }

        public double? MWDShortCollar
        {
            get { return CorrectedSurvey.MWDShortCollar; }
            set
            {
                if (CorrectedSurvey.MWDShortCollar.CompareDoubleByFormat(value, MxSFormatType.Azimuth,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.MWDShortCollar = value;
            }
        }

        public double? MWDLongCollar
        {
            get { return CorrectedSurvey.MWDLongCollar; }
            set
            {
                if (CorrectedSurvey.MWDLongCollar.CompareDoubleByFormat(value, MxSFormatType.Azimuth,
                    CorrectedSurvey.Run.Well.UnitSystem)) return;
                CorrectedSurvey.MWDLongCollar = value;
            }
        }

        public DateTime? AtomicRigTime
        {
            get { return CorrectedSurvey.GetAtomicRigTime(); }
        }

        public double? SolGt
        {
            get { return CorrectedSurvey.SolGt; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolGt))
                    return;
                CorrectedSurvey.SolGt = value;
            }
        }

        public double? SolBt
        {
            get { return CorrectedSurvey.SolBt; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolBt)) return;
                CorrectedSurvey.SolBt = value;
            }
        }

        public double? SolDip
        {
            get { return CorrectedSurvey.SolDip; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolDip)) return;
                CorrectedSurvey.SolDip = value;
            }
        }

        public double? SolBtDip
        {
            get { return CorrectedSurvey.SolBtDip; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolBtDip)) return;
                CorrectedSurvey.SolBtDip = value;
            }
        }

        public double? SolAzimSC
        {
            get { return CorrectedSurvey.SolAzmSc; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolAzmSc)) return;
                CorrectedSurvey.SolAzmSc = value;
            }
        }

        public double? SolAzimLC
        {
            get { return CorrectedSurvey.SolAzmLc; }
            set
            {
                if (value.Equals(CorrectedSurvey.SolAzmLc)) return;
                CorrectedSurvey.SolAzmLc = value;
            }
        }

        #region Common

        public double? GtRawQC
        {
            get { return CorrectedSurvey.CalculateGtRawQc(); }
        }

        public string Ipm
        {
            get
            {
                return CorrectedSurvey != null && CorrectedSurvey.Solution != null && CorrectedSurvey.Solution.QCType.IsIncludeDynamic()
                    ? CorrectedSurvey.Solution.IPMToolCode
                    : string.Empty;
            }
        }

        public double? Sigma
        {
            get { return (CorrectedSurvey != null && CorrectedSurvey.Solution != null && CorrectedSurvey.Solution.Sigma.HasValue) ? CorrectedSurvey.Solution.Sigma : null; }
        }

        #endregion Common

        #region MFM Values

        public double? MMDipe
        {
            get
            {
                Waypoint waypoint = this.GetRelateWaypoint();
                if (waypoint != null)
                    return waypoint.BGGMDip;
                return null;
            }
        }

        public double? MMBe
        {
            get
            {
                Waypoint waypoint = this.GetRelateWaypoint();
                if (waypoint != null)
                    return waypoint.BGGMBTotal;
                return null;
            }
        }

        public double? MMTotalDec
        {
            get
            {
                Waypoint waypoint = this.GetRelateWaypoint();
                if (waypoint != null)
                    return waypoint.BGGMDeclination;
                return null;
            }
        }

        #endregion MFM Values

        #region MFM

        private CorrectedSurveyValues MfmValues
        {
            get
            {
                return
                    CorrectedSurvey.Values.FirstOrDefault(
                        p => p.CalculationType == MxSCalculationType.MFM && p.State != MxSState.Deleted);
            }
        }

        public double? MFMLC
        {
            get { return MfmValues != null ? MfmValues.LongCollarAzimuth : new Nullable<double>(); }
        }

        public double? MFMSC
        {
            get { return MfmValues != null ? MfmValues.ShortCollarAzimuth : new Nullable<double>(); }
        }

        public double? MFMNS
        {
            get { return MfmValues != null ? MfmValues.NSDeparture : new Nullable<double>(); }
        }

        public double? MFMEW
        {
            get { return MfmValues != null ? MfmValues.EWDeparture : new Nullable<double>(); }
        }

        public double? MFMTVD
        {
            get { return MfmValues != null ? MfmValues.TVD : new Nullable<double>(); }
        }

        public double? MFMBt
        {
            get { return MfmValues != null ? MfmValues.BTotal : new Nullable<double>(); }
        }

        public double? MFMDip
        {
            get { return MfmValues != null ? MfmValues.Dip : new Nullable<double>(); }
        }

        public double? MFMBv
        {
            get { return MfmValues != null ? MfmValues.Bv : new Nullable<double>(); }
        }

        public double? MFMBh
        {
            get { return MfmValues != null ? MfmValues.Bh : new Nullable<double>(); }
        }

        public double? MFMBtBzCalc
        {
            get { return MfmValues != null ? MfmValues.BTotalBzCalc : new Nullable<double>(); }
        }

        public double? MFMDipBzCalc
        {
            get { return MfmValues != null ? MfmValues.DipBzCalc : new Nullable<double>(); }
        }

        public double? MFMBtDipBzCalc
        {
            get { return MfmValues != null ? MfmValues.BTotalDipBzCalc : new Nullable<double>(); }
        }

        public double? MFMBtDipBzMeasured
        {
            get { return MfmValues != null ? MfmValues.BTotalDipBzMeasured : new Nullable<double>(); }
        }

        public double? MFMBvBzCalc
        {
            get { return MfmValues != null ? MfmValues.BvBzCalc : new Nullable<double>(); }
        }

        public double? MFMBhBzCalc
        {
            get { return MfmValues != null ? MfmValues.BhBzCalc : new Nullable<double>(); }
        }

        public double? MFMBzCalc
        {
            get { return MfmValues != null ? MfmValues.BzCalc : new Nullable<double>(); }
        }

        public double? MFMDec
        {
            get { return MfmValues != null ? MfmValues.Declination : new Nullable<double>(); }
        }

        #endregion MFM

        #region IFR1

        private CorrectedSurveyValues IFR1Values
        {
            get
            {
                return
                    CorrectedSurvey.Values.FirstOrDefault(
                        p => p.CalculationType == MxSCalculationType.IFR && p.State != MxSState.Deleted);
            }
        }

        public double? IFR1LC
        {
            get { return IFR1Values != null ? IFR1Values.LongCollarAzimuth : new Nullable<double>(); }
        }

        public double? IFR1SC
        {
            get { return IFR1Values != null ? IFR1Values.ShortCollarAzimuth : new Nullable<double>(); }
        }

        public double? IFR1NS
        {
            get { return IFR1Values != null ? IFR1Values.NSDeparture : new Nullable<double>(); }
        }

        public double? IFR1EW
        {
            get { return IFR1Values != null ? IFR1Values.EWDeparture : new Nullable<double>(); }
        }

        public double? IFR1TVD
        {
            get { return IFR1Values != null ? IFR1Values.TVD : new Nullable<double>(); }
        }

        public double? IFR1Bt
        {
            get { return IFR1Values != null ? IFR1Values.BTotal : new Nullable<double>(); }
        }

        public double? IFR1Dip
        {
            get { return IFR1Values != null ? IFR1Values.Dip : new Nullable<double>(); }
        }

        public double? IFR1Bv
        {
            get { return IFR1Values != null ? IFR1Values.Bv : new Nullable<double>(); }
        }

        public double? IFR1Bh
        {
            get { return IFR1Values != null ? IFR1Values.Bh : new Nullable<double>(); }
        }

        public double? IFR1BtBzCalc
        {
            get { return IFR1Values != null ? IFR1Values.BTotalBzCalc : new Nullable<double>(); }
        }

        public double? IFR1DipBzCalc
        {
            get { return IFR1Values != null ? IFR1Values.DipBzCalc : new Nullable<double>(); }
        }

        public double? IFR1BtDipBzCalc
        {
            get { return IFR1Values != null ? IFR1Values.BTotalDipBzCalc : new Nullable<double>(); }
        }

        public double? IFR1BtDipBzMeasured
        {
            get { return IFR1Values != null ? IFR1Values.BTotalDipBzMeasured : new Nullable<double>(); }
        }

        public double? IFR1BvBzCalc
        {
            get { return IFR1Values != null ? IFR1Values.BvBzCalc : new Nullable<double>(); }
        }

        public double? IFR1BhBzCalc
        {
            get { return IFR1Values != null ? IFR1Values.BhBzCalc : new Nullable<double>(); }
        }

        public double? IFR1BzCalc
        {
            get { return IFR1Values != null ? IFR1Values.BzCalc : new Nullable<double>(); }
        }

        public double? IFR1Dec
        {
            get { return IFR1Values != null ? IFR1Values.Declination : new Nullable<double>(); }
        }

        #endregion IFR1

        #region IFR2

        private CorrectedSurveyValues IFR2Values
        {
            get
            {
                return
                    CorrectedSurvey.Values.SingleOrDefault(
                        p => p.CalculationType == MxSCalculationType.IIFR && p.State != MxSState.Deleted);
            }
        }

        public double? IFR2LC
        {
            get { return IFR2Values != null ? IFR2Values.LongCollarAzimuth : new Nullable<double>(); }
        }

        public double? IFR2SC
        {
            get { return IFR2Values != null ? IFR2Values.ShortCollarAzimuth : new Nullable<double>(); }
        }

        public double? IFR2NS
        {
            get { return IFR2Values != null ? IFR2Values.NSDeparture : new Nullable<double>(); }
        }

        public double? IFR2EW
        {
            get { return IFR2Values != null ? IFR2Values.EWDeparture : new Nullable<double>(); }
        }

        public double? IFR2TVD
        {
            get { return IFR2Values != null ? IFR2Values.TVD : new Nullable<double>(); }
        }

        public double? IFR2Bt
        {
            get { return IFR2Values != null ? IFR2Values.BTotal : new Nullable<double>(); }
        }

        public double? IFR2Dip
        {
            get { return IFR2Values != null ? IFR2Values.Dip : new Nullable<double>(); }
        }

        public double? IFR2Bv
        {
            get { return IFR2Values != null ? IFR2Values.Bv : new Nullable<double>(); }
        }

        public double? IFR2Bh
        {
            get { return IFR2Values != null ? IFR2Values.Bh : new Nullable<double>(); }
        }

        public double? IFR2BtBzCalc
        {
            get { return IFR2Values != null ? IFR2Values.BTotalBzCalc : new Nullable<double>(); }
        }

        public double? IFR2DipBzCalc
        {
            get { return IFR2Values != null ? IFR2Values.DipBzCalc : new Nullable<double>(); }
        }

        public double? IFR2BtDipBzCalc
        {
            get { return IFR2Values != null ? IFR2Values.BTotalDipBzCalc : new Nullable<double>(); }
        }

        public double? IFR2BtDipBzMeasured
        {
            get { return IFR2Values != null ? IFR2Values.BTotalDipBzMeasured : new Nullable<double>(); }
        }

        public double? IFR2BvBzCalc
        {
            get { return IFR2Values != null ? IFR2Values.BvBzCalc : new Nullable<double>(); }
        }

        public double? IFR2BhBzCalc
        {
            get { return IFR2Values != null ? IFR2Values.BhBzCalc : new Nullable<double>(); }
        }

        public double? IFR2BzCalc
        {
            get { return IFR2Values != null ? IFR2Values.BzCalc : new Nullable<double>(); }
        }

        public double? IFR2Dec
        {
            get { return IFR2Values != null ? IFR2Values.Declination : new Nullable<double>(); }
        }

        public string RigTimeOffset
        {
            get { return TimeHelper.GetTimeSpanString(CorrectedSurvey.RigTimeOffset); }
        }

        #endregion IFR2

        #region Icarus

        private CorrectedSurveyValues IcaValues
        {
            get
            {
                return
                    CorrectedSurvey.Values.SingleOrDefault(
                        p => p.CalculationType == MxSCalculationType.Ica && p.State != MxSState.Deleted);
            }
        }

        public double? IcaGxyzInc
        {
            get { return IcaValues != null ? IcaValues.GxyzInclination : new Nullable<double>(); }
        }

        public double? IcaGx
        {
            get { return IcaValues != null ? (IcaValues.Gx != null ? IcaValues.Gx.Value : 0) : new Nullable<double>(); }
        }

        public double? IcaGy
        {
            get { return IcaValues != null ? (IcaValues.Gy != null ? IcaValues.Gy.Value : 0) : new Nullable<double>(); }
        }

        public double? IcaGz
        {
            get { return IcaValues != null ? (IcaValues.Gz != null ? IcaValues.Gz.Value : 0) : new Nullable<double>(); }
        }

        public double? IcaGt
        {
            get { return IcaValues != null ? IcaValues.GTotal : new Nullable<double>(); }
        }

        public double? IcaGoxy
        {
            get { return IcaValues != null ? IcaValues.Goxy : new Nullable<double>(); }
        }

        #endregion Icarus

        #region Cazandra

        private CorrectedSurveyValues CazValues
        {
            get
            {
                if (CorrectedSurvey.Values == null)
                    return null;
                return
                    CorrectedSurvey.Values.SingleOrDefault(
                        p => p.CalculationType == MxSCalculationType.Caz && p.State != MxSState.Deleted);
            }
        }

        public double? CazAz
        {
            get { return CazValues != null ? CazValues.Azimuth : new Nullable<double>(); }
        }

        public double? CazNS
        {
            get { return CazValues != null ? CazValues.NSDeparture : new Nullable<double>(); }
        }

        public double? CazEW
        {
            get { return CazValues != null ? CazValues.EWDeparture : new Nullable<double>(); }
        }

        public double? CazTVD
        {
            get { return CazValues != null ? CazValues.TVD : new Nullable<double>(); }
        }

        public double? CazBt
        {
            get { return CazValues != null ? CazValues.BTotal : new Nullable<double>(); }
        }

        public double? CazDip
        {
            get { return CazValues != null ? CazValues.Dip : new Nullable<double>(); }
        }

        public double? CazBtDip
        {
            get { return CazValues != null ? CazValues.BTotalDip : new Nullable<double>(); }
        }

        public double? CazBv
        {
            get { return CazValues != null ? CazValues.Bv : new Nullable<double>(); }
        }

        public double? CazBh
        {
            get { return CazValues != null ? CazValues.Bh : new Nullable<double>(); }
        }

        public double? CazBx
        {
            get { return CazValues != null ? (CazValues.Bx != null ? CazValues.Bx.Value : 0) : new Nullable<double>(); }
        }

        public double? CazBy
        {
            get { return CazValues != null ? (CazValues.By != null ? CazValues.By.Value : 0) : new Nullable<double>(); }
        }

        public double? CazBz
        {
            get { return CazValues != null ? (CazValues.Bz != null ? CazValues.Bz.Value : 0) : new Nullable<double>(); }
        }

        public double? CazBoxy
        {
            get { return CazValues != null ? CazValues.Boxy : new Nullable<double>(); }
        }

        #endregion Cazandra
    }
}
