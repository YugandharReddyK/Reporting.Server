using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Icarus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class IcarusSfeStdData
    {
        public double dGxStd { get; set; }

        public double dGyStd { get; set; }

        public double dGzStd { get; set; }

        public double sGxStd { get; set; }

        public double sGyStd { get; set; }

        public double GtStd { get; set; }
    }
}
