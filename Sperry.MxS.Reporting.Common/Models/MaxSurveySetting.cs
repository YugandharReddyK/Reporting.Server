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
    public class MaxSurveySetting
    {
        public string BgsUserName { get; set; }

        public string Bgspassword { get; set; }

        public string BgsWaypointsUrl { get; set; }

        public string BgsObservatoriesUrl { get; set; }

        public string ProxyUrl { get; set; }

        public string ProxyPort { get; set; }
    }
}
