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
    public class CourseDetailsService<T> : ICourseDetailsService<CourseDetailsViewModel>, IGenericService<CourseDetailsViewModel>
    {
        private IGenericRepository<CourseDetails> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public CourseDetailsService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<CourseDetails> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<CourseDetailsViewModel>> IGenericService<CourseDetailsViewModel>.GetAll()
        {
            IList<CourseDetailsViewModel> models = new List<CourseDetailsViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<CourseDetails, CourseDetailsViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CourseDetailsViewModel>> GetAll(int page, int pageSize)
        {
            IList<CourseDetailsViewModel> models = new List<CourseDetailsViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<CourseDetails, CourseDetailsViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CourseDetailsViewModel>> GetAll(string include)
        {
            IList<CourseDetailsViewModel> models = new List<CourseDetailsViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<CourseDetails, CourseDetailsViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<CourseDetailsViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<CourseDetails, CourseDetailsViewModel>(getId);
            return model;
        }

        public CourseDetailsViewModel Create(CourseDetailsViewModel model)
        {

            var insertModel = mapper.Map<CourseDetailsViewModel, CourseDetails>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<CourseDetails, CourseDetailsViewModel>(modelI);

        }

        public void Update(int id, CourseDetailsViewModel model)
        {
            var updateModel = mapper.Map<CourseDetailsViewModel, CourseDetails>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<CourseDetailsViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<CourseDetailsViewModel> models = new List<CourseDetailsViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<CourseDetails, CourseDetailsViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<CourseDetailsViewModel>> IGenericService<CourseDetailsViewModel>.GetByAny(int value)
        {
            IList<CourseDetailsViewModel> models = new List<CourseDetailsViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<CourseDetails, CourseDetailsViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
