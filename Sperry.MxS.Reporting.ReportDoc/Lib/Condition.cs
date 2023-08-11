using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Lib
{
    [Serializable]
    public class Condition
    {
        public string Table { get; set; }

        public string Name { get; set; }

        public string ConditionDescription { get; set; }

        public string Syntax { get; set; }

        public string SampleValue { get; set; }

        public bool EvaluateAllRows { get; set; }

    }
}
