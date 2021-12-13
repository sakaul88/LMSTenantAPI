using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class LevelMasterViewModel
    {
        public LevelMasterViewModel()
        {
            CertificateMaster = new HashSet<CertificateMasterViewModel>();
            CourseMaster = new HashSet<CourseMasterViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<CertificateMasterViewModel> CertificateMaster { get; set; }
        public virtual ICollection<CourseMasterViewModel> CourseMaster { get; set; }
    }
}
