using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class CourseCertificateMappingController : BaseController<CourseCertificateMappingViewModel>
    {
        private readonly IGenericService<CourseCertificateMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CourseCertificateMappingController(IGenericService<CourseCertificateMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}