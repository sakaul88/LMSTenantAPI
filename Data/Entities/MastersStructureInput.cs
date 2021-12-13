using System;
using System.Collections.Generic;

namespace DeviceManager.Api.Database
{
    public partial class MastersStructureInput
    {
        public MastersStructureInput()
        {
            DetailStructureInput = new HashSet<DetailStructureInput>();
        }

        public int Id { get; set; }
        public int FkFormId { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string Title { get; set; }
        public string BreadCrumb { get; set; }
        public string Temp1 { get; set; }
        public string Temp2 { get; set; }
        public string Temp3 { get; set; }
        public string Temp4 { get; set; }
        public string Temp5 { get; set; }
        public string Temp6 { get; set; }
        public string Temp7 { get; set; }
        public string Temp8 { get; set; }
        public string Temp9 { get; set; }
        public string Temp10 { get; set; }
        public string Temp11 { get; set; }
        public string Temp12 { get; set; }
        public string Temp13 { get; set; }
        public string Temp14 { get; set; }
        public string Temp15 { get; set; }
        public string Temp16 { get; set; }
        public string Temp17 { get; set; }
        public string Temp18 { get; set; }
        public string Temp19 { get; set; }
        public string Temp20 { get; set; }
        public string Temp21 { get; set; }
        public string Temp22 { get; set; }
        public string Temp23 { get; set; }
        public string Temp24 { get; set; }
        public string Temp25 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual FormMaster FkForm { get; set; }
        public virtual ICollection<DetailStructureInput> DetailStructureInput { get; set; }
    }
}
