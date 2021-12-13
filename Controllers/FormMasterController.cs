using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class FormMasterController : BaseController<FormMasterViewModel>
    {

        // private IDbContext dbContext;
        private readonly IGenericService<FormMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        /// 
        public FormMasterController(IGenericService<FormMasterViewModel> service) : base(service)
        {
            FormMasterViewModel formdata = new FormMasterViewModel();
            Service = service;
        }
    }
}