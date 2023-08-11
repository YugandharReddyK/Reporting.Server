using Microsoft.Extensions.Options;
using Sperry.MxS.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Interfaces;

namespace Sperry.MxS.Core.Common.Utilities
{
    public class MxSConnectionUtility: IMxSConnectionUtility
    {
        private readonly ProxyInfo _proxyInfo;

        public MxSConnectionUtility(IOptions<ProxyInfo> proxyInfo)
        {
            this._proxyInfo = proxyInfo.Value;
        }

        public string Verify(BGSWebsiteInfo info)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(info.Websiteurl + @"?C=M;O=D");
            request.Credentials = new NetworkCredential(info.Username, info.Password);
            try
            {
                if (!string.IsNullOrEmpty(_proxyInfo.ProxyUrl))
                {
                    var url = _proxyInfo.ProxyUrl.Split(':');
                    var proxy = new WebProxy(url[0], Convert.ToInt32(url[1]));
                    if (string.IsNullOrEmpty(_proxyInfo.CredentialUserName))
                    {
                        proxy.Credentials = CredentialCache.DefaultCredentials;
                    }
                    else
                    {
                        proxy.Credentials = new NetworkCredential(_proxyInfo.CredentialUserName, _proxyInfo.CredentialPassword);
                    }
                    request.Proxy = proxy; //should use proxy here -Lijun
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
