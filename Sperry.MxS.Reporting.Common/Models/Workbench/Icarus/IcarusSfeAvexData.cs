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
    public class IcarusSfeAvexData
    {
        public double dGxAvex { get; set; }

        public double dGyAvex { get; set; }

        public double dGzAvex { get; set; }

        public double sGxAvex { get; set; }

        public double sGyAvex { get; set; }
    }
}
