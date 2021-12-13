using System;

namespace DeviceManager.Api.ViewModels
{
    public class ProfileFormMaintenanceViewModel

    {
        public int Id { get; set; }
        public int? FkFormId { get; set; }
        public int? FkProfileId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }



    }
}
