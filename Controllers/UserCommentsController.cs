using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{
    public class UserCommentsController : BaseController<UserCommentsViewModel>
    {
        private readonly IGenericService<UserCommentsViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public UserCommentsController(IGenericService<UserCommentsViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}