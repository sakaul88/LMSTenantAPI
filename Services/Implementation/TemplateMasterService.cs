using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Services
{
    public class TemplateMasterService
    {
    }

    public class TemplateMasterService<T> : ITemplateMasterService<TemplateMasterViewModel>, IGenericService<TemplateMasterViewModel>
    {
        private IGenericRepository<TemplateMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public TemplateMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<TemplateMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<TemplateMasterViewModel>> IGenericService<TemplateMasterViewModel>.GetAll()
        {
            IList<TemplateMasterViewModel> models = new List<TemplateMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<TemplateMaster, TemplateMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<TemplateMasterViewModel> models = new List<TemplateMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<TemplateMaster, TemplateMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<TemplateMasterViewModel>> GetAll(string include)
        {
            IList<TemplateMasterViewModel> models = new List<TemplateMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<TemplateMaster, TemplateMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<TemplateMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<TemplateMaster, TemplateMasterViewModel>(getId);
            return model;
        }

        public TemplateMasterViewModel Create(TemplateMasterViewModel model)
        {

            var insertModel = mapper.Map<TemplateMasterViewModel, TemplateMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<TemplateMaster, TemplateMasterViewModel>(modelI);

        }

        public void Update(int id, TemplateMasterViewModel model)
        {
            var updateModel = mapper.Map<TemplateMasterViewModel, TemplateMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDelete = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<TemplateMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<TemplateMasterViewModel> models = new List<TemplateMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<TemplateMaster, TemplateMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<TemplateMasterViewModel>> IGenericService<TemplateMasterViewModel>.GetByAny(int value)
        {
            IList<TemplateMasterViewModel> models = new List<TemplateMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<TemplateMaster, TemplateMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
