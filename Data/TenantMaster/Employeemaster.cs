using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Employeemaster
    {
        public Employeemaster()
        {
            Employeecoursemapping = new HashSet<Employeecoursemapping>();
            Ratingmaster = new HashSet<Ratingmaster>();
            Usercomments = new HashSet<Usercomments>();
        }

        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("EmailId")]
        public string EmailId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("UserMastercol")]
        public string UserMastercol { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("FullName")]
        public string FullName { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("ImageUrl")]
        public string ImageUrl { get; set; }

        [Column("FkProfileId")]
        public int? FkProfileId { get; set; }

        [Column("PasswordExpiryDate")]
        public DateTime? PasswordExpiryDate { get; set; }

        [Column("PasswordResetToken")]
        public string PasswordResetToken { get; set; }

        [Column("IsLocked")]
        public sbyte? IsLocked { get; set; }

        [Column("LoginFailureCount")]
        public int? LoginFailureCount { get; set; }

        [Column("CreatedBy")]
        public int? CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("ModifiedBy")]
        public int? ModifiedBy { get; set; }

        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [Column("IsActive")]
        public bool? IsActive { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Column("TokenExpiryDate")]
        public DateTime? TokenExpiryDate { get; set; }

        [Column("LastPassword")]
        public string LastPassword { get; set; }

        public virtual Profilemaster FkProfile { get; set; }

        [ForeignKey("FkEmployeeId")]
        public virtual ICollection<Employeecoursemapping> Employeecoursemapping { get; set; }

        [ForeignKey("FkEmployeeId")]
        public virtual ICollection<Ratingmaster> Ratingmaster { get; set; }

        [ForeignKey("FkEmployeeId")]
        public virtual ICollection<Usercomments> Usercomments { get; set; }
    }
}
