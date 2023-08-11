using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sperry.MxS.Reporting.Common.Services
{
    public abstract class CommunicatorServiceBase
    {
        private readonly ILogger _logger;   
        public abstract string URL { get; protected set; }

        private HttpClient _httpClient;

        public CommunicatorServiceBase(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CommunicatorServiceBase>();
        }

        public ResultObject<T> GetServerResponse<T>(string url, string token = "")
        {
            ResultObject<T> result = new ResultObject<T>();
            try
            {
                HttpClient httpClient = GetHttpClient(token);
                HttpResponseMessage responseToolsSupported = httpClient.GetAsync(url).Result;
                if (responseToolsSupported.IsSuccessStatusCode)
                {
                    string data = responseToolsSupported.Content.ReadAsStringAsync().Result;

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<ResultObject<T>>(data);
                    }
                }
                else
                {
                    ResultObject<T> data = null;
                    try
                    {
                        data = JsonConvert.DeserializeObject<ResultObject<T>>(responseToolsSupported.Content.ReadAsStringAsync().Result);
                    }
                    catch { }
                    if (data != null)
                    {
                        result.AddError(data.Message);
                    }
                    else
                    {
                        result.AddError(responseToolsSupported.ReasonPhrase ?? "Failed to talk to service and reason pharse is missing");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message ?? "");
                _logger.LogError(ex, $"Fatal: Failed to get the data from service {ex.Message}");
            }
            return result;
        }

        public ResultObject<byte[]> GetServerResponseWell<T>(string url, string token = "")
        {
            ResultObject<byte[]> result = new ResultObject<byte[]>();
            try
            {
                HttpClient httpClient = GetHttpClient(token);
                HttpResponseMessage responseToolsSupported = httpClient.GetAsync(url).Result;
                if (responseToolsSupported.IsSuccessStatusCode)
                {
                    //For AzureFunctions
                    //var data = responseToolsSupported.Content.ReadAsByteArrayAsync().Result;
                    // For Server
                    var result2 = responseToolsSupported.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
                    var data = Convert.FromBase64String(result2);

                    if (data != null)
                    {
                        result.Data = data;
                        return result;
                    }
                }
                else
                {
                    ResultObject<T> data = null;
                    try
                    {
                        data = JsonConvert.DeserializeObject<ResultObject<T>>(responseToolsSupported.Content.ReadAsStringAsync().Result);
                    }
                    catch { }
                    if (data != null)
                    {
                        result.AddError(data.Message);
                    }
                    else
                    {
                        result.AddError(responseToolsSupported.ReasonPhrase ?? "Failed to talk to service and reason pharse is missing");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message ?? "");
                _logger.LogError(ex, $"Fatal: Failed to get the data from service {ex.Message}");
            }
            return result;
        }

        public ResultObject<T> PostToServer<T>(string url, object parameter, string token = "")
        {
            ResultObject<T> result = new ResultObject<T>();
            try
            {
                HttpClient httpClient = GetHttpClient(token);
                HttpRequestMessage httpRequestMessage = null;
                if (parameter is MultipartFormDataContent)
                {
                    httpRequestMessage = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(httpClient.BaseAddress, url),
                        Method = HttpMethod.Post,
                        Content = (MultipartFormDataContent)parameter
                    };
                }
                else
                {
                    httpRequestMessage = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(httpClient.BaseAddress, url),
                        Method = HttpMethod.Post,
                        Content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json")
                    };
                }
                HttpResponseMessage responseToolsSupported = httpClient.SendAsync(httpRequestMessage).Result;
                if (responseToolsSupported.IsSuccessStatusCode)
                {
                    var data = responseToolsSupported.Content.ReadAsStringAsync().Result;

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<ResultObject<T>>(data);
                    }
                }
                else
                {
                    ResultObject<T> data = null;
                    try
                    {
                        data = JsonConvert.DeserializeObject<ResultObject<T>>(responseToolsSupported.Content.ReadAsStringAsync().Result);
                    }
                    catch { }
                    if (data != null)
                    {
                        result.AddError(data.Message);
                    }
                    else
                    {
                        result.AddError(responseToolsSupported.ReasonPhrase ?? "Failed to talk to service and reason pharse is missing");
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message ?? "");
                _logger.LogError(ex, $"Fatal: Failed to PostToServer for service {ex.Message}");

            }
            return result;
        }

        private HttpClient GetHttpClient(string token)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 5, 0);
            _httpClient.BaseAddress = new Uri(URL);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return _httpClient;
        }
    }
}
