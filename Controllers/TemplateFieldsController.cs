using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class TemplateFieldsController : BaseController<TemplateFieldsViewModel>
    {
        private readonly IGenericService<TemplateFieldsViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public TemplateFieldsController(IGenericService<TemplateFieldsViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}