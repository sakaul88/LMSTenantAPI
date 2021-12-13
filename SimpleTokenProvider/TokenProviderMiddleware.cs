using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BookRight.Api.SimpleTokenProvider
{
    /// <summary>
    /// Token generator middleware component which is added to an HTTP pipeline.
    /// This class is not created by application code directly,
    /// instead it is added by calling the <see cref="TokenProviderAppBuilderExtensions.UseSimpleTokenProvider(Microsoft.AspNetCore.Builder.IApplicationBuilder, TokenProviderOptions)"/>
    /// extension method.
    /// </summary>
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<TokenProviderMiddleware>();

            _options = options.Value;
            ThrowIfInvalidOptions(_options);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            _logger.LogInformation("Handling request: " + context.Request.Path);

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.Response.Headers.Add("Access-Control-Expose-Headers", new[] { "X-uid" });
            var eresponse = new { errMsg = "" };
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];
            var ref1 = context.Request.Form["ref"];

            AuthUser identity = await _options.IdentityResolver(username, password, ref1);
            if (identity.isAuth == false)
            {
                context.Response.StatusCode = 400;
                if (identity.status == true)
                {
                    eresponse = new { errMsg = "Invalid username or password." };
                }
                else
                {
                    eresponse = new { errMsg = "Inactive User." };
                }

                var ss1 = JsonConvert.SerializeObject(eresponse, _serializerSettings);
                await context.Response.WriteAsync(ss1);
                return;
            }


            var now = DateTime.UtcNow;

            // Specifically add the jti (nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {

                new Claim(JwtRegisteredClaimNames.Jti, await _options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, identity.uid.ToString()),
                new Claim(ClaimTypes.SerialNumber, identity.cmpid.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Locality, identity.ref1),
                new Claim(ClaimTypes.Role, string.Join(",", identity.roles)),
                new Claim(ClaimTypes.AuthorizationDecision,identity.isapprover.ToString()),
                new Claim("company_domain", identity.domainName),
                new Claim("usr_level",identity.usr_level.ToString()),
                new Claim("roleId",identity.roleId.ToString()),
                new Claim("status",identity.status.ToString()),
                new Claim("usr_country",identity.countryName),
                //new Claim("company_db_server_info",identity.DbServerName + ":" + identity.DbName + ":" + identity.DbPort + ":" + identity.DbUserName + ":" + identity.DbPassword)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                userid = identity.uid,
                roles = identity.roles.ToArray(),
                firstName = identity.firstName,
                lastName = identity.lastName,
                email = identity.email,
                isapprover = identity.isapprover,
                usr_level = identity.usr_level,
                roleId = identity.roleId,
                status = identity.status,
                usr_country = identity.countryName,
                company_domain = identity.domainName
            };

            // Serialize and return the response
            //context.Response.Headers.Add("X-roles", identity.roles.ToArray());
            context.Response.Headers.Add("X-uid", new[] { identity.uid.ToString() });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            var ss = JsonConvert.SerializeObject(response, _serializerSettings);
            await context.Response.WriteAsync(ss);
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.IdentityResolver == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.IdentityResolver));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
            }
        }

        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
