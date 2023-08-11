using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Requests;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSCustomerReportService
    {
        ResultObject<CustomerReportTemplateData> DownloadMetaData(Guid id);
       
        //ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> GetChartsForTemplates(List<Guid> templates);
       
        //ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> GetImagesForTemplates(Guid wellId, IEnumerable<Guid> templates);
       
        //CustomerReportResult<CustomerReport> GenerateMasterTemplate(Guid templateId, Guid wellId, ReportParameters reportParameters, string username, bool isProcessing);

        CustomerReportResult<CustomerReport> GenerateSubReportTemplates(IEnumerable<CustomerReportTemplate> templates, Guid wellId, ReportParameters reportParameters, string userName);

        //New Implementation
        ResultObject<Dictionary<Guid, IEnumerable<CustomerImages>>> GetImagesForTemplates(JobRequest request);

        ResultObject<Dictionary<Guid, Dictionary<string, CustomerReportChartInfo>>> GetChartsForTemplates(List<CustomerReportTemplateData> templatesData);

        CustomerReportResult<CustomerReport> GenerateMasterTemplate(JobRequest request, string username, bool isProcessing);

    }
}
