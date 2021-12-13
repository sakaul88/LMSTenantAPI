using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MastersStructureInputController : BaseController<MastersStructureInputViewModel>
    {
        private readonly IGenericService<MastersStructureInputViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public MastersStructureInputController(IGenericService<MastersStructureInputViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}