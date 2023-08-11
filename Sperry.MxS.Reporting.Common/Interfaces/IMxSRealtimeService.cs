using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces
{
    public  interface IMxSRealtimeService
    {
        bool IsConnected();

        void BroadcastCustomerReportGeneratedForWell(Guid wellId);
    }
}
