using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;


namespace DeviceManager.Api.Controllers
{
    public class PlanMasterController : BaseController<PlanMasterViewModel>
    {
        private readonly IGenericService<PlanMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public PlanMasterController(IGenericService<PlanMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}