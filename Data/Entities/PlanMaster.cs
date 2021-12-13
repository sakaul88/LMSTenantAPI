using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class PlanMaster
    {
        public PlanMaster()
        {
            PlanCourseMapping = new HashSet<PlanCourseMapping>();
            UserPlanMapping = new HashSet<UserPlanMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FkScheduleId { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? DiscountProvided { get; set; }
        public decimal? OriginalCost { get; set; }
        public bool? IsUserPlan { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ScheduleMaster FkSchedule { get; set; }
        public virtual ICollection<PlanCourseMapping> PlanCourseMapping { get; set; }
        public virtual ICollection<UserPlanMapping> UserPlanMapping { get; set; }
    }
}
