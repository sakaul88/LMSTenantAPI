using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;


namespace DeviceManager.Api.Controllers
{
    public class CourseMasterController : BaseController<CourseMasterViewModel>
    {
        private readonly IGenericService<CourseMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CourseMasterController(IGenericService<CourseMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}