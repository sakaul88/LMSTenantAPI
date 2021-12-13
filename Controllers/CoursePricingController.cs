using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class CoursePricingController : BaseController<CoursePricingViewModel>
    {
        private readonly IGenericService<CoursePricingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CoursePricingController(IGenericService<CoursePricingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}