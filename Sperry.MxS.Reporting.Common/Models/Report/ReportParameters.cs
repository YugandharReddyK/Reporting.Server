using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ReportParameters
    {
        [JsonProperty]
        public bool IsDefinitiveSurveyOnly { get; set; }

        [JsonProperty]
        public ObservableCollection<CustomerReportChartInfo> ChartParameters { get; set; }

    }
}
