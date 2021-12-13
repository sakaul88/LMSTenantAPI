using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class UserGridConfiguration
    {
        public int Id { get; set; }
        public int FkGridId { get; set; }
        public int FkUserId { get; set; }
        public string GridId { get; set; }
        public string UnmappedColumns { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string DefaultColumns { get; set; }

        public virtual FormGridMaster FkGrid { get; set; }
        public virtual UserMaster FkUser { get; set; }
    }
}
