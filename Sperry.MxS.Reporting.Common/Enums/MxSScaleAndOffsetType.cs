using System.ComponentModel;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSScaleAndOffsetType
    {
        [Description("None")]
        None = 0,
        
        [Description("Reverse Polarity")]
        ReversePolarity = 1,
       
        [Description("Scale")]
        Scale = 2,
       
        [Description("Offset")]
        Offset = 3,
       
        [Description("Scale and Offset")]
        ScaleandOffset = 4,
    }
}
