using Sperry.MxS.Core.Common.Models.CustomerReport;
using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Reporting.Common.Constants.Routes.DataProvider;
using Sperry.MxS.Reporting.Common.Interfaces;
using Sperry.MxS.Reporting.Common.Interfaces.IServiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Services
{
    public class MxSReportingService : IMxSReportingService
    {
        private readonly IMxSDataProviderCommunicator _dataProviderCommunicator;
        public MxSReportingService(IMxSDataProviderCommunicator dataProviderCommunicator)
        {
            _dataProviderCommunicator = dataProviderCommunicator;
        }

        public ResultObject<List<CustomerImages>> GetAllCustomerImages(Guid storeId)
        {
            return _dataProviderCommunicator.GetServerResponse<List<CustomerImages>>($"{DataProviderRouteConstants.GetAllCustomerImages}?storeId={storeId}");
        }

        public ResultObject<CustomerReportTemplateData> GetReportTemplateData(Guid templateId)
        {
            return _dataProviderCommunicator.GetServerResponse<CustomerReportTemplateData>($"{DataProviderRouteConstants.DownloadTemplate}?templateId={templateId}");
        }
    }
}
