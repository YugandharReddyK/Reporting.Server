using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSObservatoryDataType
    {
        [Description("Offset Required")]
        OffsetRequired = 0,
        
        [Description("Offset Not Required")]
        OffsetNotRequired = 1
    }
}
