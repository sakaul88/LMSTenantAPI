using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services.Token
{
    public static class JsonWebTokenExtensions
    {
        public static void AddJti(this ICollection<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }

        public static void AddClaims(this ICollection<Claim> claims, List<Claim> userClaims)
        {
            userClaims.ToList().ForEach(claim => claims.Add(new Claim(claim.Type, claim.Value)));
        }

        public static void AddSub(this ICollection<Claim> claims, string sub)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));
        }
    }
}
