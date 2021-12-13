using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class ProfileCourseMappingController : BaseController<ProfileCourseMappingViewModel>
    {
        private readonly IGenericService<ProfileCourseMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileCourseMappingController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public ProfileCourseMappingController(IGenericService<ProfileCourseMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}