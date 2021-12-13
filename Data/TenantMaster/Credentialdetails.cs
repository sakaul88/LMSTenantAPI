using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Credentialdetails
    {
        public Credentialdetails()
        {
            Templatemaster = new HashSet<Templatemaster>();
        }

        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("SmtpServer")]
        public string SmtpServer { get; set; }

        [Column("SmtpPort")]
        public string SmtpPort { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("SecretKey1")]
        public string SecretKey1 { get; set; }

        [Column("SecretKey2")]
        public string SecretKey2 { get; set; }

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

        [ForeignKey("FkCredentialDetailId")]
        public virtual ICollection<Templatemaster> Templatemaster { get; set; }
    }
}
