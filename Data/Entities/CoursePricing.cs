using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class CoursePricing
    {
        public CoursePricing()
        {
            CourseMaster = new HashSet<CourseMaster>();
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

        public virtual ICollection<CourseMaster> CourseMaster { get; set; }
    }
}
