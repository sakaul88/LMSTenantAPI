using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Api.Controllers
{
    public class UserGridConfigurationController : BaseController<UserGridConfigurationViewModel>
    {
        private readonly IGenericService<UserGridConfigurationViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGridConfigurationController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public UserGridConfigurationController(IGenericService<UserGridConfigurationViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}