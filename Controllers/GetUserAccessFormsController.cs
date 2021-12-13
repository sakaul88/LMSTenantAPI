using DeviceManager.Api.Common;
using DeviceManager.Api.Data;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Services.Implementation;
using DeviceManager.Api.Services.Interfaces;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class GetUserAccessFormsController : Controller
    {
        private IUserManagerService UserManagerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserAccessFormsController"/> class.
        /// </summary>
        /// <param name="userManagerService"></param>
        /// <param name="tenantConnectionService"></param>
        public GetUserAccessFormsController(IUserManagerService userManagerService)
        {
            UserManagerService = userManagerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/GetForms")]
        public async Task<IActionResult> GetAllForms()
        {
            var profileId = User.Claims.Where(x => x.Type == ClaimsConstants.ProfileId).FirstOrDefault().Value;
            return Ok(await UserManagerService.GetUserAccessForms(int.Parse(profileId)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet("api/UserGridConfigurationByAny/{formId}")]
        public async Task<IActionResult> UserGridConfigurationByAny(int formId)
        {
            var UserId = User.Claims.Where(x => x.Type == ClaimsConstants.fkEmpId).FirstOrDefault().Value;
            var result = await UserManagerService.GetUserGridConfigurationByAny(int.Parse(UserId), formId);
            return new OkObjectResult(result);
        }
        [HttpGet("api/GetUserTabConfigurationByForm/{formId}")]
        public async Task<IActionResult> GetUserTabConfigurationByForm(int formId)
        {
            var profileId = User.Claims.Where(x => x.Type == ClaimsConstants.ProfileId).FirstOrDefault().Value;
            var result = await UserManagerService.GetUserTabConfigurationByForm(int.Parse(profileId), formId);
            return new OkObjectResult(result);
        }


        [HttpGet("api/DDUserImg")]
        public IActionResult GetCompanyLogo()
        {
            var cImg = "../assets/dist/img/logo512.png";
            var cmp_name = "JKT";
            var imageurl = "";
            return Ok(new { cImg, cmp_name, imageurl });
        }

    }
}