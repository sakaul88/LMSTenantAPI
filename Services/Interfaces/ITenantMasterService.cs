using DeviceManager.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services
{
    public interface ITenantMasterService<T>
    {
        TenantMasterViewModel CreateTenant(string name);
    }
}
