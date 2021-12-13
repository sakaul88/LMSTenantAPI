using DeviceManager.Api.Data.Views;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFormDetailsController : BaseController<GetFormDetailsViewModel>
    {
        private readonly IGenericService<GetFormDetailsViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetFormDetailsController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public GetFormDetailsController(IGenericService<GetFormDetailsViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}
