using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class LinkInsiteWellResponse
    {
        public LinkInsiteWellResponse()
        {
            ValidToContinue = true;
        }

        [JsonProperty]
        public Guid? UnlinkedWellId { get; set; }

        [JsonProperty]
        public string Message { get; set; }

        [JsonProperty]
        public bool Succeeded { get; set; }

        [JsonProperty]
        public bool ConflictInsiteWell { get; set; }

        [JsonProperty]
        public bool ValidToContinue { get; set; }

        public Well Well { get; set; }

        [JsonProperty]
        public Well UnlinkedWell { get; set; }
        
    }
}
