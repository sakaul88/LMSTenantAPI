using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TabFormProfileMaintenananceController : BaseController<TabFormProfileMaintenananceViewModel>
    {
        private readonly IGenericService<TabFormProfileMaintenananceViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public TabFormProfileMaintenananceController(IGenericService<TabFormProfileMaintenananceViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}