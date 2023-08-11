using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Constants;
//using Sperry.MxS.Core.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Configurations
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class ApiServerSettings : DataModelBase
    {
        private UriBuilder _uriBuilder;

        private string _signalRUrl = string.Empty;

        [JsonProperty]
        public string BaseResource { get; set; }
        
        [JsonProperty]
        public string BgsBaseResource { get; set; }
        
        [JsonProperty]
        public string HostUrl { get; set; }
        
        [JsonProperty]
        public string Name { get; set; }
        
        [JsonProperty]
        public string Password { get; set; }
        
        [JsonProperty]
        public string BgsUserName { get; set; }
        
        [JsonProperty]
        public string BgsPassword { get; set; }
        
        [JsonProperty]
        public string BgsObservatoryUrl { get; set; }
        
        [JsonProperty]
        public string ProxyUrl { get; set; }
        
        [JsonProperty]
        public int Port { get; set; }
        
        [JsonProperty]
        public bool IsProductionServer { get; set; }
        
        [JsonProperty]
        public string Username { get; set; }
        
        [JsonProperty]
        public string LoginUrl { get; set; }
        
        [JsonProperty]
        public string LoggedInUrl { get; set; }
        
        [JsonProperty]
        public string LogoutUrl { get; set; }
        
        [JsonProperty]
        public string SignalRUrl
        {
            get
            {
                _signalRUrl = GetSignalRUrlString();
                return _signalRUrl;
            }
            set
            {
                try
                {
                    _signalRUrl = GetUrlString(value);
                }
                catch
                {
                    _signalRUrl = string.Empty;
                }

            }
        }

        public ApiServerSettings()
        {
            _uriBuilder = new UriBuilder();
            HostUrl = "localhost";
            SignalRUrl = @"https:\\localhost";
            Name = "local";
            Port = 80;
            BaseResource = $"{MxSApiEndPointConstants.ServersRoot}{MxSApiEndPointConstants.BaseRoot}";
            BgsBaseResource = $"/BgsServer{MxSApiEndPointConstants.BaseRoot}";
            Username = string.Empty;
            Password = string.Empty;
            BgsUserName = "";
            BgsPassword = "";
            BgsObservatoryUrl = "";
            ProxyUrl = "";
            LoginUrl = $"{MxSApiEndPointConstants.ServersRoot}{"/WinLogin?Source=Internal"}";
            LoggedInUrl = $"{MxSApiEndPointConstants.ServersRoot}{"/Internal"}";
            LogoutUrl = $"{MxSApiEndPointConstants.ServersRoot}{"/Logout"}";
        }

        public ApiServerSettings(ApiServerSettings apiServerSettings)
        {
            _uriBuilder = apiServerSettings._uriBuilder;
            HostUrl = apiServerSettings.HostUrl;
            SignalRUrl = apiServerSettings.SignalRUrl;
            Name = apiServerSettings.Name;
            Port = apiServerSettings.Port;
            BaseResource = apiServerSettings.BaseResource;
            BgsBaseResource = apiServerSettings.BgsBaseResource;
            Username = apiServerSettings.Username;
            Password = apiServerSettings.Password;
            BgsUserName = apiServerSettings.BgsUserName;
            BgsPassword = apiServerSettings.BgsPassword;
            BgsObservatoryUrl = apiServerSettings.BgsObservatoryUrl;
            ProxyUrl = apiServerSettings.ProxyUrl;
            LoginUrl = apiServerSettings.LoginUrl;
            LoggedInUrl = apiServerSettings.LoggedInUrl;
            LogoutUrl = apiServerSettings.LogoutUrl;
        }

        private string GetUrlString(string value)
        {
            UriBuilder uriBuilder = new UriBuilder(value);
            return uriBuilder.ToString();
        }

        private string GetSignalRUrlString()
        {
            string scheme = ((Port == 443) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
            UriBuilder uriBuilder = new UriBuilder(scheme, HostUrl, Port);
            return uriBuilder.ToString() + "Server";
        }

        public Uri GetBaseUri()
        {
            _uriBuilder.Port = Port;
            _uriBuilder.Host = HostUrl;
            _uriBuilder.Scheme = ((Port == 443) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
            return _uriBuilder.Uri;
        }
    }
}
