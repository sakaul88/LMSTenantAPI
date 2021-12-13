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
    public class CourseAttachmentService<T> : ICourseAttachmentService<CourseAttachmentViewModel>, IGenericService<CourseAttachmentViewModel>
    {
        private IGenericRepository<CourseAttachment> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public CourseAttachmentService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<CourseAttachment> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<CourseAttachmentViewModel>> IGenericService<CourseAttachmentViewModel>.GetAll()
        {
            IList<CourseAttachmentViewModel> models = new List<CourseAttachmentViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<CourseAttachment, CourseAttachmentViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CourseAttachmentViewModel>> GetAll(int page, int pageSize)
        {
            IList<CourseAttachmentViewModel> models = new List<CourseAttachmentViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<CourseAttachment, CourseAttachmentViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CourseAttachmentViewModel>> GetAll(string include)
        {
            IList<CourseAttachmentViewModel> models = new List<CourseAttachmentViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<CourseAttachment, CourseAttachmentViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<CourseAttachmentViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<CourseAttachment, CourseAttachmentViewModel>(getId);
            return model;
        }

        public CourseAttachmentViewModel Create(CourseAttachmentViewModel model)
        {

            var insertModel = mapper.Map<CourseAttachmentViewModel, CourseAttachment>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<CourseAttachment, CourseAttachmentViewModel>(modelI);

        }

        public void Update(int id, CourseAttachmentViewModel model)
        {
            var updateModel = mapper.Map<CourseAttachmentViewModel, CourseAttachment>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<CourseAttachmentViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<CourseAttachmentViewModel> models = new List<CourseAttachmentViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<CourseAttachment, CourseAttachmentViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<CourseAttachmentViewModel>> IGenericService<CourseAttachmentViewModel>.GetByAny(int value)
        {
            IList<CourseAttachmentViewModel> models = new List<CourseAttachmentViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkCourseId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<CourseAttachment, CourseAttachmentViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
