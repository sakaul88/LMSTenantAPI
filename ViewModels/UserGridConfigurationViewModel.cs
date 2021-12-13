using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class UserGridConfigurationViewModel
    {
        public int Id { get; set; }
        public int FkGridId { get; set; }
        public int FkUserId { get; set; }
        public string GridId { get; set; }
        public string unmappedColumns { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string GridIdName { get; set; }
    }
}
