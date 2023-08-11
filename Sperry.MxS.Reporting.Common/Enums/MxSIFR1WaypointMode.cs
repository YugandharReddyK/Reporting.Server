using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSIFR1WaypointMode
    {
        [Description("MFM Static + IFR1 Static")]
        WayPoint = 1,
        
        [Description("MFM DC + IFR1 DC")]
        DownwardContinuation = 2
    }
}
