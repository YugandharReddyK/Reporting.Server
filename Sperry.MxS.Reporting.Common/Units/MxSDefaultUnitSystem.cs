using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Units
{
    public class MxSDefaultUnitSystem
    {
        public string Name { get; set; }
        
        public List<MxSUnitSystemMap> Map { get; set; } = new List<MxSUnitSystemMap>();
    }
}
