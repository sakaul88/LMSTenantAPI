using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class TemplateFieldMappingViewModel
    {
        public int Id { get; set; }
        public int FkTemplateId { get; set; }
        public int? FkFieldId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TemplateFieldsViewModel FkField { get; set; }
        public virtual TemplateMasterViewModel FkTemplate { get; set; }
    }
}
