using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class ConfigurationManagerHelper
    {
        public static MxSServiceCommunicationInfo MxSWebAppInfo { get; set; }

        public static MxSServiceCommunicationInfo MxSDataProviderInfo { get; set; }

        public static string ConnectionString { get; set; }
    }
}
