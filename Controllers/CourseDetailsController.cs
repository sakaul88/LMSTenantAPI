using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
namespace DeviceManager.Api.Controllers
{
    public class CourseDetailsController : BaseController<CourseDetailsViewModel>
    {
        private readonly IGenericService<CourseDetailsViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CourseDetailsController(IGenericService<CourseDetailsViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}