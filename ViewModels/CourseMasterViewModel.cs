using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class CourseMasterViewModel
    {
        public CourseMasterViewModel()
        {
            CourseAttachment = new HashSet<CourseAttachmentViewModel>();
            CourseCertificateMapping = new HashSet<CourseCertificateMappingViewModel>();
            PlanCourseMapping = new HashSet<PlanCourseMappingViewModel>();
            RatingMaster = new HashSet<RatingMasterViewModel>();
            UserComments = new HashSet<UserCommentsViewModel>();
        }

        public int Id { get; set; }
        public int? FkCoursePricingId { get; set; }
        public int? FkCourseDetailsId { get; set; }
        public int? FkLevelId { get; set; }
        public bool? HasAttachment { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CourseDetailsViewModel FkCourseDetails { get; set; }
        public virtual CoursePricingViewModel FkCoursePricing { get; set; }
        public virtual LevelMasterViewModel FkLevel { get; set; }
        public virtual ICollection<CourseAttachmentViewModel> CourseAttachment { get; set; }
        public virtual ICollection<CourseCertificateMappingViewModel> CourseCertificateMapping { get; set; }
        public virtual ICollection<PlanCourseMappingViewModel> PlanCourseMapping { get; set; }
        public virtual ICollection<RatingMasterViewModel> RatingMaster { get; set; }
        public virtual ICollection<UserCommentsViewModel> UserComments { get; set; }
    }
}
