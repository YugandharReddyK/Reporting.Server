using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Workbench.Icarus.Base;

namespace Sperry.MxS.Core.Common.Models.Workbench.Icarus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class IcarusDiyData : IcarusDataBase
    {
        public IcarusDiyData()
        {
            DataGridName = "WbIcaDiyGrid";
        }
        public double? dGx { get; set; }

        public double? dGy { get; set; }

        public double? dGz { get; set; }

        public double? Gt { get; set; }

        public double? Inc { get; set; }

        public double? sGx { get; set; }

        public double? sGy { get; set; }

        public MxSWorkBenchSpecialDataStatus GtStatus { get; set; }
    }
}
