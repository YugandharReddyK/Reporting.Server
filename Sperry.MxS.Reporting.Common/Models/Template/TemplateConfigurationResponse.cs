using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class TemplateConfigurationResponse
    {
        public Guid TemplateId { get; set; }

        public bool Succeeded { get; set; }

        public string errMsg { get; set; }
    }
}
