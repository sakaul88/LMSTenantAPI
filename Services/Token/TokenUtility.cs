using DeviceManager.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services.Token
{
    public static class TokenUtility
    {
        public static Claim[] GenerateClaims(AuthUser authenticatedUser)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, authenticatedUser.firstName),
                //new Claim(ClaimTypes.Country, authenticatedUser.countryName),
                new Claim(ClaimsConstants.ProfileId, authenticatedUser.FkProfileId.ToString()),
                new Claim(ClaimsConstants.UserId, authenticatedUser.uid.ToString())
            };
        }

        public static Claim[] GenerateClaimsFromPayload(Dictionary<string, object> authenticatedUser)
        {
            //var claims = new Claim[];
            //return authenticatedUser.ToList().ForEach(claim => claims.AddClaims(new Claim(claim.GetType(), claim.Value)));

            return new Claim[]
            {
                new Claim(ClaimTypes.Name, authenticatedUser[ClaimTypes.Name].ToString()),
                new Claim(ClaimTypes.Country, authenticatedUser[ClaimTypes.Country].ToString()),
                new Claim(ClaimsConstants.EmployeeId, authenticatedUser[ClaimsConstants.EmployeeId].ToString()),
                new Claim(ClaimsConstants.TenantId, authenticatedUser[ClaimsConstants.TenantId].ToString()),
                new Claim(ClaimsConstants.fkEmpId, authenticatedUser[ClaimsConstants.fkEmpId].ToString()),
                new Claim(ClaimsConstants.ProfileId, authenticatedUser[ClaimsConstants.ProfileId].ToString()),
                new Claim(ClaimsConstants.UserId, authenticatedUser[ClaimsConstants.UserId].ToString())
            };
        }
    }
}
