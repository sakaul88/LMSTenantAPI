using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class EmployeeMasterController : BaseController<EmployeeMasterViewModel>
    {
        private readonly IGenericService<EmployeeMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeMasterController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public EmployeeMasterController(IGenericService<EmployeeMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}
