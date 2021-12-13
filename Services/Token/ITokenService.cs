using System.Collections.Generic;
using System.Security.Claims;

namespace DeviceManager.Api.Services
{
    public interface ITokenService
    {
         string GenerateAccessToken(IEnumerable<Claim> claims);         
         string GenerateRefreshToken();    
         ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Dictionary<string, object> Decode(string token);
    }
}