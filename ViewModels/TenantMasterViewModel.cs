using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class TenantMasterViewModel
    {
        public int Id { get; set; }
        public int? FkuserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schema { get; set; }
        public string FileSchema { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsLocked { get; set; }

        public virtual UserMasterViewModel Fkuser { get; set; }
    }
}
