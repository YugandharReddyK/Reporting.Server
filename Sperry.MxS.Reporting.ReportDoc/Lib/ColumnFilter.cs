using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    public class ColumnFilter
    {
        public string OpenParen { get; set; }

        public MergeField Column { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public string CloseParen { get; set; }

        public string AndOr { get; set; }

    }
}
