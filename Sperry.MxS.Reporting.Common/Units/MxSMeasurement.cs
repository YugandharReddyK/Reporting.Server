using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Units
{
    public class MxSMeasurement
    {
        public string Name { get; set; }
        
        public List<MxSDetailUnitInformation> Unit { get; set; } = new List<MxSDetailUnitInformation>();
    }
}
