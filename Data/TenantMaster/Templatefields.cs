using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Templatefields
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("FieldType")]
        public string FieldType { get; set; }

        [Column("ContentToReplace")]
        public string ContentToReplace { get; set; }

        [Column("FieldDatabase")]
        public string FieldDatabase { get; set; }

        [Column("FieldTable")]
        public string FieldTable { get; set; }

        [Column("FieldProperty")]
        public string FieldProperty { get; set; }

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

        [Column("WhereFieldProperty")]
        public string WhereFieldProperty { get; set; }

        [Column("Code")]
        public string Code { get; set; }
    }
}
