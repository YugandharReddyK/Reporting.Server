using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Communication
{
    public class MxSServiceCommunicationInfo
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public string SignalR { get; set; }
    }
}
