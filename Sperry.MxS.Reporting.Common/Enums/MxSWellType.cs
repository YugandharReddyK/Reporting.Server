using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    
    public enum MxSWellType
    {
        [Description("N/A")]
        NA = 0,
        
        [Description("Vertical")]
        Vertical = 1,
       
        [Description("Deviated")]
        Deviated = 2,
        
        [Description("Horizontal")]
        Horizontal = 3,
        
        [Description("Multilateral")]
        Multilateral = 4,
        
        [Description("Extended Reach")]
        ExtendedReach = 5
    }
}
