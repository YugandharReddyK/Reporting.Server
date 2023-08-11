using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.ReportDoc.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc
{
    public class HalReport
    {
        public MxSReportResultMessage ResultMessage {get;set;}

        public List<string> ErrorMessages
        {
            get { return AddinBase.ErrorMessages; }
            //set { errorMessages = value; }
        }

        public Uri ReturnUri { get; set; }

        public HalReport()
        {
        }
    }
}
