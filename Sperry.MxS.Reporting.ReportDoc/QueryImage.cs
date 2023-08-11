using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class QueryImage
    {
        public string TableName { get;set; }

        public string ImageName {get; set; }

        public decimal XSize {get; set; }
       
        public decimal YSize{get; set; }
    }
}
