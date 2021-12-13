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
        Task<IEnumerable<GetUserAccessForms>> GetUserAccessForms(int id);
        Task<IEnumerable<UserGridViewModel>> GetUserGridConfigurationByAny(int employeeId, int formId);
        Task<IEnumerable<TabFormProfileMaintenanance>> GetUserTabConfigurationByForm(int profileId, int formId);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(ResetPasswordModel model);
    }
}
