using DeviceManager.Api.Models;
using FluentValidation;

namespace DeviceManager.Api.Validation
{
    /// <summary>
    /// Validation rules related to Device controller
    /// </summary>
    public class DeviceViewModelValidationRules : AbstractValidator<DeviceViewModel>, IDeviceViewModelValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceViewModelValidationRules"/> class.
        /// <example>
        /// All validation rules can be found here: https://github.com/JeremySkinner/FluentValidation/wiki/a.-Index
        /// </example>
        /// </summary>
        public DeviceViewModelValidationRules()
        {
            RuleFor(device => device.Title)
                .NotEmpty()
                .Length(5, 10);

            RuleFor(device => device.Title)
                .NotEmpty();

            RuleFor(device => device.Title)
                .NotEmpty();
        }
    }
}
