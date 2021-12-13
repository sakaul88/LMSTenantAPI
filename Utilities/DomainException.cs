using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace DeviceManager.Api.Utilities
{
    [Serializable]
    public class DomainException : Exception
    {
        public virtual string ErrorCode { get; }
        public virtual HttpStatusCode StatusCode { get; }

        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
        public DomainException(HttpStatusCode statusCode, string errorCode, string message) : base(message) { StatusCode = statusCode; ErrorCode = errorCode; }
        public DomainException(HttpStatusCode statusCode, string errorCode, string message, Exception inner) : base(message, inner) { StatusCode = statusCode; ErrorCode = errorCode; }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceReferenceProperty = info.GetString(nameof(ResourceReferenceProperty));
        }

        public string ResourceReferenceProperty { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(ResourceReferenceProperty), ResourceReferenceProperty);
            base.GetObjectData(info, context);
        }
    }
}
