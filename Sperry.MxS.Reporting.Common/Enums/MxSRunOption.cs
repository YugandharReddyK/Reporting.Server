using System.ComponentModel;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSRunOption
    {
        [Description("Static")]
        Static = 0,
       
        [Description("Start/End Depth")]
        Depth = 1,
        
        [Description("Start/End Time")]
        Time = 2,
        
        [Description("Last Run")]
        LastRun = 3
    }
}
