using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.ASA;
using Sperry.MxS.Core.Common.Models.Surveys;
using Sperry.MxS.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Ex.Helpers;
using Sperry.MxS.Core.Common.Extensions;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Solution : DataModelBase, IEquatable<Solution>
    {
        private Run _run;
        private MxSMSA _msa;
        private bool _showEnd;
        private double _endDepth;
        private double _startDepth;
        private int _consecutiveStation;
        private List<double> _sigmaValues;
        private double _previousWpEndDepth;
        private double _icarusStationsCount;
        private MxSAzimuthTypeEnum _aziType;
        private MxSSolutionService _service;
        private MxSSolutionType _solutionType;
        private MxSCazandraSolution _cazandraSolution;
        private MxSMagneticModelType _magneticReference;

        public event EventHandler StartDepthChanged;
        public event EventHandler EndDepthChanged;

        [NotMapped]
        public List<IMxSEngineAction> AsaFailActions { get; set; } = new List<IMxSEngineAction>();

        [NotMapped]
        public List<IMxSEngineAction> AsaPassActions { get; set; } = new List<IMxSEngineAction>();

        [JsonProperty]
        public List<MaxSurveyPreConditionRule> ASAPreconditionRules{ get; set; }
        
        [JsonProperty]
        public List<MaxSurveyRule> ASARules { get; set; }

        //Commented in MxS_Core
        /*  [JsonProperty]
          public StateList<RawSurvey> RawSurveys
          {
              get { return _rawSurveys; }
              protected set
              {
                  _rawSurveys = value;

              }
          }*/


        [JsonProperty]
        public double EndDepth
        {
            get { return _endDepth; }
            set
            {
                if (_endDepth != value)
                {
                    this.DecideBeforeSetEndDepth(value, ref _previousWpEndDepth);
                    _endDepth = value;
                    OnEndDepthChanged(EventArgs.Empty);
                }
            }
        }

        [JsonProperty]
        public DateTime EndTime { get; set; }

        [JsonProperty]
        public MxSInclinationSolutionType InclinationSolution { get; set; }

        [JsonProperty]
        public bool UseSagInc { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsEndDepthValid { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsQCTypeEnabled { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public bool IsSolutionActive { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsSolutionTypeEnabled { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public bool IsStartDepthValid { get; set; }

        [NotMapped]
        [JsonProperty]
        public string MSASummary { get; set; }

        [JsonProperty]
        public MxSNorthReference NorthReference { get; set; }

        [JsonProperty]
        public ObservatoryStation ObservatoryStation { get; set; }

        [JsonProperty]
        public Guid? ObservatoryStationId { get; set; }

        [JsonProperty]
        public List<PlanSurvey> PlanSurveys { get; protected set; }

        [NotMapped]
        [JsonProperty]
        public MxSSurveyPumpStatus AllowedPumpStatus
        {
            get
            {
                return PumpStatusFilter.AllowedPumpStatus();
            }
        }

        [JsonProperty]
        public MxSPumpStatusFilter PumpStatusFilter { get; set; }

        [JsonProperty]
        public MxSQCType QCType { get; set; }
        [NotMapped]
        [JsonProperty]
        public List<MxSQCType> QCTypes { get; set; }

        [NotMapped]
        [JsonProperty]
        public TimeSpan? RigTimeOffsetForBinding
        {
            get
            {
                if (RigTimeOffset != null)
                {
                    return -TimeSpan.FromTicks(RigTimeOffset.Value);
                }

                return null;
            }
            set
            {
                if (RigTimeOffset != null)
                {
                    if (TimeSpan.FromTicks(-RigTimeOffset.Value) != value)
                    {
                        if (value == null)
                        {
                            RigTimeOffset = null;
                        }
                        else
                        {
                            RigTimeOffset = -value.Value.Ticks;
                        }
                    }
                }
                else
                {
                    if (value != null)
                    {
                        RigTimeOffset = -value.Value.Ticks;
                    }
                }
            }
        }

        [JsonProperty]
        public Run Run
        {
            get
            {
                return _run;
            }
            set { SetRun(value); }
        }

        private void SetRun(Run newRun)
        {
            //if the solutions are the same.
            if (_run != null && newRun != null)
            {
                //need to cover the case where we have multiple new solutions.
                if (_run.Id == newRun.Id && (_run.Id != Guid.Empty && newRun.Id != Guid.Empty))
                {
                    return;
                }

            }
            _run = newRun;
        }

        [JsonProperty]
        public bool RunCazandra { get; set; }

        [JsonProperty]
        public bool RunIcarus { get; set; }

        [JsonProperty]
        public Guid RunId { get; set; }

        [JsonProperty]
        public MxSSolutionService Service
        {
            get { return _service; }
            set
            {
                if (_service != value)
                {
                    _service = value;
                    this.SetUseIcarusGZ();
                    this.LoadDynamicQCValuesFromSolutionService();
                }
            }
        }

        [JsonProperty]
        public MxSSolutionType SolutionType
        {
            get { return _solutionType; }
            set
            {
                if (_solutionType != value)
                {
                    _solutionType = value;
                    this.LoadDynamicQCValuesFromSolutionService();
                }
            }
        }

        [NotMapped]
        [JsonProperty]
        public List<MxSSolutionType> SolutionTypes { get; set; }

        [JsonProperty]
        public double StartDepth
        {
            get { return _startDepth; }
            set
            {
                if (_startDepth != value)
                {
                    _startDepth = value;
                    OnStartDepthChanged(EventArgs.Empty);
                }
            }
        }

        [JsonProperty]
        public DateTime StartTime { get; set; }

        [JsonProperty]
        public MxSToolTypeOptions ToolTypeOptions { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        #region End of data

        [NotMapped]
        public bool EndEditable
        {
            get { return !_showEnd; }
        }

        [NotMapped]
        [JsonProperty]
        public bool ShowEnd
        {
            get { return _showEnd; }
            set
            {
                if (_showEnd != value)
                {
                    _showEnd = value;
                    if (_showEnd)
                    {
                        EndDepth = MxSConstant.MaximumDepth;
                    }
                    else
                    {
                        if (_previousWpEndDepth != 0)
                        {
                            EndDepth = _previousWpEndDepth;
                        }
                    }
                }
            }
        }

        #endregion

        #region For MFM

        [JsonProperty]
        public MxSRigMagValueType RigMagValueType { get; set; }

        [JsonProperty]
        public string SelectedWayPoint { get; set; }

        [JsonProperty]
        public double MfmMagneticFieldStrength { get; set; }

        [JsonProperty]
        public double MfmMagneticDipAngle { get; set; }

        [JsonProperty]
        public double MfmDeclination { get; set; }

        [JsonProperty]
        public double MfmGridConvergence { get; set; }

        [JsonProperty]
        public double MfmTotalCorrection { get; set; }

        #endregion

        #region For IFR1

        [JsonProperty]
        public double IfrMagneticFieldStrength { get; set; }

        [JsonProperty]
        public double IfrMagneticDipAngle { get; set; }

        [JsonProperty]
        public double IfrDeclination { get; set; }

        [JsonProperty]
        public double IfrGridConvergence { get; set; }

        [JsonProperty]
        public double IfrTotalCorrection { get; set; }

        #endregion

        #region For IFR2

        [JsonProperty]
        public MxSObservatoryDataType ObsDataType { get; set; }

        [JsonProperty]
        public string MagneticModel { get; set; }

        [JsonProperty]
        public bool MagneticModelChangedFlag
        {
            private get { return true; }
            set
            { }
        }
  
        [JsonProperty]
        public DateTime CalculationDate { get; set; }

        [JsonProperty]
        public bool CalculationDateChangedFlag
        {
            private get { return true; }
            set
            { }
        }

        
        [JsonProperty]
        public double ObsSiteDeclination { get; set; }

        [JsonProperty]
        public double ObsSiteDip { get; set; }

        [JsonProperty]
        public double ObsSiteBTotal { get; set; }

        [JsonProperty]
        public long? RigTimeOffset { get; set; }

        [JsonProperty]
        public bool ForceManualTimeOffset { get; set; }

        #endregion

        #region For Cazandra

        [JsonProperty]
        public MxSCazandraSolution CazandraSolution
        {
            get { return _cazandraSolution; }
            set
            {
                if (_cazandraSolution != value)
                {
                    _cazandraSolution = value;
                    this.LoadDynamicQCValuesFromSolutionService();
                }
            }
        }

        [JsonProperty]
        public MxSCazandraMagneticValsType CazandraMagneticValsType { get; set; }

        [JsonProperty]
        public int ConsecutiveStation
        {
            get { return _consecutiveStation; }
            set
            {
                if (_consecutiveStation != value)
                {
                    if (value < 5)
                    {
                        return;
                    }
                    _consecutiveStation = value;
                }
            }
        }

        [JsonProperty]
        public MxSShortCollarCorrectionQuadrant RawdataQuadrant { get; set; }

        [JsonProperty]
        public MxSShortCollarCorrectionQuadrant TFCQuadrant { get; set; }

        [JsonProperty]
        public MxSShortCollarCorrectionQuadrant TransXYQuadrant { get; set; }

        [JsonProperty]
        public MxSShortCollarCorrectionQuadrant SFEQuadrant { get; set; }

        [JsonProperty]
        public MxSShortCollarCorrectionQuadrant DIYQuadrant { get; set; }

        [JsonProperty]
        public MxSTriacSelectionMode TriacSelectionMode { get; set; }

        [JsonProperty]
        public bool ApplyBGMisalignment { get; set; }

        [JsonProperty]
        public bool ApplyBzOffset { get; set; }

        //todo: from Lijun use comparation before assign value
        [JsonProperty]
        public bool UseIcarusGz { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool UseMFMAsIFR1 { get; set; }

        [JsonProperty]
        public bool IFRData { get; set; }

        [JsonProperty]
        public bool ApplyIcarusCalculations { get; set; }

        [JsonProperty]
        public bool CalculateAndApplyBzMisalignment { get; set; }

        [JsonProperty]
        public double BGMisalignment { get; set; }

        [JsonProperty]
        public double BzOffset { get; set; }

        [JsonProperty]
        public double DBx { get; set; }

        [JsonProperty]
        public double DBy { get; set; }

        [JsonProperty]
        public double DBz { get; set; }

        [JsonProperty]
        public double SFBx { get; set; }

        [JsonProperty]
        public double SFBy { get; set; }

        // Commented in MxS_Core
        /*  public void RemoveCorrectedSurvey(CorrectedSurvey correctedSurvey)
          {
              this.CorrectedSurveys.Remove(correctedSurvey);
              correctedSurvey.Solution = null;
              RaisePropertyChanged(CorrectedSurveysPropertyName);
          }

          public void AddCorrectedSurvey(CorrectedSurvey correctedSurvey)
          {
              //check to see if there is already a corrected survey with the same datetime and depth.
              var existingCorrectedSurveys = FindCorrectedSurveyByDateAndDepth(correctedSurvey.DateTime, correctedSurvey.Depth);
              foreach (var existingCorrectedSurvey in existingCorrectedSurveys)
              {
                  RemoveCorrectedSurvey(existingCorrectedSurvey);
              }

              this.CorrectedSurveys.Add(correctedSurvey);
              correctedSurvey.Solution = this;
              RaisePropertyChanged(CorrectedSurveysPropertyName);
          }*/


        /*  public List<CorrectedSurvey> GetCorrectedSurveys()
          {
              return RawSurveys.Where(x => x.CorrectedSurvey != null && x.State != State.Deleted && x.Enabled == true).Select(x => x.CorrectedSurvey).ToList();
          }

          public List<CorrectedSurvey> FindCorrectedSurveyByDateAndDepth(DateTime date, double depth)
          {
              var rawSurveys = RawSurveys.Where(x => x.DateTime == date && x.Depth.CompareDouble(depth) && x.CorrectedSurvey != null && x.State != State.Deleted);
              var results = rawSurveys.Select(x => x.CorrectedSurvey).ToList();
              return results;
          }*/

        [JsonProperty]
        public double OffsetAngle { get; set; }

        [JsonProperty]
        public double Direction { get; set; }

        #endregion

        #region For Icarus

        [JsonProperty]
        public MxSIcarusSolution IcarusSolution { get; set; }

        [JsonProperty]
        public string IcarusVersion { get; set; }

        [JsonProperty]
        public double IcarusStationsCount
        {
            get { return _icarusStationsCount; }
            set
            {
                if (_icarusStationsCount != value)
                {
                    if (value < 5)
                    {
                        return;
                    }
                    _icarusStationsCount = value;
                }
            }
        }

        [JsonProperty]
        public bool? IcarusToolType { get; set; }

        [JsonProperty]
        public MxSLatitudeType IcarusLatitudeType { get; set; } = MxSLatitudeType.Custom;

        [JsonProperty]
        public double TheoreticGravity { get; set; } = 1;

        [JsonProperty]
        public double IcarusCalibrationLatitude { get; set; }

        [JsonProperty]
        public double IcarusWellLatitude { get; set; }

        [JsonProperty]
        public double IcarusDGx { get; set; }

        [JsonProperty]
        public double IcarusDGy { get; set; }

        [JsonProperty]
        public double IcarusDGz { get; set; }

        [JsonProperty]
        public double IcarusSFGx { get; set; }

        [JsonProperty]
        public double IcarusSFGy { get; set; }

        [JsonProperty]
        public string IcarusIngoredSurveys { get; set; } = string.Empty;

        [JsonProperty]
        public string IcarusExcludedSurveys { get; set; } = string.Empty;

        [JsonProperty]
        public string CazandraIngoredSurveys { get; set; } = string.Empty;

        [JsonProperty]
        public string CazandraExcludedSurveys { get; set; } = string.Empty;

        #endregion

        #region for dynamic QC

        [JsonProperty]
        public bool RunDynamicQC { get; set; }

        [JsonProperty]
        public bool IsAdvancedMode { get; set; }

        [JsonProperty]
        public MxSMagneticModelType MagneticReference
        {
            get { return _magneticReference; }
            set
            {
                if (_magneticReference != value)
                {
                    _magneticReference = value;
                    this.ReadDynamicQCValuesFromXML();
                }
            }
        }

        [JsonProperty]
        public MxSAzimuthTypeEnum AziType
        {
            get { return _aziType; }
            set
            {
                if (_aziType != value)
                {
                    _aziType = value;
                    this.ReadDynamicQCValuesFromXML();
                }
            }
        }

        [JsonProperty]
        public MxSMSA MSA
        {
            get { return _msa; }
            set
            {
                if (_msa != value)
                {
                    _msa = value;
                    if (_msa == MxSMSA.Yes)
                    {
                        _aziType = MxSAzimuthTypeEnum.LongCollar;
                    }
                    this.ReadDynamicQCValuesFromXML();
                }
            }
        }

        /// <summary>
        /// Property to generate IPM Tool Code in format
        /// Azimuth Type + Mag Ref + MSA
        /// </summary>
        [JsonProperty]
        public string IPMToolCode { get; set; }

        [JsonProperty]
        public double? Sigma { get; set; }

        [JsonProperty]
        public double? DGxyz { get; set; }

        [JsonProperty]
        public double? DBxyz { get; set; }

        [JsonProperty]
        public double? DBzMod { get; set; }

        [JsonProperty]
        public double? DDipe { get; set; }

        [JsonProperty]
        public double? DBe { get; set; }

        [JsonProperty]
        public double? DEC { get; set; }

        [JsonProperty]
        public double? DBH { get; set; }

        [JsonProperty]
        public double? DBNoise { get; set; }

        [JsonProperty]
        public double? DDipNoise { get; set; }

        [JsonProperty]
        public double? Sxy { get; set; }

        [JsonProperty]
        public List<double> SigmaValues
        {
            get
            {
                _sigmaValues = new List<double> { 1, 2, 2.58, 3, 4 };
                return _sigmaValues;
            }
        }

        [JsonProperty]
        public MxSDynamicQCModes Mode { get; set; }

        [NotMapped]
        [JsonProperty]
        public double CertainityValue { get; set; }

        #endregion

        #region AziError

        [NotMapped]
        [JsonProperty]
        public string AziErrorIgnoredSurveys { get; set; } = string.Empty;

        [NotMapped]
        [JsonProperty]
        public string AziErrorExcludedSurveys { get; set; } = string.Empty;

        [NotMapped]
        [JsonProperty]
        public double? AziErrorDipe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrorBe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrorDecle { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrordDipe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrordBe { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrordDecle { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrordBz { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrorSxy { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrorBGm { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? AziErrorGridConvergence { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool UseNominalValuesForAziError { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public bool UseMovingAverageForAziError { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public MxSAziErrorSurveySource AziErrorSurveySource { get; set; } = MxSAziErrorSurveySource.Surveys;

        [NotMapped]
        [JsonProperty]
        public MxSAziErrorCazandraSolution AziErrorCazandraSolution { get; set; } = MxSAziErrorCazandraSolution.Raw;

        [NotMapped]
        [JsonProperty]
        public MxSAzimuthTypeEnum AziErrorSelectedAziType { get; set; } = MxSAzimuthTypeEnum.LongCollar;

        [NotMapped]
        [JsonProperty]
        public double? AziErrorDEC { get; set; } = 0.0;

        [NotMapped]
        [JsonProperty]
        public double? AziErrorDBH { get; set; } = 0.0;

        [NotMapped]
        [JsonProperty]
        public double? AziErrorSigma { get; set; } = 2.0;

        #endregion AziError

        // Commented in MxS_Core also
        //Rick; moved external domain logic back into domain object.
        /*  public void AddRawSurvey(RawSurvey rawSurvey)
          {
              rawSurvey.Solution = this;
              RawSurveys.Add(rawSurvey);
              RaisePropertyChanged(RawSurveysPropertyName);
          }

          public void DeleteRawSurvey(RawSurvey rawSurvey)
          {
              if (RawSurveys.Delete(rawSurvey))
              {
                  rawSurvey.Solution = null;
                  RaisePropertyChanged(RawSurveysPropertyName);
              }
          }

          internal void RemoveRawSurvey(RawSurvey rawSurvey)
          {
              RawSurveys.Remove(rawSurvey);
          }

          public void MoveRawSurvey(RawSurvey rawSurvey, Solution newSolution)
          {
              this.RemoveRawSurvey(rawSurvey);
              newSolution.AddRawSurvey(rawSurvey);
          }*/

        public Solution()
        {
            PlanSurveys = new List<PlanSurvey>();
            PumpStatusFilter = MxSPumpStatusFilter.All;
            ASARules = new List<MaxSurveyRule>();
            ASAPreconditionRules = new List<MaxSurveyPreConditionRule>();
            QCTypes = new List<MxSQCType>();
            SolutionTypes = new List<MxSSolutionType>();
            QCType = MxSQCType.Static;
            IsAdvancedMode = false;
            MagneticReference = MxSMagneticModelType.BGGM;
            AziType = MxSAzimuthTypeEnum.ShortCollar;
            MSA = MxSMSA.Yes;
            Sigma = 0.0;
            UseIcarusGz = false;
            ObservatoryStation = null;
            this.FillDefaultDynamicQCParameter();
        }

        public Solution(Solution solutionToCopy, bool includeSurveyData = true) : this()
        {
            AsaPassActions = solutionToCopy.AsaPassActions;
            AsaFailActions = solutionToCopy.AsaFailActions;

            this.CopyRules(solutionToCopy);

            if (includeSurveyData)
            {
                foreach (var planSurvey in solutionToCopy.PlanSurveys)
                {
                    var planSurveyToCopy = new PlanSurvey(planSurvey)
                    {
                        Solution = this
                    };
                    PlanSurveys.Add(planSurveyToCopy);
                }
            }

            CazandraIngoredSurveys = solutionToCopy.CazandraIngoredSurveys;
            CazandraExcludedSurveys = solutionToCopy.CazandraExcludedSurveys;
            IcarusExcludedSurveys = solutionToCopy.IcarusExcludedSurveys;
            IcarusIngoredSurveys = solutionToCopy.IcarusIngoredSurveys;
            EndDepth = solutionToCopy.EndDepth;
            EndTime = solutionToCopy.EndTime;
            InclinationSolution = solutionToCopy.InclinationSolution;
            IsEndDepthValid = solutionToCopy.IsEndDepthValid;
            UseSagInc = solutionToCopy.UseSagInc;
            IsQCTypeEnabled = solutionToCopy.IsQCTypeEnabled;
            IsSolutionActive = solutionToCopy.IsSolutionActive;
            IsSolutionTypeEnabled = solutionToCopy.IsSolutionTypeEnabled;
            IsStartDepthValid = solutionToCopy.IsStartDepthValid;
            MSASummary = solutionToCopy.MSASummary;
            NorthReference = solutionToCopy.NorthReference;
            ObservatoryStation = solutionToCopy.ObservatoryStation;
            MSA = solutionToCopy.MSA;
            PumpStatusFilter = solutionToCopy.PumpStatusFilter;
            QCType = solutionToCopy.QCType;
            QCTypes = solutionToCopy.QCTypes;
            Service = solutionToCopy.Service;
            SolutionType = solutionToCopy.SolutionType;
            SolutionTypes = solutionToCopy.SolutionTypes;
            StartDepth = solutionToCopy.StartDepth;
            StartTime = solutionToCopy.StartTime;
            ToolTypeOptions = solutionToCopy.ToolTypeOptions;
            ShowEnd = solutionToCopy.ShowEnd;
            RigTimeOffset = solutionToCopy.RigTimeOffset;
            MagneticModel = solutionToCopy.MagneticModel;

            IcarusToolType = solutionToCopy.IcarusToolType;
            AziType = solutionToCopy.AziType;
            IPMToolCode = solutionToCopy.IPMToolCode;
            DBxyz = solutionToCopy.DBxyz;
            DBzMod = solutionToCopy.DBzMod;
            DDipe = solutionToCopy.DDipe;
            DBe = solutionToCopy.DBe;
            DEC = solutionToCopy.DEC;
            DBH = solutionToCopy.DBH;
            CalculationDate = solutionToCopy.CalculationDate;
            MagneticReference = solutionToCopy.MagneticReference;
            CazandraSolution = solutionToCopy.CazandraSolution;
            DBx = solutionToCopy.DBx;
            DBy = solutionToCopy.DBy;
            DBz = solutionToCopy.DBz;
            SFBx = solutionToCopy.SFBx;
            SFBy = solutionToCopy.SFBy;
            IcarusSolution = solutionToCopy.IcarusSolution;
            IcarusWellLatitude = solutionToCopy.IcarusWellLatitude;
            CertainityValue = solutionToCopy.CertainityValue;
            IcarusDGx = solutionToCopy.IcarusDGx;
            IcarusDGy = solutionToCopy.IcarusDGy;
            IcarusDGz = solutionToCopy.IcarusDGz;
            IcarusSFGx = solutionToCopy.IcarusSFGx;
            IcarusSFGy = solutionToCopy.IcarusSFGy;

            ObsSiteBTotal = solutionToCopy.ObsSiteBTotal;
            ObsDataType = solutionToCopy.ObsDataType;
            ObsSiteDeclination = solutionToCopy.ObsSiteDeclination;
            ObsSiteDip = solutionToCopy.ObsSiteDip;

            ConsecutiveStation = solutionToCopy.ConsecutiveStation;
            TheoreticGravity = solutionToCopy.TheoreticGravity;
            IcarusStationsCount = solutionToCopy.IcarusStationsCount;

            ForceManualTimeOffset = solutionToCopy.ForceManualTimeOffset;
            ApplyBGMisalignment = solutionToCopy.ApplyBGMisalignment;
            BGMisalignment = solutionToCopy.BGMisalignment;
            ApplyBzOffset = solutionToCopy.ApplyBzOffset;
            BzOffset = solutionToCopy.BzOffset;
            UseIcarusGz = solutionToCopy.UseIcarusGz;
            RawdataQuadrant = solutionToCopy.RawdataQuadrant;
            TFCQuadrant = solutionToCopy.TFCQuadrant;
            TransXYQuadrant = solutionToCopy.TransXYQuadrant;
            SFEQuadrant = solutionToCopy.SFEQuadrant;
            DIYQuadrant = solutionToCopy.DIYQuadrant;
        }
        
        public bool Equals(Solution other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Equals(obj as Solution);
        }

        public void OnEndDepthChanged(EventArgs e)
        {
            EventHandler handler = EndDepthChanged;
            if (handler != null)
            {
                handler(this , e);
            }
        }

        public void OnStartDepthChanged(EventArgs e)
        {
            EventHandler handler = StartDepthChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
