using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class FormGridMasterViewModel
    {
        public int Id { get; set; }
        public int FkFormId { get; set; }
        public string GridId { get; set; }
        public string MappedColumns { get; set; }
        public string HeaderTitle { get; set; }
        public string ButtonLabel { get; set; }
        public string apiEndPoint { get; set; }
        public string parameter { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class UserGridViewModel : FormGridMasterViewModel
    {
        public int? UserGridConfigurationId { get; set; }
        public string unmappedColumns { get; set; }
    }
}
