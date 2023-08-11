using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Extensions;
using Sperry.MxS.Core.Common.Interfaces;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ShortSurvey : DataModelBase, IMxSDateTimeDepth
    {
        private DateTime _dateTime;
        private Run _run;
        private double? _sigmaN;
        private double? _sigmaE;
        private double? _sigmaV;
        private double? _sigmaL;
        private double? _sigmaH;
        private double? _sigmaA;
        private double? _corrHL;
        private double? _corrHA;
        private double? _corrLA;
        private double? _hMajSA;
        private double? _hMinSA;
        private double? _rotAng;
        private double? _semiAx1;
        private double? _semiAx2;
        private double? _semiAx3;
        private double? _covNN;
        private double? _covNE;
        private double? _covNV;
        private double? _covEE;
        private double? _covEV;
        private double? _covVV;
        private string _toolCode;

        public ShortSurvey() : base()
        { }

        public ShortSurvey(ShortSurvey shortSurvey) : base()
        {
            _dateTime = shortSurvey.DateTime;
            Depth = shortSurvey.Depth;
            Inclination = shortSurvey.Inclination;
            Azimuth = shortSurvey.Azimuth;
            BTotal = shortSurvey.BTotal;
            GTotal = shortSurvey.GTotal;
            SurveyType = shortSurvey.SurveyType;
            NorthSouth = shortSurvey.NorthSouth;
            EastWest = shortSurvey.EastWest;
            Northing = shortSurvey.Northing;
            Easting = shortSurvey.Easting;
            TVD = shortSurvey.TVD;
            TVDss = shortSurvey.TVDss;
            Latitude = shortSurvey.Latitude;
            Longitude = shortSurvey.Longitude;
            Enabled = shortSurvey.Enabled;
            AzimuthType = shortSurvey.AzimuthType;
            ImportSource = shortSurvey.ImportSource;
            CourseLength = shortSurvey.CourseLength;

            _sigmaN = shortSurvey.SigmaN;
            _sigmaE = shortSurvey.SigmaE;
            _sigmaV = shortSurvey.SigmaV;
            _sigmaL = shortSurvey.SigmaL;
            _sigmaH = shortSurvey.SigmaH;
            _sigmaA = shortSurvey.SigmaA;
            BiasN = shortSurvey.BiasN;
            BiasE = shortSurvey.BiasE;
            BiasV = shortSurvey.BiasV;
            BiasH = shortSurvey.BiasH;
            BiasL = shortSurvey.BiasL;
            BiasA = shortSurvey.BiasA;
            _corrHL = shortSurvey.CorrHL;
            _corrHA = shortSurvey.CorrHA;
            _corrLA = shortSurvey.CorrLA;
            _hMajSA = shortSurvey.HMajSA;
            _hMinSA = shortSurvey.HMinSA;
            _rotAng = shortSurvey.RotAng;
            _semiAx1 = shortSurvey.SemiAx1;
            _semiAx2 = shortSurvey.SemiAx2;
            _semiAx3 = shortSurvey.SemiAx3;
            _covNN = shortSurvey.CovNN;
            _covNE = shortSurvey.CovNE;
            _covNV = shortSurvey.CovNV;
            _covEE = shortSurvey.CovEE;
            _covEV = shortSurvey.CovEV;
            _covVV = shortSurvey.CovVV;
            _toolCode = shortSurvey.ToolCode;

            _run = shortSurvey.Run;
        }

        public ShortSurvey(CorrectedSurvey correctedSurvey) : base()
        {
            _dateTime = correctedSurvey.DateTime;
            Depth = correctedSurvey.Depth;
            Inclination = correctedSurvey.SolInc;
            Azimuth = correctedSurvey.SolAzm;
            GTotal = correctedSurvey.GTotal;
            SurveyType = correctedSurvey.SurveyType;
            NorthSouth = correctedSurvey.NorthSouth;
            EastWest = correctedSurvey.EastWest;
            Northing = correctedSurvey.Northing;
            Easting = correctedSurvey.Easting;
            TVD = correctedSurvey.TVD;
            TVDss = correctedSurvey.TVDss;
            Latitude = correctedSurvey.Latitude;
            Longitude = correctedSurvey.Longitude;
            Enabled = correctedSurvey.Enabled;
            AzimuthType = correctedSurvey.AzimuthType;
            CourseLength = correctedSurvey.CourseLength;

            if (correctedSurvey.UncertaintyValue != null)
            {
                _sigmaN = correctedSurvey.UncertaintyValue.SigmaN;
                _sigmaE = correctedSurvey.UncertaintyValue.SigmaE;
                _sigmaV = correctedSurvey.UncertaintyValue.SigmaV;
                _sigmaL = correctedSurvey.UncertaintyValue.SigmaL;
                _sigmaH = correctedSurvey.UncertaintyValue.SigmaH;
                _sigmaA = correctedSurvey.UncertaintyValue.SigmaA;
                BiasN = correctedSurvey.UncertaintyValue.BiasN;
                BiasE = correctedSurvey.UncertaintyValue.BiasE;
                BiasV = correctedSurvey.UncertaintyValue.BiasV;
                BiasH = correctedSurvey.UncertaintyValue.BiasH;
                BiasL = correctedSurvey.UncertaintyValue.BiasL;
                BiasA = correctedSurvey.UncertaintyValue.BiasA;
                _corrHL = correctedSurvey.UncertaintyValue.CorrHL;
                _corrHA = correctedSurvey.UncertaintyValue.CorrHA;
                _corrLA = correctedSurvey.UncertaintyValue.CorrLA;
                _hMajSA = correctedSurvey.UncertaintyValue.HMajSA;
                _hMinSA = correctedSurvey.UncertaintyValue.HMinSA;
                _rotAng = correctedSurvey.UncertaintyValue.RotAng;
                _semiAx1 = correctedSurvey.UncertaintyValue.SemiAx1;
                _semiAx2 = correctedSurvey.UncertaintyValue.SemiAx2;
                _semiAx3 = correctedSurvey.UncertaintyValue.SemiAx3;
                _covNN = correctedSurvey.UncertaintyValue.CovNN;
                _covNE = correctedSurvey.UncertaintyValue.CovNE;
                _covNV = correctedSurvey.UncertaintyValue.CovNV;
                _covEE = correctedSurvey.UncertaintyValue.CovEE;
                _covEV = correctedSurvey.UncertaintyValue.CovEV;
                _covVV = correctedSurvey.UncertaintyValue.CovVV;
                _toolCode = correctedSurvey.UncertaintyValue.ToolCode;
            }
            _run = correctedSurvey.Run;
        }

        public ShortSurvey(RawSurvey rawSurvey) : base()
        {
            _dateTime = rawSurvey.DateTime;
            Depth = rawSurvey.Depth;
            Inclination = rawSurvey.Inclination;
            Azimuth = rawSurvey.Azimuth;
            BTotal = rawSurvey.BTotal;
            GTotal = rawSurvey.GTotal;
            Enabled = rawSurvey.Enabled;
            AzimuthType = rawSurvey.AzimuthType;
            ImportSource = rawSurvey.ImportSource;
            SurveyType = MxSSurveyType.Undefined;
        }

        [JsonProperty]
        public bool Enabled { get; set; }

        [JsonProperty]
        public MxSAzimuthTypeEnum AzimuthType { get; set; }

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
                if (_dateTime != value)
                {
                    _dateTime = value;
                }
            }
        }

        [JsonProperty]
        public double Depth { get; set; }

        [JsonProperty]
        public double? Azimuth { get; set; }

        [JsonProperty]
        public double? Inclination { get; set; }

        [JsonProperty]
        public double? BTotal { get; set; }

        [JsonProperty]
        public double? GTotal { get; set; }

        [JsonProperty]
        public MxSSurveyType SurveyType { get; set; }

        [JsonProperty]
        public double? NorthSouth { get; set; }

        [JsonProperty]
        public double? EastWest { get; set; }

        [JsonProperty]
        public double? Northing { get; set; }

        [JsonProperty]
        public double? Easting { get; set; }

        [JsonProperty]
        public double? Latitude { get; set; }

        [JsonProperty]
        public double? Longitude { get; set; }

        [JsonProperty]
        public double? TVD { get; set; }

        [JsonProperty]
        public double? TVDss { get; set; }

        [JsonProperty]
        public Guid RunId { get; set; }

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
        public MxSImportFileType ImportSource { get; set; }

        [JsonProperty]
        public double? CourseLength { get; set; }

        #region Uncertainty

        [JsonProperty]
        public double? SigmaN
        {
            get { return _sigmaN; }
            set
            {
                if (_sigmaN != value && value != null && !value.IsNaN())
                {
                    _sigmaN = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaE
        {
            get { return _sigmaE; }
            set
            {
                if (_sigmaE != value && value != null && !value.IsNaN())
                {
                    _sigmaE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaV
        {
            get { return _sigmaV; }
            set
            {
                if (_sigmaV != value && value != null && !value.IsNaN())
                {
                    _sigmaV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaL
        {
            get { return _sigmaL; }
            set
            {
                if (_sigmaL != value && value != null && !value.IsNaN())
                {
                    _sigmaL = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaH
        {
            get { return _sigmaH; }
            set
            {
                if (_sigmaH != value && value != null && !value.IsNaN())
                {
                    _sigmaH = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SigmaA
        {
            get { return _sigmaA; }
            set
            {
                if (_sigmaA != value && value != null && !value.IsNaN())
                {
                    _sigmaA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? BiasN { get; set; }

        [JsonProperty]
        public double? BiasE { get; set; }

        [JsonProperty]
        public double? BiasV { get; set; }

        [JsonProperty]
        public double? BiasH { get; set; }

        [JsonProperty]
        public double? BiasL { get; set; }

        [JsonProperty]
        public double? BiasA { get; set; }

        [JsonProperty]
        public double? CorrHL
        {
            get { return _corrHL; }
            set
            {
                if (_corrHL != value && value != null && !value.IsNaN())
                {
                    _corrHL = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CorrHA
        {
            get { return _corrHA; }
            set
            {
                if (_corrHA != value && value != null && !value.IsNaN())
                {
                    _corrHA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CorrLA
        {
            get { return _corrLA; }
            set
            {
                if (_corrLA != value && value != null && !value.IsNaN())
                {
                    _corrLA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? HMajSA
        {
            get { return _hMajSA; }
            set
            {
                if (_hMajSA != value && value != null && !value.IsNaN())
                {
                    _hMajSA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? HMinSA
        {
            get { return _hMinSA; }
            set
            {
                if (_hMinSA != value && value != null && !value.IsNaN())
                {
                    _hMinSA = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? RotAng
        {
            get { return _rotAng; }
            set
            {
                if (_rotAng != value && value != null && !value.IsNaN())
                {
                    _rotAng = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx1
        {
            get { return _semiAx1; }
            set
            {
                if (_semiAx1 != value && value != null && !value.IsNaN())
                {
                    _semiAx1 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx2
        {
            get { return _semiAx2; }
            set
            {
                if (_semiAx2 != value && value != null && !value.IsNaN())
                {
                    _semiAx2 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? SemiAx3
        {
            get { return _semiAx3; }
            set
            {
                if (_semiAx3 != value && value != null && !value.IsNaN())
                {
                    _semiAx3 = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNN
        {
            get { return _covNN; }
            set
            {
                if (_covNN != value && value != null && !value.IsNaN())
                {
                    _covNN = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNE
        {
            get { return _covNE; }
            set
            {
                if (_covNE != value && value != null && !value.IsNaN())
                {
                    _covNE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovNV
        {
            get { return _covNV; }
            set
            {
                if (_covNV != value && value != null && !value.IsNaN())
                {
                    _covNV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovEE
        {
            get { return _covEE; }
            set
            {
                if (_covEE != value && value != null && !value.IsNaN())
                {
                    _covEE = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovEV
        {
            get { return _covEV; }
            set
            {
                if (_covEV != value && value != null && !value.IsNaN())
                {
                    _covEV = value.Value;
                }
            }
        }

        [JsonProperty]
        public double? CovVV
        {
            get { return _covVV; }
            set
            {
                if (_covVV != value && value != null && !value.IsNaN())
                {
                    _covVV = value.Value;
                }
            }
        }

        [JsonProperty]
        public string ToolCode
        {
            get { return _toolCode; }
            set
            {
                if (_toolCode != value && value != null)
                {
                    _toolCode = value;
                }
            }
        }
        #endregion
        
        public void Reset()
        {
            try
            {
                GTotal = null;
                CourseLength = null;
                NorthSouth = null;
                EastWest = null;
                Northing = null;
                Easting = null;
                TVD = null;
                TVDss = null;
                Latitude = null;
                Longitude = null;

                _sigmaN = null;
                _sigmaE = null;
                _sigmaV = null;
                _sigmaL = null;
                _sigmaH = null;
                _sigmaA = null;
                BiasN = null;
                BiasE = null;
                BiasV = null;
                BiasH = null;
                BiasL = null;
                BiasA = null;
                _corrHL = null;
                _corrHA = null;
                _corrLA = null;
                _hMajSA = null;
                _hMinSA = null;
                _rotAng = null;
                _semiAx1 = null;
                _semiAx2 = null;
                _semiAx3 = null;
                _covNN = null;
                _covNE = null;
                _covNV = null;
                _covEE = null;
                _covEV = null;
                _covVV = null;
                _toolCode = string.Empty;
            }
            finally
            {
                // TODO: Suhail - Related to state
                //Listen = true;
                //if (State != State.Added)
                //{
                //    SetState(State.Modified);
                //}
            }
        }

        public void ResetPositionValues()
        {
            NorthSouth = null;
            EastWest = null;
            Northing = null;
            Easting = null;
            TVD = null;
            TVDss = null;
            Latitude = null;
            Longitude = null;
            CourseLength = null;
        }

        public void ResetUncertainityValues()
        {
            _sigmaN = null;
            _sigmaE = null;
            _sigmaV = null;
            _sigmaL = null;
            _sigmaH = null;
            _sigmaA = null;
            BiasN = null;
            BiasE = null;
            BiasV = null;
            BiasH = null;
            BiasL = null;
            BiasA = null;
            _corrHL = null;
            _corrHA = null;
            _corrLA = null;
            _hMajSA = null;
            _hMinSA = null;
            _rotAng = null;
            _semiAx1 = null;
            _semiAx2 = null;
            _semiAx3 = null;
            _covNN = null;
            _covNE = null;
            _covNV = null;
            _covEE = null;
            _covEV = null;
            _covVV = null;
            _toolCode = string.Empty;
        }

        private void SetRun(Run newRun)
        {
            //should ensure the run shortsurvey depth falls into the range of the run's depth.
            //if the solutions are the same.
            if (_run != null && newRun != null)
            {
                //need to cover the case where we have multiple new solutions.
                if (_run.Id == newRun.Id)
                {
                    return;
                }

            }
            //if (_run != null)
            //    _run.PropertyChanged -= _run_PropertyChanged;
            _run = newRun;
            //if (_run != null)
            //{
            //    _run.PropertyChanged -= _run_PropertyChanged;
            //    _run.PropertyChanged += _run_PropertyChanged;
            //}
            //if the newly assigned run is null then set the id to empty guid.
            RunId = _run?.Id ?? Guid.Empty;
        }

        private void _run_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Id"))
            {
                RunId = Run.Id;
            }
        }

        public new void Dispose()
        {
            //if (_run != null)
            //{
            //    _run.PropertyChanged -= _run_PropertyChanged;
            //}
            base.Dispose();
        }
    }
}
