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
    public class LevelMasterService<T> : ILevelMasterService<LevelMasterViewModel>, IGenericService<LevelMasterViewModel>
    {
        private IGenericRepository<LevelMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public LevelMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<LevelMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<LevelMasterViewModel>> IGenericService<LevelMasterViewModel>.GetAll()
        {
            IList<LevelMasterViewModel> models = new List<LevelMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<LevelMaster, LevelMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<LevelMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<LevelMasterViewModel> models = new List<LevelMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<LevelMaster, LevelMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<LevelMasterViewModel>> GetAll(string include)
        {
            IList<LevelMasterViewModel> models = new List<LevelMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<LevelMaster, LevelMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<LevelMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<LevelMaster, LevelMasterViewModel>(getId);
            return model;
        }

        public LevelMasterViewModel Create(LevelMasterViewModel model)
        {

            var insertModel = mapper.Map<LevelMasterViewModel, LevelMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<LevelMaster, LevelMasterViewModel>(modelI);

        }

        public void Update(int id, LevelMasterViewModel model)
        {
            var updateModel = mapper.Map<LevelMasterViewModel, LevelMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<LevelMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<LevelMasterViewModel> models = new List<LevelMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<LevelMaster, LevelMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<LevelMasterViewModel>> IGenericService<LevelMasterViewModel>.GetByAny(int value)
        {
            IList<LevelMasterViewModel> models = new List<LevelMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<LevelMaster, LevelMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
