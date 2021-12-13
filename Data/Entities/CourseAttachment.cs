using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class CourseAttachment
    {
        public int Id { get; set; }
        public int? FkCourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MongoId { get; set; }
        public string FileType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CourseMaster FkCourse { get; set; }
    }
}
