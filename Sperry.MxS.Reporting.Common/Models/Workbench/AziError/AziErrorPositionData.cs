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
    public class AziErrorPositionData : AziErrorBase
    {
        public AziErrorPositionData()
        {
            DataGridName = "WbAziErrorPoistionGrid";
        }

        public double? North { get; set; }

        public double? East { get; set; }

        public double? Vertical { get; set; }

        public double? Lateral { get; set; }
    }
}
