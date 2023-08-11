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
    public class IcarusRawStdData
    {
        public double GtmStd { get; set; }
    }
}
