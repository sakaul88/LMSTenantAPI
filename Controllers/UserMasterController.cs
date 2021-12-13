using DeviceManager.Api.Common.DTO.LoginApproval;
using DeviceManager.Api.Common.Enum;
using DeviceManager.Api.Services;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class UserMasterController : BaseController<UserMasterViewModel>
    {
        private readonly IGenericService<UserMasterViewModel> Service;
        private readonly IUserMasterService<UserMasterViewModel> UserMasterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMasterController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public UserMasterController(IGenericService<UserMasterViewModel> service, IUserMasterService<UserMasterViewModel> userMasterService) : base(service)
        //public UserMasterController(IGenericService<UserMasterViewModel> service) : base(service)
        {
            Service = service;
            UserMasterService = userMasterService;
        }

        /// <summary>
        ///  Approve or Reject Login request - Approve = 1, Reject = 2, Unlock = 3, ResetPassword=4
        /// </summary>
        /// <param name="userMasterIds"></param>
        /// <param name="loginApprovalReq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApproveOrReject")]        
        public async Task<IActionResult> UpdateLoginRequest(LoginUpdateRequest loginApprovalReq)
        {
            //UserMasterService.UpdateLoginRequests(loginApprovalReq);
            return new OkObjectResult(UserMasterService.UpdateLoginRequests(loginApprovalReq));
        }

    }
}