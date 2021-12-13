using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Coursemaster
    {
        public Coursemaster()
        {
            Courseattachment = new HashSet<Courseattachment>();
            Coursecertificatemapping = new HashSet<Coursecertificatemapping>();
            Employeecoursemapping = new HashSet<Employeecoursemapping>();
            Profilecoursemapping = new HashSet<Profilecoursemapping>();
            Ratingmaster = new HashSet<Ratingmaster>();
            Usercomments = new HashSet<Usercomments>();
        }

        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("FkCourseDetailsId")]
        public int? FkCourseDetailsId { get; set; }

        [Column("FkLevelId")]
        public int? FkLevelId { get; set; }

        [Column("HasAttachment")]
        public sbyte? HasAttachment { get; set; }

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

        public virtual Coursedetails FkCourseDetails { get; set; }
        public virtual Levelmaster FkLevel { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Courseattachment> Courseattachment { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Coursecertificatemapping> Coursecertificatemapping { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Employeecoursemapping> Employeecoursemapping { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Profilecoursemapping> Profilecoursemapping { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Ratingmaster> Ratingmaster { get; set; }

        [ForeignKey("FkCourseId")]
        public virtual ICollection<Usercomments> Usercomments { get; set; }
    }
}
