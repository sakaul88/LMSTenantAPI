using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class EmployeeCourseMappingController : BaseController<EmployeeCourseMappingViewModel>
    {
        private readonly IGenericService<EmployeeCourseMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateMasterController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public EmployeeCourseMappingController(IGenericService<EmployeeCourseMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}


