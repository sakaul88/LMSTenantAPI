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
    public class EmployeeCourseMappingService<T> : IEmployeeCourseMappingService<EmployeeCourseMappingViewModel>, IGenericService<EmployeeCourseMappingViewModel>
    {
        private IGenericRepository<EmployeeCourseMapping> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public EmployeeCourseMappingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<EmployeeCourseMapping> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<EmployeeCourseMappingViewModel>> IGenericService<EmployeeCourseMappingViewModel>.GetAll()
        {
            IList<EmployeeCourseMappingViewModel> models = new List<EmployeeCourseMappingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<EmployeeCourseMappingViewModel>> GetAll(int page, int pageSize)
        {
            IList<EmployeeCourseMappingViewModel> models = new List<EmployeeCourseMappingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<EmployeeCourseMappingViewModel>> GetAll(string include)
        {
            IList<EmployeeCourseMappingViewModel> models = new List<EmployeeCourseMappingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<EmployeeCourseMappingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(getId);
            return model;
        }

        public EmployeeCourseMappingViewModel Create(EmployeeCourseMappingViewModel model)
        {

            var insertModel = mapper.Map<EmployeeCourseMappingViewModel, EmployeeCourseMapping>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(modelI);

        }

        public void Update(int id, EmployeeCourseMappingViewModel model)
        {
            var updateModel = mapper.Map<EmployeeCourseMappingViewModel, EmployeeCourseMapping>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<EmployeeCourseMappingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<EmployeeCourseMappingViewModel> models = new List<EmployeeCourseMappingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<EmployeeCourseMappingViewModel>> IGenericService<EmployeeCourseMappingViewModel>.GetByAny(int value)
        {
            IList<EmployeeCourseMappingViewModel> models = new List<EmployeeCourseMappingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<EmployeeCourseMapping, EmployeeCourseMappingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
