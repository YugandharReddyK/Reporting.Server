using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSTimeZoneTypes
    {
        [Description("Rig Time Zone")]
        RigTimeZone = 0,
        
        [Description("Client Time Zone")]
        ClientTimeZone = 1,
        
        [Description("UTC Time Zone")] UTCTimeZone = 2,
    }
}
