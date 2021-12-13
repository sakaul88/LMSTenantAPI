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
    public class UserPlanMappingService<T> : IUserPlanMappingService<UserPlanMappingViewModel>, IGenericService<UserPlanMappingViewModel>
    {
        private IGenericRepository<UserPlanMapping> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public UserPlanMappingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<UserPlanMapping> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<UserPlanMappingViewModel>> IGenericService<UserPlanMappingViewModel>.GetAll()
        {
            IList<UserPlanMappingViewModel> models = new List<UserPlanMappingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserPlanMappingViewModel>> GetAll(int page, int pageSize)
        {
            IList<UserPlanMappingViewModel> models = new List<UserPlanMappingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserPlanMappingViewModel>> GetAll(string include)
        {
            IList<UserPlanMappingViewModel> models = new List<UserPlanMappingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<UserPlanMappingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(getId);
            return model;
        }

        public UserPlanMappingViewModel Create(UserPlanMappingViewModel model)
        {

            var insertModel = mapper.Map<UserPlanMappingViewModel, UserPlanMapping>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(modelI);

        }

        public void Update(int id, UserPlanMappingViewModel model)
        {
            var updateModel = mapper.Map<UserPlanMappingViewModel, UserPlanMapping>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<UserPlanMappingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<UserPlanMappingViewModel> models = new List<UserPlanMappingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<UserPlanMappingViewModel>> IGenericService<UserPlanMappingViewModel>.GetByAny(int value)
        {
            IList<UserPlanMappingViewModel> models = new List<UserPlanMappingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<UserPlanMapping, UserPlanMappingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
