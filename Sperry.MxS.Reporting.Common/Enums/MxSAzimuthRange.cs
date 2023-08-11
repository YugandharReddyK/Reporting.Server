using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    public enum MxSAzimuthRange
    {
        [EnumMember]
        ZeroTo360,

        [EnumMember]
        Minus180To180
    }
   
}
