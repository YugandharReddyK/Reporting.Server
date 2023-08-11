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
    public class TemplateConfiguration //: NotificationObject  // To do
    {
        public Guid Id { get; set; }

        public Guid WellId { get; set; }

        public string Description { get; set; }

        public string TemplateName { get; set; }

        public string ImportFileName { get; set; }

        public bool IsActive { get; set; }

        public string OutputFolder { get; set; }

        public MxSTimeZoneTypes TimeZoneType { get; set; }

        public string TemplateContent { get; set; }
    }
}
