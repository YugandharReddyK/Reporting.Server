using Newtonsoft.Json;
using Sperry.MxS.Core.Common.Enums;
using Sperry.MxS.Core.Common.Models;
using Sperry.MxS.Core.Common.Models.Security;
using Sperry.MxS.Core.Common.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Security
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class AppUser : DataModelBase
    {
        public AppUser()
        { }

        public AppUser(MxSUser user)
        {
            NetworkId = user.NetworkId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Status = MxSResourceStatus.Active;
            UserType = user.Type;
        }
        [JsonProperty]
        public string NetworkId{get;set;}

        [JsonProperty]
        public bool IsActive { get; set; } = true;

        [JsonProperty]
        public string Email{get;set; }

        [JsonProperty]
        public string FirstName { get; set; }

        [JsonProperty]
        public string LastName { get; set; }

        [JsonProperty]
        public DateTime LastLoginDate { get; set; }

        [JsonProperty]
        public MxSResourceStatus Status { get; set; }

        [JsonProperty]
        public MxSUserType UserType { get; set; }
    }
}
