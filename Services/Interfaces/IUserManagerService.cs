using DeviceManager.Api.Common;
using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Database;
using DeviceManager.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserManagerService
    {
        Task<AuthUser> AuthenticateAndGetUserDetails(UserLoginModel user);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(ResetPasswordModel model);
    }
}
