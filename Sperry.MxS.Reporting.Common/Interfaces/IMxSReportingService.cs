using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Core.Common.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public interface IMxSReportingService
    {
        ResultObject<List<CustomerImages>> GetAllCustomerImages(Guid StoreId);

        ResultObject<CustomerReportTemplateData> GetReportTemplateData(Guid templateId);
    }
}
