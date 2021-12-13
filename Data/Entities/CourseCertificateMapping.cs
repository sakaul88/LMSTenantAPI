using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class CourseCertificateMapping
    {
        public int Id { get; set; }
        public int? FkCourseId { get; set; }
        public int? FkCertificateId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CertificateMaster FkCertificate { get; set; }
        public virtual CourseMaster FkCourse { get; set; }
    }
}
