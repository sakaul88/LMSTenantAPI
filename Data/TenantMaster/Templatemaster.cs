using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceManager.Api.data.tenantmaster
{
    public partial class Templatemaster
    {
        public Templatemaster()
        {
            Certificatemaster = new HashSet<Certificatemaster>();
        }

        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("FkItemTypeId")]
        public int? FkItemTypeId { get; set; }

        [Column("FkcreditionalDetailId")]
        public int? FkcreditionalDetailId { get; set; }

        [Column("From")]
        public string From { get; set; }

        [Column("FromName")]
        public string FromName { get; set; }

        [Column("ToAddress")]
        public string ToAddress { get; set; }

        [Column("Ccaddress")]
        public string Ccaddress { get; set; }

        [Column("Bccaddress")]
        public string Bccaddress { get; set; }

        [Column("HasAttachment")]
        public sbyte? HasAttachment { get; set; }

        [Column("Subject")]
        public string Subject { get; set; }

        [Column("Salutation")]
        public string Salutation { get; set; }

        [Column("Body")]
        public string Body { get; set; }

        [Column("Regards")]
        public string Regards { get; set; }

        [Column("Footer")]
        public string Footer { get; set; }

        [Column("IsHtmlbody")]
        public sbyte? IsHtmlbody { get; set; }

        [Column("HeaderFrame")]
        public string HeaderFrame { get; set; }

        [Column("FooterFrame")]
        public string FooterFrame { get; set; }

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

        public virtual Itemtypemaster FkItemType { get; set; }
        public virtual Credentialdetails FkcreditionalDetail { get; set; }

        [ForeignKey("FkTemplateId")]
        public virtual ICollection<Certificatemaster> Certificatemaster { get; set; }
    }
}
