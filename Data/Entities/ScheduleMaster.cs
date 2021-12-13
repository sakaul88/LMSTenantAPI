using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class ScheduleMaster
    {
        public ScheduleMaster()
        {
            PlanMaster = new HashSet<PlanMaster>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<PlanMaster> PlanMaster { get; set; }
    }
}
