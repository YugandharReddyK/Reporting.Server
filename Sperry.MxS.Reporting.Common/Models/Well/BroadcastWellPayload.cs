using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BroadcastWellPayload
    {
        public Guid WellId { get; set; }

        public MxSSaveCallerEnum SaveCallerEnum { get; set; }
    }
}
