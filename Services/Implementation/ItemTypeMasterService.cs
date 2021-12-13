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
    public class ItemTypeMasterService<T> : IItemTypeMasterService<ItemTypeMasterViewModel>, IGenericService<ItemTypeMasterViewModel>
    {
        private IGenericRepository<ItemTypeMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public ItemTypeMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<ItemTypeMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<ItemTypeMasterViewModel>> IGenericService<ItemTypeMasterViewModel>.GetAll()
        {
            IList<ItemTypeMasterViewModel> models = new List<ItemTypeMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ItemTypeMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<ItemTypeMasterViewModel> models = new List<ItemTypeMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ItemTypeMasterViewModel>> GetAll(string include)
        {
            IList<ItemTypeMasterViewModel> models = new List<ItemTypeMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<ItemTypeMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(getId);
            return model;
        }

        public ItemTypeMasterViewModel Create(ItemTypeMasterViewModel model)
        {

            var insertModel = mapper.Map<ItemTypeMasterViewModel, ItemTypeMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(modelI);

        }

        public void Update(int id, ItemTypeMasterViewModel model)
        {
            var updateModel = mapper.Map<ItemTypeMasterViewModel, ItemTypeMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<ItemTypeMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<ItemTypeMasterViewModel> models = new List<ItemTypeMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<ItemTypeMasterViewModel>> IGenericService<ItemTypeMasterViewModel>.GetByAny(int value)
        {
            IList<ItemTypeMasterViewModel> models = new List<ItemTypeMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<ItemTypeMaster, ItemTypeMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
