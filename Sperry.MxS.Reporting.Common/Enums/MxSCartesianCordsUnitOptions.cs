using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSCartesianCordsUnitOptions
    {
        [Description("auto")]
        Auto,
       
        [Description("meters")]
        Metric,
        
        [Description("feet")]
        English,
        
        [Description("US feet")]
        USFeet
    }
}
