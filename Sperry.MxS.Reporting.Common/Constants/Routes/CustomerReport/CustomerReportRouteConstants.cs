using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Constants.Routes.CustomerReport
{
    public static class CustomerReportRouteConstants
    {
        public const string BaseUrl = "api/v1";

        public const string GetImagesForTemplates = "CustomerReport/GetImagesForTemplates";

        public const string DownloadMetaData = "CustomerReport/DownloadMetaData";

        public const string GetChartsForTemplates = "CustomerReport/GetChartsForTemplates";

        public const string GenerateFromMasterTemplate = "CustomerReport/GenerateFromMasterTemplate";

        public const string GenerateFromSubReportTemplates = "CustomerreportsubReport/{wellid}";
    }
}
