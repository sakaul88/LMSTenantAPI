using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Certificatemaster
    {
        public Certificatemaster()
        {
            Coursecertificatemapping = new HashSet<Coursecertificatemapping>();
        }
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("FkTemplateId")]
        public int? FkTemplateId { get; set; }

        [Column("FkLevelId")]
        public int? FkLevelId { get; set; }

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

        public virtual Levelmaster FkLevel { get; set; }
        public virtual Templatemaster FkTemplate { get; set; }

        [ForeignKey("FkCertificateId")]
        public virtual ICollection<Coursecertificatemapping> Coursecertificatemapping { get; set; }
    }
}
