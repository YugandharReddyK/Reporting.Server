using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Workbench.AziError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AziErrorOutput
    {
        public AziErrorOutput()
        {
            AziErrorPositionData = new AziErrorPositionData();
            AziErrorLCData = new AziErrorLCData();
            AziErrorLCPositionData = new AziErrorLCPositionData();
            AziErrorSCPositionData = new AziErrorSCPositionData();
            AziErrorSCData = new AziErrorSCData();
        }

        public AziErrorPositionData AziErrorPositionData { get; set; }

        public AziErrorLCData AziErrorLCData { get; set; }

        public AziErrorLCPositionData AziErrorLCPositionData { get; set; }

        public AziErrorSCData AziErrorSCData { get; set; }

        public AziErrorSCPositionData AziErrorSCPositionData { get; set; }
    }

}
