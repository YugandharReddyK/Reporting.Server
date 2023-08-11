using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSSurveyStatus
    {
        [Description("New")]
        New = 0,
        
        [Description("Processed")]
        Processed = 1,
        
        [Description("Error")]
        Error = 2,
        
        [Description("Back Corrected")]
        BackCorrected = 3,
        
        [Description("Wait Obs")]
        WaitObs = 4,
        
        [Description("Wait Obs over 5 minutes")]
        WaitObsOver5Minutes = 5,
        
        [Description("Wait Obs over 10 minutes")]
        WaitObsOver10Minutes = 6,
        
        [Description("Wait Obs over 20 minutes")]
        WaitObsOver20Minutes = 7
    }
}
