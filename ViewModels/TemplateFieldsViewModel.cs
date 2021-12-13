using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class TemplateFieldsViewModel
    {
        public TemplateFieldsViewModel()
        {
            TemplateFieldMapping = new HashSet<TemplateFieldMappingViewModel>();
        }

        public int Id { get; set; }
        public string FieldType { get; set; }
        public string ContentToReplace { get; set; }
        public string FieldDatabase { get; set; }
        public string FieldTable { get; set; }
        public string FieldProperty { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string WhereFieldProperty { get; set; }
        public string Code { get; set; }

        public virtual ICollection<TemplateFieldMappingViewModel> TemplateFieldMapping { get; set; }
    }
}
