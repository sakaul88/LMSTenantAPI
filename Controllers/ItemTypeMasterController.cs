using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
namespace DeviceManager.Api.Controllers
{
    public class ItemTypeMasterController : BaseController<ItemTypeMasterViewModel>
    {
        private readonly IGenericService<ItemTypeMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public ItemTypeMasterController(IGenericService<ItemTypeMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}