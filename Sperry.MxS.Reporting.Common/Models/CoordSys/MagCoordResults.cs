using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CoordSys
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MagCoordResults
    {
        // ref double dConvergence, ref double dDeclination, ref double dDip, ref double dBTotal
        public MagCoordResults()
        {
            Convergence = 0;
            MagDip = 0;
            MagDeclination = 0;
            MagFieldStrength = 0;
        }

        public MagCoordResults(double convergence, double magDip, double magDeclination, double magFieldStrength)
        {
            Convergence = convergence;
            MagDip = magDip;
            MagDeclination = magDeclination;
            MagFieldStrength = magFieldStrength;
        }

        [JsonProperty]
        public double Convergence { get; set; }

        [JsonProperty]
        public double MagDip { get; set; }
        
        [JsonProperty]
        public double MagDeclination { get; set; }

        [JsonProperty] 
        public double MagFieldStrength { get; set; }
       
    }
}
