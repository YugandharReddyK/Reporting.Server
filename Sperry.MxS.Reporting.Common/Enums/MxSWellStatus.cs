using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    
    public enum MxSWellStatus
    {
        [Description("Pending Review")]
        PendingReview = 0,
        
        [Description("PreSpud")]
        PreSpud = 1,
        
        [Description("Active")]
        Active = 2,
        
        [Description("TD")]
        TD = 3,
       
        [Description("Finalized")]
        Finalized = 4
    }
}
