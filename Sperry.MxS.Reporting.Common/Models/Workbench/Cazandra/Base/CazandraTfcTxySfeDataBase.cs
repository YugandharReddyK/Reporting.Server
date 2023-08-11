using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraTfcTxySfeDataBase : CazandraDataBase
    {
        public double? BGm { get; set; }

        public double? BGmMav { get; set; }

        public double? dBx { get; set; }

        public double? dBxAcc { get; set; }

        public MxSWorkBenchSpecialDataStatus dBxAccStatus { get; set; }

        public double? dBxMax { get; set; }

        public double? dBxMin { get; set; }

        public MxSWorkBenchSpecialDataStatus dBxStatus { get; set; }

        public double? dBy { get; set; }

        public double? dByAcc { get; set; }

        public MxSWorkBenchSpecialDataStatus dByAccStatus { get; set; }

        public double? dByMax { get; set; }

        public double? dByMin { get; set; }

        public MxSWorkBenchSpecialDataStatus dByStatus { get; set; }

        public double? dBzMAv { get; set; }

        public double? dBzSc { get; set; }
    }
}
