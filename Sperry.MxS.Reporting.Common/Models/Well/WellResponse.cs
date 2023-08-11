using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Results;
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
    [Obsolete("Use ResultObject<Well>")]
    public class WellResponse
    {

        public WellResponse()
        {

        }

        [Obsolete("added to maintain backwards compatibility; this object should be replaced with the ResultObject<Well>")]
        public WellResponse(ResultObject<Well> resultObject)
        {
            Succeeded = resultObject.Success;
            ErrorMessage = resultObject.Message;
            Well = resultObject.Data;
        }

        [JsonProperty]
        public bool Succeeded { get; set; }

        [JsonProperty]
        public string ErrorMessage { get; set; }

        [JsonProperty]
        public Well Well { get; set; }
    }
}
