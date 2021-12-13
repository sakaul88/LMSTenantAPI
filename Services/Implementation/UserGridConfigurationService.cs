using AutoMapper;
using DeviceManager.Api.Common;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Services.Interfaces;
using DeviceManager.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Services.Implementation
{
    public class UserGridConfigurationService<T> : IUserGridConfigurationService<UserGridConfigurationViewModel>, IGenericService<UserGridConfigurationViewModel>
    {

        private IGenericRepository<UserGridConfiguration> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public UserGridConfigurationService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<UserGridConfiguration> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<UserGridConfigurationViewModel>> IGenericService<UserGridConfigurationViewModel>.GetAll()
        {
            IList<UserGridConfigurationViewModel> models = new List<UserGridConfigurationViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserGridConfigurationViewModel>> GetAll(int page, int pageSize)
        {
            IList<UserGridConfigurationViewModel> models = new List<UserGridConfigurationViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<UserGridConfigurationViewModel>> GetAll(string include)
        {
            IList<UserGridConfigurationViewModel> models = new List<UserGridConfigurationViewModel>();
            var getAllIncludes = await GenericRepository.GetAll(include);
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<UserGridConfigurationViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(getId);
            return model;
        }

        public UserGridConfigurationViewModel Create(UserGridConfigurationViewModel model)
        {

            var insertModel = mapper.Map<UserGridConfigurationViewModel, UserGridConfiguration>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(modelI);

        }

        public void Update(int id, UserGridConfigurationViewModel model)
        {
            var updateModel = mapper.Map<UserGridConfigurationViewModel, UserGridConfiguration>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<UserGridConfigurationViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<UserGridConfigurationViewModel> models = new List<UserGridConfigurationViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<UserGridConfigurationViewModel>> IGenericService<UserGridConfigurationViewModel>.GetByAny(int value)
        {
            IList<UserGridConfigurationViewModel> models = new List<UserGridConfigurationViewModel>();

            var emp = "3";
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkGridId == value && x.FkUserId == int.Parse(emp));

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<UserGridConfiguration, UserGridConfigurationViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
