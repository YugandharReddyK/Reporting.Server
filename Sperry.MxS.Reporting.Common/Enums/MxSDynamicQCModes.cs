using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSDynamicQCModes
    {
        [Description("Basic mode")]
        BasicMode = 0,
        
        [Description("Advanced mode")]
        AdvancedMode = 1,
        
        [Description("User defined mode")]
        UserDefinedMode = 2
    }
}
