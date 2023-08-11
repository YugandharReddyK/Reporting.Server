using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSWorkBenchAzimuthProperties
    {
        double? AzSc { get; set; }

        double? AzLc { get; set; }

        double? AzLcMAv { get; set; }

        double? AzScTrue { get; set; }

        double? AzLcTrue { get; set; }

        double? AzLcMavTrue { get; set; }
    }
}
