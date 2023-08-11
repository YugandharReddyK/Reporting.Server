using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class TemplateWellInformation 
    {
        public TemplateWellInformation()
        { }

        public TemplateWellInformation(Well well)
        {
            WellId = well.Id;
            WellName = well.Name;
            LockedBy = well.LockedBy;
            ExportTemplateId = well.ExportTemplateId;
            ImportTemplateId = well.ImportTemplateId;
        }

        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public string WellName { get; set; }

        [JsonProperty]
        public string LockedBy { get; set; }

        [JsonProperty]
        public Guid ExportTemplateId { get; set; }

        [JsonProperty]
        public Guid ImportTemplateId { get; set; }

    }
}
