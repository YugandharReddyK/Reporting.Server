using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Workbench.AziError.Base;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AziErrorLCPositionData : AziErrorBase
    {
        public AziErrorLCPositionData()
        {
            DataGridName = "WbAziErrorLCPositionGrid";
        }

        public double? dBzNE { get; set; }

        public double? SxyNE { get; set; }

        public double? BGmNE { get; set; }

        public double? dDecleNE { get; set; }

        public double? TotalNE { get; set; }

        public double? dBzEE { get; set; }

        public double? SxyEE { get; set; }

        public double? BGmEE { get; set; }

        public double? dDecleEE { get; set; }

        public double? TotalEE { get; set; }

        public double? dBzLE { get; set; }

        public double? SxyLE { get; set; }

        public double? BGmLE { get; set; }

        public double? dDecleLE { get; set; }

        public double? TotalLE { get; set; }

        public double? dBzVE { get; set; }

        public double? SxyVE { get; set; }

        public double? BGmVE { get; set; }

        public double? dDecleVE { get; set; }

        public double? TotalVE { get; set; }
    }
}
