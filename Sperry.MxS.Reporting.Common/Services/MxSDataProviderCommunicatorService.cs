using Microsoft.Extensions.Logging;
using Sperry.MxS.Reporting.Common.Interfaces.IServiceManager;
using Sperry.MxS.Reporting.Common.Models.Comms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Services
{
    public class MxSDataProviderCommunicatorService : CommunicatorServiceBase, IMxSDataProviderCommunicator
    {
        public MxSDataProviderCommunicatorService(ILoggerFactory loggerFactory, MxSDataProviderCommunicationInfo mxSDataProviderCommunicationInfo)
            : base(loggerFactory)
        {
            URL = mxSDataProviderCommunicationInfo.Address;
        }

        public override string URL { get; protected set; }
    }
}
