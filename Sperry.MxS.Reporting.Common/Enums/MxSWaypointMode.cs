using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSWaypointMode
    {
        [Description("MFM Static")]
        WayPoint = 1,
        
        [Description("MFM DC")]
        DownwardContinuation = 2
    }
}
