using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError
{
    public abstract class AziErrorOutputBase
    {
        public double? Depth { get; set; }

        public double? AziErrorInclination { get; set; }

        public double? AziErrorAzimuth { get; set; }

        public double? AziErrorAzimuthMagnetic { get; set; }
    }
}
