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
    public class AziErrorSCData : AziErrorBase
    {
        public AziErrorSCData()
        {
            DataGridName = "WbAziErrorSCGrid";
        }

        public double? dAzdDipePlus { get; set; }

        public double? dAzdDipeMinus { get; set; }

        public double? dAzdBePlus { get; set; }

        public double? dAzdBeMinus { get; set; }

        public double? dAzSxy { get; set; }

        public double? dAzBGm { get; set; }

        public double? dAzTotalPlus { get; set; }

        public double? dAzTotalMinus { get; set; }

        public double? dDipcdDipePlus { get; set; }

        public double? dDipcdDipeMinus { get; set; }

        public double? dDipcdBePlus { get; set; }

        public double? dDipcdBeMinus { get; set; }

        public double? dDipcSxy { get; set; }

        public double? dDipcBGm { get; set; }

        public double? dDipcTotalPlus { get; set; }

        public double? dDipcTotalMinus { get; set; }

        public double? dBtcdDipePlus { get; set; }

        public double? dBtcdDipeMinus { get; set; }

        public double? dBtcdBePlus { get; set; }

        public double? dBtcdBeMinus { get; set; }

        public double? dBtcSxy { get; set; }

        public double? dBtcBGm { get; set; }

        public double? dBtcTotalPlus { get; set; }

        public double? dBtcTotalMinus { get; set; }

        public double? dBzcdDipePlus { get; set; }

        public double? dBzcdDipeMinus { get; set; }

        public double? dBzcdBePlus { get; set; }

        public double? dBzcdBeMinus { get; set; }

        public double? dBzcSxy { get; set; }

        public double? dBzcBGm { get; set; }

        public double? dBzcTotalPlus { get; set; }

        public double? dBzcTotalMinus { get; set; }
    }
}
