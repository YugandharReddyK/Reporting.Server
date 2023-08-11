using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.MFM
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class MFMInputParameter
    {
        public Survey ReferenceSurvey { get; set; }

        public Survey Survey { get; set; }

        public double Inclination { get; set; }

        public double ReferenceInclination { get; set; }

        public double BTotal { get; set; }

        public double Dip { get; set; }

        public double Declination { get; set; }

        public double TotalCorrection { get; set; }

        public double GridConvergence { get; set; }
    }
}
