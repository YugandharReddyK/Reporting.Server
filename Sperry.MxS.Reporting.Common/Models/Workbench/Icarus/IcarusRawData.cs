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
    public class IcarusRawData : IcarusDataBase
    {
        public IcarusRawData()
        {
            DataGridName = "WbIcaRawGrid";
        }
        public double? Gte { get; set; }

        public double? Gtm { get; set; }

        public double? GxyInc { get; set; }

        public double? GzInc { get; set; }

        public double? HsdSpread { get; set; }

        public double? Inc { get; set; }

        public MxSWorkBenchSpecialDataStatus GtStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus HsdSpreadStatus { get; set; }
    }
}
