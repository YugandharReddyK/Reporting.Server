using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Sperry.MxS.Core.Common.MathematicalFunctions;
using Sperry.MxS.Core.Common.Models.Security;
using Sperry.MxS.Core.Common.Models.Odisseus;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Well : DataModelBase
    {
        private DateTime _magneticCalcDate = DateTime.Now;
        private int _filterIndex;
        private List<string> _runsFilters;
        private string _selectedRunFilter;
        private double _gTotal = 1;
        private double? _geodeticScaleFactorValue;
        private double? _previousScaleValue;
        private bool _overrideGeodeticScaleFactor;
        private double _elevation;
        private double _easting;
        private double _latitude;
        private double _longitude;
        private string _magneticModel;
        private double _northing;
        private string _lockedBy;

        public Well()
        {
            Runs = new List<Run>();
            Waypoints = new List<Waypoint>();
            OdisseusToolCodeSections = new List<OdisseusToolCodeSection>();
            // TODO: Suhail - NotRequired
            //RangingDeterminations = new List<RangingDetermination>();
            Observatories = new Dictionary<Guid, IList<ObservatoryReading>>();
            _runsFilters = new List<string>();
            ImportTemplateId = Guid.Empty;
            ExportTemplateId = Guid.Empty;
            ASAMode = MxSASAModeEnum.OFF;
            ASASessionCount = 1;
        }

        public Well(Well wellToCopy, bool isCopyOnlyBaseProperties = false) : this()
        {
            APINumber = wellToCopy.APINumber;
            BtDipQcLimit = wellToCopy.BtDipQcLimit;
            BTotal = wellToCopy.BTotal;
            BtDipQcLimit = wellToCopy.BtDipQcLimit;
            CalculatedTimeOffset = wellToCopy.CalculatedTimeOffset;
            CartesianCordsUnitOption = wellToCopy.CartesianCordsUnitOption;
            Company = wellToCopy.Company;
            CoordinateSystem = wellToCopy.CoordinateSystem;
            Country = wellToCopy.Country;
            Customer = wellToCopy.Customer;
            Declination = wellToCopy.Declination;
            Dip = wellToCopy.Dip;
            DipQcLimit = wellToCopy.DipQcLimit;
            District = wellToCopy.District;
            _easting = wellToCopy.Easting;
            _elevation = wellToCopy.Elevation;
            Errors = wellToCopy.Errors;
            FieldName = wellToCopy.FieldName;
            _filterIndex = wellToCopy.FilterIndex;
            GridConvergence = wellToCopy.GridConvergence;
            GtQcLimit = wellToCopy.GtQcLimit;
            HighAzimuthQcLimit = wellToCopy.HighAzimuthQcLimit;
            InsiteWellId = string.Empty;
            IpAddress = wellToCopy.IpAddress;
            ImportADIServer = wellToCopy.ImportADIServer;
            IsEnableStaticQc = wellToCopy.IsEnableStaticQc;
            JobNumber = wellToCopy.JobNumber;
            LastSurveyTaken = wellToCopy.LastSurveyTaken;
            _latitude = wellToCopy.Latitude;
            Locked = wellToCopy.Locked;
            _lockedBy = wellToCopy.LockedBy;
            _longitude = wellToCopy.Longitude;
            WgsLongitude = wellToCopy.WgsLongitude;
            WgsLatitude = wellToCopy.WgsLatitude;
            LowAzimuthQcLimit = wellToCopy.LowAzimuthQcLimit;
            MagDataSource = wellToCopy.MagDataSource;
            MagDip = wellToCopy.MagDip;
            MagFieldStrength = wellToCopy.MagFieldStrength;
            _magneticCalcDate = wellToCopy.MagneticCalcDate;
            _magneticModel = wellToCopy.MagneticModel;
            Name = wellToCopy.Name;
            _northing = wellToCopy.Northing;
            NorthReference = wellToCopy.NorthReference;
            Observatories = wellToCopy.Observatories;
            OriginalInsiteWellId = wellToCopy.OriginalInsiteWellId;
            ProjectionCentralMeridian = wellToCopy.ProjectionCentralMeridian;
            ProjectionDescription = wellToCopy.ProjectionDescription;
            ProjectionGridSystem = wellToCopy.ProjectionGridSystem;
            GeodeticScaleFactorEnum = wellToCopy.GeodeticScaleFactorEnum;
            _geodeticScaleFactorValue = wellToCopy.GeodeticScaleFactorValue;
            _overrideGeodeticScaleFactor = wellToCopy.OverrideGeodeticScaleFactor;
            ProjectionGroup = wellToCopy.ProjectionGroup;
            ProjectionMethod = wellToCopy.ProjectionMethod;
            ProjectionReference = wellToCopy.ProjectionReference;
            Region = wellToCopy.Region;
            ResetTime = wellToCopy.ResetTime;
            Rig = wellToCopy.Rig;
            ROC = wellToCopy.ROC;
            RunsFilters = wellToCopy.RunsFilters;
            _selectedRunFilter = wellToCopy.SelectedRunFilter;
            Status = wellToCopy.Status;
            TimeOffset = wellToCopy.TimeOffset;
            TimeOffsetInterval = wellToCopy.TimeOffsetInterval;
            TimeZone = wellToCopy.TimeZone;
            UnitSet = wellToCopy.UnitSet;
            UnitSystem = wellToCopy.UnitSystem;
            UOM = wellToCopy.UOM;
            WellType = wellToCopy.WellType;
            ExportTemplateId = wellToCopy.ExportTemplateId;
            ImportTemplateId = wellToCopy.ImportTemplateId;
            SelectedLocalImportTemplate = wellToCopy.SelectedLocalImportTemplate;
            SelectedLocalExportTemplate = wellToCopy.SelectedLocalExportTemplate;
            SelectedSurveyCategory = wellToCopy.SelectedSurveyCategory;
            SelectedRawSurveyCategory = wellToCopy.SelectedRawSurveyCategory;
            IsDefinitiveOnly = wellToCopy.IsDefinitiveOnly;
            Comments = wellToCopy.Comments;
            Inclination = wellToCopy.Inclination;
            Azimuth = wellToCopy.Azimuth;
            BtQcLimit = wellToCopy.BtQcLimit;

            RigStaleNotification = wellToCopy.RigStaleNotification;
            MFMIFR1ValidationFailedErrorMessage = wellToCopy.MFMIFR1ValidationFailedErrorMessage;
            IsMFMIFR1ValidationFailed = wellToCopy.IsMFMIFR1ValidationFailed;
            LastSurveyDepth = wellToCopy.LastSurveyDepth;
            SinceLastSurveyTime = wellToCopy.SinceLastSurveyTime;
            ProcessingStatus = wellToCopy.ProcessingStatus;
            NewSurveysCount = wellToCopy.NewSurveysCount;
            MagneticValuesNeedsUpdating = wellToCopy.MagneticValuesNeedsUpdating;

            MagneticFieldStrength = wellToCopy.MagneticFieldStrength;
            MagneticDipAngle = wellToCopy.MagneticDipAngle;
            MagneticDeclination = wellToCopy.MagneticDeclination;
            ASAStatusEnum = wellToCopy.ASAStatusEnum;
            ASAMode = wellToCopy.ASAMode;
            ASAPendingSessionCount = wellToCopy.ASAPendingSessionCount;
            ASASessionCount = wellToCopy.ASASessionCount;

            WaitingObservatoryNotification = wellToCopy.WaitingObservatoryNotification;
            NSDeparture = wellToCopy.NSDeparture;
            EWDeparture = wellToCopy.EWDeparture;
            TVD = wellToCopy.TVD;
            MeasuredDepth = wellToCopy.MeasuredDepth;

            Toolface = wellToCopy.Toolface;
            RigType = wellToCopy.RigType;
            SigmaValue = wellToCopy.SigmaValue;
            _gTotal = wellToCopy.GTotal;

            IsForceUpdateHypercubeBGS = wellToCopy.IsForceUpdateHypercubeBGS;
            AutoCalcPosition = wellToCopy.AutoCalcPosition;
            Archived = false;

            if (!isCopyOnlyBaseProperties)
            {
                foreach (var waypoint in wellToCopy.Waypoints)
                {
                    if (waypoint.State != MxSState.Deleted)
                    {
                        var wayPointToCopy = new Waypoint(waypoint);
                        wayPointToCopy.Well = this;
                        Waypoints.Add(wayPointToCopy);
                    }
                }
                foreach (var odisseusToolCodeSection in wellToCopy.OdisseusToolCodeSections)
                {
                    if (odisseusToolCodeSection.State != MxSState.Deleted)
                    {
                        var odisseusToolCodeSectionToCopy = new OdisseusToolCodeSection(odisseusToolCodeSection);
                        odisseusToolCodeSectionToCopy.Well = this;
                        OdisseusToolCodeSections.Add(odisseusToolCodeSectionToCopy);
                    }
                }
                foreach (var run in wellToCopy.Runs)
                {
                    if (run.State != MxSState.Deleted)
                    {
                        var runTocopy = new Run(run);
                        runTocopy.Well = this;
                        Runs.Add(runTocopy);
                    }
                }
            }
        }

        [NotMapped]
        public List<CorrectedSurvey> AllCorrectedSurveys => Runs.SelectMany(r => r.GetCorrectedSurveys()).ToList();

        [NotMapped]
        public List<ShortSurvey> AllShortSurveys => Runs.SelectMany(r => r.GetAllShortSurveys()).ToList();

        [NotMapped]
        public List<PlanSurvey> AllPlanSurveys
        {
            get
            {
                List<PlanSurvey> planSurveys = new List<PlanSurvey>();
                foreach (Solution solution in AllSolutions)
                {
                    planSurveys.AddRange(solution.PlanSurveys);
                }
                return planSurveys;
            }
        }

        [NotMapped]
        public List<RawSurvey> AllRawSurveys
        {
            get
            {
                var tempRawSurveyCollection = new List<RawSurvey>();

                foreach (Run run in Runs)
                {
                    tempRawSurveyCollection.AddRange(run.GetAllRawSurveys());
                }
                List<RawSurvey> rawSurveys =
                    new List<RawSurvey>(tempRawSurveyCollection.OrderBy(a => a.Depth).ThenBy(a => a.DateTime));
                return rawSurveys;
            }
        }

        [NotMapped]
        public List<Solution> AllSolutions
        {
            get
            {
                List<Solution> solutions = new List<Solution>();
                foreach (Run run in Runs)
                {
                    solutions.AddRange(run.Solutions);
                }
                solutions.RemoveAll(s => s.State == MxSState.Deleted);
                return solutions;
            }
        }

        #region AllSurveyTypes
        //[NotMapped]
        //public ObservableCollection<CorrectedSurvey> AllCorrectedSurveys
        //{
        //    get
        //    {
        //        ObservableCollection<CorrectedSurvey> result = new ObservableCollection<CorrectedSurvey>();
        //        for (int i = 0; i < Runs.Count; i++)
        //        {
        //            result.AddRange(Runs[i].GetCorrectedSurveys());
        //        }
        //        return result;
        //    }
        //}

        //[NotMapped]

        //public ObservableCollection<ShortSurvey> AllShortSurveys
        //{
        //    get
        //    {
        //        ObservableCollection<ShortSurvey> result = new ObservableCollection<ShortSurvey>();
        //        foreach (Run run in Runs)
        //        {
        //            result.AddRange(run.GetAllShortSurveys());
        //        }
        //        return result;
        //    }
        //}
        #endregion

        [JsonProperty]
        public string APINumber { get; set; }

        //TODO: Suhail Find alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public double BtDipQcLimit { get; set; } = 300;

        [JsonProperty]
        public double BTotal { get; set; }

        //TODO: Suhail Find alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public double BtQcLimit { get; set; } = 225;

        [JsonProperty]
        public DateTime CalculatedTimeOffset { get; set; }

        [JsonProperty]
        public MxSCartesianCordsUnitOptions CartesianCordsUnitOption { get; set; }

        [JsonProperty]
        public string Company { get; set; }

        [JsonProperty]
        public string CoordinateSystem { get; set; }

        [JsonProperty]
        public string Country { get; set; }

        [JsonProperty]
        public string Customer { get; set; }

        [JsonProperty]
        public double Declination { get; set; }

        [JsonProperty]
        public double Dip { get; set; }

        //TODO: Find alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public double DipQcLimit { get; set; } = 0.25;

        [JsonProperty]
        public string District { get; set; }

        [JsonProperty]
        public double Easting
        {
            get { return _easting; }
            set
            {
                if (_easting != value)
                {
                    _easting = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        [JsonProperty]
        public double Elevation
        {
            get { return _elevation; }
            set
            {
                if (_elevation != value)
                {
                    _elevation = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        [NotMapped]
        [JsonProperty]
        public string Errors { get; set; }

        [JsonProperty]
        public string FieldName { get; set; }

        [NotMapped]
        [JsonProperty]
        public int FilterIndex
        {
            get { return _filterIndex; }
            set
            {
                if (_filterIndex != value)
                {
                    _filterIndex = value < 0 ? 0 : value;

                    if (_runsFilters != null && _runsFilters.Count > _filterIndex)
                    {
                        _selectedRunFilter = _runsFilters[_filterIndex];
                    }
                }
            }
        }

        [JsonProperty]
        public double GridConvergence { get; set; }

        //TODO: Find Alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public double GtQcLimit { get; set; } = 0.003;

        //TODO: Find Alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public double HighAzimuthQcLimit { get; set; } = 0.02;

        [JsonProperty]
        public string InsiteWellId { get; set; }

        [JsonProperty]
        public string IpAddress { get; set; }

        [JsonProperty]
        public string ImportADIServer { get; set; } = MxSConstants.DefaultImportADIServer;

        //TODO: Find Alternate
        //[IgnoreImportProperty]
        [JsonProperty]
        public bool IsEnableStaticQc { get; set; } = true;

        [JsonProperty]
        public string JobNumber { get; set; }

        [NotMapped]
        [JsonProperty]
        public DateTime LastSurveyTaken { get; set; }

        [JsonProperty]
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    MagneticValuesNeedsUpdating = true;                  
                }
            }
        }

        [JsonProperty]
        public bool Locked { get; set; }

        [JsonProperty]
        public bool Archived { get; set; } = false;

        [JsonProperty]
        public string LockedBy
        {
            get { return _lockedBy; }
            set
            {
                if (value != null)
                {
                    var lockedBy = value.ToLower();
                    if (_lockedBy != lockedBy)
                    {
                        _lockedBy = lockedBy;                      
                    }
                }
            }
        }

        [JsonProperty]
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        [JsonProperty]
        public double? WgsLongitude { get; set; }

        [JsonProperty]
        public double? WgsLatitude { get; set; }

        //[IgnoreImportProperty]
        [JsonProperty]
        public double LowAzimuthQcLimit { get; set; } = 0.01;

        [JsonProperty]
        public string MagDataSource { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MagDip { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MagFieldStrength { get; set; }

        [JsonProperty]
        public DateTime MagneticCalcDate
        {
            get { return _magneticCalcDate; }
            set
            {
                if (_magneticCalcDate != value)
                {
                    _magneticCalcDate = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        // TODO: Commented in MxS Core Also
     //   [NotMapped]
        [JsonProperty]
        public string MagneticModel
        {
            get { return _magneticModel; }
            set
            {
                if (_magneticModel != value)
                {
                    _magneticModel = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public double Northing
        {
            get { return _northing; }
            set
            {
                if (_northing != value)
                {
                    _northing = value;
                    MagneticValuesNeedsUpdating = true;
                }
            }
        }

        [JsonProperty]
        public MxSNorthReference NorthReference { get; set; }

        //use this property to store related observatory data for IFR2
        [NotMapped]
        public IDictionary<Guid, IList<ObservatoryReading>> Observatories { get; protected set; }

        [JsonProperty]
        public string OriginalInsiteWellId { get; set; }

        [JsonProperty]
        public double ProjectionCentralMeridian { get; set; }

        [JsonProperty]
        public string ProjectionDescription { get; set; }

        [JsonProperty]
        public string ProjectionGridSystem { get; set; }

        [JsonProperty]
        public MxSGeodeticScaleFactorEnum GeodeticScaleFactorEnum { get; set; } = MxSGeodeticScaleFactorEnum.No;

        [JsonProperty]
        public double? GeodeticScaleFactorValue
        {
            get { return _geodeticScaleFactorValue; }
            set
            {
                if (_geodeticScaleFactorValue != value)
                {
                    _geodeticScaleFactorValue = value;
                    if (!_previousScaleValue.HasValue)
                        _previousScaleValue = value;
                }
            }
        }

        [JsonProperty]
        public bool OverrideGeodeticScaleFactor
        {
            get { return _overrideGeodeticScaleFactor; }
            set
            {
                if (_overrideGeodeticScaleFactor != value)
                {
                    _overrideGeodeticScaleFactor = value;
                    // TODO: Suhail - changed
                    //UpdateScaleFactorValue();
                    this.UpdateScaleFactorValue(_previousScaleValue);
                }
            }
        }

        [JsonProperty]
        public string ProjectionGroup { get; set; }

        [JsonProperty]
        public string ProjectionMethod { get; set; }

        [JsonProperty]
        public string ProjectionReference { get; set; }

        // TODO: Suhail - NotRequired
        //[JsonProperty]
        //public List<RangingDetermination> RangingDeterminations { get; set; }

        [JsonProperty]
        public string Region { get; set; }

        [JsonProperty]
        public DateTime ResetTime { get; set; }

        [JsonProperty]
        public string Rig { get; set; }

        [JsonProperty]
        public MxSROCOptions ROC { get; set; }

        [JsonProperty]
        public List<Run> Runs { get; protected set; }

        [NotMapped]
        [JsonProperty]
        public List<string> RunsFilters
        {
            get { return _runsFilters; }
            set
            {
                if (_runsFilters != value)
                {
                    var tempFilters = new List<string> { MxSConstants.AllRunsFilter };
                    if (value != null)
                    {
                        tempFilters.AddRange(value);
                    }
                    _runsFilters = tempFilters;
                }
            }
        }

        //[ClientSettings]
        [NotMapped]
        [JsonProperty]
        public string SelectedRunFilter { get; set; }

        //[ClientSettings]
        [NotMapped]
        [JsonProperty]
        public string SelectedLocalImportTemplate { get; set; }

        //[ClientSettings]
        [NotMapped]
        [JsonProperty]
        public string SelectedLocalExportTemplate { get; set; }

        //[ClientSettings]
        [NotMapped]
        public bool IsDefinitiveOnly { get; set; }

        //[ClientSettings]
        [NotMapped]
        public string SelectedSurveyCategory { get; set; }

        //[ClientSettings]
        [NotMapped]
        public string SelectedRawSurveyCategory { get; set; }

        [JsonProperty]
        public MxSWellStatus Status { get; set; }

        [NotMapped]
        [JsonProperty]
        public TimeSpan TimeOffset { get; set; }

        [JsonProperty]
        public double TimeOffsetInterval { get; set; }

        [JsonProperty]
        public string TimeZone { get; set; }

        [JsonProperty]
        [NotMapped]
        public string UnitSet { get; set; }

        [JsonProperty]
        [NotMapped]
        public string CartesianCordUnitSet { get; set; } = string.Empty;

        [JsonProperty]
        public MxSUnitSystemEnum UnitSystem { get; set; }

        [JsonProperty]
        public string UOM { get; set; } //Make enum, Not string  -Hank 20131118 
       
        [JsonProperty]
        [NotMapped]
        public bool SkipPreview { get; set; } = false;

        [JsonProperty]
        public List<Waypoint> Waypoints { get; protected set; }

        [JsonProperty]
        public List<OdisseusToolCodeSection> OdisseusToolCodeSections { get; protected set; }

        [JsonProperty]
        public MxSWellType WellType { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public string Comments { get; set; } = string.Empty;

        [NotMapped]
        public TimeSpan? AverageReturnTimeForWell
        {
            get { return this.GetAverageTimeSpanForWell(); }
        }

        #region Tie-In

        [JsonProperty]
        public double MeasuredDepth { get; set; }

        [JsonProperty]
        public string Inclination { get; set; }

        [JsonProperty]
        public string Azimuth { get; set; }

        [JsonProperty]
        public double TVD { get; set; }

        [JsonProperty]
        public double NSDeparture { get; set; }

        [JsonProperty]
        public double EWDeparture { get; set; }

        #endregion

        #region client only properties

        [NotMapped]
        public bool IsPinnnedInDashBoard { get; set; }

        [NotMapped]
        public bool IsRecentlyUpdated
        {
            get
            {
                return (LastEditedTime > DateTime.Now.AddHours(-24));
            }
        }

        [JsonProperty]
        public Guid ImportTemplateId { get; set; }

        [JsonProperty]
        public Guid ExportTemplateId { get; set; }

        [JsonProperty]
        public List<AppUser> MappedAppUsers { get; set; }

        [JsonProperty]
        public MxSASAModeEnum ASAMode { get; set; } = MxSASAModeEnum.OFF;

        // TODO: Get the default value from the server. This will be handled as part of a different PBI
        [JsonProperty]
        public int ASASessionCount { get; set; } = 1;

        [JsonProperty]
        public int ASAPendingSessionCount { get; set; } = 0;

        [JsonProperty]
        public MxSASAStatusEnum ASAStatusEnum { get; set; } = MxSASAStatusEnum.NA;

        // this property to solve the following issue
        // a well pined and edited by the current user, and that well also changed by realtime service, 
        // there should be a mismatch between the edit one and broadcast one, user should merge them by himself
        [NotMapped]
        [JsonProperty]
        public bool MergeRequired { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool StateChanged { get; set; }

        [NotMapped]
        [JsonProperty]
        public string PendingRunNotification { get; set; } = string.Empty;

        [NotMapped]
        [JsonProperty]
        public string WaitingObservatoryNotification { get; set; } = string.Empty;

        [NotMapped]
        [JsonProperty]
        public string RigStaleNotification { get; set; }

        [JsonProperty]
        public bool IsForceUpdateHypercubeBGS { get; set; } = false;

        [JsonProperty]
        public bool AutoCalcPosition { get; set; } = true;

        [NotMapped]
        [JsonProperty]
        public bool IsMFMIFR1ValidationFailed { get; set; } = false;

        [NotMapped]
        [JsonProperty]
        public string MFMIFR1ValidationFailedErrorMessage { get; set; } = "";

        [NotMapped]
        [JsonProperty]
        public string LastSurveyDepth { get; set; }

        [NotMapped]
        [JsonProperty]
        public string SinceLastSurveyTime { get; set; }

        [NotMapped]
        [JsonProperty]
        public string ProcessingStatus { get; set; }

        [NotMapped]
        [JsonProperty]
        public string NewSurveysCount { get; set; } = string.Empty;

        [NotMapped]
        public bool IsPendingReview
        {
            get
            {
                if (Runs == null)
                {
                    return false;
                }
                return Runs.Exists(run => run.IsPending) || Status == MxSWellStatus.PendingReview;
            }
        }

        #endregion

        //Changes to support performance improvement when switching between wells.
        /*Hack:
            since the actual calulation is performed by a service, and the well object cant reference the service
            there needs to be some way the well object can indicate the mag values need to be recalculated.
            Add a readonly property on the well object which will indicate the mag values need to be recalculated.
            When a property which would cause the mag values to recalc is changed, then set the flag to true.
            properties which trigger re-calc
                MagneticModel
                Elevation
                Northing
                Easting
                Latitude
                Longitude
                ProjectionGridSystem

            Add a function on the well to set the calculated properties
           when the function is called then reset the flag to false.
         */

        /// <summary>
        /// used as a flag to indicate a property changed which affects the mag calculations.
        /// </summary>
        /// <summary>
        /// used as a flag to indicate a property changed which affects the mag calculations.
        /// </summary>
        [NotMapped]
        public bool MagneticValuesNeedsUpdating { get; set; } = false;

        [NotMapped]
        //[ClientSettings]
        public double MagneticFieldStrength { get; set; } = 0.0;

        [NotMapped]
        //[ClientSettings]
        public double MagneticDipAngle { get; set; } = 0.0;

        [NotMapped]
        //[ClientSettings]
        public double MagneticDeclination { get; set; } = 0.0;

        [JsonProperty]
        public MxSToolface Toolface { get; set; } = MxSToolface.TFIndependent;

        [JsonProperty]
        public MxSRigType RigType { get; set; } = MxSRigType.Float;

        [JsonProperty]
        public double SigmaValue { get; set; } = 2;

        [JsonProperty]
        public double GTotal
        {
            get { return _gTotal; }
            set
            {
                if (value < MxSConstant.GTotalMin || value > MxSConstant.GTotalMax)
                    return;

                if (value != _gTotal)
                {
                    _gTotal = value;
                }
            }
        }

        [NotMapped]
        public double TotalCorrection
        {
            get
            {
                return CommonCalculation.CalculateTotalCorrection(Declination, GridConvergence, NorthReference);
            }
        }
        [NotMapped]
        [JsonProperty]
        public string ImportTemplateName { get; set; }

        [NotMapped]
        [JsonProperty]
        public string ExportTemplateName { get; set; }

        public new void Dispose()
        {
            if (Runs != null)
            {
                int runsCount = Runs.Count;
                for (int i = 0; i < runsCount; i++)
                {
                    for (int j = 0; j < Runs[i].RawSurveys.Count; j++)
                    {
                        Runs[i].RawSurveys[j]?.CorrectedSurvey?.Dispose();
                        //Runs[i].RawSurveys[j]?.CorrectedSurvey?.Delete();
                        Runs[i].RawSurveys[j]?.Dispose();
                    }
                    //Runs[i]?.RawSurveys?.Delete();
                    Runs[i]?.Dispose();
                }
                // TODO: Suhail - Related to state
                //Runs.Delete();
            }
            base.Dispose();
        }
    }
}
