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
    public class CazandraRawData : CazandraDataBase
    {
        public CazandraRawData()
        {
            DataGridName = "WbCazRawDatasGrid";
        }
        public double? Gx { get; set; }

        public double? Gy { get; set; }

        public double? Gz { get; set; }

        public double? Bx { get; set; }

        public double? By { get; set; }

        public double? Bz { get; set; }

        public double? dBzSc { get; set; }

        public double? dBzMAv { get; set; }

        public double? Inclination { get; set; }

        public double? HsSpread { get; set; }

        public double? Gt { get; set; }

        public double? BGmMav { get; set; }

        public double? Sxy { get; set; }

        public double? SxyMav { get; set; }

        public MxSWorkBenchSpecialDataStatus GtStatus { get; set; }

        public MxSWorkBenchSpecialDataStatus HsSpreadStatus { get; set; }
    }

}
