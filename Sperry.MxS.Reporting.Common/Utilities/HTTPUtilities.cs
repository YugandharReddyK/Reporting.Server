using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sperry.MxS.Core.Common.Requests;
using System.Net.Http;

namespace Sperry.MxS.Core.Common.Utilities
{
    public class HTTPUtilities
    {
        private static HttpClient _httpClient;

        private static HttpClient GetHttpClient(ExternalWebRequest parameters)
        {
            if (_httpClient == null)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler();
                httpClientHandler.UseProxy = true;
                httpClientHandler.Credentials = new NetworkCredential(parameters.UserName, parameters.Password);
                if (!string.IsNullOrEmpty(parameters.ProxyUrl))
                {
                    WebProxy webProxy = new WebProxy(parameters.ProxyUrl, BypassOnLocal: true);
                    if (!string.IsNullOrEmpty(parameters.ProxyUserName))
                    {
                        webProxy.Credentials = new NetworkCredential(parameters.ProxyUserName, parameters.ProxyPassword, parameters.ProxyDomain);
                    }
                    else
                    {
                        webProxy.UseDefaultCredentials = true;
                    }

                    httpClientHandler.Proxy = webProxy;
                }
                else
                {
                    httpClientHandler.Proxy = WebRequest.DefaultWebProxy;
                }

                _httpClient = new HttpClient(httpClientHandler);
            }

            return _httpClient;
        }

        public static async Task<string> GetWebpageResponseAsync(ExternalWebRequest parameters, string contentType = "")
        {
            HttpClient client = GetHttpClient(parameters);
            if (parameters.Timeout != TimeSpan.Zero)
            {
                client.Timeout = parameters.Timeout;
            }

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            }

            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = false
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{parameters.UserName}:{parameters.Password}")));
            return await (await client.GetAsync(parameters.Url, HttpCompletionOption.ResponseHeadersRead)).Content.ReadAsStringAsync();
        }

        public static async Task<string> GetWebpageSourceAsync(ExternalWebRequest parameters)
        {
            try
            {
                return await GetWebpageSource(parameters);
            }
            catch
            {
            }

            return string.Empty;
        }

        public static async Task<string> GetWebpageSource(ExternalWebRequest parameters)
        {
            HttpClient client = GetHttpClient(parameters);
            if (parameters.Timeout != TimeSpan.Zero)
            {
                client.Timeout = parameters.Timeout;
            }

            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = false
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{parameters.UserName}:{parameters.Password}")));
            HttpResponseMessage response = await client.GetAsync(parameters.Url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
