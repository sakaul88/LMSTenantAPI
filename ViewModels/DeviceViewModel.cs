using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceManager.Api.Models
{
    /// <summary>
    /// View model for Device
    /// </summary>
    public class DeviceViewModel
    {
        public DeviceViewModel()
        {
            DeviceDetails = new List<DeviceDetailsModel>();
        }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the device code.
        /// </summary>
        /// <value>
        /// The device code.
        /// </value>
        //public string DeviceCode { get; set; }

        public IEnumerable<DeviceDetailsModel> DeviceDetails { get; set; }
    }
}