using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Interfaces;
using Sperry.MxS.Core.Common.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class RawSurvey : DataModelBase, IEquatable<RawSurvey>, IMxSTimeOffSet, IMxSDateTimeDepth
    {
        private CorrectedSurvey _correctedSurvey;
        private DateTime _dateTime;
        private Run _run;
        private Solution _solution;

        public RawSurvey()
        {
            //initialize the SolutionId to empty guid, so we have a default value, the SolutionId field can not be null in the db.
            SolutionId = Guid.Empty;
        }

        public RawSurvey(RawSurvey rawSurveyToCopy) : this()
        {
            this.UpdateValues(rawSurveyToCopy);
            //this.UpdateValues(rawSurveyToCopy);
            InsiteAzimAmb1 = rawSurveyToCopy.InsiteAzimAmb1;
            InsiteAzimAmb2 = rawSurveyToCopy.InsiteAzimAmb2;
            InsiteAzimStat = rawSurveyToCopy.InsiteAzimStat;
            InsiteAzimType = rawSurveyToCopy.InsiteAzimType;

            if (rawSurveyToCopy.CorrectedSurvey != null && rawSurveyToCopy.CorrectedSurvey.State != MxSState.Deleted)
            {
                _correctedSurvey = new CorrectedSurvey(rawSurveyToCopy.CorrectedSurvey);
                _correctedSurvey.RawSurvey = this;
                this.UpdateSolution();
            }
        }

        public RawSurvey(ShortSurvey shortSurvey) : this()
        {
            _dateTime = shortSurvey.DateTime;
            Depth = shortSurvey.Depth;
            Inclination = shortSurvey.Inclination;
            MWDInclination = shortSurvey.Inclination;
            Azimuth = shortSurvey.Azimuth;
            BTotal = shortSurvey.BTotal;
            GTotal = shortSurvey.GTotal;
            Enabled = shortSurvey.Enabled;
            AzimuthType = shortSurvey.AzimuthType;
            if (AzimuthType == MxSAzimuthTypeEnum.LongCollar)
            {
                MWDLongCollar = shortSurvey.Azimuth;
            }
            else if (AzimuthType == MxSAzimuthTypeEnum.ShortCollar)
            {
                MWDShortCollar = shortSurvey.Azimuth;
            }
            _run = shortSurvey.Run;
            ImportSource = shortSurvey.ImportSource;
            LastEditedBy = shortSurvey.LastEditedBy;
            CreatedBy = shortSurvey.CreatedBy;
        }

        [JsonProperty]
        public double? Azimuth { get; set; }

        [JsonProperty]
        public MxSAzimuthTypeEnum AzimuthType { get; set; }

        [JsonProperty]
        public double? Bg { get; set; }

        [JsonProperty]
        public double? Bh { get; set; }

        [JsonProperty]
        public double? BhMeasured { get; set; }

        [JsonProperty]
        public double? BtMeasured { get; set; }

        [JsonProperty]
        public double? BTotal { get; set; }

        [JsonProperty]
        public double? BTotalQcDelta { get; set; } //would this be on a raw?  -Hank 20140407

        [JsonProperty]
        public double? BvMeasured { get; set; }

        [JsonProperty]
        public double? Bx { get; set; }

        [JsonProperty]
        public double? By { get; set; }

        [JsonProperty]
        public double? Bz { get; set; }

        //[JsonProperty]
        //public CorrectedSurvey CorrectedSurvey { get; protected set; }

        // TODO: Suhail - In corrected survey RawSurvey is not required (Reference lopp)
        [JsonProperty]
        public CorrectedSurvey CorrectedSurvey
        {
            get
            {
                return _correctedSurvey;
            }
            set
            {
                _correctedSurvey = value;
                if (_correctedSurvey != null)
                {
                    _correctedSurvey.RawSurvey = this;
                }

            }
        }

        [JsonProperty]
        public double? Declination { get; set; }

        [JsonProperty]
        public double? Dip { get; set; }

        [JsonProperty]
        public double? DipMeasured { get; set; }

        [JsonProperty]
        public double? DipQcDelta { get; set; }//would this be on a raw?  -Hank 20140407

        [JsonProperty]
        public bool Enabled { get; set; }

        [JsonProperty]
        public double Error1 { get; set; }

        [JsonProperty]
        public double Error2 { get; set; }

        [JsonProperty]
        public double Error3 { get; set; }

        [JsonProperty]
        public double? GridConvergence { get; set; }

        [JsonProperty]
        public double? GTotal { get; set; }

        [JsonProperty]
        public double? GTotalQcDelta { get; set; }//would this be on a raw?  -Hank 20140407

        [NotMapped]
        [JsonProperty]
        public double? GtRawQC { get; set; }

        [JsonProperty]
        public double? Gx { get; set; }

        [JsonProperty]
        public double? Gy { get; set; }

        [JsonProperty]
        public double? Gz { get; set; }

        public double HighSide
        {
            get
            {
                if (Gx == null || Gy == null)
                {
                    return 0;
                }
                return Math.Atan2(Gy.Value, -Gx.Value) / Math.PI * 180;
            }
        }

        [JsonProperty]
        public MxSImportFileType ImportSource { get; set; }

        [JsonProperty]
        public double? Inclination { get; set; }

        [JsonProperty]
        public bool ManualTimeOffsetFlag { get; set; }

        [JsonProperty]
        public double? MWDInclination { get; set; }

        [JsonProperty]
        public double? MWDLongCollar { get; set; }

        [JsonProperty]
        public double? MWDShortCollar { get; set; }

        //TODO: Kiran - Temp fix to get the RD save issue resolved. Need to find the root cause and fix accordingly.
        //[JsonProperty]
        //public virtual ICollection<PMRSurveyBase> PMRSurveys { get; set; }

        [JsonProperty]
        public MxSSurveyPumpStatus PumpStatus { get; set; } = MxSSurveyPumpStatus.NA;

        [JsonProperty]
        public double? SagInclination { get; set; }

        [JsonProperty]
        public string Source { get; set; }


        [JsonProperty]
        public double? Temperature { get; set; }

        [JsonProperty]
        public double Triac1 { get; set; }

        [JsonProperty]
        public double Triac2 { get; set; }

        [JsonProperty]
        public double Triac3 { get; set; }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }

        [JsonProperty]
        public DateTime DateTime
        {
            get
            {
                if (_dateTime < MxSConstants.MinDateTime)
                {
                    return MxSConstants.MinDateTime;
                }
                return _dateTime;
            }
            set
            {
                if (_dateTime == value)
                {
                    return;
                }
                _dateTime = value;
            }
        }

        [JsonProperty]
        public long? RigTimeOffset { get; set; }

        #region Import from Insite, Long and short collar azimuth logic related

        [NotMapped]
        [JsonProperty]
        public string InsiteAzimType { get; set; }

        [NotMapped]
        [JsonProperty]
        public string InsiteAzimStat { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? InsiteAzimAmb1 { get; set; }

        [NotMapped]
        [JsonProperty]
        public double? InsiteAzimAmb2 { get; set; }

        #endregion Import from Insite, Long and short collar azimuth logic related

        #region raw inclinations

        [JsonProperty]
        public double? GxyzInclination { get; set; }

        [JsonProperty]
        public double? GxyInclination { get; set; }

        [JsonProperty]
        public double? GzInclination { get; set; }

        #endregion raw inclinations

        //TODO: Suhail - Need to find Alternative
        //[IgnoreDataMember]
        public string RunNo
        {
            get
            {
                return (Run != null) ? Run.RunNumber : string.Empty;
            }
        }

        [JsonProperty]
        public Guid RunId { get; set; }

        // TODO: Suhail - Reference loop
        [JsonProperty]
        public Run Run
        {
            get
            {
                return _run;
            }
            set { SetRun(value); }
        }

        [JsonProperty]
        public Guid SolutionId { get; set; }

        //TODO: Suhail - Need to find Alternate & commented some in set also
        //[IgnoreDataMember]
        public Solution Solution
        {
            get
            {
                if (_solution == null && Run != null)
                {
                    _solution = Run.GetSolution(SolutionId);
                }
                return _solution;
            }
            internal protected set
            {
                //if (_solution != null)
                //    _solution.PropertyChanged -= _solution_PropertyChanged;
                SetSolution(value);
                if (value != null)
                {
                    //_solution.PropertyChanged -= _solution_PropertyChanged;
                    //_solution.PropertyChanged += _solution_PropertyChanged;
                    SolutionId = value.Id;
                }
                else
                {
                    SolutionId = Guid.Empty;
                }
            }
        }


        //[IgnoreDataMember]
        public string SurveyIdentifier
        {
            get
            {
                return DateTime.ToString(MxSConstants.SurveyIndicationFormat) + "|" + Depth;
            }
        }

        public new void Dispose()
        {
            //if (_run != null)
            //{
            //    _run.PropertyChanged -= _run_PropertyChanged;
            //}
            //if (_solution != null)
            //{
            //    _solution.PropertyChanged -= _solution_PropertyChanged;
            //}
            CorrectedSurvey?.Dispose();
            base.Dispose();
        }

        public bool Equals(RawSurvey other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            var result = DateTime == other.DateTime &&
                         Depth.CompareDouble(other.Depth) && Gx.CompareDouble(other.Gx) && Gy.CompareDouble(other.Gy) &&
                         Gz.CompareDouble(other.Gz) && Bx.CompareDouble(other.Bx) && By.CompareDouble(other.By) && Bz.CompareDouble(other.Bz);

            return result;

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RawSurvey);
        }

        public DateTime? GetAtomicRigTime()
        {
            return CorrectedSurvey?.GetAtomicRigTime();
        }

        // TODO: Suhail - Related to DataModelBase Need to ask Kiran Sir
        //public override void Delete()
        //{
        //    if (CorrectedSurvey != null)
        //    {
        //        CorrectedSurvey.Delete();
        //    }
        //    base.Delete();
        //}

        //public override int GetHashCode()
        //{
        //    int hash = 23;
        //    hash = hash * 31 + Id.GetHashCode();
        //    hash = hash * 31 + DateTime.GetHashCode();
        //    hash = hash * 31 + Depth.GetHashCode();
        //    hash = hash * 31 + Gx.GetHashCode();

        //    hash = hash * 31 + Gy.GetHashCode();
        //    hash = hash * 31 + Gz.GetHashCode();
        //    hash = hash * 31 + Bx.GetHashCode();
        //    hash = hash * 31 + By.GetHashCode();
        //    hash = hash * 31 + Bz.GetHashCode();
        //    return hash;
        //}

        ~RawSurvey() { }

        #region Need To Check With Kiran Sir

        public CorrectedSurvey CreateDefaultCorrectedSurvey()
        {
            if (Enabled == true)
            {
                if (CorrectedSurvey == null)
                {
                    CorrectedSurvey = new CorrectedSurvey(this);
                    CorrectedSurvey.UpdateSolution();
                }
                if (CorrectedSurvey.RawSurvey == null || CorrectedSurvey.State == MxSState.Deleted)
                {
                    // TODO: Suhail - State property is removed
                    //CorrectedSurvey.SetState(MxSState.Modified);
                    CorrectedSurvey.RawSurvey = this;
                    CorrectedSurvey.Reset();
                }
            }
            return CorrectedSurvey;
        }

        public void ResetCorrectedSurvey()
        {
            if (CorrectedSurvey == null)
            {
                return;
            }
            //CorrectedSurvey.SetState(State.Modified);
            CorrectedSurvey.RawSurvey = this;
            CorrectedSurvey.Reset();
            if (!Enabled)
            {
                DeleteCorrectedSurvey();
            }
        }

        public void DeleteCorrectedSurvey()
        {
            if (CorrectedSurvey != null)
            {
                //TODO: Suhail - Modified as it is ralated to state
                //var run = CorrectedSurvey.Run;
                //if (State == State.Added || CorrectedSurvey.State == State.Added)
                //{
                //    CorrectedSurvey.Delete();
                //    CorrectedSurvey.RawSurvey = null;
                //    CorrectedSurvey = null;
                //}
                //else
                //{
                //    CorrectedSurvey.Delete();
                //    CorrectedSurvey.RawSurvey = null;
                //}
                CorrectedSurvey = null;
            }
        }

        private void SetRun(Run newRun)
        {
            //should ensure the run rawsurvey depth falls into the range of the run's depth.

            //if the solutions are the same.
            if (_run != null && newRun != null)
            {
                //need to cover the case where we have multiple new solutions.
                if (_run.Id == newRun.Id)
                {
                    return;
                }

            }
            // TODO: Suhail - Property change 
            //if (_run != null)
            //    _run.PropertyChanged -= _run_PropertyChanged;
            _run = newRun;
            //if (_run != null)
            //{
            //    _run.PropertyChanged -= _run_PropertyChanged;
            //    _run.PropertyChanged += _run_PropertyChanged;
            //}
            //if the newly assigned run is null then set the id to empty guid.
            
            //TODO : Suhail - RunId is changing on set run.
            //RunId = _run?.Id ?? Guid.Empty;
        }

        private void _run_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Id"))
            {
                RunId = Run.Id;
            }
        }

        private void _solution_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Id"))
            {
                SolutionId = Solution.Id;
            }
        }
        #endregion



        //TODO Sandeep Added here because of Recursion in extension method
        internal void SetSolution(Solution newSolution)
        {
            //if the solutions are the same.
            if (_solution != null && newSolution != null)
            {
                if (_solution.Id == newSolution.Id)
                {
                    return;
                }
            }

            if (newSolution != null)
            {
                //rawsurvey depth does not fit in the newsolution depth range, then just return.
                if (!newSolution.IsDepthInRange(Depth))
                {
                    return;
                }
                SolutionId = newSolution.Id;
            }
            else
            {
                //if the new value is null, then set the solution id to guid.empty
                SolutionId = Guid.Empty;
            }
            _solution = newSolution;
            //RaisePropertyChanged(SolutionPropertyName);

        }
    }
}
