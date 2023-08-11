using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DPA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class DPAParameters
    {
        public double DrillingNorth { get; set; }

        public double DrillingEast { get; set; }

        public double DrillingTvd { get; set; }

        public double TargetNorth { get; set; }

        public double TargetEast { get; set; }

        public double TargetTvd { get; set; }

        public double MinPickX { get; set; }

        public double MinPickY { get; set; }

        public double MaxPickX { get; set; }

        public double MaxPickY { get; set; }

        public double MinDepth { get; set; }

        public double MaxDepth { get; set; }

        public double Casing { get; set; }

        public double CasingDiameter { get; set; }

        public double CasingMagneticWavenumber { get; set; }

        public double MaxCumulative { get; set; }

        public double ReferencedBg { get; set; }

        public double ReferencedBh { get; set; }

        public double ReferencedInclination { get; set; }

        public double ReferencedAzimuth { get; set; }

        public double Decilination { get; set; }

        private MxSAzimuthRange AzimuthRange { get; set; }
    }
}
