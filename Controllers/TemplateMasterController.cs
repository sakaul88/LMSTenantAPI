using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class TemplateMasterController : BaseController<TemplateMasterViewModel>
    {
        private readonly IGenericService<TemplateMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public TemplateMasterController(IGenericService<TemplateMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}