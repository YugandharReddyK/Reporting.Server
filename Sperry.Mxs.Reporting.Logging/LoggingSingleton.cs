using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Logging
{
    public class LoggingSingleton
    {
        private const string DEFAULT_LOG_FILE_NAME = "DesktopReportingLog.log";
        private static readonly Lazy<LoggingSingleton> _lazyInstance = new Lazy<LoggingSingleton>(() => new LoggingSingleton());
        //private HalFrameworkLogging _HalFrameworkLogging = null;
        private MxSLoggingProvider _loggingProvider = MxSLoggingProvider.HomeGrown;

        private ILogMessage _logAdapter;

        public string LogFileName
        {
            get { return _logAdapter.LogFileName; }
            set { _logAdapter.LogFileName = value; }
        }

        public MxSLoggingProvider LoggingProvider
        {
            get { return _loggingProvider; }
            set { _loggingProvider = value; }
        }

        public MxSLogLevel RequestedLogLevel { get; set; }


        public static LoggingSingleton Instance
        {
            get { return _lazyInstance.Value; }
        }

        private LoggingSingleton()
        {
            _logAdapter = new LogAdapter(LoggingProvider, DEFAULT_LOG_FILE_NAME);
        }

        public void LogMessage(MxSLogLevel logLevel, string category, string message, bool enableLog)
        {
            if (!enableLog)
            {
                return;
            }
            if (logLevel <= RequestedLogLevel)
            {
                _logAdapter.LogMessage(logLevel, category, message);
            }
            //Core.Logging.Interface.HalTraceType value2 = (Core.Logging.Interface.HalTraceType)Enum.Parse(typeof(Core.Logging.Interface.HalTraceType), logLevel.ToString());
            // _HalFrameworkLogging.LogMessage(value2,category,message);
        }

        public void LogMessage(MxSLogLevel logLevel, string message, bool enableLog)
        {
            if (!enableLog)
            {
                return;
            }

            if (logLevel <= RequestedLogLevel)
            {
                _logAdapter.LogMessage(logLevel, message);
            }
            //Core.Logging.Interface.HalTraceType value2 = (Core.Logging.Interface.HalTraceType)Enum.Parse(typeof(Core.Logging.Interface.HalTraceType), logLevel.ToString());
            // _HalFrameworkLogging.LogMessage(value2, message);
        }

        public void LogMessage(Exception exception, bool enableLog)
        {
            if (!enableLog)
            {
                return;
            }
            // LogMessage(LogLevel.Error, string.Format("{0} - {1}", exception.Message, exception.StackTrace));
            //LogMessage(LogLevel.Error, string.Format("{0} - {1}", exception.Message, exception.StackTrace));
            //_HalFrameworkLogging.LogMessage(exception);
        }
    }
}
