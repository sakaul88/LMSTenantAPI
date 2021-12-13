using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Common
{
    public class AuthUser: DatabaseContext
    {
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int refeshtoken_expires_in { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool isAuth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int uid { get; set; }
        public int cmpid { get; set; }
        public int ulevel { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string countryName { get; set; }
        public string domainName { get; set; }
        public int usr_level { get; set; }
        public int roleId { get; set; }
        public bool status { get; set; }
        public bool isapprover { get; set; }
        public int? FkProfileId { get; set; }
        public int fkEmpId { get; set; }
        public string EmployeeId { get; set; }
		public string imgurl { get; set; }
        public string location { get; set; }
        public int? FkLandingPageId { get; set; }
        public string landingPageURL { get; set; }
        public int? FkDepartmentId { get; set; }
        public string departmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ref1 { get; set; }
        public string desgination { get; set; }
    }

    public class ResetPasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
