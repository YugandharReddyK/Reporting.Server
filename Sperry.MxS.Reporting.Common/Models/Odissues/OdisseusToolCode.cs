using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Odisseus;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.Odisseus
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public partial class OdisseusToolCode : DataModelBase
    {
        [JsonProperty]
        public string FileName { get;set; }

        [JsonProperty]
        public string Description { get;set; }  

        [JsonProperty]
        public bool IsEnabled { get; set; }

        [NotMapped]
        public string FilePath { get; set; }

        [JsonProperty]
        public List<OdisseusToolCodeParams> OdisseusToolCodeParameters { get; protected set; }
        
    }
}
