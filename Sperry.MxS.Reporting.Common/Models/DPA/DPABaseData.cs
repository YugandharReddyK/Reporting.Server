using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.DPA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class DPABaseData
    {
        public double[] Depths { get; set; }

        public double[] Inclinations { get; set; }

        public double[] SourceAzimuths { get; set; }

        public double[] DrillingAzimuths { get; set; }

        public double[] CorrectedAzimuths { get; set; }

        public double[] Bg { get; set; }

        public double[] Bh { get; set; }

        public double[] Bgs { get; set; }

        public double[] Bhs { get; set; }

    }
}
