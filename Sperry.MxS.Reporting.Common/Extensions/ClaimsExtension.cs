using Sperry.MxS.Core.Common.Models.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Sperry.MxS.Core.Common.Extensions
{
    public static class ClaimsExtension
    {
        public static MxSUser GetUser(this IEnumerable<Claim> claims)
        {
            return new MxSUser(claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)));
            //MxSUser user = new MxSUser()
            //{
            //    FirstName = "Yugandhar",
            //    LastName = "Reddy",
            //    Email = "Yugandhar.Reddy@halliburton.com"
            //};
            //return user;
        }
    }
}
