using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CorrectedSurvey : DataModelBase, IMxSDataAvailable, IMxSDateTimeDepth, IMxSTimeOffSet
    {
        private MaxSurveyRuleSetResponse _ruleSetResponse;
        private MxSSurveyType _surveyType;
        private double? _btLimit;
        private double? _dipLimit;
        private string _appliedIcarusSolution;
        private string _appliedCazandraSolution;
        private string _appliedService;
        private string _azimuthSolution;
        private string _typeEditedBy;
        private RawSurvey _rawSurvey;

        private readonly List<MxSSurveyStatus> UnsettledSurveyStatuses = new List<MxSSurveyStatus>
        {
            MxSSurveyStatus.New,
            MxSSurveyStatus.WaitObs,
            MxSSurveyStatus.WaitObsOver20Minutes,
            MxSSurveyStatus.WaitObsOver10Minutes,
            MxSSurveyStatus.WaitObsOver5Minutes,
            MxSSurveyStatus.Error
        };

        [Obsolete("This is only for workbench and any other components should not be using this functionality")]
        public CorrectedSurvey(PlanSurvey planSurvey) : this()
        {
            RawSurvey rawSurvey = new RawSurvey() { DateTime = planSurvey.DateTime, Depth = planSurvey.Depth, Solution = planSurvey.Solution, MWDInclination = planSurvey.MWDInclination, MWDShortCollar = planSurvey.MWDShortCollar };
            this.ResetValues(rawSurvey);
            SurveyStatus = MxSSurveyStatus.Processed;
        }

        public CorrectedSurvey(CorrectedSurvey surveyToCopy) : this()
        {
            //Listen = false; Not Using
            LastEditedTime = surveyToCopy.LastEditedTime;
            ErrorMessage = surveyToCopy.ErrorMessage;
            Expired = surveyToCopy.Expired;
            _surveyType = surveyToCopy.SurveyType;
            SurveyStatus = surveyToCopy.SurveyStatus;
            AzimuthSolution = surveyToCopy.AzimuthSolution;
            CalculationDateTime = surveyToCopy.CalculationDateTime;
            NomGt = surveyToCopy.NomGt;
            NomBt = surveyToCopy.NomBt;
            NomDip = surveyToCopy.NomDip;
            NomBtDip = surveyToCopy.NomBtDip;
            NomDeclination = surveyToCopy.NomDeclination;
            NomGrid = surveyToCopy.NomGrid;
            NomBv = surveyToCopy.NomBv;
            NomBh = surveyToCopy.NomBh;
            BtMeasured = surveyToCopy.BtMeasured;
            DipMeasured = surveyToCopy.DipMeasured;
            BvMeasured = surveyToCopy.BvMeasured;
            BhMeasured = surveyToCopy.BhMeasured;
            AzRssDynamicLimit = surveyToCopy.AzRssDynamicLimit;
            BtRssDynamicLimit = surveyToCopy.BtRssDynamicLimit;
            DipRssDynamicLimit = surveyToCopy.DipRssDynamicLimit;
            BtDipRssDynamicLimit = surveyToCopy.BtDipRssDynamicLimit;
            SolAzm = surveyToCopy.SolAzm;
            SolAzmSc = surveyToCopy.SolAzmSc;
            SolAzmLc = surveyToCopy.SolAzmLc;
            SolDec = surveyToCopy.SolDec;
            SolGridConv = surveyToCopy.SolGridConv;
            SolInc = surveyToCopy.SolInc;
            SolBz = surveyToCopy.SolBz;
            SolGt = surveyToCopy.SolGt;
            SolBt = surveyToCopy.SolBt;
            SolDip = surveyToCopy.SolDip;
            SolBtDip = surveyToCopy.SolBtDip;
            SolBv = surveyToCopy.SolBv;
            SolBh = surveyToCopy.SolBh;
            RigInclination = surveyToCopy.RigInclination;
            RigAzimuthSC = surveyToCopy.RigAzimuthSC;
            RigAzimuthLC = surveyToCopy.RigAzimuthLC;
            GravityToolFace = surveyToCopy.GravityToolFace;
            GxyzInclination = surveyToCopy.GxyzInclination;
            GxyInclination = surveyToCopy.GxyInclination;
            GzInclination = surveyToCopy.GzInclination;
            ReferenceInclination = surveyToCopy.ReferenceInclination;
            GTotal = surveyToCopy.GTotal;
            Goxy = surveyToCopy.Goxy;
            MWDShortCollar = surveyToCopy.MWDShortCollar;
            MWDLongCollar = surveyToCopy.MWDLongCollar;
            AzimuthType = surveyToCopy.AzimuthType;
            DateTime = surveyToCopy.DateTime;
            Depth = surveyToCopy.Depth;
            Enabled = surveyToCopy.Enabled;
            PumpStatus = surveyToCopy.PumpStatus;
            Gx = surveyToCopy.Gx;
            Gy = surveyToCopy.Gy;
            Gz = surveyToCopy.Gz;
            Bx = surveyToCopy.Bx;
            By = surveyToCopy.By;
            Bz = surveyToCopy.Bz;

            IcadGx = surveyToCopy.IcadGx;
            IcadGy = surveyToCopy.IcadGy;
            IcadGz = surveyToCopy.IcadGz;
            IcaSFGx = surveyToCopy.IcaSFGx;
            IcaSFGy = surveyToCopy.IcaSFGy;
            CazdBx = surveyToCopy.CazdBx;
            CazdBy = surveyToCopy.CazdBy;
            CazdBz = surveyToCopy.CazdBz;
            CazSFBx = surveyToCopy.CazSFBx;
            CazSFBy = surveyToCopy.CazSFBy;
            CourseLength = surveyToCopy.CourseLength;

            SoldBx = surveyToCopy.SoldBx;
            SoldBy = surveyToCopy.SoldBy;
            SoldBz = surveyToCopy.SoldBz;

            AppliedService = surveyToCopy.AppliedService;
            AppliedCazandraSolution = surveyToCopy.AppliedCazandraSolution;
            AppliedIcarusSolution = surveyToCopy.AppliedIcarusSolution;


            MWDInclination = surveyToCopy.MWDInclination;
            SagInclination = surveyToCopy.SagInclination;
            SurveyRecorded = surveyToCopy.SurveyRecorded;
            _btLimit = surveyToCopy.BtLimit;
            _dipLimit = surveyToCopy.DipLimit;
            BtDipLimit = surveyToCopy.BtDipLimit;
            DipLimit = surveyToCopy.DipLimit;
            BtDipLimit = surveyToCopy.BtDipLimit;
            if (!string.IsNullOrWhiteSpace(surveyToCopy.TypeEditedBy))
            {
                _typeEditedBy = string.Intern(surveyToCopy.TypeEditedBy);
            }

            SISA = surveyToCopy.SISA;
            CazUsed = surveyToCopy.CazUsed;

            RigTimeOffset = surveyToCopy.RigTimeOffset;
            ManualTimeOffsetFlag = surveyToCopy.ManualTimeOffsetFlag;
            ObservatoryBTRaw = surveyToCopy.ObservatoryBTRaw;
            ObservatoryDateTimeRaw = surveyToCopy.ObservatoryDateTimeRaw;
            ObservatoryDecRaw = surveyToCopy.ObservatoryDecRaw;
            ObservatoryDipRaw = surveyToCopy.ObservatoryDipRaw;
            ObservatoryTimeOffset = surveyToCopy.ObservatoryTimeOffset;
            ASAMode = surveyToCopy.ASAMode;
            ASAStatusEnum = surveyToCopy.ASAStatusEnum;
            ASAResult = surveyToCopy.ASAResult;

            IcaUsed = surveyToCopy.IcaUsed;


            if (surveyToCopy.UncertaintyValues != null)
            {
                Uncertainty uncertainty = surveyToCopy.UncertaintyValues.FirstOrDefault();

                if (uncertainty != null)
                {
                    if (uncertainty.State != MxSState.Deleted)
                    {

                        Uncertainty copiedValues = new Uncertainty(uncertainty);
                        this.AddUncertaintyValues(copiedValues);
                    }
                }


            }

            if (surveyToCopy.Values != null)
            {
                foreach (var values in surveyToCopy.Values)
                {
                    if (values.State == MxSState.Deleted)
                        continue;

                    CorrectedSurveyValues copiedValues = new CorrectedSurveyValues(values);
                    this.AddCorrectedSurveyValue(copiedValues);

                }
            }

            if (surveyToCopy.BgsDataPoints != null)
            {
                foreach (var bgsDataPoint in surveyToCopy.BgsDataPoints)
                {
                    //if (bgsDataPoint.State == State.Deleted)
                    //    continue;
                    BGSDataPoint copiedDataPoint = new BGSDataPoint(bgsDataPoint);
                    this.AddBgsDatapoint(copiedDataPoint);
                }
            }

            if (surveyToCopy.MaxSurveyRuleSetResponse != null)
            {
                foreach (var rulesetResponse in surveyToCopy.MaxSurveyRuleSetResponse)
                {
                    //if (rulesetResponse.State == State.Deleted)
                    //    continue;
                    this.AddMaxSurveyRuleSetResponse(new MaxSurveyRuleSetResponse(rulesetResponse));
                }
            }

            LCBtQCDelta = surveyToCopy.LCBtQCDelta;
            SCBtQCDelta = surveyToCopy.SCBtQCDelta;
            CazBtQCDelta = surveyToCopy.CazBtQCDelta;
            LCDipQCDelta = surveyToCopy.LCDipQCDelta;
            SCDipQCDelta = surveyToCopy.SCDipQCDelta;
            CazDipQCDelta = surveyToCopy.CazDipQCDelta;

            NorthSouth = surveyToCopy.NorthSouth;
            EastWest = surveyToCopy.EastWest;
            Northing = surveyToCopy.Northing;
            Easting = surveyToCopy.Easting;
            TVD = surveyToCopy.TVD;
            TVDss = surveyToCopy.TVDss;
            Latitude = surveyToCopy.Latitude;
            Longitude = surveyToCopy.Longitude;

            NorthReference = surveyToCopy.NorthReference;

            RawSurvey = surveyToCopy.RawSurvey;

            DCLatitude = surveyToCopy.DCLatitude;
            DCLongitude = surveyToCopy.DCLongitude;
            DCTVDss = surveyToCopy.DCTVDss;
            Comment = surveyToCopy.Comment;

            ReceiveTime = surveyToCopy.ReceiveTime;
            ProcessedTime = surveyToCopy.ProcessedTime;
            SetDefinitiveTime = surveyToCopy.SetDefinitiveTime;

            //Listen = true; not Using
        }

        [JsonProperty]
        public List<BGSDataPoint> BgsDataPoints { get; protected set; }

        [NotMapped]
        [JsonProperty]
        public DateTime SurveyRecorded { get; set; }

        public Run Run => RawSurvey != null ? RawSurvey.Run : null;

        [JsonIgnore]
        // public string RunNo => RawSurvey.RunNo;
        // Changed By Naveen Kumar
        public string RunNo => RawSurvey != null ? RawSurvey.RunNo : null;

        [JsonIgnore]
        public Solution Solution => RawSurvey?.Solution;

        [JsonProperty]
        public List<Uncertainty> UncertaintyValues { get; protected set; }

        public Uncertainty UncertaintyValue => UncertaintyValues.LastOrDefault(item => item.State != MxSState.Deleted);

        public double? UncertaintySigmaN => UncertaintyValue != null ? UncertaintyValue.SigmaN : null;

        public double? UncertaintySigmaH => UncertaintyValue != null ? UncertaintyValue.SigmaH : null;

        public double? UncertaintySigmaL => UncertaintyValue != null ? UncertaintyValue.SigmaL : null;

        public double? UncertaintySigmaA => UncertaintyValue != null ? UncertaintyValue.SigmaA : null;

        public double? UncertaintySigmaE => UncertaintyValue != null ? UncertaintyValue.SigmaE : null;

        public double? UncertaintySigmaV => UncertaintyValue != null ? UncertaintyValue.SigmaV : null;

        public double? UncertaintyBiasA => UncertaintyValue != null ? UncertaintyValue.BiasA : null;

        public double? UncertaintyBiasE => UncertaintyValue != null ? UncertaintyValue.BiasE : null;

        public double? UncertaintyBiasH => UncertaintyValue != null ? UncertaintyValue.BiasH : null;

        public double? UncertaintyBiasL => UncertaintyValue != null ? UncertaintyValue.BiasL : null;

        public double? UncertaintyBiasN => UncertaintyValue != null ? UncertaintyValue.BiasN : null;

        public double? UncertaintyBiasV => UncertaintyValue != null ? UncertaintyValue.BiasV : null;

        public double? UncertaintyCorrHL => UncertaintyValue != null ? UncertaintyValue.CorrHL : null;

        public double? UncertaintyCorrHA => UncertaintyValue != null ? UncertaintyValue.CorrHA : null;

        public double? UncertaintyCorrLA => UncertaintyValue != null ? UncertaintyValue.CorrLA : null;

        public double? UncertaintyCovEE => UncertaintyValue != null ? UncertaintyValue.CovEE : null;

        public double? UncertaintyCovEV => UncertaintyValue != null ? UncertaintyValue.CovEV : null;

        public double? UncertaintyCovNE => UncertaintyValue != null ? UncertaintyValue.CovNE : null;

        public double? UncertaintyCovNN => UncertaintyValue != null ? UncertaintyValue.CovNN : null;

        public double? UncertaintyCovNV => UncertaintyValue != null ? UncertaintyValue.CovNV : null;

        public double? UncertaintyCovVV => UncertaintyValue != null ? UncertaintyValue.CovVV : null;

        public double? UncertaintyHMajSA => UncertaintyValue != null ? UncertaintyValue.HMajSA : null;

        public double? UncertaintyHMinSA => UncertaintyValue != null ? UncertaintyValue.HMinSA : null;

        public double? UncertaintySemiAx1 => UncertaintyValue != null ? UncertaintyValue.SemiAx1 : null;

        public double? UncertaintySemiAx2 => UncertaintyValue != null ? UncertaintyValue.SemiAx2 : null;

        public double? UncertaintySemiAx3 => UncertaintyValue != null ? UncertaintyValue.SemiAx3 : null;

        public double? UncertaintyRotAng => UncertaintyValue != null ? UncertaintyValue.RotAng : null;

        public string UncertaintyToolCode => UncertaintyValue != null ? UncertaintyValue.ToolCode : string.Empty;

        [JsonProperty]
        public Guid RawSurveyId { get; set; }

        // Commented by Naveen Kumar and Changed to
        [JsonProperty]
        public RawSurvey RawSurvey { get; set; }

        //public RawSurvey RawSurvey
        //{
        //    get { return _rawSurvey; }
        //    set
        //    {
        //        //if (_rawSurvey != null)
        //            _rawSurvey = value;
        //            //_rawSurvey.PropertyChanged -= _rawSurvey_PropertyChanged;
        //        if (value != null)
        //        {
        //            //_rawSurvey.PropertyChanged -= _rawSurvey_PropertyChanged;
        //            //_rawSurvey.PropertyChanged += _rawSurvey_PropertyChanged;
        //            //RawSurveyId = value.Id;
        //        }
        //        else
        //        {
        //            RawSurveyId = Guid.Empty;
        //        }
        //    }
        //}

        [JsonProperty]
        public MaxSurveyRuleSetResponse RuleSetResponse
        {
            get
            {
                return MaxSurveyRuleSetResponse.FirstOrDefault(response => response.State != MxSState.Deleted);
            }
            protected set { _ruleSetResponse = value; }
        }

        [JsonProperty]
        public List<MaxSurveyRuleSetResponse> MaxSurveyRuleSetResponse { get; set; }

        [JsonProperty]
        public List<CorrectedSurveyValues> Values { get; protected set; }

        [JsonProperty]
        public DateTime DateTime { get; set; } = MxSConstants.MinDateTime;

        [JsonProperty]
        public long? RigTimeOffset { get; set; }

        [JsonProperty]
        public bool ManualTimeOffsetFlag { get; set; }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public bool Enabled { get; set; }

        [JsonProperty]
        public MxSSurveyPumpStatus PumpStatus { get; set; } = MxSSurveyPumpStatus.NA;

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
        public double? MWDInclination { get; set; }

        [JsonProperty]
        public double? SagInclination { get; set; }

        [JsonProperty]
        public double? MWDShortCollar { get; set; }

        [JsonProperty]
        public double? MWDLongCollar { get; set; }

        [JsonProperty]
        public MxSAzimuthTypeEnum AzimuthType { get; set; }

        [JsonProperty]
        public double? GravityToolFace { get; set; }

        [JsonProperty]
        public double? GxyzInclination { get; set; }

        [JsonProperty]
        public double? GxyInclination { get; set; }

        [JsonProperty]
        public double? GzInclination { get; set; }

        [JsonProperty]
        public double? ReferenceInclination { get; set; }

        [JsonProperty]
        public double? GTotal { get; set; }

        [JsonProperty]
        public double? Goxy { get; set; }

        [JsonProperty]
        public double? RigInclination { get; set; }

        [JsonProperty]
        public double? RigAzimuthSC { get; set; }

        [JsonProperty]
        public double? RigAzimuthLC { get; set; }

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

        [JsonProperty]
        public double? SolBv { get; set; }

        [JsonProperty]
        public double? SolBh { get; set; }

        public SurveyQCLimits QCLimits
        {
            get
            {
                if (RawSurvey != null && RawSurvey.Run != null && RawSurvey.Run.Well != null && RawSurvey.Run.Well.IsEnableStaticQc)
                {
                    return new SurveyQCLimits(
                        RawSurvey.Run.Well.Id,
                        RawSurvey.Run.Well.GtQcLimit,
                        RawSurvey.Run.Well.BtQcLimit,
                        RawSurvey.Run.Well.BtDipQcLimit,
                        RawSurvey.Run.Well.DipQcLimit,
                        RawSurvey.Run.Well.LowAzimuthQcLimit,
                        RawSurvey.Run.Well.HighAzimuthQcLimit
                        );
                }
                return null;
            }
        }

        [JsonProperty]
        public double? BtLimit
        {
            get { return _btLimit; }
            set
            {
                _btLimit = value;
                BtLimitForCharts = value;
            }
        }

        [NotMapped]
        public static double? BtLimitForCharts { get; set; }

        [JsonProperty]
        public double? DipLimit
        {
            get { return _dipLimit; }
            set
            {
                _dipLimit = value;
                DipLimitForCharts = value;
            }
        }

        [NotMapped]
        public static double? DipLimitForCharts { get; set; }

        [JsonProperty]
        public double? BtDipLimit { get; set; }

        [JsonProperty]
        public double? AzRssDynamicLimit { get; set; }

        [JsonProperty]
        public double? BtRssDynamicLimit { get; set; }

        [JsonProperty]
        public double? DipRssDynamicLimit { get; set; }

        [JsonProperty]
        public double? BtDipRssDynamicLimit { get; set; }

        [JsonProperty]
        public double? BtMeasured { get; set; }

        [JsonProperty]
        public double? DipMeasured { get; set; }

        [JsonProperty]
        public double? BvMeasured { get; set; }

        [JsonProperty]
        public double? BhMeasured { get; set; }

        [JsonProperty]
        public double? NomGt { get; set; } = 1.0;

        [JsonProperty]
        public double? NomBt { get; set; }

        [JsonProperty]
        public double? NomDip { get; set; }

        [JsonProperty]
        public double? NomBtDip { get; set; }

        [JsonProperty]
        public double? NomDeclination { get; set; }

        [JsonProperty]
        public double? NomGrid { get; set; }

        [JsonProperty]
        public int IcaUsed { get; set; }

        [JsonProperty]
        public int CazUsed { get; set; }

        [JsonProperty]
        public string Comment { get; set; }

        public double NomAzimSc
        {
            get
            {
                if (RawSurvey != null)
                    return RawSurvey.MWDShortCollar ?? double.NaN;
                return double.NaN;
            }
        }

        public double NomAzimLc
        {
            get
            {
                if (RawSurvey != null)
                    return RawSurvey.MWDLongCollar ?? double.NaN;
                return double.NaN;
            }
        }

        [JsonProperty]
        public double? NomBv { get; set; }

        [JsonProperty]
        public double? NomBh { get; set; }

        [NotMapped]
        public double? GtQcDelta
        {
            get { return -(NomGt - SolGt); }
        }

        [NotMapped]
        public double? BtQcDelta
        {
            get { return -(NomBt - SolBt); }
        }

        [NotMapped]
        public double? DipQcDelta
        {
            get { return -(NomDip - SolDip); }
        }

        [NotMapped]
        public double? BtDipQcDelta
        {
            get { return SolBtDip - NomBtDip; }
        }

        [NotMapped]
        public double? InclinationDelta
        {
            get { return -(MWDInclination - RigInclination); }
        }

        public double AzimuthLcQcDelta
        {
            get
            {
                if (double.IsNaN(NomAzimLc))
                    return 0;
                return -(NomAzimLc - SolAzmLc ?? 0);
            }
        }

        public double AzimuthScQcDelta
        {
            get
            {
                if (double.IsNaN(NomAzimSc))
                    return 0;
                return -(NomAzimSc - SolAzmSc ?? 0);
            }
        }

        [JsonProperty]
        public double? ObservatoryDipRaw { get; set; }

        [JsonProperty]
        public double? ObservatoryBTRaw { get; set; }

        [JsonProperty]
        public double? ObservatoryDecRaw { get; set; }

        [JsonProperty]
        public long? ObservatoryTimeOffset { get; set; }

        [JsonProperty]
        public DateTime? ObservatoryDateTimeRaw { get; set; }

        [JsonProperty]
        public string AppliedIcarusSolution
        {
            get { return _appliedIcarusSolution; }
            set
            {
                if (_appliedIcarusSolution != value)
                {
                    _appliedIcarusSolution = !string.IsNullOrEmpty(value) ? string.Intern(value) : value;
                }
            }
        }

        [JsonProperty]
        public string AppliedCazandraSolution
        {
            get { return _appliedCazandraSolution; }
            set
            {
                if (_appliedCazandraSolution != value)
                {
                    _appliedCazandraSolution = !string.IsNullOrEmpty(value) ? string.Intern(value) : value;
                }
            }
        }

        [JsonProperty]
        public string AppliedService
        {
            get { return _appliedService; }
            set
            {
                if (_appliedService != value)
                {
                    _appliedService = !string.IsNullOrEmpty(value) ? string.Intern(value) : value;
                }
            }
        }

        [JsonProperty]
        public MxSNorthReference? NorthReference { get; set; }

        [JsonProperty]
        public string AzimuthSolution
        {
            get { return _azimuthSolution; }
            set
            {
                if (_azimuthSolution != value)
                {
                    _azimuthSolution = !string.IsNullOrEmpty(value) ? string.Intern(value) : value;
                }
            }
        }

        public double HighSide
        {
            get
            {
                if (Gx == null || Gy == null)
                    return 0;
                return Math.Atan2(Gy.Value, -Gx.Value) / Math.PI * 180;
            }
        }

        [JsonProperty]
        public DateTime CalculationDateTime { get; set; }

        [JsonProperty]
        public MxSSurveyStatus SurveyStatus { get; set; }

        [JsonProperty]
        public MxSSurveyType SurveyType
        {
            get { return _surveyType; }
            set
            {

                _surveyType = value;
                if (_surveyType == MxSSurveyType.Definitive)
                {
                    SetDefinitiveTime = DateTime.Now.ToUniversalTime();
                }
                else
                {
                    SetDefinitiveTime = null;
                }
                TypeEditedBy = MxSConstants.UserName;
            }
        }

        [JsonProperty]
        public bool Expired { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorAzimuth { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorAzimuthMagnetic { get; set; }

        [JsonProperty]
        //[IgnoreImportProperty]
        public string TypeEditedBy
        {
            get { return _typeEditedBy; }
            set
            {
                if (_typeEditedBy != value)
                {
                    _typeEditedBy = !string.IsNullOrEmpty(value) ? string.Intern(value) : value;
                }

            }
        }

        [JsonProperty]
        public MxSASAStatusEnum ASAStatusEnum { get; set; } = MxSASAStatusEnum.NA;

        [JsonProperty]
        public MxSASAModeEnum ASAMode { get; set; } = MxSASAModeEnum.OFF;

        [NotMapped]
        [JsonProperty]
        public double AziErrorInclination { get; set; }

        [NotMapped]
        [JsonProperty]
        public double AziErrorDipe { get; set; }

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

        [JsonProperty]
        public string ErrorMessage { get; set; }

        [JsonProperty]
        public MxSRuleResultEnum ASAResult { get; set; } = MxSRuleResultEnum.NotExecuted;

        [JsonProperty]
        public double? SISA { get; set; }

        [NotMapped]
        [JsonProperty]
        public MxSMsaState MsaState { get; set; }

        [NotMapped]
        [JsonProperty]
        public object IcarusResult { get; set; }

        [JsonProperty]
        public double? IcadGx { get; set; } = 0.0;

        [JsonProperty]
        public double? IcadGy { get; set; } = 0.0;

        [JsonProperty]
        public double? IcadGz { get; set; } = 0.0;

        [JsonProperty]
        public double? IcaSFGx { get; set; } = 0.0;

        [JsonProperty]
        public double? IcaSFGy { get; set; } = 0.0;

        [JsonProperty]
        public double? CazdBx { get; set; } = 0.0;

        [JsonProperty]
        public double? CazdBy { get; set; } = 0.0;

        [JsonProperty]
        public double? CazdBz { get; set; } = 0.0;

        [JsonProperty]
        public double? SoldBx { get; set; } = 0.0;

        [JsonProperty]
        public double? SoldBy { get; set; } = 0.0;

        [JsonProperty]
        public double? SoldBz { get; set; } = 0.0;

        [JsonProperty]
        public double? CazSFBx { get; set; } = 0.0;

        [JsonProperty]
        public double? CazSFBy { get; set; } = 0.0;

        [JsonProperty]
        public double? CourseLength { get; set; } = null;

        [JsonProperty]
        public double? LCBtQCDelta { get; set; }

        [JsonProperty]
        public double? SCBtQCDelta { get; set; }

        [JsonProperty]
        public double? CazBtQCDelta { get; set; }

        [JsonProperty]
        public double? LCDipQCDelta { get; set; }

        [JsonProperty]
        public double? SCDipQCDelta { get; set; }

        [JsonProperty]
        public double? CazDipQCDelta { get; set; }

        [JsonProperty]
        public double? NorthSouth { get; set; }

        [JsonProperty]
        public double? EastWest { get; set; }

        [JsonProperty]
        public double? Northing { get; set; }

        [JsonProperty]
        public double? Easting { get; set; }

        [JsonProperty]
        public double? DCLatitude { get; set; }

        [JsonProperty]
        public double? DCLongitude { get; set; }

        [JsonProperty]
        public double? DCTVDss { get; set; }

        [JsonProperty]
        public double? Latitude { get; set; }

        [JsonProperty]
        public double? Longitude { get; set; }

        [JsonProperty]
        public double? TVD { get; set; }

        [JsonProperty]
        public double? TVDss { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        public ObservatoryStation ObservatoryStation
        {
            get
            {
                return Solution != null ? Solution.ObservatoryStation : null;
            }
        }

        [JsonProperty]
        public DateTime? ReceiveTime { get; set; } = null;

        [JsonProperty]
        public DateTime? ProcessedTime { get; set; } = null;

        [JsonProperty]
        public DateTime? SetDefinitiveTime { get; set; } = null;

        [NotMapped]
        public TimeSpan? SurveyReturnTime
        {
            get
            {
                if (SetDefinitiveTime.HasValue && ReceiveTime.HasValue)
                {
                    return (SetDefinitiveTime - ReceiveTime);
                }

                return null;
            }
        }

        [NotMapped]
        public TimeSpan? SurveyTotalTime
        {
            get
            {
                if (SetDefinitiveTime.HasValue && this.GetAtomicRigTime().HasValue)
                {
                    return (SetDefinitiveTime.Value - this.GetAtomicRigTime().Value.ToLocalTime().ToUniversalTime());
                }

                return null;
            }
        }

        [NotMapped]
        public TimeSpan? SurveyProcessTime
        {
            get
            {
                if (ProcessedTime.HasValue && ReceiveTime.HasValue)
                {
                    return (ProcessedTime - ReceiveTime);
                }

                return null;
            }
        }

        [NotMapped]
        public double DipQcLimit
        {
            get
            {
                Well well = this.GetWell();
                return well?.DipQcLimit ?? 0.0D;
            }
        }

        [NotMapped]
        public double GTotalQcLimit
        {
            get
            {
                Well well = this.GetWell();
                return well?.GtQcLimit ?? 0.0D;
            }
        }

        [NotMapped]
        public double GTotalQcMax
        {
            get
            {
                return NomGt.HasValue ? NomGt.Value + GTotalQcLimit : 0.0D;
            }
        }

        [NotMapped]
        public double GTotalQcMin
        {
            get
            {
                return NomGt.HasValue ? NomGt.Value - GTotalQcLimit : 0.0D;
            }
        }

        [NotMapped]
        public double DipQcMax
        {
            get
            {
                return this.GetMagneticParameters().Dip + DipQcLimit;

            }
        }

        [NotMapped]
        public double DipQcMin
        {
            get { return this.GetMagneticParameters().Dip - DipQcLimit; }
        }

        [NotMapped]
        public double BTotalNomMin
        {
            get { return this.GetNomBTotalQCLimitsValues().BTotalNomMin; }
        }

        [NotMapped]
        public double BTotalNomMax
        {
            get { return this.GetNomBTotalQCLimitsValues().BTotalNomMax; }
        }

        [NotMapped]
        public double BTotalMFMQCMin
        {
            get { return this.GetMFMBTotalQCLimitsValues().BTotalMFMQCMin; }
        }

        [NotMapped]
        public double BTotalMFMQCMax
        {
            get { return this.GetMFMBTotalQCLimitsValues().BTotalMFMQCMax; }
        }

        [NotMapped]
        public double BTotalIFRQCMin
        {
            get { return this.GetIFRBTotalQCLimitsValues().BTotalIFRQCMin; }
        }

        [NotMapped]
        public double BTotalIFRQCMax
        {
            get { return this.GetIFRBTotalQCLimitsValues().BTotalIFRQCMax; }
        }

        [NotMapped]
        public double BTotalIIFRQCMin
        {
            get { return this.GetIIFRBTotalQCLimitsValues().BTotalIIFRQCMin; }
        }

        [NotMapped]
        public double BTotalIIFRQCMax
        {
            get { return this.GetIIFRBTotalQCLimitsValues().BTotalIIFRQCMax; }
        }

        [NotMapped]
        public double? GtLimit
        {
            get { return QCLimits?.GtLimit; }
        }

        [NotMapped]

        // Modified by Naveen Kumar

        //public double? Temperature
        //{
        //    get { return RawSurvey.Temperature; }             

        //}

        public double? Temperature => RawSurvey != null ? RawSurvey.Temperature : null;

        public MxSQCLevel AzimLcLevel
        {
            get
            {
                if (IsNoQCLimitsAppliable() || Solution == null)
                    return MxSQCLevel.Normal;
                if (AzimuthType != MxSAzimuthTypeEnum.LongCollar)
                    return MxSQCLevel.Normal;
                return this.GetThreeRanksQCLevel(MWDLongCollar ?? 0, RigAzimuthLC ?? 0, QCLimits.AzimLowLimit, QCLimits.AzimHighLimit);
            }
        }

        public MxSQCLevel AzimScLevel
        {
            get
            {
                if (IsNoQCLimitsAppliable() || Solution == null)
                    return MxSQCLevel.Normal;
                if (AzimuthType != MxSAzimuthTypeEnum.ShortCollar)
                    return MxSQCLevel.Normal;
                return this.GetThreeRanksQCLevel(MWDShortCollar ?? 0, RigAzimuthSC ?? 0, QCLimits.AzimLowLimit, QCLimits.AzimHighLimit);
            }
        }

        public MxSQCLevel InclinationLevel
        {
            get
            {
                if (IsNoQCLimitsAppliable())
                    return MxSQCLevel.Normal;
                return this.GetThreeRanksQCLevel(MWDInclination ?? 0, RigInclination ?? 0, QCLimits.AzimLowLimit, QCLimits.AzimHighLimit);
            }
        }

        // Reset Caz Variables to Null

        // Reset Ica Variables to Null

        public CorrectedSurvey()
        {
            Values = new List<CorrectedSurveyValues>();
            UncertaintyValues = new List<Uncertainty>();
            BgsDataPoints = new List<BGSDataPoint>();
            MaxSurveyRuleSetResponse = new List<MaxSurveyRuleSetResponse>();
            ReceiveTime = DateTime.Now.ToUniversalTime();
        }

        public CorrectedSurvey(RawSurvey rawSurvey) : this()
        {
            this.ResetValues(rawSurvey);
            SurveyStatus = MxSSurveyStatus.New;
        }

        public CorrectedSurvey(ShortSurvey shortSurvey) : this()
        {
            DateTime = shortSurvey.DateTime;
            Depth = shortSurvey.Depth;
            SolInc = shortSurvey.Inclination;
            SolAzm = shortSurvey.Azimuth;
            SolBt = shortSurvey.BTotal;
            SolGt = shortSurvey.GTotal;
            SurveyType = shortSurvey.SurveyType;
            Enabled = shortSurvey.Enabled;
            NorthSouth = shortSurvey.NorthSouth;
            EastWest = shortSurvey.EastWest;
            Northing = shortSurvey.Northing;
            Easting = shortSurvey.Easting;
            TVD = shortSurvey.TVD;
            TVDss = shortSurvey.TVDss;
            Latitude = shortSurvey.Latitude;
            Longitude = shortSurvey.Longitude;

            if (this.UncertaintyValues != null)
            {
                var uncertainty = new Uncertainty();

                uncertainty.SigmaN = shortSurvey.SigmaN;
                uncertainty.SigmaE = shortSurvey.SigmaE;
                uncertainty.SigmaV = shortSurvey.SigmaV;
                uncertainty.SigmaL = shortSurvey.SigmaL;
                uncertainty.SigmaH = shortSurvey.SigmaH;
                uncertainty.SigmaA = shortSurvey.SigmaA;
                uncertainty.BiasN = shortSurvey.BiasN;
                uncertainty.BiasE = shortSurvey.BiasE;
                uncertainty.BiasV = shortSurvey.BiasV;
                uncertainty.BiasH = shortSurvey.BiasH;
                uncertainty.BiasL = shortSurvey.BiasL;
                uncertainty.BiasA = shortSurvey.BiasA;
                uncertainty.CorrHL = shortSurvey.CorrHL;
                uncertainty.CorrHA = shortSurvey.CorrHA;
                uncertainty.CorrLA = shortSurvey.CorrLA;
                uncertainty.HMajSA = shortSurvey.HMajSA;
                uncertainty.HMinSA = shortSurvey.HMinSA;
                uncertainty.RotAng = shortSurvey.RotAng;
                uncertainty.SemiAx1 = shortSurvey.SemiAx1;
                uncertainty.SemiAx2 = shortSurvey.SemiAx2;
                uncertainty.SemiAx3 = shortSurvey.SemiAx3;
                uncertainty.CovNN = shortSurvey.CovNN;
                uncertainty.CovNE = shortSurvey.CovNE;
                uncertainty.CovNV = shortSurvey.CovNV;
                uncertainty.CovEE = shortSurvey.CovEE;
                uncertainty.CovEV = shortSurvey.CovEV;
                uncertainty.CovVV = shortSurvey.CovVV;
                uncertainty.ToolCode = shortSurvey.ToolCode;

                this.AddUncertaintyValues(uncertainty);
            }


            CourseLength = shortSurvey.CourseLength;
            RawSurvey = new RawSurvey() { Run = shortSurvey.Run };
            SurveyStatus = MxSSurveyStatus.New;
            LastEditedBy = shortSurvey.LastEditedBy;
            CreatedBy = shortSurvey.CreatedBy;
        }

        public bool IsNoQCLimitsAppliable()
        {
            return UnsettledSurveyStatuses.Contains(SurveyStatus) || QCLimits == null;
        }

        public DateTime? GetAtomicRigTime()
        {
            if (IsAtomicTimeCalculationPossible())
            {
                DateTime time = DateTime;
                if (RigTimeOffset == null) return time;

                var timeOffset = TimeSpan.FromTicks(RigTimeOffset.Value);
                if (DateTime - DateTime.MinValue > -timeOffset)
                    time = DateTime + timeOffset;
                return time;
            }
            return null;
        }

        public bool IsAtomicTimeCalculationPossible()
        {
            return RigTimeOffset != null;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    base.Dispose(disposing);
        //    //if (_rawSurvey != null) { _rawSurvey.PropertyChanged -= _rawSurvey_PropertyChanged; }
        //    //Values.CollectionChanged -= Values_CollectionChanged;
        //    foreach (var correctedSurveyValue in Values)
        //    {
        //        correctedSurveyValue?.Dispose();
        //    }
        //    //MaxSurveyRuleSetResponse.CollectionChanged -= MaxSurveyRuleSetResponse_CollectionChanged;
        //    MaxSurveyRuleSetResponse?.Clear();
        //    Values?.Clear();
        //    UncertaintyValues?.Clear();
        //}
    }
}

