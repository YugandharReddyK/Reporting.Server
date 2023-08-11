using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Workbench.AziError.Base
{
    public abstract class AziErrorBase : WorkBenchDataModel
    {
        public double? AziErrorInclination { get; set; }

        public double? AziErrorAzimuth { get; set; }

        public double? AziErrorAzimuthMagnetic { get; set; }
    }

}
