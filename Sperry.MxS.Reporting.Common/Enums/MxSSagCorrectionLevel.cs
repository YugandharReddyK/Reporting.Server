using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSagCorrectionLevel
    {
        [Description("None")]
        None = 0,
        
        [Description("Sag 1")]
        Sag1 = 1,
        
        [Description("Sag 2")]
        Sag2 = 2,
        
        [Description("Sag 3")]
        Sag3 = 3
    }
}
