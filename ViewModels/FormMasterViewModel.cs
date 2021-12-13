using System;

namespace DeviceManager.Api.ViewModels
{
    public class FormMasterViewModel
    {


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


    }
}
