using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DetailStructureInputController : BaseController<DetailStructureInputViewModel>
    {
        private readonly IGenericService<DetailStructureInputViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public DetailStructureInputController(IGenericService<DetailStructureInputViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}