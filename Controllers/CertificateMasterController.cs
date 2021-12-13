using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class CertificateMasterController : BaseController<CertificateMasterViewModel>
    {
        private readonly IGenericService<CertificateMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateMasterController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CertificateMasterController(IGenericService<CertificateMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}