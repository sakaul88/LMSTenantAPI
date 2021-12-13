using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.data.tenantmaster;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Services
{
    public class TenantMasterService<T> : ITenantMasterService<TenantMasterViewModel>, IGenericService<TenantMasterViewModel>
    {
        private IGenericRepository<TenantMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IOptions<ConnectionSettings> Options;
        private readonly lmstenantContext TenantContext;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public TenantMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IOptions<ConnectionSettings> options,
            IMapper mapper, IGenericRepository<TenantMaster> genericRepository,
            lmstenantContext tenantContext)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
            Options = options;
            TenantContext = tenantContext;
        }

        async Task<IEnumerable<TenantMasterViewModel>> IGenericService<TenantMasterViewModel>.GetAll()
        {
            IList<TenantMasterViewModel> models = new List<TenantMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<TenantMaster, TenantMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TenantMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<TenantMasterViewModel> models = new List<TenantMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<TenantMaster, TenantMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TenantMasterViewModel>> GetAll(string include)
        {
            IList<TenantMasterViewModel> models = new List<TenantMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<TenantMaster, TenantMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<TenantMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<TenantMaster, TenantMasterViewModel>(getId);
            return model;
        }

        public TenantMasterViewModel Create(TenantMasterViewModel model)
        {

            var insertModel = mapper.Map<TenantMasterViewModel, TenantMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<TenantMaster, TenantMasterViewModel>(modelI);

        }

        public void Update(int id, TenantMasterViewModel model)
        {
            var updateModel = mapper.Map<TenantMasterViewModel, TenantMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<TenantMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<TenantMasterViewModel> models = new List<TenantMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<TenantMaster, TenantMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<TenantMasterViewModel>> IGenericService<TenantMasterViewModel>.GetByAny(int value)
        {
            IList<TenantMasterViewModel> models = new List<TenantMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<TenantMaster, TenantMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }

        public TenantMasterViewModel CreateTenant(string name)
        {
            var connection = TenantContext.Database.GetDbConnection().ConnectionString;
            var tenantConnection = connection.Replace("{{tenant}}", name);
            TenantContext.Database.GetDbConnection().ConnectionString = tenantConnection;
            TenantContext.Database.EnsureCreated();
            return new TenantMasterViewModel();
        }
    }
}
