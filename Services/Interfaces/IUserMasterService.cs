using DeviceManager.Api.Common.DTO.LoginApproval;
using DeviceManager.Api.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services
{
    /// <summary>
    /// Device service interface
    /// </summary>
    public interface IUserMasterService<T>
    {
        bool UpdateLoginRequests(LoginUpdateRequest loginApprovalReq);
    }
}