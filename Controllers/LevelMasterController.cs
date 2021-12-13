using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;


namespace DeviceManager.Api.Controllers
{
    public class LevelMasterController : BaseController<LevelMasterViewModel>
    {
        private readonly IGenericService<LevelMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public LevelMasterController(IGenericService<LevelMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}