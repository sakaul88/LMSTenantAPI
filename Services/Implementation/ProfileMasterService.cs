using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeviceManager.Api.Data.Management;
using DeviceManager.Api.Database;
using DeviceManager.Api.Repository;
using DeviceManager.Api.Services.Generic;
using DeviceManager.Api.Utilities;
using DeviceManager.Api.ViewModels;

namespace DeviceManager.Api.Services
{
    /// <inheritdoc />
    public class ProfileMasterService<T> : IProfileMasterService<ProfileMasterViewModel>, IGenericService<ProfileMasterViewModel>
    {
        private IGenericRepository<ProfileMaster> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public ProfileMasterService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<ProfileMaster> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<ProfileMasterViewModel>> IGenericService<ProfileMasterViewModel>.GetAll()
        {
            IList<ProfileMasterViewModel> models = new List<ProfileMasterViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<ProfileMaster, ProfileMasterViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileMasterViewModel>> GetAll(int page, int pageSize)
        {
            IList<ProfileMasterViewModel> models = new List<ProfileMasterViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<ProfileMaster, ProfileMasterViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileMasterViewModel>> GetAll(string include)
        {
            IList<ProfileMasterViewModel> models = new List<ProfileMasterViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<ProfileMaster, ProfileMasterViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<ProfileMasterViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<ProfileMaster, ProfileMasterViewModel>(getId);
            return model;
        }

        public ProfileMasterViewModel Create(ProfileMasterViewModel model)
        {
            var exists = GenericRepository.FindBy(i => i.Name == model.Name).Result.FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Profile '{0}' already exists!!", model.Name));

            var insertModel = mapper.Map<ProfileMasterViewModel, ProfileMaster>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<ProfileMaster, ProfileMasterViewModel>(modelI);

        }

        public void Update(int id, ProfileMasterViewModel model)
        {
            var exists = GenericRepository
                .FindBy(i => i.Name == model.Name && i.Id != id).Result.FirstOrDefault();
            if (exists != null)
                throw new DomainException(HttpStatusCode.Found, "User077",
                    string.Format("Profile '{0}' already exists!!", model.Name));

            var updateModel = mapper.Map<ProfileMasterViewModel, ProfileMaster>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<ProfileMasterViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<ProfileMasterViewModel> models = new List<ProfileMasterViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<ProfileMaster, ProfileMasterViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<ProfileMasterViewModel>> IGenericService<ProfileMasterViewModel>.GetByAny(int value)
        {
            IList<ProfileMasterViewModel> models = new List<ProfileMasterViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<ProfileMaster, ProfileMasterViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
