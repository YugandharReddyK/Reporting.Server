using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Run : DataModelBase
    {
        private string _runNumber = string.Empty;
        private bool _showEnd;

        //TODO: Suhail - changed accessModifier private to public to access in extensions
        public SolutionRawsurveyManager solutionRawsurveyManager;

        public Run()
        {
            Solutions = new List<Solution>();
            RawSurveys = new List<RawSurvey>();
            ShortSurveys = new List<ShortSurvey>();
            ToolType = MxSToolTypeOptions.DM;
            IsPending = false;
            EndTime = new DateTime(1970, 1, 1);
            PrimaryOrSecondary = MxSPrimaryOrSecondary.Primary;
            SagCorrectionLevel = MxSSagCorrectionLevel.None;
            solutionRawsurveyManager = new SolutionRawsurveyManager(this);
        }

        public Run(Run runToCopy, bool isCopyOnlyBaseProperties = false) : this()
        {
            UpdateValues(runToCopy);

            if (!isCopyOnlyBaseProperties)
            {
                foreach (var rawSurvey in runToCopy.RawSurveys)
                {
                    RawSurvey rawSurveyTocopy = new RawSurvey(rawSurvey);
                    this.AddRawSurvey(rawSurveyTocopy);
                }
                foreach (var solution in runToCopy.Solutions)
                {
                    Solution solutionTocopy = new Solution(solution);
                    this.AddSolution(solutionTocopy);
                }
                foreach (var shortSurvey in runToCopy.ShortSurveys)
                {
                    ShortSurvey rawSurveyTocopy = new ShortSurvey(shortSurvey);
                    this.AddShortSurvey(rawSurveyTocopy);
                }
            }

        }

        [JsonProperty]
        public double AzimuthLongCollarTolerance { get; set; } = 0.01;

        [JsonProperty]
        public double DistanceToBit { get; set; }

        [JsonProperty]
        public double EndDepth { get; set; }

        [JsonProperty]
        public DateTime EndTime { get; set; }

        [JsonProperty]
        public double InclinationTolerance { get; set; } = 0.01;

        [JsonProperty]
        public bool IsPending { get; set; }

        [JsonProperty]
        public double NonMagAboveSensor { get; set; }

        [JsonProperty]
        public double NonMagBelowSensor { get; set; }

        [JsonProperty]
        public MxSPrimaryOrSecondary PrimaryOrSecondary { get; set; }

        [JsonProperty]
        public string RunNumber
        {
            get { return _runNumber; }
            set
            {
                if (_runNumber != value)
                {
                    _runNumber = this.FormatRunNumber(value);
                }
            }
        }

        [JsonProperty]
        public string SerialNumber { get; set; } = string.Empty;

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
                }
            }
        }

        [NotMapped]
        public TimeSpan? AverageReturnTimeForRun
        {
            get { return this.GetAverageTimeSpan(); }
        }

        [JsonProperty]
        public List<Solution> Solutions { get; protected set; }

        [JsonProperty]
        public double StartDepth { get; set; }

        [JsonProperty]
        public DateTime StartTime { get; set; }

        [JsonProperty]
        public string SurveyDescription { get; set; }

        [JsonProperty]
        public double? ToolfaceOffset { get; set; }

        [JsonProperty]
        public MxSToolTypeOptions ToolType { get; set; }

        // TODO: Suhail - SelfReference loop 
        [JsonProperty]
        public Well Well { get; set; }

        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public MxSSagCorrectionLevel SagCorrectionLevel { get; set; }

        [JsonProperty]
        public string Comments { get; set; } = string.Empty;

        [JsonProperty]
        public List<RawSurvey> RawSurveys { get; protected set; }

        [JsonProperty]
        public List<ShortSurvey> ShortSurveys { get; protected set; }

        ~Run()
        { }

        public void UpdateValues(Run runToCopy)
        {
            AzimuthLongCollarTolerance = runToCopy.AzimuthLongCollarTolerance;
            DistanceToBit = runToCopy.DistanceToBit;
            EndDepth = runToCopy.EndDepth;
            EndTime = runToCopy.EndTime;
            InclinationTolerance = runToCopy.InclinationTolerance;
            IsPending = runToCopy.IsPending;
            NonMagAboveSensor = runToCopy.NonMagAboveSensor;
            NonMagBelowSensor = runToCopy.NonMagAboveSensor;
            PrimaryOrSecondary = runToCopy.PrimaryOrSecondary;
            SagCorrectionLevel = runToCopy.SagCorrectionLevel;
            _runNumber = runToCopy.RunNumber;
            SerialNumber = runToCopy.SerialNumber;
            _showEnd = runToCopy.ShowEnd;
            StartDepth = runToCopy.StartDepth;
            StartTime = runToCopy.StartTime;
            ToolfaceOffset = runToCopy.ToolfaceOffset;
            ToolType = runToCopy.ToolType;
            Comments = runToCopy.Comments;
            NonMagAboveSensor = runToCopy.NonMagAboveSensor;
            NonMagBelowSensor = runToCopy.NonMagBelowSensor;
            SurveyDescription = runToCopy.SurveyDescription;
        }

        public new void Dispose()
        {
            if (ShortSurveys != null)
            {
                foreach (var shortSurvey in ShortSurveys)
                {
                    shortSurvey.Dispose();
                }
            }
            if (RawSurveys != null)
            {
                foreach (var rawSurvey in RawSurveys)
                {

                    rawSurvey.Dispose();
                }
            }
        }
    }
}
