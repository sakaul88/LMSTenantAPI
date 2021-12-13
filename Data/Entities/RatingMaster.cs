using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class RatingMaster
    {
        public int Id { get; set; }
        public int? FkEmployeeId { get; set; }
        public int? FkCourseId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? Rating { get; set; }

        public virtual CourseMaster FkCourse { get; set; }
        public virtual EmployeeMaster FkEmployee { get; set; }
    }
}
