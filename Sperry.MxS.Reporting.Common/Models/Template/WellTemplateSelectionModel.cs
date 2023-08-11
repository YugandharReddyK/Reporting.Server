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
    public class WellTemplateSelectionModel //: NotificationObject // to do 
    {
        public Guid ImportTemplateId { get; set; }

        public Guid ExportTemplateId { get; set; }

        public Guid WellId { get; set; }

    }
}
