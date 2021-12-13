using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Api.Controllers
{
    public class FormGridMasterController : BaseController<FormGridMasterViewModel>
    {
        private readonly IGenericService<FormGridMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormGridMasterController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public FormGridMasterController(IGenericService<FormGridMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}