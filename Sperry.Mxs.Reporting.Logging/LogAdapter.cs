using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Logging
{
    public class LogAdapter : ILogMessage
    {
        private ILogMessage _logImplementer;
        private string _logFileName;

        public LogAdapter(MxSLoggingProvider loggingProvider, string logFileName)
        {
            LogFileName = logFileName;
            SetLogImplementer(loggingProvider);
        }

        public string LogFileName
        {
            get { return _logFileName; }
            set
            {
                _logFileName = value;

                if (_logImplementer != null)
                {
                    _logImplementer.LogFileName = _logFileName;
                }
            }
        }

        private void SetLogImplementer(MxSLoggingProvider loggingProvider)
        {
            switch (loggingProvider)
            {
                case MxSLoggingProvider.HomeGrown:
                    _logImplementer = new HomeGrownLogging(LogFileName);
                    break;
            }
        }

        public void LogMessage(MxSLogLevel logLevel, string category, string message)
        {
            _logImplementer.LogMessage(logLevel, category, message);
        }

        public void LogMessage(MxSLogLevel logLevel, string message)
        {
            _logImplementer.LogMessage(logLevel, message);
        }
    }
}
