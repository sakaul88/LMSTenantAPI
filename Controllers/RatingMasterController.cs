﻿using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;


namespace DeviceManager.Api.Controllers
{
    public class RatingMasterController : BaseController<RatingMasterViewModel>
    {
        private readonly IGenericService<RatingMasterViewModel> Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="service">The device service.</param>
        public RatingMasterController(IGenericService<RatingMasterViewModel> service) : base(service)
        {
            Service = service;
        }
    }
}