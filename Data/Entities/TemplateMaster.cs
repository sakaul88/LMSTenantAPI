using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class TemplateMaster
    {
        public TemplateMaster()
        {
            CertificateMaster = new HashSet<CertificateMaster>();
            TemplateFieldMapping = new HashSet<TemplateFieldMapping>();
        }

        public int Id { get; set; }
        public int? FkItemTypeId { get; set; }
        public int? FkcreditionalDetailId { get; set; }
        public int? FkEmailActionTypeId { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string ToAddress { get; set; }
        public string Ccaddress { get; set; }
        public string Bccaddress { get; set; }
        public bool? HasAttachment { get; set; }
        public string Subject { get; set; }
        public string Salutation { get; set; }
        public string Body { get; set; }
        public string Regards { get; set; }
        public string Footer { get; set; }
        public bool? IsHtmlbody { get; set; }
        public string HeaderFrame { get; set; }
        public string FooterFrame { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ItemTypeMaster FkItemType { get; set; }
        public virtual CredentialDetails FkcreditionalDetail { get; set; }
        public virtual ICollection<CertificateMaster> CertificateMaster { get; set; }
        public virtual ICollection<TemplateFieldMapping> TemplateFieldMapping { get; set; }
    }
}
