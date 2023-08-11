using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.CustomerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.ReportDoc.Interface
{
    public interface IMxSCustomerReport
    {
        void CreateReportDataFile(Well well, string outputPath, ReportParameters reportParameters);

        HalReport GenerateReport(Well well, string templatePath, string outputReportPath, ReportParameters reportParameters);

        HalReport GenerateReport(Well well, IEnumerable<CustomerReportTemplate> templates, string outputReportPath, ReportParameters reportParameters);

        Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>> GetChartsForTemplates(Dictionary<Guid, string> templates);

        //IEnumerable<string> GetImagesForTemplates(Guid wellId, IEnumerable<string> templates);

        Dictionary<string, string> GetMissingImagesForTemplates(Well well, IEnumerable<string> templates);

        IEnumerable<string> GetImagesForTemplates(byte[] compressedWell, IEnumerable<string> templates);
    }
}
