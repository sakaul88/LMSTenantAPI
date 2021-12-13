using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class PlanCourseMappingController : BaseController<PlanCourseMappingViewModel>
    {
        private readonly IGenericService<PlanCourseMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public PlanCourseMappingController(IGenericService<PlanCourseMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}