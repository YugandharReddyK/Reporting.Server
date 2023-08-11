using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Admin
{
    [Serializable]
    //[JsonObject(MemberSerialization.OptIn, IsReference = true)]
    [Export(typeof(MonitorConfig))]
    public class MonitorConfig
    {
        public MonitorConfig() 
        { }

        public MonitorConfig(MonitorConfig config)
        {
            this.ServerName = config.ServerName;
            this.DriveLetter = config.DriveLetter;
        }

        public string ServerName { get; set; }

        public string DriveLetter { get; set; }

    }
}
