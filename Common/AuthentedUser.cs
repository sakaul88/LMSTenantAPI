using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Common
{
    public class AuthentedUser
    {
        public string AccessToken { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int TenantId { get; set; }
        public int ProfileId { get; set; }

    }

    public class LoggedInUser
    {
        public static AuthentedUser AuthentedUser { get; set; }
    }
}
