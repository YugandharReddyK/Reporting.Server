using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Admin
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class DiskHealthStat
    {
        public Double FreeSpace { get; set; }

        public Double TotalSpace { get; set; }

        public ResultStatus ResultStatus { get; set; }
    }
}
