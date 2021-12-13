using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class FormGridMaster
    {
        public FormGridMaster()
        {
            UserGridConfiguration = new HashSet<UserGridConfiguration>();
        }

        public int Id { get; set; }
        public int FkFormId { get; set; }
        public string GridId { get; set; }
        public string MappedColumns { get; set; }
        public string Parameter { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string ApiEndPoint { get; set; }
        public string HeaderTitle { get; set; }
        public string ButtonLabel { get; set; }

        public virtual FormMaster FkForm { get; set; }
        public virtual ICollection<UserGridConfiguration> UserGridConfiguration { get; set; }
    }
}
