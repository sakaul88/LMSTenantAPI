using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class PlanMasterViewModel
    {
        public PlanMasterViewModel()
        {
            PlanCourseMapping = new HashSet<PlanCourseMappingViewModel>();
            UserPlanMapping = new HashSet<UserPlanMappingViewModel>();
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

        public virtual ScheduleMasterViewModel FkSchedule { get; set; }
        public virtual ICollection<PlanCourseMappingViewModel> PlanCourseMapping { get; set; }
        public virtual ICollection<UserPlanMappingViewModel> UserPlanMapping { get; set; }
    }
}
