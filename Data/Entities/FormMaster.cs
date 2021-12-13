using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class FormMaster
    {
        public FormMaster()
        {
            FormGridMaster = new HashSet<FormGridMaster>();
            MastersStructureInput = new HashSet<MastersStructureInput>();
            ProfileFormMaintenance = new HashSet<ProfileFormMaintenance>();
            TabFormProfileMaintenanance = new HashSet<TabFormProfileMaintenanance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ModuleName { get; set; }
        public string FormDescription { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int? MenuId { get; set; }
        public int? SubMenuId { get; set; }
        public int? SortOrder { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<FormGridMaster> FormGridMaster { get; set; }
        public virtual ICollection<MastersStructureInput> MastersStructureInput { get; set; }
        public virtual ICollection<ProfileFormMaintenance> ProfileFormMaintenance { get; set; }
        public virtual ICollection<TabFormProfileMaintenanance> TabFormProfileMaintenanance { get; set; }
    }
}
