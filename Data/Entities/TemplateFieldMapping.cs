using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class TemplateFieldMapping
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
    }
}
