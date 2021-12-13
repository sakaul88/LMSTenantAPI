using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class CredentialDetailsController : BaseController<CredentialDetailsViewModel>
    {
        private readonly IGenericService<CredentialDetailsViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public CredentialDetailsController(IGenericService<CredentialDetailsViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}