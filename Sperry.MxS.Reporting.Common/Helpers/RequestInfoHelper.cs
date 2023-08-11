using Microsoft.AspNetCore.Http;
using Sperry.MxS.Core.Common.Models.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public static class RequestInfoHelper
    {
        public static string GetEmailFromContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var userToken))
                {
                    string jwt = userToken.ToString().Substring(7);
                    return GetEmailFromToken(jwt);
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static string GetEmailFromToken(string jwt)
        {
            var handlerx = new JwtSecurityTokenHandler();
            var email = handlerx.ReadJwtToken(jwt).Payload["email"];
            return email.ToString();
        }

        public static string GetEmailFromRequest(HttpRequest httpRequest)
        {
            try
            {
                if (httpRequest != null)
                {
                    return httpRequest.Headers["Authorization"].ToString().Split(' ')[1];
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
