using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class ServerMetaData
    {
        [JsonProperty]
        public bool IsDataBaseUp { get; set; }

        [JsonProperty]
        public int NoOfWells { get; set; }
       
        [JsonProperty]
        public int NoOfActiveWells { get; set; }
    }
}
