using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Common.Enum
{
    public enum LoginUpdateRequestType
    {
        //Pending = 0,
        Approve = 1,
        Reject = 2,
        Unlock = 3,
        ResetPassword=4
    }
}
