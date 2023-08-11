using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Sperry.MxS.Core.Common.Utilities
{
    public static class AppSettingsHelper
    {
        public static string ReadSetting(string key)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ReadSecureSetting(string key)
        {
            try
            {
                NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("secureAppSettings");
                return section[key] ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
