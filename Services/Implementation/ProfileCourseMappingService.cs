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
    public class ProfileCourseMappingService<T> : IProfileCourseMappingService<ProfileCourseMappingViewModel>, IGenericService<ProfileCourseMappingViewModel>
    {
        private IGenericRepository<ProfileCourseMapping> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public ProfileCourseMappingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<ProfileCourseMapping> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<ProfileCourseMappingViewModel>> IGenericService<ProfileCourseMappingViewModel>.GetAll()
        {
            IList<ProfileCourseMappingViewModel> models = new List<ProfileCourseMappingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileCourseMappingViewModel>> GetAll(int page, int pageSize)
        {
            IList<ProfileCourseMappingViewModel> models = new List<ProfileCourseMappingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<ProfileCourseMappingViewModel>> GetAll(string include)
        {
            IList<ProfileCourseMappingViewModel> models = new List<ProfileCourseMappingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<ProfileCourseMappingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(getId);
            return model;
        }

        public ProfileCourseMappingViewModel Create(ProfileCourseMappingViewModel model)
        {

            var insertModel = mapper.Map<ProfileCourseMappingViewModel, ProfileCourseMapping>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(modelI);

        }

        public void Update(int id, ProfileCourseMappingViewModel model)
        {
            var updateModel = mapper.Map<ProfileCourseMappingViewModel, ProfileCourseMapping>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<ProfileCourseMappingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<ProfileCourseMappingViewModel> models = new List<ProfileCourseMappingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<ProfileCourseMappingViewModel>> IGenericService<ProfileCourseMappingViewModel>.GetByAny(int value)
        {
            IList<ProfileCourseMappingViewModel> models = new List<ProfileCourseMappingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<ProfileCourseMapping, ProfileCourseMappingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}