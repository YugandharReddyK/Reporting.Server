using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TemplateInfo : DataModelBase
    {
        public TemplateInfo()
        {
            TemplateType = MxSTemplateType.Import;
        }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public MxSTemplateType TemplateType { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public string TemplateContent { get; set; }

        [JsonProperty]
        public bool Deleted { get; set; }
        
    }
}
