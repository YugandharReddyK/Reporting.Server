using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class BGSWebsiteInfo : DataModelBase
    {
        //ToDo:- This class needs to refactor to use DataModelBase

        [JsonProperty]
        public string Websiteurl { get; set; }
        
        [JsonProperty]
        public string Username { get; set; }
        
        [JsonProperty]
        public string Password { get; set; }
        
        [JsonProperty]
        public string SiteStatus { get; set; }
        
        [JsonProperty]
        public string Websitename { get; set; }

        public BGSWebsiteInfo()
        {
            State = MxSState.Added;
            SiteStatus = "Standby";
        }

        public void ChangeSiteStatus(string siteStatus)
        {
            SiteStatus = siteStatus;
            //this.SetState(MxSState.Modified);
        }
    }
}
