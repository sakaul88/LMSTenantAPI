using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProfileMasterController : BaseController<ProfileMasterViewModel>
    {
        private readonly IGenericService<ProfileMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public ProfileMasterController(IGenericService<ProfileMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}