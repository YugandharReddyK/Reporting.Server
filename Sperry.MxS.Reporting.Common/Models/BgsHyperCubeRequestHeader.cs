using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public struct BgsHyperCubeRequestHeader
    {
        public BGSDataPoint BGSDataPoint;

        public string HyperCubeId;

        public HypercubeWebsiteInfo HypercubeWebsiteInfo;

        public BgsHyperCubeRequestHeader(string hyperCubeId, BGSDataPoint bgsDataPoint, HypercubeWebsiteInfo hypercubeWebsiteInfo)
        {
            BGSDataPoint = bgsDataPoint;
            HyperCubeId = hyperCubeId;
            HypercubeWebsiteInfo = hypercubeWebsiteInfo;
        }
    }
}
