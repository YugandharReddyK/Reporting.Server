using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CustomerReport : DataModelBase
    {
        [JsonProperty]
        public Guid WellId { get; set; }

        [JsonProperty]
        public Guid? TemplateId { get; set; }

        [JsonProperty]
        public String TemplateName { get; set; }

        [NotMapped]
        [JsonProperty]
        public CustomerReportData ReportData { get; set; }

        public CustomerReport()
        {
            CreatedTime = DateTime.UtcNow;
            LastEditedTime = DateTime.UtcNow;
        }

        public CustomerReport(string user, Guid wellId, Guid? templateId) : this()
        {
            WellId = wellId;
            TemplateId = templateId;
            CreatedBy = user;
            LastEditedBy = user;
        }
    }
}
