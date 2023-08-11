using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class Waypoint : DataModelBase
    {
        private double _previousWpEndDepth;
        private double _endDepth;
        private bool _showEnd;
        private string _magneticModel;

        public Waypoint()
        {

        }

        public Waypoint(Waypoint waypointToCopy) : this()
        {
            Name = waypointToCopy.Name;
            StartDepth = waypointToCopy.StartDepth;
            IsGeographicLocationInDecimal = waypointToCopy.IsGeographicLocationInDecimal;
            _showEnd = waypointToCopy.ShowEnd;
            _endDepth = waypointToCopy.EndDepth;
            IsStartDepthValid = waypointToCopy.IsStartDepthValid;
            IsEndDepthValid = waypointToCopy.IsEndDepthValid;
            Elevation = waypointToCopy.Elevation;
            Type = waypointToCopy.Type;
            MeasuredDepth = waypointToCopy.MeasuredDepth;
            VerticalDepth = waypointToCopy.VerticalDepth;
            Latitude = waypointToCopy.Latitude;
            Longitude = waypointToCopy.Longitude;
            Gravity = waypointToCopy.Gravity;
            CalculatedDate = waypointToCopy.CalculatedDate;
            Source = waypointToCopy.Source;
            GridConvergence = waypointToCopy.GridConvergence;
            Btotal = waypointToCopy.Btotal;
            Dip = waypointToCopy.Dip;
            BGGMDeclination = waypointToCopy.BGGMDeclination;
            BGGMDip = waypointToCopy.BGGMDip;
            BGGMBTotal = waypointToCopy.BGGMBTotal;
            IFRDeclination = waypointToCopy.IFRDeclination;
            IFRDip = waypointToCopy.IFRDip;
            IFRBTotal = waypointToCopy.IFRBTotal;
            CADecOffset = waypointToCopy.CADecOffset;
            CADipOffset = waypointToCopy.CADipOffset;
            CABtOffset = waypointToCopy.CABtOffset;
            MagneticFieldStrength = waypointToCopy.MagneticFieldStrength;
            MagneticDipAngle = waypointToCopy.MagneticDipAngle;
            MagneticDeclination = waypointToCopy.MagneticDeclination;
            RefDeclination = waypointToCopy.RefDeclination;
            RefDip = waypointToCopy.RefDip;
            RefBTotal = waypointToCopy.RefBTotal;
            _magneticModel = waypointToCopy.MagneticModel;
            BGGMTotalCorrection = waypointToCopy.BGGMTotalCorrection;
            IFRTotalCorrection = waypointToCopy.IFRTotalCorrection;
            WaypointMode = waypointToCopy.WaypointMode;
            IFR1WaypointMode = waypointToCopy.IFR1WaypointMode;
            IsForceMagDate = waypointToCopy.IsForceMagDate;
            HyperCube = waypointToCopy.HyperCube;
        }

        #region General Information

        [JsonProperty]
        public Guid WellId { get; set; }

        // TODO: Suhail - Need to remove Well Reference loop
        [NotMapped]
        [JsonProperty]
        public Well Well { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public double StartDepth { get; set; }

        #region Latitude/longitude string in datagrid view

        [NotMapped]
        [JsonProperty]
        public bool IsGeographicLocationInDecimal { get; set; }

        #endregion

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
                            EndDepth = _previousWpEndDepth;
                    }
                }
            }
        }

        #endregion

        [JsonProperty]
        public double EndDepth
        {
            get { return _endDepth; }
            set
            {
                if (_endDepth != value)
                {
                    DecideBeforeSetEndDepth(value);
                    _endDepth = value;
                }
            }
        }

        [NotMapped]
        [JsonProperty]
        public bool IsStartDepthValid { get; set; }

        [NotMapped]
        [JsonProperty]
        public bool IsEndDepthValid { get; set; }

        [JsonProperty]
        public double Elevation { get; set; }

        [JsonProperty]
        public MxSWaypointType Type { get; set; }

        [JsonProperty]
        public MxSWaypointMode WaypointMode { get; set; } = MxSWaypointMode.WayPoint;

        [JsonProperty]
        public MxSIFR1WaypointMode IFR1WaypointMode { get; set; } = MxSIFR1WaypointMode.WayPoint;

        [JsonProperty]
        public double MeasuredDepth { get; set; }

        [JsonProperty]
        public double VerticalDepth { get; set; }

        [JsonProperty]
        public double Latitude { get; set; }

        [JsonProperty]
        public double Longitude { get; set; }

        [JsonProperty]
        public double Gravity { get; set; }

        [JsonProperty]
        public DateTime CalculatedDate { get; set; }

        [JsonProperty]
        public string Source { get; set; }

        [JsonProperty]
        public double GridConvergence { get; set; }

        #endregion

        [NotMapped]
        [JsonProperty]
        public double Btotal { get; set; }

        [NotMapped]
        [JsonProperty]
        public double Dip { get; set; }

        [JsonProperty]
        public double BGGMDeclination { get; set; }

        [JsonProperty]
        public double BGGMDip { get; set; }

        [JsonProperty]
        public double BGGMBTotal { get; set; }

        [JsonProperty]
        public double IFRDeclination { get; set; }

        [JsonProperty]
        public double IFRDip { get; set; }

        [JsonProperty]
        public double IFRBTotal { get; set; }

        [JsonProperty]
        public double CADecOffset { get; set; }

        [JsonProperty]
        public double CADipOffset { get; set; }

        [JsonProperty]
        public double CABtOffset { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MagneticFieldStrength { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MagneticDipAngle { get; set; }

        [NotMapped]
        [JsonProperty]
        public double MagneticDeclination { get; set; }

        [JsonProperty]
        public double RefDeclination { get; set; }

        [JsonProperty]
        public double RefDip { get; set; }

        [JsonProperty]
        public double RefBTotal { get; set; }

        [JsonProperty]
        public string MagneticModel
        {
            get { return _magneticModel; }
            set
            {
                if (_magneticModel != value)
                {
                    _magneticModel = value;
                    _magneticModel = _magneticModel?.ToUpper();
                }
            }
        }

        [JsonProperty]
        public bool IsForceMagDate { get; set; }

        [JsonProperty]
        public string HyperCube { get; set; }

        #region Calulated Fields

        [JsonProperty]
        public double BGGMTotalCorrection { get; set; }

        [JsonProperty]
        public double IFRTotalCorrection { get; set; }

        #endregion

        [JsonProperty]
        public bool Deleted { get; set; }

        public void DecideBeforeSetEndDepth(double value)
        {
            _showEnd = value == MxSConstant.MaximumDepth;
            if (_endDepth != MxSConstant.MaximumDepth && _endDepth != 0)
            {
                _previousWpEndDepth = _endDepth;
            }
        }

    }
}
