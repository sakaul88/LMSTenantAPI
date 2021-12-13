using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class ProfileFormMaintenance
    {
        public int Id { get; set; }
        public int? FkFormId { get; set; }
        public int? FkProfileId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual FormMaster FkForm { get; set; }
        public virtual ProfileMaster FkProfile { get; set; }
    }
}
