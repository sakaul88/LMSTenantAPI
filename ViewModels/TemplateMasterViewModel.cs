using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class TemplateMasterViewModel
    {
        public TemplateMasterViewModel()
        {
            CertificateMaster = new HashSet<CertificateMasterViewModel>();
            TemplateFieldMapping = new HashSet<TemplateFieldMappingViewModel>();
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
        public sbyte IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ItemTypeMasterViewModel FkItemType { get; set; }
        public virtual CredentialDetailsViewModel FkcreditionalDetail { get; set; }
        public virtual ICollection<CertificateMasterViewModel> CertificateMaster { get; set; }
        public virtual ICollection<TemplateFieldMappingViewModel> TemplateFieldMapping { get; set; }
    }
}
