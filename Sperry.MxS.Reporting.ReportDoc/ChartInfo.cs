using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class ChartInfo
    {
        public string ChartName { get; set; }
        
        public double? YAxisMin { get; set; }
       
        public double? YAxisMax { get; set; }

        public double? XAxisMin { get; set; }

        public double? XAxisMax { get; set; }

        public int? XAxisIntervalNumber { get; set; }
    }

}
