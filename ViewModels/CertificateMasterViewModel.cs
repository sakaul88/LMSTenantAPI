using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public partial class CertificateMasterViewModel
    {
        public CertificateMasterViewModel()
        {
            CourseCertificateMapping = new HashSet<CourseCertificateMappingViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FkTemplateId { get; set; }
        public int? FkLevelId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual LevelMasterViewModel FkLevel { get; set; }
        public virtual TemplateMasterViewModel FkTemplate { get; set; }
        public virtual ICollection<CourseCertificateMappingViewModel> CourseCertificateMapping { get; set; }
    }
}
