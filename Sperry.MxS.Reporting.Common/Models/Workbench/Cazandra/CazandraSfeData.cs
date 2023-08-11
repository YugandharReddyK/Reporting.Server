using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraSfeData : CazandraTfcTxySfeDataBase
    {
        public CazandraSfeData()
        {
            DataGridName = "WbCazSfeGrid";
        }
        public double? dBz { get; set; }

        public double? Sx { get; set; }

        public double? Sy { get; set; }

        public double? dBzAcc { get; set; }

        public double? SxAcc { get; set; }

        public double? SyAcc { get; set; }

        public double? SxMax { get; set; }

        public double? SxMin { get; set; }

        public double? SyMax { get; set; }

        public double? SyMin { get; set; }

        public MxSWorkBenchSpecialDataStatus SxAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus SxStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus SyAccStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus SyStatus { get; set; }
    }
}
