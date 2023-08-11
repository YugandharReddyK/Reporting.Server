using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;

namespace Sperry.MxS.Core.Common.Models.DynamicQC
{
    [Serializable]
    public class DynamicQCInputParameters
    {
        public DynamicQCInputParameters()
        {
            NorthReference = MxSNorthReference.True; // InSite assumes True north
        }

        public double Sigma { get; set; }

        public MxSAzimuthTypeEnum AziType { get; set; }

        public MxSMSA MSA { get; set; }

        public double DBxyz { get; set; }

        public double DGxyz { get; set; }

        public double DBzMod { get; set; }

        public double DDipe { get; set; }

        public double DBe { get; set; }

        public double DEC { get; set; }

        public double DBH { get; set; }

        public double DBNoise { get; set; }

        public double DDipNoise { get; set; }

        public double Sxy { get; set; }

        public double? BGm { get; set; }

        public MxSMagneticModelType MagneticReference { get; set; }

        public string IPMToolCode { get; set; }

        public MxSNorthReference NorthReference { get; set; }
    }
}
