using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSCustomerReportDataProvider
    {
        //string GetMetaData(Well well);

        bool CreateReportDataFile(Well well, string outputFilePath, ReportParameters reportParameters);

        void ResetImageCacheForWell(Guid wellId);
    }
}
