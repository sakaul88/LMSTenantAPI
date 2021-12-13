using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ViewModels
{
    public class CredentialDetailsViewModel
    {
        public CredentialDetailsViewModel()
        {
            TemplateMaster = new HashSet<TemplateMasterViewModel>();
        }

        public int Id { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SecretKey1 { get; set; }
        public string SecretKey2 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<TemplateMasterViewModel> TemplateMaster { get; set; }
    }
}
