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
    public class IcarusSfeData : IcarusDataBase
    {
        public IcarusSfeData()
        {
            DataGridName = "WbIcaSfeGrid";
        }
        public double? dGx { get; set; }

        public double? dGxAcc { get; set; }

        public double? dGxMax { get; set; }

        public double? dGxMin { get; set; }

        public double? dGy { get; set; }

        public double? dGyAcc { get; set; }

        public double? dGyMax { get; set; }

        public double? dGyMin { get; set; }

        public double? dGz { get; set; }

        public double? dGzAcc { get; set; }

        public double? Gt { get; set; }

        public double? Inc { get; set; }

        public double? sGx { get; set; }

        public double? sGxAcc { get; set; }

        public double? sGxMax { get; set; }

        public double? sGxMin { get; set; }

        public double? sGy { get; set; }

        public double? sGyAcc { get; set; }

        public double? sGyMax { get; set; }

        public double? sGyMin { get; set; }

        public MxSWorkBenchSpecialDataStatus dGxStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGxAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGyStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGyAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGzStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus dGzAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus GtStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus sGxStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus sGxAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus sGyStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus sGyAccStatus { get; set; }
    }
}
