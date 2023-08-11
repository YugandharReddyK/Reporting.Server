using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Workbench.Cazandra.Base;

namespace Sperry.MxS.Core.Common.Models.Workbench.Cazandra
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraTxyData : CazandraTfcTxySfeDataBase
    {
        public CazandraTxyData()
        {
            DataGridName = "WbCazTxyGrid";
        }
        public double? K { get; set; }

        public double? KAcc { get; set; }

        public double? Sxy { get; set; }

        public double? SxyMav { get; set; }
    }
}
