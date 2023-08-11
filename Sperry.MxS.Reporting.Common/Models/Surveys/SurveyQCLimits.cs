using Newtonsoft.Json;
using System;

namespace Sperry.MxS.Core.Common.Models.Surveys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class SurveyQCLimits : IEquatable<SurveyQCLimits>
    {
        public SurveyQCLimits()
        {
            WellId = Guid.Empty;
            GtLimit = double.PositiveInfinity;
            BtLimit = double.PositiveInfinity;
            BtDipLimit = double.PositiveInfinity;
            DipLimit = double.PositiveInfinity;
            AzimLowLimit = double.PositiveInfinity;
            AzimHighLimit = double.PositiveInfinity;
        }

        public SurveyQCLimits(Guid wellId, double gtLimit, double btLimit, double btDipLimit, double dipLimit, double azimLowLimit, double azimHighLimit)
        {
            WellId = wellId;
            GtLimit = gtLimit;
            BtLimit = btLimit;
            BtDipLimit = btDipLimit;
            DipLimit = dipLimit;
            AzimLowLimit = azimLowLimit;
            AzimHighLimit = azimHighLimit;
        }

        public Guid WellId { get; set; }

        public double GtLimit { get; set; }

        public double BtLimit { get; set; }

        public double BtDipLimit { get; set; }

        public double DipLimit { get; set; }

        public double AzimLowLimit { get; set; }

        public double AzimHighLimit { get; set; }

        public bool Equals(SurveyQCLimits other)
        {
            if (other == null)
                return false;

            return GtLimit == other.GtLimit
                && BtLimit == other.BtLimit
                && BtDipLimit == other.BtDipLimit
                && DipLimit == other.DipLimit
                && AzimHighLimit == other.AzimHighLimit
                && AzimLowLimit == other.AzimLowLimit;
        }

    }
}
