using Sperry.MxS.Core.Common.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Common.Interfaces.IServiceManager
{
    public interface IMxSCommunicatorBase
    {
        string URL { get; }

        ResultObject<T> PostToServer<T>(string url, object parameter, string token = "");

        ResultObject<T> GetServerResponse<T>(string url, string token = "");

        ResultObject<byte[]> GetServerResponseWell<T>(string url, string token = "");
    }
}
