using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Requests
{
    [Serializable]
    public class ExternalWebRequest
    {
        public ExternalWebRequest()
        {
            ProxyDomain = CredentialCache.DefaultNetworkCredentials.Domain;
            ProxyPassword = CredentialCache.DefaultNetworkCredentials.Password;
            ProxyUserName = CredentialCache.DefaultNetworkCredentials.UserName;
            Timeout = TimeSpan.Zero;
        }

        public ExternalWebRequest(ExternalWebRequest requestToCopy)
        {
            ProxyDomain = requestToCopy.ProxyDomain;
            ProxyPassword = requestToCopy.ProxyPassword;
            ProxyUserName = requestToCopy.ProxyUserName;
            ProxyUrl = requestToCopy.ProxyUrl;
            Url = requestToCopy.Url;
            UserName = requestToCopy.UserName;
            Password = requestToCopy.Password;
            Timeout = requestToCopy.Timeout;
        }

        public string Url { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string ProxyUserName { get; set; }
        
        public string ProxyPassword { get; set; }
        
        public string ProxyDomain { get; set; }
        
        public string ProxyUrl { get; set; }

        public TimeSpan Timeout { get; set; }
    }

    public class BgsExternalWebRequest : ExternalWebRequest
    {
        public string BgsWebSiteName { get; set; }
    }
}
