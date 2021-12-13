using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class UserPlanMappingController : BaseController<UserPlanMappingViewModel>
    {
        private readonly IGenericService<UserPlanMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public UserPlanMappingController(IGenericService<UserPlanMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}