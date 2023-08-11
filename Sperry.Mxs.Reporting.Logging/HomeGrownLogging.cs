using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Reporting.Logging.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Reporting.Logging
{
    public class HomeGrownLogging : ILogMessage
    {
        private string _logPath;
        private string _logFileName;

        public string LogFileName
        {
            get { return _logFileName; }
            set
            {
                _logFileName = value;
                _logPath = Path.Combine(Path.GetTempPath(), LogFileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public HomeGrownLogging(string logFileName)
        {
            LogFileName = logFileName;

            if (File.Exists(_logPath))
            {
                string[] lines = File.ReadAllLines(_logPath);

                if (lines.Length > 1000)
                {
                    string[] newLines = lines.Skip(lines.Length - 1000).ToArray();

                    File.WriteAllLines(_logPath, newLines);
                }
            }

        }

        public void LogMessage(MxSLogLevel logLevel, string category, string message)
        {
            if (string.IsNullOrEmpty(category))
            {
                LogMessage(logLevel, message);
            }
            else
            {
                string logLevelName = Enum.GetName(typeof(MxSLogLevel), logLevel);

                WriteLog(string.Format("DateTime: {0} | Category: {1} | LogLevel: {2} | Message: {3}", DateTime.Now, category, logLevelName, message));
            }
        }

        public void LogMessage(MxSLogLevel logLevel, string message)
        {
            string logLevelName = Enum.GetName(typeof(MxSLogLevel), logLevel);

            WriteLog(string.Format("DateTime: {0} | LogLevel: {1} | Message: {2}", DateTime.Now, logLevelName, message));
        }

        private void WriteLog(string message)
        {
            File.AppendAllText(_logPath, message + Environment.NewLine);
        }
    }
}
