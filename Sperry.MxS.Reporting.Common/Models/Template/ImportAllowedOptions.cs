using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Template
{
    [Serializable]
   // [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ImportAllowedOptions //: NotificationObject  // to do
    {
        public List<ExcelAllowedOption> ExcelAllowedOptions { get; set; } = new List<ExcelAllowedOption>();
    }
}
