using DeviceManager.Api.Common.Enum;
using DeviceManager.Api.Database;
using System;

namespace DeviceManager.Api.ViewModels
{
    public class UserMasterViewModel
    {        
        public int Id { get; set; }
        public int? FkCompanyId { get; set; }
        public int? FkEmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImgUrl { get; set; }
        public int? FkDesignationId { get; set; }
        public int? FkRoleid { get; set; }
        public int? FkSubRoleId { get; set; }
        public int? FkProfileId { get; set; }
        public int? FkDepartmentId { get; set; }
        public int? FkUrlId { get; set; }
        public DateTime? LastLogindatetime { get; set; }
        public string LastPassword { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? TokenExpiryDate { get; set; }
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public bool? isLocked { get; set; }
        public int? loginFailureCount { get; set; }
        public UserStatus UserType { get; set; }
    }
}
