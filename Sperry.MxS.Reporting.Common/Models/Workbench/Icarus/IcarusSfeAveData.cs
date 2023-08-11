using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Icarus
{
    [Serializable]
   // [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class IcarusSfeAveData
    {
        public double dGxAve { get; set; }

        public double dGyAve { get; set; }

        public double dGzAve { get; set; }

        public double sGxAve { get; set; }

        public double sGyAve { get; set; }

        public double GtAve { get; set; }
    }
}
