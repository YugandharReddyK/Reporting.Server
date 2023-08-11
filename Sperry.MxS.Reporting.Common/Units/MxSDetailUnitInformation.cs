using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Units
{
    public class MxSDetailUnitInformation
    {
        public string Name { get; set; }
        
        public string Measurement { get; set; }
        
        public double Factor { get; set; }
        
        public double Offset { get; set; }
        
        public string HeaderDisplayValue { get; set; }
    }
}
