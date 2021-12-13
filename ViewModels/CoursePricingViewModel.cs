using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class CoursePricingViewModel
    {
        public CoursePricingViewModel()
        {
            CourseMaster = new HashSet<CourseMasterViewModel>();
        }

        public int Id { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? DiscountAllocated { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<CourseMasterViewModel> CourseMaster { get; set; }
    }
}
