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
    public class CoursePricingService<T> : ICoursePricingService<CoursePricingViewModel>, IGenericService<CoursePricingViewModel>
    {
        private IGenericRepository<CoursePricing> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public CoursePricingService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<CoursePricing> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<CoursePricingViewModel>> IGenericService<CoursePricingViewModel>.GetAll()
        {
            IList<CoursePricingViewModel> models = new List<CoursePricingViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<CoursePricing, CoursePricingViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CoursePricingViewModel>> GetAll(int page, int pageSize)
        {
            IList<CoursePricingViewModel> models = new List<CoursePricingViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<CoursePricing, CoursePricingViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<CoursePricingViewModel>> GetAll(string include)
        {
            IList<CoursePricingViewModel> models = new List<CoursePricingViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<CoursePricing, CoursePricingViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<CoursePricingViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<CoursePricing, CoursePricingViewModel>(getId);
            return model;
        }

        public CoursePricingViewModel Create(CoursePricingViewModel model)
        {

            var insertModel = mapper.Map<CoursePricingViewModel, CoursePricing>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<CoursePricing, CoursePricingViewModel>(modelI);

        }

        public void Update(int id, CoursePricingViewModel model)
        {
            var updateModel = mapper.Map<CoursePricingViewModel, CoursePricing>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<CoursePricingViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<CoursePricingViewModel> models = new List<CoursePricingViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<CoursePricing, CoursePricingViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<CoursePricingViewModel>> IGenericService<CoursePricingViewModel>.GetByAny(int value)
        {
            IList<CoursePricingViewModel> models = new List<CoursePricingViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.Id == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<CoursePricing, CoursePricingViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
