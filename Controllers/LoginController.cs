using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DeviceManager.Api.Common;
using DeviceManager.Api.Configuration;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Data;
using DeviceManager.Api.Services.Implementation;
using DeviceManager.Api.Services.Interfaces;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DeviceManager.Api.Services.Token;
using DeviceManager.Api.Services;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ActionFilters;
using System.Net;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [Route("api")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly HttpContext httpContext;
        private readonly IOptions<ConnectionSettings> ConnectionOptions;
        private readonly IUserManagerService UserManagerService;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly ITokenService TokenService;
        private readonly IGenericService<EmployeeMasterViewModel> UserMasterService;
        /// <summary>
        /// 
        /// </summary>
        public LoginController(Microsoft.Extensions.Configuration.IConfiguration configuration
            , IOptions<ConnectionSettings> connectionOptions
            , IHttpContextAccessor httpContentAccessor
            , ITokenService tokenService
            , IGenericService<EmployeeMasterViewModel> userMasterService,
            IUserManagerService userManagerService)
        {
            ConnectionOptions = connectionOptions;
            httpContext = httpContentAccessor.HttpContext;
            _config = configuration;
            HttpContextAccessor = httpContentAccessor;
            var _currentUser = HttpContextAccessor.CurrentUser();
            TokenService = tokenService;
            UserMasterService = userMasterService;
            UserManagerService = userManagerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("login/GetMe")]
        public IActionResult GetUserDetails()
        {
            return new ObjectResult(new
            {
                Username = User.Identity.Name,
                Country = User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault().Value,
                EmpId = User.Claims.Where(x => x.Type == ClaimsConstants.EmployeeId).FirstOrDefault().Value,
                FkEmpId = User.Claims.Where(x => x.Type == ClaimsConstants.fkEmpId).FirstOrDefault().Value,
                Tenantid = User.Claims.Where(x => x.Type == ClaimsConstants.TenantId).FirstOrDefault().Value
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Route("login/token")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var authenticatedUser = await UserManagerService.AuthenticateAndGetUserDetails(user);
            if (authenticatedUser.isAuth)
            {
                var claims = TokenUtility.GenerateClaims(authenticatedUser);
                authenticatedUser.access_token = TokenService.GenerateAccessToken(claims);
                authenticatedUser.refresh_token = TokenService.GenerateRefreshToken();
                return new ObjectResult(authenticatedUser);
            }
            else
                return Unauthorized();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [GettingAuthTokenFilter]
        [HttpPost, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var authenticatedUser = TokenService.Decode(LoggedInUser.AuthentedUser.AccessToken);
            if (authenticatedUser != null && authenticatedUser[ClaimsConstants.UserId]!=null)
            {
                var employeeId = Convert.ToInt32(authenticatedUser[ClaimsConstants.fkEmpId]);
                //UserLogDetailService.logout(employeeId);
                return new OkResult();
            }
            else
                return BadRequest("invalid userId to logout!!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshTokenReq"></param>
        /// <returns></returns>
        [HttpPost, Route("login/RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenReq refreshTokenReq)
        {
            //int.Parse(httpContext.Request.Headers[TenantIdFieldName]);
            if (refreshTokenReq == null || string.IsNullOrWhiteSpace(refreshTokenReq.RefreshToken) || string.IsNullOrWhiteSpace(refreshTokenReq.AccessToken))
            {
                return BadRequest("Invalid refesh token request");
            }

            var principal = TokenService.GetPrincipalFromExpiredToken(refreshTokenReq.AccessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            //var refreshToken;
            var authenticatedUser = TokenService.Decode(refreshTokenReq.AccessToken);
            if (authenticatedUser != null && !string.IsNullOrEmpty(authenticatedUser[ClaimsConstants.fkEmpId].ToString()))
            {
                var claims = TokenUtility.GenerateClaimsFromPayload(authenticatedUser);

                var refreshToken = new RefreshTokenRes
                {
                    AccessToken = TokenService.GenerateAccessToken(claims),
                    RefreshToken = TokenService.GenerateRefreshToken()
                };
                return new ObjectResult(refreshToken);
            }
            else
                return Unauthorized();
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> Registration([FromBody]EmployeeMasterViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            //user.Status = "New";
            user.PasswordExpiryDate = DateTime.UtcNow.AddDays(ConnectionOptions.Value.PassswordExpirationDays);
            user.IsActive = false;
            user.IsDeleted = false;
            var registerUser = UserMasterService.Create(user);

            if (registerUser != null)
            {
                return new ObjectResult(registerUser);
            }
            else
                return new ObjectResult("Registration failed");
        }

        [HttpPost, Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid forgot password request");
            }

            var token = await UserManagerService.ForgotPassword(email);
            return Ok(token);
        }

        [HttpPost, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid reset password request");
            }

            var result = await UserManagerService.ResetPassword(model);
            return Ok(result);
        }
    }
}