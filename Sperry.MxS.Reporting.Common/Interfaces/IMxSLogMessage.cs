using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Interfaces
{
    public interface IMxSLogMessage
    {
        string LogFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="category"></param>
        /// <param name="message"></param>
        void LogMessage(MxSLogLevel logLevel, string category, string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        void LogMessage(MxSLogLevel logLevel, string message);
    }
}
