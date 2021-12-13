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
    /// <inheritdoc />
    public class DetailStructureInputService<T> : IDetailStructureInputService<DetailStructureInputViewModel>, IGenericService<DetailStructureInputViewModel>
    {
        private IGenericRepository<DetailStructureInput> GenericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IDeviceValidationService deviceValidationService;

        /// <inheritdoc />
        public DetailStructureInputService(
            IUnitOfWork unitOfWork,
            //IDeviceValidationService deviceValidationService,
            IMapper mapper, IGenericRepository<DetailStructureInput> genericRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.deviceValidationService = deviceValidationService;
            this.mapper = mapper;
            GenericRepository = genericRepository;
        }

        async Task<IEnumerable<DetailStructureInputViewModel>> IGenericService<DetailStructureInputViewModel>.GetAll()
        {
            IList<DetailStructureInputViewModel> models = new List<DetailStructureInputViewModel>();
            var getAllgs = await GenericRepository.GetAll();
            foreach (var getAll in getAllgs)
            {
                models.Add(mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(getAll));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<DetailStructureInputViewModel>> GetAll(int page, int pageSize)
        {
            IList<DetailStructureInputViewModel> models = new List<DetailStructureInputViewModel>();
            var getAllByPages = await GenericRepository.GetAll(page, pageSize);
            foreach (var getAllByPage in getAllByPages)
            {
                models.Add(mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(getAllByPage));
            }
            return models.AsEnumerable();
        }

        public async Task<IEnumerable<DetailStructureInputViewModel>> GetAll(string include)
        {
            IList<DetailStructureInputViewModel> models = new List<DetailStructureInputViewModel>();
            var getAllIncludes = await GenericRepository.GetAll();
            foreach (var getAllInclude in getAllIncludes)
            {
                models.Add(mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(getAllInclude));
            }
            return models.AsEnumerable();
        }

        public async Task<DetailStructureInputViewModel> GetById(int id)
        {
            var getId = await GenericRepository.GetById(id);
            var model = mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(getId);
            return model;
        }

        public DetailStructureInputViewModel Create(DetailStructureInputViewModel model)
        {

            var insertModel = mapper.Map<DetailStructureInputViewModel, DetailStructureInput>(model);
            var modelI = GenericRepository.Create(insertModel);
            return mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(modelI);

        }

        public void Update(int id, DetailStructureInputViewModel model)
        {
            var updateModel = mapper.Map<DetailStructureInputViewModel, DetailStructureInput>(model);
            GenericRepository.Update(id, updateModel);
        }

        public void Delete(int id)
        {
            var deleteModel = Task.Run(() => GenericRepository.GetById(id)).Result;
            deleteModel.IsActive = false;
            deleteModel.IsDeleted = true;
            GenericRepository.Update(deleteModel.Id, deleteModel);
        }

        public async Task<IEnumerable<DetailStructureInputViewModel>> GetAllWithData(int LoggedInUserId)
        {
            IList<DetailStructureInputViewModel> models = new List<DetailStructureInputViewModel>();
            var dbEntities = await GenericRepository.GetAll("pleaseAddFk");
            var dbEntity = dbEntities.FirstOrDefault(x => x.Id == LoggedInUserId);
            if (dbEntity != null)
            {
                var approvalGetAll = mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(dbEntity);
                models.Add(approvalGetAll);
            }
            return models.AsEnumerable();
        }

        async Task<IEnumerable<DetailStructureInputViewModel>> IGenericService<DetailStructureInputViewModel>.GetByAny(int value)
        {
            IList<DetailStructureInputViewModel> models = new List<DetailStructureInputViewModel>();
            var getByAnyIds = await GenericRepository.FindBy(x => x.FkMasterStructureId == value);

            foreach (var getByAnyId in getByAnyIds)
            {
                models.Add(mapper.Map<DetailStructureInput, DetailStructureInputViewModel>(getByAnyId));
            }
            return models.AsEnumerable();
        }
    }
}
