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
   // [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CazandraTfcAveData : CazandraAveBase
    {
        public double dBxAve { get; set; }

        public double dByAve { get; set; }

        public double dBzAve { get; set; }

        public double SxyAve { get; set; }

    }
}
