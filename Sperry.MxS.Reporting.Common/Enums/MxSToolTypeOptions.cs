using System.ComponentModel;
using System.Runtime.Serialization;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSToolTypeOptions
    {
        DM,
        
        PCD,
        
        PCDC,
        
        [Description("PCD-P4M")]
        PCD_P4M,
        
        [Description("PM-III")]
        PM_III,
        
        [Description("Not Available")]
        NotAvailable
    }
}
