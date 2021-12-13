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
    public class PlanCourseMappingService<T> : IPlanCourseMappingService<PlanCourseMappingViewModel>, IGenericService<PlanCourseMappingViewModel>
    {
        private IGenericRepository<PlanCourseMapping> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public PlanCourseMappingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<PlanCourseMapping> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<PlanCourseMappingViewModel>> IGenericService<PlanCourseMappingViewModel>.GetAll()
        {
            IList<PlanCourseMappingViewModel> models = new List<PlanCourseMappingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<PlanCourseMappingViewModel>> GetAll(int page, int pageSize)
        {
            IList<PlanCourseMappingViewModel> models = new List<PlanCourseMappingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<PlanCourseMappingViewModel>> GetAll(string include)
        {
            IList<PlanCourseMappingViewModel> models = new List<PlanCourseMappingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<PlanCourseMappingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(getId);
            return model;
        }

        public PlanCourseMappingViewModel Create(PlanCourseMappingViewModel model)
        {

            var insertModel = mapper.Map<PlanCourseMappingViewModel, PlanCourseMapping>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(modelI);

        }

        public void Update(int id, PlanCourseMappingViewModel model)
        {
            var updateModel = mapper.Map<PlanCourseMappingViewModel, PlanCourseMapping>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<PlanCourseMappingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<PlanCourseMappingViewModel> models = new List<PlanCourseMappingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<PlanCourseMappingViewModel>> IGenericService<PlanCourseMappingViewModel>.GetByAny(int value)
        {
            IList<PlanCourseMappingViewModel> models = new List<PlanCourseMappingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkCourseId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<PlanCourseMapping, PlanCourseMappingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
