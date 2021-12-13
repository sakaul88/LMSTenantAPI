using DeviceManager.Api.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Common.DTO.LoginApproval
{
    public class LoginUpdateRequest
    {
        public List<int> UserMasterIds { get; set; }
        public LoginUpdateRequestType RequestStatus { get; set; }
    }
}
