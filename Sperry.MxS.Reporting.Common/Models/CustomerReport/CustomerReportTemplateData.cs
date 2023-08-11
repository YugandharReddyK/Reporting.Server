using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class CustomerReportTemplateData : DataModelBase
    {
        [JsonProperty]
        public byte[] Data { get; set; }
    }
}
