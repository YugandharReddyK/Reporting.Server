using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.ASA
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ASAReset
    {
        [JsonProperty] 
        public Well Well { get; set; }

        [JsonProperty]
        public int ASASessionCount { get; set; }

        [JsonProperty] 
        public MxSASAModeEnum ASAMode { get; set; }
    }
}
