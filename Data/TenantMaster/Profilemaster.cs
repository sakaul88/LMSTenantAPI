using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Profilemaster
    {
        public Profilemaster()
        {
            Employeemaster = new HashSet<Employeemaster>();
            Profilecoursemapping = new HashSet<Profilecoursemapping>();
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

        [ForeignKey("FkProfileId")]
        public virtual ICollection<Employeemaster> Employeemaster { get; set; }

        [ForeignKey("FkProfileId")]
        public virtual ICollection<Profilecoursemapping> Profilecoursemapping { get; set; }
    }
}
