using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSTimeOffSet
    {
        DateTime DateTime { get; set; }

        long? RigTimeOffset { get; set; }

        DateTime? GetAtomicRigTime();

    }
}
