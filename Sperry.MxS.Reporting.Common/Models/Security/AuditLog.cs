using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Security
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AuditLog : DataModelBase
    {
        [JsonProperty]
        public Guid EntityId { get; set; }

        [JsonProperty]
        public string EntityType { get; set; }

        [JsonProperty]
        public string Data { get; set; }

        [JsonProperty]
        public MxSAuditType AuditType { get; set; }

        [JsonProperty]
        public string Comments { get; set; }
    }

}
