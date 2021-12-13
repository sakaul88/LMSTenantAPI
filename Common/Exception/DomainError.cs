using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Common
{
    public class DomainErrors
    {
        public virtual string Error_code { get; set; }
        public virtual string Message { get; set; }
        public virtual ValidationErrorCollection[] ValidationErrors { get; set; }
    }

    public class ValidationErrorCollection
    {
        public string Field { get; set; }
        public string[] Errors { get; set; }
    }
}
