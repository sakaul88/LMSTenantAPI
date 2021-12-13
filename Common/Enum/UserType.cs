using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Common.Enum
{
    
    public enum UserStatus
    {    
        Approved = 1,
        Rejected = 2,
        locked = 3,
        ResetPassword = 4,
        Pending = 5,
        PasswordExpired = 6,
    }
}
