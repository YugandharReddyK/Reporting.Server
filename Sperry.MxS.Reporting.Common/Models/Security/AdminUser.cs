using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Security
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AdminUser : DataModelBase
    {
        [JsonProperty]
        public string NetworkId { get; set; } = "";

        [JsonProperty]
        public string EmailId { get; set; } = "";

        [JsonProperty]
        public bool IsDeletable { get; set; } = true;

        [NotMapped]
        [JsonIgnore]
        public string DisplayName { get; set; } = "";
    }
}
