using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class TemplateFields
    {
        public TemplateFields()
        {
            TemplateFieldMapping = new HashSet<TemplateFieldMapping>();
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
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string WhereFieldProperty { get; set; }
        public string Code { get; set; }

        public virtual ICollection<TemplateFieldMapping> TemplateFieldMapping { get; set; }
    }
}
