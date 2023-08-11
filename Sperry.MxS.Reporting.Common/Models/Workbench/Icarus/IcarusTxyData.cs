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
    public class IcarusTxyData : IcarusDataBase
    {
        public IcarusTxyData()
        {
            DataGridName = "WbIcaTxyGrid";
        }
        public double? dGx { get; set; }

        public double? dGxAcc { get; set; }

        public double? dGxMax { get; set; }

        public double? dGxMin { get; set; }

        public double? dGy { get; set; }

        public double? dGyAcc { get; set; }

        public double? dGyMax { get; set; }

        public double? dGyMin { get; set; }

        public double? Gt { get; set; }

        public double? Inc { get; set; }

        public double? K { get; set; }

        public double? KAcc { get; set; }

        public MxSWorkBenchSpecialDataStatus dGxStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGxAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGyStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGyAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus KStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus KAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus GtStatus { get; set; }
    }
}
