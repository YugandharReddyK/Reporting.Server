using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CustomerReportTemplate : DataModelBase
    {
        [NotMapped]
        public int Index { get; set; }

        [JsonProperty]
        public string TemplateName { get; set; }

        [NotMapped]
        public MxSCustomerReportTemplateType ReportType { get; set; }

        [JsonProperty]
        public string TemplateType
        {
            get
            { return ReportType.ToString(); }
            set
            {
                ReportType = value.ParseEnum<MxSCustomerReportTemplateType>();
            }
        }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public bool IsEnabled { get; set; }
        
        public List<CustomerReportChartInfo> ChartParameters { get; set; }

        [NotMapped]
        public string TemplateFile { get; set; }

    }

    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
