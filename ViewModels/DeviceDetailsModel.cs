using System;

namespace DeviceManager.Api.Models
{
    public class DeviceDetailsModel
    {
        public Guid? FkDeviceId { get; set; }
        public string Model { get; set; }
        public Guid Id { get; set; }

    }
}
