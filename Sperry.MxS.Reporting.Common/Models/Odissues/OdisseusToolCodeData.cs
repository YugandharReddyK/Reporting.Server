using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public partial class OdisseusToolCodeData : DataModelBase
    {
        [JsonProperty]
        public byte[] Data { get; set; }
    }
}
