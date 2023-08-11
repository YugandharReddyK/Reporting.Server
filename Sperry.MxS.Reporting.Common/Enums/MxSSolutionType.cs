using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSolutionType
    {
        
        [Description("SC")]
        ShortCollar = 0,
        
        [Description("LC")]
        LongCollar = 1,
        
        Cazandra = 2
    }
}
