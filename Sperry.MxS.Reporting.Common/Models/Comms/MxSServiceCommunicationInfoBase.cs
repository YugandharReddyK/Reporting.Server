using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Models.Comms
{
    public abstract class MxSServiceCommunicationInfoBase
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public string SignalR { get; set; }
    }
}
