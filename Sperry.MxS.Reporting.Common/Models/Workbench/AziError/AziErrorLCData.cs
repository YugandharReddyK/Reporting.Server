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
    public class AziErrorLCData : AziErrorBase
    {
        public AziErrorLCData()
        {
            DataGridName = "WbAziErrorLCGrid";
        }

        public double? dAzdBz { get; set; }

        public double? dAzSxy { get; set; }

        public double? dAzBGm { get; set; }

        public double? dAzTotal { get; set; }

        public double? dDipdBz { get; set; }

        public double? dDipSxy { get; set; }

        public double? dDipBGm { get; set; }

        public double? dDipTotal { get; set; }

        public double? dBtdBz { get; set; }

        public double? dBtSxy { get; set; }

        public double? dBtBGm { get; set; }

        public double? dBtTotal { get; set; }
    }
}
