using DeviceManager.Api.Services;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeviceManager.Api.Controllers
{
    public class TenantMasterController : BaseController<TenantMasterViewModel>
    {
        private readonly IGenericService<TenantMasterViewModel> Service;
        private readonly ITenantMasterService<TenantMasterViewModel> TService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public TenantMasterController(IGenericService<TenantMasterViewModel> service,
            ITenantMasterService<TenantMasterViewModel> tService) : base(service)
        {
            Service = service;
            TService = tService;
        }

        [HttpPost, Route("GetShortContractDetailsByVehicleIds")]
        public async Task<IActionResult> GetShortContractDetailsByVehicleIds(string name)
        {
            return new OkObjectResult(TService.CreateTenant(name));
        }
    }
}