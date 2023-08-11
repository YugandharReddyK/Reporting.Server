using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Logging.Interface
{
    public interface ILogMessage
    {
        string LogFileName { get; set; }

        
        void LogMessage(MxSLogLevel logLevel, string category, string message);

        void LogMessage(MxSLogLevel logLevel, string message);

    }
}
