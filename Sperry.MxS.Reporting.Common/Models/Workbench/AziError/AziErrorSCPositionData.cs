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
    public class AziErrorSCPositionData : AziErrorBase
    {
        public AziErrorSCPositionData()
        {
            DataGridName = "WbAziErrorSCPositionGrid";
        }
        public double? dDipePlusNE { get; set; }

        public double? dDipeMinusNE { get; set; }

        public double? dBePlusNE { get; set; }

        public double? dBeMinusNE { get; set; }

        public double? SxyNE { get; set; }

        public double? BGmNE { get; set; }

        public double? dDecleNE { get; set; }

        public double? TotalNEPlus { get; set; }

        public double? TotalNEMinus { get; set; }

        public double? dDipePlusEE { get; set; }

        public double? dDipeMinusEE { get; set; }

        public double? dBePlusEE { get; set; }

        public double? dBeMinusEE { get; set; }

        public double? SxyEE { get; set; }

        public double? BGmEE { get; set; }

        public double? dDecleEE { get; set; }

        public double? TotalEEPlus { get; set; }

        public double? TotalEEMinus { get; set; }

        public double? dDipePlusLE { get; set; }

        public double? dDipeMinusLE { get; set; }

        public double? dBePlusLE { get; set; }

        public double? dBeMinusLE { get; set; }

        public double? SxyLE { get; set; }

        public double? BGmLE { get; set; }

        public double? dDecleLE { get; set; }

        public double? TotalLEPlus { get; set; }

        public double? TotalLEMinus { get; set; }

        public double? dDipePlusVE { get; set; }

        public double? dDipeMinusVE { get; set; }

        public double? dBePlusVE { get; set; }

        public double? dBeMinusVE { get; set; }

        public double? SxyVE { get; set; }

        public double? BGmVE { get; set; }

        public double? dDecleVE { get; set; }

        public double? TotalVEPlus { get; set; }

        public double? TotalVEMinus { get; set; }

    }

}
