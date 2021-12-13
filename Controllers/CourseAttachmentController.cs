using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class CourseAttachmentController : BaseController<CourseAttachmentViewModel>
    {
        private readonly IGenericService<CourseAttachmentViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CourseAttachmentController(IGenericService<CourseAttachmentViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}