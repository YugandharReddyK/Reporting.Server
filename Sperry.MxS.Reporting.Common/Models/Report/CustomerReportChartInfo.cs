using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]                                                                  
    public class CustomerReportChartInfo : DataModelBase
    {
        [JsonProperty]
        public Guid TemplateId { get; set; }

        [JsonProperty]
        public string TemplateName { get; set; }

        [JsonProperty]
        public string ChartName { get; set; }

        [JsonProperty]
        public string CharDisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(ChartName))
                {
                    return ChartName.Replace("CHART:", "");
                }
                return ChartName;
            }
        }

        [JsonProperty]
        public double? YAxisMin { get; set; }

        [JsonProperty]
        public double? YAxisMax { get; set; }

        [JsonProperty]
        public double? XAxisMin { get; set; }

        [JsonProperty]
        public double? XAxisMax { get; set; }

        [JsonProperty]
        public int? XAxisInterval { get; set; }

        public bool IsValid
        {
            get { return YAxisMax.HasValue && YAxisMin.HasValue; }
        }
        [JsonProperty]
        public bool IsChartSettingsOverride { get; set; }

    }
}
