using Sperry.MxS.Core.Common.Models.Results;
using Sperry.MxS.Reporting.Common.Constants.Routes.CustomerReport;
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
    public class MxSWellService : IMxSWellService
    {
        private readonly IMxSDataProviderCommunicator _dataProviderCommunicator;

        public MxSWellService(IMxSDataProviderCommunicator dataProviderCommunicator)
        {
            _dataProviderCommunicator = dataProviderCommunicator;
        }

        public ResultObject<byte[]> GetWell(Guid wellId)
        {
            return _dataProviderCommunicator.GetServerResponseWell<byte[]>($"{DataProviderRouteConstants.GetWell}?wellId={wellId}");
        }
    }
}
