using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSurveyType
    {
        
        [Description("Undefined")]
        Undefined, //default
        
        [Description("CheckShot")]
        CheckShot,
        
        [Description("Rot.Checkshot")]
        RotCheckshot,
        
        [Description("Definitive")]
        Definitive,
        
        [Description("Secondary")]
        Secondary
    }
}
