using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CustomerImages : DataModelBase
    {
        [JsonProperty]
        public byte[] Data { get; set; }

        [JsonProperty]
        public string FileName { get; set; }

        [JsonProperty]
        public string ContentType { get; set; }

        [JsonProperty]
        public Guid WellId { get; set; }
    }
}
