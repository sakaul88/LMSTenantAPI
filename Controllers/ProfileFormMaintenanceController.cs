using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class ProfileFormMaintenanceController : BaseController<ProfileFormMaintenanceViewModel>
    {
        private readonly IGenericService<ProfileFormMaintenanceViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public ProfileFormMaintenanceController(IGenericService<ProfileFormMaintenanceViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}