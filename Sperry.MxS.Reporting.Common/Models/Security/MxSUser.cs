using Sperry.MxS.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Models.Security
{
    public class MxSUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string NetworkId { get; set; }

        public MxSUserType Type { get; set; }

        public string Domain { get; set; }

        public string DisplayName { get; set; }

        public string[] Roles { get; set; }

        public MxSUser()
        {
        }

        public MxSUser(string firstName, string lastName, string email, string networkId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            NetworkId = networkId;
            if (!string.IsNullOrEmpty(email) && email.EndsWith("@halliburton.com", StringComparison.CurrentCultureIgnoreCase))
            {
                Type = MxSUserType.Internal;
            }
            else
            {
                Type = MxSUserType.External;
            }
        }

        public MxSUser(IEnumerable<KeyValuePair<string, string>> claims)
        {
            FirstName = claims.FirstOrDefault((k) => k.Key == "GivenName" || k.Key == "FirstName").Value;
            LastName = claims.FirstOrDefault((k) => k.Key == "FamilyName" || k.Key == "LastName").Value;
            Email = claims.FirstOrDefault((k) => k.Key == ClaimTypes.Email || k.Key == "Email").Value;
            NetworkId = claims.FirstOrDefault((k) => k.Key == "preferred_username" || k.Key == "NetworkId").Value;
            if (Email.EndsWith("@Halliburton.com", StringComparison.InvariantCultureIgnoreCase))
            {
                Domain = "Halamerica";
                Type = MxSUserType.Internal;
            }
            else
            {
                Domain = "xnet";
                Type = MxSUserType.External;
            }
        }
    }
}
