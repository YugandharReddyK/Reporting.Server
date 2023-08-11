using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Enums
{
    [DataContract]
    public enum MxSState
    {
        Unchanged = 0,
        Added = 1,
        Deleted = 2,
        Modified = 3,
    }
}
