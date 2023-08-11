using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class ColumnSort
    {
        public MergeField Column { get; set; }
        public string Sort { get; set; }
        public int SortRank { get; set; }
    }
}
