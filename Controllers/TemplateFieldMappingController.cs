using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class TemplateFieldMappingController : BaseController<TemplateFieldMappingViewModel>
    {
        private readonly IGenericService<TemplateFieldMappingViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public TemplateFieldMappingController(IGenericService<TemplateFieldMappingViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}